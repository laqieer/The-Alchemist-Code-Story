﻿// Decompiled with JetBrains decompiler
// Type: SRPG.CustomFieldAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
