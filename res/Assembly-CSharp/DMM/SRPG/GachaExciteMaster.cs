// Decompiled with JetBrains decompiler
// Type: SRPG.GachaExciteMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class GachaExciteMaster
  {
    public static int[] Select(Json_GachaExcite[] json, int rare)
    {
      int maxValue = 0;
      foreach (Json_GachaExcite jsonGachaExcite in json)
      {
        if (rare == jsonGachaExcite.fields.rarity)
          maxValue += jsonGachaExcite.fields.weight;
      }
      int num1 = new Random().Next(maxValue);
      int num2 = 0;
      foreach (Json_GachaExcite jsonGachaExcite in json)
      {
        if (rare == jsonGachaExcite.fields.rarity)
        {
          num2 += jsonGachaExcite.fields.weight;
          if (num1 < num2)
            return new int[5]
            {
              GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color1),
              GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color2),
              GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color3),
              GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color4),
              GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color5)
            };
        }
      }
      return new int[5]{ 1, 1, 1, 1, 1 };
    }

    public static int[] SelectStone(Json_GachaExcite[] json, int rare)
    {
      int maxValue = 0;
      foreach (Json_GachaExcite jsonGachaExcite in json)
      {
        if (rare == jsonGachaExcite.fields.rarity)
          maxValue += jsonGachaExcite.fields.weight;
      }
      int num1 = new Random().Next(maxValue);
      int num2 = 0;
      foreach (Json_GachaExcite jsonGachaExcite in json)
      {
        if (rare == jsonGachaExcite.fields.rarity)
        {
          num2 += jsonGachaExcite.fields.weight;
          if (num1 < num2)
            return new int[3]
            {
              GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color1),
              GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color2),
              GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color3)
            };
        }
      }
      return new int[3]{ 1, 1, 1 };
    }

    private static int ColorString2Int(string cstr)
    {
      switch (cstr)
      {
        case "blue":
          return 1;
        case "yellow":
          return 2;
        case "red":
          return 3;
        default:
          DebugUtility.LogError("Invalid color string.");
          return 0;
      }
    }
  }
}
