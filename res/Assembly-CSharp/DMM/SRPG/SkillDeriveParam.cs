// Decompiled with JetBrains decompiler
// Type: SRPG.SkillDeriveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class SkillDeriveParam : BaseDeriveParam<SkillParam>
  {
    public string BaseSkillIname => this.m_BaseParam.iname;

    public string DeriveSkillIname => this.m_DeriveParam.iname;

    public string BaseSkillName => this.m_BaseParam.name;

    public string DeriveSkillName => this.m_DeriveParam.name;
  }
}
