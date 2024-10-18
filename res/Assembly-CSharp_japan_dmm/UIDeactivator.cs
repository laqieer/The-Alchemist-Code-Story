﻿// Decompiled with JetBrains decompiler
// Type: UIDeactivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class UIDeactivator : MonoBehaviour
{
  private float mCountDown;

  public UIDeactivator() => this.keyname = "close";

  public string keyname { get; set; }

  private void OnEnable() => this.mCountDown = 0.5f;

  private void OnDisable() => this.mCountDown = 0.5f;

  private void LateUpdate()
  {
    bool flag = false;
    Animator component1 = ((Component) this).GetComponent<Animator>();
    if (Object.op_Inequality((Object) component1, (Object) null) && GameUtility.CompareAnimatorStateName((Component) component1, this.keyname))
    {
      AnimatorStateInfo animatorStateInfo = component1.GetCurrentAnimatorStateInfo(0);
      if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime >= 1.0)
        flag = ((flag ? 1 : 0) | 1) != 0;
    }
    CanvasGroup component2 = ((Component) this).GetComponent<CanvasGroup>();
    if (Object.op_Inequality((Object) component2, (Object) null))
      flag |= (double) component2.alpha <= 0.0;
    if (flag)
    {
      if ((double) this.mCountDown <= 0.0)
        ((Component) this).gameObject.SetActive(false);
      else
        this.mCountDown = Mathf.Max(this.mCountDown - Time.deltaTime, 0.0f);
    }
    else
      this.mCountDown = 0.5f;
  }
}
