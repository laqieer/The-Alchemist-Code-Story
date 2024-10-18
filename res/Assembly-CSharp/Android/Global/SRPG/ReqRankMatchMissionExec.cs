// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMissionExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

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
