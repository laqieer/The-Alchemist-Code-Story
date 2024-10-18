// Decompiled with JetBrains decompiler
// Type: SRPG.ExpPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Level Up", FlowNode.PinTypes.Output, 100)]
  public class ExpPanel : MonoBehaviour, IFlowInterface
  {
    public ExpPanel.LevelChangeEvent OnLevelChange = (ExpPanel.LevelChangeEvent) ((a, b) => {});
    public ExpPanel.ExpPanelEvent OnFinish = (ExpPanel.ExpPanelEvent) (() => {});
    public Text Level;
    public Text LevelMax;
    public Text ValueCurrent;
    public Text ValueCurrentInLv;
    public Text ValueLeft;
    public Text ValueTotal;
    public Slider LevelSlider;
    public Slider ExpSlider;
    public float ExpAnimLength;
    private int mCurrentExp;
    private float mExpStart;
    private float mExpEnd;
    private float mExpAnimTime;
    private int mLevelCap;
    private ExpPanel.CalcEvent mOnCalcExp;
    private ExpPanel.CalcEvent mOnCalcLevel;

    public bool IsBusy
    {
      get
      {
        return (double) this.mExpStart != (double) this.mExpEnd;
      }
    }

    public int Exp
    {
      set
      {
        this.mCurrentExp = value;
        this.mExpStart = this.mExpEnd = (float) this.mCurrentExp;
        this.SetValues((float) this.mCurrentExp);
        this.Stop();
      }
      get
      {
        return this.mCurrentExp;
      }
    }

    public int LevelCap
    {
      get
      {
        return this.mLevelCap;
      }
      set
      {
        this.mLevelCap = value;
        if (!((UnityEngine.Object) this.LevelMax != (UnityEngine.Object) null))
          return;
        this.LevelMax.text = this.mLevelCap.ToString();
      }
    }

    public void AnimateTo(int newExp)
    {
      this.mExpStart = Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / this.ExpAnimLength));
      this.mExpEnd = (float) newExp;
      this.mExpAnimTime = 0.0f;
      this.enabled = true;
    }

    public void Stop()
    {
      this.mExpEnd = this.mExpStart;
      this.enabled = false;
    }

    public void SetDelegate(ExpPanel.CalcEvent expFromLv, ExpPanel.CalcEvent lvFromExp)
    {
      this.mOnCalcExp = expFromLv;
      this.mOnCalcLevel = lvFromExp;
    }

    private void SetValues(float exp)
    {
      int a = Mathf.FloorToInt(exp);
      int num1 = Mathf.Min(this.mOnCalcLevel(a), this.mLevelCap);
      int num2 = this.mOnCalcExp(num1);
      int b = this.mOnCalcExp(Mathf.Min(num1 + 1, this.mLevelCap));
      int num3 = Mathf.Min(a, b);
      if ((UnityEngine.Object) this.Level != (UnityEngine.Object) null)
        this.Level.text = num1.ToString();
      if ((UnityEngine.Object) this.LevelSlider != (UnityEngine.Object) null)
      {
        this.LevelSlider.maxValue = (float) this.mOnCalcExp(this.mLevelCap);
        this.LevelSlider.minValue = 0.0f;
        this.LevelSlider.value = (float) num3;
      }
      if ((UnityEngine.Object) this.ExpSlider != (UnityEngine.Object) null)
      {
        if (num1 >= this.mLevelCap)
        {
          this.ExpSlider.maxValue = 1f;
          this.ExpSlider.minValue = 0.0f;
          this.ExpSlider.value = 1f;
        }
        else
        {
          this.ExpSlider.maxValue = (float) b;
          this.ExpSlider.minValue = (float) num2;
          this.ExpSlider.value = (float) num3;
        }
      }
      if ((UnityEngine.Object) this.ValueCurrent != (UnityEngine.Object) null)
        this.ValueCurrent.text = num3.ToString();
      if ((UnityEngine.Object) this.ValueLeft != (UnityEngine.Object) null)
        this.ValueLeft.text = (b - num3).ToString();
      if ((UnityEngine.Object) this.ValueCurrentInLv != (UnityEngine.Object) null)
        this.ValueCurrentInLv.text = (num3 - num2).ToString();
      if (!((UnityEngine.Object) this.ValueTotal != (UnityEngine.Object) null))
        return;
      this.ValueTotal.text = (b - num2).ToString();
    }

    private void AnimateExp(float dt)
    {
      if (this.mOnCalcExp == null || this.mOnCalcLevel == null)
        return;
      float f = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / this.ExpAnimLength);
      this.mExpAnimTime += dt;
      bool flag1 = (double) this.mExpAnimTime >= (double) this.ExpAnimLength;
      float num1 = !flag1 ? Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / this.ExpAnimLength)) : this.mExpEnd;
      int num2 = Mathf.FloorToInt(f);
      int num3 = Mathf.FloorToInt(num1);
      int levelOld = Mathf.Min(this.mOnCalcLevel(num2), this.mLevelCap);
      int levelNew = Mathf.Min(this.mOnCalcLevel(num3), this.mLevelCap);
      bool flag2 = levelOld != levelNew;
      this.mCurrentExp = Mathf.FloorToInt(num1);
      this.SetValues(num1);
      if (flag2)
      {
        this.OnLevelChange(levelOld, levelNew);
        if (levelOld < levelNew)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      if (!flag1)
        return;
      this.enabled = false;
      this.mExpStart = this.mExpEnd = (float) this.mCurrentExp;
      this.OnFinish();
    }

    private void Update()
    {
      if ((double) this.mExpStart >= (double) this.mExpEnd)
        return;
      this.AnimateExp(Time.unscaledDeltaTime);
    }

    public void Activated(int pinID)
    {
    }

    public delegate int CalcEvent(int value);

    public delegate void LevelChangeEvent(int levelOld, int levelNew);

    public delegate void ExpPanelEvent();
  }
}
