﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventActionInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class EventActionInfo : Attribute
  {
    public string Name;
    public string Description;
    public Color32 DefaultColor;
    public Color32 FocusColor;

    public EventActionInfo(string name, string description, int defaultColor = 5592405, int focusColor = 4473992)
    {
      this.Name = name;
      this.Description = description;
      this.DefaultColor = new Color32((byte) (defaultColor >> 16 & (int) byte.MaxValue), (byte) (defaultColor >> 8 & (int) byte.MaxValue), (byte) (defaultColor & (int) byte.MaxValue), byte.MaxValue);
      this.FocusColor = new Color32((byte) (focusColor >> 16 & (int) byte.MaxValue), (byte) (focusColor >> 8 & (int) byte.MaxValue), (byte) (focusColor & (int) byte.MaxValue), byte.MaxValue);
    }
  }
}
