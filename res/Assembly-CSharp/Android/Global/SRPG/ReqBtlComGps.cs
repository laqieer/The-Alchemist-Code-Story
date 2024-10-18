// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComGps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ReqBtlComGps : WebAPI
  {
    public ReqBtlComGps(Network.ResponseCallback response, Vector2 location)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "btl/com/areaquest";
      stringBuilder.Append("\"location\":{");
      stringBuilder.Append("\"lat\":" + (object) location.x + ",");
      stringBuilder.Append("\"lng\":" + (object) location.y);
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
