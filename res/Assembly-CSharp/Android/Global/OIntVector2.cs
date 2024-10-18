// Decompiled with JetBrains decompiler
// Type: OIntVector2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

public struct OIntVector2
{
  public OInt x;
  public OInt y;

  public OIntVector2(int a, int b)
  {
    this.x = (OInt) a;
    this.y = (OInt) b;
  }

  public override string ToString()
  {
    return string.Format("[OIntVector2] {0}, {1}", (object) this.x, (object) this.y);
  }

  public override bool Equals(object obj)
  {
    if (obj is OIntVector2)
      return (OIntVector2) obj == this;
    return false;
  }

  public override int GetHashCode()
  {
    return base.GetHashCode();
  }

  public static bool operator ==(OIntVector2 a, OIntVector2 b)
  {
    if ((int) a.x == (int) b.x)
      return (int) a.y == (int) b.y;
    return false;
  }

  public static bool operator !=(OIntVector2 a, OIntVector2 b)
  {
    if ((int) a.x == (int) b.x)
      return (int) a.y != (int) b.y;
    return true;
  }
}
