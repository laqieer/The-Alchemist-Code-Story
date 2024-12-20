﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchClassList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RankMatchClassList : SRPG_ListBase
  {
    [SerializeField]
    private RankMatchClassListItem ListItem;

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
        return;
      this.ListItem.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardUnit == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardUnit.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardItem == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardItem.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardCard == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardCard.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardArtifact == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardArtifact.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardAward == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardAward.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardGold == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardGold.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardCoin == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardCoin.SetActive(false);
      GameManager gm = MonoSingleton<GameManager>.Instance;
      gm.GetVersusRankClassList(gm.RankMatchScheduleId).ForEach((Action<VersusRankClassParam>) (vrc =>
      {
        RankMatchClassListItem item = UnityEngine.Object.Instantiate<RankMatchClassListItem>(this.ListItem);
        DataSource.Bind<VersusRankClassParam>(item.gameObject, vrc, false);
        this.AddItem((ListItemEvents) item);
        item.transform.SetParent(this.transform, false);
        item.gameObject.SetActive(true);
        gm.GetVersusRankClassRewardList(vrc.RewardId).ForEach((Action<VersusRankReward>) (reward =>
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
              DataSource.Bind<ItemParam>(gameObject, itemParam, false);
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
              DataSource.Bind<ArtifactParam>(gameObject, artifactParam, false);
              break;
            case RewardType.Unit:
              UnitParam unitParam = gm.GetUnitParam(reward.IName);
              if (unitParam == null)
                return;
              gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardUnit);
              DataSource.Bind<UnitParam>(gameObject, unitParam, false);
              break;
            case RewardType.Award:
              AwardParam awardParam = gm.GetAwardParam(reward.IName);
              if (awardParam == null)
                return;
              gameObject = UnityEngine.Object.Instantiate<GameObject>(item.RewardAward);
              DataSource.Bind<AwardParam>(gameObject, awardParam, false);
              break;
            default:
              return;
          }
          if (flag)
          {
            Transform transform = gameObject.transform.Find("amount/Text_amount");
            if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
            {
              Text component = transform.GetComponent<Text>();
              if ((UnityEngine.Object) component != (UnityEngine.Object) null)
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
