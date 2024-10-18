// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ChatChannelItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject Begginer;
    [SerializeField]
    private GameObject ChannelName;
    [SerializeField]
    private GameObject Fever;
    private int mChannelID;

    public int ChannelID => this.mChannelID;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Begginer, (Object) null))
        this.Begginer.SetActive(false);
      if (Object.op_Inequality((Object) this.ChannelName, (Object) null))
        this.ChannelName.SetActive(false);
      if (!Object.op_Inequality((Object) this.Fever, (Object) null))
        return;
      this.Fever.SetActive(false);
    }

    public void Refresh(ChatChannelParam param)
    {
      if (param == null)
        return;
      if (param.category_id == 2)
        this.Begginer.SetActive(true);
      this.mChannelID = param.id;
      string str = "CH " + param.id.ToString();
      if (!string.IsNullOrEmpty(param.name))
        str = param.name;
      ((Component) this.ChannelName.transform.Find("text")).GetComponent<Text>().text = str;
      this.ChannelName.SetActive(true);
      this.Fever.GetComponent<ImageArray>().ImageIndex = param.fever_level < 15 ? (param.fever_level < 10 ? 0 : 1) : 2;
      this.Fever.SetActive(true);
    }
  }
}
