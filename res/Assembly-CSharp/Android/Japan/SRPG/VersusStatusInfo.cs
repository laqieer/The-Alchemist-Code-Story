// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStatusInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
      this.RefreshData();
    }

    private void RefreshData()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if ((UnityEngine.Object) this.FreeCnt != (UnityEngine.Object) null)
        this.FreeCnt.text = player.VersusFreeWinCnt.ToString();
      if ((UnityEngine.Object) this.TowerCnt != (UnityEngine.Object) null)
        this.TowerCnt.text = player.VersusTowerWinCnt.ToString();
      if ((UnityEngine.Object) this.FriendCnt != (UnityEngine.Object) null)
        this.FriendCnt.text = player.VersusFriendWinCnt.ToString();
      ItemData itemDataByItemId = player.FindItemDataByItemID(this.COIN_NAME);
      if (itemDataByItemId != null && (UnityEngine.Object) this.VSCoinCnt != (UnityEngine.Object) null)
        this.VSCoinCnt.text = itemDataByItemId.Num.ToString();
      if ((UnityEngine.Object) this.FreeRate != (UnityEngine.Object) null)
        this.FreeRate.text = this.GenerateWinRateString(player.VersusFreeWinCnt, player.VersusFreeCnt);
      if ((UnityEngine.Object) this.TowerRate != (UnityEngine.Object) null)
        this.TowerRate.text = this.GenerateWinRateString(player.VersusTowerWinCnt, player.VersusTowerCnt);
      if ((UnityEngine.Object) this.FriendRate != (UnityEngine.Object) null)
        this.FriendRate.text = this.GenerateWinRateString(player.VersusFriendWinCnt, player.VersusFriendCnt);
      DataSource.Bind<PlayerData>(this.gameObject, MonoSingleton<GameManager>.Instance.Player, false);
    }

    private string GenerateWinRateString(int wincnt, int totalcnt)
    {
      float num = 0.0f;
      if (wincnt > 0)
        num = (float) ((double) wincnt / (double) totalcnt * 100.0);
      if ((double) num >= 100.0)
        return "100";
      return num.ToString("F1");
    }
  }
}
