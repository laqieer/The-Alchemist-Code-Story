// Decompiled with JetBrains decompiler
// Type: AdjustMovieImageSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class AdjustMovieImageSize : UIBehaviour
{
  private RectTransform m_ParentTransform;
  private RectTransform m_Transform;
  private Coroutine m_AdjustCoroutine;

  protected virtual void Awake()
  {
    base.Awake();
    this.m_Transform = ((Component) this).transform as RectTransform;
    this.m_ParentTransform = ((Component) this).transform.parent as RectTransform;
  }

  protected virtual void Start()
  {
    base.Start();
    this.AsyncAdjustRecttransformSize();
  }

  protected virtual void OnRectTransformDimensionsChange() => this.AsyncAdjustRecttransformSize();

  private void AsyncAdjustRecttransformSize()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    if (this.m_AdjustCoroutine != null)
      ((MonoBehaviour) this).StopCoroutine(this.m_AdjustCoroutine);
    this.m_AdjustCoroutine = ((MonoBehaviour) this).StartCoroutine(this.StartAdjustSize());
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
    Rect rect = this.m_ParentTransform.rect;
    this.m_Transform.SetSizeWithCurrentAnchors((RectTransform.Axis) 1, ((Rect) ref rect).size.y);
    float num = 1f / canvasBoundsScale;
    Vector2 vector2 = Vector2.op_Implicit(((Transform) this.m_Transform).localScale);
    vector2.x = num;
    vector2.y = num;
    ((Transform) this.m_Transform).localScale = Vector2.op_Implicit(vector2);
  }
}
