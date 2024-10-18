// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRankMatch : WebAPI
  {
    public ReqRankMatch(
      string iname,
      int plid,
      int seat,
      string uid,
      Network.ResponseCallback response)
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
