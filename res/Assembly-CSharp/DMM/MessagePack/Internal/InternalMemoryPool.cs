// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.InternalMemoryPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Internal
{
  internal static class InternalMemoryPool
  {
    [ThreadStatic]
    private static byte[] buffer;

    public static byte[] GetBuffer()
    {
      if (InternalMemoryPool.buffer == null)
        InternalMemoryPool.buffer = new byte[65536];
      return InternalMemoryPool.buffer;
    }
  }
}
