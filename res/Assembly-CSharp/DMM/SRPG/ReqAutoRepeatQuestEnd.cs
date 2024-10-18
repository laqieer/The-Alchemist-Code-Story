// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAutoRepeatQuestEnd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqAutoRepeatQuestEnd : WebAPI
  {
    public ReqAutoRepeatQuestEnd(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "btl/auto_repeat/end";
      this.body = WebAPI.GetRequestString<ReqAutoRepeatQuestEnd.RequestParam>((ReqAutoRepeatQuestEnd.RequestParam) null);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public int start_index;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_AutoRepeatQuestData auto_repeat;
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public Json_BtlRewardConceptCard[] cards;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
      public JSON_TrophyProgress[] guild_trophies;
      public JSON_QuestProgress[] quests;
      public JSON_ChapterCount area;
      public int guildraid_bp_charge;
      public int rune_storage_used;
      public JSON_StoryExChallengeCount story_ex_challenge;
      public Json_RuneData[] runes_detail;
    }
  }
}
