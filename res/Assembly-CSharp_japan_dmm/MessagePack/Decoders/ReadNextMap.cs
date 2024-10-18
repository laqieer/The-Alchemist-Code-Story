﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Decoders.ReadNextMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace MessagePack.Decoders
{
  internal sealed class ReadNextMap : IReadNextDecoder
  {
    internal static readonly IReadNextDecoder Instance = (IReadNextDecoder) new ReadNextMap();

    private ReadNextMap()
    {
    }

    public int Read(byte[] bytes, int offset)
    {
      int num1 = offset;
      int readSize;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      for (int index = 0; index < num2; ++index)
      {
        offset += MessagePackBinary.ReadNext(bytes, offset);
        offset += MessagePackBinary.ReadNext(bytes, offset);
      }
      return offset - num1;
    }
  }
}
