// Decompiled with JetBrains decompiler
// Type: SRPG.BanStatusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
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
        this.Title.text = "ログイン制限";
      if ((UnityEngine.Object) this.Message != (UnityEngine.Object) null)
        this.Message.text = "再びログイン可能になるのは以下の日時です。";
      if ((UnityEngine.Object) this.LimitDate != (UnityEngine.Object) null)
      {
        int banStatus = GlobalVars.BanStatus;
        this.LimitDate.text = banStatus != 1 ? new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double) banStatus).ToString() : "無期限ログイン不可";
      }
      if (!((UnityEngine.Object) this.CustomerID != (UnityEngine.Object) null))
        return;
      this.CustomerID.text = GlobalVars.CustomerID;
    }
  }
}
