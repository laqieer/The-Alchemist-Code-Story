// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;

#nullable disable
namespace SRPG
{
  public class ReqMasterParam : WebAPI
  {
    public ReqMasterParam(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "mst/10/master";
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }
  }
}
