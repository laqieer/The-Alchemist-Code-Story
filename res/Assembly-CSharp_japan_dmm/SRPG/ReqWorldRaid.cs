// Decompiled with JetBrains decompiler
// Type: SRPG.ReqWorldRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqWorldRaid : WebAPI
  {
    public ReqWorldRaid(
      int lastLogId,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK)
    {
      this.name = "worldraid";
      this.body = WebAPI.GetRequestString<ReqWorldRaid.RequestParam>(new ReqWorldRaid.RequestParam()
      {
        last_log_id = lastLogId
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int last_log_id;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_WorldRaidBossChallengedData[] bosses;
      public int total_challenge;
      public JSON_WorldRaidLogData[] logs;
      public int refresh_wait_sec;
      public int auto_refresh_wait_sec;
    }
  }
}
