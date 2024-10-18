// Decompiled with JetBrains decompiler
// Type: SRPG.DrawBaseStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class DrawBaseStatus : BaseStatus
  {
    private Dictionary<ParamTypes, int> m_AdditionalValues;

    [IgnoreMember]
    public override int this[ParamTypes type]
    {
      get
      {
        return type == ParamTypes.Assist_ConditionAll || type == ParamTypes.Resist_ConditionAll ? this.GetAditionalValue(type) : base[type];
      }
    }

    public void SetAditionalValue(ParamTypes paramType, int value)
    {
      if (this.m_AdditionalValues == null)
        this.m_AdditionalValues = new Dictionary<ParamTypes, int>();
      if (this.m_AdditionalValues.ContainsKey(paramType))
        this.m_AdditionalValues[paramType] = value;
      else
        this.m_AdditionalValues.Add(paramType, value);
    }

    public int GetAditionalValue(ParamTypes paramType)
    {
      if (this.m_AdditionalValues == null)
        return 0;
      int aditionalValue = 0;
      this.m_AdditionalValues.TryGetValue(paramType, out aditionalValue);
      return aditionalValue;
    }

    public void AddDrawStatus(DrawBaseStatus src)
    {
      this.Add((BaseStatus) src);
      if (src.m_AdditionalValues == null)
        return;
      foreach (KeyValuePair<ParamTypes, int> additionalValue in src.m_AdditionalValues)
        this.SetAditionalValue(additionalValue.Key, this[additionalValue.Key] + additionalValue.Value);
    }
  }
}
