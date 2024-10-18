// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeParam
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
  public class MultiPlayResumeParam
  {
    public MultiPlayResumeUnitData[] unit;
    public MultiPlayGimmickEventParam[] gimmick;
    public MultiPlayTrickParam[] trick;
    public uint[] rndseed;
    public uint[] dmgrndseed;
    public uint damageseed;
    public uint seed;
    public int unitcastindex;
    public int unitstartcount;
    public int treasurecount;
    public uint versusturn;
    public int resumeID;
    public int[] otherresume;
    public bool[] scr_ev_trg;
    public int ctm;
    public int ctt;
    public MultiPlayResumeParam.WeatherInfo wti = new MultiPlayResumeParam.WeatherInfo();

    [MessagePackObject(true)]
    [Serializable]
    public class WeatherInfo
    {
      public string wid;
      public int mun;
      public int rnk;
      public int rcp;
      public int ccl;
    }
  }
}
