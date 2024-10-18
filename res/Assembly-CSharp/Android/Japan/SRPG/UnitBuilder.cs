// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBuilder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  internal class UnitBuilder
  {
    private string m_UnitIname;
    private int m_Exp;
    private int m_Awake;
    private string m_JobIname;
    private int m_Rarity;
    private int m_JobRank;
    private int m_UnlockTobiraNum;

    public UnitBuilder(string unitIname)
    {
      this.m_UnitIname = unitIname;
    }

    public UnitBuilder SetLevelByExp(int exp)
    {
      this.m_Exp = MonoSingleton<GameManager>.Instance.MasterParam.CalcUnitLevel(exp, 250);
      this.m_Exp = exp;
      return this;
    }

    public UnitBuilder SetExpByLevel(int level)
    {
      this.m_Exp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(level);
      return this;
    }

    public UnitBuilder SetRarity(int value)
    {
      this.m_Rarity = value;
      return this;
    }

    public UnitBuilder SetAwake(int value)
    {
      this.m_Awake = value;
      return this;
    }

    public UnitBuilder SetJob(string jobIname, int jobRank)
    {
      this.m_JobIname = jobIname;
      this.m_JobRank = jobRank;
      return this;
    }

    public UnitBuilder SetUnlockTobiraNum(int value)
    {
      this.m_UnlockTobiraNum = value;
      return this;
    }

    public UnitData Build()
    {
      UnitData unitData = new UnitData();
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.m_UnitIname);
      unitData.Setup(this.m_UnitIname, this.m_Exp, this.m_Rarity, this.m_Awake, this.m_JobIname, this.m_JobRank, unitParam.element, this.m_UnlockTobiraNum);
      return unitData;
    }
  }
}
