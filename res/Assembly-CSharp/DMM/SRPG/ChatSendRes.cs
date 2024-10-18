// Decompiled with JetBrains decompiler
// Type: SRPG.ChatSendRes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ChatSendRes
  {
    public byte is_success;

    public bool IsSuccess => this.is_success == (byte) 1;

    public void Deserialize(JSON_ChatSendRes json)
    {
      if (json == null)
        return;
      this.is_success = json.is_success;
    }
  }
}
