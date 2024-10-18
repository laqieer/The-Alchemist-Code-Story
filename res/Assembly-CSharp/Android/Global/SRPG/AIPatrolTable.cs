﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AIPatrolTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class AIPatrolTable
  {
    public AIPatrolPoint[] routes;
    public int looped;
    public int keeped;

    public void Clear()
    {
      this.routes = (AIPatrolPoint[]) null;
      this.looped = 0;
      this.keeped = 0;
    }

    public void CopyTo(AIPatrolTable dst)
    {
      dst.routes = (AIPatrolPoint[]) null;
      dst.looped = 0;
      dst.keeped = 0;
      if (this.routes == null || this.routes.Length == 0)
        return;
      dst.routes = new AIPatrolPoint[this.routes.Length];
      for (int index = 0; index < this.routes.Length; ++index)
      {
        dst.routes[index] = new AIPatrolPoint();
        this.routes[index].CopyTo(dst.routes[index]);
      }
      dst.looped = this.looped;
      dst.keeped = this.keeped;
    }
  }
}
