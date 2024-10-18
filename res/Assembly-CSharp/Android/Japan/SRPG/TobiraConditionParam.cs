// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraConditionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class TobiraConditionParam
  {
    private TobiraConditionParam.ConditionType mCondType;
    private string mCondIname;
    private TobiraCondsUnitParam mCondUnit;

    public TobiraConditionParam.ConditionType CondType
    {
      get
      {
        return this.mCondType;
      }
    }

    public string CondIname
    {
      get
      {
        return this.mCondIname;
      }
    }

    public TobiraCondsUnitParam CondUnit
    {
      get
      {
        return this.mCondUnit;
      }
    }

    public void Deserialize(JSON_TobiraConditionParam json)
    {
      if (json == null)
        return;
      this.mCondType = (TobiraConditionParam.ConditionType) json.conds_type;
      this.mCondIname = json.conds_iname;
    }

    public void SetCondUnit(TobiraCondsUnitParam cond_unit)
    {
      this.mCondUnit = cond_unit;
    }

    public enum ConditionType
    {
      None,
      Unit,
      Quest,
    }
  }
}
