// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraCondsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class TobiraCondsParam
  {
    private List<TobiraConditionParam> mConditions = new List<TobiraConditionParam>();
    private string mUnitIname;
    private TobiraParam.Category mCategory;

    public string UnitIname
    {
      get
      {
        return this.mUnitIname;
      }
    }

    public TobiraParam.Category TobiraCategory
    {
      get
      {
        return this.mCategory;
      }
    }

    public TobiraConditionParam[] Conditions
    {
      get
      {
        return this.mConditions.ToArray();
      }
    }

    public void Deserialize(JSON_TobiraCondsParam json)
    {
      if (json == null)
        return;
      this.mUnitIname = json.unit_iname;
      this.mCategory = (TobiraParam.Category) json.category;
      this.mConditions.Clear();
      if (json.conds == null)
        return;
      for (int index = 0; index < json.conds.Length; ++index)
      {
        TobiraConditionParam tobiraConditionParam = new TobiraConditionParam();
        tobiraConditionParam.Deserialize(json.conds[index]);
        this.mConditions.Add(tobiraConditionParam);
      }
    }
  }
}
