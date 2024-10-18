// Decompiled with JetBrains decompiler
// Type: SRPG.SGHighlightObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class SGHighlightObject : MonoBehaviour
  {
    private static float smallHighlightValue = 0.7f;
    private float animTime = 0.2f;
    private Vector2 DialogSize = Vector2.zero;
    public GameObject highlightedObject;
    public Canvas canvas;
    public IntVector2 highlightedGrid;
    [SerializeField]
    private GameObject shadowTop;
    [SerializeField]
    private GameObject shadowRight;
    [SerializeField]
    private GameObject shadowLeft;
    [SerializeField]
    private GameObject shadowBottom;
    [SerializeField]
    private GameObject blockAll;
    [SerializeField]
    private GameObject highlightArrow;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private Text noPortraitText;
    [SerializeField]
    private Text portraitText;
    [SerializeField]
    private EventDialogBubble dialogBubble;
    [SerializeField]
    private GameObject dialogGroup;
    private bool isHighlighted;
    private bool isPortraitVisible;
    private bool isSmallerHighlight;
    private bool isOverlayInteractable;
    private bool buttonDown;
    private bool floorValues;
    private float holdDownTime;
    private float elapsedAnimTime;
    private SGHighlightObject.OnActivateCallback onActivate;
    private LoadRequest mPortraitResource;
    private string mTextData;
    private UnitParam mUnit;
    private string TextID;
    private string UnitID;
    private Vector3 lastHighlightPosition;
    private EventDialogBubble.Anchors DialogBubbleAnchor;
    private UnityEngine.Camera uiCam;
    private static SGHighlightObject instance;
    private UnityAction clickAction;
    private GraphicRaycaster rayCaster;

    public static SGHighlightObject Instance()
    {
      return SGHighlightObject.instance;
    }

    public void Highlight(string unitID, string textID, SGHighlightObject.OnActivateCallback callback, EventDialogBubble.Anchors dialogBubbleAnchor, bool isPortraitShown = true, bool isInteractable = false, bool isSmaller = false)
    {
      RectTransform rectTransform = (RectTransform) null;
      if ((UnityEngine.Object) this.highlightedObject != (UnityEngine.Object) null)
        rectTransform = this.highlightedObject.GetComponent<RectTransform>();
      if ((UnityEngine.Object) rectTransform != (UnityEngine.Object) null || true)
      {
        this.isHighlighted = true;
        this.shadowTop.SetActive(false);
        this.shadowRight.SetActive(false);
        this.shadowLeft.SetActive(false);
        this.shadowBottom.SetActive(false);
        this.UnitID = unitID;
        this.TextID = textID;
        AnalyticsManager.TrackTutorialAnalyticsEvent(this.TextID);
        this.DialogBubbleAnchor = dialogBubbleAnchor == EventDialogBubble.Anchors.None ? EventDialogBubble.Anchors.None : dialogBubbleAnchor;
        this.onActivate = callback;
        this.uiCam = UnityEngine.Camera.main;
        this.isPortraitVisible = isPortraitShown;
        this.isSmallerHighlight = isSmaller;
      }
      else
        this.HideHighlight();
      if (isInteractable)
      {
        this.onActivate = callback;
        this.isOverlayInteractable = true;
      }
      this.nextButton.gameObject.SetActive(isInteractable);
      if (!string.IsNullOrEmpty(unitID))
        this.mUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(unitID);
      this.elapsedAnimTime = 0.0f;
      this.StartCoroutine(this.LoadAssets());
      this.floorValues = false;
      if (MonoSingleton<GameManager>.Instance.GetNextTutorialStep() == "ShowCollectGachaMissionReward")
        this.floorValues = true;
      this.rayCaster = this.gameObject.GetComponent<GraphicRaycaster>();
    }

    private void HideHighlight()
    {
      float width = this.canvas.pixelRect.width;
      float height = this.canvas.pixelRect.height;
      this.isHighlighted = false;
      this.shadowTop.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
      this.shadowTop.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
      this.shadowRight.GetComponent<RectTransform>().offsetMin = new Vector2(width, 0.0f);
      this.shadowRight.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
      this.shadowLeft.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, height);
      this.shadowLeft.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
      this.shadowBottom.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, height);
      this.shadowBottom.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
      this.shadowTop.SetActive(false);
      this.shadowRight.SetActive(false);
      this.shadowLeft.SetActive(false);
      this.shadowBottom.SetActive(false);
      this.blockAll.SetActive(true);
      this.highlightArrow.gameObject.SetActive(false);
    }

    private void Awake()
    {
      this.isHighlighted = false;
      this.uiCam = (UnityEngine.Camera) null;
      SGHighlightObject.instance = this;
      this.canvas = this.gameObject.GetComponent<Canvas>();
      this.clickAction = new UnityAction(this.OnClick);
      this.canvas.gameObject.AddComponent<Button>().onClick.AddListener(this.clickAction);
      if ((UnityEngine.Object) this.nextButton != (UnityEngine.Object) null)
        this.nextButton.onClick.AddListener(this.clickAction);
      HoldGesture holdGesture = this.canvas.gameObject.AddComponent<HoldGesture>();
      if (!((UnityEngine.Object) holdGesture != (UnityEngine.Object) null))
        return;
      UnityAction call1 = new UnityAction(this.OnDown);
      holdGesture.OnHoldStart = new UnityEvent();
      holdGesture.OnHoldStart.AddListener(call1);
      UnityAction call2 = new UnityAction(this.OnUp);
      holdGesture.OnHoldEnd = new UnityEvent();
      holdGesture.OnHoldEnd.AddListener(call2);
    }

    private void OnDown()
    {
      this.buttonDown = true;
      this.holdDownTime = Time.time;
    }

    private void OnUp()
    {
      this.buttonDown = false;
    }

    private void OnClick()
    {
      if (!((UnityEngine.Object) this.dialogBubble != (UnityEngine.Object) null))
        return;
      if (this.dialogBubble.Finished)
      {
        if (this.isOverlayInteractable && this.onActivate != null)
        {
          this.isOverlayInteractable = false;
          this.onActivate();
        }
        this.dialogBubble.Forward();
      }
      else
      {
        if (!this.dialogBubble.IsPrinting)
          return;
        this.dialogBubble.Skip();
      }
    }

    private void Start()
    {
      if (this.isHighlighted)
        return;
      this.Highlight((string) null, (string) null, (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.None, true, false, false);
    }

    private void Update()
    {
      if (!this.dialogBubble.Finished && this.buttonDown && (double) Time.time - (double) this.holdDownTime > 0.200000002980232)
        this.dialogBubble.Skip();
      if ((UnityEngine.Object) this.rayCaster != (UnityEngine.Object) null && !this.rayCaster.enabled)
        this.rayCaster.enabled = true;
      this.canvas.sortingOrder = 50;
      if (!this.isHighlighted)
        return;
      if (this.DialogBubbleAnchor != EventDialogBubble.Anchors.None)
        this.dialogBubble.Anchor = this.DialogBubbleAnchor;
      if ((UnityEngine.Object) this.highlightedObject != (UnityEngine.Object) null)
      {
        RectTransform component = this.highlightedObject.GetComponent<RectTransform>();
        Rect rect = component.rect;
        float width = rect.width;
        float height = rect.height;
        if ((double) this.canvas.scaleFactor != 0.0)
        {
          rect.x += component.position.x / this.canvas.scaleFactor;
          rect.y += component.position.y / this.canvas.scaleFactor;
          width *= this.canvas.scaleFactor;
          height *= this.canvas.scaleFactor;
        }
        this.HighlightArea(rect);
        Vector3 position = component.transform.position;
        Vector2 pivot = ((RectTransform) this.highlightArrow.transform).pivot;
        float num1 = 0.0f;
        float num2 = 0.5f;
        float num3 = num1 + (pivot.x - component.pivot.x);
        float num4 = num2 + (pivot.y - component.pivot.y);
        Vector3 localScale = this.highlightArrow.transform.localScale;
        float num5 = 80f * this.canvas.scaleFactor;
        float num6 = component.rect.height * (1f - component.pivot.y) * this.canvas.scaleFactor;
        if ((double) this.canvas.pixelRect.height - (double) (position.y + num6) < (double) num5)
        {
          num4 *= -1f;
          localScale.y = -1f;
          this.highlightArrow.transform.localScale = localScale;
        }
        else
        {
          localScale.y = 1f;
          this.highlightArrow.transform.localScale = localScale;
        }
        if (!this.isSmallerHighlight)
        {
          position.y += height * num4;
          position.x += width * num3;
        }
        else
        {
          position.x += width * num3 * SGHighlightObject.smallHighlightValue;
          position.y += height * num4 * SGHighlightObject.smallHighlightValue;
        }
        this.highlightArrow.transform.position = position;
      }
      else if (this.highlightedGrid.x != 0 && this.highlightedGrid.y != 0)
      {
        if (this.DialogBubbleAnchor != EventDialogBubble.Anchors.None)
          this.dialogBubble.Anchor = this.DialogBubbleAnchor;
        SceneBattle instance = SceneBattle.Instance;
        int x = this.highlightedGrid.x;
        int y = this.highlightedGrid.y;
        Vector3 position1 = instance.CalcGridCenter(x - 1, y);
        Vector3 position2 = instance.CalcGridCenter(x, y - 1);
        Vector3 position3 = instance.CalcGridCenter(x, y + 1);
        Vector3 position4 = instance.CalcGridCenter(x, y - 1);
        if (!((UnityEngine.Object) this.uiCam != (UnityEngine.Object) null))
          return;
        Vector3 screenPoint1 = this.uiCam.WorldToScreenPoint(position1);
        Vector3 screenPoint2 = this.uiCam.WorldToScreenPoint(position2);
        Vector3 screenPoint3 = this.uiCam.WorldToScreenPoint(position3);
        Vector3 screenPoint4 = this.uiCam.WorldToScreenPoint(position4);
        Rect rect1 = new Rect();
        rect1.position = new Vector2(screenPoint4.x, screenPoint4.y);
        rect1.width = screenPoint3.x - screenPoint1.x;
        rect1.height = (float) -((double) screenPoint2.y - (double) screenPoint1.y);
        Rect rect2 = rect1;
        if ((double) this.canvas.scaleFactor != 0.0)
        {
          RectTransform transform = this.canvas.transform as RectTransform;
          rect2.x = rect1.x / (float) Screen.width * transform.rect.width;
          rect2.y = rect1.y / (float) Screen.height * transform.rect.height;
          rect2.width /= this.canvas.scaleFactor;
          rect2.height /= this.canvas.scaleFactor;
        }
        this.HighlightArea(rect2);
        Vector3 position5 = (Vector3) rect1.position;
        position5.y += rect1.height;
        position5.x += rect1.width / 2f;
        this.highlightArrow.transform.position = position5;
      }
      else
      {
        this.HideHighlight();
        this.blockAll.SetActive(true);
      }
    }

    private void HighlightArea(Rect rect)
    {
      float width = this.canvas.pixelRect.width;
      float height = this.canvas.pixelRect.height;
      if (this.isSmallerHighlight)
      {
        float num = (float) ((1.0 - (double) SGHighlightObject.smallHighlightValue) / 2.0);
        rect.x += rect.width * num;
        rect.y += rect.height * num;
        rect.width *= SGHighlightObject.smallHighlightValue;
        rect.height *= SGHighlightObject.smallHighlightValue;
      }
      if ((double) this.canvas.scaleFactor != 0.0)
      {
        width /= this.canvas.scaleFactor;
        height /= this.canvas.scaleFactor;
      }
      float y1 = rect.yMax;
      float x1 = rect.xMax;
      float x2 = (float) -((double) width - (double) rect.xMin);
      float y2 = (float) -((double) height - (double) rect.yMin);
      if ((double) this.elapsedAnimTime < (double) this.animTime)
      {
        this.blockAll.SetActive(true);
        if (this.dialogBubble.Finished)
          this.elapsedAnimTime += Time.smoothDeltaTime;
        float a1 = height;
        float a2 = width;
        float a3 = -width;
        float a4 = -height;
        float yMax = rect.yMax;
        float xMax = rect.xMax;
        float b1 = (float) -((double) width - (double) rect.xMin);
        float b2 = (float) -((double) height - (double) rect.yMin);
        y1 = Mathf.Lerp(a1, yMax, this.elapsedAnimTime / this.animTime);
        x1 = Mathf.Lerp(a2, xMax, this.elapsedAnimTime / this.animTime);
        x2 = Mathf.Lerp(a3, b1, this.elapsedAnimTime / this.animTime);
        y2 = Mathf.Lerp(a4, b2, this.elapsedAnimTime / this.animTime);
        this.highlightArrow.gameObject.SetActive(false);
      }
      else
      {
        this.highlightArrow.gameObject.SetActive(true);
        if (!this.nextButton.gameObject.GetActive())
          this.blockAll.SetActive(false);
      }
      float y3 = (float) -((double) height - (double) rect.yMax);
      float yMin = rect.yMin;
      this.shadowTop.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, y1);
      this.shadowTop.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, 0.0f);
      this.shadowRight.GetComponent<RectTransform>().offsetMin = new Vector2(x1, 0.0f);
      this.shadowRight.GetComponent<RectTransform>().offsetMax = new Vector2(0.0f, y3);
      this.shadowLeft.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, yMin);
      this.shadowLeft.GetComponent<RectTransform>().offsetMax = new Vector2(x2, y3);
      this.shadowBottom.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
      this.shadowBottom.GetComponent<RectTransform>().offsetMax = new Vector2((float) -((double) width - (double) rect.xMax), y2);
    }

    public void Activated(int pinID)
    {
      if (pinID != 0 || this.onActivate == null)
        return;
      this.onActivate();
    }

    private void LoadTextData(string TextID)
    {
      if (!string.IsNullOrEmpty(TextID))
        this.mTextData = LocalizedText.Get(TextID).Split('\t')[0];
      else
        this.mTextData = (string) null;
    }

    [DebuggerHidden]
    private IEnumerator LoadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SGHighlightObject.\u003CLoadAssets\u003Ec__Iterator53() { \u003C\u003Ef__this = this };
    }

    public void GridSelected(int gridX, int gridY)
    {
      if (this.onActivate == null)
        return;
      this.onActivate();
    }

    public delegate void OnActivateCallback();
  }
}
