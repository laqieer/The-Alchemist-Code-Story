// Decompiled with JetBrains decompiler
// Type: OBool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OBool
{
  private ObscuredBool value;

  public OBool(bool value)
  {
    this.value = (ObscuredBool) value;
  }

  public static implicit operator OBool(bool value)
  {
    return new OBool(value);
  }

  public static implicit operator bool(OBool value)
  {
    return (bool) value.value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }
}
