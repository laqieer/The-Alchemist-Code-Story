// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTopParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GachaTopParam
  {
    public string[] iname = new string[4];
    public string[] category = new string[4];
    public int[] coin = new int[4];
    public int[] gold = new int[4];
    public int[] coin_p = new int[4];
    public List<UnitParam> units;
    public int[] num = new int[4];
    public string[] ticket = new string[4];
    public int[] ticket_num = new int[4];
    public bool[] step = new bool[4];
    public int[] step_num = new int[4];
    public int[] step_index = new int[4];
    public bool[] limit = new bool[4];
    public int[] limit_num = new int[4];
    public int[] limit_stock = new int[4];
    public string type;
    public string asset_title;
    public string asset_bg;
    public string group;
    public string[] btext = new string[4];
    public string[] confirm = new string[4];
    public List<int> sort = new List<int>();
  }
}
