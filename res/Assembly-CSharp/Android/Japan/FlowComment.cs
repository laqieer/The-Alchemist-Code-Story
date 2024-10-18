// Decompiled with JetBrains decompiler
// Type: FlowComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
