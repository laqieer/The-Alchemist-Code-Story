// Decompiled with JetBrains decompiler
// Type: SRPG.EmbeddedTutorialMasterParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class EmbeddedTutorialMasterParams
  {
    private static void SetParam<T>(object target, List<object> param, out T[] values)
    {
      values = new T[param.Count];
      for (int index = 0; index < values.Length; ++index)
        values[index] = (T) param[index];
    }

    [MessagePackObject(true)]
    public class JSON_EmbededMasterParam
    {
      private Dictionary<string, Dictionary<string, string>> DataTable;
      public JSON_UnitParam[] Unit;
      public JSON_JobSetParam[] JobSet;
      public JSON_JobParam[] Job;
      public JSON_ArtifactParam[] Artifact;
      public JSON_AbilityParam[] Ability;
      public JSON_SkillParam[] Skill;
      public JSON_BuffEffectParam[] Buff;
      public JSON_TrickParam[] Trick;
      public JSON_CondEffectParam[] Cond;
      public JSON_GrowParam[] Grow;
      public JSON_AIParam[] AI;
      public JSON_ItemParam[] Item;
      public JSON_UnitJobOverwriteParam[] UnitJobOverwrite;
      public JSON_WeaponParam[] Weapon;
      public JSON_TipsParam[] Tips;
      public JSON_GeoParam[] Geo;
      public JSON_FixParam[] Fix;
      public JSON_UnlockParam[] Unlock;
      public JSON_RarityParam[] Rarity;
      public JSON_PlayerParam[] Player;
      public int[] UnitLvTbl;
      public int[] PlayerLvTbl;

      public void Serialize<T>(List<object> param, out T[] values)
      {
        EmbeddedTutorialMasterParams.SetParam<T>((object) this, param, out values);
      }
    }

    [MessagePackObject(true)]
    public class JSON_EmbededQuestParam
    {
      public JSON_SectionParam[] worlds;
      public JSON_ChapterParam[] areas;
      public JSON_QuestParam[] quests;

      public void Serialize<T>(List<object> param, out T[] values)
      {
        EmbeddedTutorialMasterParams.SetParam<T>((object) this, param, out values);
      }
    }
  }
}
