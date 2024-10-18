// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusFreematchStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusFreematchStatus : WebAPI
  {
    public ReqVersusFreematchStatus(Network.ResponseCallback response)
    {
      this.name = "vs/freematch/status";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class EnableTimeSchedule
    {
      public long expired;
      public long next;
    }

    public class Response
    {
      public ReqVersusFreematchStatus.EnableTimeSchedule enabletime;
    }
  }
}
