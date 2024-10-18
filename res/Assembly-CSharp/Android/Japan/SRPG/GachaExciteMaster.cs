// Decompiled with JetBrains decompiler
// Type: SRPG.GachaExciteMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
            return new int[5]{ GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color1), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color2), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color3), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color4), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color5) };
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
            return new int[3]{ GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color1), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color2), GachaExciteMaster.ColorString2Int(jsonGachaExcite.fields.color3) };
        }
      }
      return new int[3]{ 1, 1, 1 };
    }

    private static int ColorString2Int(string cstr)
    {
      if (cstr != null)
      {
        if (cstr == "blue")
          return 1;
        if (cstr == "yellow")
          return 2;
        if (cstr == "red")
          return 3;
      }
      DebugUtility.LogError("Invalid color string.");
      return 0;
    }
  }
}
