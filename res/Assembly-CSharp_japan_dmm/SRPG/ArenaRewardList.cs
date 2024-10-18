// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRewardList
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
  public class ArenaRewardList : SRPG_ListBase
  {
    [SerializeField]
    private ArenaRewardListItem ListItem;

    protected override void Start()
    {
      this.Refresh();
      base.Start();
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        return;
      ((Component) this.ListItem).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardCoin, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardCoin.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardArenaCoin, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardArenaCoin.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardGold, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardGold.SetActive(false);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem.RewardItem, (UnityEngine.Object) null))
        return;
      this.ListItem.RewardItem.SetActive(false);
      List<ArenaRewardParam> arp = MonoSingleton<GameManager>.Instance.MasterParam.GetArenaRewardParams();
      arp.ForEach((Action<ArenaRewardParam>) (reward =>
      {
        ArenaRewardListItem arenaRewardListItem = UnityEngine.Object.Instantiate<ArenaRewardListItem>(this.ListItem);
        ((Component) arenaRewardListItem).transform.SetParent(((Component) this.ListItem).transform.parent, false);
        arenaRewardListItem.Initialize(reward, arp.IndexOf(reward) >= arp.Count - 1);
        this.AddItem((ListItemEvents) arenaRewardListItem);
      }));
    }
  }
}
