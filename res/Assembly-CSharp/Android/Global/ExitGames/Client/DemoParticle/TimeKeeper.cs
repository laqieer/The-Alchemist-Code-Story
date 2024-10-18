// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.DemoParticle.TimeKeeper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

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
        if (!this.shouldExecute)
          return Environment.TickCount - this.lastExecutionTime > this.Interval;
        return true;
      }
      set
      {
        this.shouldExecute = value;
      }
    }

    public void Reset()
    {
      this.shouldExecute = false;
      this.lastExecutionTime = Environment.TickCount;
    }
  }
}
