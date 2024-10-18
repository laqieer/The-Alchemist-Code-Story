﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "表示更新", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "表示更新(完了)", FlowNode.PinTypes.Output, 100)]
  public class QuestDetail : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_REFRESH_UI = 10;
    private const int OUTPUT_REFRESH_UI = 100;
    [HeaderBar("▼ミッション報酬表示用オブジェクトの親")]
    [SerializeField]
    private RectTransform m_ContentRoot;
    [HeaderBar("▼ミッション報酬表示用テンプレート")]
    [SerializeField]
    private QuestMissionItem m_RewardItemTemplate;
    [SerializeField]
    private QuestMissionItem m_RewardUnitTemplate;
    [SerializeField]
    private QuestMissionItem m_RewardArtifactTemplate;
    [SerializeField]
    private QuestMissionItem m_RewardCardTemplate;
    [SerializeField]
    private QuestMissionItem m_RewardNothingTemplate;
    [HeaderBar("▼ミッション報酬が何も設定されていない時に表示するオブジェクト")]
    [SerializeField]
    private GameObject m_NoMissionText;
    [SerializeField]
    private bool m_RefreshOnStart = true;
    [HeaderBar("▼ミッションの表示順(デフォルト=ソートしない)")]
    [SerializeField]
    private QuestDetail.SortOrder m_SortOrder;
    [HeaderBar("▼勝利条件、クエスト情報、クエストミッション、入手アイテムへのアクセサ")]
    [SerializeField]
    public GameObject GoCondition;
    [SerializeField]
    public GameObject GoLimit;
    [SerializeField]
    public GameObject GoMission;
    [SerializeField]
    public GameObject GoDropList;
    private List<QuestDetail.ViewParam> m_ListItems = new List<QuestDetail.ViewParam>();

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.m_RewardItemTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_RewardUnitTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_RewardArtifactTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_RewardCardTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_RewardNothingTemplate, false);
      GameUtility.SetGameObjectActive(this.m_NoMissionText, false);
      if (!this.m_RefreshOnStart)
        return;
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void Refresh()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
      if (dataOfClass == null)
        return;
      if (!dataOfClass.HasMission())
      {
        GameUtility.SetGameObjectActive(this.m_NoMissionText, true);
      }
      else
      {
        this.CreateItems(dataOfClass);
        this.SortItems();
      }
    }

    private void CreateItems(QuestParam questParam)
    {
      if (questParam.bonusObjective == null)
        return;
      for (int index = 0; index < questParam.bonusObjective.Length; ++index)
      {
        QuestBonusObjective bonusObjective = questParam.bonusObjective[index];
        QuestMissionItem rewardItem = this.CreateRewardItem(bonusObjective);
        if (bonusObjective.itemType == RewardType.ConceptCard)
        {
          ConceptCardIcon componentInChildren = ((Component) rewardItem).gameObject.GetComponentInChildren<ConceptCardIcon>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          {
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(bonusObjective.item);
            componentInChildren.Setup(cardDataForDisplay);
          }
        }
        rewardItem.SetGameParameterIndex(index);
        this.m_ListItems.Add(new QuestDetail.ViewParam()
        {
          ListItem = rewardItem,
          MissionIndex = index,
          IsAchieved = questParam.IsMissionClear(index)
        });
        GameParameter.UpdateAll(((Component) rewardItem).gameObject);
      }
    }

    private QuestMissionItem CreateRewardItem(QuestBonusObjective bonusObjective)
    {
      if (bonusObjective == null)
        return (QuestMissionItem) null;
      QuestMissionItem questMissionItem;
      if (bonusObjective.itemType == RewardType.Artifact)
        questMissionItem = this.m_RewardArtifactTemplate;
      else if (bonusObjective.itemType == RewardType.ConceptCard)
        questMissionItem = this.m_RewardCardTemplate;
      else if (bonusObjective.itemType == RewardType.Nothing)
      {
        questMissionItem = this.m_RewardNothingTemplate;
      }
      else
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(bonusObjective.item);
        if (itemParam == null)
          return (QuestMissionItem) null;
        questMissionItem = itemParam.type != EItemType.Unit ? this.m_RewardItemTemplate : this.m_RewardUnitTemplate;
      }
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((Component) questMissionItem).gameObject);
      gameObject.SetActive(true);
      gameObject.transform.SetParent(((Component) this.m_ContentRoot).transform, false);
      return gameObject.GetComponent<QuestMissionItem>();
    }

    private void SortItems()
    {
      if (this.m_SortOrder == QuestDetail.SortOrder.None)
        return;
      if (this.m_SortOrder == QuestDetail.SortOrder.AchievemedToUnachieved)
        this.m_ListItems.Sort((Comparison<QuestDetail.ViewParam>) ((item1, item2) => this.Compare_AchievemedToUnachieved(item1, item2)));
      else if (this.m_SortOrder == QuestDetail.SortOrder.UnachievedToAchievemed)
        this.m_ListItems.Sort((Comparison<QuestDetail.ViewParam>) ((item1, item2) => this.Compare_UnachievedToAchievemed(item1, item2)));
      for (int index = 0; index < this.m_ListItems.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_ListItems[index].ListItem, (UnityEngine.Object) null))
          ((Component) this.m_ListItems[index].ListItem).transform.SetSiblingIndex(index);
      }
    }

    private int Compare_AchievemedToUnachieved(
      QuestDetail.ViewParam viewParam1,
      QuestDetail.ViewParam viewParam2)
    {
      if (viewParam1 == null)
        return -1;
      if (viewParam2 == null)
        return 1;
      if (viewParam1 == viewParam2)
        return 0;
      if (viewParam1.IsAchieved == viewParam2.IsAchieved)
        return viewParam1.MissionIndex.CompareTo(viewParam2.MissionIndex);
      return viewParam1.IsAchieved ? -1 : 1;
    }

    private int Compare_UnachievedToAchievemed(
      QuestDetail.ViewParam viewParam1,
      QuestDetail.ViewParam viewParam2)
    {
      if (viewParam1 == null)
        return -1;
      if (viewParam2 == null)
        return 1;
      if (viewParam1 == viewParam2)
        return 0;
      if (viewParam1.IsAchieved == viewParam2.IsAchieved)
        return viewParam1.MissionIndex.CompareTo(viewParam2.MissionIndex);
      return !viewParam1.IsAchieved ? -1 : 1;
    }

    public enum SortOrder
    {
      None,
      AchievemedToUnachieved,
      UnachievedToAchievemed,
    }

    private class ViewParam
    {
      private QuestMissionItem m_ListItem;
      private int m_MissionIndex;
      private bool m_IsAchieved;

      public QuestMissionItem ListItem
      {
        get => this.m_ListItem;
        set => this.m_ListItem = value;
      }

      public int MissionIndex
      {
        get => this.m_MissionIndex;
        set => this.m_MissionIndex = value;
      }

      public bool IsAchieved
      {
        get => this.m_IsAchieved;
        set => this.m_IsAchieved = value;
      }
    }
  }
}
