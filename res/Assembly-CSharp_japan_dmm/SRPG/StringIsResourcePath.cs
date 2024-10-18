﻿// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsResourcePath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class StringIsResourcePath : PropertyAttribute
  {
    public System.Type ResourceType;
    public string ParentDirectory;
    public string EmptyLabel;

    public StringIsResourcePath(System.Type type)
    {
      this.ResourceType = type;
      this.ParentDirectory = (string) null;
    }

    public StringIsResourcePath(System.Type type, string dir)
    {
      this.ResourceType = type;
      this.ParentDirectory = dir;
    }

    public StringIsResourcePath(System.Type type, string dir, string empty_label)
    {
      this.ResourceType = type;
      this.ParentDirectory = dir;
      this.EmptyLabel = empty_label;
    }
  }
}
