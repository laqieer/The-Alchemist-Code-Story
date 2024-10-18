// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class TobiraParam
  {
    public static readonly int MAX_TOBIRA_COUNT = 8;
    private string mUnitIname;
    private bool mEnable;
    private TobiraParam.Category mCategory;
    private string mRecipeId;
    private string mSkillIname;
    private List<TobiraLearnAbilityParam> mLearnAbilities = new List<TobiraLearnAbilityParam>();
    private string mOverwriteLeaderSkillIname;
    private int mOverwriteLeaderSkillLevel;
    private int mPriority;

    public static string GetCategoryName(TobiraParam.Category category)
    {
      switch (category)
      {
        case TobiraParam.Category.Envy:
          return LocalizedText.Get("sys.CMD_TOBIRA_ENVY");
        case TobiraParam.Category.Wrath:
          return LocalizedText.Get("sys.CMD_TOBIRA_WRATH");
        case TobiraParam.Category.Sloth:
          return LocalizedText.Get("sys.CMD_TOBIRA_SLOTH");
        case TobiraParam.Category.Lust:
          return LocalizedText.Get("sys.CMD_TOBIRA_LUST");
        case TobiraParam.Category.Gluttony:
          return LocalizedText.Get("sys.CMD_TOBIRA_GLUTTONY");
        case TobiraParam.Category.Greed:
          return LocalizedText.Get("sys.CMD_TOBIRA_GREED");
        case TobiraParam.Category.Pride:
          return LocalizedText.Get("sys.CMD_TOBIRA_PRIDE");
        default:
          return string.Empty;
      }
    }

    public string UnitIname => this.mUnitIname;

    public bool Enable => this.mEnable;

    public TobiraParam.Category TobiraCategory => this.mCategory;

    public string RecipeId => this.mRecipeId;

    public string SkillIname => this.mSkillIname;

    public TobiraLearnAbilityParam[] LeanAbilityParam => this.mLearnAbilities.ToArray();

    public string OverwriteLeaderSkillIname => this.mOverwriteLeaderSkillIname;

    public int OverwriteLeaderSkillLevel => this.mOverwriteLeaderSkillLevel;

    public int Priority => this.mPriority;

    public bool HasLeaerSkill => !string.IsNullOrEmpty(this.mOverwriteLeaderSkillIname);

    public bool IsUnlockConceptCardSlot2
    {
      get
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        if (Object.op_Equality((Object) instanceDirect, (Object) null) || instanceDirect.MasterParam == null || instanceDirect.MasterParam.FixParam == null)
        {
          DebugUtility.LogError("このプロパティはゲーム実行中に呼び出してください");
          return false;
        }
        return instanceDirect.MasterParam.FixParam.ConceptcardSlot2UnlockTobira != TobiraParam.Category.START && this.mCategory == instanceDirect.MasterParam.FixParam.ConceptcardSlot2UnlockTobira;
      }
    }

    public void Deserialize(JSON_TobiraParam json)
    {
      if (json == null)
        return;
      this.mUnitIname = json.unit_iname;
      this.mEnable = json.enable == 1;
      this.mCategory = (TobiraParam.Category) json.category;
      this.mRecipeId = json.recipe_id;
      this.mSkillIname = json.skill_iname;
      this.mLearnAbilities.Clear();
      if (json.learn_abils != null)
      {
        for (int index = 0; index < json.learn_abils.Length; ++index)
        {
          TobiraLearnAbilityParam learnAbilityParam = new TobiraLearnAbilityParam();
          learnAbilityParam.Deserialize(json.learn_abils[index]);
          this.mLearnAbilities.Add(learnAbilityParam);
        }
      }
      this.mOverwriteLeaderSkillIname = json.overwrite_ls_iname;
      if (!string.IsNullOrEmpty(this.mOverwriteLeaderSkillIname))
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        if (Object.op_Inequality((Object) instanceDirect, (Object) null) && instanceDirect.MasterParam != null)
          this.mOverwriteLeaderSkillLevel = (int) instanceDirect.MasterParam.FixParam.TobiraLvCap;
      }
      this.mPriority = json.priority;
    }

    public enum Category
    {
      START = 0,
      Unlock = 0,
      Envy = 1,
      Wrath = 2,
      Sloth = 3,
      Lust = 4,
      Gluttony = 5,
      Greed = 6,
      Pride = 7,
      MAX = 8,
    }
  }
}
