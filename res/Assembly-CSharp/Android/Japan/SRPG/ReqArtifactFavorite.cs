// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
