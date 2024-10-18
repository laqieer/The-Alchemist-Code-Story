// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTrophyAchievedQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTrophyAchievedQuest : WebAPI
  {
    public ReqTrophyAchievedQuest(string iname, Network.ResponseCallback response)
    {
      this.name = "trophy/history";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iname\":\"" + iname + "\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
