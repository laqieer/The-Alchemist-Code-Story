﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GimmickEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
