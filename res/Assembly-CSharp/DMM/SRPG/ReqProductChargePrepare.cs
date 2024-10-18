// Decompiled with JetBrains decompiler
// Type: SRPG.ReqProductChargePrepare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;

#nullable disable
namespace SRPG
{
  public class ReqProductChargePrepare : WebAPI
  {
    public ReqProductChargePrepare(
      string ID,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "charge/prepare";
      this.body = string.Empty;
      ReqProductChargePrepare productChargePrepare = this;
      productChargePrepare.body = productChargePrepare.body + "\"id\":\"" + ID + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }
  }
}
