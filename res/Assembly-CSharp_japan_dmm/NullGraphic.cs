﻿// Decompiled with JetBrains decompiler
// Type: NullGraphic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[ExecuteInEditMode]
public class NullGraphic : Graphic
{
  protected virtual void OnPopulateMesh(VertexHelper vh) => vh.Clear();
}
