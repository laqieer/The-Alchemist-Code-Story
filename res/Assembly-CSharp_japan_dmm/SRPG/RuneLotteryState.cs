// Decompiled with JetBrains decompiler
// Type: SRPG.RuneLotteryState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneLotteryState
  {
    public string iname;
    public ParamTypes type;
    public SkillParamCalcTypes calc;
    public short lot_min;
    public short lot_max;

    public bool Deserialize(JSON_RuneLotteryState json)
    {
      this.iname = json.iname;
      this.type = (ParamTypes) json.type;
      this.calc = (SkillParamCalcTypes) json.calc;
      short num1 = short.MaxValue;
      short num2 = 0;
      foreach (JSON_RuneLottery jsonRuneLottery in json.lottery)
      {
        num1 = (short) Mathf.Min((int) num1, jsonRuneLottery.min);
        num2 = (short) Mathf.Max((int) num2, jsonRuneLottery.max);
      }
      this.lot_min = num1;
      this.lot_max = num2;
      return true;
    }
  }
}
