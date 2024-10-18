// Decompiled with JetBrains decompiler
// Type: OByte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OByte
{
  private ObscuredByte value;

  public OByte(byte value)
  {
    this.value = (ObscuredByte) value;
  }

  public static implicit operator OByte(byte value)
  {
    return new OByte(value);
  }

  public static implicit operator byte(OByte value)
  {
    return (byte) value.value;
  }

  public static OByte operator ++(OByte value)
  {
    ref OByte local = ref value;
    local.value = (ObscuredByte) ((byte) ((uint) (byte) local.value + 1U));
    return value;
  }

  public static OByte operator --(OByte value)
  {
    ref OByte local = ref value;
    local.value = (ObscuredByte) ((byte) ((uint) (byte) local.value - 1U));
    return value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }
}
