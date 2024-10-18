// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GenesisChapterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
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
