// Decompiled with JetBrains decompiler
// Type: SRPG.GpsGift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;
using UnityEngine;

#nullable disable
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
