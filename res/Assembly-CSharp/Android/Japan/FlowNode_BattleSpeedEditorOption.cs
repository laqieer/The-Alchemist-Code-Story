// Decompiled with JetBrains decompiler
// Type: FlowNode_BattleSpeedEditorOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
    if (!(bool) ((Object) this.battleSpeedToggle))
      return;
    if (BattleSpeedController.isPremium)
    {
      if ((bool) ((Object) this.battleSpeedLockObj))
        this.battleSpeedLockObj.SetActive(false);
      this.battleSpeedToggle.interactable = true;
    }
    else
    {
      if ((bool) ((Object) this.battleSpeedLockObj))
        this.battleSpeedLockObj.SetActive(true);
      this.battleSpeedToggle.interactable = false;
      GameUtility.Config_UseBattleSpeed.Value = false;
    }
    this.battleSpeedToggle.isOn = GameUtility.Config_UseBattleSpeed.Value;
  }
}
