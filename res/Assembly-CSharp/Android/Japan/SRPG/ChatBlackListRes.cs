// Decompiled with JetBrains decompiler
// Type: SRPG.ChatBlackListRes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ChatBlackListRes
  {
    public byte is_success;

    public bool IsSuccess
    {
      get
      {
        return this.is_success == (byte) 1;
      }
    }

    public void Deserialize(JSON_ChatBlackListRes json)
    {
      if (json == null)
        return;
      this.is_success = json.is_success;
    }
  }
}
