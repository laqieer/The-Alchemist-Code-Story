﻿// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.ValueType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace Gsc.DOM
{
  [Flags]
  public enum ValueType : uint
  {
    Null = 0,
    Object = 1,
    Array = 2,
    Bool = 4,
    String = 8,
    Integer = 3840, // 0x00000F00
    Int32 = 256, // 0x00000100
    UInt32 = 512, // 0x00000200
    Int64 = 1024, // 0x00000400
    UInt64 = 2048, // 0x00000800
    Number = 393216, // 0x00060000
    Float = 131072, // 0x00020000
    Double = 262144, // 0x00040000
  }
}
