// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGaugeMark
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitGaugeMark : MonoBehaviour
  {
    private UnitGaugeMark.EGemIcon mGemIconType;
    public string EndAnimationName = "chest_kari_end";
    public string EndTriggerName = "mode";
    public int EndTriggerValue = 2;
    public UnitGauge UnitGauge;
    public UnitGaugeMark.EMarkType MarkType;
    public GameObject MapChest;
    public UnitGaugeGemIcon MapGem;
    private List<UnitGaugeMark.ObjectAnim> mActiveMarkLists = new List<UnitGaugeMark.ObjectAnim>(Enum.GetNames(typeof (UnitGaugeMark.EMarkType)).Length - 1 + Enum.GetNames(typeof (UnitGaugeMark.EGemIcon)).Length - 1);
    private bool mIsGaugeUpdate;
    private bool mIsUnitDead;
    private bool mIsUseSkill;

    public UnitGaugeMark.EGemIcon GemIconType => this.mGemIconType;

    public bool IsGaugeUpdate
    {
      get => this.mIsGaugeUpdate;
      set => this.mIsGaugeUpdate = value;
    }

    public bool IsUnitDead
    {
      get => this.mIsUnitDead;
      set => this.mIsUnitDead = value;
    }

    public bool IsUseSkill
    {
      get => this.mIsUseSkill;
      set => this.mIsUseSkill = value;
    }

    public bool IsUpdatable(UnitGaugeMark.EMarkType MarkType)
    {
      bool flag = false;
      if ((UnitGauge.GaugeMode) this.GetUnitGaugeMode() == UnitGauge.GaugeMode.Normal && !this.mIsGaugeUpdate && !this.mIsUnitDead)
        flag = true;
      return flag;
    }

    private int GetUnitGaugeMode() => this.UnitGauge.Mode;

    private GameObject CreateMarkObject()
    {
      GameObject markObject = (GameObject) null;
      switch (this.MarkType)
      {
        case UnitGaugeMark.EMarkType.MapChest:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.MapChest))
          {
            markObject = UnityEngine.Object.Instantiate<GameObject>(this.MapChest, ((Component) this).transform.position, ((Component) this).transform.rotation);
            break;
          }
          break;
        case UnitGaugeMark.EMarkType.MapGem:
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.MapGem))
          {
            UnitGaugeGemIcon unitGaugeGemIcon = UnityEngine.Object.Instantiate<UnitGaugeGemIcon>(this.MapGem, ((Component) this).transform.position, ((Component) this).transform.rotation);
            unitGaugeGemIcon.IconImages.ImageIndex = (int) this.mGemIconType;
            markObject = ((Component) unitGaugeGemIcon).gameObject;
            break;
          }
          break;
      }
      return markObject;
    }

    private void SetEndAnimation(UnitGaugeMark.ObjectAnim mark)
    {
      if (mark == null || UnityEngine.Object.op_Equality((UnityEngine.Object) mark.Animator, (UnityEngine.Object) null) || mark.IsEnd)
        return;
      mark.IsEnd = true;
      mark.Animator.SetInteger(this.EndTriggerName, this.EndTriggerValue);
    }

    public void SetEndAnimation(UnitGaugeMark.EMarkType Type)
    {
      foreach (UnitGaugeMark.ObjectAnim mActiveMarkList in this.mActiveMarkLists)
      {
        if (mActiveMarkList.MarkType == Type)
          this.SetEndAnimation(mActiveMarkList);
      }
    }

    public void SetEndAnimationAll()
    {
      foreach (UnitGaugeMark.ObjectAnim mActiveMarkList in this.mActiveMarkLists)
        this.SetEndAnimation(mActiveMarkList);
    }

    public void ChangeAnimationByUnitType(EUnitType Type)
    {
      switch (Type)
      {
        case EUnitType.Treasure:
          this.MarkType = UnitGaugeMark.EMarkType.MapChest;
          break;
        case EUnitType.Gem:
          this.MarkType = UnitGaugeMark.EMarkType.MapGem;
          break;
      }
      this.mGemIconType = UnitGaugeMark.EGemIcon.Normal;
    }

    public void SetGemIcon(EEventGimmick EventType)
    {
      this.MarkType = UnitGaugeMark.EMarkType.MapGem;
      switch (EventType)
      {
        case EEventGimmick.CriUp:
          this.mGemIconType = UnitGaugeMark.EGemIcon.CriUp;
          break;
        case EEventGimmick.MovUp:
          this.mGemIconType = UnitGaugeMark.EGemIcon.MovUp;
          break;
        default:
          if (EventType == EEventGimmick.Heal)
          {
            this.mGemIconType = UnitGaugeMark.EGemIcon.Heal;
            break;
          }
          this.mGemIconType = UnitGaugeMark.EGemIcon.Normal;
          break;
      }
    }

    public void DeleteIconAll()
    {
      int num;
      for (int index1 = 0; index1 < this.mActiveMarkLists.Count; index1 = num + 1)
      {
        this.mActiveMarkLists[index1]?.Release();
        List<UnitGaugeMark.ObjectAnim> mActiveMarkLists = this.mActiveMarkLists;
        int index2 = index1;
        num = index2 - 1;
        mActiveMarkLists.RemoveAt(index2);
      }
    }

    private void Update()
    {
      for (int index = 0; index < this.mActiveMarkLists.Count; ++index)
      {
        UnitGaugeMark.ObjectAnim mActiveMarkList = this.mActiveMarkLists[index];
        if (!this.IsUpdatable(mActiveMarkList.MarkType))
          mActiveMarkList.Object.SetActive(false);
        else if (!mActiveMarkList.Object.activeInHierarchy)
        {
          mActiveMarkList.Release();
          this.mActiveMarkLists.RemoveAt(index--);
        }
        else if (mActiveMarkList.Object.activeInHierarchy && UnityEngine.Object.op_Inequality((UnityEngine.Object) mActiveMarkList.Animator, (UnityEngine.Object) null))
        {
          AnimatorStateInfo animatorStateInfo = mActiveMarkList.Animator.GetCurrentAnimatorStateInfo(0);
          if (((AnimatorStateInfo) ref animatorStateInfo).IsName(this.EndAnimationName) && !mActiveMarkList.Animator.IsInTransition(0) && (double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime >= 1.0)
          {
            mActiveMarkList.Release();
            this.mActiveMarkLists.RemoveAt(index--);
          }
        }
      }
      if (this.MarkType != UnitGaugeMark.EMarkType.None && this.IsUpdatable(this.MarkType) && this.mActiveMarkLists.Find((Predicate<UnitGaugeMark.ObjectAnim>) (am => am.MarkType == this.MarkType && am.GemIconType == this.mGemIconType)) == null)
      {
        GameObject markObject = this.CreateMarkObject();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) markObject, (UnityEngine.Object) null))
        {
          markObject.transform.SetParent(((Component) this).transform);
          markObject.transform.SetAsLastSibling();
          Animator component = markObject.GetComponent<Animator>();
          component.SetInteger(this.EndTriggerName, 0);
          this.mActiveMarkLists.Add(new UnitGaugeMark.ObjectAnim(markObject, component, this.MarkType, this.mGemIconType));
        }
      }
      this.MarkType = UnitGaugeMark.EMarkType.None;
    }

    private class ObjectAnim
    {
      public GameObject Object;
      public Animator Animator;
      public bool IsEnd;
      public UnitGaugeMark.EMarkType MarkType;
      public UnitGaugeMark.EGemIcon GemIconType;

      public ObjectAnim(
        GameObject Object,
        Animator Animator,
        UnitGaugeMark.EMarkType mark_type,
        UnitGaugeMark.EGemIcon gem_icon)
      {
        this.Object = Object;
        this.Animator = Animator;
        this.MarkType = mark_type;
        this.GemIconType = gem_icon;
        this.IsEnd = false;
      }

      public void Release()
      {
        this.Animator = (Animator) null;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Object, (UnityEngine.Object) null))
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.Object);
      }
    }

    [Serializable]
    public enum EMarkType
    {
      None,
      MapChest,
      MapGem,
    }

    [Serializable]
    public enum EGemIcon
    {
      Normal,
      Heal,
      CriUp,
      MovUp,
    }
  }
}
