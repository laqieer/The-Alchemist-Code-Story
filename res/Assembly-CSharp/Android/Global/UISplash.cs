// Decompiled with JetBrains decompiler
// Type: UISplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class UISplash : MonoBehaviour
{
  public bool IsTapSplash;

  private void Start()
  {
  }

  private void Update()
  {
    if (!this.IsTapSplash)
      return;
    Animator component = this.GetComponent<Animator>();
    if (!((Object) component != (Object) null) || !GameUtility.CompareAnimatorStateName((Component) component, "loop") || !Input.GetMouseButtonDown(0))
      return;
    component.SetBool("close", true);
  }

  private void LateUpdate()
  {
    Animator component = this.GetComponent<Animator>();
    if (!((Object) component != (Object) null))
      return;
    if (this.IsTapSplash)
    {
      if (!GameUtility.CompareAnimatorStateName((Component) component, "close") || (double) component.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
        return;
      UIUtility.PopCanvas(true);
      GameUtility.DestroyGameObject((Component) this.GetComponentInParent<Canvas>());
    }
    else
    {
      if (!GameUtility.CompareAnimatorStateName((Component) component, "open") || (double) component.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
        return;
      GameUtility.SetGameObjectActive(this.gameObject, false);
      GameUtility.DestroyGameObject(this.gameObject);
    }
  }
}
