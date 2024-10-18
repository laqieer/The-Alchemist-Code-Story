// Decompiled with JetBrains decompiler
// Type: SRPG.AIPatrolPoint
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
  public class AIPatrolPoint
  {
    public int x;
    public int y;
    public int length;

    public void CopyTo(AIPatrolPoint dst)
    {
      dst.x = this.x;
      dst.y = this.y;
      dst.length = this.length;
    }
  }
}
