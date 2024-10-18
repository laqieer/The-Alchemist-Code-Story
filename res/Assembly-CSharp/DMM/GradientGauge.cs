// Decompiled with JetBrains decompiler
// Type: GradientGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
    ((Graphic) this).UpdateGeometry();
  }

  public void UpdateValue(float val)
  {
    this.Value = val;
    ((Graphic) this).UpdateGeometry();
  }

  public bool IsAnimating => (double) this.mCurrentTime < (double) this.mAnimateTime;

  public void AnimateRangedValue(long current, long maxValue, float time)
  {
    this.AnimateValue(maxValue == 0L ? 0.0f : Mathf.Clamp01((float) current / (float) maxValue), time);
  }

  public void AnimateValue(float newValue, float time)
  {
    this.mStartValue = this.Value;
    this.mEndValue = newValue;
    this.mAnimateTime = time;
    this.mCurrentTime = 0.0f;
    if ((double) time <= 0.0)
      this.Value = this.mEndValue;
    ((Graphic) this).UpdateGeometry();
  }

  private void Update()
  {
    if ((double) this.mCurrentTime >= (double) this.mAnimateTime)
      return;
    this.mCurrentTime += Time.deltaTime;
    this.Value = Mathf.Lerp(this.mStartValue, this.mEndValue, Mathf.Clamp01(this.mCurrentTime / this.mAnimateTime));
    ((Graphic) this).UpdateGeometry();
  }

  private Color32 ApplyBaseColor(Color32 c)
  {
    Color color = ((Graphic) this).color;
    c.r = (byte) ((double) c.r * (double) color.r);
    c.g = (byte) ((double) c.g * (double) color.g);
    c.b = (byte) ((double) c.b * (double) color.b);
    c.a = (byte) ((double) color.a * (double) byte.MaxValue);
    return c;
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if (Object.op_Equality((Object) this.sprite, (Object) null) || this.Colors.Length <= 0 || (double) this.Value <= 0.0)
      return;
    Sprite sprite = this.sprite;
    Vector4 border = sprite.border;
    Rect rect1 = sprite.rect;
    Rect rect2 = (((Component) this).transform as RectTransform).rect;
    float num1 = 1f / (float) ((Texture) sprite.texture).width;
    float num2 = 1f / (float) ((Texture) sprite.texture).height;
    UIVertex simpleVert = UIVertex.simpleVert;
    int num3 = 0;
    simpleVert.position.x = ((Rect) ref rect2).xMin;
    simpleVert.position.y = ((Rect) ref rect2).yMin;
    simpleVert.color = this.ApplyBaseColor(this.Colors[0]);
    simpleVert.uv0.x = ((Rect) ref rect1).xMin * num1;
    simpleVert.uv0.y = ((Rect) ref rect1).yMin * num2;
    vh.AddVert(simpleVert);
    simpleVert.position.y = ((Rect) ref rect2).yMax;
    simpleVert.uv0.y = ((Rect) ref rect1).yMax * num2;
    vh.AddVert(simpleVert);
    simpleVert.position.x = ((Rect) ref rect2).xMin + border.x;
    simpleVert.uv0.x = (((Rect) ref rect1).xMin + border.x) * num1;
    vh.AddVert(simpleVert);
    simpleVert.position.y = ((Rect) ref rect2).yMin;
    simpleVert.uv0.y = ((Rect) ref rect1).yMin * num2;
    vh.AddVert(simpleVert);
    vh.AddTriangle(num3, num3 + 1, num3 + 2);
    vh.AddTriangle(num3 + 2, num3 + 3, num3);
    int num4 = num3 + 4;
    float num5 = (((Rect) ref rect2).width - border.x - border.z) * this.Value;
    float num6 = (((Rect) ref rect1).xMin + border.x) * num1;
    float num7 = (((Rect) ref rect1).xMax - border.z) * num1;
    int num8 = 0;
    for (int index = 0; index < this.Colors.Length; ++index)
      num8 += (int) this.Colors[index].a;
    int num9 = 0;
    for (int index = 0; index < this.Colors.Length; ++index)
    {
      if (this.Colors[index].a > (byte) 0)
      {
        float num10 = (float) num9 / (float) num8;
        float num11 = (float) (num9 + (int) this.Colors[index].a) / (float) num8;
        float num12 = (float) ((double) ((Rect) ref rect2).xMin + (double) border.x + (double) num5 * (double) num10);
        float num13 = (float) ((double) ((Rect) ref rect2).xMin + (double) border.x + (double) num5 * (double) num11);
        simpleVert.position.x = num12;
        simpleVert.position.y = ((Rect) ref rect2).yMin;
        simpleVert.color = this.ApplyBaseColor(this.Colors[index]);
        simpleVert.uv0.x = Mathf.Lerp(num6, num7, num10);
        simpleVert.uv0.y = ((Rect) ref rect1).yMin * num2;
        vh.AddVert(simpleVert);
        simpleVert.position.y = ((Rect) ref rect2).yMax;
        simpleVert.uv0.y = ((Rect) ref rect1).yMax * num2;
        vh.AddVert(simpleVert);
        simpleVert.position.x = num13;
        simpleVert.uv0.x = Mathf.Lerp(num6, num7, num11);
        vh.AddVert(simpleVert);
        simpleVert.position.y = ((Rect) ref rect2).yMin;
        simpleVert.uv0.y = ((Rect) ref rect1).yMin * num2;
        vh.AddVert(simpleVert);
        vh.AddTriangle(num4, num4 + 1, num4 + 2);
        vh.AddTriangle(num4 + 2, num4 + 3, num4);
        num4 += 4;
        num9 += (int) this.Colors[index].a;
      }
    }
    float num14 = ((Rect) ref rect2).xMin + border.x + num5;
    simpleVert.position.x = num14;
    simpleVert.position.y = ((Rect) ref rect2).yMin;
    simpleVert.uv0.x = (((Rect) ref rect1).xMax - border.x) * num1;
    simpleVert.uv0.y = ((Rect) ref rect1).yMin * num2;
    vh.AddVert(simpleVert);
    simpleVert.position.y = ((Rect) ref rect2).yMax;
    simpleVert.uv0.y = ((Rect) ref rect1).yMax * num2;
    vh.AddVert(simpleVert);
    simpleVert.position.x = num14 + border.z;
    simpleVert.uv0.x = ((Rect) ref rect1).xMax * num1;
    vh.AddVert(simpleVert);
    simpleVert.position.y = ((Rect) ref rect2).yMin;
    simpleVert.uv0.y = ((Rect) ref rect1).yMin * num2;
    vh.AddVert(simpleVert);
    vh.AddTriangle(num4, num4 + 1, num4 + 2);
    vh.AddTriangle(num4 + 2, num4 + 3, num4);
    int num15 = num4 + 4;
  }

  public enum eColorType
  {
    NOW,
    DIF,
  }
}
