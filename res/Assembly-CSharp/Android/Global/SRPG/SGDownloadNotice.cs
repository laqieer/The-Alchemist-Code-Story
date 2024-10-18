// Decompiled with JetBrains decompiler
// Type: SRPG.SGDownloadNotice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SGDownloadNotice : MonoBehaviour
  {
    [SerializeField]
    private SRPG_Button m_decideButton;
    [SerializeField]
    private Toggle m_agreeToggle;
    [SerializeField]
    private Toggle m_bgdlcToggle;

    private void Start()
    {
      this.m_decideButton.interactable = false;
      this.m_bgdlcToggle.isOn = BackgroundDownloader.Instance.IsEnabled;
    }

    public void OnTermsOfUseAgreed()
    {
      this.m_decideButton.interactable = this.m_agreeToggle.isOn;
    }

    public void OnBackgroundDownloadAgreed()
    {
      BackgroundDownloader.Instance.IsEnabled = this.m_bgdlcToggle.isOn;
    }
  }
}
