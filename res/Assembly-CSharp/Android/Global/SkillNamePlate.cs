// Decompiled with JetBrains decompiler
// Type: SkillNamePlate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

public class SkillNamePlate : MonoBehaviour
{
  public string EndStateTrigger = "open";
  public string HideStateTrigger = "hide";
  public string EndStateName = "close";
  public Text SkillName;
  public ImageArray SkillBgHead;
  public ImageArray SkillBgBody;
  public ImageArray SkillAttackType;
  public ImageArray SkillElement;
  public ImageArray SkillAttackDetail;
  private Animator mAnimator;
  private float mSpeed;
  public bool mClose;

  public void SetSkillName(string Name, EUnitSide side, EElement element = EElement.None, AttackDetailTypes ad_type = AttackDetailTypes.None, AttackTypes atk_type = AttackTypes.None)
  {
    if ((bool) ((UnityEngine.Object) this.SkillName))
      this.SkillName.text = Name;
    if ((bool) ((UnityEngine.Object) this.SkillBgHead))
    {
      int num = (int) side;
      if (num >= 0 && num < this.SkillBgHead.Images.Length)
        this.SkillBgHead.ImageIndex = num;
    }
    if ((bool) ((UnityEngine.Object) this.SkillBgBody))
    {
      int num = (int) side;
      if (num >= 0 && num < this.SkillBgBody.Images.Length)
        this.SkillBgBody.ImageIndex = num;
    }
    if ((bool) ((UnityEngine.Object) this.SkillAttackType))
    {
      if (atk_type != AttackTypes.None)
      {
        int num = (int) atk_type;
        if (num >= 0 && num < this.SkillAttackType.Images.Length)
          this.SkillAttackType.ImageIndex = num;
      }
      this.SkillAttackType.gameObject.SetActive(atk_type != AttackTypes.None);
    }
    if ((bool) ((UnityEngine.Object) this.SkillElement))
    {
      if (element != EElement.None)
      {
        int num = (int) element;
        if (num >= 0 && num < this.SkillElement.Images.Length)
          this.SkillElement.ImageIndex = num;
      }
      this.SkillElement.gameObject.SetActive(element != EElement.None);
    }
    if (!(bool) ((UnityEngine.Object) this.SkillAttackDetail))
      return;
    if (ad_type != AttackDetailTypes.None)
    {
      int num = (int) ad_type;
      if (num >= 0 && num < this.SkillAttackDetail.Images.Length)
        this.SkillAttackDetail.ImageIndex = num;
    }
    this.SkillAttackDetail.gameObject.SetActive(ad_type != AttackDetailTypes.None);
  }

  public void Open(float speed = 1f)
  {
    if (!(bool) ((UnityEngine.Object) this.mAnimator))
      return;
    this.gameObject.SetActive(true);
    this.mAnimator.SetBool(this.EndStateTrigger, true);
    this.mAnimator.SetBool(this.HideStateTrigger, false);
    this.mSpeed = speed;
    this.mClose = false;
  }

  public void Close()
  {
    this.mClose = true;
  }

  private void Start()
  {
    this.mAnimator = this.GetComponent<Animator>();
    this.mSpeed = 1f;
    this.mClose = true;
    if ((bool) ((UnityEngine.Object) this.SkillElement))
      this.SkillElement.gameObject.SetActive(false);
    if (!(bool) ((UnityEngine.Object) this.SkillAttackDetail))
      return;
    this.SkillAttackDetail.gameObject.SetActive(false);
  }

  private void Update()
  {
    if (this.mClose)
      this.mAnimator.SetBool(this.EndStateTrigger, false);
    if (this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.EndStateTrigger))
    {
      this.mAnimator.speed = this.mSpeed;
    }
    else
    {
      this.mSpeed = 1f;
      this.mAnimator.speed = 1f;
    }
    if (!this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName(this.EndStateName) || this.mAnimator.IsInTransition(0) || (double) this.mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
      return;
    this.mAnimator.SetBool(this.HideStateTrigger, true);
    this.gameObject.SetActive(false);
  }

  public bool IsClosed()
  {
    return this.mClose && (!this.gameObject.activeSelf || this.mAnimator.GetCurrentAnimatorStateInfo(0).IsName("closed") && !this.mAnimator.IsInTransition(0) && (double) this.mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0);
  }
}
