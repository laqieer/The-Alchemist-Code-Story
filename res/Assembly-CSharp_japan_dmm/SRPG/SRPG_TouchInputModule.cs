// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_TouchInputModule
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Event/Touch Input Module (SRPG)")]
  public class SRPG_TouchInputModule : PointerInputModule
  {
    private static int mLockCount;
    public GameObject TouchEffect;
    private GameObject[] mTouchEffectPool = new GameObject[8];
    private int mNumActiveTouchEffects;
    private bool mTouchEffectPoolInitialized;
    public SRPG_TouchInputModule.OnDoubleTapDelegate OnDoubleTap;
    private float mDoubleTap1stReleasedTime = -1f;
    private readonly int BUTTON_INDEX_MAX = 3;
    private int pressing_button_index = -1;
    public static bool IsMultiTouching;
    private Vector2 m_LastMousePosition;
    private Vector2 m_MousePosition;
    [SerializeField]
    private bool m_AllowActivationOnStandalone = true;
    private int mPrimaryFingerID = -1;

    public static void LockInput()
    {
      ++SRPG_TouchInputModule.mLockCount;
      ((Behaviour) EventSystem.current.currentInputModule).enabled = SRPG_TouchInputModule.mLockCount == 0;
    }

    public static void UnlockInput(bool forceReset = false)
    {
      if (forceReset)
        SRPG_TouchInputModule.mLockCount = 0;
      else
        --SRPG_TouchInputModule.mLockCount;
      ((Behaviour) EventSystem.current.currentInputModule).enabled = SRPG_TouchInputModule.mLockCount == 0;
    }

    private bool IsHandling { get; set; }

    private void InitTouchEffects()
    {
      if (this.mTouchEffectPoolInitialized)
        return;
      if (Object.op_Inequality((Object) this.TouchEffect, (Object) null))
      {
        for (int index = 0; index < this.mTouchEffectPool.Length; ++index)
        {
          this.mTouchEffectPool[index] = Object.Instantiate<GameObject>(this.TouchEffect);
          this.mTouchEffectPool[index].SetActive(false);
          this.mTouchEffectPool[index].transform.SetParent(((Component) UIUtility.ParticleCanvas).transform, false);
        }
      }
      this.mTouchEffectPoolInitialized = true;
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      UIUtility.InitParticleCanvas();
      this.InitTouchEffects();
      this.OnDoubleTap += (SRPG_TouchInputModule.OnDoubleTapDelegate) (position => { });
    }

    public bool allowActivationOnStandalone
    {
      get => this.m_AllowActivationOnStandalone;
      set => this.m_AllowActivationOnStandalone = value;
    }

    public virtual void UpdateModule()
    {
      this.m_LastMousePosition = this.m_MousePosition;
      this.m_MousePosition = Vector2.op_Implicit(Input.mousePosition);
    }

    public virtual bool IsModuleSupported()
    {
      return this.m_AllowActivationOnStandalone || Application.isMobilePlatform;
    }

    public virtual bool ShouldActivateModule()
    {
      if (!((BaseInputModule) this).ShouldActivateModule())
        return false;
      if (this.UseFakeInput())
      {
        int num1 = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ? (true ? 1 : 0) : (Input.GetMouseButtonDown(2) ? 1 : 0);
        Vector2 vector2 = Vector2.op_Subtraction(this.m_MousePosition, this.m_LastMousePosition);
        int num2 = (double) ((Vector2) ref vector2).sqrMagnitude > 0.0 ? 1 : 0;
        return (num1 | num2) != 0;
      }
      for (int index = 0; index < Input.touchCount; ++index)
      {
        Touch touch = Input.GetTouch(index);
        if (((Touch) ref touch).phase == null || ((Touch) ref touch).phase == 1 || ((Touch) ref touch).phase == 2)
          return true;
      }
      return false;
    }

    private bool UseFakeInput() => !Application.isMobilePlatform;

    public virtual void Process()
    {
      this.IsHandling = false;
      if (Application.platform == 7 || Application.platform == 2 || Application.platform == null || Application.platform == 1)
        this.SendUpdateEventToSelectedObject();
      if (!((Behaviour) this).enabled)
        return;
      if (this.UseFakeInput())
        this.FakeTouches();
      else
        this.ProcessTouchEvents();
      if (this.IsHandling || !Input.GetKeyDown((KeyCode) 27))
        return;
      BackHandler.Invoke();
      Input.ResetInputAxes();
    }

    private bool SendUpdateEventToSelectedObject()
    {
      if (Object.op_Equality((Object) ((BaseInputModule) this).eventSystem.currentSelectedGameObject, (Object) null))
        return false;
      BaseEventData baseEventData = ((BaseInputModule) this).GetBaseEventData();
      ExecuteEvents.Execute<IUpdateSelectedHandler>(((BaseInputModule) this).eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
      return ((AbstractEventData) baseEventData).used;
    }

    private PointerEventData GetMousePointerEvent(int index)
    {
      int num = new int[3]{ -1, -2, -3 }[index];
      bool mouseButtonDown = Input.GetMouseButtonDown(num);
      PointerEventData mousePointerEvent;
      bool pointerData = this.GetPointerData(num, ref mousePointerEvent, true);
      ((AbstractEventData) mousePointerEvent).Reset();
      if (pointerData)
        mousePointerEvent.position = Vector2.op_Implicit(Input.mousePosition);
      Vector2 vector2 = Vector2.op_Implicit(Input.mousePosition);
      mousePointerEvent.delta = Vector2.op_Subtraction(vector2, mousePointerEvent.position);
      mousePointerEvent.position = vector2;
      mousePointerEvent.scrollDelta = Input.mouseScrollDelta;
      ((BaseInputModule) this).eventSystem.RaycastAll(mousePointerEvent, ((BaseInputModule) this).m_RaycastResultCache);
      RaycastResult firstRaycast = BaseInputModule.FindFirstRaycast(((BaseInputModule) this).m_RaycastResultCache);
      mousePointerEvent.pointerCurrentRaycast = firstRaycast;
      if (mouseButtonDown)
        mousePointerEvent.delta = Vector2.zero;
      return mousePointerEvent;
    }

    private void FakeTouches()
    {
      bool flag = false;
      bool pressed = false;
      bool released = false;
      if (this.pressing_button_index <= -1)
      {
        for (int index = 0; index < this.BUTTON_INDEX_MAX; ++index)
        {
          if (Input.GetMouseButtonDown(index))
          {
            this.pressing_button_index = index;
            pressed = true;
            break;
          }
        }
      }
      for (int index = 0; index < this.BUTTON_INDEX_MAX; ++index)
      {
        if (Input.GetMouseButtonUp(index) && this.pressing_button_index == index)
        {
          this.pressing_button_index = -1;
          released = true;
          break;
        }
      }
      this.IsHandling = pressed || released;
      PointerEventData buttonData1 = this.GetMousePointerEventData().GetButtonState((PointerEventData.InputButton) 0).eventData.buttonData;
      this.ProcessTouchPress(buttonData1, pressed, released);
      if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
      {
        this.IsHandling = true;
        this.ProcessMove(buttonData1);
        this.ProcessDrag(buttonData1);
        RaycastResult pointerPressRaycast1 = buttonData1.pointerPressRaycast;
        if (((RaycastResult) ref pointerPressRaycast1).isValid)
        {
          PointerEventData buttonData2 = this.GetMousePointerEventData().GetButtonState((PointerEventData.InputButton) 1).eventData.buttonData;
          if (Input.GetMouseButtonDown(1))
            buttonData2.pointerPressRaycast = buttonData2.pointerCurrentRaycast;
          if (Input.GetMouseButton(1))
          {
            RaycastResult pointerPressRaycast2 = buttonData1.pointerPressRaycast;
            GameObject gameObject1 = ((RaycastResult) ref pointerPressRaycast2).gameObject;
            RaycastResult pointerPressRaycast3 = buttonData2.pointerPressRaycast;
            GameObject gameObject2 = ((RaycastResult) ref pointerPressRaycast3).gameObject;
            flag = Object.op_Equality((Object) gameObject1, (Object) gameObject2);
          }
        }
      }
      SRPG_TouchInputModule.IsMultiTouching = flag;
    }

    private void ProcessTouchEvents()
    {
      List<GameObject> gameObjectList1 = new List<GameObject>();
      if (this.mPrimaryFingerID >= 0 && Input.touchCount <= 0)
        this.mPrimaryFingerID = -1;
      for (int index = 0; index < Input.touchCount; ++index)
      {
        Touch touch = Input.GetTouch(index);
        bool pressed;
        bool released;
        PointerEventData pointerEventData = this.GetTouchPointerEventData(touch, ref pressed, ref released);
        if (this.mPrimaryFingerID == -1 && pressed)
          this.mPrimaryFingerID = ((Touch) ref touch).fingerId;
        if (this.mPrimaryFingerID == ((Touch) ref touch).fingerId)
        {
          this.ProcessTouchPress(pointerEventData, pressed, released);
          if (!released)
          {
            List<GameObject> gameObjectList2 = gameObjectList1;
            RaycastResult pointerPressRaycast = pointerEventData.pointerPressRaycast;
            GameObject gameObject = ((RaycastResult) ref pointerPressRaycast).gameObject;
            gameObjectList2.Add(gameObject);
            this.ProcessMove(pointerEventData);
            this.ProcessDrag(pointerEventData);
          }
          else
            this.mPrimaryFingerID = -1;
        }
        else if (pressed)
          pointerEventData.pointerPressRaycast = pointerEventData.pointerCurrentRaycast;
        else if (!released)
        {
          List<GameObject> gameObjectList3 = gameObjectList1;
          RaycastResult pointerPressRaycast = pointerEventData.pointerPressRaycast;
          GameObject gameObject = ((RaycastResult) ref pointerPressRaycast).gameObject;
          gameObjectList3.Add(gameObject);
        }
        if (released)
          this.RemovePointerData(pointerEventData);
      }
      SRPG_TouchInputModule.IsMultiTouching = false;
      if (this.mPrimaryFingerID == -1 || gameObjectList1.Count < 2)
        return;
      for (int index1 = 0; index1 < gameObjectList1.Count; ++index1)
      {
        if (Object.op_Equality((Object) gameObjectList1[index1], (Object) null))
          return;
        for (int index2 = index1 + 1; index2 < gameObjectList1.Count; ++index2)
        {
          if (Object.op_Inequality((Object) gameObjectList1[index1], (Object) gameObjectList1[index2]))
            return;
        }
      }
      SRPG_TouchInputModule.IsMultiTouching = true;
    }

    private void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
    {
      RaycastResult pointerCurrentRaycast = pointerEvent.pointerCurrentRaycast;
      GameObject gameObject1 = ((RaycastResult) ref pointerCurrentRaycast).gameObject;
      if (pressed)
      {
        pointerEvent.eligibleForClick = true;
        pointerEvent.delta = Vector2.zero;
        pointerEvent.pressPosition = pointerEvent.position;
        pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
        if (Object.op_Inequality((Object) pointerEvent.pointerEnter, (Object) gameObject1))
        {
          ((BaseInputModule) this).HandlePointerExitAndEnter(pointerEvent, gameObject1);
          pointerEvent.pointerEnter = gameObject1;
        }
        GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject1, (BaseEventData) pointerEvent, ExecuteEvents.pointerDownHandler);
        if (Object.op_Equality((Object) gameObject2, (Object) null))
          gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject1);
        if (Object.op_Inequality((Object) gameObject2, (Object) pointerEvent.pointerPress))
        {
          pointerEvent.pointerPress = gameObject2;
          pointerEvent.rawPointerPress = gameObject1;
          pointerEvent.clickCount = 0;
        }
        pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject1);
        if (Object.op_Inequality((Object) pointerEvent.pointerDrag, (Object) null))
          ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, (BaseEventData) pointerEvent, ExecuteEvents.beginDragHandler);
        ((BaseInputModule) this).eventSystem.SetSelectedGameObject(ExecuteEvents.GetEventHandler<ISelectHandler>(gameObject1), (BaseEventData) pointerEvent);
      }
      if (!released)
        return;
      float unscaledTime1 = Time.unscaledTime;
      if ((double) this.mDoubleTap1stReleasedTime < 0.0)
        this.mDoubleTap1stReleasedTime = unscaledTime1;
      else if ((double) unscaledTime1 - (double) this.mDoubleTap1stReleasedTime >= 0.30000001192092896)
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
      if (Object.op_Equality((Object) pointerEvent.pointerPress, (Object) eventHandler) && pointerEvent.eligibleForClick)
      {
        float unscaledTime2 = Time.unscaledTime;
        if ((double) unscaledTime2 - (double) pointerEvent.clickTime < 0.30000001192092896)
          ++pointerEvent.clickCount;
        else
          pointerEvent.clickCount = 1;
        pointerEvent.clickTime = unscaledTime2;
        ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, (BaseEventData) pointerEvent, ExecuteEvents.pointerClickHandler);
        this.SpawnTouchEffect(pointerEvent.position);
      }
      else if (Object.op_Inequality((Object) pointerEvent.pointerDrag, (Object) null))
        ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject1, (BaseEventData) pointerEvent, ExecuteEvents.dropHandler);
      pointerEvent.eligibleForClick = false;
      pointerEvent.pointerPress = (GameObject) null;
      pointerEvent.rawPointerPress = (GameObject) null;
      if (Object.op_Inequality((Object) pointerEvent.pointerDrag, (Object) null))
        ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, (BaseEventData) pointerEvent, ExecuteEvents.endDragHandler);
      pointerEvent.pointerDrag = (GameObject) null;
      ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, (BaseEventData) pointerEvent, ExecuteEvents.pointerExitHandler);
      pointerEvent.pointerEnter = (GameObject) null;
    }

    public virtual void DeactivateModule()
    {
      ((BaseInputModule) this).DeactivateModule();
      this.ClearSelection();
    }

    public virtual string ToString()
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
        if (Object.op_Inequality((Object) this.mTouchEffectPool[index], (Object) null) && !this.mTouchEffectPool[index].activeSelf)
        {
          GameObject gameObject = this.mTouchEffectPool[index];
          RectTransform transform = gameObject.transform as RectTransform;
          Vector2 vector2;
          RectTransformUtility.ScreenPointToLocalPointInRectangle(((Transform) transform).parent as RectTransform, position, (Camera) null, ref vector2);
          transform.anchoredPosition = vector2;
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
            for (int index3 = componentsInChildren.Length - 1; index3 >= 0; --index3)
              componentsInChildren[index3].ResetParticleSystem();
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
