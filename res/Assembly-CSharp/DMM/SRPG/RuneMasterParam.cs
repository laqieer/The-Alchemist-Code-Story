// Decompiled with JetBrains decompiler
// Type: SRPG.RuneMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  internal class RuneMasterParam
  {
    public static bool Deserialize(
      JSON_RuneParam[] json,
      ref List<RuneParam> mRuneParam,
      ref Dictionary<string, RuneParam> mRuneParamDict)
    {
      if (json == null)
        return false;
      mRuneParam = new List<RuneParam>();
      mRuneParamDict = new Dictionary<string, RuneParam>();
      for (int index = 0; index < json.Length; ++index)
      {
        RuneParam runeParam = new RuneParam();
        runeParam.Deserialize(json[index]);
        mRuneParam.Add(runeParam);
        if (!mRuneParamDict.ContainsKey(runeParam.iname))
          mRuneParamDict.Add(runeParam.iname, runeParam);
      }
      return true;
    }

    public static bool Deserialize(
      JSON_RuneLotteryBaseState[] json,
      ref List<RuneLotteryBaseState> mRuneLotteryBaseState)
    {
      if (json == null)
        return false;
      mRuneLotteryBaseState = new List<RuneLotteryBaseState>();
      for (int index = 0; index < json.Length; ++index)
      {
        RuneLotteryBaseState lotteryBaseState = new RuneLotteryBaseState();
        lotteryBaseState.Deserialize(json[index]);
        mRuneLotteryBaseState.Add(lotteryBaseState);
      }
      return true;
    }

    public static bool Deserialize(
      JSON_RuneLotteryEvoState[] json,
      ref List<RuneLotteryEvoState> mRuneLotteryEvoState)
    {
      if (json == null)
        return false;
      mRuneLotteryEvoState = new List<RuneLotteryEvoState>();
      for (int index = 0; index < json.Length; ++index)
      {
        RuneLotteryEvoState runeLotteryEvoState = new RuneLotteryEvoState();
        runeLotteryEvoState.Deserialize(json[index]);
        mRuneLotteryEvoState.Add(runeLotteryEvoState);
      }
      return true;
    }

    public static bool Deserialize(
      JSON_RuneMaterial[] json,
      ref List<RuneMaterial> mRuneMaterial,
      MasterParam master)
    {
      if (json == null)
        return false;
      mRuneMaterial = new List<RuneMaterial>();
      for (int index = 0; index < json.Length; ++index)
      {
        RuneMaterial runeMaterial = new RuneMaterial();
        runeMaterial.Deserialize(json[index], master);
        mRuneMaterial.Add(runeMaterial);
      }
      return true;
    }

    public static bool Deserialize(JSON_RuneCost[] json, ref List<RuneCost> mRuneCost)
    {
      if (json == null)
        return false;
      mRuneCost = new List<RuneCost>();
      for (int index = 0; index < json.Length; ++index)
      {
        RuneCost runeCost = new RuneCost();
        runeCost.Deserialize(json[index]);
        mRuneCost.Add(runeCost);
      }
      return true;
    }

    public static bool Deserialize(JSON_RuneSetEff[] json, ref List<RuneSetEff> mRuneSetEff)
    {
      if (json == null)
        return false;
      mRuneSetEff = new List<RuneSetEff>();
      for (int index = 0; index < json.Length; ++index)
      {
        RuneSetEff runeSetEff = new RuneSetEff();
        runeSetEff.Deserialize(json[index]);
        mRuneSetEff.Add(runeSetEff);
      }
      return true;
    }
  }
}
