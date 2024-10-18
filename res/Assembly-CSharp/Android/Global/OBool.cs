// Decompiled with JetBrains decompiler
// Type: OBool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OBool
{
  private ObscuredBool value;

  public OBool(bool value)
  {
    this.value = (ObscuredBool) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OBool(bool value)
  {
    return new OBool(value);
  }

  public static implicit operator bool(OBool value)
  {
    return (bool) value.value;
  }
}
