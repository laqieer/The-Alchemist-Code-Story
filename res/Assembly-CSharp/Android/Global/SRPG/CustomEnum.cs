// Decompiled with JetBrains decompiler
// Type: SRPG.CustomEnum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CustomEnum : PropertyAttribute
  {
    public System.Type EnumType;
    public int DefaultValue;

    public CustomEnum(System.Type enumType, int defaultValue)
    {
      this.EnumType = enumType;
      this.DefaultValue = defaultValue;
    }
  }
}
