// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_VersusRankParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_VersusRankParam
  {
    public int id;
    public int btl_mode;
    public string name;
    public int limit;
    public string begin_at;
    public string end_at;
    public int win_pt_base;
    public int lose_pt_base;
    public string[] disabledate;
    public string hurl;
  }
}
