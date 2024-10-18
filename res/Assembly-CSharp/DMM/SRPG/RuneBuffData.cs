// Decompiled with JetBrains decompiler
// Type: SRPG.RuneBuffData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RuneBuffData
  {
    protected float PowerPercentage(ParamTypes param_types, short val, RuneLotteryState lottery)
    {
      if (lottery == null || param_types == ParamTypes.None)
        return 0.0f;
      short num1 = (short) ((int) val - (int) lottery.lot_min + 1);
      short num2 = (short) ((int) lottery.lot_max - (int) lottery.lot_min + 1);
      return num2 != (short) 0 ? (float) num1 / (float) num2 : 1f;
    }

    protected enum StateType
    {
      BaseState,
      EvoState,
    }
  }
}
