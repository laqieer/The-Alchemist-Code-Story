// Decompiled with JetBrains decompiler
// Type: SRPG.ChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
