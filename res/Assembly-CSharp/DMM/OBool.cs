// Decompiled with JetBrains decompiler
// Type: OBool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using MessagePack;

#nullable disable
[MessagePackObject(true)]
public struct OBool
{
  private ObscuredBool value;

  public OBool(bool value) => this.value = (ObscuredBool) value;

  public static implicit operator OBool(bool value) => new OBool(value);

  public static implicit operator bool(OBool value) => (bool) value.value;

  public override string ToString() => this.value.ToString();
}
