// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlComResume : WebAPI
  {
    public ReqBtlComResume(long btlid, Network.ResponseCallback response)
    {
      this.name = "btl/com/resume";
      this.body = "\"btlid\":" + (object) btlid;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
