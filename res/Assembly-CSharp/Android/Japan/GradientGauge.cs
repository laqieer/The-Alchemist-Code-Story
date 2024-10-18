// Decompiled with JetBrains decompiler
// Type: GradientGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (CanvasRenderer))]
public class GradientGauge : Image
{
  public Color32[] Colors = new Color32[1]
  {
    new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue)
  };
  [Range(0.0f, 1f)]
  public float Value = 1f;
  private float mStartValue;
  private float mEndValue;
  private float mAnimateTime;
  private float mCurrentTime;

  public void UpdateColors(Color32[] colors)
  {
    this.Colors = colors;
    this.UpdateGeometry();
  }

  public void UpdateValue(float val)
  {
    this.Value = val;
    this.UpdateGeometry();
  }

  public bool IsAnimating
  {
    get
    {
      return (double) this.mCurrentTime < (double) this.mAnimateTime;
    }
  }

  public void AnimateRangedValue(int current, int maxValue, float time)
  {
    this.AnimateValue(Mathf.Clamp01((float) current / (float) maxValue), time);
  }

  public void AnimateValue(float newValue, float time)
  {
    this.mStartValue = this.Value;
    this.mEndValue = newValue;
    this.mAnimateTime = time;
    this.mCurrentTime = 0.0f;
    if ((double) time <= 0.0)
      this.Value = this.mEndValue;
    this.UpdateGeometry();
  }

  private void Update()
  {
    if ((double) this.mCurrentTime >= (double) this.mAnimateTime)
      return;
    this.mCurrentTime += Time.deltaTime;
    this.Value = Mathf.Lerp(this.mStartValue, this.mEndValue, Mathf.Clamp01(this.mCurrentTime / this.mAnimateTime));
    this.UpdateGeometry();
  }

  private Color32 ApplyBaseColor(Color32 c)
  {
    Color color = this.color;
    c.r = (byte) ((double) c.r * (double) color.r);
    c.g = (byte) ((double) c.g * (double) color.g);
    c.b = (byte) ((double) c.b * (double) color.b);
    c.a = (byte) ((double) color.a * (double) byte.MaxValue);
    return c;
  }

  protected override void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if ((Object) this.sprite == (Object) null || this.Colors.Length <= 0 || (double) this.Value <= 0.0)
      return;
    Sprite sprite = this.sprite;
    Vector4 border = sprite.border;
    Rect rect1 = sprite.rect;
    Rect rect2 = (this.transform as RectTransform).rect;
    float num1 = 1f / (float) sprite.texture.width;
    float num2 = 1f / (float) sprite.texture.height;
    UIVertex simpleVert = UIVertex.simpleVert;
    int num3 = 0;
    simpleVert.position.x = rect2.xMin;
    simpleVert.position.y = rect2.yMin;
    simpleVert.color = this.ApplyBaseColor(this.Colors[0]);
    simpleVert.uv0.x = rect1.xMin * num1;
    simpleVert.uv0.y = rect1.yMin * num2;
    vh.AddVert(simpleVert);
    simpleVert.position.y = rect2.yMax;
    simpleVert.uv0.y = rect1.yMax * num2;
    vh.AddVert(simpleVert);
    simpleVert.position.x = rect2.xMin + border.x;
    simpleVert.uv0.x = (rect1.xMin + border.x) * num1;
    vh.AddVert(simpleVert);
    simpleVert.position.y = rect2.yMin;
    simpleVert.uv0.y = rect1.yMin * num2;
    vh.AddVert(simpleVert);
    vh.AddTriangle(num3, num3 + 1, num3 + 2);
    vh.AddTriangle(num3 + 2, num3 + 3, num3);
    int num4 = num3 + 4;
    float num5 = (rect2.width - border.x - border.z) * this.Value;
    float a = (rect1.xMin + border.x) * num1;
    float b = (rect1.xMax - border.z) * num1;
    int num6 = 0;
    for (int index = 0; index < this.Colors.Length; ++index)
      num6 += (int) this.Colors[index].a;
    int num7 = 0;
    for (int index = 0; index < this.Colors.Length; ++index)
    {
      if (this.Colors[index].a > (byte) 0)
      {
        float t1 = (float) num7 / (float) num6;
        float t2 = (float) (num7 + (int) this.Colors[index].a) / (float) num6;
        float num8 = (float) ((double) rect2.xMin + (double) border.x + (double) num5 * (double) t1);
        float num9 = (float) ((double) rect2.xMin + (double) border.x + (double) num5 * (double) t2);
        simpleVert.position.x = num8;
        simpleVert.position.y = rect2.yMin;
        simpleVert.color = this.ApplyBaseColor(this.Colors[index]);
        simpleVert.uv0.x = Mathf.Lerp(a, b, t1);
        simpleVert.uv0.y = rect1.yMin * num2;
        vh.AddVert(simpleVert);
        simpleVert.position.y = rect2.yMax;
        simpleVert.uv0.y = rect1.yMax * num2;
        vh.AddVert(simpleVert);
        simpleVert.position.x = num9;
        simpleVert.uv0.x = Mathf.Lerp(a, b, t2);
        vh.AddVert(simpleVert);
        simpleVert.position.y = rect2.yMin;
        simpleVert.uv0.y = rect1.yMin * num2;
        vh.AddVert(simpleVert);
        vh.AddTriangle(num4, num4 + 1, num4 + 2);
        vh.AddTriangle(num4 + 2, num4 + 3, num4);
        num4 += 4;
        num7 += (int) this.Colors[index].a;
      }
    }
    float num10 = rect2.xMin + border.x + num5;
    simpleVert.position.x = num10;
    simpleVert.position.y = rect2.yMin;
    simpleVert.uv0.x = (rect1.xMax - border.x) * num1;
    simpleVert.uv0.y = rect1.yMin * num2;
    vh.AddVert(simpleVert);
    simpleVert.position.y = rect2.yMax;
    simpleVert.uv0.y = rect1.yMax * num2;
    vh.AddVert(simpleVert);
    simpleVert.position.x = num10 + border.z;
    simpleVert.uv0.x = rect1.xMax * num1;
    vh.AddVert(simpleVert);
    simpleVert.position.y = rect2.yMin;
    simpleVert.uv0.y = rect1.yMin * num2;
    vh.AddVert(simpleVert);
    vh.AddTriangle(num4, num4 + 1, num4 + 2);
    vh.AddTriangle(num4 + 2, num4 + 3, num4);
    int num11 = num4 + 4;
  }
}
