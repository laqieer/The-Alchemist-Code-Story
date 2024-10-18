// Decompiled with JetBrains decompiler
// Type: SRPG.RewardListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RewardListItem : ListItemEvents
  {
    public GameObject RewardUnit;
    public GameObject RewardItem;
    public GameObject RewardCard;
    public GameObject RewardArtifact;
    public GameObject RewardAward;
    public GameObject RewardGold;
    public GameObject RewardCoin;
    public GameObject RewardEmblem;
    public Transform RewardList;

    public void AllNotActive()
    {
      GameUtility.SetGameObjectActive(this.RewardUnit, false);
      GameUtility.SetGameObjectActive(this.RewardItem, false);
      GameUtility.SetGameObjectActive(this.RewardCard, false);
      GameUtility.SetGameObjectActive(this.RewardArtifact, false);
      GameUtility.SetGameObjectActive(this.RewardAward, false);
      GameUtility.SetGameObjectActive(this.RewardGold, false);
      GameUtility.SetGameObjectActive(this.RewardCoin, false);
      GameUtility.SetGameObjectActive(this.RewardEmblem, false);
    }
  }
}
