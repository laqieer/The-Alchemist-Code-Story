// Decompiled with JetBrains decompiler
// Type: SRPG.OptionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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
      if (!Object.op_Implicit((Object) instance))
        return;
      instance.ReflectCastSkill(is_disp);
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.SoundVolume, (Object) null))
      {
        this.SoundVolume.value = GameUtility.Config_SoundVolume;
        Slider.SliderEvent onValueChanged = this.SoundVolume.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (OptionDetail.\u003C\u003Ef__am\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          OptionDetail.\u003C\u003Ef__am\u0024cache0 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__0));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache0 = OptionDetail.\u003C\u003Ef__am\u0024cache0;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache0);
      }
      if (Object.op_Inequality((Object) this.MusicVolume, (Object) null))
      {
        this.MusicVolume.value = GameUtility.Config_MusicVolume;
        Slider.SliderEvent onValueChanged = this.MusicVolume.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (OptionDetail.\u003C\u003Ef__am\u0024cache1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          OptionDetail.\u003C\u003Ef__am\u0024cache1 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__1));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache1 = OptionDetail.\u003C\u003Ef__am\u0024cache1;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache1);
      }
      if (Object.op_Inequality((Object) this.VoiceVolume, (Object) null))
      {
        this.VoiceVolume.value = GameUtility.Config_VoiceVolume;
        Slider.SliderEvent onValueChanged = this.VoiceVolume.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (OptionDetail.\u003C\u003Ef__am\u0024cache2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          OptionDetail.\u003C\u003Ef__am\u0024cache2 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__2));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache2 = OptionDetail.\u003C\u003Ef__am\u0024cache2;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache2);
      }
      if (Object.op_Inequality((Object) this.battleSpeedButton, (Object) null))
      {
        if (BattleSpeedController.isPremium)
        {
          // ISSUE: method pointer
          ((UnityEvent) this.battleSpeedButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClickBattleSpeedButton)));
          if (Object.op_Implicit((Object) this.battleSpeedLockObj))
            this.battleSpeedLockObj.SetActive(false);
          ((Selectable) this.battleSpeedButton).interactable = true;
          this.UpdateBattleSpeedText();
        }
        else
        {
          if (Object.op_Implicit((Object) this.battleSpeedLockObj))
            this.battleSpeedLockObj.SetActive(true);
          ((Selectable) this.battleSpeedButton).interactable = false;
          this.battleSpeedText.text = LocalizedText.Get("sys.BTN_CHARGE_DISP_OFF");
        }
      }
      this.UpdateChargeDispText(GameUtility.Config_ChargeDisp.Value);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void UpdateChargeDispText(bool is_disp)
    {
      if (!Object.op_Implicit((Object) this.ChargeDispText))
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
      if (!Object.op_Implicit((Object) this.battleSpeedText))
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
