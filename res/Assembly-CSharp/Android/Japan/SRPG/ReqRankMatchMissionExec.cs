// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMissionExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
