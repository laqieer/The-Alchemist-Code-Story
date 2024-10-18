// Decompiled with JetBrains decompiler
// Type: SRPG.UnitProfileWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class UnitProfileWindow : MonoBehaviour
  {
    [FlexibleArray]
    public UnityEngine.UI.Text[] ProfileTexts = new UnityEngine.UI.Text[0];
    public string DebugUnitID;
    private MySound.Voice mUnitVoice;

    private void Start()
    {
      UnitData data = !string.IsNullOrEmpty(this.DebugUnitID) ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.DebugUnitID) : MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      string skinVoiceSheetName = data.GetUnitSkinVoiceSheetName(-1);
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = data.GetUnitSkinVoiceCueName(-1) + "_";
      this.mUnitVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix, false);
      this.PlayProfileVoice();
      DataSource.Bind<UnitData>(this.gameObject, data, false);
      GameParameter.UpdateAll(this.gameObject);
      if (data == null)
        return;
      for (int index = 0; index < this.ProfileTexts.Length; ++index)
      {
        if (!((UnityEngine.Object) this.ProfileTexts[index] == (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.ProfileTexts[index].text))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("unit.");
          stringBuilder.Append(data.UnitParam.iname);
          stringBuilder.Append("_");
          stringBuilder.Append(this.ProfileTexts[index].text);
          this.ProfileTexts[index].text = LocalizedText.Get(stringBuilder.ToString());
        }
      }
    }

    private void PlayProfileVoice()
    {
      if (this.mUnitVoice == null)
        return;
      this.mUnitVoice.Play("chara_0001", 0.0f, false);
    }

    private void OnDestroy()
    {
      if (this.mUnitVoice != null)
      {
        this.mUnitVoice.StopAll(0.0f);
        this.mUnitVoice.Cleanup();
      }
      this.mUnitVoice = (MySound.Voice) null;
    }
  }
}
