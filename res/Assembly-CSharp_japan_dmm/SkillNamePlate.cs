// Decompiled with JetBrains decompiler
// Type: SkillNamePlate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
  private float mDispTime;
  public bool mClose;

  public void SetSkillName(
    string Name,
    EUnitSide side,
    EElement element = EElement.None,
    AttackDetailTypes ad_type = AttackDetailTypes.None,
    AttackTypes atk_type = AttackTypes.None)
  {
    if (Object.op_Implicit((Object) this.SkillName))
      this.SkillName.text = Name;
    if (Object.op_Implicit((Object) this.SkillBgHead))
    {
      int num = (int) side;
      if (num >= 0 && num < this.SkillBgHead.Images.Length)
        this.SkillBgHead.ImageIndex = num;
    }
    if (Object.op_Implicit((Object) this.SkillBgBody))
    {
      int num = (int) side;
      if (num >= 0 && num < this.SkillBgBody.Images.Length)
        this.SkillBgBody.ImageIndex = num;
    }
    if (Object.op_Implicit((Object) this.SkillAttackType))
    {
      if (atk_type != AttackTypes.None)
      {
        int num = (int) atk_type;
        if (num >= 0 && num < this.SkillAttackType.Images.Length)
          this.SkillAttackType.ImageIndex = num;
      }
      ((Component) this.SkillAttackType).gameObject.SetActive(atk_type != AttackTypes.None);
    }
    if (Object.op_Implicit((Object) this.SkillElement))
    {
      if (element != EElement.None)
      {
        int num = (int) element;
        if (num >= 0 && num < this.SkillElement.Images.Length)
          this.SkillElement.ImageIndex = num;
      }
      ((Component) this.SkillElement).gameObject.SetActive(element != EElement.None);
    }
    if (!Object.op_Implicit((Object) this.SkillAttackDetail))
      return;
    if (ad_type != AttackDetailTypes.None)
    {
      int num = (int) ad_type;
      if (num >= 0 && num < this.SkillAttackDetail.Images.Length)
        this.SkillAttackDetail.ImageIndex = num;
    }
    ((Component) this.SkillAttackDetail).gameObject.SetActive(ad_type != AttackDetailTypes.None);
  }

  public void Open(float speed = 1f, float disp_time = 0.0f)
  {
    if (!Object.op_Implicit((Object) this.mAnimator))
      return;
    ((Component) this).gameObject.SetActive(true);
    this.mAnimator.SetBool(this.EndStateTrigger, true);
    this.mAnimator.SetBool(this.HideStateTrigger, false);
    this.mSpeed = speed;
    this.mDispTime = disp_time;
    this.mClose = false;
  }

  public void Close() => this.mClose = true;

  private void Start()
  {
    this.mAnimator = ((Component) this).GetComponentInChildren<Animator>();
    this.mSpeed = 1f;
    this.mClose = true;
    if (Object.op_Implicit((Object) this.SkillElement))
      ((Component) this.SkillElement).gameObject.SetActive(false);
    if (!Object.op_Implicit((Object) this.SkillAttackDetail))
      return;
    ((Component) this.SkillAttackDetail).gameObject.SetActive(false);
  }

  private void Update()
  {
    if ((double) this.mDispTime > 0.0)
    {
      this.mDispTime -= Time.deltaTime;
      if ((double) this.mDispTime <= 0.0)
      {
        this.mDispTime = 0.0f;
        this.mClose = true;
      }
    }
    if (this.mClose)
      this.mAnimator.SetBool(this.EndStateTrigger, false);
    AnimatorStateInfo animatorStateInfo1 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
    if (((AnimatorStateInfo) ref animatorStateInfo1).IsName(this.EndStateTrigger))
    {
      this.mAnimator.speed = this.mSpeed;
    }
    else
    {
      this.mSpeed = 1f;
      this.mAnimator.speed = 1f;
    }
    AnimatorStateInfo animatorStateInfo2 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
    if (!((AnimatorStateInfo) ref animatorStateInfo2).IsName(this.EndStateName) || this.mAnimator.IsInTransition(0))
      return;
    AnimatorStateInfo animatorStateInfo3 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
    if ((double) ((AnimatorStateInfo) ref animatorStateInfo3).normalizedTime < 1.0)
      return;
    this.mAnimator.SetBool(this.HideStateTrigger, true);
    ((Component) this).gameObject.SetActive(false);
  }

  public bool IsClosed()
  {
    if (this.mClose)
    {
      if (!((Component) this).gameObject.activeSelf)
        return true;
      AnimatorStateInfo animatorStateInfo1 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
      if (((AnimatorStateInfo) ref animatorStateInfo1).IsName("closed") && !this.mAnimator.IsInTransition(0))
      {
        AnimatorStateInfo animatorStateInfo2 = this.mAnimator.GetCurrentAnimatorStateInfo(0);
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo2).normalizedTime >= 1.0)
          return true;
      }
    }
    return false;
  }
}
