// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStatusInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusStatusInfo : MonoBehaviour
  {
    private readonly string COIN_NAME = "IT_EVENT_VS";
    public Text FreeCnt;
    public Text TowerCnt;
    public Text FriendCnt;
    public Text VSCoinCnt;
    public Text FreeRate;
    public Text TowerRate;
    public Text FriendRate;

    private void Start() => this.RefreshData();

    private void RefreshData()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (Object.op_Inequality((Object) this.FreeCnt, (Object) null))
        this.FreeCnt.text = player.VersusFreeWinCnt.ToString();
      if (Object.op_Inequality((Object) this.TowerCnt, (Object) null))
        this.TowerCnt.text = player.VersusTowerWinCnt.ToString();
      if (Object.op_Inequality((Object) this.FriendCnt, (Object) null))
        this.FriendCnt.text = player.VersusFriendWinCnt.ToString();
      ItemData itemDataByItemId = player.FindItemDataByItemID(this.COIN_NAME);
      if (itemDataByItemId != null && Object.op_Inequality((Object) this.VSCoinCnt, (Object) null))
        this.VSCoinCnt.text = itemDataByItemId.Num.ToString();
      if (Object.op_Inequality((Object) this.FreeRate, (Object) null))
        this.FreeRate.text = this.GenerateWinRateString(player.VersusFreeWinCnt, player.VersusFreeCnt);
      if (Object.op_Inequality((Object) this.TowerRate, (Object) null))
        this.TowerRate.text = this.GenerateWinRateString(player.VersusTowerWinCnt, player.VersusTowerCnt);
      if (Object.op_Inequality((Object) this.FriendRate, (Object) null))
        this.FriendRate.text = this.GenerateWinRateString(player.VersusFriendWinCnt, player.VersusFriendCnt);
      DataSource.Bind<PlayerData>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.Player);
    }

    private string GenerateWinRateString(int wincnt, int totalcnt)
    {
      float num = 0.0f;
      if (wincnt > 0)
        num = (float) ((double) wincnt / (double) totalcnt * 100.0);
      return (double) num >= 100.0 ? "100" : num.ToString("F1");
    }
  }
}
