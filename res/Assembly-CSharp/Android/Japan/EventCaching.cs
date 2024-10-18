﻿// Decompiled with JetBrains decompiler
// Type: EventCaching
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

public enum EventCaching : byte
{
  DoNotCache = 0,
  [Obsolete] MergeCache = 1,
  [Obsolete] ReplaceCache = 2,
  [Obsolete] RemoveCache = 3,
  AddToRoomCache = 4,
  AddToRoomCacheGlobal = 5,
  RemoveFromRoomCache = 6,
  RemoveFromRoomCacheForActorsLeft = 7,
  SliceIncreaseIndex = 10, // 0x0A
  SliceSetIndex = 11, // 0x0B
  SlicePurgeIndex = 12, // 0x0C
  SlicePurgeUpToIndex = 13, // 0x0D
}
