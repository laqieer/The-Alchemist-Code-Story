﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailLevel : ConceptCardDetailBase
  {
    private readonly string ANIM_NAME_LV_TEXT_STYLE_DEFAULT = "default";
    private readonly string ANIM_NAME_LV_TEXT_STYLE_ENHANCE = "enhance";
    [SerializeField]
    private float mMixEffectAnimTime = 1f;
    [SerializeField]
    private GameObject mTrustList;
    [SerializeField]
    private Animator mCardLvAnimator;
    [SerializeField]
    private Text mCardLvCapText;
    [SerializeField]
    private Text mCardLvText;
    [SerializeField]
    private Text mCardNextExpText;
    [SerializeField]
    private Text mCardTrustItemText;
    [SerializeField]
    private Text mCardTrustItemMaxText;
    [SerializeField]
    private Text mCardTrustBonusText;
    [SerializeField]
    private Text mCardTrustBonusMaxText;
    [SerializeField]
    private Text mCardTrustName;
    [SerializeField]
    private Text mCardPredictLvWhiteText;
    [SerializeField]
    private Text mCardPredictLvGreenText;
    [SerializeField]
    private Text mCardPredictLvCapWhiteText;
    [SerializeField]
    private Text mCardPredictLvCapGreenText;
    [SerializeField]
    private GameObject mCardPredictLvSlash;
    [SerializeField]
    private Text mCardNextPredictExpText;
    [SerializeField]
    private Text mCardPredictTrustItemText;
    [SerializeField]
    private Text mCardPredictTrustItemMaxText;
    [SerializeField]
    private Text mCardPredictTrustBonusText;
    [SerializeField]
    private Text mCardPredictTrustBonusMaxText;
    [SerializeField]
    private GameObject mCardPredictLvArrow;
    [SerializeField]
    private Slider mCardLvSlider;
    [SerializeField]
    private Slider mCardPredictLvSlider;
    [SerializeField]
    private GameObject mTrustMasterRewardBase;
    [SerializeField]
    private RawImage mTrustMasterRewardIcon;
    [SerializeField]
    private Image mTrustMasuterRewardFrame;
    [SerializeField]
    private GameObject mTrustMasterRewardItemIconObject;
    [SerializeField]
    private ConceptCardIcon mTrustMasterRewardCardIcon;
    [SerializeField]
    private GameObject mTrustMasterRewardAmountCountParent;
    [SerializeField]
    private Text mTrustMasterRewardAmountCount;
    [SerializeField]
    private Animator mTrustUpAnimator;
    [SerializeField]
    private GameObject mAwakeCountIconsParent;
    private int mExpStart;
    private int mExpEnd;
    private int mTrustStart;
    private int mTrustEnd;
    private ConceptCardDetailLevel.EffectCallBack mCallback;
    private int mAddExp;
    private int mAddTrust;
    private int mAddAwakeLv;
    private int mAddAwakeCount;
    private int mAddTrustLv;
    private bool mEnhance;

    public override void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
    {
      this.mConceptCardData = card_data;
      this.mAddExp = addExp;
      this.mAddTrust = addTrust;
      this.mAddAwakeLv = addAwakeLv;
      this.mAddAwakeCount = this.mAddAwakeLv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
      this.mAddTrustLv = (addTrust + (int) this.mConceptCardData.Trust) / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax - (int) this.mConceptCardData.Trust / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      this.mEnhance = ConceptCardDescription.IsEnhance;
    }

    public override void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      this.RefreshParam((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Exp, (int) this.mConceptCardData.Trust, (int) this.mConceptCardData.CurrentLvCap, this.mEnhance, false);
      this.RefreshIcon();
      this.RefreshFrame();
      this.RefreshRewardAmountCount();
    }

    public void RefreshIcon()
    {
      if ((UnityEngine.Object) this.mTrustMasterRewardBase == (UnityEngine.Object) null)
        return;
      ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
      if (reward != null)
      {
        bool is_on = reward.reward_type == eRewardType.ConceptCard;
        this.SwitchObject(is_on, this.mTrustMasterRewardCardIcon.gameObject, this.mTrustMasterRewardItemIconObject);
        this.mTrustMasterRewardBase.gameObject.SetActive(true);
        if (is_on)
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.iname);
          if (cardDataForDisplay != null)
            this.mTrustMasterRewardCardIcon.Setup(cardDataForDisplay);
        }
        else
          this.LoadImage(reward.GetIconPath(), this.mTrustMasterRewardIcon);
        if (!((UnityEngine.Object) this.mCardTrustName != (UnityEngine.Object) null))
          return;
        this.mCardTrustName.gameObject.SetActive(true);
        this.SetText(this.mCardTrustName, reward.GetItemName());
      }
      else
      {
        if ((UnityEngine.Object) this.mCardTrustName != (UnityEngine.Object) null)
          this.mCardTrustName.gameObject.SetActive(false);
        this.mTrustMasterRewardBase.gameObject.SetActive(false);
      }
    }

    public void RefreshFrame()
    {
      ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
      if (reward == null)
        return;
      this.SetSprite(this.mTrustMasuterRewardFrame, reward.GetFrameSprite());
    }

    private void RefreshRewardAmountCount()
    {
      if ((UnityEngine.Object) this.mTrustMasterRewardAmountCountParent == (UnityEngine.Object) null || (UnityEngine.Object) this.mTrustMasterRewardAmountCount == (UnityEngine.Object) null)
        return;
      this.mTrustMasterRewardAmountCountParent.SetActive(false);
      ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
      if (reward == null || reward.reward_num <= 1)
        return;
      this.mTrustMasterRewardAmountCountParent.gameObject.SetActive(true);
      this.mTrustMasterRewardAmountCount.text = reward.reward_num.ToString();
    }

    public void RefreshParam(int rarity, int baseExp, int baseTrust, int lvCap, bool enhance, bool enhance_anim = false)
    {
      int lv1;
      int nextExp1;
      int expTbl1;
      ConceptCardUtility.GetExpParameter(rarity, baseExp, (int) this.mConceptCardData.CurrentLvCap, out lv1, out nextExp1, out expTbl1);
      int cardTrustMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      int num1 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(this.mConceptCardData) + this.mAddAwakeCount * cardTrustMax;
      bool flag1 = enhance && this.mAddExp != 0 && nextExp1 != 0;
      bool flag2 = enhance && this.mAddTrust != 0 && baseTrust != num1;
      bool flag3 = enhance && this.mAddAwakeLv != 0;
      bool flag4 = enhance && (this.mAddTrustLv != 0 || this.mAddAwakeCount != 0);
      if (this.mConceptCardData.GetReward() != null)
        this.mTrustList.SetActive(true);
      else
        this.mTrustList.SetActive(false);
      this.SetText(this.mCardLvText, lv1.ToString());
      this.SetText(this.mCardLvCapText, lvCap.ToString());
      this.SetText(this.mCardNextExpText, nextExp1.ToString());
      ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.mCardTrustItemText, baseTrust, false);
      if ((UnityEngine.Object) this.mCardTrustItemMaxText != (UnityEngine.Object) null)
        ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.mCardTrustItemMaxText, MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(this.mConceptCardData), true);
      int num2 = (int) this.mConceptCardData.AwakeCount + 1 - this.mConceptCardData.TrustBonus;
      if ((UnityEngine.Object) this.mCardTrustBonusText != (UnityEngine.Object) null)
      {
        this.SetText(this.mCardTrustBonusText, num2.ToString());
        if (num2 <= 0 && this.mConceptCardData.GetReward() != null)
          this.mCardTrustBonusText.color = Color.red;
        else
          this.mCardTrustBonusText.color = Color.white;
      }
      this.SetText(this.mCardTrustBonusMaxText, ((int) this.mConceptCardData.AwakeCount + 1).ToString());
      this.mCardLvSlider.value = (float) (1.0 - (double) nextExp1 / (double) expTbl1);
      this.mCardPredictLvArrow.SetActive(false);
      this.mCardPredictLvSlash.SetActive(false);
      this.mCardPredictLvWhiteText.gameObject.SetActive(false);
      this.mCardPredictLvGreenText.gameObject.SetActive(false);
      this.mCardPredictLvCapWhiteText.gameObject.SetActive(false);
      this.mCardPredictLvCapGreenText.gameObject.SetActive(false);
      this.mCardPredictTrustItemText.gameObject.SetActive(false);
      this.mCardLvAnimator.Play(this.ANIM_NAME_LV_TEXT_STYLE_DEFAULT);
      if (flag1 || flag3)
      {
        int lv2;
        int nextExp2;
        int expTbl2;
        ConceptCardUtility.GetExpParameter(rarity, baseExp + this.mAddExp, lvCap + this.mAddAwakeLv, out lv2, out nextExp2, out expTbl2);
        this.SetText(this.mCardNextPredictExpText, nextExp2.ToString());
        this.mCardPredictLvSlider.value = lv1 >= lv2 ? (float) (1.0 - (double) nextExp2 / (double) expTbl2) : 1f;
        this.mCardPredictLvArrow.SetActive(true);
        this.mCardPredictLvSlash.SetActive(true);
        this.mCardLvAnimator.Play(this.ANIM_NAME_LV_TEXT_STYLE_ENHANCE);
        if (lv1 < lv2)
        {
          this.mCardPredictLvGreenText.gameObject.SetActive(true);
          this.SetText(this.mCardPredictLvGreenText, lv2.ToString());
        }
        else
        {
          this.mCardPredictLvWhiteText.gameObject.SetActive(true);
          this.SetText(this.mCardPredictLvWhiteText, lv2.ToString());
        }
        if (flag3)
        {
          this.mCardPredictLvCapGreenText.gameObject.SetActive(true);
          this.SetText(this.mCardPredictLvCapGreenText, (lvCap + this.mAddAwakeLv).ToString());
        }
        else
        {
          this.mCardPredictLvCapWhiteText.gameObject.SetActive(true);
          this.SetText(this.mCardPredictLvCapWhiteText, (lvCap + this.mAddAwakeLv).ToString());
        }
      }
      int num3 = (int) this.mConceptCardData.AwakeCount + 1 + this.mAddAwakeCount;
      if (flag4)
      {
        if (num3 > this.mConceptCardData.AwakeCountCap + 1)
          num3 = this.mConceptCardData.AwakeCountCap + 1;
        ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.mCardPredictTrustItemMaxText, num3 * cardTrustMax, true);
        this.SetText(this.mCardPredictTrustBonusMaxText, num3.ToString());
        ConceptCardManager instance = ConceptCardManager.Instance;
        int num4 = 0;
        if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
          num4 = ConceptCardManager.CalcTotalTrustBonusMixCount(instance.SelectedConceptCardData, instance.SelectedMaterials);
        int num5 = num4 + (((int) this.mConceptCardData.Trust + this.mAddTrust) / cardTrustMax - num4);
        int num6 = num3 - num5;
        if (num6 < 0)
          num6 = 0;
        this.SetText(this.mCardPredictTrustBonusText, num6.ToString());
      }
      if (flag2)
      {
        int trust = baseTrust + this.mAddTrust;
        if (num3 * cardTrustMax < baseTrust + this.mAddTrust)
          trust = num3 * cardTrustMax;
        this.SetText(this.mCardPredictTrustItemText, ConceptCardManager.ParseTrustFormat(this.mConceptCardData, trust));
      }
      if (!enhance_anim)
        this.RefreshAwakeIcons(enhance);
      this.mCardNextExpText.gameObject.SetActive(!flag1);
      this.mCardNextPredictExpText.gameObject.SetActive(flag1);
      this.mCardPredictLvSlider.gameObject.SetActive(flag1);
      this.mCardTrustItemText.gameObject.SetActive(!flag2);
      this.mCardPredictTrustItemText.gameObject.SetActive(flag2);
      if ((bool) ((UnityEngine.Object) this.mCardTrustItemMaxText))
        this.mCardTrustItemMaxText.gameObject.SetActive(!flag4);
      if ((bool) ((UnityEngine.Object) this.mCardPredictTrustItemMaxText))
        this.mCardPredictTrustItemMaxText.gameObject.SetActive(flag4);
      if ((bool) ((UnityEngine.Object) this.mCardTrustBonusText))
        this.mCardTrustBonusText.gameObject.SetActive(!flag4);
      if ((bool) ((UnityEngine.Object) this.mCardTrustBonusMaxText))
        this.mCardTrustBonusMaxText.gameObject.SetActive(!flag4);
      if ((UnityEngine.Object) this.mCardPredictTrustBonusText != (UnityEngine.Object) null)
        this.mCardPredictTrustBonusText.gameObject.SetActive(flag4);
      if (!(bool) ((UnityEngine.Object) this.mCardPredictTrustBonusMaxText))
        return;
      this.mCardPredictTrustBonusMaxText.gameObject.SetActive(flag4);
    }

    private void GetExpParameter(int rarity, int exp, int lvcap, out int lv, out int nextExp, out int expTbl)
    {
      lv = this.Master.CalcConceptCardLevel(rarity, exp, lvcap);
      int conceptCardLevelExp = this.Master.GetConceptCardLevelExp(rarity, lv);
      if (lv < lvcap)
      {
        expTbl = this.Master.GetConceptCardNextExp(rarity, lv + 1);
        nextExp = expTbl - (exp - conceptCardLevelExp);
      }
      else
      {
        expTbl = 1;
        nextExp = 0;
      }
    }

    private void RefreshAwakeIcons(bool is_enhance)
    {
      this.mAwakeCountIconsParent.SetActive(this.mConceptCardData.IsEnableAwake);
      int num1 = 0;
      if (is_enhance)
        num1 = this.mAddAwakeLv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
      int num2 = 0;
      IEnumerator enumerator1 = this.mAwakeCountIconsParent.transform.GetEnumerator();
      try
      {
        while (enumerator1.MoveNext())
        {
          Transform current = (Transform) enumerator1.Current;
          IEnumerator enumerator2 = current.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
              ((Component) enumerator2.Current).gameObject.SetActive(false);
          }
          finally
          {
            IDisposable disposable;
            if ((disposable = enumerator2 as IDisposable) != null)
              disposable.Dispose();
          }
          string name = "off";
          if (num2 < (int) this.mConceptCardData.AwakeCount)
            name = "on";
          else if (num2 < (int) this.mConceptCardData.AwakeCount + num1)
            name = "up";
          Transform transform = current.Find(name);
          if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
          {
            transform.gameObject.SetActive(true);
            ++num2;
          }
        }
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator1 as IDisposable) != null)
          disposable.Dispose();
      }
    }

    public void SetupLevelupAnimation(int addExp, int addTrust, int addAwakeLv = 0)
    {
      this.mExpStart = (int) this.mConceptCardData.Exp;
      this.mExpEnd = this.mExpStart + addExp;
      this.mTrustStart = (int) this.mConceptCardData.Trust;
      this.mTrustEnd = this.mTrustStart + addTrust;
      int b = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(this.mConceptCardData) + this.mAddAwakeLv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      if (b > (this.mConceptCardData.AwakeCountCap + 1) * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax)
        b = (this.mConceptCardData.AwakeCountCap + 1) * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      this.mTrustStart = Mathf.Min(this.mTrustStart, b);
      this.mTrustEnd = Mathf.Min(this.mTrustEnd, b);
    }

    public void StartLevelupAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      this.mAddExp = 0;
      this.mAddTrust = 0;
      this.mEnhance = false;
      bool bLevelUpEffect = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpStart, (int) this.mConceptCardData.CurrentLvCap) < this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpEnd, (int) this.mConceptCardData.CurrentLvCap);
      bool bTrustUpEffect = this.mTrustStart < this.mTrustEnd;
      this.mExpEnd = Mathf.Min(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.CurrentLvCap), this.mExpEnd);
      this.StartCoroutine(this.LevelupUpdate(this.mConceptCardData, bLevelUpEffect, bTrustUpEffect));
    }

    public void StartAwakeAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      this.StartCoroutine(this.AwakeUpdate(this.mAddAwakeLv > 0));
    }

    public void StartTrustMasterAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      int cardTrustMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      this.StartCoroutine(this.TrustMasterUpdate(this.mConceptCardData, this.mConceptCardData.GetReward() != null && this.mConceptCardData.TrustBonusCount != 0 && this.mTrustStart < this.mTrustEnd && this.mTrustEnd / cardTrustMax - this.mTrustStart / cardTrustMax > 0));
    }

    public void StartGroupSkillPowerUpAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      int num1 = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpStart, (int) this.mConceptCardData.CurrentLvCap);
      bool bGroupSkillPowerUp = this.mAddAwakeLv > 0;
      ConceptCardEffectRoutine.EffectAltData altData = new ConceptCardEffectRoutine.EffectAltData();
      int num2 = this.mAddAwakeLv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
      altData.prevAwakeCount = (int) this.mConceptCardData.AwakeCount - num2;
      altData.prevLevel = num1;
      this.StartCoroutine(this.GroupSkillPowerUpUpdate(this.mConceptCardData, bGroupSkillPowerUp, altData));
    }

    public void StartGroupSkillMaxPowerUpAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      int num1 = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpStart, (int) this.mConceptCardData.CurrentLvCap);
      int num2 = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpEnd, (int) this.mConceptCardData.CurrentLvCap);
      int num3 = this.mAddAwakeLv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
      bool bGroupSkillMaxPowerUp = (num1 != num2 || num3 > 0) && ((int) this.mConceptCardData.AwakeCount == this.mConceptCardData.AwakeCountCap && (int) this.mConceptCardData.Lv == (int) this.mConceptCardData.LvCap);
      ConceptCardEffectRoutine.EffectAltData altData = new ConceptCardEffectRoutine.EffectAltData();
      if (this.mAddAwakeLv > 0)
      {
        altData.prevAwakeCount = (int) this.mConceptCardData.AwakeCount;
        altData.prevLevel = (int) this.mConceptCardData.Lv;
      }
      else
      {
        altData.prevAwakeCount = (int) this.mConceptCardData.AwakeCount - num3;
        altData.prevLevel = num1;
      }
      this.StartCoroutine(this.GroupSkillMaxPowerUpUpdate(this.mConceptCardData, bGroupSkillMaxPowerUp, altData));
    }

    [DebuggerHidden]
    private IEnumerator LevelupUpdate(ConceptCardData cardData, bool bLevelUpEffect, bool bTrustUpEffect)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CLevelupUpdate\u003Ec__Iterator0() { cardData = cardData, bTrustUpEffect = bTrustUpEffect, bLevelUpEffect = bLevelUpEffect, \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator AwakeUpdate(bool is_awake)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CAwakeUpdate\u003Ec__Iterator1() { is_awake = is_awake, \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator TrustMasterUpdate(ConceptCardData cardData, bool bTrustMasterEffect)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CTrustMasterUpdate\u003Ec__Iterator2() { bTrustMasterEffect = bTrustMasterEffect, cardData = cardData, \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator GroupSkillPowerUpUpdate(ConceptCardData cardData, bool bGroupSkillPowerUp, ConceptCardEffectRoutine.EffectAltData altData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CGroupSkillPowerUpUpdate\u003Ec__Iterator3() { bGroupSkillPowerUp = bGroupSkillPowerUp, cardData = cardData, altData = altData, \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator GroupSkillMaxPowerUpUpdate(ConceptCardData cardData, bool bGroupSkillMaxPowerUp, ConceptCardEffectRoutine.EffectAltData altData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CGroupSkillMaxPowerUpUpdate\u003Ec__Iterator4() { bGroupSkillMaxPowerUp = bGroupSkillMaxPowerUp, cardData = cardData, altData = altData, \u0024this = this };
    }

    public delegate void EffectCallBack();
  }
}
