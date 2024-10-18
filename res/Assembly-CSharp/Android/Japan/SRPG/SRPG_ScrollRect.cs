// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ScrollRect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    private List<GameObject> child_objects = new List<GameObject>();
    private List<RaycastResult> raycast_result_list = new List<RaycastResult>();
    private PointerEventData pointer_event;
    private ScrollRect.MovementType defalt_movement_type;
    private float axis_val;
    private float rest_add_time;
    private bool is_scroll_now;
    private bool is_wheel_scroll;

    protected override void Awake()
    {
      base.Awake();
      if ((UnityEngine.Object) this.verticalScrollbar != (UnityEngine.Object) null)
        this.verticalScrollbar.value = 1f;
      this.defalt_movement_type = this.movementType;
    }

    public bool IsScrollNow
    {
      get
      {
        return this.is_scroll_now;
      }
    }

    private bool IsWheelScroll
    {
      get
      {
        return this.is_wheel_scroll;
      }
      set
      {
        this.is_wheel_scroll = value;
        if (this.movementType != ScrollRect.MovementType.Unrestricted)
          this.movementType = !value ? this.defalt_movement_type : ScrollRect.MovementType.Clamped;
        this.OnEndDrag(new PointerEventData(EventSystem.current));
      }
    }

    public bool IsWheelScrollNow
    {
      get
      {
        return this.IsWheelScroll;
      }
    }

    protected override void LateUpdate()
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
      SRPG_ScrollRect[] componentsInChildren = this.GetComponentsInChildren<SRPG_ScrollRect>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (!((UnityEngine.Object) this == (UnityEngine.Object) componentsInChildren[index]) && componentsInChildren[index].IsHitRayCast())
          return false;
      }
      this.raycast_result_list.Clear();
      this.pointer_event = new PointerEventData(EventSystem.current);
      this.pointer_event.position = (Vector2) Input.mousePosition;
      EventSystem.current.RaycastAll(this.pointer_event, this.raycast_result_list);
      if (this.raycast_result_list.Count <= 0)
        return false;
      this.child_objects.Clear();
      foreach (Component componentsInChild in this.GetComponentsInChildren<Transform>())
        this.child_objects.Add(componentsInChild.gameObject);
      return this.child_objects.Contains(this.raycast_result_list[0].gameObject);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
      base.OnBeginDrag(eventData);
      if (!this.IsWheelScroll)
        return;
      this.IsWheelScroll = false;
      this.StopMovement();
    }

    public void ResetHorizontalPosition()
    {
      this.SetNormalizedPosition(0.0f, 0);
    }

    public void ResetVerticalPosition()
    {
      this.SetNormalizedPosition(0.0f, 1);
    }
  }
}
