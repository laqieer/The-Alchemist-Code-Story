﻿// Decompiled with JetBrains decompiler
// Type: SRPG.CustomFieldAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class CustomFieldAttribute : Attribute
  {
    public CustomFieldAttribute(string _text, CustomFieldAttribute.Type _type)
    {
    }

    public enum Type
    {
      MonoBehaviour,
      GameObject,
      UIText,
      UIRawImage,
      UIImage,
      UISprite,
    }
  }
}