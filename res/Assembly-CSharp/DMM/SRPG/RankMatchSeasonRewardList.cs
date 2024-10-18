// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchSeasonRewardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RankMatchSeasonRewardList : SRPG_ListBase
  {
    [SerializeField]
    private RankMatchClassListItem ListItem;

    protected override void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        return;
      ((Component) this.ListItem).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardUnit, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardUnit.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardItem, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardItem.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardCard, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardCard.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardArtifact, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardArtifact.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardAward, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardAward.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardGold, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardGold.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardCoin, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardCoin.SetActive(false);
      GameManager gm = MonoSingleton<GameManager>.Instance;
      RankMatchClassListItem item = UnityEngine.Object.Instantiate<RankMatchClassListItem>(this.ListItem);
      this.AddItem((ListItemEvents) item);
      GlobalVars.RankMatchSeasonReward.ForEach((Action<List<VersusRankReward>>) (vrc =>
      {
        ((Component) item).transform.SetParent(((Component) this).transform, false);
        ((Component) item).gameObject.SetActive(true);
        vrc.ForEach((Action<VersusRankReward>) (reward =>
        {
          bool flag = false;
          GameObject gameObject;
          switch (reward.Type)
          {
            case RewardType.Item:
              ItemParam itemParam = gm.GetItemParam(reward.IName);
              if (itemParam == null)
                return;
              gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardItem);
              DataSource.Bind<ItemParam>(gameObject, itemParam);
              flag = true;
              break;
            case RewardType.Gold:
              gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardGold);
              flag = true;
              break;
            case RewardType.Coin:
              gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardCoin);
              flag = true;
              break;
            case RewardType.Artifact:
              ArtifactParam artifactParam = gm.MasterParam.GetArtifactParam(reward.IName);
              if (artifactParam == null)
                return;
              gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardArtifact);
              DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
              break;
            case RewardType.Unit:
              UnitParam unitParam = gm.GetUnitParam(reward.IName);
              if (unitParam == null)
                return;
              gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardUnit);
              DataSource.Bind<UnitParam>(gameObject, unitParam);
              break;
            case RewardType.Award:
              AwardParam awardParam = gm.GetAwardParam(reward.IName);
              if (awardParam == null)
                return;
              gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardAward);
              DataSource.Bind<AwardParam>(gameObject, awardParam);
              break;
            default:
              return;
          }
          if (flag)
          {
            Transform transform = gameObject.transform.Find("amount/Text_amount");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
            {
              Text component = ((Component) transform).GetComponent<Text>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                component.text = reward.Num.ToString();
            }
          }
          gameObject.transform.SetParent(item.RewardList, false);
          gameObject.SetActive(true);
        }));
      }));
    }
  }
}
