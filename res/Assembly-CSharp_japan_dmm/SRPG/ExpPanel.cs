// Decompiled with JetBrains decompiler
// Type: SRPG.ExpPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "Level Up", FlowNode.PinTypes.Output, 100)]
  public class ExpPanel : MonoBehaviour, IFlowInterface
  {
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
    public ExpPanel.LevelChangeEvent OnLevelChange = (ExpPanel.LevelChangeEvent) ((a, b) => { });
    public ExpPanel.ExpPanelEvent OnFinish = (ExpPanel.ExpPanelEvent) (() => { });

    public bool IsBusy => (double) this.mExpStart != (double) this.mExpEnd;

    public int Exp
    {
      set
      {
        this.mCurrentExp = value;
        this.mExpStart = this.mExpEnd = (float) this.mCurrentExp;
        this.SetValues((float) this.mCurrentExp);
        this.Stop();
      }
      get => this.mCurrentExp;
    }

    public int LevelCap
    {
      get => this.mLevelCap;
      set
      {
        this.mLevelCap = value;
        if (!Object.op_Inequality((Object) this.LevelMax, (Object) null))
          return;
        this.LevelMax.text = this.mLevelCap.ToString();
      }
    }

    public void AnimateTo(int newExp)
    {
      this.mExpStart = Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / this.ExpAnimLength));
      this.mExpEnd = (float) newExp;
      this.mExpAnimTime = 0.0f;
      ((Behaviour) this).enabled = true;
    }

    public void Stop()
    {
      this.mExpEnd = this.mExpStart;
      ((Behaviour) this).enabled = false;
    }

    public void SetDelegate(ExpPanel.CalcEvent expFromLv, ExpPanel.CalcEvent lvFromExp)
    {
      this.mOnCalcExp = expFromLv;
      this.mOnCalcLevel = lvFromExp;
    }

    private void SetValues(float exp)
    {
      int num1 = Mathf.FloorToInt(exp);
      int num2 = Mathf.Min(this.mOnCalcLevel(num1), this.mLevelCap);
      int num3 = this.mOnCalcExp(num2);
      int num4 = this.mOnCalcExp(Mathf.Min(num2 + 1, this.mLevelCap));
      int num5 = Mathf.Min(num1, num4);
      if (Object.op_Inequality((Object) this.Level, (Object) null))
        this.Level.text = num2.ToString();
      if (Object.op_Inequality((Object) this.LevelSlider, (Object) null))
      {
        this.LevelSlider.maxValue = (float) this.mOnCalcExp(this.mLevelCap);
        this.LevelSlider.minValue = 0.0f;
        this.LevelSlider.value = (float) num5;
      }
      if (Object.op_Inequality((Object) this.ExpSlider, (Object) null))
      {
        if (num2 >= this.mLevelCap)
        {
          this.ExpSlider.maxValue = 1f;
          this.ExpSlider.minValue = 0.0f;
          this.ExpSlider.value = 1f;
        }
        else
        {
          this.ExpSlider.maxValue = (float) num4;
          this.ExpSlider.minValue = (float) num3;
          this.ExpSlider.value = (float) num5;
        }
      }
      if (Object.op_Inequality((Object) this.ValueCurrent, (Object) null))
        this.ValueCurrent.text = num5.ToString();
      if (Object.op_Inequality((Object) this.ValueLeft, (Object) null))
        this.ValueLeft.text = (num4 - num5).ToString();
      if (Object.op_Inequality((Object) this.ValueCurrentInLv, (Object) null))
        this.ValueCurrentInLv.text = (num5 - num3).ToString();
      if (!Object.op_Inequality((Object) this.ValueTotal, (Object) null))
        return;
      this.ValueTotal.text = (num4 - num3).ToString();
    }

    private void AnimateExp(float dt)
    {
      if (this.mOnCalcExp == null || this.mOnCalcLevel == null)
        return;
      float num1 = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / this.ExpAnimLength);
      this.mExpAnimTime += dt;
      bool flag1 = (double) this.mExpAnimTime >= (double) this.ExpAnimLength;
      float exp = !flag1 ? Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / this.ExpAnimLength)) : this.mExpEnd;
      int num2 = Mathf.FloorToInt(num1);
      int num3 = Mathf.FloorToInt(exp);
      int levelOld = Mathf.Min(this.mOnCalcLevel(num2), this.mLevelCap);
      int levelNew = Mathf.Min(this.mOnCalcLevel(num3), this.mLevelCap);
      bool flag2 = levelOld != levelNew;
      this.mCurrentExp = Mathf.FloorToInt(exp);
      this.SetValues(exp);
      if (flag2)
      {
        this.OnLevelChange(levelOld, levelNew);
        if (levelOld < levelNew)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      if (!flag1)
        return;
      ((Behaviour) this).enabled = false;
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
