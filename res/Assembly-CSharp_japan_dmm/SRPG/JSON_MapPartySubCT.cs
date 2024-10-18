// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapPartySubCT
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
