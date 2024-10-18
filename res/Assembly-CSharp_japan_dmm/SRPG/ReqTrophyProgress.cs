// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTrophyProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;

#nullable disable
namespace SRPG
{
  public class ReqTrophyProgress : WebAPI
  {
    public ReqTrophyProgress(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "trophy";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }
  }
}
