// Decompiled with JetBrains decompiler
// Type: SRPG.BaseDeriveParam`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class BaseDeriveParam<T>
  {
    public SkillAbilityDeriveParam m_SkillAbilityDeriveParam;
    public T m_BaseParam;
    public T m_DeriveParam;

    public int MasterIndex => this.m_SkillAbilityDeriveParam.m_OriginIndex;
  }
}
