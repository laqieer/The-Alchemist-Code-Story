// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_DependStateSpcEffParam
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
  public class JSON_DependStateSpcEffParam
  {
    public string iname;
    public int is_and;
    public int[] buff_ids;
    public int[] buff_types;
    public int[] cond_ids;
    public int inv_tkrate;
    public int is_inv_t_buff;
    public int is_inv_t_cond;
    public int is_inv_s_buff;
    public int is_inv_s_cond;
  }
}
