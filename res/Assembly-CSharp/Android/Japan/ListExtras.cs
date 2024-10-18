// Decompiled with JetBrains decompiler
// Type: ListExtras
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/ListExtras")]
[DisallowMultipleComponent]
[RequireComponent(typeof (ScrollRect))]
public class ListExtras : MonoBehaviour
{
  public float PageScrollTime = 0.3f;
  private float mScrollAnimTime = -1f;
  public Selectable PageUpButton;
  public Selectable PageDownButton;
  private Vector2 mScrollAnimStart;
  private Vector2 mScrollAnimEnd;
  private ScrollRect mScrollRect;

  protected void Awake()
  {
    this.mScrollRect = this.GetComponent<ScrollRect>();
    if (!((Object) this.mScrollRect == (Object) null))
      return;
    this.enabled = false;
  }

  protected void Update()
  {
    if ((double) this.mScrollAnimTime >= 0.0)
    {
      this.mScrollAnimTime += Time.deltaTime;
      float t = (double) this.PageScrollTime <= 0.0 ? 1f : Mathf.Sin((float) ((double) Mathf.Clamp01(this.mScrollAnimTime / this.PageScrollTime) * 3.14159274101257 * 0.5));
      this.mScrollRect.normalizedPosition = Vector2.Lerp(this.mScrollAnimStart, this.mScrollAnimEnd, t);
      if ((double) t >= 1.0)
        this.mScrollAnimTime = -1f;
    }
    float num1 = Mathf.Abs(Vector2.Dot(this.mScrollRect.normalizedPosition, this.ScrollDir));
    RectTransform transform1 = this.mScrollRect.transform as RectTransform;
    RectTransform transform2 = this.mScrollRect.content.transform as RectTransform;
    if (!((Object) this.mScrollRect.content != (Object) null))
      return;
    float num2 = Mathf.Abs(Vector2.Dot(transform1.rect.size, this.ScrollDir));
    float num3 = Mathf.Abs(Vector2.Dot(transform2.rect.size, this.ScrollDir));
    if (this.mScrollRect.horizontal)
      num1 = 1f - num1;
    if ((Object) this.PageUpButton != (Object) null)
      this.PageUpButton.interactable = (double) num1 < 0.999000012874603 && (double) num2 < (double) num3;
    if (!((Object) this.PageDownButton != (Object) null))
      return;
    this.PageDownButton.interactable = (double) num1 > 1.0 / 1000.0 && (double) num2 < (double) num3;
  }

  private Vector2 ScrollDir
  {
    get
    {
      if (this.mScrollRect.vertical)
        return -Vector2.up;
      return Vector2.right;
    }
  }

  public void PageUp(float delta)
  {
    this.Scroll(-delta);
  }

  public void PageDown(float delta)
  {
    this.Scroll(delta);
  }

  public void SetScrollPos(float position)
  {
    this.mScrollRect.normalizedPosition = new Vector2(Mathf.Abs(this.ScrollDir.x), Mathf.Abs(this.ScrollDir.y)) * position;
    this.mScrollAnimTime = -1f;
  }

  public void ScrollTo(float normalizedPosition)
  {
    this.mScrollAnimStart = this.mScrollRect.normalizedPosition;
    this.mScrollAnimEnd = this.ScrollDir * normalizedPosition;
    this.mScrollAnimTime = 0.0f;
  }

  private void Scroll(float delta)
  {
    Vector2 scrollDir = this.ScrollDir;
    RectTransform content = this.mScrollRect.content;
    RectTransform transform = (RectTransform) this.mScrollRect.transform;
    float num1 = Mathf.Abs(Vector2.Dot(scrollDir, transform.rect.size));
    float num2 = Mathf.Abs(Vector2.Dot(scrollDir, content.rect.size)) - num1;
    if ((double) num2 <= 0.0)
      return;
    float num3 = num1 * delta / num2;
    Vector2 vector2 = this.mScrollRect.normalizedPosition + scrollDir * num3;
    vector2.x = Mathf.Clamp01(vector2.x);
    vector2.y = Mathf.Clamp01(vector2.y);
    this.mScrollAnimStart = this.mScrollRect.normalizedPosition;
    this.mScrollAnimEnd = vector2;
    this.mScrollAnimTime = 0.0f;
  }
}
