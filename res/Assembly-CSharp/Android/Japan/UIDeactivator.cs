// Decompiled with JetBrains decompiler
// Type: UIDeactivator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIDeactivator : MonoBehaviour
{
  private float mCountDown;

  public UIDeactivator()
  {
    this.keyname = "close";
  }

  public string keyname { get; set; }

  private void OnEnable()
  {
    this.mCountDown = 0.5f;
  }

  private void OnDisable()
  {
    this.mCountDown = 0.5f;
  }

  private void LateUpdate()
  {
    bool flag = false;
    Animator component1 = this.GetComponent<Animator>();
    if ((Object) component1 != (Object) null && GameUtility.CompareAnimatorStateName((Component) component1, this.keyname) && (double) component1.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0)
      flag = ((flag ? 1 : 0) | 1) != 0;
    CanvasGroup component2 = this.GetComponent<CanvasGroup>();
    if ((Object) component2 != (Object) null)
      flag |= (double) component2.alpha <= 0.0;
    if (flag)
    {
      if ((double) this.mCountDown <= 0.0)
        this.gameObject.SetActive(false);
      else
        this.mCountDown = Mathf.Max(this.mCountDown - Time.deltaTime, 0.0f);
    }
    else
      this.mCountDown = 0.5f;
  }
}
