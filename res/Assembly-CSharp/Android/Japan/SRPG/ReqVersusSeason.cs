// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusSeason
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusSeason : WebAPI
  {
    public ReqVersusSeason(Network.ResponseCallback response)
    {
      this.name = "vs/towermatch/season";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public int unreadmail;
    }
  }
}
