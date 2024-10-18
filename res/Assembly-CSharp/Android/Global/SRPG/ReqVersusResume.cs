// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusResume : WebAPI
  {
    public ReqVersusResume(long btlID, Network.ResponseCallback response)
    {
      this.name = "vs/resume";
      this.body = string.Empty;
      ReqVersusResume reqVersusResume = this;
      reqVersusResume.body = reqVersusResume.body + "\"btlid\":" + (object) btlID;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Quest
    {
      public string iname;
    }

    public class Response
    {
      public ReqVersusResume.Quest quest;
      public int btlid;
      public string app_id;
      public string token;
      public string type;
      public Json_BtlInfo_Multi btlinfo;
    }
  }
}
