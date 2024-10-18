// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapPartySubCT
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_MapPartySubCT
  {
    public int ct_calc;
    public int ct_val;

    public void CopyTo(JSON_MapPartySubCT dst)
    {
      dst.ct_calc = this.ct_calc;
      dst.ct_val = this.ct_val;
    }
  }
}
