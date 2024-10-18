// Decompiled with JetBrains decompiler
// Type: OLong
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using MessagePack;

#nullable disable
[MessagePackObject(true)]
public struct OLong
{
  private ObscuredLong value;

  public OLong(long value) => this.value = (ObscuredLong) value;

  public static implicit operator OLong(long value) => new OLong(value);

  public static implicit operator long(OLong value) => (long) value.value;

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

  public override string ToString() => this.value.ToString();
}
