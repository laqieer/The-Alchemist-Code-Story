// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeBuff
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  [Serializable]
  public class MultiPlayResumeBuff
  {
    public List<int> atl = new List<int>();
    public string iname;
    public int turn;
    public int unitindex;
    public int checkunit;
    public int timing;
    public bool passive;
    public int condition;
    public int type;
    public int vtp;
    public int calc;
    public int curse;
    public int skilltarget;
    public string bc_id;
    public uint lid;
    public int ubc;
  }
}
