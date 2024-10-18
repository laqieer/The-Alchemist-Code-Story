// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankUpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArenaRankUpWindow : MonoBehaviour
  {
    public Text OldRank;
    public Text NewRank;
    public Text DeltaRank;

    private void Start()
    {
      if (Object.op_Equality((Object) SceneBattle.Instance, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ArenaBattleResponse arenaBattleResponse = GlobalVars.ResultArenaBattleResponse;
      int num = instance.Player.ArenaRankBest - arenaBattleResponse.new_rank;
      DataSource.Bind<RewardData>(((Component) this).gameObject, new RewardData()
      {
        ArenaMedal = arenaBattleResponse.reward_info.arenacoin,
        Coin = arenaBattleResponse.reward_info.coin
      });
      this.OldRank.text = instance.Player.ArenaRankBest.ToString();
      this.NewRank.text = arenaBattleResponse.new_rank.ToString();
      this.DeltaRank.text = num.ToString();
      ((Behaviour) this.DeltaRank).enabled = num > 0;
      RewardWindow componentInChildren = ((Component) this).GetComponentInChildren<RewardWindow>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.Refresh();
    }
  }
}
