// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRankMatchMission : WebAPI
  {
    public ReqRankMatchMission(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/mission";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class MissionProgress
    {
      public string iname;
      public int prog;
      public string rewarded_at;
    }

    [Serializable]
    public class Response
    {
      public ReqRankMatchMission.MissionProgress[] missionprogs;
    }
  }
}
