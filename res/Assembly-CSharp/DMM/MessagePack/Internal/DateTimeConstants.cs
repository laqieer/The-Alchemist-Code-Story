// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.DateTimeConstants
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace MessagePack.Internal
{
  internal static class DateTimeConstants
  {
    internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    internal const long BclSecondsAtUnixEpoch = 62135596800;
    internal const int NanosecondsPerTick = 100;
  }
}
