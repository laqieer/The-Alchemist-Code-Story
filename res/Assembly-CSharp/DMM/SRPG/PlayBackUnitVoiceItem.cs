// Decompiled with JetBrains decompiler
// Type: SRPG.PlayBackUnitVoiceItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class PlayBackUnitVoiceItem : MonoBehaviour
  {
    [SerializeField]
    private Text VoiceName;
    [SerializeField]
    private Toggle PlayingBadge;
    [SerializeField]
    private GameObject LockIcon;
    private UnitData.UnitVoiceCueInfo mUnitVoiceCueInfo;
    private string m_CueName = string.Empty;
    private string m_ButtonName = string.Empty;
    private bool m_IsLocked;
    public PlayBackUnitVoiceItem.TapEvent OnTabEvent;

    public string CueName => this.m_CueName;

    public bool IsLocked => this.m_IsLocked;

    private void Start()
    {
    }

    public void SetUp(UnitData.UnitVoiceCueInfo UnitVoiceCueInfo)
    {
      this.mUnitVoiceCueInfo = UnitVoiceCueInfo;
      this.m_CueName = UnitVoiceCueInfo.cueInfo.name;
      this.m_ButtonName = UnitVoiceCueInfo.voice_name;
    }

    public void Refresh()
    {
      if (this.m_ButtonName == null)
        return;
      this.VoiceName.text = this.m_ButtonName;
    }

    public void SetPlayingBadge(bool value)
    {
      if (Object.op_Equality((Object) this.PlayingBadge, (Object) null))
        DebugUtility.LogError("PlayingBadgeが指定されていません");
      else
        this.PlayingBadge.isOn = value;
    }

    public string GetUnlockConditionsText() => this.mUnitVoiceCueInfo.unlock_conditions_text;

    public void Lock()
    {
      this.m_IsLocked = true;
      SRPG_Button componentInChildren = ((Component) this).GetComponentInChildren<SRPG_Button>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        ((Selectable) componentInChildren).interactable = false;
      if (!Object.op_Inequality((Object) this.LockIcon, (Object) null))
        return;
      this.LockIcon.SetActive(true);
    }

    public void Unlock()
    {
      this.m_IsLocked = false;
      SRPG_Button componentInChildren = ((Component) this).GetComponentInChildren<SRPG_Button>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        ((Selectable) componentInChildren).interactable = true;
      if (!Object.op_Inequality((Object) this.LockIcon, (Object) null))
        return;
      this.LockIcon.SetActive(false);
    }

    public delegate void TapEvent();
  }
}
