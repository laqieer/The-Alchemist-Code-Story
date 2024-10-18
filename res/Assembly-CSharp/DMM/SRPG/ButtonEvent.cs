// Decompiled with JetBrains decompiler
// Type: SRPG.ButtonEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ButtonEvent : EventTrigger
  {
    private const float HODL_TIME = 0.5f;
    public GameObject syncEvent;
    [FormerlySerializedAs("autoLock")]
    public int flag;
    public ButtonEvent.SelectableType selectableType;
    public List<ButtonEvent.Event> events = new List<ButtonEvent.Event>();
    public bool flickEventEnable;
    public ButtonEvent.Event flickEvent = new ButtonEvent.Event();
    private Selectable m_Selectable;
    private Vector2 m_Pos = Vector2.zero;
    private bool m_DragMove;
    private float m_BeginEnterTime;
    private bool m_Enter;
    private bool m_HoldCheck;
    private bool m_HoldOn;
    public const string SYSTEM_LOCK = "system";
    private static Dictionary<string, int> m_Lock = new Dictionary<string, int>();
    private static List<ButtonEvent.Listener> m_Listener = new List<ButtonEvent.Listener>();

    private bool isInteractable
    {
      get
      {
        return !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Selectable, (UnityEngine.Object) null) || this.m_Selectable.IsInteractable();
      }
    }

    private void Awake()
    {
      ButtonEvent.SelectableType selectableType = ButtonEvent.SelectableType.AUTO;
      this.m_Selectable = (Selectable) ((Component) this).GetComponent<Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Selectable, (UnityEngine.Object) null))
      {
        selectableType = ButtonEvent.SelectableType.BUTTON;
      }
      else
      {
        this.m_Selectable = (Selectable) ((Component) this).GetComponent<Toggle>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Selectable, (UnityEngine.Object) null))
          selectableType = ButtonEvent.SelectableType.TOGGLE;
      }
      if (this.selectableType != ButtonEvent.SelectableType.AUTO)
        return;
      this.selectableType = selectableType;
    }

    protected virtual void Start()
    {
    }

    private void Update() => this.OnHold((EventTriggerType) 0);

    private void LateUpdate() => this.RefreshButton();

    private void RefreshButton()
    {
      if (!this.IsFlag(ButtonEvent.Flag.AUTOLOCK) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Selectable, (UnityEngine.Object) null))
        return;
      if (this.selectableType == ButtonEvent.SelectableType.TAB)
      {
        Toggle selectable = this.m_Selectable as Toggle;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) selectable, (UnityEngine.Object) null) && selectable.isOn)
        {
          this.m_Selectable.interactable = false;
          return;
        }
      }
      bool flag = !ButtonEvent.IsLock();
      if (this.m_Selectable.interactable == flag)
        return;
      this.m_Selectable.interactable = flag;
    }

    public void SetFlag(ButtonEvent.Flag value) => this.flag |= (int) value;

    public void ResetFlag(ButtonEvent.Flag value) => this.flag &= (int) ~value;

    public bool IsFlag(ButtonEvent.Flag value)
    {
      return ((ButtonEvent.Flag) this.flag & value) != (ButtonEvent.Flag) 0;
    }

    public bool HasEvent(EventTriggerType trigger)
    {
      return this.events.FindIndex((Predicate<ButtonEvent.Event>) (prop => !prop.hold && prop.trigger == trigger)) != -1;
    }

    public bool HasHoldEvent(EventTriggerType trigger)
    {
      return this.events.FindIndex((Predicate<ButtonEvent.Event>) (prop => prop.hold && prop.trigger == trigger)) != -1;
    }

    public ButtonEvent.Event[] GetEvents(EventTriggerType trigger)
    {
      return this.events.Where<ButtonEvent.Event>((Func<ButtonEvent.Event, bool>) (prop => !prop.hold && prop.trigger == trigger)).ToArray<ButtonEvent.Event>();
    }

    public ButtonEvent.Event[] GetHoldEvents(EventTriggerType trigger)
    {
      return this.events.Where<ButtonEvent.Event>((Func<ButtonEvent.Event, bool>) (prop => prop.hold && prop.trigger == trigger)).ToArray<ButtonEvent.Event>();
    }

    public ButtonEvent.Event GetEvent(string eventName)
    {
      return this.events.Find((Predicate<ButtonEvent.Event>) (prop => prop.eventName == eventName));
    }

    public ButtonEvent.Event[] GetEvents(string eventName)
    {
      return this.events.Where<ButtonEvent.Event>((Func<ButtonEvent.Event, bool>) (prop => prop.eventName == eventName)).ToArray<ButtonEvent.Event>();
    }

    public void PlaySe(ButtonEvent.Event ev)
    {
      if (ev.se < 0)
        return;
      SystemSound.Play((SystemSound.ECue) ev.se);
    }

    private bool Send(ButtonEvent.Event[] evs, GameObject select)
    {
      bool flag = false;
      if (evs != null)
      {
        for (int index = 0; index < evs.Length; ++index)
        {
          ButtonEvent.Event ev = evs[index];
          if (ev != null && !string.IsNullOrEmpty(ev.eventName) && this.isInteractable)
          {
            this.PlaySe(ev);
            SerializeValueList serializeValueList = new SerializeValueList(ev.valueList);
            serializeValueList.AddField("_self", ((Component) this).gameObject);
            serializeValueList.AddField("_select", select);
            if (ev.ignoreLock)
              ButtonEvent.ForceInvoke(ev.eventName, (object) serializeValueList);
            else
              ButtonEvent.Invoke(ev.eventName, (object) serializeValueList);
            flag = true;
          }
        }
      }
      return flag;
    }

    private bool Send(ButtonEvent.Event[] evs, Vector2 pos)
    {
      bool flag = false;
      if (evs != null)
      {
        for (int index = 0; index < evs.Length; ++index)
        {
          ButtonEvent.Event ev = evs[index];
          if (ev != null && !string.IsNullOrEmpty(ev.eventName) && this.isInteractable)
          {
            this.PlaySe(ev);
            SerializeValueList serializeValueList = new SerializeValueList(ev.valueList);
            serializeValueList.AddField("_self", ((Component) this).gameObject);
            serializeValueList.AddField("_pos", pos);
            if (ev.ignoreLock)
              ButtonEvent.ForceInvoke(ev.eventName, (object) serializeValueList);
            else
              ButtonEvent.Invoke(ev.eventName, (object) serializeValueList);
            flag = true;
          }
        }
      }
      return flag;
    }

    private bool Send(ButtonEvent.Event[] evs, Vector2 pos, Vector2 vct)
    {
      bool flag = false;
      if (evs != null)
      {
        for (int index = 0; index < evs.Length; ++index)
        {
          ButtonEvent.Event ev = evs[index];
          if (!string.IsNullOrEmpty(ev.eventName) && this.isInteractable)
          {
            this.PlaySe(ev);
            SerializeValueList serializeValueList = new SerializeValueList(ev.valueList);
            serializeValueList.AddField("_self", ((Component) this).gameObject);
            serializeValueList.AddField("_pos", pos);
            serializeValueList.AddField("_vct", vct);
            if (ev.ignoreLock)
              ButtonEvent.ForceInvoke(ev.eventName, (object) serializeValueList);
            else
              ButtonEvent.Invoke(ev.eventName, (object) serializeValueList);
            flag = true;
          }
        }
      }
      return flag;
    }

    private bool Send(EventTriggerType trigger, GameObject select)
    {
      return this.Send(this.GetEvents(trigger), select);
    }

    private bool Send(EventTriggerType trigger, Vector2 pos)
    {
      return this.Send(this.GetEvents(trigger), pos);
    }

    private bool Send(EventTriggerType trigger, Vector2 pos, Vector2 vct)
    {
      return this.Send(this.GetEvents(trigger), pos, vct);
    }

    private bool SendHold(EventTriggerType trigger, Vector2 pos)
    {
      bool flag = false;
      ButtonEvent.Event[] holdEvents = this.GetHoldEvents(trigger);
      if (holdEvents != null)
      {
        for (int index = 0; index < holdEvents.Length; ++index)
        {
          ButtonEvent.Event ev = holdEvents[index];
          if (ev != null && !string.IsNullOrEmpty(ev.eventName) && this.isInteractable)
          {
            this.PlaySe(ev);
            SerializeValueList serializeValueList = new SerializeValueList(ev.valueList);
            serializeValueList.AddField("_self", ((Component) this).gameObject);
            serializeValueList.AddField("_pos", pos);
            if (ev.ignoreLock)
              ButtonEvent.ForceInvoke(ev.eventName, (object) serializeValueList);
            else
              ButtonEvent.Invoke(ev.eventName, (object) serializeValueList);
            flag = true;
          }
        }
      }
      return flag;
    }

    private void OnHold(EventTriggerType trigger)
    {
      if (this.m_HoldCheck)
        this.m_HoldOn = false;
      else if (this.m_Enter)
      {
        if (this.IsFlag(ButtonEvent.Flag.DRAGMOVELOCK) && this.m_DragMove || (double) (Time.unscaledTime - this.m_BeginEnterTime) <= 0.5)
          return;
        this.m_HoldCheck = true;
        if (!this.HasHoldEvent(trigger))
          return;
        this.m_HoldOn = this.SendHold(trigger, this.m_Pos);
      }
      else
        this.m_HoldCheck = true;
    }

    public virtual void OnPointerEnter(PointerEventData data)
    {
      this.m_Enter = true;
      this.m_BeginEnterTime = Time.unscaledTime;
      if (!this.HasEvent((EventTriggerType) 0))
        return;
      this.Send((EventTriggerType) 0, data.position);
    }

    public virtual void OnPointerExit(PointerEventData data)
    {
      this.m_Enter = false;
      if (!this.HasEvent((EventTriggerType) 1))
        return;
      this.Send((EventTriggerType) 1, data.position);
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
      this.m_HoldCheck = false;
      if (!this.HasEvent((EventTriggerType) 2))
        return;
      this.Send((EventTriggerType) 2, data.position);
    }

    public virtual void OnPointerUp(PointerEventData data)
    {
      if (!this.HasEvent((EventTriggerType) 3))
        return;
      this.Send((EventTriggerType) 3, data.position);
    }

    public virtual void OnPointerClick(PointerEventData data)
    {
      if (this.m_HoldOn || !this.HasEvent((EventTriggerType) 4))
        return;
      this.OnHold((EventTriggerType) 3);
      if (this.m_HoldOn || this.IsFlag(ButtonEvent.Flag.DRAGMOVELOCK) && this.m_DragMove)
        return;
      this.Send((EventTriggerType) 4, data.position);
    }

    public virtual void OnBeginDrag(PointerEventData data)
    {
      this.m_DragMove = false;
      this.m_Pos = data.position;
      if (this.HasEvent((EventTriggerType) 13))
        this.Send((EventTriggerType) 13, data.position);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.syncEvent, (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IBeginDragHandler>()?.OnBeginDrag(data);
    }

    public virtual void OnDrag(PointerEventData data)
    {
      if (this.HasEvent((EventTriggerType) 5))
        this.Send((EventTriggerType) 5, data.position);
      if (!this.m_DragMove)
      {
        Vector2 vector2 = Vector2.op_Subtraction(data.position, this.m_Pos);
        if ((double) ((Vector2) ref vector2).magnitude > 10.0)
          this.m_DragMove = true;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.syncEvent, (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IDragHandler>()?.OnDrag(data);
    }

    public virtual void OnEndDrag(PointerEventData data)
    {
      this.m_DragMove = false;
      if (this.HasEvent((EventTriggerType) 14))
        this.Send((EventTriggerType) 14, data.position);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.syncEvent, (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IEndDragHandler>()?.OnEndDrag(data);
    }

    public virtual void OnSelect(BaseEventData data)
    {
      if (!this.HasEvent((EventTriggerType) 9))
        return;
      this.Send((EventTriggerType) 9, data.selectedObject);
    }

    public virtual void OnUpdateSelected(BaseEventData data)
    {
      if (!this.HasEvent((EventTriggerType) 8))
        return;
      this.Send((EventTriggerType) 8, data.selectedObject);
    }

    public virtual void OnCancel(BaseEventData data)
    {
      if (!this.HasEvent((EventTriggerType) 16))
        return;
      this.Send((EventTriggerType) 16, data.selectedObject);
    }

    public virtual void OnDeselect(BaseEventData data)
    {
      if (!this.HasEvent((EventTriggerType) 10))
        return;
      this.Send((EventTriggerType) 10, data.selectedObject);
    }

    public virtual void OnDrop(PointerEventData data)
    {
      if (!this.HasEvent((EventTriggerType) 6))
        return;
      this.Send((EventTriggerType) 6, data.position);
    }

    public virtual void OnInitializePotentialDrag(PointerEventData data)
    {
      if (this.HasEvent((EventTriggerType) 12))
        this.Send((EventTriggerType) 12, data.position);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.syncEvent, (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IInitializePotentialDragHandler>()?.OnInitializePotentialDrag(data);
    }

    public virtual void OnMove(AxisEventData data)
    {
      if (!this.HasEvent((EventTriggerType) 11))
        return;
      this.Send((EventTriggerType) 11, data.moveVector);
    }

    public virtual void OnScroll(PointerEventData data)
    {
      if (this.HasEvent((EventTriggerType) 7))
        this.Send((EventTriggerType) 7, data.position);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.syncEvent, (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IScrollHandler>()?.OnScroll(data);
    }

    public virtual void OnSubmit(BaseEventData data)
    {
      if (!this.HasEvent((EventTriggerType) 15))
        return;
      this.Send((EventTriggerType) 15, data.selectedObject);
    }

    public static void Reset()
    {
      ButtonEvent.m_Lock.Clear();
      ButtonEvent.m_Listener.Clear();
    }

    public static void ResetLock(string key)
    {
      if (string.IsNullOrEmpty(key))
        return;
      ButtonEvent.m_Lock.Remove(key);
    }

    public static int GetLockCount() => ButtonEvent.m_Lock.Count;

    public static void Lock(string key)
    {
      if (string.IsNullOrEmpty(key))
        key = "system";
      if (!ButtonEvent.m_Lock.ContainsKey(key))
      {
        ButtonEvent.m_Lock.Add(key, 1);
      }
      else
      {
        Dictionary<string, int> dictionary;
        string key1;
        (dictionary = ButtonEvent.m_Lock)[key1 = key] = dictionary[key1] + 1;
      }
    }

    public static void Lock() => ButtonEvent.Lock("system");

    public static void UnLock(string key)
    {
      if (string.IsNullOrEmpty(key))
        key = "system";
      int num = 0;
      if (!ButtonEvent.m_Lock.TryGetValue(key, out num))
        return;
      --num;
      if (num > 0)
        ButtonEvent.m_Lock[key] = num;
      else
        ButtonEvent.m_Lock.Remove(key);
    }

    public static void UnLock() => ButtonEvent.UnLock("system");

    public static bool IsLock() => ButtonEvent.m_Lock.Count > 0;

    public static ButtonEvent.Listener AddListener(string eventName, Action<bool, object> callback)
    {
      ButtonEvent.Listener listener = new ButtonEvent.Listener();
      listener.eventName = eventName;
      listener.callback = callback;
      ButtonEvent.m_Listener.Add(listener);
      return listener;
    }

    public static void RemoveListener(ButtonEvent.Listener listener)
    {
      ButtonEvent.m_Listener.Remove(listener);
    }

    public static void Invoke(string eventName, object value)
    {
      if (ButtonEvent.IsLock())
        return;
      for (int index = 0; index < ButtonEvent.m_Listener.Count; ++index)
      {
        ButtonEvent.Listener listener = ButtonEvent.m_Listener[index];
        if (listener.eventName == eventName)
          listener.callback(false, value);
      }
    }

    public static void ForceInvoke(string eventName, object value)
    {
      for (int index = 0; index < ButtonEvent.m_Listener.Count; ++index)
      {
        ButtonEvent.Listener listener = ButtonEvent.m_Listener[index];
        if (listener.eventName == eventName)
          listener.callback(true, value);
      }
    }

    public enum SelectableType
    {
      AUTO,
      BUTTON,
      TOGGLE,
      TAB,
    }

    public enum Flag
    {
      AUTOLOCK = 1,
      DRAGMOVELOCK = 2,
    }

    [Serializable]
    public class Event
    {
      public EventTriggerType trigger = (EventTriggerType) 4;
      public string eventName = string.Empty;
      public int se = -1;
      [FormerlySerializedAs("ignoreLock")]
      public int flag;
      public SerializeValueList valueList = new SerializeValueList();

      public bool ignoreLock
      {
        set => this.SetFlag(ButtonEvent.Event.Flag.IGNORELOCK, value);
        get => this.IsFlag(ButtonEvent.Event.Flag.IGNORELOCK);
      }

      public bool hold
      {
        set => this.SetFlag(ButtonEvent.Event.Flag.HOLD, value);
        get => this.IsFlag(ButtonEvent.Event.Flag.HOLD);
      }

      private void SetFlag(ButtonEvent.Event.Flag value, bool sw)
      {
        if (sw)
          this.flag |= (int) value;
        else
          this.flag &= (int) ~value;
      }

      private bool IsFlag(ButtonEvent.Event.Flag value)
      {
        return ((ButtonEvent.Event.Flag) this.flag & value) != (ButtonEvent.Event.Flag) 0;
      }

      private enum Flag
      {
        IGNORELOCK = 1,
        HOLD = 2,
      }
    }

    public class Listener
    {
      public string eventName;
      public Action<bool, object> callback;
    }
  }
}
