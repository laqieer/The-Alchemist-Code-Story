// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusFriendScore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusFriendScore : WebAPI
  {
    public ReqVersusFriendScore(Network.ResponseCallback response)
    {
      this.name = "vs/towermatch/friend_score";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public Json_VersusFriendScore[] friends;
    }
  }
}
