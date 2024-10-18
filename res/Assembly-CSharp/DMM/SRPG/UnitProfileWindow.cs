// Decompiled with JetBrains decompiler
// Type: SRPG.UnitProfileWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitProfileWindow : MonoBehaviour
  {
    public string DebugUnitID;
    private MySound.Voice mUnitVoice;
    [FlexibleArray]
    public UnityEngine.UI.Text[] ProfileTexts = new UnityEngine.UI.Text[0];
    [SerializeField]
    public UnityEngine.UI.Text Illustrator;

    private void Start()
    {
      UnitData data = !string.IsNullOrEmpty(this.DebugUnitID) ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.DebugUnitID) : MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      string skinVoiceSheetName = data.GetUnitSkinVoiceSheetName();
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = data.GetUnitSkinVoiceCueName() + "_";
      this.mUnitVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix);
      this.PlayProfileVoice();
      DataSource.Bind<UnitData>(((Component) this).gameObject, data);
      GameParameter.UpdateAll(((Component) this).gameObject);
      if (data != null)
      {
        for (int index = 0; index < this.ProfileTexts.Length; ++index)
        {
          if (!Object.op_Equality((Object) this.ProfileTexts[index], (Object) null) && !string.IsNullOrEmpty(this.ProfileTexts[index].text))
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
      if (!Object.op_Inequality((Object) this.Illustrator, (Object) null))
        return;
      ArtifactParam selectedSkin = data.GetSelectedSkin();
      string illustrator = selectedSkin == null ? (string) null : selectedSkin.Illustrator;
      if (!string.IsNullOrEmpty(illustrator))
        this.Illustrator.text = illustrator;
      else
        this.Illustrator.text = LocalizedText.Get("unit." + data.UnitParam.iname + "_ILLUST");
    }

    private void PlayProfileVoice()
    {
      if (this.mUnitVoice == null)
        return;
      this.mUnitVoice.Play("chara_0001");
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
