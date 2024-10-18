// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTaskAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace Gsc.Network
{
  [Flags]
  public enum WebTaskAttribute : uint
  {
    None = 0,
    Reliable = 1,
    Interrupt = 2,
    Silent = 4,
    Parallel = 8,
  }
}
