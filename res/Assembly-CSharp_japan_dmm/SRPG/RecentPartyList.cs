// Decompiled with JetBrains decompiler
// Type: SRPG.RecentPartyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Output", FlowNode.PinTypes.Output, 100)]
  public class RecentPartyList : MonoBehaviour, IFlowInterface
  {
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
    private List<RecentPartyPanel> m_RecentPartyPanels = new List<RecentPartyPanel>();
    private List<RecentPartyList.ViewParam> m_ViewParams = new List<RecentPartyList.ViewParam>();
    private QuestParam m_CurrentQuest;
    private bool m_OwnedUnitOnly;
    private int m_CurrentPage;
    private int m_MaxPage;
    private int m_UnitId;

    private bool IsNoData => this.m_ViewParams.Count < 1;

    public void Activated(int pinID)
    {
      this.m_CurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.m_CurrentPage = 1;
      this.Connect();
    }

    private void Initialize()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PartyPanelTemplate, (UnityEngine.Object) null))
        ((Component) this.PartyPanelTemplate).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ErrorText, (UnityEngine.Object) null))
        ((Component) this.ErrorText).gameObject.SetActive(false);
      this.ScrollRect.verticalNormalizedPosition = 1f;
      this.ScrollRect.horizontalNormalizedPosition = 1f;
    }

    private void Refresh()
    {
      this.DestroyPanels();
      this.Initialize();
      this.CreatePanels();
      if (this.IsNoData)
      {
        this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_NOT_FOUND"));
        this.DisableUnnecessaryUIOnError();
      }
      else
      {
        if (this.m_OwnedUnitOnly)
        {
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          for (int index = 0; index < this.m_ViewParams.Count && index < this.m_RecentPartyPanels.Count; ++index)
          {
            foreach (UnitData unit in this.m_ViewParams[index].m_Units)
            {
              if (unit != null && player.FindUnitDataByUnitID(unit.UnitParam.iname) == null)
                ((Component) this.m_RecentPartyPanels[index]).gameObject.SetActive(false);
            }
          }
        }
        if (this.m_RecentPartyPanels.All<RecentPartyPanel>((Func<RecentPartyPanel, bool>) (panel => !((Component) panel).gameObject.activeSelf)))
          this.EnableErrorText(LocalizedText.Get("sys.PARTYEDITOR_RECENT_CLEARED_PARTY_NOT_FOUND_OWNED_UNIT"));
        this.SetActiveUICoponent();
        this.CurrentPage.text = this.m_CurrentPage.ToString();
        this.MaxPage.text = this.m_MaxPage.ToString();
      }
    }

    private void DisableUnnecessaryUIOnError()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        ((Component) this.ScrollRect).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageInfo, (UnityEngine.Object) null))
        this.PageInfo.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
        ((Component) this.PrevButton).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        ((Component) this.NextButton).gameObject.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CheckBox, (UnityEngine.Object) null))
        return;
      this.CheckBox.gameObject.SetActive(false);
    }

    private void EnableErrorText(string errorMessage)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ErrorText, (UnityEngine.Object) null))
        return;
      ((Component) this.ErrorText).gameObject.SetActive(true);
      this.ErrorText.text = errorMessage;
    }

    private void SetActiveUICoponent()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        ((Component) this.ScrollRect).gameObject.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageInfo, (UnityEngine.Object) null))
        this.PageInfo.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
      {
        ((Component) this.PrevButton).gameObject.SetActive(true);
        ((Selectable) this.PrevButton).interactable = this.m_CurrentPage > 1;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
      {
        ((Component) this.NextButton).gameObject.SetActive(true);
        ((Selectable) this.NextButton).interactable = this.m_CurrentPage < this.m_MaxPage;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CheckBox, (UnityEngine.Object) null))
        return;
      this.CheckBox.gameObject.SetActive(true);
    }

    private string GetClearedTime(string iso8601String)
    {
      CultureInfo provider = new CultureInfo("ja-JP");
      DateTime result;
      return DateTime.TryParseExact(iso8601String, TimeManager.ISO_8601_FORMAT, (IFormatProvider) provider, DateTimeStyles.None, out result) ? result.ToString("yyyy/M/d") : string.Empty;
    }

    private RecentPartyPanel CreatePartyPanel(RecentPartyList.ViewParam viewParam, int index)
    {
      SRPG_Button.ButtonClickEvent buttonClickEvent = (SRPG_Button.ButtonClickEvent) (b => this.OnButtonClick(index));
      RecentPartyPanel component1 = UnityEngine.Object.Instantiate<GameObject>(((Component) this.PartyPanelTemplate).gameObject).GetComponent<RecentPartyPanel>();
      component1.SetPartyInfo(viewParam.m_Units, viewParam.m_Support, this.m_CurrentQuest);
      component1.SetUnitIconPressedCallback(buttonClickEvent);
      SRPG_Button component2 = ((Component) component1).GetComponent<SRPG_Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        component2.AddListener(buttonClickEvent);
      component1.SetUserName(viewParam.m_Name);
      component1.SetUserRank(viewParam.m_Level.ToString());
      component1.SetClearDate(this.GetClearedTime(viewParam.m_CreatedAt));
      for (int index1 = 0; index1 < viewParam.m_Achieves.Length; ++index1)
        component1.SetConditionStarActive(index1, viewParam.m_Achieves[index1] != 0);
      if (this.m_CurrentQuest.type == QuestTypes.Tower)
        component1.SetConditionItemActive(2, false);
      ((Component) component1).gameObject.SetActive(true);
      return component1;
    }

    private void CreatePanels()
    {
      for (int index = 0; index < this.m_ViewParams.Count; ++index)
      {
        RecentPartyPanel partyPanel = this.CreatePartyPanel(this.m_ViewParams[index], index);
        ((Component) partyPanel).transform.SetParent(this.PartyPanelHolder.gameObject.transform, false);
        this.m_RecentPartyPanels.Add(partyPanel);
      }
    }

    private void DestroyPanels()
    {
      foreach (Component recentPartyPanel in this.m_RecentPartyPanels)
        UnityEngine.Object.Destroy((UnityEngine.Object) recentPartyPanel.gameObject);
      this.m_RecentPartyPanels.Clear();
    }

    private void Connect()
    {
      Network.RequestAPI((WebAPI) new ReqBtlComRecord(this.m_CurrentQuest.iname, this.m_CurrentPage, this.m_UnitId, new Network.ResponseCallback(this.ResponseCallback)));
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
        this.m_MaxPage = jsonObject.body.option.totalPage;
        Network.RemoveAPI();
        this.Deserialize(jsonObject.body.list);
        this.Refresh();
      }
    }

    private void Deserialize(RecentPartyList.JSON_List[] winRecords)
    {
      this.m_ViewParams.Clear();
      if (winRecords == null)
        return;
      foreach (RecentPartyList.JSON_List winRecord in winRecords)
      {
        if (winRecord.id > this.m_UnitId)
          this.m_UnitId = winRecord.id;
        this.m_ViewParams.Add(new RecentPartyList.ViewParam()
        {
          m_Units = this.Deserialize_Party(winRecord.detail.my.units),
          m_Support = this.Deserialize_Support(winRecord.detail.help),
          m_UsedItems = this.Deserialize_UsedItems(winRecord.detail.items),
          m_Achieves = winRecord.achieved,
          m_Level = winRecord.detail.my.lv,
          m_Name = winRecord.detail.my.name,
          m_CreatedAt = winRecord.created_at
        });
      }
    }

    private UnitData[] Deserialize_Party(Json_Unit[] jsonUnit)
    {
      List<UnitData> unitDataList = new List<UnitData>();
      foreach (Json_Unit json in jsonUnit)
      {
        UnitData unitData = new UnitData();
        if (json == null || string.IsNullOrEmpty(json.iname))
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

    private SupportData Deserialize_Support(Json_Support json)
    {
      if (json == null)
        return (SupportData) null;
      SupportData supportData = new SupportData();
      supportData.Deserialize(json);
      return supportData;
    }

    private ItemData[] Deserialize_UsedItems(RecentPartyList.JSON_Item[] jsonItem)
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

    private void OnButtonClick(int index)
    {
      if (index < 0 || index >= this.m_ViewParams.Count)
        return;
      RecentPartyList.ViewParam viewParam = this.m_ViewParams[index];
      GlobalVars.UserSelectionPartyDataInfo = new GlobalVars.UserSelectionPartyData()
      {
        unitData = viewParam.m_Units,
        supportData = viewParam.m_Support,
        achievements = viewParam.m_Achieves,
        usedItems = viewParam.m_UsedItems
      };
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void OnToggleEnabled()
    {
      this.m_OwnedUnitOnly = true;
      this.Refresh();
    }

    public void OnToggleDisabled()
    {
      this.m_OwnedUnitOnly = false;
      this.Refresh();
    }

    public void OnPrevButtonPressed()
    {
      if (this.m_CurrentPage - 1 < 1)
        return;
      --this.m_CurrentPage;
      this.Connect();
    }

    public void OnNextButtonPressed()
    {
      if (this.m_CurrentPage + 1 > this.m_MaxPage)
        return;
      ++this.m_CurrentPage;
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

    private class ViewParam
    {
      public UnitData[] m_Units;
      public SupportData m_Support;
      public ItemData[] m_UsedItems;
      public int[] m_Achieves;
      public string m_CreatedAt = string.Empty;
      public string m_Name = string.Empty;
      public int m_Level;
    }
  }
}
