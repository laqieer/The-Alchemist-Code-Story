// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiPlayResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
