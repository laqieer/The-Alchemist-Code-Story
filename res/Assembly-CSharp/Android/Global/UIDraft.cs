﻿// Decompiled with JetBrains decompiler
// Type: UIDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public abstract class UIDraft : MonoBehaviour
{
  private void Start()
  {
    this.enabled = false;
  }

  public class AutoGenerated : Attribute
  {
  }

  public class ObsoleteHeader : PropertyAttribute
  {
    public readonly string header;

    public ObsoleteHeader(string header)
    {
      this.header = header;
    }
  }
}
