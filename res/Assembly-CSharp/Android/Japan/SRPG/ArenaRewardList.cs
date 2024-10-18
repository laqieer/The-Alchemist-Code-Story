// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRewardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

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
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
        return;
      this.ListItem.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardCoin == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardCoin.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardArenaCoin == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardArenaCoin.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardGold == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardGold.SetActive(false);
      if ((UnityEngine.Object) this.ListItem.RewardItem == (UnityEngine.Object) null)
        return;
      this.ListItem.RewardItem.SetActive(false);
      List<ArenaRewardParam> arp = MonoSingleton<GameManager>.Instance.MasterParam.GetArenaRewardParams();
      arp.ForEach((Action<ArenaRewardParam>) (reward =>
      {
        ArenaRewardListItem arenaRewardListItem = UnityEngine.Object.Instantiate<ArenaRewardListItem>(this.ListItem);
        arenaRewardListItem.transform.SetParent(this.ListItem.transform.parent, false);
        arenaRewardListItem.Initialize(reward, arp.IndexOf(reward) >= arp.Count - 1);
        this.AddItem((ListItemEvents) arenaRewardListItem);
      }));
    }
  }
}
