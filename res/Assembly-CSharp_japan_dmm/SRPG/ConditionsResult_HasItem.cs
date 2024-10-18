// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_HasItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class ConditionsResult_HasItem : ConditionsResult
  {
    public ConditionsResult_HasItem(string iname, int condsItemNum)
    {
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
      if (itemDataByItemId != null)
        this.mCurrentValue = itemDataByItemId.Num;
      this.mTargetValue = condsItemNum;
      this.mIsClear = this.mCurrentValue >= this.mTargetValue;
    }

    public ConditionsResult_HasItem(string[] inames, int condsItemNum)
    {
      if (inames == null || inames.Length == 0)
        return;
      int num = 0;
      for (int index = 0; index < inames.Length; ++index)
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(inames[index]);
        if (itemDataByItemId != null)
          num += itemDataByItemId.Num;
      }
      this.mCurrentValue = num;
      this.mTargetValue = condsItemNum;
      this.mIsClear = this.mCurrentValue >= this.mTargetValue;
    }

    public override string text => string.Empty;

    public override string errorText => LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
  }
}
