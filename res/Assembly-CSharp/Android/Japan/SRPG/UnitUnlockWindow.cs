﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  public class UnitUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mGainedQuests = new List<GameObject>();
    public GameObject QuestList;
    public RectTransform QuestListParent;
    public GameObject QuestListItemTemplate;
    public Text TxtTitle;
    public Text TxtComment;
    public Text TxtQuestNothing;
    public GameObject GOUnlockLimit;
    public Button BtnDecide;
    public Button BtnCancel;
    private UnitParam UnlockUnit;

    private void Start()
    {
      if ((UnityEngine.Object) this.QuestListItemTemplate != (UnityEngine.Object) null)
        this.QuestListItemTemplate.SetActive(false);
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      string unlockUnitId = GlobalVars.UnlockUnitID;
      this.UnlockUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(unlockUnitId);
      DataSource.Bind<UnitParam>(this.gameObject, this.UnlockUnit, false);
      UnitData unitDataByUnitId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUnitID(unlockUnitId);
      if (unitDataByUnitId != null)
        DataSource.Bind<UnitData>(this.gameObject, unitDataByUnitId, false);
      bool flag = false;
      if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(this.UnlockUnit) == null)
        flag = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.UnlockUnit.piece) >= this.UnlockUnit.GetUnlockNeedPieces();
      if ((UnityEngine.Object) this.QuestList != (UnityEngine.Object) null)
        this.QuestList.SetActive(!flag);
      if ((UnityEngine.Object) this.BtnDecide != (UnityEngine.Object) null)
        this.BtnDecide.gameObject.SetActive(flag);
      if ((UnityEngine.Object) this.BtnCancel != (UnityEngine.Object) null)
        this.BtnCancel.gameObject.SetActive(flag);
      if (flag)
      {
        if ((UnityEngine.Object) this.TxtTitle != (UnityEngine.Object) null)
          this.TxtTitle.text = LocalizedText.Get("sys.UNIT_UNLOCK_TITLE");
        if ((UnityEngine.Object) this.TxtComment != (UnityEngine.Object) null)
        {
          this.TxtComment.text = LocalizedText.Get("sys.UNIT_UNLOCK_COMMENT");
          this.TxtComment.gameObject.SetActive(true);
        }
      }
      else
      {
        if ((UnityEngine.Object) this.TxtTitle != (UnityEngine.Object) null)
          this.TxtTitle.text = LocalizedText.Get("sys.UNIT_GAINED_QUEST_TITLE");
        if ((UnityEngine.Object) this.TxtComment != (UnityEngine.Object) null)
        {
          this.TxtComment.text = LocalizedText.Get("sys.UNIT_GAINED_COMMENT");
          this.TxtComment.gameObject.SetActive(this.mGainedQuests.Count == 0);
        }
        if ((UnityEngine.Object) this.GOUnlockLimit != (UnityEngine.Object) null && MonoSingleton<GameManager>.Instance.MasterParam.GetUnitUnlockTimeParam(this.UnlockUnit.unlock_time) != null)
          this.GOUnlockLimit.SetActive(true);
        this.RefreshGainedQuests(this.UnlockUnit);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void RefreshGainedQuests(UnitParam unit)
    {
      this.ClearPanel();
      this.QuestList.SetActive(false);
      if ((UnityEngine.Object) this.QuestListItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.QuestListParent == (UnityEngine.Object) null)
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.UnlockUnit.piece);
      DataSource.Bind<ItemParam>(this.QuestList, itemParam, false);
      this.QuestList.SetActive(true);
      this.SetScrollTop();
      if (!((UnityEngine.Object) QuestDropParam.Instance != (UnityEngine.Object) null))
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      List<QuestParam> itemDropQuestList = QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime());
      foreach (QuestParam questparam in itemDropQuestList)
        this.AddPanel(questparam, availableQuests);
      if (itemDropQuestList.Count != 0 || !((UnityEngine.Object) this.TxtQuestNothing != (UnityEngine.Object) null))
        return;
      this.TxtQuestNothing.gameObject.SetActive(true);
    }

    private void SetScrollTop()
    {
      RectTransform component = this.QuestListParent.GetComponent<RectTransform>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      Vector2 anchoredPosition = component.anchoredPosition;
      anchoredPosition.y = 0.0f;
      component.anchoredPosition = anchoredPosition;
    }

    public void ClearPanel()
    {
      this.mGainedQuests.Clear();
      for (int index = 0; index < this.QuestListParent.childCount; ++index)
      {
        GameObject gameObject = this.QuestListParent.GetChild(index).gameObject;
        if ((UnityEngine.Object) this.QuestListItemTemplate != (UnityEngine.Object) gameObject)
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
    }

    private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
    {
      this.QuestList.SetActive(true);
      if (questparam == null || questparam.IsMulti)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestListItemTemplate);
      SRPG_Button component1 = gameObject.GetComponent<SRPG_Button>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        component1.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
      this.mGainedQuests.Add(gameObject);
      Button component2 = gameObject.GetComponent<Button>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
      {
        bool flag1 = questparam.IsDateUnlock(-1L);
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questparam)) != null;
        component2.interactable = flag1 && flag2;
      }
      DataSource.Bind<QuestParam>(gameObject, questparam, false);
      gameObject.transform.SetParent((Transform) this.QuestListParent, false);
      gameObject.SetActive(true);
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[this.mGainedQuests.IndexOf(button.gameObject)], (QuestParam) null);
      if (quest == null)
        return;
      if (!quest.IsDateUnlock(-1L))
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }
  }
}