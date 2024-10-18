// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBookmarkKakeraWindow
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
  [FlowNode.Pin(100, "クエスト選択", FlowNode.PinTypes.Output, 100)]
  public class QuestBookmarkKakeraWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mGainedQuests = new List<GameObject>();
    [SerializeField]
    private RectTransform QuestListParent;
    [SerializeField]
    private GameObject QuestListItemTemplate;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      GameParameter.UpdateAll(this.gameObject);
    }

    private void Awake()
    {
      if (!((UnityEngine.Object) this.QuestListItemTemplate != (UnityEngine.Object) null))
        return;
      this.QuestListItemTemplate.SetActive(false);
    }

    public void Refresh(UnitParam unit, IEnumerable<QuestParam> quests)
    {
      if (unit == null || quests == null)
        return;
      DataSource.Bind<UnitParam>(this.gameObject, unit, false);
      this.RefreshGainedQuests(unit, quests);
      GameParameter.UpdateAll(this.gameObject);
    }

    private void RefreshGainedQuests(UnitParam unit, IEnumerable<QuestParam> quests)
    {
      if ((UnityEngine.Object) this.QuestListItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.QuestListParent == (UnityEngine.Object) null || (unit == null || !((UnityEngine.Object) QuestDropParam.Instance != (UnityEngine.Object) null)))
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      foreach (QuestParam quest in quests)
        this.AddPanel(quest, availableQuests);
    }

    private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
    {
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
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }
  }
}
