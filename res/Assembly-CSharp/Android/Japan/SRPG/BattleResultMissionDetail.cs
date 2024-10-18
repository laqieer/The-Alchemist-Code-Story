// Decompiled with JetBrains decompiler
// Type: SRPG.BattleResultMissionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleResultMissionDetail : MonoBehaviour
  {
    private static readonly float WaitTime = 0.2f;
    private List<GameObject> allStarObjects = new List<GameObject>();
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
    private Coroutine lastCoroutine;
    private float m_ItemHeight;

    private void Awake()
    {
      QuestParam questParam = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      if (questParam == null && (UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
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
          questMissionItem = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactTemplate.gameObject).GetComponent<QuestMissionItem>();
        else if (questBonusObjective.itemType == RewardType.ConceptCard)
        {
          questMissionItem = UnityEngine.Object.Instantiate<GameObject>(this.ConceptCardTemplate.gameObject).GetComponent<QuestMissionItem>();
          ConceptCardIcon component = questMissionItem.gameObject.GetComponent<ConceptCardIcon>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(questBonusObjective.item);
            component.Setup(cardDataForDisplay);
          }
        }
        else if (questBonusObjective.itemType == RewardType.Nothing)
        {
          questMissionItem = UnityEngine.Object.Instantiate<GameObject>(this.NothingRewardTemplate.gameObject).GetComponent<QuestMissionItem>();
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(questBonusObjective.item);
          if (itemParam != null)
            questMissionItem = itemParam.type != EItemType.Unit ? UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate.gameObject).GetComponent<QuestMissionItem>() : UnityEngine.Object.Instantiate<GameObject>(this.UnitTemplate.gameObject).GetComponent<QuestMissionItem>();
          else
            continue;
        }
        if (!((UnityEngine.Object) questMissionItem == (UnityEngine.Object) null))
        {
          if ((UnityEngine.Object) questMissionItem.Star != (UnityEngine.Object) null)
            questMissionItem.Star.Index = index;
          if ((UnityEngine.Object) questMissionItem.FrameParam != (UnityEngine.Object) null)
            questMissionItem.FrameParam.Index = index;
          if ((UnityEngine.Object) questMissionItem.IconParam != (UnityEngine.Object) null)
            questMissionItem.IconParam.Index = index;
          if ((UnityEngine.Object) questMissionItem.NameParam != (UnityEngine.Object) null)
            questMissionItem.NameParam.Index = index;
          if ((UnityEngine.Object) questMissionItem.AmountParam != (UnityEngine.Object) null)
            questMissionItem.AmountParam.Index = index;
          if ((UnityEngine.Object) questMissionItem.ObjectigveParam != (UnityEngine.Object) null)
            questMissionItem.ObjectigveParam.Index = index;
          this.m_ItemHeight = (questMissionItem.transform as RectTransform).rect.height;
          questMissionItem.gameObject.SetActive(true);
          questMissionItem.transform.SetParent(this.ContentsParent.transform, false);
          this.allStarObjects.Add(questMissionItem.Star.gameObject);
          GameParameter.UpdateAll(questMissionItem.gameObject);
        }
      }
      if (!((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null))
        return;
      this.ScrollRect.verticalNormalizedPosition = 1f;
      this.ScrollRect.horizontalNormalizedPosition = 1f;
    }

    public List<GameObject> GetObjectiveStars()
    {
      return this.allStarObjects;
    }

    public float MoveTo(int index)
    {
      if ((UnityEngine.Object) this.ScrollRect == (UnityEngine.Object) null)
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
      return (IEnumerator) new BattleResultMissionDetail.\u003CMoveScrollCoroutine\u003Ec__Iterator0() { index = index, \u0024this = this };
    }
  }
}
