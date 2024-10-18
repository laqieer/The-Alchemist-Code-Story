// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_TouchInputModule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  [AddComponentMenu("Event/Touch Input Module (SRPG)")]
  public class SRPG_TouchInputModule : PointerInputModule
  {
    private GameObject[] mTouchEffectPool = new GameObject[8];
    private float mDoubleTap1stReleasedTime = -1f;
    private readonly int BUTTON_INDEX_MAX = 3;
    private int pressing_button_index = -1;
    [SerializeField]
    private bool m_AllowActivationOnStandalone = true;
    private int mPrimaryFingerID = -1;
    private static int mLockCount;
    public GameObject TouchEffect;
    private int mNumActiveTouchEffects;
    private bool mTouchEffectPoolInitialized;
    public SRPG_TouchInputModule.OnDoubleTapDelegate OnDoubleTap;
    public static bool IsMultiTouching;
    private Vector2 m_LastMousePosition;
    private Vector2 m_MousePosition;

    public static void LockInput()
    {
      ++SRPG_TouchInputModule.mLockCount;
      EventSystem.current.currentInputModule.enabled = SRPG_TouchInputModule.mLockCount == 0;
    }

    public static void UnlockInput(bool forceReset = false)
    {
      if (forceReset)
        SRPG_TouchInputModule.mLockCount = 0;
      else
        --SRPG_TouchInputModule.mLockCount;
      EventSystem.current.currentInputModule.enabled = SRPG_TouchInputModule.mLockCount == 0;
    }

    private bool IsHandling { get; set; }

    private void InitTouchEffects()
    {
      if (this.mTouchEffectPoolInitialized)
        return;
      if ((UnityEngine.Object) this.TouchEffect != (UnityEngine.Object) null)
      {
        for (int index = 0; index < this.mTouchEffectPool.Length; ++index)
        {
          this.mTouchEffectPool[index] = UnityEngine.Object.Instantiate<GameObject>(this.TouchEffect);
          this.mTouchEffectPool[index].SetActive(false);
          this.mTouchEffectPool[index].transform.SetParent(UIUtility.ParticleCanvas.transform, false);
        }
      }
      this.mTouchEffectPoolInitialized = true;
    }

    protected override void Start()
    {
      base.Start();
      UIUtility.InitParticleCanvas();
      this.InitTouchEffects();
      this.OnDoubleTap += (SRPG_TouchInputModule.OnDoubleTapDelegate) (position => {});
    }

    public bool allowActivationOnStandalone
    {
      get
      {
        return this.m_AllowActivationOnStandalone;
      }
      set
      {
        this.m_AllowActivationOnStandalone = value;
      }
    }

    public override void UpdateModule()
    {
      this.m_LastMousePosition = this.m_MousePosition;
      this.m_MousePosition = (Vector2) Input.mousePosition;
    }

    public override bool IsModuleSupported()
    {
      if (!this.m_AllowActivationOnStandalone)
        return Application.isMobilePlatform;
      return true;
    }

    public override bool ShouldActivateModule()
    {
      if (!base.ShouldActivateModule())
        return false;
      if (this.UseFakeInput())
        return (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) | (double) (this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0.0;
      for (int index = 0; index < Input.touchCount; ++index)
      {
        Touch touch = Input.GetTouch(index);
        if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
          return true;
      }
      return false;
    }

    private bool UseFakeInput()
    {
      return !Application.isMobilePlatform;
    }

    public override void Process()
    {
      this.IsHandling = false;
      if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer))
        this.SendUpdateEventToSelectedObject();
      if (!this.enabled)
        return;
      if (this.UseFakeInput())
        this.FakeTouches();
      else
        this.ProcessTouchEvents();
      if (this.IsHandling || !Input.GetKeyDown(KeyCode.Escape))
        return;
      BackHandler.Invoke();
      Input.ResetInputAxes();
    }

    private bool SendUpdateEventToSelectedObject()
    {
      if ((UnityEngine.Object) this.eventSystem.currentSelectedGameObject == (UnityEngine.Object) null)
        return false;
      BaseEventData baseEventData = this.GetBaseEventData();
      ExecuteEvents.Execute<IUpdateSelectedHandler>(this.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
      return baseEventData.used;
    }

    private PointerEventData GetMousePointerEvent(int index)
    {
      int num = new int[3]{ -1, -2, -3 }[index];
      bool mouseButtonDown = Input.GetMouseButtonDown(num);
      PointerEventData data;
      bool pointerData = this.GetPointerData(num, out data, true);
      data.Reset();
      if (pointerData)
        data.position = (Vector2) Input.mousePosition;
      Vector2 mousePosition = (Vector2) Input.mousePosition;
      data.delta = mousePosition - data.position;
      data.position = mousePosition;
      data.scrollDelta = Input.mouseScrollDelta;
      this.eventSystem.RaycastAll(data, this.m_RaycastResultCache);
      RaycastResult firstRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
      data.pointerCurrentRaycast = firstRaycast;
      if (mouseButtonDown)
        data.delta = Vector2.zero;
      return data;
    }

    private void FakeTouches()
    {
      bool flag = false;
      bool pressed = false;
      bool released = false;
      if (this.pressing_button_index <= -1)
      {
        for (int button = 0; button < this.BUTTON_INDEX_MAX; ++button)
        {
          if (Input.GetMouseButtonDown(button))
          {
            this.pressing_button_index = button;
            pressed = true;
            break;
          }
        }
      }
      for (int button = 0; button < this.BUTTON_INDEX_MAX; ++button)
      {
        if (Input.GetMouseButtonUp(button) && this.pressing_button_index == button)
        {
          this.pressing_button_index = -1;
          released = true;
          break;
        }
      }
      this.IsHandling = pressed || released;
      PointerEventData buttonData1 = this.GetMousePointerEventData().GetButtonState(PointerEventData.InputButton.Left).eventData.buttonData;
      this.ProcessTouchPress(buttonData1, pressed, released);
      if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
      {
        this.IsHandling = true;
        this.ProcessMove(buttonData1);
        this.ProcessDrag(buttonData1);
        if (buttonData1.pointerPressRaycast.isValid)
        {
          PointerEventData buttonData2 = this.GetMousePointerEventData().GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData;
          if (Input.GetMouseButtonDown(1))
            buttonData2.pointerPressRaycast = buttonData2.pointerCurrentRaycast;
          if (Input.GetMouseButton(1))
            flag = (UnityEngine.Object) buttonData1.pointerPressRaycast.gameObject == (UnityEngine.Object) buttonData2.pointerPressRaycast.gameObject;
        }
      }
      SRPG_TouchInputModule.IsMultiTouching = flag;
    }

    private void ProcessTouchEvents()
    {
      List<GameObject> gameObjectList = new List<GameObject>();
      if (this.mPrimaryFingerID >= 0 && Input.touchCount <= 0)
        this.mPrimaryFingerID = -1;
      for (int index = 0; index < Input.touchCount; ++index)
      {
        Touch touch = Input.GetTouch(index);
        bool pressed;
        bool released;
        PointerEventData pointerEventData = this.GetTouchPointerEventData(touch, out pressed, out released);
        if (this.mPrimaryFingerID == -1 && pressed)
          this.mPrimaryFingerID = touch.fingerId;
        if (this.mPrimaryFingerID == touch.fingerId)
        {
          this.ProcessTouchPress(pointerEventData, pressed, released);
          if (!released)
          {
            gameObjectList.Add(pointerEventData.pointerPressRaycast.gameObject);
            this.ProcessMove(pointerEventData);
            this.ProcessDrag(pointerEventData);
          }
          else
            this.mPrimaryFingerID = -1;
        }
        else if (pressed)
          pointerEventData.pointerPressRaycast = pointerEventData.pointerCurrentRaycast;
        else if (!released)
          gameObjectList.Add(pointerEventData.pointerPressRaycast.gameObject);
        if (released)
          this.RemovePointerData(pointerEventData);
      }
      SRPG_TouchInputModule.IsMultiTouching = false;
      if (this.mPrimaryFingerID == -1 || gameObjectList.Count < 2)
        return;
      for (int index1 = 0; index1 < gameObjectList.Count; ++index1)
      {
        if ((UnityEngine.Object) gameObjectList[index1] == (UnityEngine.Object) null)
          return;
        for (int index2 = index1 + 1; index2 < gameObjectList.Count; ++index2)
        {
          if ((UnityEngine.Object) gameObjectList[index1] != (UnityEngine.Object) gameObjectList[index2])
            return;
        }
      }
      SRPG_TouchInputModule.IsMultiTouching = true;
    }

    private void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
    {
      GameObject gameObject1 = pointerEvent.pointerCurrentRaycast.gameObject;
      if (pressed)
      {
        pointerEvent.eligibleForClick = true;
        pointerEvent.delta = Vector2.zero;
        pointerEvent.pressPosition = pointerEvent.position;
        pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
        if ((UnityEngine.Object) pointerEvent.pointerEnter != (UnityEngine.Object) gameObject1)
        {
          this.HandlePointerExitAndEnter(pointerEvent, gameObject1);
          pointerEvent.pointerEnter = gameObject1;
        }
        GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject1, (BaseEventData) pointerEvent, ExecuteEvents.pointerDownHandler);
        if ((UnityEngine.Object) gameObject2 == (UnityEngine.Object) null)
          gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject1);
        if ((UnityEngine.Object) gameObject2 != (UnityEngine.Object) pointerEvent.pointerPress)
        {
          pointerEvent.pointerPress = gameObject2;
          pointerEvent.rawPointerPress = gameObject1;
          pointerEvent.clickCount = 0;
        }
        pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject1);
        if ((UnityEngine.Object) pointerEvent.pointerDrag != (UnityEngine.Object) null)
          ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, (BaseEventData) pointerEvent, ExecuteEvents.beginDragHandler);
        this.eventSystem.SetSelectedGameObject(ExecuteEvents.GetEventHandler<ISelectHandler>(gameObject1), (BaseEventData) pointerEvent);
      }
      if (!released)
        return;
      float unscaledTime1 = Time.unscaledTime;
      if ((double) this.mDoubleTap1stReleasedTime < 0.0)
        this.mDoubleTap1stReleasedTime = unscaledTime1;
      else if ((double) unscaledTime1 - (double) this.mDoubleTap1stReleasedTime >= 0.300000011920929)
      {
        this.mDoubleTap1stReleasedTime = unscaledTime1;
      }
      else
      {
        this.OnDoubleTap(pointerEvent.position);
        this.mDoubleTap1stReleasedTime = -1f;
      }
      ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, (BaseEventData) pointerEvent, ExecuteEvents.pointerUpHandler);
      GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject1);
      if ((UnityEngine.Object) pointerEvent.pointerPress == (UnityEngine.Object) eventHandler && pointerEvent.eligibleForClick)
      {
        float unscaledTime2 = Time.unscaledTime;
        if ((double) unscaledTime2 - (double) pointerEvent.clickTime < 0.300000011920929)
          ++pointerEvent.clickCount;
        else
          pointerEvent.clickCount = 1;
        pointerEvent.clickTime = unscaledTime2;
        ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, (BaseEventData) pointerEvent, ExecuteEvents.pointerClickHandler);
        this.SpawnTouchEffect(pointerEvent.position);
      }
      else if ((UnityEngine.Object) pointerEvent.pointerDrag != (UnityEngine.Object) null)
        ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject1, (BaseEventData) pointerEvent, ExecuteEvents.dropHandler);
      pointerEvent.eligibleForClick = false;
      pointerEvent.pointerPress = (GameObject) null;
      pointerEvent.rawPointerPress = (GameObject) null;
      if ((UnityEngine.Object) pointerEvent.pointerDrag != (UnityEngine.Object) null)
        ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, (BaseEventData) pointerEvent, ExecuteEvents.endDragHandler);
      pointerEvent.pointerDrag = (GameObject) null;
      ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, (BaseEventData) pointerEvent, ExecuteEvents.pointerExitHandler);
      pointerEvent.pointerEnter = (GameObject) null;
    }

    public override void DeactivateModule()
    {
      base.DeactivateModule();
      this.ClearSelection();
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(!this.UseFakeInput() ? "Input: Touch" : "Input: Faked");
      if (this.UseFakeInput())
      {
        PointerEventData pointerEventData1 = this.GetLastPointerEventData(-1);
        if (pointerEventData1 != null)
          stringBuilder.AppendLine(pointerEventData1.ToString());
        PointerEventData pointerEventData2 = this.GetLastPointerEventData(-2);
        if (pointerEventData2 != null)
          stringBuilder.AppendLine(pointerEventData2.ToString());
        PointerEventData pointerEventData3 = this.GetLastPointerEventData(-3);
        if (pointerEventData3 != null)
          stringBuilder.AppendLine(pointerEventData3.ToString());
      }
      else
      {
        foreach (KeyValuePair<int, PointerEventData> keyValuePair in this.m_PointerData)
          stringBuilder.AppendLine(keyValuePair.ToString());
      }
      return stringBuilder.ToString();
    }

    private void SpawnTouchEffect(Vector2 position)
    {
      if (!this.mTouchEffectPoolInitialized)
        this.InitTouchEffects();
      for (int index = 0; index < this.mTouchEffectPool.Length; ++index)
      {
        if ((UnityEngine.Object) this.mTouchEffectPool[index] != (UnityEngine.Object) null && !this.mTouchEffectPool[index].activeSelf)
        {
          GameObject gameObject = this.mTouchEffectPool[index];
          RectTransform transform = gameObject.transform as RectTransform;
          Vector2 localPoint;
          RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, position, (UnityEngine.Camera) null, out localPoint);
          transform.anchoredPosition = localPoint;
          gameObject.SetActive(true);
          ++this.mNumActiveTouchEffects;
          break;
        }
      }
    }

    private void UpdateTouchEffects()
    {
      for (int index1 = 0; index1 < this.mTouchEffectPool.Length; ++index1)
      {
        if (this.mTouchEffectPool[index1].activeSelf)
        {
          bool flag = false;
          UIParticleSystem[] componentsInChildren = this.mTouchEffectPool[index1].GetComponentsInChildren<UIParticleSystem>();
          for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
          {
            if (componentsInChildren[index2].IsAlive())
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
              componentsInChildren[index2].ResetParticleSystem();
            this.mTouchEffectPool[index1].SetActive(false);
            --this.mNumActiveTouchEffects;
          }
        }
      }
    }

    private void Update()
    {
      if (this.mNumActiveTouchEffects <= 0)
        return;
      this.UpdateTouchEffects();
    }

    public delegate void OnDoubleTapDelegate(Vector2 position);
  }
}
