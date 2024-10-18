// Decompiled with JetBrains decompiler
// Type: SRPG.Grid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class Grid
  {
    public const int DEFAULT_COST = 1;
    public int x;
    public int y;
    public int height;
    public int cost = 1;
    public byte step = 127;
    public string tile;
    public GeoParam geo;
    public int enter;

    public bool IsDisableStopped() => this.geo != null && (bool) this.geo.DisableStopped;

    public bool IsEnter(eMovType mov_type)
    {
      return (this.enter & 1 << (int) (mov_type & (eMovType) 31)) != 0;
    }
  }
}
