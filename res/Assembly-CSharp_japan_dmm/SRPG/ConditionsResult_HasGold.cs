// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_HasGold
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class ConditionsResult_HasGold : ConditionsResult
  {
    public ConditionsResult_HasGold(int condsNum)
    {
      this.mCurrentValue = MonoSingleton<GameManager>.Instance.Player.Gold;
      this.mTargetValue = condsNum;
      this.mIsClear = this.mCurrentValue >= this.mTargetValue;
    }

    public override string text => string.Empty;

    public override string errorText => LocalizedText.Get("sys.GOLD_NOT_ENOUGH");
  }
}
