// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemComposit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemComposit : WebAPI
  {
    public ReqItemComposit(string iname, bool is_cmn, Network.ResponseCallback response)
    {
      this.name = "item/gousei";
      int num = !is_cmn ? 0 : 1;
      this.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\",\"is_cmn\":" + (object) num);
      this.callback = response;
    }
  }
}
