// Decompiled with JetBrains decompiler
// Type: SRPG.WeaponParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class WeaponParam
  {
    public string iname;
    public OInt atk;
    public OInt formula;

    public WeaponFormulaTypes FormulaType
    {
      get
      {
        return (WeaponFormulaTypes) (int) this.formula;
      }
    }

    public bool Deserialize(JSON_WeaponParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.atk = (OInt) json.atk;
      this.formula = (OInt) json.formula;
      return true;
    }
  }
}
