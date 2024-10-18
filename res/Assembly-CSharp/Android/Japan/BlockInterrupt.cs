﻿// Decompiled with JetBrains decompiler
// Type: BlockInterrupt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

public class BlockInterrupt
{
  private static List<BlockInterrupt> mInstances = new List<BlockInterrupt>();
  private bool mActive;
  private BlockInterrupt.EType mType;

  public static bool IsBlocked(BlockInterrupt.EType type)
  {
    return BlockInterrupt.mInstances.Find((Predicate<BlockInterrupt>) (i =>
    {
      if (i.mType != type)
        return i.mType == BlockInterrupt.EType.ALL;
      return true;
    })) != null;
  }

  public static BlockInterrupt Create(BlockInterrupt.EType type)
  {
    BlockInterrupt blockInterrupt = new BlockInterrupt();
    if (blockInterrupt == null)
      return (BlockInterrupt) null;
    blockInterrupt.mActive = true;
    blockInterrupt.mType = type;
    BlockInterrupt.mInstances.Add(blockInterrupt);
    return blockInterrupt;
  }

  public void Destroy()
  {
    if (!this.mActive)
      return;
    this.mActive = false;
    BlockInterrupt.mInstances.Remove(this);
  }

  ~BlockInterrupt()
  {
    this.Destroy();
  }

  public enum EType
  {
    NOP,
    ALL,
    PHOTON_DISCONNECTED,
    URL_SCHEME_LAUNCH,
    NUM,
  }
}
