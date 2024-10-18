// Decompiled with JetBrains decompiler
// Type: OString
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.ObscuredTypes;
using MessagePack;

#nullable disable
[MessagePackObject(true)]
public struct OString
{
  private ObscuredString value;

  public OString(string value) => this.value = (ObscuredString) value;

  public static implicit operator OString(string value) => new OString(value);

  public static implicit operator string(OString value) => (string) value.value;

  public override string ToString() => this.value.ToString();
}
