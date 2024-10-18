// Decompiled with JetBrains decompiler
// Type: SRPG.OptionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "ToggleChargeDisp", FlowNode.PinTypes.Input, 1)]
  public class OptionDetail : MonoBehaviour, IFlowInterface
  {
    public Slider SoundVolume;
    public Slider MusicVolume;
    public Slider VoiceVolume;
    public LText ChargeDispText;
    [SerializeField]
    private Button battleSpeedButton;
    [SerializeField]
    private LText battleSpeedText;
    [SerializeField]
    private GameObject battleSpeedLockObj;

    public void Activated(int pinID)
    {
      if (pinID != 11)
        return;
      bool is_disp = !GameUtility.Config_ChargeDisp.Value;
      this.UpdateChargeDispText(is_disp);
      GameUtility.Config_ChargeDisp.Value = is_disp;
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      instance.ReflectCastSkill(is_disp);
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.SoundVolume != (UnityEngine.Object) null)
      {
        this.SoundVolume.value = GameUtility.Config_SoundVolume;
        this.SoundVolume.onValueChanged.AddListener((UnityAction<float>) (value => GameUtility.Config_SoundVolume = value));
      }
      if ((UnityEngine.Object) this.MusicVolume != (UnityEngine.Object) null)
      {
        this.MusicVolume.value = GameUtility.Config_MusicVolume;
        this.MusicVolume.onValueChanged.AddListener((UnityAction<float>) (value => GameUtility.Config_MusicVolume = value));
      }
      if ((UnityEngine.Object) this.VoiceVolume != (UnityEngine.Object) null)
      {
        this.VoiceVolume.value = GameUtility.Config_VoiceVolume;
        this.VoiceVolume.onValueChanged.AddListener((UnityAction<float>) (value => GameUtility.Config_VoiceVolume = value));
      }
      if ((UnityEngine.Object) this.battleSpeedButton != (UnityEngine.Object) null)
      {
        if (BattleSpeedController.isPremium)
        {
          this.battleSpeedButton.onClick.AddListener(new UnityAction(this.OnClickBattleSpeedButton));
          if ((bool) ((UnityEngine.Object) this.battleSpeedLockObj))
            this.battleSpeedLockObj.SetActive(false);
          this.battleSpeedButton.interactable = true;
          this.UpdateBattleSpeedText();
        }
        else
        {
          if ((bool) ((UnityEngine.Object) this.battleSpeedLockObj))
            this.battleSpeedLockObj.SetActive(true);
          this.battleSpeedButton.interactable = false;
          this.battleSpeedText.text = LocalizedText.Get("sys.BTN_CHARGE_DISP_OFF");
        }
      }
      this.UpdateChargeDispText(GameUtility.Config_ChargeDisp.Value);
      GameParameter.UpdateAll(this.gameObject);
    }

    private void UpdateChargeDispText(bool is_disp)
    {
      if (!(bool) ((UnityEngine.Object) this.ChargeDispText))
        return;
      if (is_disp)
        this.ChargeDispText.text = LocalizedText.Get("sys.BTN_CHARGE_DISP_ON");
      else
        this.ChargeDispText.text = LocalizedText.Get("sys.BTN_CHARGE_DISP_OFF");
    }

    private void OnClickBattleSpeedButton()
    {
      GameUtility.Config_UseBattleSpeed.Value = !GameUtility.Config_UseBattleSpeed.Value;
      this.UpdateBattleSpeedText();
    }

    private void UpdateBattleSpeedText()
    {
      if (!(bool) ((UnityEngine.Object) this.battleSpeedText))
        return;
      if (GameUtility.Config_UseBattleSpeed.Value)
        this.battleSpeedText.text = LocalizedText.Get("sys.BTN_CHARGE_DISP_ON");
      else
        this.battleSpeedText.text = LocalizedText.Get("sys.BTN_CHARGE_DISP_OFF");
    }

    private void OnDestroy()
    {
      if (BattleSpeedController.BattleTimeConfig)
      {
        if (BattleSpeedController.isAutoOnly)
          GameUtility.Config_UseAutoPlay.Value = true;
        BattleSpeedController.SetUp();
      }
      else
        BattleSpeedController.ResetSpeed();
      GlobalEvent.Invoke("AUTO_TOGGLE_CHANGE_KEY", (object) null);
    }
  }
}
