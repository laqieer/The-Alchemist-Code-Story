// Decompiled with JetBrains decompiler
// Type: OIntVector2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
[MessagePackObject(true)]
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

  public static bool operator ==(OIntVector2 a, OIntVector2 b)
  {
    return (int) a.x == (int) b.x && (int) a.y == (int) b.y;
  }

  public static bool operator !=(OIntVector2 a, OIntVector2 b)
  {
    return (int) a.x != (int) b.x || (int) a.y != (int) b.y;
  }

  public override bool Equals(object obj) => obj is OIntVector2 ointVector2 && ointVector2 == this;

  public override int GetHashCode() => base.GetHashCode();
}
