﻿// Decompiled with JetBrains decompiler
// Type: SRPG.WindowController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(20, "Close", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(5, "Lock", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(6, "Unlock", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(11, "Opened", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(21, "Closed", FlowNode.PinTypes.Output, 11)]
  public class WindowController : UIBehaviour, IFlowInterface
  {
    public WindowController.WindowStateChangeEvent OnWindowStateChange;
    public Transform Body;
    public string StateInt = "st";
    public string StateBool = string.Empty;
    public bool InvertState;
    public bool StartOpened;
    public bool UpdateCollision;
    public bool AutoSwap;
    public bool ToggleCanvas;
    public string OpenedState = "opened";
    public string ClosedState = "closed";
    private bool mHasCanvasStack;
    private bool mDesiredState;
    private Animator mAnimator;
    private bool mOpened;
    private bool mPollState;
    private CanvasGroup mCanvasGroup;
    private bool mSwappedOut;
    private bool mJustOpened;
    private bool mJustClosed;

    public static void OpenIfAvailable(Component c)
    {
      WindowController component = c.GetComponent<WindowController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Open();
    }

    public static void CloseIfAvailable(Component c)
    {
      WindowController component = c.GetComponent<WindowController>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Close();
    }

    private void UpdateAnimator(bool visible)
    {
      if (!string.IsNullOrEmpty(this.StateInt))
        this.mAnimator.SetInteger(this.StateInt, !visible ? 0 : 1);
      else
        this.mAnimator.SetBool(this.StateBool, !this.InvertState ? visible : !visible);
    }

    public bool IsOpened => this.mOpened;

    public bool IsClosed => !this.mOpened;

    public void Open()
    {
      if (this.mDesiredState || this.mOpened)
        return;
      if (this.mSwappedOut)
        this.SwapWindow(true);
      this.mPollState = !this.mDesiredState;
      this.mDesiredState = true;
      this.SetCanvas(true);
      this.SetCollision(false);
      this.UpdateAnimator(true);
    }

    public void Close()
    {
      if (!this.mDesiredState || !this.mOpened)
        return;
      this.mPollState = this.mDesiredState;
      this.mDesiredState = false;
      this.SetCollision(false);
      this.UpdateAnimator(false);
    }

    public void ForceClose()
    {
      if (!this.mDesiredState && !this.mOpened)
        return;
      if (this.mDesiredState && this.mOpened)
      {
        this.Close();
      }
      else
      {
        if (!this.mDesiredState)
          return;
        this.mPollState = true;
        this.mDesiredState = false;
        this.SetCollision(false);
        this.UpdateAnimator(false);
      }
    }

    public void ForceOpen()
    {
      if (this.mDesiredState && this.mOpened)
        return;
      if (!this.mDesiredState && !this.mOpened)
      {
        this.Open();
      }
      else
      {
        if (this.mDesiredState)
          return;
        this.mPollState = false;
        this.mDesiredState = true;
        this.SetCollision(true);
        this.UpdateAnimator(true);
      }
    }

    public bool IsOpening()
    {
      return (this.mDesiredState || this.mOpened) && (!this.mDesiredState || !this.mOpened) && this.mDesiredState;
    }

    public bool IsClosing()
    {
      return (this.mDesiredState || this.mOpened) && (!this.mDesiredState || !this.mOpened) && !this.mDesiredState;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 5:
          this.SetCollision(false);
          break;
        case 6:
          this.SetCollision(true);
          break;
        case 10:
          this.Open();
          break;
        case 20:
          this.Close();
          break;
      }
    }

    private void SwapWindow(bool visible)
    {
      if (!this.AutoSwap || !Object.op_Inequality((Object) this.Body, (Object) null))
        return;
      if (visible)
      {
        this.Body.SetParent(((Component) this).transform, false);
        this.mSwappedOut = false;
      }
      else
      {
        this.Body.SetParent((Transform) UIUtility.Pool, false);
        this.mSwappedOut = true;
      }
    }

    private void SetCanvas(bool visible)
    {
      if (!this.ToggleCanvas)
        return;
      Canvas componentInParent = ((Component) this).GetComponentInParent<Canvas>();
      if (!Object.op_Inequality((Object) componentInParent, (Object) null))
        return;
      ((Behaviour) componentInParent).enabled = visible;
      if (!this.mHasCanvasStack)
        return;
      CanvasStack.SortCanvases();
    }

    public void SetCollision(bool collide)
    {
      if (!this.UpdateCollision || !Object.op_Inequality((Object) this.mCanvasGroup, (Object) null))
        return;
      this.mCanvasGroup.blocksRaycasts = collide;
    }

    protected virtual void Awake()
    {
      base.Awake();
      this.mAnimator = ((Component) this).GetComponentInChildren<Animator>();
      this.mCanvasGroup = ((Component) this).GetComponent<CanvasGroup>();
      this.mHasCanvasStack = Object.op_Inequality((Object) ((Component) this).GetComponent<CanvasStack>(), (Object) null);
      this.SetCollision(false);
      this.mDesiredState = this.StartOpened;
      if (this.StartOpened)
        return;
      this.SetCanvas(false);
    }

    protected virtual void Start()
    {
      base.Start();
      if (this.StartOpened)
      {
        this.UpdateAnimator(true);
        this.mPollState = true;
      }
      else
      {
        this.mPollState = true;
        this.UpdateAnimator(false);
        this.SwapWindow(false);
      }
    }

    protected virtual void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) this.Body, (Object) null) || !this.mSwappedOut)
        return;
      Object.Destroy((Object) ((Component) this.Body).gameObject);
    }

    private void Update()
    {
      if (Object.op_Equality((Object) this.mAnimator, (Object) null) || this.mAnimator.IsInTransition(0))
        return;
      AnimatorStateInfo animatorStateInfo = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      if (!string.IsNullOrEmpty(this.OpenedState))
        this.mJustOpened = ((AnimatorStateInfo) ref animatorStateInfo).IsName(this.OpenedState);
      if (!string.IsNullOrEmpty(this.ClosedState))
        this.mJustClosed = ((AnimatorStateInfo) ref animatorStateInfo).IsName(this.ClosedState);
      if (this.mPollState && this.mJustOpened && this.mDesiredState)
      {
        this.mPollState = false;
        this.mOpened = true;
        this.mJustOpened = false;
        this.SetCollision(true);
        if (this.OnWindowStateChange != null)
          this.OnWindowStateChange(((Component) this).gameObject, true);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      }
      else
      {
        if (!this.mPollState || !this.mJustClosed || this.mDesiredState)
          return;
        this.mPollState = false;
        this.mOpened = false;
        this.mJustClosed = false;
        if (this.OnWindowStateChange != null)
          this.OnWindowStateChange(((Component) this).gameObject, false);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
        this.SwapWindow(false);
        this.SetCanvas(false);
      }
    }

    public void OnOpen()
    {
      if (!string.IsNullOrEmpty(this.OpenedState))
        return;
      this.mJustOpened = true;
    }

    public void OnClose()
    {
      if (!string.IsNullOrEmpty(this.ClosedState))
        return;
      this.mJustClosed = true;
    }

    public delegate void WindowStateChangeEvent(GameObject go, bool visible);
  }
}