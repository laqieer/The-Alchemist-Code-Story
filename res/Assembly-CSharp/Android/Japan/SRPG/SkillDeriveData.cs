// Decompiled with JetBrains decompiler
// Type: SRPG.SkillDeriveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class SkillDeriveData
  {
    private SkillDeriveParam m_Param;
    private bool m_IsAdd;
    private bool m_IsDisable;

    public SkillDeriveData(SkillDeriveParam param)
    {
      this.m_Param = param;
    }

    public bool IsAdd
    {
      get
      {
        return this.m_IsAdd;
      }
      set
      {
        this.m_IsAdd = value;
      }
    }

    public bool IsDisable
    {
      get
      {
        return this.m_IsDisable;
      }
      set
      {
        this.m_IsDisable = value;
      }
    }

    public SkillDeriveParam Param
    {
      get
      {
        return this.m_Param;
      }
    }
  }
}
