// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwSkip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
