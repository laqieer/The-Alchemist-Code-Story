// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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

    public int ChannelID
    {
      get
      {
        return this.mChannelID;
      }
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.Begginer != (UnityEngine.Object) null)
        this.Begginer.SetActive(false);
      if ((UnityEngine.Object) this.ChannelName != (UnityEngine.Object) null)
        this.ChannelName.SetActive(false);
      if (!((UnityEngine.Object) this.Fever != (UnityEngine.Object) null))
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
      this.ChannelName.transform.FindChild("text").GetComponent<Text>().text = str;
      this.ChannelName.SetActive(true);
      this.Fever.GetComponent<ImageArray>().ImageIndex = param.fever_level < 15 ? (param.fever_level < 10 ? 0 : 1) : 2;
      this.Fever.SetActive(true);
    }
  }
}
