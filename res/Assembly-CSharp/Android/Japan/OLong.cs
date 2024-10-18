// Decompiled with JetBrains decompiler
// Type: OLong
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OLong
{
  private ObscuredLong value;

  public OLong(long value)
  {
    this.value = (ObscuredLong) value;
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

  public override string ToString()
  {
    return this.value.ToString();
  }
}
