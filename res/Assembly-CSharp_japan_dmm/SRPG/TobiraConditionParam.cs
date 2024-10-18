// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraConditionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class TobiraConditionParam
  {
    private TobiraConditionParam.ConditionType mCondType;
    private string mCondIname;
    private TobiraCondsUnitParam mCondUnit;

    public TobiraConditionParam.ConditionType CondType => this.mCondType;

    public string CondIname => this.mCondIname;

    public TobiraCondsUnitParam CondUnit => this.mCondUnit;

    public void Deserialize(JSON_TobiraConditionParam json)
    {
      if (json == null)
        return;
      this.mCondType = (TobiraConditionParam.ConditionType) json.conds_type;
      this.mCondIname = json.conds_iname;
    }

    public void SetCondUnit(TobiraCondsUnitParam cond_unit) => this.mCondUnit = cond_unit;

    public enum ConditionType
    {
      None,
      Unit,
      Quest,
    }
  }
}
