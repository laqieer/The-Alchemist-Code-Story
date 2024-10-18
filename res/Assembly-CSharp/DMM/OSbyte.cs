// Decompiled with JetBrains decompiler
// Type: OSbyte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using MessagePack;

#nullable disable
[MessagePackObject(true)]
public struct OSbyte
{
  private ObscuredSByte value;

  public OSbyte(sbyte value) => this.value = (ObscuredSByte) value;

  public static implicit operator OSbyte(sbyte value) => new OSbyte(value);

  public static implicit operator sbyte(OSbyte value) => (sbyte) value.value;

  public static OSbyte operator ++(OSbyte value)
  {
    ref OSbyte local = ref value;
    local.value = (ObscuredSByte) (sbyte) ((int) (sbyte) local.value + 1);
    return value;
  }

  public static OSbyte operator --(OSbyte value)
  {
    ref OSbyte local = ref value;
    local.value = (ObscuredSByte) (sbyte) ((int) (sbyte) local.value - 1);
    return value;
  }

  public override string ToString() => this.value.ToString();
}
