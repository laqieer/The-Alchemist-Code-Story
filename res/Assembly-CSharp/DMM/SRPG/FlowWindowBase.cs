// Decompiled with JetBrains decompiler
// Type: SRPG.FlowWindowBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class FlowWindowBase
  {
    private FlowWindowController m_Controller;
    protected GameObject m_Window;
    protected Animator m_Animator;
    protected FlowWindowBase.AnimState m_AnimState;
    protected bool m_StartUp;
    protected List<int> m_Request = new List<int>();
    protected bool m_AnimStart;
    protected int m_FrameCount;
    protected List<IEnumerator> m_TaskReq = new List<IEnumerator>();
    protected IEnumerator m_Task;

    public virtual string name => "FriendPresentWindowBase";

    public bool isValid => UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null);

    public bool isOpen => this.m_AnimState == FlowWindowBase.AnimState.OPEN;

    public bool isClose => this.m_AnimState == FlowWindowBase.AnimState.CLOSE;

    public bool isOpened => this.m_AnimState == FlowWindowBase.AnimState.OPENED;

    public bool isClosed => this.m_AnimState == FlowWindowBase.AnimState.CLOSED;

    public virtual void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      this.m_Request.Clear();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) param.window, (UnityEngine.Object) null))
        return;
      this.m_Window = param.window;
      this.m_Animator = param.window.GetComponent<Animator>();
      this.m_Window.SetActive(true);
    }

    public virtual void Release()
    {
      this.ClearTask();
      this.m_Request.Clear();
    }

    public virtual void Start() => this.StartUp();

    public void Update(FlowWindowController controller)
    {
      this.m_Controller = controller;
      if (this.UpdateTask())
        return;
      if (this.m_Request.Count > 0)
      {
        for (int index = 0; index < this.m_Request.Count; ++index)
        {
          int pinId = this.OnActivate(this.m_Request[index]);
          if (pinId != -1)
            controller.ActivateOutputLinks(pinId);
        }
        this.m_Request.Clear();
      }
      int pinId1 = this.Update();
      if (pinId1 != -1)
        controller.ActivateOutputLinks(pinId1);
      this.m_Controller = (FlowWindowController) null;
    }

    public void LateUpdate(FlowNode flowNode)
    {
    }

    public virtual int Update()
    {
      if (this.m_AnimState == FlowWindowBase.AnimState.OPEN)
      {
        if (this.m_AnimStart && this.m_FrameCount != Time.frameCount)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
            this.m_Animator.SetBool("close", false);
          this.m_AnimStart = false;
          this.m_FrameCount = 0;
        }
        if (this.IsState("opened"))
        {
          int pinId = this.OnOpened();
          if (pinId != -1)
            this.m_Controller.ActivateOutputLinks(pinId);
          this.m_AnimState = FlowWindowBase.AnimState.OPENED;
        }
      }
      else if (this.m_AnimState == FlowWindowBase.AnimState.CLOSE && this.IsState("closed"))
      {
        int pinId = this.OnClosed();
        if (pinId != -1)
          this.m_Controller.ActivateOutputLinks(pinId);
        this.m_AnimState = FlowWindowBase.AnimState.CLOSED;
      }
      return -1;
    }

    public void Open()
    {
      this.m_AnimState = FlowWindowBase.AnimState.OPEN;
      this.m_AnimStart = true;
      this.m_FrameCount = Time.frameCount;
      this.SetActiveChild(true);
    }

    public void Close(bool immidiate = false)
    {
      this.m_AnimState = FlowWindowBase.AnimState.CLOSE;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
        return;
      this.m_Animator.SetBool("close", true);
      if (!immidiate)
        return;
      this.m_Animator.Play("closed");
      this.SetActiveChild(false);
    }

    private void ClearTask()
    {
      this.m_TaskReq.Clear();
      this.m_Task = (IEnumerator) null;
    }

    protected void AddTask(IEnumerator enumrator) => this.m_TaskReq.Add(enumrator);

    public bool UpdateTask()
    {
      if (this.m_TaskReq.Count == 0)
        return false;
      for (int index = 0; index < this.m_TaskReq.Count; ++index)
      {
        IEnumerator enumerator = this.m_TaskReq[index];
        if (enumerator == null || !enumerator.MoveNext())
        {
          this.m_TaskReq.RemoveAt(index);
          --index;
        }
      }
      return true;
    }

    public void StartUp() => this.m_StartUp = true;

    public void SetActiveChild(GameObject root, bool value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
        return;
      Transform transform = root.transform;
      for (int index = 0; index < transform.childCount; ++index)
      {
        Transform child = transform.GetChild(index);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
          ((Component) child).gameObject.SetActive(value);
      }
    }

    public void SetActiveChild(bool value) => this.SetActiveChild(this.m_Window, value);

    public GameObject GetChild(string name) => this.GetChild(this.m_Window, name);

    public GameObject GetChild(GameObject root, string name)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
      {
        if (((UnityEngine.Object) root).name == name)
          return root;
        Transform transform = root.transform;
        for (int index = 0; index < transform.childCount; ++index)
        {
          Transform child = transform.GetChild(index);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null) && ((UnityEngine.Object) child).name == name)
            return ((Component) child).gameObject;
        }
      }
      return (GameObject) null;
    }

    public GameObject GetChildAll(string name) => this.GetChildAll(this.m_Window, name);

    public GameObject GetChildAll(GameObject root, string name)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
      {
        if (((UnityEngine.Object) root).name == name)
          return root;
        Transform transform = root.transform;
        List<GameObject> gameObjectList = new List<GameObject>();
        for (int index = 0; index < transform.childCount; ++index)
        {
          Transform child = transform.GetChild(index);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
          {
            if (((UnityEngine.Object) child).name == name)
              return ((Component) child).gameObject;
            gameObjectList.Add(((Component) child).gameObject);
          }
        }
        for (int index = 0; index < gameObjectList.Count; ++index)
        {
          GameObject child = this.GetChild(gameObjectList[index], name);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
            return child;
        }
      }
      return (GameObject) null;
    }

    public T GetChildComponent<T>(GameObject root, string name) where T : Component
    {
      GameObject child = this.GetChild(root, name);
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null) ? child.GetComponent<T>() : (T) null;
    }

    public T GetChildComponent<T>(string name) where T : Component
    {
      GameObject child = this.GetChild(name);
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null) ? child.GetComponent<T>() : (T) null;
    }

    public T GetChildAllComponent<T>(string name) where T : Component
    {
      GameObject childAll = this.GetChildAll(name);
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) childAll, (UnityEngine.Object) null) ? childAll.GetComponent<T>() : (T) null;
    }

    public bool IsStartUp() => this.m_StartUp;

    public bool IsState(string stateName)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Animator.runtimeAnimatorController, (UnityEngine.Object) null) || this.m_Animator.runtimeAnimatorController.animationClips == null || this.m_Animator.runtimeAnimatorController.animationClips.Length == 0)
        return true;
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      return ((AnimatorStateInfo) ref animatorStateInfo).IsName(stateName);
    }

    public void RequestPin(int pinId) => this.m_Request.Add(pinId);

    public virtual int OnActivate(int pinId) => -1;

    protected virtual int OnOpened() => -1;

    protected virtual int OnClosed() => -1;

    public enum AnimState
    {
      IDEL,
      OPEN,
      CLOSE,
      OPENED,
      CLOSED,
    }

    [Serializable]
    public class SerializeParamBase
    {
      public GameObject window;

      public virtual System.Type type => typeof (FlowWindowBase);
    }
  }
}
