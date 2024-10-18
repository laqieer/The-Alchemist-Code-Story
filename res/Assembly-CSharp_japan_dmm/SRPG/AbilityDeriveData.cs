// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityDeriveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class AbilityDeriveData
  {
    private AbilityDeriveParam m_Param;
    private bool m_IsAdd;
    private bool m_IsDisable;

    public AbilityDeriveData(AbilityDeriveParam param) => this.m_Param = param;

    public bool IsAdd
    {
      get => this.m_IsAdd;
      set => this.m_IsAdd = value;
    }

    public bool IsDisable
    {
      get => this.m_IsDisable;
      set => this.m_IsDisable = value;
    }

    public AbilityDeriveParam Param => this.m_Param;
  }
}
