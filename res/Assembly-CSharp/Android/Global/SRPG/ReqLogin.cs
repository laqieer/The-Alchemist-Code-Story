// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLogin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ReqLogin : WebAPI
  {
    public ReqLogin(Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"device\":\"");
      stringBuilder.Append(SystemInfo.deviceModel);
      stringBuilder.Append("\",");
      string str = AssetManager.Format.ToPath().Replace("/", string.Empty);
      stringBuilder.Append("\"dlc\":\"");
      stringBuilder.Append(str);
      stringBuilder.Append("\"");
      this.name = "login";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
