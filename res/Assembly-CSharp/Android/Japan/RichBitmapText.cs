// Decompiled with JetBrains decompiler
// Type: RichBitmapText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class RichBitmapText : BitmapText
{
  public Color32 TopColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
  public Color32 BottomColor = new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
  [Range(-1f, 1f)]
  public float Shear;

  private static Color32 Multiply(Color32 x, Color32 y)
  {
    x.r = (byte) ((int) x.r * (int) y.r / (int) byte.MaxValue);
    x.g = (byte) ((int) x.g * (int) y.g / (int) byte.MaxValue);
    x.b = (byte) ((int) x.b * (int) y.b / (int) byte.MaxValue);
    x.a = (byte) ((int) x.a * (int) y.a / (int) byte.MaxValue);
    return x;
  }

  protected override void OnPopulateMesh(VertexHelper toFill)
  {
    toFill.Clear();
    base.OnPopulateMesh(toFill);
    UIVertex simpleVert = UIVertex.simpleVert;
    int currentVertCount = toFill.currentVertCount;
    int i1;
    for (int i2 = 0; i2 < currentVertCount; i2 = i1 + 1)
    {
      toFill.PopulateUIVertex(ref simpleVert, i2);
      simpleVert.color = RichBitmapText.Multiply(simpleVert.color, this.TopColor);
      toFill.SetUIVertex(simpleVert, i2);
      int i3 = i2 + 1;
      toFill.PopulateUIVertex(ref simpleVert, i3);
      simpleVert.color = RichBitmapText.Multiply(simpleVert.color, this.TopColor);
      toFill.SetUIVertex(simpleVert, i3);
      int i4 = i3 + 1;
      toFill.PopulateUIVertex(ref simpleVert, i4);
      simpleVert.color = RichBitmapText.Multiply(simpleVert.color, this.BottomColor);
      toFill.SetUIVertex(simpleVert, i4);
      i1 = i4 + 1;
      toFill.PopulateUIVertex(ref simpleVert, i1);
      simpleVert.color = RichBitmapText.Multiply(simpleVert.color, this.BottomColor);
      toFill.SetUIVertex(simpleVert, i1);
    }
    if ((double) this.Shear == 0.0)
      return;
    float num = this.Shear * (float) this.fontSize;
    int i5;
    for (int i2 = 0; i2 < currentVertCount; i2 = i5 + 1)
    {
      toFill.PopulateUIVertex(ref simpleVert, i2);
      simpleVert.position.x += num;
      toFill.SetUIVertex(simpleVert, i2);
      int i3 = i2 + 1;
      toFill.PopulateUIVertex(ref simpleVert, i3);
      simpleVert.position.x += num;
      toFill.SetUIVertex(simpleVert, i3);
      int i4 = i3 + 1;
      toFill.PopulateUIVertex(ref simpleVert, i4);
      simpleVert.position.x -= num;
      toFill.SetUIVertex(simpleVert, i4);
      i5 = i4 + 1;
      toFill.PopulateUIVertex(ref simpleVert, i5);
      simpleVert.position.x -= num;
      toFill.SetUIVertex(simpleVert, i5);
    }
  }
}
