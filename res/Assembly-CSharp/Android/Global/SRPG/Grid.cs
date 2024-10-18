// Decompiled with JetBrains decompiler
// Type: SRPG.Grid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class Grid
  {
    public int cost = 1;
    public byte step = 127;
    public int x;
    public int y;
    public int height;
    public string tile;
    public GeoParam geo;
  }
}
