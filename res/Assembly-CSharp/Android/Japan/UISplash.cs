// Decompiled with JetBrains decompiler
// Type: UISplash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UISplash : MonoBehaviour
{
  public bool IsTapSplash;

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
