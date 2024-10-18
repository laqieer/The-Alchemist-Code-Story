// Decompiled with JetBrains decompiler
// Type: UIWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CanvasGroup))]
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
      if (!((Component) this).gameObject.activeSelf)
        return false;
      AnimatorStateInfo animatorStateInfo = ((Component) this).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
      return ((AnimatorStateInfo) ref animatorStateInfo).IsName(this.CloseState) && (double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime >= 1.0;
    }
  }

  public bool IsOpened
  {
    get
    {
      if (!((Component) this).gameObject.activeSelf)
        return false;
      AnimatorStateInfo animatorStateInfo = ((Component) this).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
      return ((AnimatorStateInfo) ref animatorStateInfo).IsName(this.WaitState);
    }
  }

  public static bool CheckOpened(GameObject obj)
  {
    UIWindow component = obj.GetComponent<UIWindow>();
    if (Object.op_Inequality((Object) component, (Object) null))
      return component.IsOpened;
    Debug.LogError((object) (obj.ToString() + " has no UIWindow component."));
    return false;
  }

  public static bool CheckClosed(GameObject obj)
  {
    UIWindow component = obj.GetComponent<UIWindow>();
    if (Object.op_Inequality((Object) component, (Object) null))
      return component.IsClosed;
    Debug.LogError((object) (obj.ToString() + " has no UIWindow component."));
    return false;
  }

  public void Open()
  {
    bool activeInHierarchy = ((Component) this).gameObject.activeInHierarchy;
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
      ((Component) this).GetComponent<CanvasGroup>().blocksRaycasts = false;
      ((Component) this).gameObject.SetActive(true);
      this.mUpdateAnimatorState = true;
    }
  }

  public void Close()
  {
    if (!((Component) this).gameObject.activeInHierarchy || this.mClose)
      return;
    ((Component) this).GetComponent<CanvasGroup>().blocksRaycasts = false;
    this.mClose = true;
    this.UpdateAnimatorState();
  }

  private void UpdateAnimatorState()
  {
    ((Component) this).GetComponent<Animator>().SetBool("close", this.mClose);
    this.mWaitForAnimatorStateChange = true;
  }

  private void Awake()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
    {
      this.mClose = true;
    }
    else
    {
      ((Component) this).GetComponent<CanvasGroup>().blocksRaycasts = false;
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
        ((Component) this).gameObject.SetActive(false);
        this.mWaitForAnimatorStateChange = false;
        if (this.OnWindowClose == null)
          return;
        this.OnWindowClose(this);
      }
      else
      {
        if (!this.IsOpened)
          return;
        ((Component) this).GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.mWaitForAnimatorStateChange = false;
        if (this.OnWindowOpen == null)
          return;
        this.OnWindowOpen(this);
      }
    }
  }

  public delegate void WindowEvent(UIWindow window);
}
