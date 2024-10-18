// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqMultiTwResume : WebAPI
  {
    public ReqMultiTwResume(long btlID, Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/resume";
      this.body = string.Empty;
      ReqMultiTwResume reqMultiTwResume = this;
      reqMultiTwResume.body = reqMultiTwResume.body + "\"btlid\":" + (object) btlID;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Quest
    {
      public string iname;
    }

    public class Response
    {
      public int btlid;
      public string app_id;
      public string token;
      public string type;
      public Json_BtlInfo_Multi btlinfo;
    }
  }
}
