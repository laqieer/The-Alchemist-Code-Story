// Decompiled with JetBrains decompiler
// Type: SRPG.OptionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
  }
}
