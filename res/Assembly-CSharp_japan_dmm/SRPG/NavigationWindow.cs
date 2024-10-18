// Decompiled with JetBrains decompiler
// Type: SRPG.NavigationWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class NavigationWindow : MonoBehaviour
  {
    private static NavigationWindow mCurrent;
    private static int mNumNavigations;
    private static GameObject mNavigationCanvas;
    private const int CanvasPriority = 5000;
    public Text Text;
    public float DestroyDelay = 1f;
    public string HideTrigger = string.Empty;
    public Vector2 Margin = new Vector2(20f, 20f);
    private Animator mAnimator;
    private RectTransform mRect;

    public static void DiscardCurrent()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) NavigationWindow.mCurrent, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) NavigationWindow.mCurrent).gameObject, NavigationWindow.mCurrent.DestroyDelay);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) NavigationWindow.mCurrent.mAnimator, (UnityEngine.Object) null) && !string.IsNullOrEmpty(NavigationWindow.mCurrent.HideTrigger))
        NavigationWindow.mCurrent.mAnimator.SetTrigger(NavigationWindow.mCurrent.HideTrigger);
      NavigationWindow.mCurrent = (NavigationWindow) null;
    }

    public static void Show(
      NavigationWindow template,
      string text,
      NavigationWindow.Alignment align)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) template, (UnityEngine.Object) null))
        return;
      if (NavigationWindow.mNumNavigations == 0)
      {
        NavigationWindow.mNavigationCanvas = new GameObject("NavigationCanvas", new System.Type[3]
        {
          typeof (Canvas),
          typeof (SRPG_CanvasScaler),
          typeof (SetCanvasBounds)
        });
        Canvas component1 = NavigationWindow.mNavigationCanvas.GetComponent<Canvas>();
        component1.renderMode = (RenderMode) 0;
        component1.sortingOrder = 5000;
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) NavigationWindow.mNavigationCanvas);
        SetCanvasBounds component2 = NavigationWindow.mNavigationCanvas.GetComponent<SetCanvasBounds>();
        component2.panel = (RectTransform) null;
        GameObject gameObject = new GameObject("CanvasBoundsPanel", new System.Type[1]
        {
          typeof (RectTransform)
        });
        gameObject.transform.SetParent(NavigationWindow.mNavigationCanvas.transform, false);
        component2.panel = gameObject.GetComponent<RectTransform>();
      }
      Transform panel = (Transform) NavigationWindow.mNavigationCanvas.GetComponent<SetCanvasBounds>().panel;
      NavigationWindow navigationWindow = UnityEngine.Object.Instantiate<NavigationWindow>(template);
      navigationWindow.SetAlignment(align);
      navigationWindow.SetText(text);
      ((Component) navigationWindow).transform.SetParent(panel, false);
      ++NavigationWindow.mNumNavigations;
    }

    private void Awake()
    {
      this.mRect = ((Component) this).GetComponent<RectTransform>();
      this.mAnimator = ((Component) this).GetComponent<Animator>();
    }

    private void Start()
    {
      NavigationWindow.DiscardCurrent();
      NavigationWindow.mCurrent = this;
    }

    private void OnDestroy()
    {
      --NavigationWindow.mNumNavigations;
      if (NavigationWindow.mNumNavigations != 0)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) NavigationWindow.mNavigationCanvas.gameObject);
      NavigationWindow.mNavigationCanvas = (GameObject) null;
    }

    private void SetText(string text) => this.Text.text = text;

    private void SetAlignment(NavigationWindow.Alignment alignment)
    {
      Vector2 zero1 = Vector2.zero;
      Vector2 zero2 = Vector2.zero;
      Vector2 zero3 = Vector2.zero;
      int num1 = (int) (alignment & (NavigationWindow.Alignment.Bottom | NavigationWindow.Alignment.BottomRight));
      int num2 = (int) alignment >> 2 & 3;
      switch (num1)
      {
        case 0:
          zero1.x = 0.0f;
          zero2.x = 0.0f;
          zero3.x = this.Margin.x;
          break;
        case 1:
          zero1.x = 0.5f;
          zero2.x = 0.5f;
          break;
        case 2:
          zero1.x = 1f;
          zero2.x = 1f;
          zero3.x = -this.Margin.x;
          break;
      }
      switch (num2)
      {
        case 0:
          zero1.y = 0.0f;
          zero2.y = 0.0f;
          zero3.y = this.Margin.y;
          break;
        case 1:
          zero1.y = 0.5f;
          zero2.y = 0.5f;
          break;
        case 2:
          zero1.y = 1f;
          zero2.y = 1f;
          zero3.y = -this.Margin.y;
          break;
      }
      RectTransform mRect = this.mRect;
      Vector2 vector2_1 = zero1;
      this.mRect.anchorMax = vector2_1;
      Vector2 vector2_2 = vector2_1;
      mRect.anchorMin = vector2_2;
      this.mRect.pivot = zero2;
      this.mRect.anchoredPosition = zero3;
    }

    public enum Alignment
    {
      BottomLeft = 0,
      Bottom = 1,
      BottomRight = 2,
      MiddleLeft = 4,
      Middle = 5,
      MiddleRight = 6,
      TopLeft = 8,
      Top = 9,
      TopRight = 10, // 0x0000000A
    }
  }
}
