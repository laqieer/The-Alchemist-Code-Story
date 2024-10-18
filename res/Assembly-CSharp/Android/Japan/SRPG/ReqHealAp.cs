// Decompiled with JetBrains decompiler
// Type: SRPG.ReqHealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqHealAp : WebAPI
  {
    public ReqHealAp(long iid, int num, Network.ResponseCallback response)
    {
      this.name = "item/addstm";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\" : ");
      stringBuilder.Append(iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"num\" : ");
      stringBuilder.Append(num);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
