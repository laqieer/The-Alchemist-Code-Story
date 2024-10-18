// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
