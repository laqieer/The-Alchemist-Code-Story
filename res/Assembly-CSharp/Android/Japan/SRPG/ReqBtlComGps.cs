// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComGps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ReqBtlComGps : WebAPI
  {
    public ReqBtlComGps(Network.ResponseCallback response, Vector2 location, bool isMulti = false)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "btl/com/areaquest";
      stringBuilder.Append("\"location\":{");
      stringBuilder.Append("\"lat\":" + (object) location.x + ",");
      stringBuilder.Append("\"lng\":" + (object) location.y);
      stringBuilder.Append("}");
      stringBuilder.Append(",");
      stringBuilder.Append("\"is_multi\":");
      stringBuilder.Append(!isMulti ? 0 : 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
