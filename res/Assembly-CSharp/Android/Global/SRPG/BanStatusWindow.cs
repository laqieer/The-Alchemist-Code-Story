// Decompiled with JetBrains decompiler
// Type: SRPG.BanStatusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BanStatusWindow : MonoBehaviour
  {
    public Text Title;
    public Text LimitDate;
    public Text Message;
    public Text CustomerID;

    private void Awake()
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.Title != (UnityEngine.Object) null)
        this.Title.text = LocalizedText.Get("sys.BAN_USER_TITLE");
      if ((UnityEngine.Object) this.Message != (UnityEngine.Object) null)
        this.Message.text = LocalizedText.Get("sys.BAN_USER_MESSAGE");
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
