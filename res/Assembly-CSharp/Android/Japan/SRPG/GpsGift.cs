// Decompiled with JetBrains decompiler
// Type: SRPG.GpsGift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class GpsGift : WebAPI
  {
    public GpsGift(Vector2 location, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "mail/area";
      stringBuilder.Append("\"location\":{");
      stringBuilder.Append("\"lat\":" + (object) location.x + ",");
      stringBuilder.Append("\"lng\":" + (object) location.y);
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
