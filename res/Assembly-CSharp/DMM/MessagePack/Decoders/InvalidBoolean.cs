﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.InvalidBoolean
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class InvalidBoolean : IBooleanDecoder
  {
    internal static IBooleanDecoder Instance = (IBooleanDecoder) new InvalidBoolean();

    private InvalidBoolean()
    {
    }

    public bool Read() => throw new InvalidOperationException("code is invalid.");
  }
}