// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankUpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArenaRankUpWindow : MonoBehaviour
  {
    public Text OldRank;
    public Text NewRank;
    public Text DeltaRank;

    private void Start()
    {
      if ((UnityEngine.Object) SceneBattle.Instance == (UnityEngine.Object) null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ArenaBattleResponse arenaBattleResponse = GlobalVars.ResultArenaBattleResponse;
      int num = instance.Player.ArenaRankBest - arenaBattleResponse.new_rank;
      DataSource.Bind<RewardData>(this.gameObject, new RewardData()
      {
        ArenaMedal = arenaBattleResponse.reward_info.arenacoin,
        Coin = arenaBattleResponse.reward_info.coin
      }, false);
      this.OldRank.text = instance.Player.ArenaRankBest.ToString();
      this.NewRank.text = arenaBattleResponse.new_rank.ToString();
      this.DeltaRank.text = num.ToString();
      this.DeltaRank.enabled = num > 0;
      RewardWindow componentInChildren = this.GetComponentInChildren<RewardWindow>();
      if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
        return;
      componentInChildren.Refresh();
    }
  }
}
