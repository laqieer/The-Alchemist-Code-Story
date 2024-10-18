// Decompiled with JetBrains decompiler
// Type: IntVector2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
[Serializable]
public struct IntVector2
{
  public int x;
  public int y;

  public IntVector2(int a, int b)
  {
    this.x = a;
    this.y = b;
  }

  public override string ToString()
  {
    return string.Format("[IntVector2] {0}, {1}", (object) this.x, (object) this.y);
  }

  public static bool operator ==(IntVector2 a, IntVector2 b) => a.x == b.x && a.y == b.y;

  public static bool operator !=(IntVector2 a, IntVector2 b) => a.x != b.x || a.y != b.y;

  public override bool Equals(object obj) => obj is IntVector2 intVector2 && intVector2 == this;

  public override int GetHashCode() => base.GetHashCode();
}
