// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusSeason
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
