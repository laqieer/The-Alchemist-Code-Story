// Decompiled with JetBrains decompiler
// Type: SRPG.ReqStoryExChallengeCountReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqStoryExChallengeCountReset : WebAPI
  {
    public ReqStoryExChallengeCountReset(
      eResetCostType cost_type,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "btl/com/story_ex_reset";
      this.body = WebAPI.GetRequestString<ReqStoryExChallengeCountReset.RequestParam>(new ReqStoryExChallengeCountReset.RequestParam()
      {
        cost_type = (int) cost_type
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int cost_type;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public JSON_StoryExChallengeCount story_ex_challenge;
      public Json_Item[] items;
    }
  }
}
