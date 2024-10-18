// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLogin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
