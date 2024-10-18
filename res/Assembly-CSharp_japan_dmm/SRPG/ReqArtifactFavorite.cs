// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqArtifactFavorite : WebAPI
  {
    public ReqArtifactFavorite(long iid, bool isFavorite, Network.ResponseCallback response)
    {
      this.name = "unit/job/artifact/favorite";
      this.body = WebAPI.GetRequestString("\"iid\":" + (object) iid + ",\"fav\":" + (object) (!isFavorite ? 0 : 1));
      this.callback = response;
    }
  }
}
