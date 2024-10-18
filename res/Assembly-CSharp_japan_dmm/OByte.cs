// Decompiled with JetBrains decompiler
// Type: OByte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using MessagePack;

#nullable disable
[MessagePackObject(true)]
public struct OByte
{
  private ObscuredByte value;

  public OByte(byte value) => this.value = (ObscuredByte) value;

  public static implicit operator OByte(byte value) => new OByte(value);

  public static implicit operator byte(OByte value) => (byte) value.value;

  public static OByte operator ++(OByte value)
  {
    ref OByte local = ref value;
    local.value = (ObscuredByte) (byte) ((uint) (byte) local.value + 1U);
    return value;
  }

  public static OByte operator --(OByte value)
  {
    ref OByte local = ref value;
    local.value = (ObscuredByte) (byte) ((uint) (byte) local.value - 1U);
    return value;
  }

  public override string ToString() => this.value.ToString();
}
