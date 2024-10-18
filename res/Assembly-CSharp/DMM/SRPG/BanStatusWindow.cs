// Decompiled with JetBrains decompiler
// Type: SRPG.BanStatusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Title, (UnityEngine.Object) null))
        this.Title.text = "ログイン制限";
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Message, (UnityEngine.Object) null))
        this.Message.text = "再びログイン可能になるのは以下の日時です。";
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitDate, (UnityEngine.Object) null))
      {
        int banStatus = GlobalVars.BanStatus;
        this.LimitDate.text = banStatus != 1 ? new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double) banStatus).ToString() : "無期限ログイン不可";
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CustomerID, (UnityEngine.Object) null))
        return;
      this.CustomerID.text = GlobalVars.CustomerID;
    }
  }
}
