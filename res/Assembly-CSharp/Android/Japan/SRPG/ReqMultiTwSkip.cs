// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwSkip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqMultiTwSkip : WebAPI
  {
    public ReqMultiTwSkip(string tower_id, int skip_floor, Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/skip";
      this.body = WebAPI.GetRequestString<ReqMultiTwSkip.RequestParam>(new ReqMultiTwSkip.RequestParam()
      {
        tower_id = tower_id,
        floor = skip_floor
      });
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public ReqMultiTwStatus.FloorParam[] floors;
      public Json_PlayerDataAll player;
    }

    [Serializable]
    public class RequestParam
    {
      public string tower_id;
      public int floor;
    }
  }
}
