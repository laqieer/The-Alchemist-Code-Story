// Decompiled with JetBrains decompiler
// Type: FlowNode_BattleSpeedEditorOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[FlowNode.NodeType("System/Battle/Speed/BattleSpeedEditorOption", 32741)]
[FlowNode.Pin(1, "オートプレイON", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(2, "オートプレイOFF", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(4, "倍速機能ON", FlowNode.PinTypes.Input, 4)]
[FlowNode.Pin(5, "倍速機能OFF", FlowNode.PinTypes.Input, 5)]
[FlowNode.Pin(3, "表示更新", FlowNode.PinTypes.Input, 3)]
public class FlowNode_BattleSpeedEditorOption : FlowNode
{
  private const int INPUT_AUTO_PLAY_ON = 1;
  private const int INPUT_AUTO_PLAY_OFF = 2;
  private const int INPUT_REFLESH = 3;
  private const int INPUT_BATTLE_SPEED_ON = 4;
  private const int INPUT_BATTLE_SPEED_OFF = 5;
  [SerializeField]
  private Toggle AutoPlayToggle;
  [SerializeField]
  private Toggle battleSpeedToggle;
  [SerializeField]
  private GameObject battleSpeedLockObj;
  [SerializeField]
  private ScrollablePulldown BattleSpeedPulldown;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 2:
        if (!BattleSpeedController.isAutoOnly)
          break;
        GameUtility.BooleanConfig configUseBattleSpeed = GameUtility.Config_UseBattleSpeed;
        bool flag1 = false;
        this.battleSpeedToggle.isOn = flag1;
        int num1 = flag1 ? 1 : 0;
        configUseBattleSpeed.Value = num1 != 0;
        break;
      case 3:
        this.Refresh();
        break;
      case 4:
        if (!BattleSpeedController.isPremium)
          break;
        if (BattleSpeedController.isAutoOnly)
        {
          GameUtility.BooleanConfig configUseAutoPlay = GameUtility.Config_UseAutoPlay;
          bool flag2 = true;
          this.AutoPlayToggle.isOn = flag2;
          int num2 = flag2 ? 1 : 0;
          configUseAutoPlay.Value = num2 != 0;
        }
        GameUtility.Config_UseBattleSpeed.Value = true;
        break;
      case 5:
        GameUtility.Config_UseBattleSpeed.Value = false;
        break;
    }
  }

  private void Refresh()
  {
    if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.battleSpeedToggle))
    {
      if (BattleSpeedController.isPremium)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.battleSpeedLockObj))
          this.battleSpeedLockObj.SetActive(false);
        ((Selectable) this.battleSpeedToggle).interactable = true;
      }
      else
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.battleSpeedLockObj))
          this.battleSpeedLockObj.SetActive(true);
        ((Selectable) this.battleSpeedToggle).interactable = false;
        GameUtility.Config_UseBattleSpeed.Value = false;
      }
      this.battleSpeedToggle.isOn = GameUtility.Config_UseBattleSpeed.Value;
    }
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BattleSpeedPulldown, (UnityEngine.Object) null))
      return;
    this.BattleSpeedPulldown.ClearItems();
    this.BattleSpeedPulldown.Selection = -1;
    float[] lists = BattleSpeedController.CreateBattleSpeedList();
    List<ExpansionPurchaseData> expansionPurchaseDataList = new List<ExpansionPurchaseData>((IEnumerable<ExpansionPurchaseData>) MonoSingleton<GameManager>.Instance.Player.GetEnableExpansionDatas(ExpansionPurchaseParam.eExpansionType.TripleSpeed));
    int num = 0;
    if (lists == null)
      return;
    for (int i = 0; i < lists.Length; ++i)
    {
      string label = LocalizedText.Get("sys.PARTYEDITOR_BATTLESET_SELECT_DEFAULT_SPEED");
      if ((double) lists[i] > 1.0)
        label = LocalizedText.Get("sys.PARTYEDITOR_BATTLESET_SELECT_SPEED_SUFFIX", (object) (int) lists[i]);
      if ((double) lists[i] == (double) BattleSpeedController.PrefsSpeed)
        num = i;
      bool is_lock = false;
      if ((double) lists[i] > 2.0)
      {
        is_lock = expansionPurchaseDataList == null || expansionPurchaseDataList.Count <= 0 || expansionPurchaseDataList.FindIndex((Predicate<ExpansionPurchaseData>) (p => (double) p.param.Value == (double) lists[i])) == -1;
        if (is_lock && num == i)
          num = BattleSpeedController.SPEED_LIST.Length - 1;
      }
      this.BattleSpeedPulldown.AddItem(label, i, is_lock);
    }
    this.BattleSpeedPulldown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.SetBattleSpeed);
    this.BattleSpeedPulldown.Selection = num;
  }

  private void SetBattleSpeed(int index)
  {
    BattleSpeedController.SetStartSpeed(BattleSpeedController.CreateBattleSpeedList()[index]);
  }
}
