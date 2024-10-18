// Decompiled with JetBrains decompiler
// Type: FlowNode_BattleSpeedAutoSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.UI;

[FlowNode.NodeType("System/Battle/Speed/BattleSpeedAutoSetting", 32741)]
[FlowNode.Pin(1, "オプション画面での倍速切り替え", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(2, "オートプレイON", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(3, "オートプレイOFF", FlowNode.PinTypes.Input, 3)]
[FlowNode.Pin(4, "倍速機能ON", FlowNode.PinTypes.Input, 4)]
[FlowNode.Pin(5, "倍速機能OFF", FlowNode.PinTypes.Input, 5)]
public class FlowNode_BattleSpeedAutoSetting : FlowNode
{
  [SerializeField]
  private bool isShowButtonNotVip = true;
  private const int INPUT_CHANGE_BATTLE_SPEED = 1;
  private const int INPUT_AUTO_PLAY_ON = 2;
  private const int INPUT_AUTO_PLAY_OFF = 3;
  private const int INPUT_BATTLE_SPEED_ON = 4;
  private const int INPUT_BATTLE_SPEED_OFF = 5;
  [SerializeField]
  private Toggle battleSpeedToggle;
  [SerializeField]
  private GameObject battleSpeedLockObj;
  [SerializeField]
  private Toggle autoPlayToggle;
  public const string AUTO_TOGGLE_CHANGE_KEY = "AUTO_TOGGLE_CHANGE_KEY";

  protected override void Awake()
  {
    base.Awake();
    BattleSpeedController.BattleTimeConfig = GameUtility.Config_UseBattleSpeed.Value;
    if (!BattleSpeedController.IsShowSpeedButton() && (bool) ((UnityEngine.Object) this.battleSpeedToggle))
      this.battleSpeedToggle.gameObject.SetActive(false);
    else if (BattleSpeedController.isPremium)
    {
      if ((bool) ((UnityEngine.Object) this.battleSpeedLockObj))
        this.battleSpeedLockObj.SetActive(false);
      this.battleSpeedToggle.interactable = true;
      GameUtility.SetToggle(this.battleSpeedToggle, BattleSpeedController.BattleTimeConfig);
    }
    else
    {
      if ((bool) ((UnityEngine.Object) this.battleSpeedLockObj))
        this.battleSpeedLockObj.SetActive(true);
      if (this.isShowButtonNotVip)
      {
        this.battleSpeedToggle.interactable = false;
        GameUtility.SetToggle(this.battleSpeedToggle, false);
      }
      else
        this.battleSpeedToggle.gameObject.SetActive(false);
    }
  }

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 1:
        if (BattleSpeedController.isAutoOnly && BattleSpeedController.BattleTimeConfig)
          this.autoPlayToggle.isOn = true;
        this.battleSpeedToggle.isOn = BattleSpeedController.BattleTimeConfig;
        break;
      case 3:
        if (!BattleSpeedController.isAutoOnly)
          break;
        BattleSpeedController.BattleTimeConfig = false;
        this.battleSpeedToggle.isOn = false;
        BattleSpeedController.ResetSpeed();
        break;
      case 4:
        if (!BattleSpeedController.isPremium)
          break;
        if (BattleSpeedController.isAutoOnly)
          this.autoPlayToggle.isOn = true;
        BattleSpeedController.BattleTimeConfig = true;
        BattleSpeedController.SetUp();
        break;
      case 5:
        BattleSpeedController.BattleTimeConfig = false;
        BattleSpeedController.ResetSpeed();
        break;
    }
  }

  protected override void OnDestroy()
  {
    if (!((UnityEngine.Object) MonoSingleton<TimeManager>.Instance != (UnityEngine.Object) null))
      return;
    BattleSpeedController.EndBattle();
  }
}
