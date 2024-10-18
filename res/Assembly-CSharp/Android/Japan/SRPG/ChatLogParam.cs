// Decompiled with JetBrains decompiler
// Type: SRPG.ChatLogParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

    public ChatLogParam.eChatMessageType messageType
    {
      get
      {
        return (ChatLogParam.eChatMessageType) this.message_type;
      }
      set
      {
        this.message_type = (byte) value;
      }
    }

    public enum eChatMessageType : byte
    {
      UNKNOWN,
      MESSAGE,
      STAMP,
      SYSTEM,
      GUILD_INVITE,
    }
  }
}
