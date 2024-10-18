// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStatusInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusStatusInfo : MonoBehaviour
  {
    public Text FreeCnt;
    public Text TowerCnt;
    public Text FriendCnt;

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
      DataSource.Bind<PlayerData>(this.gameObject, MonoSingleton<GameManager>.Instance.Player);
    }
  }
}
