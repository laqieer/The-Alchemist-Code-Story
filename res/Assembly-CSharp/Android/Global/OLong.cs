// Decompiled with JetBrains decompiler
// Type: OLong
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OLong
{
  private ObscuredLong value;

  public OLong(long value)
  {
    this.value = (ObscuredLong) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OLong(long value)
  {
    return new OLong(value);
  }

  public static implicit operator long(OLong value)
  {
    return (long) value.value;
  }

  public static OLong operator ++(OLong value)
  {
    ref OLong local = ref value;
    local.value = (ObscuredLong) ((long) local.value + 1L);
    return value;
  }

  public static OLong operator --(OLong value)
  {
    ref OLong local = ref value;
    local.value = (ObscuredLong) ((long) local.value - 1L);
    return value;
  }
}
