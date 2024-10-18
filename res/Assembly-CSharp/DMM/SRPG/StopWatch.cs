// Decompiled with JetBrains decompiler
// Type: SRPG.StopWatch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Diagnostics;

#nullable disable
namespace SRPG
{
  public class StopWatch : Stopwatch
  {
    private int milliSec = (int) ((double) TimeManager.FPS * 1000.0);

    public void ReStart()
    {
      this.Reset();
      this.Start();
    }

    public void SetMilliSec(int time) => this.milliSec = time;

    public bool IsElapsec() => this.ElapsedMilliseconds > (long) this.milliSec;
  }
}
