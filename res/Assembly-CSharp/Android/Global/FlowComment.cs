// Decompiled with JetBrains decompiler
// Type: FlowComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

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
