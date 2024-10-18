// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqVersusMake : WebAPI
  {
    public ReqVersusMake(
      VERSUS_TYPE type,
      string comment,
      string iname,
      bool isLine = false,
      Network.ResponseCallback response = null)
    {
      this.name = "vs/" + type.ToString().ToLower() + "match/make";
      this.body = string.Empty;
      ReqVersusMake reqVersusMake1 = this;
      reqVersusMake1.body = reqVersusMake1.body + "\"comment\":\"" + JsonEscape.Escape(comment) + "\",";
      ReqVersusMake reqVersusMake2 = this;
      reqVersusMake2.body = reqVersusMake2.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\",";
      ReqVersusMake reqVersusMake3 = this;
      reqVersusMake3.body = reqVersusMake3.body + "\"Line\":" + (object) (!isLine ? 0 : 1);
      ReqVersusMake reqVersusMake4 = this;
      reqVersusMake4.body = reqVersusMake4.body + ",\"is_draft\":" + (object) (!GlobalVars.IsVersusDraftMode ? 0 : 1);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public string token;
      public string owner_name;
      public int roomid;
      public string btl_ver;
    }
  }
}
