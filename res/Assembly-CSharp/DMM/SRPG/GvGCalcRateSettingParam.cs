// Decompiled with JetBrains decompiler
// Type: SRPG.GvGCalcRateSettingParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGCalcRateSettingParam : GvGMasterParam<JSON_GvGCalcRateSettingParam>
  {
    public int Enum1 { get; private set; }

    public int Enum2 { get; private set; }

    public int Enum3 { get; private set; }

    public int Enum4 { get; private set; }

    public override bool Deserialize(JSON_GvGCalcRateSettingParam json)
    {
      if (json == null)
        return false;
      this.Enum1 = json.enum1;
      this.Enum2 = json.enum2;
      this.Enum3 = json.enum3;
      this.Enum4 = json.enum4;
      return true;
    }
  }
}
