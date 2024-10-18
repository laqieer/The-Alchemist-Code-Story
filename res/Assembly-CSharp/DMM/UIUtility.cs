// Decompiled with JetBrains decompiler
// Type: UIUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class UIUtility
{
  private static Canvas mParticleCanvas;
  private static RectTransform mUIPool;
  private static Canvas mCanvas2D;
  public const float DEFAULT_ANIM_COMPLETE_TIME = 0.3f;

  public static void InitParticleCanvas()
  {
    if (!UnityEngine.Object.op_Equality((UnityEngine.Object) UIUtility.mParticleCanvas, (UnityEngine.Object) null))
      return;
    GameObject gameObject = new GameObject("ParticleCanvas", new System.Type[3]
    {
      typeof (Canvas),
      typeof (GraphicRaycaster),
      typeof (SRPG_CanvasScaler)
    });
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject);
    UIUtility.mParticleCanvas = gameObject.GetComponent<Canvas>();
    UIUtility.mParticleCanvas.renderMode = (RenderMode) 0;
    UIUtility.mParticleCanvas.sortingOrder = (int) short.MaxValue;
  }

  public static Canvas ParticleCanvas
  {
    get
    {
      UIUtility.InitParticleCanvas();
      return UIUtility.mParticleCanvas;
    }
  }

  public static Canvas PushCanvas(bool systemModal = false, int systemModalPriority = -1)
  {
    Canvas canvas = UnityEngine.Object.Instantiate<Canvas>(GameSettings.Instance.Canvas2D);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) ((Component) canvas).gameObject);
    CanvasStack canvasStack = ((Component) canvas).GetComponent<CanvasStack>();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) canvasStack, (UnityEngine.Object) null))
      canvasStack = ((Component) canvas).gameObject.AddComponent<CanvasStack>();
    if (systemModal)
    {
      canvasStack.SystemModal = true;
      canvasStack.Priority = systemModalPriority;
    }
    ((Component) canvas).gameObject.SetActive(false);
    ((Component) canvas).gameObject.SetActive(true);
    return canvas;
  }

  public static void PopCanvas() => UIUtility.PopCanvas(false);

  public static void PopCanvas(bool keepInstance)
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) CanvasStack.Top, (UnityEngine.Object) null))
      return;
    if (!keepInstance)
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) ((Component) CanvasStack.Top).gameObject);
    else
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) ((Component) CanvasStack.Top).GetComponent<CanvasStack>());
  }

  public static void PopCanvasAll()
  {
    while (UnityEngine.Object.op_Inequality((UnityEngine.Object) CanvasStack.Top, (UnityEngine.Object) null))
      UIUtility.PopCanvas();
  }

  public static void ResetInput()
  {
    EventSystem current = EventSystem.current;
    ((Behaviour) current).enabled = false;
    ((Behaviour) current).enabled = true;
  }

  public static GameObject ConfirmBox(
    string text,
    string confirmID,
    UIUtility.DialogResultEvent okEventListener,
    UIUtility.DialogResultEvent cancelEventListener,
    GameObject parent = null,
    bool systemModal = false,
    int systemModalPriority = -1)
  {
    if (string.IsNullOrEmpty(confirmID))
      return UIUtility.ConfirmBox(text, okEventListener, cancelEventListener);
    GameSettings instance = GameSettings.Instance;
    if (PlayerPrefsUtility.GetInt(confirmID) != 0)
    {
      okEventListener((GameObject) null);
      return (GameObject) null;
    }
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parent, (UnityEngine.Object) null))
      ((Component) canvas).transform.SetParent(parent.transform);
    Win_Btn_DecideCancel_FL_Check_C decideCancelFlCheckC = UIUtility.Instantiate<Win_Btn_DecideCancel_FL_Check_C>(instance.Dialogs.YesNoDialogWithCheckBox);
    ((Component) decideCancelFlCheckC).transform.SetParent(((Component) canvas).transform, false);
    decideCancelFlCheckC.OnClickYes = okEventListener;
    decideCancelFlCheckC.OnClickNo = cancelEventListener;
    decideCancelFlCheckC.Text_Message.text = text;
    decideCancelFlCheckC.ConfirmID = confirmID;
    decideCancelFlCheckC.Toggle.isOn = false;
    return ((Component) decideCancelFlCheckC).gameObject;
  }

  public static GameObject ConfirmBox(
    string text,
    UIUtility.DialogResultEvent okEventListener,
    UIUtility.DialogResultEvent cancelEventListener,
    GameObject parent = null,
    bool systemModal = false,
    int systemModalPriority = -1,
    string yesText = null,
    string noText = null)
  {
    GameSettings instance = GameSettings.Instance;
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parent, (UnityEngine.Object) null))
      ((Component) canvas).transform.SetParent(parent.transform);
    Win_Btn_DecideCancel_FL_C btnDecideCancelFlC = UIUtility.Instantiate<Win_Btn_DecideCancel_FL_C>(instance.Dialogs.YesNoDialog);
    ((Component) btnDecideCancelFlC).transform.SetParent(((Component) canvas).transform, false);
    btnDecideCancelFlC.OnClickYes = okEventListener;
    btnDecideCancelFlC.OnClickNo = cancelEventListener;
    btnDecideCancelFlC.Text_Message.text = text;
    if (yesText != null)
      btnDecideCancelFlC.Txt_Yes.text = yesText;
    if (noText != null)
      btnDecideCancelFlC.Txt_No.text = noText;
    return ((Component) btnDecideCancelFlC).gameObject;
  }

  public static GameObject ConfirmBoxTitle(
    string title,
    string text,
    UIUtility.DialogResultEvent okEventListener,
    UIUtility.DialogResultEvent cancelEventListener,
    GameObject parent = null,
    bool systemModal = false,
    int systemModalPriority = -1,
    string yesText = null,
    string noText = null)
  {
    GameSettings instance = GameSettings.Instance;
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parent, (UnityEngine.Object) null))
      ((Component) canvas).transform.SetParent(parent.transform);
    Win_Btn_YN_Title_Flx winBtnYnTitleFlx = UIUtility.Instantiate<Win_Btn_YN_Title_Flx>(instance.Dialogs.YesNoDialogWithTitle);
    ((Component) winBtnYnTitleFlx).transform.SetParent(((Component) canvas).transform, false);
    winBtnYnTitleFlx.OnClickYes = okEventListener;
    winBtnYnTitleFlx.OnClickNo = cancelEventListener;
    winBtnYnTitleFlx.Text_Title.text = title;
    winBtnYnTitleFlx.Text_Message.text = text;
    if (yesText != null)
      winBtnYnTitleFlx.Txt_Yes.text = yesText;
    if (noText != null)
      winBtnYnTitleFlx.Txt_No.text = noText;
    return ((Component) winBtnYnTitleFlx).gameObject;
  }

  public static GameObject SystemMessage(
    string title,
    string msg,
    UIUtility.DialogResultEvent okEventListener,
    GameObject parent = null,
    bool systemModal = false,
    int systemModalPriority = -1)
  {
    GameSettings instance = GameSettings.Instance;
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parent, (UnityEngine.Object) null))
      ((Component) canvas).transform.SetParent(parent.transform);
    Win_Btn_Decide_Title_Flx btnDecideTitleFlx = UIUtility.Instantiate<Win_Btn_Decide_Title_Flx>(instance.Dialogs.YesDialogWithTitle);
    ((Component) btnDecideTitleFlx).transform.SetParent(((Component) canvas).transform, false);
    btnDecideTitleFlx.Text_Title.text = title;
    btnDecideTitleFlx.Text_Message.text = msg;
    btnDecideTitleFlx.OnClickYes = okEventListener;
    return ((Component) btnDecideTitleFlx).gameObject;
  }

  public static GameObject SystemMessage(
    string msg,
    UIUtility.DialogResultEvent okEventListener,
    GameObject parent = null,
    bool systemModal = false,
    int systemModalPriority = -1)
  {
    GameSettings instance = GameSettings.Instance;
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parent, (UnityEngine.Object) null))
      ((Component) canvas).transform.SetParent(parent.transform);
    Win_Btn_Decide_Flx winBtnDecideFlx = UIUtility.Instantiate<Win_Btn_Decide_Flx>(instance.Dialogs.YesDialog);
    ((Component) winBtnDecideFlx).transform.SetParent(((Component) canvas).transform, false);
    winBtnDecideFlx.Text_Message.text = msg;
    winBtnDecideFlx.OnClickYes = okEventListener;
    return ((Component) winBtnDecideFlx).gameObject;
  }

  public static GameObject NegativeSystemMessage(
    string title,
    string msg,
    UIUtility.DialogResultEvent okEventListener,
    GameObject parent = null,
    bool systemModal = false,
    int systemModalPriority = -1)
  {
    GameObject gameObject = UIUtility.SystemMessage(title, msg, okEventListener, parent, systemModal, systemModalPriority);
    SystemSound componentInChildren = !UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) ? gameObject.GetComponentInChildren<SystemSound>() : (SystemSound) null;
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
      componentInChildren.Cue = SystemSound.ECue.Tap;
    return gameObject;
  }

  public static void SetImage(GameObject obj, Texture tex)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
      return;
    RawImage component = obj.GetComponent<RawImage>();
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return;
    component.texture = tex;
  }

  public static void SetImage(Component obj, Texture tex)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
      return;
    UIUtility.SetImage(obj.gameObject, tex);
  }

  public static void SetSprite(GameObject obj, Sprite tex)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
      return;
    Image component = obj.GetComponent<Image>();
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return;
    component.sprite = tex;
  }

  public static void SetSprite(Component obj, Sprite tex)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
      return;
    UIUtility.SetSprite(obj.gameObject, tex);
  }

  public static T Instantiate<T>(T prefab) where T : Component
  {
    return (T) UIUtility.Instantiate(prefab.gameObject).GetComponent(typeof (T));
  }

  public static GameObject Instantiate(GameObject prefab)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) CanvasStack.Top, (UnityEngine.Object) null))
      UIUtility.PushCanvas();
    RectTransform transform1 = (RectTransform) ((Component) CanvasStack.Top).transform;
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
    gameObject.transform.SetParent((Transform) transform1, false);
    RectTransform transform2 = (RectTransform) prefab.transform;
    RectTransform transform3 = (RectTransform) gameObject.transform;
    ((Transform) transform3).localRotation = ((Transform) transform2).localRotation;
    ((Transform) transform3).localScale = ((Transform) transform2).localScale;
    ((Transform) transform3).position = ((Transform) transform2).position;
    transform3.sizeDelta = transform2.sizeDelta;
    transform3.anchoredPosition = transform2.anchoredPosition;
    transform3.pivot = transform2.pivot;
    return gameObject;
  }

  public static GameObject Instantiate(ResourceRequest req)
  {
    return UIUtility.Instantiate((GameObject) req.asset);
  }

  public static T Instantiate<T>(Component prefab) where T : Component
  {
    return (T) UIUtility.Instantiate(prefab.gameObject).GetComponent(typeof (T));
  }

  public static T Instantiate<T>(GameObject prefab) where T : Component
  {
    return (T) UIUtility.Instantiate(prefab).GetComponent(typeof (T));
  }

  public static T Instantiate<T>(ResourceRequest req) where T : Component
  {
    return (T) (!(req.asset is GameObject) ? UIUtility.Instantiate<Component>((Component) req.asset).gameObject : UIUtility.Instantiate((GameObject) req.asset)).GetComponent(typeof (T));
  }

  public static void ToggleWindowState(GameObject go, bool open, bool keepActive)
  {
    bool activeInHierarchy = go.activeInHierarchy;
    if (open && !activeInHierarchy)
      go.SetActive(true);
    Animator component = go.GetComponent<Animator>();
    if (!activeInHierarchy)
    {
      ((Behaviour) component).enabled = false;
      ((Behaviour) component).enabled = true;
    }
    if (GameUtility.ValidateAnimator(component))
      component.SetBool("close", !open);
    if (keepActive)
      return;
    UIDeactivator uiDeactivator = go.GetComponent<UIDeactivator>();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) uiDeactivator, (UnityEngine.Object) null))
      uiDeactivator = go.AddComponent<UIDeactivator>();
    if (!open)
      ((Behaviour) uiDeactivator).enabled = true;
    else
      ((Behaviour) uiDeactivator).enabled = false;
  }

  public static void ToggleWindowState(GameObject go, bool open)
  {
    UIUtility.ToggleWindowState(go, open, false);
  }

  public static void ToggleWindowState(Component go, bool open)
  {
    UIUtility.ToggleWindowState(go.gameObject, open, false);
  }

  public static void ToggleWindowState(Component go, bool open, bool keepActive)
  {
    UIUtility.ToggleWindowState(go.gameObject, open, keepActive);
  }

  public static void SetText(GameObject go, string text)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    foreach (Text componentsInChild in go.GetComponentsInChildren<Text>(true))
      componentsInChild.text = text;
  }

  public static void SetText(Component go, string text) => UIUtility.SetText(go.gameObject, text);

  public static void SetButtonText(Button go, string text)
  {
    UIUtility.SetText((Component) go, text);
  }

  public static void SpawnParticle(
    GameObject particleObject,
    RectTransform targetRect,
    Vector2 anchor)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) particleObject, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) targetRect, (UnityEngine.Object) null))
      return;
    Vector3[] vector3Array = new Vector3[4];
    targetRect.GetWorldCorners(vector3Array);
    Vector3 vector3_1 = Vector3.op_Subtraction(vector3Array[3], vector3Array[0]);
    Vector3 vector3_2 = Vector3.op_Subtraction(vector3Array[1], vector3Array[0]);
    Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint((Camera) null, Vector3.op_Addition(Vector3.op_Addition(vector3Array[0], Vector3.op_Multiply(vector3_1, anchor.x)), Vector3.op_Multiply(vector3_2, anchor.y)));
    UIUtility.SpawnParticle(particleObject, screenPoint);
  }

  public static void SpawnParticle(GameObject particleObject, Vector2 screenPosition)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) particleObject, (UnityEngine.Object) null))
      return;
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(particleObject);
    gameObject.AddComponent<OneShotParticle>();
    RectTransform transform = gameObject.transform as RectTransform;
    ((Transform) transform).SetParent(((Component) UIUtility.ParticleCanvas).transform, false);
    Vector2 vector2;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(((Component) UIUtility.ParticleCanvas).transform as RectTransform, screenPosition, (Camera) null, ref vector2);
    transform.anchoredPosition = vector2;
  }

  public static RectTransform Pool
  {
    get
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) UIUtility.mUIPool, (UnityEngine.Object) null))
      {
        GameObject gameObject = new GameObject("UIPOOL", new System.Type[1]
        {
          typeof (RectTransform)
        });
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject);
        UIUtility.mUIPool = gameObject.transform as RectTransform;
        UIUtility.mUIPool.sizeDelta = new Vector2((float) Screen.width, (float) Screen.height);
      }
      return UIUtility.mUIPool;
    }
  }

  public static void AddEventListener(
    GameObject go,
    UnityEvent e,
    UIUtility.EventListener listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey1()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__0)));
  }

  public static void AddEventListener<T0>(
    GameObject go,
    UnityEvent<T0> e,
    UIUtility.EventListener1Arg<T0> listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction<T0>((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey2<T0>()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__0)));
  }

  public static void AddEventListener<T0, T1>(
    GameObject go,
    UnityEvent<T0, T1> e,
    UIUtility.EventListener2Arg<T0, T1> listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction<T0, T1>((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey3<T0, T1>()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__0)));
  }

  public static void AddEventListener<T0, T1, T2>(
    GameObject go,
    UnityEvent<T0, T1, T2> e,
    UIUtility.EventListener3Arg<T0, T1, T2> listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction<T0, T1, T2>((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey4<T0, T1, T2>()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__0)));
  }

  public static void AddEventListener<T0, T1, T2, T3>(
    GameObject go,
    UnityEvent<T0, T1, T2, T3> e,
    UIUtility.EventListener4Arg<T0, T1, T2, T3> listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction<T0, T1, T2, T3>((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey5<T0, T1, T2, T3>()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__0)));
  }

  [DebuggerHidden]
  public static IEnumerator SwitchVisibleAsync(
    List<CanvasGroup> listOn,
    List<CanvasGroup> listOff,
    float complete_time = 0.3f)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIUtility.\u003CSwitchVisibleAsync\u003Ec__Iterator0()
    {
      listOn = listOn,
      listOff = listOff,
      complete_time = complete_time
    };
  }

  public enum DialogResults
  {
    None,
    Yes,
    No,
  }

  public struct DialogState
  {
    public UIUtility.DialogResults Result;
    public bool DoNotAskAgain;
  }

  public delegate void DialogResultEvent(GameObject dialog);

  public delegate void EventListener(GameObject go);

  public delegate void EventListener1Arg<T0>(GameObject go, T0 arg0);

  public delegate void EventListener2Arg<T0, T1>(GameObject go, T0 arg0, T1 arg1);

  public delegate void EventListener3Arg<T0, T1, T2>(GameObject go, T0 arg0, T1 arg1, T2 arg2);

  public delegate void EventListener4Arg<T0, T1, T2, T3>(
    GameObject go,
    T0 arg0,
    T1 arg1,
    T2 arg2,
    T3 arg3);
}
