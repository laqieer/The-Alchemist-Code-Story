// Decompiled with JetBrains decompiler
// Type: RichBitmapText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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

  protected virtual void OnPopulateMesh(VertexHelper toFill)
  {
    toFill.Clear();
    base.OnPopulateMesh(toFill);
    UIVertex simpleVert = UIVertex.simpleVert;
    int currentVertCount = toFill.currentVertCount;
    int num1;
    for (int index = 0; index < currentVertCount; index = num1 + 1)
    {
      toFill.PopulateUIVertex(ref simpleVert, index);
      simpleVert.color = RichBitmapText.Multiply(simpleVert.color, this.TopColor);
      toFill.SetUIVertex(simpleVert, index);
      int num2 = index + 1;
      toFill.PopulateUIVertex(ref simpleVert, num2);
      simpleVert.color = RichBitmapText.Multiply(simpleVert.color, this.TopColor);
      toFill.SetUIVertex(simpleVert, num2);
      int num3 = num2 + 1;
      toFill.PopulateUIVertex(ref simpleVert, num3);
      simpleVert.color = RichBitmapText.Multiply(simpleVert.color, this.BottomColor);
      toFill.SetUIVertex(simpleVert, num3);
      num1 = num3 + 1;
      toFill.PopulateUIVertex(ref simpleVert, num1);
      simpleVert.color = RichBitmapText.Multiply(simpleVert.color, this.BottomColor);
      toFill.SetUIVertex(simpleVert, num1);
    }
    if ((double) this.Shear == 0.0)
      return;
    float num4 = this.Shear * (float) this.fontSize;
    int num5;
    for (int index = 0; index < currentVertCount; index = num5 + 1)
    {
      toFill.PopulateUIVertex(ref simpleVert, index);
      simpleVert.position.x += num4;
      toFill.SetUIVertex(simpleVert, index);
      int num6 = index + 1;
      toFill.PopulateUIVertex(ref simpleVert, num6);
      simpleVert.position.x += num4;
      toFill.SetUIVertex(simpleVert, num6);
      int num7 = num6 + 1;
      toFill.PopulateUIVertex(ref simpleVert, num7);
      simpleVert.position.x -= num4;
      toFill.SetUIVertex(simpleVert, num7);
      num5 = num7 + 1;
      toFill.PopulateUIVertex(ref simpleVert, num5);
      simpleVert.position.x -= num4;
      toFill.SetUIVertex(simpleVert, num5);
    }
  }
}
