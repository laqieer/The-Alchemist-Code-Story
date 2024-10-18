// Decompiled with JetBrains decompiler
// Type: SRPG.BanStatusWindowBelgium
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BanStatusWindowBelgium : MonoBehaviour
  {
    public Text Title;
    public Text Message;
    public Text PlayerName;
    public Text LimitDate;
    public Text PaidGemBalance;
    public Text CustomerID;

    private void Awake()
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.Title != (UnityEngine.Object) null)
        this.Title.text = LocalizedText.Get("sys.BAN_USER_TITLE_BELGIUM");
      if ((UnityEngine.Object) this.Message != (UnityEngine.Object) null)
        this.Message.text = LocalizedText.Get("sys.BAN_USER_MESSAGE_BELGIUM");
      if ((UnityEngine.Object) this.PlayerName != (UnityEngine.Object) null)
        this.PlayerName.text = !PlayerPrefs.HasKey("PlayerName") ? string.Empty : LocalizedText.Get("sys.CONTACT_PLAYER_NAME") + ": " + PlayerPrefs.GetString("PlayerName");
      if ((UnityEngine.Object) this.PaidGemBalance != (UnityEngine.Object) null)
        this.PaidGemBalance.text = LocalizedText.Get("sys.CONFIG_CHECKCOIN_PAY") + ": " + (object) MonoSingleton<GameManager>.Instance.Player.PaidCoin;
      if ((UnityEngine.Object) this.LimitDate != (UnityEngine.Object) null)
      {
        int banStatus = GlobalVars.BanStatus;
        this.LimitDate.text = banStatus != 1 ? TimeManager.FromUnixTime((long) banStatus).ToString() : LocalizedText.Get("sys.BAN_USER_INDEFINITE");
      }
      if (!((UnityEngine.Object) this.CustomerID != (UnityEngine.Object) null))
        return;
      this.CustomerID.text = GlobalVars.CustomerID;
    }
  }
}
