// Decompiled with JetBrains decompiler
// Type: ListExtras
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("UI/ListExtras")]
[DisallowMultipleComponent]
[RequireComponent(typeof (ScrollRect))]
public class ListExtras : MonoBehaviour
{
  public Selectable PageUpButton;
  public Selectable PageDownButton;
  public float PageScrollTime = 0.3f;
  private Vector2 mScrollAnimStart;
  private Vector2 mScrollAnimEnd;
  private float mScrollAnimTime = -1f;
  private ScrollRect mScrollRect;

  protected void Awake()
  {
    this.mScrollRect = ((Component) this).GetComponent<ScrollRect>();
    if (!Object.op_Equality((Object) this.mScrollRect, (Object) null))
      return;
    ((Behaviour) this).enabled = false;
  }

  protected void Update()
  {
    if ((double) this.mScrollAnimTime >= 0.0)
    {
      this.mScrollAnimTime += Time.deltaTime;
      float num = (double) this.PageScrollTime <= 0.0 ? 1f : Mathf.Sin((float) ((double) Mathf.Clamp01(this.mScrollAnimTime / this.PageScrollTime) * 3.1415927410125732 * 0.5));
      this.mScrollRect.normalizedPosition = Vector2.Lerp(this.mScrollAnimStart, this.mScrollAnimEnd, num);
      if ((double) num >= 1.0)
        this.mScrollAnimTime = -1f;
    }
    float num1 = Mathf.Abs(Vector2.Dot(this.mScrollRect.normalizedPosition, this.ScrollDir));
    RectTransform transform1 = ((Component) this.mScrollRect).transform as RectTransform;
    RectTransform transform2 = ((Component) this.mScrollRect.content).transform as RectTransform;
    if (!Object.op_Inequality((Object) this.mScrollRect.content, (Object) null))
      return;
    Rect rect1 = transform1.rect;
    float num2 = Mathf.Abs(Vector2.Dot(((Rect) ref rect1).size, this.ScrollDir));
    Rect rect2 = transform2.rect;
    float num3 = Mathf.Abs(Vector2.Dot(((Rect) ref rect2).size, this.ScrollDir));
    if (this.mScrollRect.horizontal)
      num1 = 1f - num1;
    if (Object.op_Inequality((Object) this.PageUpButton, (Object) null))
      this.PageUpButton.interactable = (double) num1 < 0.99900001287460327 && (double) num2 < (double) num3;
    if (!Object.op_Inequality((Object) this.PageDownButton, (Object) null))
      return;
    this.PageDownButton.interactable = (double) num1 > 1.0 / 1000.0 && (double) num2 < (double) num3;
  }

  private Vector2 ScrollDir
  {
    get => this.mScrollRect.vertical ? Vector2.op_UnaryNegation(Vector2.up) : Vector2.right;
  }

  public void PageUp(float delta) => this.Scroll(-delta);

  public void PageDown(float delta) => this.Scroll(delta);

  public void SetScrollPos(float position)
  {
    this.mScrollRect.normalizedPosition = Vector2.op_Multiply(new Vector2(Mathf.Abs(this.ScrollDir.x), Mathf.Abs(this.ScrollDir.y)), position);
    this.mScrollAnimTime = -1f;
  }

  public void ScrollTo(float normalizedPosition)
  {
    this.mScrollAnimStart = this.mScrollRect.normalizedPosition;
    this.mScrollAnimEnd = Vector2.op_Multiply(this.ScrollDir, normalizedPosition);
    this.mScrollAnimTime = 0.0f;
  }

  private void Scroll(float delta)
  {
    Vector2 scrollDir = this.ScrollDir;
    RectTransform content = this.mScrollRect.content;
    RectTransform transform = (RectTransform) ((Component) this.mScrollRect).transform;
    Vector2 vector2_1 = scrollDir;
    Rect rect1 = transform.rect;
    Vector2 size1 = ((Rect) ref rect1).size;
    float num1 = Mathf.Abs(Vector2.Dot(vector2_1, size1));
    Vector2 vector2_2 = scrollDir;
    Rect rect2 = content.rect;
    Vector2 size2 = ((Rect) ref rect2).size;
    float num2 = Mathf.Abs(Vector2.Dot(vector2_2, size2)) - num1;
    if ((double) num2 <= 0.0)
      return;
    float num3 = num1 * delta / num2;
    Vector2 vector2_3 = Vector2.op_Addition(this.mScrollRect.normalizedPosition, Vector2.op_Multiply(scrollDir, num3));
    vector2_3.x = Mathf.Clamp01(vector2_3.x);
    vector2_3.y = Mathf.Clamp01(vector2_3.y);
    this.mScrollAnimStart = this.mScrollRect.normalizedPosition;
    this.mScrollAnimEnd = vector2_3;
    this.mScrollAnimTime = 0.0f;
  }
}
