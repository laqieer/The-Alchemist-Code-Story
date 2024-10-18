// Decompiled with JetBrains decompiler
// Type: SRPG.StoryExTotalChallengeCountResetConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "初期化完了", FlowNode.PinTypes.Output, 100)]
  public class StoryExTotalChallengeCountResetConfirm : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT_START = 10;
    private const int PIN_OUTPUT_INIT_END = 100;
    [SerializeField]
    private eResetCostType mCostType;
    [SerializeField]
    private Text mConfirmText;
    [SerializeField]
    private Text mBeforeNum;
    [SerializeField]
    private Text mAfterNum;
    [SerializeField]
    private Text mRestResetCount;
    [SerializeField]
    private GameObject mItemIcon;

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      switch (this.mCostType)
      {
        case eResetCostType.Coin:
          this.Refresh_Coin();
          break;
        case eResetCostType.Item:
          this.Refresh_Item();
          break;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void Refresh_Item()
    {
      ResetCostParam resetCost = MonoSingleton<GameManager>.Instance.MasterParam.FindResetCost(MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExRsetCost);
      if (resetCost == null || !resetCost.IsEnableItemCost())
        return;
      ResetCostInfoParam resetCostInfo = resetCost.GetResetCostInfo(eResetCostType.Item);
      if (Object.op_Inequality((Object) this.mItemIcon, (Object) null))
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(resetCostInfo.Item);
        if (itemParam != null)
        {
          DataSource.Bind<ItemParam>(this.mItemIcon, itemParam);
          GameParameter.UpdateAll(this.mItemIcon);
        }
        else
          GameUtility.SetGameObjectActive(this.mItemIcon, false);
      }
      int needNum = resetCostInfo.GetNeedNum(MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.Reset);
      int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(resetCostInfo.Item);
      if (Object.op_Inequality((Object) this.mConfirmText, (Object) null))
        this.mConfirmText.text = string.Format(LocalizedText.Get("sys.STORY_EX_CHALLENGE_COUNT_RESET_CONFIRM_ITEM"), (object) itemAmount, (object) needNum);
      if (Object.op_Inequality((Object) this.mBeforeNum, (Object) null))
        this.mBeforeNum.text = itemAmount.ToString();
      if (Object.op_Inequality((Object) this.mAfterNum, (Object) null))
        this.mAfterNum.text = Mathf.Max(0, itemAmount - needNum).ToString();
      if (!Object.op_Inequality((Object) this.mRestResetCount, (Object) null))
        return;
      this.mRestResetCount.text = MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.RestResetCount.ToString();
    }

    private void Refresh_Coin()
    {
      GameUtility.SetGameObjectActive(this.mItemIcon, false);
      ResetCostParam resetCost = MonoSingleton<GameManager>.Instance.MasterParam.FindResetCost(MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExRsetCost);
      if (resetCost == null || !resetCost.IsEnableCoinCost())
        return;
      int needNum = resetCost.GetResetCostInfo(eResetCostType.Coin).GetNeedNum(MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.Reset);
      if (Object.op_Inequality((Object) this.mConfirmText, (Object) null))
        this.mConfirmText.text = string.Format(LocalizedText.Get("sys.STORY_EX_CHALLENGE_COUNT_RESET_CONFIRM_COIN"), (object) needNum);
      if (!Object.op_Inequality((Object) this.mRestResetCount, (Object) null))
        return;
      this.mRestResetCount.text = MonoSingleton<GameManager>.Instance.Player.StoryExChallengeCount.RestResetCount.ToString();
    }
  }
}
