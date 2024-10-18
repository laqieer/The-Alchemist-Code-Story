// Decompiled with JetBrains decompiler
// Type: SRPG.BattleResultMissionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleResultMissionDetail : MonoBehaviour
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
    private ScrollRect ScrollRect;
    [SerializeField]
    private VerticalLayoutGroup VerticalLayout;
    private static readonly float WaitTime = 0.2f;
    private List<GameObject> allStarObjects = new List<GameObject>();
    private Coroutine lastCoroutine;
    private float m_ItemHeight;

    private void Awake()
    {
      QuestParam questParam = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
      if (questParam == null && Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
      if (questParam == null)
        return;
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
          Rect rect = (((Component) questMissionItem).transform as RectTransform).rect;
          this.m_ItemHeight = ((Rect) ref rect).height;
          ((Component) questMissionItem).gameObject.SetActive(true);
          ((Component) questMissionItem).transform.SetParent(this.ContentsParent.transform, false);
          this.allStarObjects.Add(((Component) questMissionItem.Star).gameObject);
          GameParameter.UpdateAll(((Component) questMissionItem).gameObject);
        }
      }
      if (!Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        return;
      this.ScrollRect.verticalNormalizedPosition = 1f;
      this.ScrollRect.horizontalNormalizedPosition = 1f;
    }

    public List<GameObject> GetObjectiveStars() => this.allStarObjects;

    public float MoveTo(int index)
    {
      if (Object.op_Equality((Object) this.ScrollRect, (Object) null))
        return 0.0f;
      if (this.lastCoroutine != null)
        this.StopCoroutine(this.lastCoroutine);
      this.lastCoroutine = this.StartCoroutine(this.MoveScrollCoroutine(index));
      return BattleResultMissionDetail.WaitTime;
    }

    [DebuggerHidden]
    private IEnumerator MoveScrollCoroutine(int index)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new BattleResultMissionDetail.\u003CMoveScrollCoroutine\u003Ec__Iterator0()
      {
        index = index,
        \u0024this = this
      };
    }
  }
}
