// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GimmickEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class JSON_GimmickEvent
  {
    public int ev_type;
    public string skill = string.Empty;
    public string su_iname = string.Empty;
    public string su_tag = string.Empty;
    public string st_iname = string.Empty;
    public string st_tag = string.Empty;
    public int type;
    public string cu_iname = string.Empty;
    public string cu_tag = string.Empty;
    public string ct_iname = string.Empty;
    public string ct_tag = string.Empty;
    public int count;
    public int[] x = new int[1];
    public int[] y = new int[1];
  }
}
