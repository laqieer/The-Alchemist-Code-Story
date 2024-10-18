// Decompiled with JetBrains decompiler
// Type: SRPG.ButtonEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SRPG
{
  public class ButtonEvent : UnityEngine.EventSystems.EventTrigger
  {
    private static Dictionary<string, int> m_Lock = new Dictionary<string, int>();
    private static List<ButtonEvent.Listener> m_Listener = new List<ButtonEvent.Listener>();
    public List<ButtonEvent.Event> events = new List<ButtonEvent.Event>();
    public ButtonEvent.Event flickEvent = new ButtonEvent.Event();
    private Vector2 m_Pos = Vector2.zero;
    private const float HODL_TIME = 0.5f;
    public GameObject syncEvent;
    [FormerlySerializedAs("autoLock")]
    public int flag;
    public ButtonEvent.SelectableType selectableType;
    public bool flickEventEnable;
    private Selectable m_Selectable;
    private bool m_DragMove;
    private float m_BeginEnterTime;
    private bool m_Enter;
    private bool m_HoldCheck;
    private bool m_HoldOn;
    public const string SYSTEM_LOCK = "system";

    private bool isInteractable
    {
      get
      {
        if ((UnityEngine.Object) this.m_Selectable != (UnityEngine.Object) null)
          return this.m_Selectable.IsInteractable();
        return true;
      }
    }

    private void Awake()
    {
      ButtonEvent.SelectableType selectableType = ButtonEvent.SelectableType.AUTO;
      this.m_Selectable = (Selectable) this.GetComponent<Button>();
      if ((UnityEngine.Object) this.m_Selectable != (UnityEngine.Object) null)
      {
        selectableType = ButtonEvent.SelectableType.BUTTON;
      }
      else
      {
        this.m_Selectable = (Selectable) this.GetComponent<Toggle>();
        if ((UnityEngine.Object) this.m_Selectable != (UnityEngine.Object) null)
          selectableType = ButtonEvent.SelectableType.TOGGLE;
      }
      if (this.selectableType != ButtonEvent.SelectableType.AUTO)
        return;
      this.selectableType = selectableType;
    }

    protected virtual void Start()
    {
    }

    private void Update()
    {
      this.OnHold(EventTriggerType.PointerEnter);
    }

    private void LateUpdate()
    {
      this.RefreshButton();
    }

    private void RefreshButton()
    {
      if (!this.IsFlag(ButtonEvent.Flag.AUTOLOCK) || !((UnityEngine.Object) this.m_Selectable != (UnityEngine.Object) null))
        return;
      if (this.selectableType == ButtonEvent.SelectableType.TAB)
      {
        Toggle selectable = this.m_Selectable as Toggle;
        if ((UnityEngine.Object) selectable != (UnityEngine.Object) null && selectable.isOn)
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

    public void SetFlag(ButtonEvent.Flag value)
    {
      this.flag |= (int) value;
    }

    public void ResetFlag(ButtonEvent.Flag value)
    {
      this.flag &= (int) ~value;
    }

    public bool IsFlag(ButtonEvent.Flag value)
    {
      return ((ButtonEvent.Flag) this.flag & value) != (ButtonEvent.Flag) 0;
    }

    public bool HasEvent(EventTriggerType trigger)
    {
      return this.events.FindIndex((Predicate<ButtonEvent.Event>) (prop =>
      {
        if (!prop.hold)
          return prop.trigger == trigger;
        return false;
      })) != -1;
    }

    public bool HasHoldEvent(EventTriggerType trigger)
    {
      return this.events.FindIndex((Predicate<ButtonEvent.Event>) (prop =>
      {
        if (prop.hold)
          return prop.trigger == trigger;
        return false;
      })) != -1;
    }

    public ButtonEvent.Event[] GetEvents(EventTriggerType trigger)
    {
      return this.events.Where<ButtonEvent.Event>((Func<ButtonEvent.Event, bool>) (prop =>
      {
        if (!prop.hold)
          return prop.trigger == trigger;
        return false;
      })).ToArray<ButtonEvent.Event>();
    }

    public ButtonEvent.Event[] GetHoldEvents(EventTriggerType trigger)
    {
      return this.events.Where<ButtonEvent.Event>((Func<ButtonEvent.Event, bool>) (prop =>
      {
        if (prop.hold)
          return prop.trigger == trigger;
        return false;
      })).ToArray<ButtonEvent.Event>();
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
            serializeValueList.AddField("_self", this.gameObject);
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
            serializeValueList.AddField("_self", this.gameObject);
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
            serializeValueList.AddField("_self", this.gameObject);
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
            serializeValueList.AddField("_self", this.gameObject);
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

    public override void OnPointerEnter(PointerEventData data)
    {
      this.m_Enter = true;
      this.m_BeginEnterTime = Time.unscaledTime;
      if (!this.HasEvent(EventTriggerType.PointerEnter))
        return;
      this.Send(EventTriggerType.PointerEnter, data.position);
    }

    public override void OnPointerExit(PointerEventData data)
    {
      this.m_Enter = false;
      if (!this.HasEvent(EventTriggerType.PointerExit))
        return;
      this.Send(EventTriggerType.PointerExit, data.position);
    }

    public override void OnPointerDown(PointerEventData data)
    {
      this.m_HoldCheck = false;
      if (!this.HasEvent(EventTriggerType.PointerDown))
        return;
      this.Send(EventTriggerType.PointerDown, data.position);
    }

    public override void OnPointerUp(PointerEventData data)
    {
      if (!this.HasEvent(EventTriggerType.PointerUp))
        return;
      this.Send(EventTriggerType.PointerUp, data.position);
    }

    public override void OnPointerClick(PointerEventData data)
    {
      if (this.m_HoldOn || !this.HasEvent(EventTriggerType.PointerClick))
        return;
      this.OnHold(EventTriggerType.PointerUp);
      if (this.m_HoldOn || this.IsFlag(ButtonEvent.Flag.DRAGMOVELOCK) && this.m_DragMove)
        return;
      this.Send(EventTriggerType.PointerClick, data.position);
    }

    public override void OnBeginDrag(PointerEventData data)
    {
      this.m_DragMove = false;
      this.m_Pos = data.position;
      if (this.HasEvent(EventTriggerType.BeginDrag))
        this.Send(EventTriggerType.BeginDrag, data.position);
      if (!((UnityEngine.Object) this.syncEvent != (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IBeginDragHandler>()?.OnBeginDrag(data);
    }

    public override void OnDrag(PointerEventData data)
    {
      if (this.HasEvent(EventTriggerType.Drag))
        this.Send(EventTriggerType.Drag, data.position);
      if (!this.m_DragMove && (double) (data.position - this.m_Pos).magnitude > 10.0)
        this.m_DragMove = true;
      if (!((UnityEngine.Object) this.syncEvent != (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IDragHandler>()?.OnDrag(data);
    }

    public override void OnEndDrag(PointerEventData data)
    {
      this.m_DragMove = false;
      if (this.HasEvent(EventTriggerType.EndDrag))
        this.Send(EventTriggerType.EndDrag, data.position);
      if (!((UnityEngine.Object) this.syncEvent != (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IEndDragHandler>()?.OnEndDrag(data);
    }

    public override void OnSelect(BaseEventData data)
    {
      if (!this.HasEvent(EventTriggerType.Select))
        return;
      this.Send(EventTriggerType.Select, data.selectedObject);
    }

    public override void OnUpdateSelected(BaseEventData data)
    {
      if (!this.HasEvent(EventTriggerType.UpdateSelected))
        return;
      this.Send(EventTriggerType.UpdateSelected, data.selectedObject);
    }

    public override void OnCancel(BaseEventData data)
    {
      if (!this.HasEvent(EventTriggerType.Cancel))
        return;
      this.Send(EventTriggerType.Cancel, data.selectedObject);
    }

    public override void OnDeselect(BaseEventData data)
    {
      if (!this.HasEvent(EventTriggerType.Deselect))
        return;
      this.Send(EventTriggerType.Deselect, data.selectedObject);
    }

    public override void OnDrop(PointerEventData data)
    {
      if (!this.HasEvent(EventTriggerType.Drop))
        return;
      this.Send(EventTriggerType.Drop, data.position);
    }

    public override void OnInitializePotentialDrag(PointerEventData data)
    {
      if (this.HasEvent(EventTriggerType.InitializePotentialDrag))
        this.Send(EventTriggerType.InitializePotentialDrag, data.position);
      if (!((UnityEngine.Object) this.syncEvent != (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IInitializePotentialDragHandler>()?.OnInitializePotentialDrag(data);
    }

    public override void OnMove(AxisEventData data)
    {
      if (!this.HasEvent(EventTriggerType.Move))
        return;
      this.Send(EventTriggerType.Move, data.moveVector);
    }

    public override void OnScroll(PointerEventData data)
    {
      if (this.HasEvent(EventTriggerType.Scroll))
        this.Send(EventTriggerType.Scroll, data.position);
      if (!((UnityEngine.Object) this.syncEvent != (UnityEngine.Object) null))
        return;
      this.syncEvent.GetComponent<IScrollHandler>()?.OnScroll(data);
    }

    public override void OnSubmit(BaseEventData data)
    {
      if (!this.HasEvent(EventTriggerType.Submit))
        return;
      this.Send(EventTriggerType.Submit, data.selectedObject);
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

    public static int GetLockCount()
    {
      return ButtonEvent.m_Lock.Count;
    }

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
        string index;
        (dictionary = ButtonEvent.m_Lock)[index = key] = dictionary[index] + 1;
      }
    }

    public static void Lock()
    {
      ButtonEvent.Lock("system");
    }

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

    public static void UnLock()
    {
      ButtonEvent.UnLock("system");
    }

    public static bool IsLock()
    {
      return ButtonEvent.m_Lock.Count > 0;
    }

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
      public EventTriggerType trigger = EventTriggerType.PointerClick;
      public string eventName = string.Empty;
      public int se = -1;
      public SerializeValueList valueList = new SerializeValueList();
      [FormerlySerializedAs("ignoreLock")]
      public int flag;

      public bool ignoreLock
      {
        set
        {
          this.SetFlag(ButtonEvent.Event.Flag.IGNORELOCK, value);
        }
        get
        {
          return this.IsFlag(ButtonEvent.Event.Flag.IGNORELOCK);
        }
      }

      public bool hold
      {
        set
        {
          this.SetFlag(ButtonEvent.Event.Flag.HOLD, value);
        }
        get
        {
          return this.IsFlag(ButtonEvent.Event.Flag.HOLD);
        }
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
