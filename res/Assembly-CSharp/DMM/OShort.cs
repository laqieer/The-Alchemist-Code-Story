// Decompiled with JetBrains decompiler
// Type: OShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using MessagePack;

#nullable disable
[MessagePackObject(true)]
public struct OShort
{
  private ObscuredShort value;

  public OShort(short value) => this.value = (ObscuredShort) value;

  public OShort(int value) => this.value = (ObscuredShort) (short) value;

  public static implicit operator OShort(short value) => new OShort(value);

  public static implicit operator short(OShort value) => (short) value.value;

  public static implicit operator int(OShort value) => (int) (short) value.value;

  public static implicit operator OShort(OInt value) => new OShort((int) value);

  public static implicit operator OShort(int value) => new OShort(value);

  public static OShort operator ++(OShort value)
  {
    ref OShort local = ref value;
    local.value = (ObscuredShort) (short) ((int) (short) local.value + 1);
    return value;
  }

  public static OShort operator --(OShort value)
  {
    ref OShort local = ref value;
    local.value = (ObscuredShort) (short) ((int) (short) local.value - 1);
    return value;
  }

  public override string ToString() => this.value.ToString();
}
