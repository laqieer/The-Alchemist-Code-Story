// Decompiled with JetBrains decompiler
// Type: ScreenBlankSpaceFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
public class ScreenBlankSpaceFrame : UIBehaviour
{
  [SerializeField]
  private RectTransform m_ImageLeft;
  [SerializeField]
  private RectTransform m_ImageRight;
  [SerializeField]
  private RectTransform m_ImageTop;
  [SerializeField]
  private RectTransform m_ImageBottom;
  [SerializeField]
  private RectTransform m_CanvasBoundsPanel;
  private Coroutine m_AdjustCoroutine;
  private DrivenRectTransformTracker m_Tracker;

  protected virtual void Start()
  {
    base.Start();
    this.AsyncAdjustRecttransformSize();
  }

  protected virtual void OnRectTransformDimensionsChange() => this.AsyncAdjustRecttransformSize();

  protected virtual void OnDisable()
  {
    ((DrivenRectTransformTracker) ref this.m_Tracker).Clear();
    base.OnDisable();
  }

  private void AsyncAdjustRecttransformSize()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    if (Application.isPlaying)
    {
      if (this.m_AdjustCoroutine != null)
        ((MonoBehaviour) this).StopCoroutine(this.m_AdjustCoroutine);
      this.m_AdjustCoroutine = ((MonoBehaviour) this).StartCoroutine(this.StartAdjustSize());
    }
    else
    {
      SetCanvasBounds.ClearScreenInfo();
      this.SetScreenOutFrameSize();
    }
  }

  [DebuggerHidden]
  private IEnumerator StartAdjustSize()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new ScreenBlankSpaceFrame.\u003CStartAdjustSize\u003Ec__Iterator0()
    {
      \u0024this = this
    };
  }

  private void SetScreenOutFrameSize()
  {
    Vector2 screenSize = SetCanvasBounds.GetScreenSize();
    Vector2 vector2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2).\u002Ector((float) Screen.width, (float) Screen.height);
    DrivenTransformProperties transformProperties = (DrivenTransformProperties) 65286;
    ((DrivenRectTransformTracker) ref this.m_Tracker).Clear();
    if (Object.op_Inequality((Object) this.m_ImageLeft, (Object) null))
      ((DrivenRectTransformTracker) ref this.m_Tracker).Add((Object) this, this.m_ImageLeft, transformProperties);
    if (Object.op_Inequality((Object) this.m_ImageRight, (Object) null))
      ((DrivenRectTransformTracker) ref this.m_Tracker).Add((Object) this, this.m_ImageRight, transformProperties);
    if (Object.op_Inequality((Object) this.m_ImageTop, (Object) null))
      ((DrivenRectTransformTracker) ref this.m_Tracker).Add((Object) this, this.m_ImageTop, transformProperties);
    if (Object.op_Inequality((Object) this.m_ImageBottom, (Object) null))
      ((DrivenRectTransformTracker) ref this.m_Tracker).Add((Object) this, this.m_ImageBottom, transformProperties);
    if (Vector2.op_Equality(vector2, screenSize))
    {
      GameUtility.SetGameObjectActive((Component) this.m_ImageLeft, false);
      GameUtility.SetGameObjectActive((Component) this.m_ImageRight, false);
      GameUtility.SetGameObjectActive((Component) this.m_ImageTop, false);
      GameUtility.SetGameObjectActive((Component) this.m_ImageBottom, false);
    }
    else if (SetCanvasBounds.IsWideScreen)
    {
      GameUtility.SetGameObjectActive((Component) this.m_ImageLeft, false);
      GameUtility.SetGameObjectActive((Component) this.m_ImageRight, false);
      GameUtility.SetGameObjectActive((Component) this.m_ImageTop, false);
      GameUtility.SetGameObjectActive((Component) this.m_ImageBottom, false);
    }
    else
    {
      GameUtility.SetGameObjectActive((Component) this.m_ImageLeft, false);
      GameUtility.SetGameObjectActive((Component) this.m_ImageRight, false);
      GameUtility.SetGameObjectActive((Component) this.m_ImageTop, true);
      GameUtility.SetGameObjectActive((Component) this.m_ImageBottom, true);
      this.SetAnchorAndPivot(this.m_ImageTop, (RectTransform.Edge) 2);
      this.SetRect(this.m_ImageTop, (RectTransform.Edge) 2);
      this.SetAnchorAndPivot(this.m_ImageBottom, (RectTransform.Edge) 3);
      this.SetRect(this.m_ImageBottom, (RectTransform.Edge) 3);
    }
  }

  private void SetRect(RectTransform rt, RectTransform.Edge edge)
  {
    if (Object.op_Equality((Object) rt, (Object) null))
      return;
    Rect cameraViewport = SetCanvasBounds.GetCameraViewport();
    Vector2 zero1 = Vector2.zero;
    Vector2 zero2 = Vector2.zero;
    Rect rect1 = new Rect();
    switch ((int) edge)
    {
      case 0:
        ref Vector2 local1 = ref zero1;
        double x1 = (double) ((Rect) ref cameraViewport).x;
        Rect rect2 = this.m_CanvasBoundsPanel.rect;
        double x2 = (double) ((Rect) ref rect2).size.x;
        double num1 = x1 * x2;
        local1.x = (float) num1;
        zero1.y = 0.0f;
        ref Vector2 local2 = ref zero2;
        double x3 = (double) ((Rect) ref cameraViewport).x;
        Rect rect3 = this.m_CanvasBoundsPanel.rect;
        double x4 = (double) ((Rect) ref rect3).size.x;
        double num2 = x3 * x4;
        local2.x = (float) num2;
        ref Vector2 local3 = ref zero2;
        Rect rect4 = this.m_CanvasBoundsPanel.rect;
        double y1 = (double) ((Rect) ref rect4).size.y;
        local3.y = (float) y1;
        ((Rect) ref rect1).position = zero1;
        ((Rect) ref rect1).size = zero2;
        rect1 = new RectOffset(0, 0, 1, 1).Add(rect1);
        break;
      case 1:
        ref Vector2 local4 = ref zero1;
        double xMax = (double) ((Rect) ref cameraViewport).xMax;
        Rect rect5 = this.m_CanvasBoundsPanel.rect;
        double x5 = (double) ((Rect) ref rect5).size.x;
        double num3 = xMax * x5;
        local4.x = (float) num3;
        zero1.y = 0.0f;
        ref Vector2 local5 = ref zero2;
        double x6 = (double) ((Rect) ref cameraViewport).x;
        Rect rect6 = this.m_CanvasBoundsPanel.rect;
        double x7 = (double) ((Rect) ref rect6).size.x;
        double num4 = x6 * x7;
        local5.x = (float) num4;
        ref Vector2 local6 = ref zero2;
        Rect rect7 = this.m_CanvasBoundsPanel.rect;
        double y2 = (double) ((Rect) ref rect7).size.y;
        local6.y = (float) y2;
        ((Rect) ref rect1).position = zero1;
        ((Rect) ref rect1).size = zero2;
        rect1 = new RectOffset(0, 0, 1, 1).Add(rect1);
        break;
      case 2:
        ref Vector2 local7 = ref zero1;
        double x8 = (double) ((Rect) ref cameraViewport).x;
        Rect rect8 = this.m_CanvasBoundsPanel.rect;
        double x9 = (double) ((Rect) ref rect8).size.x;
        double num5 = x8 * x9;
        local7.x = (float) num5;
        ref Vector2 local8 = ref zero1;
        double y3 = (double) ((Rect) ref cameraViewport).y;
        Rect rect9 = this.m_CanvasBoundsPanel.rect;
        double y4 = (double) ((Rect) ref rect9).size.y;
        double num6 = y3 * y4 * -1.0;
        local8.y = (float) num6;
        ref Vector2 local9 = ref zero2;
        double width1 = (double) ((Rect) ref cameraViewport).width;
        Rect rect10 = this.m_CanvasBoundsPanel.rect;
        double x10 = (double) ((Rect) ref rect10).size.x;
        double num7 = width1 * x10;
        local9.x = (float) num7;
        ref Vector2 local10 = ref zero2;
        double y5 = (double) ((Rect) ref cameraViewport).y;
        Rect rect11 = this.m_CanvasBoundsPanel.rect;
        double y6 = (double) ((Rect) ref rect11).size.y;
        double num8 = y5 * y6;
        local10.y = (float) num8;
        ((Rect) ref rect1).position = zero1;
        ((Rect) ref rect1).size = zero2;
        rect1 = new RectOffset(1, 1, 0, 0).Add(rect1);
        break;
      case 3:
        ref Vector2 local11 = ref zero1;
        double x11 = (double) ((Rect) ref cameraViewport).x;
        Rect rect12 = this.m_CanvasBoundsPanel.rect;
        double x12 = (double) ((Rect) ref rect12).size.x;
        double num9 = x11 * x12;
        local11.x = (float) num9;
        ref Vector2 local12 = ref zero1;
        double yMax = (double) ((Rect) ref cameraViewport).yMax;
        Rect rect13 = this.m_CanvasBoundsPanel.rect;
        double y7 = (double) ((Rect) ref rect13).size.y;
        double num10 = yMax * y7 * -1.0;
        local12.y = (float) num10;
        ref Vector2 local13 = ref zero2;
        double width2 = (double) ((Rect) ref cameraViewport).width;
        Rect rect14 = this.m_CanvasBoundsPanel.rect;
        double x13 = (double) ((Rect) ref rect14).size.x;
        double num11 = width2 * x13;
        local13.x = (float) num11;
        ref Vector2 local14 = ref zero2;
        double y8 = (double) ((Rect) ref cameraViewport).y;
        Rect rect15 = this.m_CanvasBoundsPanel.rect;
        double y9 = (double) ((Rect) ref rect15).size.y;
        double num12 = y8 * y9;
        local14.y = (float) num12;
        ((Rect) ref rect1).position = zero1;
        ((Rect) ref rect1).size = zero2;
        rect1 = new RectOffset(1, 1, 0, 0).Add(rect1);
        break;
    }
    rt.anchoredPosition = ((Rect) ref rect1).position;
    rt.sizeDelta = ((Rect) ref rect1).size;
  }

  private void SetAnchorAndPivot(RectTransform rt, RectTransform.Edge edge)
  {
    if (Object.op_Equality((Object) rt, (Object) null))
      return;
    switch ((int) edge)
    {
      case 0:
        rt.pivot = new Vector2(1f, 1f);
        rt.anchorMin = new Vector2(0.0f, 1f);
        rt.anchorMax = new Vector2(0.0f, 1f);
        break;
      case 1:
        rt.pivot = new Vector2(0.0f, 1f);
        rt.anchorMin = new Vector2(0.0f, 1f);
        rt.anchorMax = new Vector2(0.0f, 1f);
        break;
      case 2:
        rt.pivot = new Vector2(0.0f, 0.0f);
        rt.anchorMin = new Vector2(0.0f, 1f);
        rt.anchorMax = new Vector2(0.0f, 1f);
        break;
      case 3:
        rt.pivot = new Vector2(0.0f, 1f);
        rt.anchorMin = new Vector2(0.0f, 1f);
        rt.anchorMax = new Vector2(0.0f, 1f);
        break;
    }
  }
}
