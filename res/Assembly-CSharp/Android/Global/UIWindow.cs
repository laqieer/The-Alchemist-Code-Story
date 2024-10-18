// Decompiled with JetBrains decompiler
// Type: UIWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (CanvasGroup))]
[RequireComponent(typeof (Animator))]
[AddComponentMenu("UI/Window")]
public class UIWindow : MonoBehaviour
{
  public string OpenState = "open";
  public string WaitState = "loop";
  public string CloseState = "close";
  private bool mClose;
  private bool mUpdateAnimatorState;
  private bool mWaitForAnimatorStateChange;
  public UIWindow.WindowEvent OnWindowClose;
  public UIWindow.WindowEvent OnWindowOpen;

  public bool IsClosed
  {
    get
    {
      if (!this.gameObject.activeSelf)
        return false;
      AnimatorStateInfo animatorStateInfo = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
      if (animatorStateInfo.IsName(this.CloseState))
        return (double) animatorStateInfo.normalizedTime >= 1.0;
      return false;
    }
  }

  public bool IsOpened
  {
    get
    {
      if (!this.gameObject.activeSelf)
        return false;
      return this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(this.WaitState);
    }
  }

  public static bool CheckOpened(GameObject obj)
  {
    UIWindow component = obj.GetComponent<UIWindow>();
    if ((Object) component != (Object) null)
      return component.IsOpened;
    Debug.LogError((object) (obj.ToString() + " has no UIWindow component."));
    return false;
  }

  public static bool CheckClosed(GameObject obj)
  {
    UIWindow component = obj.GetComponent<UIWindow>();
    if ((Object) component != (Object) null)
      return component.IsClosed;
    Debug.LogError((object) (obj.ToString() + " has no UIWindow component."));
    return false;
  }

  public void Open()
  {
    bool activeInHierarchy = this.gameObject.activeInHierarchy;
    if (!this.mClose)
      return;
    this.mClose = false;
    this.mWaitForAnimatorStateChange = true;
    if (activeInHierarchy)
    {
      this.UpdateAnimatorState();
    }
    else
    {
      this.GetComponent<CanvasGroup>().blocksRaycasts = false;
      this.gameObject.SetActive(true);
      this.mUpdateAnimatorState = true;
    }
  }

  public void Close()
  {
    if (!this.gameObject.activeInHierarchy || this.mClose)
      return;
    this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    this.mClose = true;
    this.UpdateAnimatorState();
  }

  private void UpdateAnimatorState()
  {
    this.GetComponent<Animator>().SetBool("close", this.mClose);
    this.mWaitForAnimatorStateChange = true;
  }

  private void Awake()
  {
    if (!this.gameObject.activeInHierarchy)
    {
      this.mClose = true;
    }
    else
    {
      this.GetComponent<CanvasGroup>().blocksRaycasts = false;
      this.mWaitForAnimatorStateChange = true;
    }
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
  }

  private void Update()
  {
    if (this.mUpdateAnimatorState)
    {
      this.mUpdateAnimatorState = false;
      this.UpdateAnimatorState();
    }
    else
    {
      if (!this.mWaitForAnimatorStateChange)
        return;
      if (this.mClose)
      {
        if (!this.IsClosed)
          return;
        this.gameObject.SetActive(false);
        this.mWaitForAnimatorStateChange = false;
        if (this.OnWindowClose == null)
          return;
        this.OnWindowClose(this);
      }
      else
      {
        if (!this.IsOpened)
          return;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.mWaitForAnimatorStateChange = false;
        if (this.OnWindowOpen == null)
          return;
        this.OnWindowOpen(this);
      }
    }
  }

  public delegate void WindowEvent(UIWindow window);
}
