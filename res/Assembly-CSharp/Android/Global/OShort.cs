﻿// Decompiled with JetBrains decompiler
// Type: OShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OShort
{
  private ObscuredShort value;

  public OShort(short value)
  {
    this.value = (ObscuredShort) value;
  }

  public OShort(int value)
  {
    this.value = (ObscuredShort) ((short) value);
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OShort(short value)
  {
    return new OShort(value);
  }

  public static implicit operator short(OShort value)
  {
    return (short) value.value;
  }

  public static implicit operator int(OShort value)
  {
    return (int) (short) value.value;
  }

  public static implicit operator OShort(OInt value)
  {
    return new OShort((int) value);
  }

  public static implicit operator OShort(int value)
  {
    return new OShort(value);
  }

  public static OShort operator ++(OShort value)
  {
    ref OShort local = ref value;
    local.value = (ObscuredShort) ((short) ((int) (short) local.value + 1));
    return value;
  }

  public static OShort operator --(OShort value)
  {
    ref OShort local = ref value;
    local.value = (ObscuredShort) ((short) ((int) (short) local.value - 1));
    return value;
  }
}
