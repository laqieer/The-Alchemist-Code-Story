// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayResumeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
