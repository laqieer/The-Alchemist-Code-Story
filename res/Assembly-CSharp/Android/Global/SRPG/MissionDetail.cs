﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MissionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
    private GameObject ContentsParent;
    [SerializeField]
    private GameObject Window;
    [SerializeField]
    private ScrollRect ScrollRect;
    [SerializeField]
    private GameObject Scrollbar;

    private void Awake()
    {
      QuestParam questParam = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      if (questParam == null && (UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
        questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
      if (questParam == null || questParam.bonusObjective == null)
        return;
      if (questParam.bonusObjective.Length > 3)
      {
        if ((UnityEngine.Object) this.Scrollbar != (UnityEngine.Object) null)
          this.Scrollbar.SetActive(true);
        if ((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null)
        {
          this.ScrollRect.horizontal = false;
          this.ScrollRect.vertical = true;
        }
        if ((UnityEngine.Object) this.Window == (UnityEngine.Object) null)
          return;
        RectTransform transform = this.Window.transform as RectTransform;
        if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
          transform.sizeDelta = new Vector2(transform.sizeDelta.x, transform.sizeDelta.y + 120f);
      }
      else
      {
        if ((UnityEngine.Object) this.Scrollbar != (UnityEngine.Object) null)
          this.Scrollbar.SetActive(false);
        if ((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null)
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
        QuestMissionItem questMissionItem;
        if (questParam.bonusObjective[index].itemType == RewardType.Artifact)
        {
          questMissionItem = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactTemplate.gameObject).GetComponent<QuestMissionItem>();
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(questParam.bonusObjective[index].item);
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
          questMissionItem.gameObject.SetActive(true);
          questMissionItem.transform.SetParent(this.ContentsParent.transform, false);
          GameParameter.UpdateAll(questMissionItem.gameObject);
        }
      }
    }
  }
}
