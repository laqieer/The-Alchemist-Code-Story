// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemCompositAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqItemCompositAll : WebAPI
  {
    public ReqItemCompositAll(string iname, bool is_cmn, Network.ResponseCallback response)
    {
      this.name = "item/gouseiall";
      int num = !is_cmn ? 0 : 1;
      this.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\",\"is_cmn\":" + (object) num);
      this.callback = response;
    }
  }
}
