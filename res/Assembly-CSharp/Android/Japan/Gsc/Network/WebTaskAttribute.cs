// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTaskAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
