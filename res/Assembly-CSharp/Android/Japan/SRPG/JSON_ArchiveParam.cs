// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ArchiveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_ArchiveParam
  {
    public string iname;
    public string area_iname;
    public string area_iname_multi;
    public int type;
    public string begin_at;
    public string end_at;
    public string keyitem1;
    public int keynum1;
    public int keytime;
    public string unit1;
    public string unit2;
    public JSON_ArchiveItemsParam[] items;
  }
}
