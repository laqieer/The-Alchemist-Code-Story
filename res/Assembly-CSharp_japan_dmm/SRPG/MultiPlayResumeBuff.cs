// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeBuff
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class MultiPlayResumeBuff
  {
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
    public List<int> atl = new List<int>();
    public List<MultiPlayResumeBuff.ResistStatus> rsl = new List<MultiPlayResumeBuff.ResistStatus>();

    [MessagePackObject(true)]
    [Serializable]
    public class ResistStatus
    {
      public int rst;
      public int rsv;
    }
  }
}
