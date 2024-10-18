// Decompiled with JetBrains decompiler
// Type: OInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OInt
{
  private ObscuredInt value;

  public OInt(int value)
  {
    this.value = (ObscuredInt) value;
  }

  public static implicit operator OInt(int value)
  {
    return new OInt(value);
  }

  public static implicit operator int(OInt value)
  {
    return (int) value.value;
  }

  public static implicit operator OInt(short value)
  {
    return new OInt((int) value);
  }

  public static implicit operator OInt(OShort value)
  {
    return new OInt((int) value);
  }

  public static implicit operator OInt(sbyte value)
  {
    return new OInt((int) value);
  }

  public static implicit operator OInt(OSbyte value)
  {
    return new OInt((int) (sbyte) value);
  }

  public static OInt operator ++(OInt value)
  {
    ref OInt local = ref value;
    local.value = (ObscuredInt) ((int) local.value + 1);
    return value;
  }

  public static OInt operator --(OInt value)
  {
    ref OInt local = ref value;
    local.value = (ObscuredInt) ((int) local.value - 1);
    return value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }
}
