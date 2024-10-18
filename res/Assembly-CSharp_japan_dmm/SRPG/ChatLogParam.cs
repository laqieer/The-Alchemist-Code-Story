// Decompiled with JetBrains decompiler
// Type: SRPG.ChatLogParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ChatLogParam
  {
    public long id;
    public byte message_type;
    public string fuid;
    public string uid;
    public string icon;
    public string skin_iname;
    public string job_iname;
    public string message;
    public int stamp_id;
    public string name;
    public long posted_at;
    public long gid;
    public string guild_name;
    public int lower_level;
    public int is_auto_approval;
    public string award_id;
    public int policy;
    public int report_id;

    public ChatLogParam.eChatMessageType messageType
    {
      get => (ChatLogParam.eChatMessageType) this.message_type;
      set => this.message_type = (byte) value;
    }

    public enum eChatMessageType : byte
    {
      UNKNOWN,
      MESSAGE,
      STAMP,
      SYSTEM,
      GUILD_INVITE,
      SYSTEM_GUILD,
      GUILDRAID_PARTY,
    }
  }
}
