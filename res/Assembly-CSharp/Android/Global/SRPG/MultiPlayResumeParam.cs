// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class MultiPlayResumeParam
  {
    public MultiPlayResumeParam.WeatherInfo wti = new MultiPlayResumeParam.WeatherInfo();
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
