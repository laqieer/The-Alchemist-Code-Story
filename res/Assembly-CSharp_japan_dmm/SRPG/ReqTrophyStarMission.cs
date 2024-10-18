// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTrophyStarMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqTrophyStarMission : WebAPI
  {
    public ReqTrophyStarMission(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "trophy/star_mission";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class StarMission
    {
      public ReqTrophyStarMission.StarMission.Info daily;
      public ReqTrophyStarMission.StarMission.Info weekly;

      [MessagePackObject(true)]
      [Serializable]
      public class Info
      {
        public string iname;
        public int ymd;
        public int star_num;
        public int[] rewards;
      }
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public ReqTrophyStarMission.StarMission star_mission;
    }
  }
}
