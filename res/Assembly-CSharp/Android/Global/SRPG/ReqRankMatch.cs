// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRankMatch : WebAPI
  {
    public ReqRankMatch(string iname, int plid, int seat, string uid, Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/req";
      this.body = WebAPI.GetRequestString<ReqRankMatch.RequestParam>(new ReqRankMatch.RequestParam()
      {
        iname = iname,
        token = GlobalVars.SelectedMultiPlayRoomName,
        plid = plid,
        seat = seat,
        uid = uid
      });
      this.callback = response;
    }

    [Serializable]
    private class RequestParam
    {
      public string iname;
      public string token;
      public int plid;
      public int seat;
      public string uid;
    }
  }
}
