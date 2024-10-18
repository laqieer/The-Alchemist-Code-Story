// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
