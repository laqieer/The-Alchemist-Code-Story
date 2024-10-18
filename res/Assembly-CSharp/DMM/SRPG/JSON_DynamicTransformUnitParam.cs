// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_DynamicTransformUnitParam
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
  public class JSON_DynamicTransformUnitParam
  {
    public string iname;
    public string name;
    public string tr_unit_id;
    public int turn;
    public string upper_to_abid;
    public string lower_to_abid;
    public string react_to_abid;
    public int is_no_wa;
    public int is_no_va;
    public int is_no_item;
    public string ct_eff;
    public int ct_dis_ms;
    public int ct_app_ms;
    public int is_tr_hpf;
    public int is_cc_hpf;
    public int is_inh_skin;
  }
}
