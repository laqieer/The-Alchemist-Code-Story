﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.IDateTimeDecoder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal interface IDateTimeDecoder
  {
    DateTime Read(byte[] bytes, int offset, out int readSize);
  }
}
