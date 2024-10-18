// Decompiled with JetBrains decompiler
// Type: SRPG.Grid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
