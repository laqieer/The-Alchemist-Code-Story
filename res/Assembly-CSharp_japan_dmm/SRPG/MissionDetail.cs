// Decompiled with JetBrains decompiler
// Type: SRPG.MissionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MissionDetail : MonoBehaviour
  {
    [SerializeField]
    private QuestMissionItem ItemTemplate;
    [SerializeField]
    private QuestMissionItem UnitTemplate;
    [SerializeField]
    private QuestMissionItem ArtifactTemplate;
    [SerializeField]
    private QuestMissionItem ConceptCardTemplate;
    [SerializeField]
    private QuestMissionItem NothingRewardTemplate;
    [SerializeField]
    private GameObject ContentsParent;
    [SerializeField]
    private GameObject Window;
    [SerializeField]
    private ScrollRect ScrollRect;
    [SerializeField]
    private GameObject Scrollbar;

    private void Awake()
    {
      QuestParam questParam = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
      if (questParam == null && Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
      if (questParam == null || questParam.bonusObjective == null)
        return;
      if (questParam.bonusObjective.Length > 3)
      {
        if (Object.op_Inequality((Object) this.Scrollbar, (Object) null))
          this.Scrollbar.SetActive(true);
        if (Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        {
          this.ScrollRect.horizontal = false;
          this.ScrollRect.vertical = true;
        }
        if (Object.op_Equality((Object) this.Window, (Object) null))
          return;
        RectTransform transform = this.Window.transform as RectTransform;
        if (Object.op_Inequality((Object) transform, (Object) null))
          transform.sizeDelta = new Vector2(transform.sizeDelta.x, transform.sizeDelta.y + 120f);
      }
      else
      {
        if (Object.op_Inequality((Object) this.Scrollbar, (Object) null))
          this.Scrollbar.SetActive(false);
        if (Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        {
          this.ScrollRect.horizontal = false;
          this.ScrollRect.vertical = false;
        }
      }
      this.RefreshQuestMissionReward(questParam);
    }

    private void RefreshQuestMissionReward(QuestParam questParam)
    {
      if (questParam.bonusObjective == null)
        return;
      for (int index = 0; index < questParam.bonusObjective.Length; ++index)
      {
        QuestBonusObjective questBonusObjective = questParam.bonusObjective[index];
        QuestMissionItem questMissionItem;
        if (questBonusObjective.itemType == RewardType.Artifact)
          questMissionItem = Object.Instantiate<GameObject>(((Component) this.ArtifactTemplate).gameObject).GetComponent<QuestMissionItem>();
        else if (questBonusObjective.itemType == RewardType.ConceptCard)
        {
          questMissionItem = Object.Instantiate<GameObject>(((Component) this.ConceptCardTemplate).gameObject).GetComponent<QuestMissionItem>();
          ConceptCardIcon component = ((Component) questMissionItem).gameObject.GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(questBonusObjective.item);
            component.Setup(cardDataForDisplay);
          }
        }
        else if (questBonusObjective.itemType == RewardType.Nothing)
        {
          questMissionItem = Object.Instantiate<GameObject>(((Component) this.NothingRewardTemplate).gameObject).GetComponent<QuestMissionItem>();
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(questBonusObjective.item);
          if (itemParam != null)
            questMissionItem = itemParam.type != EItemType.Unit ? Object.Instantiate<GameObject>(((Component) this.ItemTemplate).gameObject).GetComponent<QuestMissionItem>() : Object.Instantiate<GameObject>(((Component) this.UnitTemplate).gameObject).GetComponent<QuestMissionItem>();
          else
            continue;
        }
        if (!Object.op_Equality((Object) questMissionItem, (Object) null))
        {
          if (Object.op_Inequality((Object) questMissionItem.Star, (Object) null))
            questMissionItem.Star.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.FrameParam, (Object) null))
            questMissionItem.FrameParam.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.IconParam, (Object) null))
            questMissionItem.IconParam.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.NameParam, (Object) null))
            questMissionItem.NameParam.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.AmountParam, (Object) null))
            questMissionItem.AmountParam.Index = index;
          if (Object.op_Inequality((Object) questMissionItem.ObjectigveParam, (Object) null))
            questMissionItem.ObjectigveParam.Index = index;
          ((Component) questMissionItem).gameObject.SetActive(true);
          ((Component) questMissionItem).transform.SetParent(this.ContentsParent.transform, false);
          GameParameter.UpdateAll(((Component) questMissionItem).gameObject);
        }
      }
    }
  }
}
