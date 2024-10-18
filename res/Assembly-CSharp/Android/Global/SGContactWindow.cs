// Decompiled with JetBrains decompiler
// Type: SGContactWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SGContactWindow : MonoBehaviour
{
  [SerializeField]
  private Dropdown issueDropdown;
  [SerializeField]
  private Text emailText;
  [SerializeField]
  private Text messageText;
  [SerializeField]
  private Text playerNameText;
  [SerializeField]
  private Text deviceModelText;
  [SerializeField]
  private Text clientVerText;

  private void Start()
  {
    this.UpdateDropdownList();
    this.UpdateText();
  }

  protected void UpdateDropdownList()
  {
    for (int index = 0; index < this.issueDropdown.get_options().Count; ++index)
      this.issueDropdown.get_options()[index].text = LocalizedText.Get(this.issueDropdown.get_options()[index].text);
    this.issueDropdown.captionText.text = this.issueDropdown.get_options()[0].text;
  }

  protected void UpdateText()
  {
    this.playerNameText.text = !PlayerPrefs.HasKey("PlayerName") ? string.Empty : LocalizedText.Get("sys.CONTACT_PLAYER_NAME") + ": " + PlayerPrefs.GetString("PlayerName");
    this.deviceModelText.text = LocalizedText.Get("sys.CONTACT_DEVICE_MODEL") + ": " + SystemInfo.deviceModel;
    this.clientVerText.text = "v" + MyApplicationPlugin.version;
  }
}
