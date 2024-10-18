// Decompiled with JetBrains decompiler
// Type: OInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using MessagePack;

#nullable disable
[MessagePackObject(true)]
public struct OInt
{
  private ObscuredInt value;

  public OInt(int value) => this.value = (ObscuredInt) value;

  public static implicit operator OInt(int value) => new OInt(value);

  public static implicit operator int(OInt value) => (int) value.value;

  public static implicit operator OInt(short value) => new OInt((int) value);

  public static implicit operator OInt(OShort value) => new OInt((int) value);

  public static implicit operator OInt(sbyte value) => new OInt((int) value);

  public static implicit operator OInt(OSbyte value) => new OInt((int) (sbyte) value);

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

  public override string ToString() => this.value.ToString();
}
