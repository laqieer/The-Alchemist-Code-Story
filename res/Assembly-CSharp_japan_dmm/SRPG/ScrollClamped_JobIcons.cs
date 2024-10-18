// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_JobIcons
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ScrollClamped_JobIcons : MonoBehaviour, ScrollListSetUp
  {
    private float FRAME_OUT_POSITION_VALUE = 100000f;
    private float JOB_ICON_DISP_EFFECT_TIME = 0.1f;
    private float MIN_SCALE_DOUBLE = 0.65f;
    private float MIN_SCALE_SINGLE = 0.75f;
    private float VELOCITY_MAX = 800f;
    private float AUTOFIT_BEGIN_VELOCITY = 200f;
    public float Space = 1f;
    public int Max;
    public RectTransform ViewObj;
    public ScrollAutoFit AutoFit;
    public Button back;
    private JobIconScrollListController mController;
    private float mOffset;
    private float mStartPos;
    private float mCenter;
    private int mSelectIdx;
    private bool mIsSelected;
    private bool mIsImmediateFocusNow;
    private float mDefaultAutoFitTime;
    private WindowController mWindowController;
    private bool mIsNeedAutoFit;
    private bool mIsPreDragging;
    private bool mIsFocusNow;
    private Vector2 mDragStartLocalPosition;
    private Vector3 mDefaultViewportLocalPosition;
    private float mJobIconDispEffectTime;
    public ScrollClamped_JobIcons.FrameOutItem OnFrameOutItem;

    private WindowController WinController
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mWindowController, (UnityEngine.Object) null))
          this.mWindowController = ((Component) this).GetComponentInParent<WindowController>();
        return this.mWindowController;
      }
    }

    private bool IsNeedAutoFit
    {
      get => this.mIsNeedAutoFit;
      set
      {
        this.mIsNeedAutoFit = value;
        if (!this.mIsNeedAutoFit || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WinController, (UnityEngine.Object) null))
          return;
        this.WinController.SetCollision(false);
      }
    }

    public void Start()
    {
      this.mDefaultAutoFitTime = this.AutoFit.FitTime;
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      Rect rect = this.ViewObj.rect;
      this.mCenter = ((Rect) ref rect).width * 0.5f - component.anchoredPosition.x;
      // ISSUE: method pointer
      this.AutoFit.OnScrollStop.AddListener(new UnityAction((object) this, __methodptr(OnScrollStop)));
      // ISSUE: method pointer
      this.mController.OnItemPositionAreaOver.AddListener(new UnityAction<GameObject>((object) this, __methodptr(OnItemPositionAreaOver)));
    }

    private void Update()
    {
      this.UpdateIsNeedAutoFitFlagByDrag();
      this.UpdateIsNeedAutoFitFlagByWheel();
      if (this.AutoFit.CurrentState == ScrollAutoFit.State.Wait)
        this.mIsFocusNow = false;
      if (this.mIsImmediateFocusNow)
      {
        this.ImmediateFocus();
      }
      else
      {
        if (!this.IsNeedAutoFit)
          return;
        this.CheckNeedExecAutoFocus();
      }
    }

    private void CheckNeedExecAutoFocus()
    {
      this.AutoFit.velocity = new Vector2(Mathf.Clamp(this.AutoFit.velocity.x, -this.VELOCITY_MAX, this.VELOCITY_MAX), 0.0f);
      if ((double) Mathf.Abs(this.AutoFit.velocity.x) > (double) this.AUTOFIT_BEGIN_VELOCITY)
        return;
      List<GameObject> objects = new List<GameObject>();
      foreach (Component componentsInChild in ((Component) this).GetComponentsInChildren<UnitInventoryJobIcon>())
        objects.Add(componentsInChild.gameObject);
      GameObject gameObject = this.Focus(objects, focus_time: 0.1f);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        UnitInventoryJobIcon component = gameObject.GetComponent<UnitInventoryJobIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !component.IsDisabledBaseJobIcon())
          UnitEnhanceV3.Instance.OnJobSlotClick(((Component) component.BaseJobIconButton).gameObject);
      }
      this.IsNeedAutoFit = false;
    }

    private void ImmediateFocus()
    {
      if (this.AutoFit.CurrentState != ScrollAutoFit.State.Wait)
        return;
      this.mJobIconDispEffectTime -= Time.deltaTime;
      if ((double) this.mJobIconDispEffectTime > 0.0)
        return;
      this.mIsImmediateFocusNow = false;
      ((Component) this).transform.parent.localPosition = this.mDefaultViewportLocalPosition;
      this.IsNeedAutoFit = false;
    }

    private void UpdateIsNeedAutoFitFlagByWheel()
    {
      if (!this.AutoFit.IsWheelScrollNow)
        return;
      this.IsNeedAutoFit = true;
    }

    private void UpdateIsNeedAutoFitFlagByDrag()
    {
      if (!this.mIsPreDragging && this.AutoFit.IsDrag)
      {
        this.mDragStartLocalPosition = Vector2.op_Implicit(((Component) this).transform.localPosition);
        this.mIsPreDragging = this.AutoFit.IsDrag;
      }
      else
      {
        if (!this.mIsPreDragging || this.AutoFit.IsDrag)
          return;
        if (!this.mIsFocusNow && (double) Mathf.Abs(this.mDragStartLocalPosition.x - ((Component) this).transform.localPosition.x) >= 1.0)
          this.IsNeedAutoFit = true;
        this.mIsPreDragging = this.AutoFit.IsDrag;
      }
    }

    private void OnScrollStop()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WinController, (UnityEngine.Object) null))
        return;
      this.WinController.SetCollision(true);
    }

    public void OnSetUpItems()
    {
      this.mController = ((Component) this).GetComponent<JobIconScrollListController>();
      // ISSUE: method pointer
      this.mController.OnItemUpdate.AddListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      // ISSUE: method pointer
      this.mController.OnUpdateItemEvent.AddListener(new UnityAction<List<JobIconScrollListController.ItemData>>((object) this, __methodptr(OnUpdateScale)));
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      ((UnityEngine.Object) obj).name = idx.ToString();
      if (this.OnFrameOutItem == null)
        return;
      this.OnFrameOutItem(obj, idx);
    }

    public void OnItemPositionAreaOver(GameObject obj) => this.AutoFit.velocity = Vector2.zero;

    public void OnUpdateScale(List<JobIconScrollListController.ItemData> items)
    {
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      Rect rect = this.ViewObj.rect;
      float num1 = ((Rect) ref rect).width * 0.5f;
      this.mCenter = num1 - component.anchoredPosition.x;
      for (int index = 0; index < items.Count; ++index)
      {
        float num2 = !items[index].job_icon.IsSingleIcon ? this.MIN_SCALE_DOUBLE : this.MIN_SCALE_SINGLE;
        float num3 = items[index].position.x + component.anchoredPosition.x - num1;
        float num4 = Mathf.Clamp((float) (1.0 - (double) Mathf.Abs(num3) / (double) num1), 0.0f, 1f);
        float num5 = num2 + (1f - num2) * num4;
        items[index].gameObject.transform.localScale = new Vector3(num5, num5, num5);
        float num6 = items[index].job_icon.HalfWidth * (1f - num5);
        if ((double) num3 > 0.0)
          items[index].rectTransform.anchoredPosition = new Vector2(items[index].position.x - num6, items[index].position.y);
        if ((double) num3 < 0.0)
          items[index].rectTransform.anchoredPosition = new Vector2(items[index].position.x + num6, items[index].position.y);
      }
    }

    public void OnClick(GameObject obj) => this.Focus(obj);

    public GameObject Focus(
      List<GameObject> objects,
      bool is_immediate = false,
      bool is_hide = false,
      float focus_time = 0.0f)
    {
      if (objects.Count <= 0)
        return (GameObject) null;
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      Rect rect = this.ViewObj.rect;
      this.mCenter = ((Rect) ref rect).width * 0.5f - component.anchoredPosition.x;
      float num1 = float.MaxValue;
      GameObject gameObject = objects[0];
      for (int index = 0; index < objects.Count; ++index)
      {
        float num2 = Mathf.Abs(this.mCenter - (objects[index].transform as RectTransform).anchoredPosition.x);
        if ((double) num1 >= (double) num2)
        {
          num1 = num2;
          gameObject = objects[index];
        }
      }
      this.Focus(gameObject, is_immediate, is_hide, focus_time);
      return gameObject;
    }

    public void Focus(GameObject obj, bool is_immediate = false, bool is_hide = false, float focus_time = 0.0f)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      float num = this.mDefaultAutoFitTime;
      if ((double) focus_time > 0.0)
        num = focus_time;
      if (is_immediate)
      {
        num = 0.0f;
        if (is_hide)
        {
          this.mIsImmediateFocusNow = true;
          if (Vector3.op_Equality(this.mDefaultViewportLocalPosition, Vector3.zero))
            this.mDefaultViewportLocalPosition = ((Component) this).transform.parent.localPosition;
          ((Component) this).transform.parent.localPosition = new Vector3(0.0f, this.FRAME_OUT_POSITION_VALUE, 0.0f);
          this.mJobIconDispEffectTime = this.JOB_ICON_DISP_EFFECT_TIME;
        }
      }
      RectTransform component = ((Component) this).gameObject.GetComponent<RectTransform>();
      int index = this.mController.Items.FindIndex((Predicate<JobIconScrollListController.ItemData>) (data => ((UnityEngine.Object) data.gameObject).GetInstanceID() == ((UnityEngine.Object) obj).GetInstanceID()));
      if (index == -1)
        return;
      this.AutoFit.FitTime = num;
      this.AutoFit.SetScrollToHorizontal(component.anchoredPosition.x - (this.mController.Items[index].position.x - this.mCenter));
      if (is_immediate)
        this.AutoFit.Step();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.back, (UnityEngine.Object) null))
        ((Selectable) this.back).interactable = false;
      this.mIsFocusNow = true;
    }

    public delegate void FrameOutItem(GameObject target, int index);
  }
}
