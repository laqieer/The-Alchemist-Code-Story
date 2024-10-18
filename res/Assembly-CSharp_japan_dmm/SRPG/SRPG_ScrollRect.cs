// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ScrollRect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("UI/ScrollRect (SRPG)")]
  public class SRPG_ScrollRect : ScrollRect
  {
    private float DEFAULT_REST_ADD_TIME = 0.05f;
    private float DEFAULT_SCROLL_DECELERATION_RATE = 0.135f;
    private float WHEEL_SCROLL_DECELERATION_RATE = 0.0005f;
    private float SCROLL_FIXED_VALUE = 500f;
    private float SCROLL_VALUE_COEF = 5000f;
    private bool IS_ENABLE_WHEEL_SCROLL_FOR_HORIZENTAL = true;
    private PointerEventData pointer_event;
    private List<GameObject> child_objects = new List<GameObject>();
    private List<RaycastResult> raycast_result_list = new List<RaycastResult>();
    private ScrollRect.MovementType defalt_movement_type;
    private float axis_val;
    private float rest_add_time;
    private bool is_scroll_now;
    private bool is_wheel_scroll;

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      if (Object.op_Inequality((Object) this.verticalScrollbar, (Object) null))
        this.verticalScrollbar.value = 1f;
      this.defalt_movement_type = this.movementType;
    }

    public bool IsScrollNow => this.is_scroll_now;

    private bool IsWheelScroll
    {
      get => this.is_wheel_scroll;
      set
      {
        this.is_wheel_scroll = value;
        if (this.movementType != null)
          this.movementType = !value ? this.defalt_movement_type : (ScrollRect.MovementType) (object) 2;
        this.OnEndDrag(new PointerEventData(EventSystem.current));
      }
    }

    public bool IsWheelScrollNow => this.IsWheelScroll;

    protected virtual void LateUpdate()
    {
      base.LateUpdate();
      this.UpdateDecelerationRate();
      this.UpdateWheelScrollFlag();
      this.UpdateScroll();
    }

    private void UpdateDecelerationRate()
    {
      this.decelerationRate = !this.IsWheelScroll ? this.DEFAULT_SCROLL_DECELERATION_RATE : this.WHEEL_SCROLL_DECELERATION_RATE;
    }

    private void UpdateWheelScrollFlag()
    {
      if (!this.IsWheelScroll || (double) this.velocity.x != 0.0 || (double) this.velocity.y != 0.0)
        return;
      this.IsWheelScroll = false;
    }

    private void UpdateScroll()
    {
      this.axis_val = Input.GetAxis("Mouse ScrollWheel");
      if ((double) this.axis_val == 0.0)
      {
        this.is_scroll_now = false;
      }
      else
      {
        if (!this.IsHitRayCast())
          return;
        this.rest_add_time = Mathf.Max(0.0f, this.rest_add_time - Time.deltaTime);
        if ((double) this.rest_add_time <= 0.0)
        {
          this.rest_add_time = this.DEFAULT_REST_ADD_TIME;
          this.SetGlideValue(this.axis_val, true);
          this.IsWheelScroll = true;
        }
        else
        {
          this.SetGlideValue(this.axis_val, false);
          this.IsWheelScroll = true;
        }
      }
    }

    private void SetGlideValue(float _axis_value, bool _is_begin)
    {
      float num = !_is_begin ? 0.0f : this.SCROLL_FIXED_VALUE;
      if (this.vertical)
      {
        if ((double) this.axis_val >= 0.0)
          num = (float) (-(double) num + -(double) this.axis_val * (double) this.SCROLL_VALUE_COEF);
        else
          num += -this.axis_val * this.SCROLL_VALUE_COEF;
        this.velocity = new Vector2(this.velocity.x, this.velocity.y + num);
        this.is_scroll_now = true;
      }
      if (!this.horizontal || !this.IS_ENABLE_WHEEL_SCROLL_FOR_HORIZENTAL)
        return;
      this.velocity = new Vector2(this.velocity.x + ((double) this.axis_val < 0.0 ? (float) (-(double) num + (double) this.axis_val * (double) this.SCROLL_VALUE_COEF) : num + this.axis_val * this.SCROLL_VALUE_COEF), this.velocity.y);
      this.is_scroll_now = true;
    }

    private bool IsHitRayCast()
    {
      SRPG_ScrollRect[] componentsInChildren = ((Component) this).GetComponentsInChildren<SRPG_ScrollRect>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (!Object.op_Equality((Object) this, (Object) componentsInChildren[index]) && componentsInChildren[index].IsHitRayCast())
          return false;
      }
      this.raycast_result_list.Clear();
      this.pointer_event = new PointerEventData(EventSystem.current);
      this.pointer_event.position = Vector2.op_Implicit(Input.mousePosition);
      EventSystem.current.RaycastAll(this.pointer_event, this.raycast_result_list);
      if (this.raycast_result_list.Count <= 0)
        return false;
      this.child_objects.Clear();
      foreach (Component componentsInChild in ((Component) this).GetComponentsInChildren<Transform>())
        this.child_objects.Add(componentsInChild.gameObject);
      List<GameObject> childObjects = this.child_objects;
      RaycastResult raycastResult = this.raycast_result_list[0];
      GameObject gameObject = ((RaycastResult) ref raycastResult).gameObject;
      return childObjects.Contains(gameObject);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
      base.OnBeginDrag(eventData);
      if (!this.IsWheelScroll)
        return;
      this.IsWheelScroll = false;
      this.StopMovement();
    }

    public void ResetHorizontalPosition() => this.SetNormalizedPosition(0.0f, 0);

    public void ResetVerticalPosition(bool reverse = false)
    {
      this.SetNormalizedPosition(0.0f, !reverse ? 1 : 0);
    }
  }
}
