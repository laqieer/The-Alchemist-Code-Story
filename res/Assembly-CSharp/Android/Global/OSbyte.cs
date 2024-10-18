// Decompiled with JetBrains decompiler
// Type: OSbyte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OSbyte
{
  private ObscuredSByte value;

  public OSbyte(sbyte value)
  {
    this.value = (ObscuredSByte) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OSbyte(sbyte value)
  {
    return new OSbyte(value);
  }

  public static implicit operator sbyte(OSbyte value)
  {
    return (sbyte) value.value;
  }

  public static OSbyte operator ++(OSbyte value)
  {
    ref OSbyte local = ref value;
    local.value = (ObscuredSByte) ((sbyte) ((int) (sbyte) local.value + 1));
    return value;
  }

  public static OSbyte operator --(OSbyte value)
  {
    ref OSbyte local = ref value;
    local.value = (ObscuredSByte) ((sbyte) ((int) (sbyte) local.value - 1));
    return value;
  }
}
