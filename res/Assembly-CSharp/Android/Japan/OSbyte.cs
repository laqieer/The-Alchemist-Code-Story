// Decompiled with JetBrains decompiler
// Type: OSbyte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OSbyte
{
  private ObscuredSByte value;

  public OSbyte(sbyte value)
  {
    this.value = (ObscuredSByte) value;
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

  public override string ToString()
  {
    return this.value.ToString();
  }
}
