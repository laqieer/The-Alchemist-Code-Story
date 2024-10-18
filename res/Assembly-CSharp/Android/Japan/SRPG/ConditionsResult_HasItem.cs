// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_HasItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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

    public override string text
    {
      get
      {
        return string.Empty;
      }
    }

    public override string errorText
    {
      get
      {
        return LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
      }
    }
  }
}
