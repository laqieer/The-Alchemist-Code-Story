// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLogin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ReqLogin : WebAPI
  {
    public ReqLogin(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
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
      this.serializeCompressMethod = serializeCompressMethod;
    }
  }
}
