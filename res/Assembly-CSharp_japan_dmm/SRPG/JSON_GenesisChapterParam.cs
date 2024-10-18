// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GenesisChapterParam
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
  public class JSON_GenesisChapterParam
  {
    public string iname;
    public int priority;
    public string area_id;
    public string name;
    public string box_iname;
    public int chapter_ui_index;
    public string chapter_banner;
    public string chapter_detail_url;
    public string boss_hint_url;
    public JSON_GenesisChapterModeInfoParam[] mode_info;
  }
}
