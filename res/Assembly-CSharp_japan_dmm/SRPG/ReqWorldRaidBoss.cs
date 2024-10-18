// Decompiled with JetBrains decompiler
// Type: SRPG.ReqWorldRaidBoss
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqWorldRaidBoss : WebAPI
  {
    public ReqWorldRaidBoss(
      string worldRaidIname,
      string bossIname,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK)
    {
      this.name = "worldraid/boss";
      this.body = WebAPI.GetRequestString<ReqWorldRaidBoss.RequestParam>(new ReqWorldRaidBoss.RequestParam()
      {
        worldraid_iname = worldRaidIname,
        boss_iname = bossIname
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string worldraid_iname;
      public string boss_iname;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JSON_WorldRaidBossDetailData boss;
    }
  }
}
