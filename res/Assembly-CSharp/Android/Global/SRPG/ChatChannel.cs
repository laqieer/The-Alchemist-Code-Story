// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ChatChannel
  {
    public ChatChannelParam[] channels;

    public void Deserialize(JSON_ChatChannel json)
    {
      if (json == null || json.channels == null)
        return;
      this.channels = new ChatChannelParam[json.channels.Length];
      ChatChannelMasterParam[] chatChannelMaster = MonoSingleton<GameManager>.Instance.GetChatChannelMaster();
      for (int index = 0; index < json.channels.Length; ++index)
      {
        this.channels[index] = json.channels[index];
        if (chatChannelMaster.Length >= this.channels[index].id)
        {
          this.channels[index].category_id = (int) chatChannelMaster[this.channels[index].id - 1].category_id;
          this.channels[index].name = chatChannelMaster[this.channels[index].id - 1].name;
        }
      }
    }
  }
}
