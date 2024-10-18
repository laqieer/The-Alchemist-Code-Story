// Decompiled with JetBrains decompiler
// Type: SRPG.RuneEnhanceSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneEnhanceSettings
  {
    private RuneEnhanceSettings.eEnhanceMode mMode;
    private RuneEnhanceSettings.eEnhanceMode mStartedMode;
    private int mValue;
    private bool mUseGauge;

    public RuneEnhanceSettings.eEnhanceMode Mode => this.mMode;

    public RuneEnhanceSettings.eEnhanceMode StartedMode => this.mStartedMode;

    public int Value => this.mValue;

    public bool UseGauge => this.mUseGauge;

    public void Setup(RuneEnhanceSettings.eEnhanceMode mode, int value, bool use_gauge)
    {
      this.mMode = mode;
      this.mStartedMode = mode;
      this.mValue = value;
      this.mUseGauge = use_gauge;
    }

    public void Reset(bool is_all = false)
    {
      this.mMode = RuneEnhanceSettings.eEnhanceMode.Normal;
      this.mValue = 0;
      this.mUseGauge = false;
      if (!is_all)
        return;
      this.mStartedMode = RuneEnhanceSettings.eEnhanceMode.Normal;
    }

    public void Decrement()
    {
      if (this.mMode != RuneEnhanceSettings.eEnhanceMode.Limit_EnhanceCount)
        return;
      this.mValue = Mathf.Max(0, this.mValue - 1);
    }

    public bool IsEnableContinueEnhance(RuneData rune)
    {
      switch (this.mMode)
      {
        case RuneEnhanceSettings.eEnhanceMode.Normal:
          return false;
        case RuneEnhanceSettings.eEnhanceMode.Limit_PlusCount:
          return rune.EnhanceNum < this.mValue;
        case RuneEnhanceSettings.eEnhanceMode.Limit_EnhanceCount:
          return this.mValue > 0;
        default:
          return false;
      }
    }

    public enum eEnhanceMode
    {
      Normal,
      Limit_PlusCount,
      Limit_EnhanceCount,
    }
  }
}
