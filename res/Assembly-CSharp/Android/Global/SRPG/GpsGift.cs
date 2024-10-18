// Decompiled with JetBrains decompiler
// Type: SRPG.GpsGift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
