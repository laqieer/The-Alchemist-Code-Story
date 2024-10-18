// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMissionExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRankMatchMissionExec : WebAPI
  {
    public ReqRankMatchMissionExec(string iname, Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/mission/exec";
      this.body = WebAPI.GetRequestString<ReqRankMatchMissionExec.RequestParam>(new ReqRankMatchMissionExec.RequestParam()
      {
        iname = iname
      });
      this.callback = response;
    }

    [Serializable]
    private class RequestParam
    {
      public string iname;
    }
  }
}
