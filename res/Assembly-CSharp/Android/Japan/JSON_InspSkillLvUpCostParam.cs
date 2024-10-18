// Decompiled with JetBrains decompiler
// Type: JSON_InspSkillLvUpCostParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class JSON_InspSkillLvUpCostParam
{
  public int id;
  public JSON_InspSkillLvUpCostParam.JSON_CostData[] costs;

  [Serializable]
  public class JSON_CostData
  {
    public int gold;
  }
}
