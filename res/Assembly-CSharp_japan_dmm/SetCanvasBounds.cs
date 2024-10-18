// Decompiled with JetBrains decompiler
// Type: SetCanvasBounds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class SetCanvasBounds : MonoBehaviour
{
  [SerializeField]
  private bool IgnoreApplySafeAreaFlag;
  private static Vector2 add2DFlamePos;
  private static SetCanvasBounds.ScreenInfo m_CachedScreenInfo;
  public RectTransform panel;
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);

  private static SetCanvasBounds.ScreenInfo CachedScreenInfo
  {
    get
    {
      if (SetCanvasBounds.m_CachedScreenInfo == null)
      {
        SetCanvasBounds.m_CachedScreenInfo = new SetCanvasBounds.ScreenInfo();
        SetCanvasBounds.m_CachedScreenInfo.Initialize();
      }
      return SetCanvasBounds.m_CachedScreenInfo;
    }
  }

  public static bool HasSafeArea => SetCanvasBounds.CachedScreenInfo.HasSafeArea;

  public static bool IsWideScreen => SetCanvasBounds.CachedScreenInfo.IsWide;

  public static void ClearScreenInfo()
  {
    SetCanvasBounds.m_CachedScreenInfo = (SetCanvasBounds.ScreenInfo) null;
  }

  public static Vector2 GetAddFrame()
  {
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    int num = (Screen.width - Screen.height / 9 * 16) / 2;
    SetCanvasBounds.add2DFlamePos.x = (float) (num - (int) ((Rect) ref safeArea).x);
    if ((double) SetCanvasBounds.add2DFlamePos.x < 0.0)
      SetCanvasBounds.add2DFlamePos.x = 0.0f;
    return SetCanvasBounds.add2DFlamePos;
  }

  private static int PointToPixel(int pt, int retina) => pt * retina;

  private static Rect CalcSafeAreaPointToPixel(RectOffset edgeInsets, int retina)
  {
    Rect rect;
    // ISSUE: explicit constructor call
    ((Rect) ref rect).\u002Ector(0.0f, 0.0f, (float) Screen.width, (float) Screen.height);
    edgeInsets.left = SetCanvasBounds.PointToPixel(edgeInsets.left, retina);
    edgeInsets.right = SetCanvasBounds.PointToPixel(edgeInsets.right, retina);
    edgeInsets.top = SetCanvasBounds.PointToPixel(edgeInsets.top, retina);
    edgeInsets.bottom = SetCanvasBounds.PointToPixel(edgeInsets.bottom, retina);
    return edgeInsets.Remove(rect);
  }

  public static Rect GetSafeArea(bool bgScale = false)
  {
    Rect safeArea;
    // ISSUE: explicit constructor call
    ((Rect) ref safeArea).\u002Ector(0.0f, 0.0f, (float) Screen.width, (float) Screen.height);
    float num1 = 1.77866662f;
    if (bgScale)
    {
      // ISSUE: explicit constructor call
      ((Rect) ref safeArea).\u002Ector(0.0f, 0.0f, (float) Screen.width, (float) Screen.height);
      if ((double) num1 < (double) (Screen.width / Screen.height))
      {
        float num2 = (float) (((double) Screen.width - (double) Screen.height / 750.0 * 1334.0) / 2.0);
        ref Rect local1 = ref safeArea;
        ((Rect) ref local1).x = ((Rect) ref local1).x + num2 / 2f;
        ref Rect local2 = ref safeArea;
        ((Rect) ref local2).y = ((Rect) ref local2).y + num2 / 4f;
        ref Rect local3 = ref safeArea;
        ((Rect) ref local3).width = ((Rect) ref local3).width - num2 / 2f;
        ref Rect local4 = ref safeArea;
        ((Rect) ref local4).height = ((Rect) ref local4).height - num2 / 4f;
      }
    }
    return safeArea;
  }

  public static float CalcCanvasBoundsScale()
  {
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    return ((Rect) ref safeArea).width / (float) Screen.width;
  }

  private void Start()
  {
    if (this.IgnoreApplySafeAreaFlag)
      return;
    this.ApplySafeAreaScale(SetCanvasBounds.GetSafeArea());
  }

  private void ApplySafeAreaScale(Rect area)
  {
    if (Object.op_Inequality((Object) this.panel, (Object) null))
    {
      float num = ((Rect) ref area).width / (float) Screen.width;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(num, num, num);
      if (SetCanvasBounds.CachedScreenInfo.HasSafeArea)
      {
        if (SetCanvasBounds.CachedScreenInfo.IsWide)
        {
          this.panel.sizeDelta = new Vector2(0.0f, 750f);
          this.panel.anchorMin = new Vector2(0.0f, 1f);
          this.panel.anchorMax = new Vector2(1f, 1f);
          this.panel.pivot = new Vector2(0.5f, 1f);
        }
        else
        {
          this.panel.sizeDelta = new Vector2(0.0f, 750f);
          this.panel.anchorMin = new Vector2(0.0f, 0.5f);
          this.panel.anchorMax = new Vector2(1f, 0.5f);
          this.panel.pivot = new Vector2(0.5f, 0.5f);
        }
      }
      else if (SetCanvasBounds.CachedScreenInfo.IsWide)
      {
        this.panel.sizeDelta = new Vector2(0.0f, 750f);
        this.panel.anchorMin = new Vector2(0.0f, 0.5f);
        this.panel.anchorMax = new Vector2(1f, 0.5f);
        this.panel.pivot = new Vector2(0.5f, 0.5f);
        this.panel.offsetMin = new Vector2(0.0f, this.panel.offsetMin.y);
        this.panel.offsetMax = new Vector2(0.0f, this.panel.offsetMax.y);
      }
      else
      {
        this.panel.sizeDelta = new Vector2(0.0f, 750f);
        this.panel.anchorMin = new Vector2(0.0f, 0.5f);
        this.panel.anchorMax = new Vector2(1f, 0.5f);
        this.panel.pivot = new Vector2(0.5f, 0.5f);
      }
      ((Transform) this.panel).localScale = vector3;
    }
    this.lastSafeArea = area;
  }

  public static Vector2 GetScreenSize()
  {
    Rect viewportRect = SetCanvasBounds.CachedScreenInfo.ViewportRect;
    return ((Rect) ref viewportRect).size;
  }

  public static Rect GetCameraViewport()
  {
    return SetCanvasBounds.CachedScreenInfo.IsWide ? new Rect(0.0f, 0.0f, 1f, 1f) : SetCanvasBounds.CachedScreenInfo.NormalizedViewportRect;
  }

  private class ScreenInfo
  {
    private Rect m_SafeArea;
    private Rect m_NormalizedViewportRect;
    private Rect m_ViewportRect;
    private bool m_HasSafeArea;
    private bool m_IsWide;

    public Rect SafeArea
    {
      get => this.m_SafeArea;
      set
      {
        this.m_SafeArea = value;
        this.m_HasSafeArea = (double) ((Rect) ref this.m_SafeArea).width != (double) Screen.width || (double) ((Rect) ref this.m_SafeArea).height != (double) Screen.height;
      }
    }

    public Rect NormalizedViewportRect
    {
      get => this.m_NormalizedViewportRect;
      set
      {
        this.m_NormalizedViewportRect = value;
        this.m_ViewportRect = new Rect();
        ((Rect) ref this.m_ViewportRect).position = new Vector2((float) Screen.width * ((Rect) ref this.m_NormalizedViewportRect).x, (float) Screen.height * ((Rect) ref this.m_NormalizedViewportRect).y);
        ((Rect) ref this.m_ViewportRect).size = new Vector2((float) Screen.width * ((Rect) ref this.m_NormalizedViewportRect).size.x, (float) Screen.height * ((Rect) ref this.m_NormalizedViewportRect).size.y);
      }
    }

    public Rect ViewportRect => this.m_ViewportRect;

    public bool HasSafeArea => this.m_HasSafeArea;

    public bool IsWide
    {
      get => this.m_IsWide;
      set => this.m_IsWide = value;
    }

    public Vector2 ScreenSize => ((Rect) ref this.m_SafeArea).size;

    public void Initialize()
    {
      Rect safeArea = SetCanvasBounds.GetSafeArea();
      Vector2 size = ((Rect) ref safeArea).size;
      this.SafeArea = safeArea;
      float num1 = (float) Screen.width / (float) Screen.height / 1.77866662f;
      Vector2 zero1 = Vector2.zero;
      Vector2 zero2 = Vector2.zero;
      if (1.0 > (double) num1)
      {
        // ISSUE: explicit constructor call
        ((Vector2) ref zero1).\u002Ector(0.0f, (float) ((1.0 - (double) num1) / 2.0));
        // ISSUE: explicit constructor call
        ((Vector2) ref zero2).\u002Ector(1f, num1);
        this.m_IsWide = false;
      }
      else
      {
        float num2 = 1f / num1;
        // ISSUE: explicit constructor call
        ((Vector2) ref zero1).\u002Ector((float) ((1.0 - (double) num2) / 2.0), 0.0f);
        // ISSUE: explicit constructor call
        ((Vector2) ref zero2).\u002Ector(num2, 1f);
        this.m_IsWide = true;
      }
      this.NormalizedViewportRect = new Rect(zero1, zero2);
    }
  }
}
