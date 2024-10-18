// Decompiled with JetBrains decompiler
// Type: SRPG.LogFall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class LogFall : BattleLog
  {
    public List<LogFall.Param> mLists = new List<LogFall.Param>();
    public bool mIsPlayDamageMotion;

    public void Add(Unit self, Grid landing = null)
    {
      this.mLists.Add(new LogFall.Param()
      {
        mSelf = self,
        mLanding = landing
      });
    }

    public struct Param
    {
      public Unit mSelf;
      public Grid mLanding;
    }
  }
}
