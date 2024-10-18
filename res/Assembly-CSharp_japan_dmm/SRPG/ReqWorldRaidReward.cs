// Decompiled with JetBrains decompiler
// Type: SRPG.ReqWorldRaidReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqWorldRaidReward : WebAPI
  {
    public ReqWorldRaidReward(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK)
    {
      this.name = "worldraid/reward";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    public enum RewardStatus
    {
      None,
      Granted,
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public ReqWorldRaidReward.RewardStatus status;
    }
  }
}
