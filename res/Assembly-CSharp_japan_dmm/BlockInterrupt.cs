// Decompiled with JetBrains decompiler
// Type: BlockInterrupt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
public class BlockInterrupt
{
  private static List<BlockInterrupt> mInstances = new List<BlockInterrupt>();
  private bool mActive;
  private BlockInterrupt.EType mType;

  public static bool IsBlocked(BlockInterrupt.EType type)
  {
    return BlockInterrupt.mInstances.Find((Predicate<BlockInterrupt>) (i => i.mType == type || i.mType == BlockInterrupt.EType.ALL)) != null;
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

  ~BlockInterrupt() => this.Destroy();

  public enum EType
  {
    NOP,
    ALL,
    PHOTON_DISCONNECTED,
    URL_SCHEME_LAUNCH,
    NUM,
  }
}
