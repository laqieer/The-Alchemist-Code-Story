// Decompiled with JetBrains decompiler
// Type: FlowNode_BattleSpeedAutoSetting
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[FlowNode.NodeType("System/Battle/Speed/BattleSpeedAutoSetting", 32741)]
[FlowNode.Pin(1, "オプション画面での倍速切り替え", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(2, "オートプレイON", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(3, "オートプレイOFF", FlowNode.PinTypes.Input, 3)]
[FlowNode.Pin(4, "倍速機能ON", FlowNode.PinTypes.Input, 4)]
[FlowNode.Pin(5, "倍速機能OFF", FlowNode.PinTypes.Input, 5)]
[FlowNode.Pin(6, "オートプレイ操作可", FlowNode.PinTypes.Input, 6)]
[FlowNode.Pin(7, "オートプレイ操作不可", FlowNode.PinTypes.Input, 7)]
[FlowNode.Pin(8, "倍速機能 操作可", FlowNode.PinTypes.Input, 8)]
[FlowNode.Pin(9, "倍速機能 操作不可", FlowNode.PinTypes.Input, 9)]
[FlowNode.Pin(10, "倍速切り替え", FlowNode.PinTypes.Input, 10)]
public class FlowNode_BattleSpeedAutoSetting : FlowNode
{
  private const int INPUT_CHANGE_BATTLE_SPEED = 1;
  private const int INPUT_AUTO_PLAY_ON = 2;
  private const int INPUT_AUTO_PLAY_OFF = 3;
  private const int INPUT_BATTLE_SPEED_ON = 4;
  private const int INPUT_BATTLE_SPEED_OFF = 5;
  private const int INPUT_AUTO_PLAY_INTERACTABLE_ON = 6;
  private const int INPUT_AUTO_PLAY_INTERACTABLE_OFF = 7;
  private const int INPUT_BATTLE_SPEED_INTERACTABLE_ON = 8;
  private const int INPUT_BATTLE_SPEED_INTERACTABLE_OFF = 9;
  private const int INPUT_BATTLE_SPEED_CHANGE = 10;
  [SerializeField]
  private Toggle battleSpeedToggle;
  [SerializeField]
  private GameObject battleSpeedLockObj;
  [SerializeField]
  private Toggle autoPlayToggle;
  [SerializeField]
  private bool isShowButtonNotVip = true;
  [SerializeField]
  private ImageArray battleSpeedArray;
  [SerializeField]
  private Button battleSpeedChangeButton;
  private int battleSpeedIndex;
  private float[] speedList;
  public const string AUTO_TOGGLE_CHANGE_KEY = "AUTO_TOGGLE_CHANGE_KEY";

  protected override void Awake()
  {
    base.Awake();
    BattleSpeedController.BattleTimeConfig = true;
    if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) && SceneBattle.Instance.CurrentQuest != null && (SceneBattle.Instance.CurrentQuest.IsGvG || SceneBattle.Instance.CurrentQuest.type == QuestTypes.Arena))
      BattleSpeedController.SetupEnableBattleSpeedList(MonoSingleton<GameManager>.Instance.Player.GetExpansionDatas(ExpansionPurchaseParam.eExpansionType.TripleSpeed));
    this.speedList = BattleSpeedController.EnableSpeedList;
    for (int index = 0; index < this.speedList.Length; ++index)
    {
      if ((double) this.speedList[index] == (double) BattleSpeedController.PrefsSpeed)
      {
        this.battleSpeedIndex = index;
        break;
      }
    }
    if (Object.op_Inequality((Object) this.battleSpeedArray, (Object) null))
      this.battleSpeedArray.ImageIndex = this.battleSpeedIndex;
    if (!BattleSpeedController.IsShowSpeedButton())
    {
      GameUtility.SetGameObjectActive((Component) this.battleSpeedChangeButton, false);
      GameUtility.SetGameObjectActive((Component) this.battleSpeedToggle, false);
    }
    else if (BattleSpeedController.isPremium)
    {
      if (Object.op_Implicit((Object) this.battleSpeedLockObj))
        this.battleSpeedLockObj.SetActive(false);
      if (Object.op_Implicit((Object) this.battleSpeedToggle))
        ((Selectable) this.battleSpeedToggle).interactable = true;
      if (Object.op_Implicit((Object) this.battleSpeedChangeButton))
        ((Selectable) this.battleSpeedChangeButton).interactable = true;
      if (MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.IsAutoRepeatQuestMeasuring)
      {
        BattleSpeedController.BattleTimeConfig = true;
        if (Object.op_Implicit((Object) this.battleSpeedToggle))
          ((Selectable) this.battleSpeedToggle).interactable = false;
        if (Object.op_Implicit((Object) this.battleSpeedChangeButton))
          ((Selectable) this.battleSpeedChangeButton).interactable = false;
        if (Object.op_Inequality((Object) this.battleSpeedArray, (Object) null))
          this.battleSpeedArray.ImageIndex = BattleSpeedController.SPEED_LIST.Length - 1;
      }
      if (!Object.op_Implicit((Object) this.battleSpeedToggle))
        return;
      GameUtility.SetToggle(this.battleSpeedToggle, BattleSpeedController.BattleTimeConfig);
    }
    else
    {
      if (Object.op_Implicit((Object) this.battleSpeedLockObj))
        this.battleSpeedLockObj.SetActive(true);
      if (this.isShowButtonNotVip)
      {
        if (!Object.op_Implicit((Object) this.battleSpeedChangeButton) || !Object.op_Implicit((Object) this.battleSpeedToggle))
          return;
        ((Selectable) this.battleSpeedToggle).interactable = false;
        GameUtility.SetToggle(this.battleSpeedToggle, false);
      }
      else
      {
        if (!Object.op_Implicit((Object) this.battleSpeedToggle))
          return;
        GameUtility.SetGameObjectActive(((Component) this.battleSpeedToggle).gameObject, false);
      }
    }
  }

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 1:
        if (BattleSpeedController.isAutoOnly && BattleSpeedController.BattleTimeConfig && Object.op_Implicit((Object) this.autoPlayToggle))
          this.autoPlayToggle.isOn = true;
        if (!Object.op_Implicit((Object) this.battleSpeedToggle))
          break;
        this.battleSpeedToggle.isOn = BattleSpeedController.BattleTimeConfig;
        break;
      case 3:
        if (!BattleSpeedController.isAutoOnly)
          break;
        BattleSpeedController.BattleTimeConfig = false;
        if (Object.op_Implicit((Object) this.battleSpeedToggle))
          this.battleSpeedToggle.isOn = false;
        BattleSpeedController.ResetSpeed();
        break;
      case 4:
        if (!BattleSpeedController.isPremium)
          break;
        if (BattleSpeedController.isAutoOnly && Object.op_Implicit((Object) this.autoPlayToggle))
          this.autoPlayToggle.isOn = true;
        BattleSpeedController.BattleTimeConfig = true;
        BattleSpeedController.SetUp();
        break;
      case 5:
        BattleSpeedController.BattleTimeConfig = false;
        BattleSpeedController.ResetSpeed();
        break;
      case 6:
        this.ChangeToggleInteractable(this.autoPlayToggle, true);
        break;
      case 7:
        this.ChangeToggleInteractable(this.autoPlayToggle, false);
        break;
      case 8:
        this.ChangeToggleInteractable(this.battleSpeedToggle, true);
        GameUtility.SetButtonIntaractable(this.battleSpeedChangeButton, true);
        if (!Object.op_Inequality((Object) this.battleSpeedArray, (Object) null))
          break;
        this.battleSpeedArray.ImageIndex = this.battleSpeedIndex;
        break;
      case 9:
        this.ChangeToggleInteractable(this.battleSpeedToggle, false);
        GameUtility.SetButtonIntaractable(this.battleSpeedChangeButton, false);
        if (!Object.op_Inequality((Object) this.battleSpeedArray, (Object) null))
          break;
        this.battleSpeedArray.ImageIndex = this.battleSpeedIndex;
        break;
      case 10:
        this.OnChangeBattleSpeed();
        break;
    }
  }

  protected override void OnDestroy()
  {
    if (!Object.op_Inequality((Object) MonoSingleton<TimeManager>.Instance, (Object) null))
      return;
    BattleSpeedController.EndBattle();
  }

  private void ChangeToggleInteractable(Toggle target, bool is_interactable)
  {
    if (Object.op_Equality((Object) target, (Object) null))
      return;
    ((Selectable) target).interactable = is_interactable;
  }

  private void OnChangeBattleSpeed()
  {
    this.battleSpeedIndex = this.battleSpeedIndex + 1 < this.speedList.Length ? this.battleSpeedIndex + 1 : 0;
    if (Object.op_Inequality((Object) this.battleSpeedArray, (Object) null))
      this.battleSpeedArray.ImageIndex = this.battleSpeedIndex;
    BattleSpeedController.SetSpeed(this.speedList[this.battleSpeedIndex]);
  }
}
