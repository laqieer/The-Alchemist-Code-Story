// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAdditionalParam
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
  public class SkillAdditionalParam
  {
    private string mIname;
    private SkillAdditionalParam.eAddtionalCond mCond;
    private string mSkillId;
    private SkillParam mSkill;

    public string Iname => this.mIname;

    public SkillAdditionalParam.eAddtionalCond Cond => this.mCond;

    public string SkillId => this.mSkillId;

    public SkillParam Skill
    {
      get
      {
        if (this.mSkill == null)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (Object.op_Implicit((Object) instance) && instance.MasterParam != null)
            this.mSkill = instance.MasterParam.GetSkillParam(this.mSkillId);
        }
        return this.mSkill;
      }
    }

    public void Deserialize(JSON_SkillAdditionalParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mCond = (SkillAdditionalParam.eAddtionalCond) json.cond;
      this.mSkillId = json.skill;
    }

    public static void Deserialize(
      ref List<SkillAdditionalParam> list,
      JSON_SkillAdditionalParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<SkillAdditionalParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        SkillAdditionalParam skillAdditionalParam = new SkillAdditionalParam();
        skillAdditionalParam.Deserialize(json[index]);
        list.Add(skillAdditionalParam);
      }
    }

    public enum eAddtionalCond : byte
    {
      None,
      TargetDefeat,
    }
  }
}
