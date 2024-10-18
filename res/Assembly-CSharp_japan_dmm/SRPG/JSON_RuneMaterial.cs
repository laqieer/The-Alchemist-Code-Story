// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RuneMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_RuneMaterial
  {
    public int rarity;
    public string[] enh_cost;
    public string[] evo_cost;
    public int disassembly_zeny;
    public JSON_RuneDisassembly[] disassembly;
    public int[] enhance_probability;
    public int[] magnification;
    public string[] reset_base_param_costs;
    public string[] reset_evo_status_costs;
    public string[] param_enh_evo_costs;
  }
}
