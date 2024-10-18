// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendBlockApply
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqFriendBlockApply : WebAPI
  {
    public ReqFriendBlockApply(string[] _friends, string[] _blocks, Network.ResponseCallback _response)
    {
      this.name = "friend/multi/req";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"friends\":[");
      if (_friends != null && _friends.Length > 0)
      {
        for (int index = 0; index < _friends.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("\"");
          stringBuilder.Append(_friends[index]);
          stringBuilder.Append("\"");
        }
      }
      stringBuilder.Append("]");
      stringBuilder.Append(",");
      stringBuilder.Append("\"blocks\":[");
      if (_blocks != null && _blocks.Length > 0)
      {
        for (int index = 0; index < _blocks.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("\"");
          stringBuilder.Append(_blocks[index]);
          stringBuilder.Append("\"");
        }
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = _response;
    }
  }
}
