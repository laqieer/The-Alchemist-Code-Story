// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDisassembly
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneDisassembly
  {
    public List<RuneDisassembly.Materials> success = new List<RuneDisassembly.Materials>();
    public List<RuneDisassembly.Materials> great = new List<RuneDisassembly.Materials>();
    public List<RuneDisassembly.Materials> super = new List<RuneDisassembly.Materials>();

    public bool Deserialize(JSON_RuneMaterial json, MasterParam master = null)
    {
      if (master == null)
        master = MonoSingleton<GameManager>.Instance.MasterParam;
      if (master == null)
      {
        DebugUtility.LogError("GameManager.Instance.MasterParam を読み込んだ後に RuneDisassembly をデシリアライズしてください");
        return false;
      }
      for (int index = 0; index < json.disassembly.Length; ++index)
      {
        if (json.disassembly[index] != null)
        {
          if (json.disassembly[index].success_item != null)
          {
            RuneDisassembly.Materials materials = new RuneDisassembly.Materials();
            materials.num = (short) json.disassembly[index].success_num;
            materials.item = master.GetItemParam(json.disassembly[index].success_item);
            if (materials.item == null)
              DebugUtility.LogError(json.disassembly[index].success_item + "がItemシートに存在しません");
            this.success.Add(materials);
          }
          if (json.disassembly[index].great_item != null)
          {
            RuneDisassembly.Materials materials = new RuneDisassembly.Materials();
            materials.num = (short) json.disassembly[index].great_num;
            materials.item = master.GetItemParam(json.disassembly[index].great_item);
            if (materials.item == null)
              DebugUtility.LogError(json.disassembly[index].great_item + "がItemシートに存在しません");
            this.great.Add(materials);
          }
          if (json.disassembly[index].super_item != null)
          {
            RuneDisassembly.Materials materials = new RuneDisassembly.Materials();
            materials.num = (short) json.disassembly[index].super_num;
            materials.item = master.GetItemParam(json.disassembly[index].super_item);
            if (materials.item == null)
              DebugUtility.LogError(json.disassembly[index].super_item + "がItemシートに存在しません");
            this.super.Add(materials);
          }
        }
      }
      return true;
    }

    public static void ResultCalc(
      List<BindRuneData> runes,
      ReqRuneDisassembly.Response.Result result,
      out Dictionary<string, RuneDisassembly.Materials> dict)
    {
      dict = new Dictionary<string, RuneDisassembly.Materials>();
      foreach (BindRuneData rune1 in runes)
      {
        RuneData rune2 = rune1.Rune;
        if (rune2 == null)
          DebugUtility.LogError("ResultCalc の success で計算ミス発生");
        float evoRate = RuneDisassembly.GetEvoRate(rune2);
        switch (result)
        {
          case ReqRuneDisassembly.Response.Result.success:
            using (List<RuneDisassembly.Materials>.Enumerator enumerator = rune2.RuneMaterial.disassembly.success.GetEnumerator())
            {
              while (enumerator.MoveNext())
                RuneDisassembly.ResultAdd(enumerator.Current, dict, evoRate);
              continue;
            }
          case ReqRuneDisassembly.Response.Result.great:
            using (List<RuneDisassembly.Materials>.Enumerator enumerator = rune2.RuneMaterial.disassembly.great.GetEnumerator())
            {
              while (enumerator.MoveNext())
                RuneDisassembly.ResultAdd(enumerator.Current, dict, evoRate);
              continue;
            }
          case ReqRuneDisassembly.Response.Result.super:
            using (List<RuneDisassembly.Materials>.Enumerator enumerator = rune2.RuneMaterial.disassembly.super.GetEnumerator())
            {
              while (enumerator.MoveNext())
                RuneDisassembly.ResultAdd(enumerator.Current, dict, evoRate);
              continue;
            }
          default:
            continue;
        }
      }
    }

    private static void ResultAdd(
      RuneDisassembly.Materials material,
      Dictionary<string, RuneDisassembly.Materials> dict,
      float rate)
    {
      if (dict.ContainsKey(material.item.iname))
      {
        RuneDisassembly.Materials materials;
        dict.TryGetValue(material.item.iname, out materials);
        materials.num += (short) ((double) material.num * (double) rate);
      }
      else
      {
        RuneDisassembly.Materials materials = new RuneDisassembly.Materials();
        dict.Add(material.item.iname, materials);
        materials.item = material.item;
        materials.num = (short) ((double) material.num * (double) rate);
      }
    }

    private static float GetEvoRate(RuneData _data)
    {
      float evoRate = 1f;
      if (_data.EvoNum > 0)
        evoRate = (float) _data.RuneMaterial.magnification[_data.EvoNum - 1] / 100f;
      return evoRate;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Materials
    {
      public ItemParam item;
      public short num;
    }
  }
}
