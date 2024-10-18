// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityDeriveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class AbilityDeriveParam : BaseDeriveParam<AbilityParam>
  {
    public string BaseAbilityIname
    {
      get
      {
        return this.m_BaseParam.iname;
      }
    }

    public string DeriveAbilityIname
    {
      get
      {
        return this.m_DeriveParam.iname;
      }
    }

    public string BaseAbilityName
    {
      get
      {
        return this.m_BaseParam.name;
      }
    }

    public string DeriveAbilityName
    {
      get
      {
        return this.m_DeriveParam.name;
      }
    }
  }
}
