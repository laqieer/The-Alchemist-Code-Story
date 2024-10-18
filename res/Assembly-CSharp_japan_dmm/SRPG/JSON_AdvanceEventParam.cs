// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_AdvanceEventParam
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
  public class JSON_AdvanceEventParam
  {
    public string iname;
    public int trans_type;
    public int priority;
    public string area_id;
    public string name;
    public string box_iname;
    public int event_ui_index;
    public string event_banner;
    public string event_detail_url;
    public string boss_hint_url;
    public JSON_AdvanceEventModeInfoParam[] mode_info;
  }
}
