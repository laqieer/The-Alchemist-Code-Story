// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_DependStateSpcEffParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_DependStateSpcEffParam
  {
    public string iname;
    public int is_and;
    public int[] buff_ids;
    public int[] cond_ids;
    public int inv_tkrate;
    public int is_inv_t_buff;
    public int is_inv_t_cond;
    public int is_inv_s_buff;
    public int is_inv_s_cond;
  }
}
