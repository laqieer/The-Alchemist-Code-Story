// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraLearnAbilityParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class TobiraLearnAbilityParam
  {
    private string mAbilityIname;
    private int mLevel;
    private TobiraLearnAbilityParam.AddType mAddType;
    private string mTargeJob;
    private string mAbilityOverwrite;

    public string AbilityIname => this.mAbilityIname;

    public int Level => this.mLevel;

    public TobiraLearnAbilityParam.AddType AbilityAddType => this.mAddType;

    public string TargetJob => this.mTargeJob;

    public string AbilityOverwrite => this.mAbilityOverwrite;

    public void Deserialize(JSON_TobiraLearnAbilityParam json)
    {
      if (json == null)
        return;
      this.mAbilityIname = json.abil_iname;
      this.mLevel = json.learn_lv;
      this.mAddType = (TobiraLearnAbilityParam.AddType) json.add_type;
      this.mTargeJob = json.target_job;
      this.mAbilityOverwrite = json.abil_overwrite;
    }

    public enum AddType
    {
      Unknow,
      JobOverwrite,
      MasterAdd,
      MasterOverwrite,
    }
  }
}
