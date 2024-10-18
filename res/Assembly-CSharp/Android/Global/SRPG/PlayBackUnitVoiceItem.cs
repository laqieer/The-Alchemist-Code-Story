// Decompiled with JetBrains decompiler
// Type: SRPG.PlayBackUnitVoiceItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class PlayBackUnitVoiceItem : MonoBehaviour
  {
    private string m_CueName = string.Empty;
    private string m_ButtonName = string.Empty;
    [SerializeField]
    private Text VoiceName;
    [SerializeField]
    private Toggle PlayingBadge;
    [SerializeField]
    private GameObject LockIcon;
    private UnitData.UnitVoiceCueInfo mUnitVoiceCueInfo;
    private bool m_IsLocked;
    public PlayBackUnitVoiceItem.TapEvent OnTabEvent;

    public string CueName
    {
      get
      {
        return this.m_CueName;
      }
    }

    public bool IsLocked
    {
      get
      {
        return this.m_IsLocked;
      }
    }

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
      if ((UnityEngine.Object) this.PlayingBadge == (UnityEngine.Object) null)
        DebugUtility.LogError("PlayingBadgeが指定されていません");
      else
        this.PlayingBadge.isOn = value;
    }

    public string GetUnlockConditionsText()
    {
      return this.mUnitVoiceCueInfo.unlock_conditions_text;
    }

    public void Lock()
    {
      this.m_IsLocked = true;
      SRPG_Button componentInChildren = this.GetComponentInChildren<SRPG_Button>();
      if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
        componentInChildren.interactable = false;
      if (!((UnityEngine.Object) this.LockIcon != (UnityEngine.Object) null))
        return;
      this.LockIcon.SetActive(true);
    }

    public void Unlock()
    {
      this.m_IsLocked = false;
      SRPG_Button componentInChildren = this.GetComponentInChildren<SRPG_Button>();
      if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null)
        componentInChildren.interactable = true;
      if (!((UnityEngine.Object) this.LockIcon != (UnityEngine.Object) null))
        return;
      this.LockIcon.SetActive(false);
    }

    public delegate void TapEvent();
  }
}
