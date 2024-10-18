// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiPlayResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqMultiPlayResume : WebAPI
  {
    public ReqMultiPlayResume(long btlID, Network.ResponseCallback response)
    {
      this.name = "btl/multi/resume";
      this.body = string.Empty;
      ReqMultiPlayResume reqMultiPlayResume = this;
      reqMultiPlayResume.body = reqMultiPlayResume.body + "\"btlid\":" + (object) btlID;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Quest
    {
      public string iname;
    }

    public class Response
    {
      public ReqMultiPlayResume.Quest quest;
      public int btlid;
      public string app_id;
      public string token;
      public Json_BtlInfo_Multi btlinfo;
    }
  }
}
