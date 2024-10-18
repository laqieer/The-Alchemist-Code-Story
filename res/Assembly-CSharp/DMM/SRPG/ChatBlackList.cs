// Decompiled with JetBrains decompiler
// Type: SRPG.ChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ChatBlackList
  {
    public ChatBlackListParam[] lists;
    public int total;

    public void Deserialize(JSON_ChatBlackList json)
    {
      if (json == null)
        return;
      this.lists = (ChatBlackListParam[]) null;
      if (json.blacklist != null)
      {
        this.lists = new ChatBlackListParam[json.blacklist.Length];
        for (int index = 0; index < json.blacklist.Length; ++index)
          this.lists[index] = json.blacklist[index];
      }
      else
        this.lists = new ChatBlackListParam[0];
      this.total = json.total;
    }
  }
}
