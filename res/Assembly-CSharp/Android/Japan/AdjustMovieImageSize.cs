// Decompiled with JetBrains decompiler
// Type: AdjustMovieImageSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdjustMovieImageSize : UIBehaviour
{
  private RectTransform m_ParentTransform;
  private RectTransform m_Transform;
  private Coroutine m_AdjustCoroutine;

  protected override void Awake()
  {
    base.Awake();
    this.m_Transform = this.transform as RectTransform;
    this.m_ParentTransform = this.transform.parent as RectTransform;
  }

  protected override void Start()
  {
    base.Start();
    this.AsyncAdjustRecttransformSize();
  }

  protected override void OnRectTransformDimensionsChange()
  {
    this.AsyncAdjustRecttransformSize();
  }

  private void AsyncAdjustRecttransformSize()
  {
    if (!this.gameObject.activeInHierarchy)
      return;
    if (this.m_AdjustCoroutine != null)
      this.StopCoroutine(this.m_AdjustCoroutine);
    this.m_AdjustCoroutine = this.StartCoroutine(this.StartAdjustSize());
  }

  [DebuggerHidden]
  private IEnumerator StartAdjustSize()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AdjustMovieImageSize.\u003CStartAdjustSize\u003Ec__Iterator0()
    {
      \u0024this = this
    };
  }

  private void InternalAdjustRecttransformSize(float canvasBoundsScale)
  {
    this.m_Transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.m_ParentTransform.rect.size.y);
    float num = 1f / canvasBoundsScale;
    Vector2 localScale = (Vector2) this.m_Transform.localScale;
    localScale.x = num;
    localScale.y = num;
    this.m_Transform.localScale = (Vector3) localScale;
  }
}
