// Decompiled with JetBrains decompiler
// Type: SRPG.WeaponParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class WeaponParam
  {
    public string iname;
    public OInt atk;
    public OInt formula;

    public WeaponFormulaTypes FormulaType => (WeaponFormulaTypes) (int) this.formula;

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
