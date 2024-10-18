// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayTrickParam
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
  public class MultiPlayTrickParam
  {
    public string tid;
    public bool val;
    public int cun;
    public int rnk;
    public int rcp;
    public int grx;
    public int gry;
    public int rac;
    public int ccl;
    public string tag;
  }
}
