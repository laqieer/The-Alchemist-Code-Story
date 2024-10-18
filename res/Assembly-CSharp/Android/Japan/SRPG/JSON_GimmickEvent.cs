// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GimmickEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_GimmickEvent
  {
    public string skill = string.Empty;
    public string su_iname = string.Empty;
    public string su_tag = string.Empty;
    public string st_iname = string.Empty;
    public string st_tag = string.Empty;
    public string cu_iname = string.Empty;
    public string cu_tag = string.Empty;
    public string ct_iname = string.Empty;
    public string ct_tag = string.Empty;
    public int[] x = new int[1];
    public int[] y = new int[1];
    public int ev_type;
    public int type;
    public int count;
  }
}
