// Decompiled with JetBrains decompiler
// Type: JSON_InspSkillLvUpCostParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
[MessagePackObject(true)]
[Serializable]
public class JSON_InspSkillLvUpCostParam
{
  public int id;
  public JSON_InspSkillLvUpCostParam.JSON_CostData[] costs;

  [MessagePackObject(true)]
  [Serializable]
  public class JSON_CostData
  {
    public int gold;
  }
}
