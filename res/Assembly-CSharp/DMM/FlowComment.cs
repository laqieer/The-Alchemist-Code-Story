// Decompiled with JetBrains decompiler
// Type: FlowComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
public class FlowComment : MonoBehaviour
{
  public List<FlowComment.Comment> Comments = new List<FlowComment.Comment>();

  private void Awake()
  {
  }

  [Serializable]
  public struct Comment
  {
    public Vector2 Position;
    public string Text;
    public Color Color;
    public Color Background;
    public int FontSize;

    public Comment(Vector2 pos, string text)
    {
      this.Position = pos;
      this.Text = text;
      this.FontSize = 20;
      this.Background = Color.gray;
      this.Color = Color.black;
    }
  }
}
