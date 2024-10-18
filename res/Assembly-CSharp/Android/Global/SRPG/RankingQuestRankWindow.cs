// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestRankWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(200, "ランキングリスト更新完了", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(100, "ランキングリスト更新", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(300, "リストが選択された", FlowNode.PinTypes.Output, 300)]
  public class RankingQuestRankWindow : MonoBehaviour, IFlowInterface
  {
    public const int INPUT_LIST_UPDATE = 100;
    public const int OUTPUT_LIST_UPDATED = 200;
    public const int OUTPUT_LIST_SELECTED = 300;
    [SerializeField]
    private RankingQuestRankList m_TargetList;
    [SerializeField]
    private ScrollListController m_ScrollListController;
    [SerializeField]
    private GameObject m_RootObject;
    [SerializeField]
    private Text m_WindowTitleText;
    [SerializeField]
    private GameObject m_OwnRankBanner;
    [SerializeField]
    private GameObject m_NotRegisteredText;
    [SerializeField]
    private GameObject m_NotSummaryData;
    private RankingQuestParam m_RankingQuestParam;

    private void Start()
    {
      if (!((UnityEngine.Object) this.m_OwnRankBanner != (UnityEngine.Object) null))
        return;
      ListItemEvents component = this.m_OwnRankBanner.GetComponent<ListItemEvents>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
    }

    public void Activated(int pinID)
    {
      if (pinID != 100)
        return;
      RankingQuestParam rankingQuestParam = GlobalVars.SelectedRankingQuestParam;
      if (GlobalVars.SelectedRankingQuestParam == null)
        return;
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(rankingQuestParam.iname);
      if (quest != null && (UnityEngine.Object) this.m_WindowTitleText != (UnityEngine.Object) null)
        this.m_WindowTitleText.text = quest.name;
      this.m_RankingQuestParam = rankingQuestParam;
      this.m_ScrollListController.enabled = true;
      this.m_TargetList.OnSetUpItems();
      this.m_ScrollListController.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    public void SetData(RankingQuestUserData[] data)
    {
      this.m_TargetList.SetData(data);
      if (!((UnityEngine.Object) this.m_NotSummaryData != (UnityEngine.Object) null))
        return;
      this.m_NotSummaryData.SetActive(data == null || data.Length < 1);
    }

    public void SetData(RankingQuestUserData[] data, RankingQuestUserData ownData)
    {
      this.m_TargetList.SetData(data);
      this.SetOwnRankingData(ownData);
      if (!((UnityEngine.Object) this.m_NotSummaryData != (UnityEngine.Object) null))
        return;
      this.m_NotSummaryData.SetActive(data == null || data.Length < 1);
    }

    private void SetOwnRankingData(RankingQuestUserData ownData)
    {
      bool flag = ownData != null;
      if (!((UnityEngine.Object) this.m_OwnRankBanner != (UnityEngine.Object) null) || !((UnityEngine.Object) this.m_NotRegisteredText != (UnityEngine.Object) null))
        return;
      if (flag)
      {
        this.m_OwnRankBanner.SetActive(true);
        this.m_NotRegisteredText.SetActive(false);
        DataSource.Bind<RankingQuestUserData>(this.m_OwnRankBanner, ownData);
        DataSource.Bind<UnitData>(this.m_OwnRankBanner, ownData.m_UnitData);
        RankingQuestInfo component = this.m_OwnRankBanner.GetComponent<RankingQuestInfo>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.UpdateValue();
      }
      else
      {
        this.m_OwnRankBanner.SetActive(false);
        this.m_NotRegisteredText.SetActive(true);
      }
    }

    public void OnItemSelect(GameObject go)
    {
      RankingQuestUserData dataOfClass = DataSource.FindDataOfClass<RankingQuestUserData>(go, (RankingQuestUserData) null);
      if (dataOfClass == null)
        return;
      ReqQuestRankingPartyData data = new ReqQuestRankingPartyData();
      data.m_ScheduleID = this.m_RankingQuestParam.schedule_id;
      data.m_RankingType = this.m_RankingQuestParam.type;
      data.m_QuestID = this.m_RankingQuestParam.iname;
      data.m_TargetUID = dataOfClass.m_UID;
      DataSource.Bind<RankingQuestUserData>(this.m_RootObject, dataOfClass);
      DataSource.Bind<ReqQuestRankingPartyData>(this.m_RootObject, data);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
    }
  }
}
