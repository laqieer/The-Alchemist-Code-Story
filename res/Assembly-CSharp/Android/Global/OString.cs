// Decompiled with JetBrains decompiler
// Type: OString
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OString
{
  private ObscuredString value;

  public OString(string value)
  {
    this.value = (ObscuredString) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OString(string value)
  {
    return new OString(value);
  }

  public static implicit operator string(OString value)
  {
    return (string) value.value;
  }
}
