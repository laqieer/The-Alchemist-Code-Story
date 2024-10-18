// Decompiled with JetBrains decompiler
// Type: SRPG.GuildTrophyMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildTrophyMasterParam
  {
    public static void Deserialize(
      ref GuildTrophyParam[] refTrophy,
      ref TrophyCategoryParam[] refTrophyCategory,
      ref Dictionary<string, GuildTrophyParam> refTrophyInameDict,
      JSON_TrophyParam[] json_Trophy,
      JSON_TrophyCategoryParam[] json_TrophyCategory)
    {
      Dictionary<int, TrophyCategoryParam> refTrophyCategoryDict = new Dictionary<int, TrophyCategoryParam>();
      GuildTrophyMasterParam.Deserialize(ref refTrophyCategory, ref refTrophyCategoryDict, json_TrophyCategory);
      GuildTrophyMasterParam.Deserialize(ref refTrophy, ref refTrophyInameDict, json_Trophy, refTrophyCategoryDict);
    }

    private static void Deserialize(
      ref TrophyCategoryParam[] refTrophyCategory,
      ref Dictionary<int, TrophyCategoryParam> refTrophyCategoryDict,
      JSON_TrophyCategoryParam[] json_TrophyCategory)
    {
      if (json_TrophyCategory == null)
        return;
      List<TrophyCategoryParam> trophyCategoryParamList = new List<TrophyCategoryParam>(json_TrophyCategory.Length);
      for (int index = 0; index < json_TrophyCategory.Length; ++index)
      {
        TrophyCategoryParam trophyCategoryParam = new TrophyCategoryParam();
        if (trophyCategoryParam.Deserialize(json_TrophyCategory[index]))
        {
          trophyCategoryParamList.Add(trophyCategoryParam);
          if (!refTrophyCategoryDict.ContainsKey(trophyCategoryParam.hash_code))
            refTrophyCategoryDict.Add(trophyCategoryParam.hash_code, trophyCategoryParam);
        }
      }
      refTrophyCategory = trophyCategoryParamList.ToArray();
    }

    private static void Deserialize(
      ref GuildTrophyParam[] refTrophy,
      ref Dictionary<string, GuildTrophyParam> refTrophyInameDict,
      JSON_TrophyParam[] json_Trophy,
      Dictionary<int, TrophyCategoryParam> trophy_category_dict)
    {
      if (json_Trophy == null || trophy_category_dict.Count < 0)
        return;
      List<GuildTrophyParam> guildTrophyParamList = new List<GuildTrophyParam>(json_Trophy.Length);
      for (int index = 0; index < json_Trophy.Length; ++index)
      {
        GuildTrophyParam guildTrophyParam = new GuildTrophyParam();
        if (guildTrophyParam.Deserialize(json_Trophy[index]))
        {
          if (trophy_category_dict.ContainsKey(guildTrophyParam.category_hash_code))
            guildTrophyParam.CategoryParam = trophy_category_dict[guildTrophyParam.category_hash_code];
          guildTrophyParamList.Add(guildTrophyParam);
        }
      }
      refTrophy = guildTrophyParamList.ToArray();
      refTrophyInameDict = new Dictionary<string, GuildTrophyParam>();
      foreach (GuildTrophyParam guildTrophyParam in refTrophy)
      {
        if (!refTrophyInameDict.ContainsKey(guildTrophyParam.iname))
          refTrophyInameDict.Add(guildTrophyParam.iname, guildTrophyParam);
      }
    }
  }
}
