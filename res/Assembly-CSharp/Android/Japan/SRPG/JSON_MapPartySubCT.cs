// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapPartySubCT
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
