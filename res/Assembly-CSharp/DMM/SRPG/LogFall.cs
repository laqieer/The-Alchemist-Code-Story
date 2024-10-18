// Decompiled with JetBrains decompiler
// Type: SRPG.LogFall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
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
