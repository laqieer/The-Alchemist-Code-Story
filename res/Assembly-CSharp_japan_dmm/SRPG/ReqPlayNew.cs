// Decompiled with JetBrains decompiler
// Type: SRPG.ReqPlayNew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;

#nullable disable
namespace SRPG
{
  public class ReqPlayNew : WebAPI
  {
    public ReqPlayNew(
      int debugNumber,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "playnew";
      this.body = string.Empty;
      string str = string.Empty;
      if (debugNumber > 0)
        str = "\"debug\":" + (object) debugNumber + ",";
      this.body += WebAPI.GetRequestString(str + "\"permanent_id\":\"" + MonoSingleton<GameManager>.Instance.UdId + "\"");
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }
  }
}
