// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_BreakObjParam
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
  public class JSON_BreakObjParam
  {
    public string iname;
    public string name;
    public string expr;
    public string unit_id;
    public int clash_type;
    public int ai_type;
    public int side_type;
    public int ray_type;
    public int is_ui;
    public string rest_hps;
    public int clock;
    public int appear_dir;
  }
}
