// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.DemoParticle.TimeKeeper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace ExitGames.Client.DemoParticle
{
  public class TimeKeeper
  {
    private int lastExecutionTime = Environment.TickCount;
    private bool shouldExecute;

    public TimeKeeper(int interval)
    {
      this.IsEnabled = true;
      this.Interval = interval;
    }

    public int Interval { get; set; }

    public bool IsEnabled { get; set; }

    public bool ShouldExecute
    {
      get
      {
        if (!this.IsEnabled)
          return false;
        return this.shouldExecute || Environment.TickCount - this.lastExecutionTime > this.Interval;
      }
      set => this.shouldExecute = value;
    }

    public void Reset()
    {
      this.shouldExecute = false;
      this.lastExecutionTime = Environment.TickCount;
    }
  }
}
