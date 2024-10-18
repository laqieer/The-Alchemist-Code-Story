// Decompiled with JetBrains decompiler
// Type: UISplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class UISplash : MonoBehaviour
{
  public bool IsTapSplash;

  private void Update()
  {
    if (!this.IsTapSplash)
      return;
    Animator component = ((Component) this).GetComponent<Animator>();
    if (!Object.op_Inequality((Object) component, (Object) null) || !GameUtility.CompareAnimatorStateName((Component) component, "loop") || !Input.GetMouseButtonDown(0))
      return;
    component.SetBool("close", true);
  }

  private void LateUpdate()
  {
    Animator component = ((Component) this).GetComponent<Animator>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    if (this.IsTapSplash)
    {
      if (!GameUtility.CompareAnimatorStateName((Component) component, "close"))
        return;
      AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
      if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
        return;
      UIUtility.PopCanvas(true);
      GameUtility.DestroyGameObject((Component) ((Component) this).GetComponentInParent<Canvas>());
    }
    else
    {
      if (!GameUtility.CompareAnimatorStateName((Component) component, "open"))
        return;
      AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
      if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
        return;
      GameUtility.SetGameObjectActive(((Component) this).gameObject, false);
      GameUtility.DestroyGameObject(((Component) this).gameObject);
    }
  }
}
