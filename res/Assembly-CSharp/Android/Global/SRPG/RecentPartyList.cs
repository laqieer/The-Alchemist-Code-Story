﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RecentPartyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Output", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  public class RecentPartyList : MonoBehaviour, IFlowInterface
  {
    private List<UnitData[]> allParties = new List<UnitData[]>();
    private List<SupportData> allHelpUnits = new List<SupportData>();
    private List<int[]> allAchieves = new List<int[]>();
    private List<ItemData[]> allUsedItems = new List<ItemData[]>();
    private List<RecentPartyPanel> allTeamPanel = new List<RecentPartyPanel>();
    [SerializeField]
    private RecentPartyPanel PartyPanelTemplate;
    [SerializeField]
    private GameObject PartyPanelHolder;
    [SerializeField]
    private Text ErrorText;
    [SerializeField]
    private ScrollRect ScrollRect;
    [SerializeField]
    private SRPG_Button PrevButton;
    [SerializeField]
    private SRPG_Button NextButton;
    [SerializeField]
    private GameObject PageInfo;
    [SerializeField]
    private Text CurrentPage;
    [SerializeField]
    private Text MaxPage;
    [SerializeField]
    private GameObject CheckBox;
    private QuestParam mCurrentQuest;
    private bool mIsHeloOnly;
    private RecentPartyList.JSON_List[] mLastRecords;
    private bool mOwnedUnitOnly;
    private int mCurrentPage;
    private int mMaxPage;
    private int mUnitId;

    public void Activated(int pinID)
    {
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.mCurrentPage = 1;
      this.Connect();
    }

    private void Connect()
    {
      Network.RequestAPI((WebAPI) new ReqBtlComRecord(this.mCurrentQuest.iname, this.mCurrentPage, this.mUnitId, new Network.ResponseCallback(this.ResponseCallback)), false);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.BattleRecordMaintenance:
          case Network.EErrCode.RecordLimitUpload:
            Network.RemoveAPI();
            Network.ResetError();
            this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_ERROR_UPLOAD_LIMIT_2"));
            this.DisableUnnecessaryUIOnError();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<RecentPartyList.JSON_Body> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<RecentPartyList.JSON_Body>>(www.text);
        this.mLastRecords = jsonObject.body.list;
        this.mMaxPage = jsonObject.body.option.totalPage;
        Network.RemoveAPI();
        this.Refresh();
      }
    }

    private void Refresh()
    {
      this.Initialize();
      if (this.mLastRecords == null)
      {
        this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_NOT_FOUND"));
        this.DisableUnnecessaryUIOnError();
      }
      else
      {
        this.LoadAllParties(this.mLastRecords);
        if (this.mOwnedUnitOnly)
        {
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          for (int index = 0; index < this.allParties.Count && index < this.allTeamPanel.Count; ++index)
          {
            foreach (UnitData unitData in this.allParties[index])
            {
              if (unitData != null && player.FindUnitDataByUnitID(unitData.UnitParam.iname) == null)
                this.allTeamPanel[index].gameObject.SetActive(false);
            }
          }
        }
        if (this.allTeamPanel.All<RecentPartyPanel>((Func<RecentPartyPanel, bool>) (panel => !panel.gameObject.activeSelf)))
          this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_NOT_FOUND_OWNED_UNIT"));
        this.SetActiveUICoponent();
        this.CurrentPage.text = this.mCurrentPage.ToString();
        this.MaxPage.text = this.mMaxPage.ToString();
      }
    }

    private void Initialize()
    {
      this.allParties.Clear();
      this.allHelpUnits.Clear();
      this.allAchieves.Clear();
      using (List<RecentPartyPanel>.Enumerator enumerator = this.allTeamPanel.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) enumerator.Current.gameObject);
      }
      this.allTeamPanel.Clear();
      if ((UnityEngine.Object) this.PartyPanelTemplate != (UnityEngine.Object) null)
        this.PartyPanelTemplate.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.ErrorText != (UnityEngine.Object) null)
        this.ErrorText.gameObject.SetActive(false);
      this.ScrollRect.verticalNormalizedPosition = 1f;
      this.ScrollRect.horizontalNormalizedPosition = 1f;
    }

    private void DisableUnnecessaryUIOnError()
    {
      if ((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null)
        this.ScrollRect.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.PageInfo != (UnityEngine.Object) null)
        this.PageInfo.SetActive(false);
      if ((UnityEngine.Object) this.PrevButton != (UnityEngine.Object) null)
        this.PrevButton.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.NextButton != (UnityEngine.Object) null)
        this.NextButton.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this.CheckBox != (UnityEngine.Object) null))
        return;
      this.CheckBox.gameObject.SetActive(false);
    }

    private void EnableErrorText(string errorMessage)
    {
      if (!((UnityEngine.Object) this.ErrorText != (UnityEngine.Object) null))
        return;
      this.ErrorText.gameObject.SetActive(true);
      this.ErrorText.text = errorMessage;
    }

    private void SetActiveUICoponent()
    {
      if ((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null)
        this.ScrollRect.gameObject.SetActive(true);
      if ((UnityEngine.Object) this.PageInfo != (UnityEngine.Object) null)
        this.PageInfo.SetActive(true);
      if ((UnityEngine.Object) this.PrevButton != (UnityEngine.Object) null)
      {
        this.PrevButton.gameObject.SetActive(true);
        this.PrevButton.interactable = this.mCurrentPage > 1;
      }
      if ((UnityEngine.Object) this.NextButton != (UnityEngine.Object) null)
      {
        this.NextButton.gameObject.SetActive(true);
        this.NextButton.interactable = this.mCurrentPage < this.mMaxPage;
      }
      if (!((UnityEngine.Object) this.CheckBox != (UnityEngine.Object) null))
        return;
      this.CheckBox.gameObject.SetActive(true);
    }

    private void LoadAllParties(RecentPartyList.JSON_List[] winRecords)
    {
      int num = 0;
      foreach (RecentPartyList.JSON_List winRecord in winRecords)
      {
        int index = num;
        SRPG_Button.ButtonClickEvent buttonClickEvent = (SRPG_Button.ButtonClickEvent) (b => this.OnButtonClick(index));
        if (winRecord.id > this.mUnitId)
          this.mUnitId = winRecord.id;
        UnitData[] party = this.LoadParty((IEnumerable<Json_Unit>) winRecord.detail.my.units);
        SupportData support = this.LoadHelpUnit(winRecord.detail.help);
        ItemData[] itemDataArray = this.LoadUsedItems(winRecord.detail.items);
        this.allParties.Add(party);
        this.allHelpUnits.Add(support);
        this.allAchieves.Add(winRecord.achieved);
        this.allUsedItems.Add(itemDataArray);
        RecentPartyPanel partyPanel = this.CreatePartyPanel(party, support, winRecord, buttonClickEvent);
        this.allTeamPanel.Add(partyPanel);
        partyPanel.transform.SetParent(this.PartyPanelHolder.gameObject.transform, false);
        SRPG_Button component = partyPanel.GetComponent<SRPG_Button>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.AddListener(buttonClickEvent);
        ++num;
      }
    }

    private UnitData[] LoadParty(IEnumerable<Json_Unit> jsonUnit)
    {
      List<UnitData> unitDataList = new List<UnitData>();
      foreach (Json_Unit json in jsonUnit)
      {
        UnitData unitData = new UnitData();
        if (string.IsNullOrEmpty(json.iname))
        {
          unitDataList.Add((UnitData) null);
        }
        else
        {
          unitData.Deserialize(json);
          unitDataList.Add(unitData);
        }
      }
      return unitDataList.ToArray();
    }

    private SupportData LoadHelpUnit(Json_Support json)
    {
      if (json == null)
        return (SupportData) null;
      SupportData supportData = new SupportData();
      supportData.Deserialize(json);
      return supportData;
    }

    private ItemData[] LoadUsedItems(RecentPartyList.JSON_Item[] jsonItem)
    {
      if (jsonItem == null)
        return (ItemData[]) null;
      List<ItemData> itemDataList = new List<ItemData>();
      foreach (RecentPartyList.JSON_Item jsonItem1 in jsonItem)
      {
        ItemData itemData = new ItemData();
        itemData.Setup(0L, jsonItem1.iname, jsonItem1.num);
        itemDataList.Add(itemData);
      }
      return itemDataList.ToArray();
    }

    private RecentPartyPanel CreatePartyPanel(UnitData[] party, SupportData support, RecentPartyList.JSON_List record, SRPG_Button.ButtonClickEvent buttonClickCallback)
    {
      RecentPartyPanel component = UnityEngine.Object.Instantiate<GameObject>(this.PartyPanelTemplate.gameObject).GetComponent<RecentPartyPanel>();
      component.SetPartyInfo(party, support, this.mCurrentQuest);
      component.SetUnitIconPressedCallback(buttonClickCallback);
      if ((UnityEngine.Object) component.UserName != (UnityEngine.Object) null)
        component.UserName.text = record.detail.my.name;
      if ((UnityEngine.Object) component.Level != (UnityEngine.Object) null)
        component.Level.text = record.detail.my.lv.ToString();
      if ((UnityEngine.Object) component.ClearDate != (UnityEngine.Object) null)
        component.ClearDate.text = this.GetClearedTime(record.created_at);
      if (component.ConditionStars != null)
      {
        for (int index = 0; index < record.achieved.Length && index < component.ConditionStars.Length; ++index)
          component.ConditionStars[index].SetActive(record.achieved[index] != 0);
      }
      if (component.ConditionItems != null && this.mCurrentQuest.type == QuestTypes.Tower && component.ConditionItems.Length >= 3)
        component.ConditionItems[2].SetActive(false);
      component.gameObject.SetActive(true);
      return component;
    }

    private void OnButtonClick(int index)
    {
      if (index < 0 || index >= this.allParties.Count || index >= this.allAchieves.Count)
        return;
      GlobalVars.UserSelectionPartyDataInfo = new GlobalVars.UserSelectionPartyData()
      {
        unitData = this.allParties[index],
        supportData = this.allHelpUnits[index],
        achievements = this.allAchieves[index],
        usedItems = this.allUsedItems[index]
      };
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private string GetClearedTime(string iso8601String)
    {
      CultureInfo cultureInfo = new CultureInfo("ja-JP");
      DateTime result;
      if (DateTime.TryParseExact(iso8601String, TimeManager.ISO_8601_FORMAT, (IFormatProvider) cultureInfo, DateTimeStyles.None, out result))
        return result.ToString("yyyy/M/d");
      return string.Empty;
    }

    public void OnToggleEnabled()
    {
      this.mOwnedUnitOnly = true;
      this.Refresh();
    }

    public void OnToggleDisabled()
    {
      this.mOwnedUnitOnly = false;
      this.Refresh();
    }

    public void OnPrevButtonPressed()
    {
      if (this.mCurrentPage - 1 < 1)
        return;
      --this.mCurrentPage;
      this.Connect();
    }

    public void OnNextButtonPressed()
    {
      if (this.mCurrentPage + 1 > this.mMaxPage)
        return;
      ++this.mCurrentPage;
      this.Connect();
    }

    public class JSON_Body
    {
      public RecentPartyList.JSON_List[] list;
      public RecentPartyList.JSON_Option option;
    }

    public class JSON_Option
    {
      public int totalPage;
    }

    public class JSON_List
    {
      public int id;
      public int[] achieved;
      public string created_at;
      public RecentPartyList.JSON_Detail detail;
    }

    public class JSON_Detail
    {
      public RecentPartyList.JSON_My my;
      public Json_Support help;
      public RecentPartyList.JSON_Item[] items;
    }

    public class JSON_My
    {
      public int lv;
      public string name;
      public Json_Unit[] units;
    }

    public class JSON_Item
    {
      public string iname;
      public int num;
    }
  }
}
