// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2_MultiPlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Multi/クエスト一覧")]
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(50, "鍵なし", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(51, "鍵あり", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 101)]
  public class QuestListV2_MultiPlay : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 50:
          GlobalVars.EditMultiPlayRoomPassCode = (string) null;
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.QUEST_MULTI_LOCK);
          break;
        case 51:
          GlobalVars.EditMultiPlayRoomPassCode = "1";
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.QUEST_MULTI_LOCK);
          break;
      }
    }

    private void Awake()
    {
      GlobalVars.EditMultiPlayRoomPassCode = "0";
      string s = FlowNode_Variable.Get("MultiPlayPasscode");
      if (!string.IsNullOrEmpty(s))
      {
        int result = 0;
        if (int.TryParse(s, out result) && result != 0)
          this.Activated(51);
      }
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.DetailTemplate, (Object) null) || !this.DetailTemplate.activeInHierarchy)
        return;
      this.DetailTemplate.SetActive(false);
    }

    private void Start() => this.RefreshItems();

    public void Refresh()
    {
      this.RefreshItems();
      if (!Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        return;
      ListExtras component = ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.normalizedPosition = Vector2.one;
    }

    private void RefreshItems()
    {
      Transform transform = ((Component) this).transform;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null) && ((Component) child).gameObject.activeSelf)
          Object.DestroyImmediate((Object) ((Component) child).gameObject);
      }
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.Quests.Length; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.Quests[index];
        if (quest != null && (quest.type == QuestTypes.Multi || quest.IsMultiAreaQuest) && quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent && !string.IsNullOrEmpty(quest.ChapterID) && quest.ChapterID.Equals(GlobalVars.SelectedMultiPlayArea) && (quest.IsDateUnlock() || MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(quest.ChapterID)))
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
          ((Object) gameObject).hideFlags = (HideFlags) 52;
          DataSource.Bind<QuestParam>(gameObject, quest);
          ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          }
          gameObject.transform.SetParent(transform, false);
          gameObject.gameObject.SetActive(true);
        }
      }
    }

    private void OnSelectItem(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) QuestDropParam.Instance, (Object) null))
        return;
      bool flag = QuestDropParam.Instance.IsChangedQuestDrops(dataOfClass);
      GlobalVars.SetDropTableGeneratedTime();
      if (flag && !QuestDropParam.Instance.IsWarningPopupDisable)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), (UIUtility.DialogResultEvent) (obj =>
        {
          ListItemEvents component = go.GetComponent<ListItemEvents>();
          if (!Object.op_Inequality((Object) component, (Object) null))
            return;
          component.OpenDetail();
        }));
      }
      else
      {
        GlobalVars.SelectedQuestID = dataOfClass.iname;
        DebugUtility.Log("Select Quest:" + GlobalVars.SelectedQuestID);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!Object.op_Inequality((Object) this.mDetailInfo, (Object) null))
        return;
      Object.DestroyImmediate((Object) this.mDetailInfo.gameObject);
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      CanvasGroup componentInParent = ((Component) this).gameObject.GetComponentInParent<CanvasGroup>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null) && !componentInParent.interactable || !Object.op_Equality((Object) this.mDetailInfo, (Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = Object.Instantiate<GameObject>(this.DetailTemplate);
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass);
      this.mDetailInfo.SetActive(true);
    }
  }
}
