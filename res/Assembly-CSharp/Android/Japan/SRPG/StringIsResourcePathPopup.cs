﻿// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsResourcePathPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class StringIsResourcePathPopup : PropertyAttribute
  {
    public System.Type ResourceType;
    public string ParentDirectory;

    public StringIsResourcePathPopup(System.Type type)
    {
      this.ResourceType = type;
      this.ParentDirectory = (string) null;
    }

    public StringIsResourcePathPopup(System.Type type, string dir)
    {
      this.ResourceType = type;
      this.ParentDirectory = dir;
    }
  }
}
