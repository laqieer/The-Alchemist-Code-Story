// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCompensateReceiveTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqCompensateReceiveTrophy : WebAPI
  {
    public ReqCompensateReceiveTrophy(Network.ResponseCallback response)
    {
      this.name = "compensate/receive/trophy";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
