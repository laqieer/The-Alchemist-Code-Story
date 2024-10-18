﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "詳細表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "アイテム詳細を表示", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "武具詳細を表示", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(13, "念装詳細を表示", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(12, "トラスト報酬なし", FlowNode.PinTypes.Output, 12)]
  public class ConceptCardTrustWindow : ConceptCardDetailBase, IFlowInterface
  {
    private const int PIN_SHOW_DETAIL = 0;
    private const int PIN_DETAIL_ITEM = 10;
    private const int PIN_DETAIL_ARIFACT = 11;
    private const int PIN_DETAIL_NON = 12;
    private const int PIN_DETAIL_CONCEPT_CARD = 13;
    [SerializeField]
    private GameObject mConceptCardPrefab;
    private ConceptCardEquipDetail mConceptCardEquipDetail;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.ShowDetail();
    }

    public void ShowDetail()
    {
      if (Object.op_Inequality((Object) ConceptCardManager.Instance, (Object) null))
        this.mConceptCardData = ConceptCardManager.Instance.SelectedConceptCardData;
      else
        this.mConceptCardData = (ConceptCardData) GlobalVars.SelectedConceptCardData;
      if (this.mConceptCardData == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
      }
      else
      {
        ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
        if (reward == null)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
        }
        else
        {
          bool flag = false;
          switch (reward.reward_type)
          {
            case eRewardType.Item:
              flag = this.SetItem(reward);
              break;
            case eRewardType.Artifact:
              flag = this.SetArtifact(reward);
              break;
            case eRewardType.ConceptCard:
              flag = this.SetConceptCard(reward);
              break;
          }
          if (flag)
            return;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
        }
      }
    }

    public bool SetItem(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return false;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(reward_item.iname);
      Transform parent = ((Component) this).transform.parent;
      if (Object.op_Inequality((Object) parent, (Object) null))
      {
        DataSource.Bind<ItemParam>(((Component) parent).gameObject, itemParam);
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam.iname);
        DataSource.Bind<ItemData>(((Component) parent).gameObject, itemDataByItemId);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      return true;
    }

    public bool SetArtifact(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return false;
      ArtifactDetailWindow.SetArtifactParam(MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward_item.iname));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      return true;
    }

    public bool SetConceptCard(ConceptCardTrustRewardItemParam reward_item)
    {
      if (string.IsNullOrEmpty(reward_item.iname))
        return false;
      ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(reward_item.iname).iname);
      GlobalVars.SelectedConceptCardData.Set(cardDataForDisplay);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
      return true;
    }
  }
}
