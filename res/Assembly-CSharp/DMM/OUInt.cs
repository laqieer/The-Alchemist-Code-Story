// Decompiled with JetBrains decompiler
// Type: OUInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using MessagePack;

#nullable disable
[MessagePackObject(true)]
public struct OUInt
{
  private ObscuredUInt value;

  public OUInt(uint value) => this.value = (ObscuredUInt) value;

  public static implicit operator OUInt(uint value) => new OUInt(value);

  public static implicit operator uint(OUInt value) => (uint) value.value;

  public static OUInt operator ++(OUInt value)
  {
    ref OUInt local = ref value;
    local.value = (ObscuredUInt) ((uint) local.value + 1U);
    return value;
  }

  public static OUInt operator --(OUInt value)
  {
    ref OUInt local = ref value;
    local.value = (ObscuredUInt) ((uint) local.value - 1U);
    return value;
  }

  public override string ToString() => this.value.ToString();

  public string ToString(string format) => this.value.ToString(format);
}
