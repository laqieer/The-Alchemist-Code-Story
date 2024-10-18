// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
