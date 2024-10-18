// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSubSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class UnitSubSetting
  {
    public eMapUnitCtCalcType startCtCalc;
    public OInt startCtVal;

    public UnitSubSetting()
    {
    }

    public UnitSubSetting(JSON_MapPartySubCT json)
    {
      this.startCtCalc = (eMapUnitCtCalcType) json.ct_calc;
      this.startCtVal = (OInt) json.ct_val;
    }
  }
}
