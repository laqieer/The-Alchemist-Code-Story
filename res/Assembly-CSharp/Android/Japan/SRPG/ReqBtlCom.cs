// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlCom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;

namespace SRPG
{
  public class ReqBtlCom : WebAPI
  {
    public ReqBtlCom(Network.ResponseCallback response, bool refresh = false, bool tower_progress = false, bool is_genesis = false)
    {
      this.name = "btl/com";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      if (is_genesis)
      {
        stringBuilder.Append("\"is_genesis\":1,");
      }
      else
      {
        if (refresh)
          stringBuilder.Append("\"event\":1,");
        if (tower_progress)
          stringBuilder.Append("\"is_tower\":1,");
      }
      string body = stringBuilder.ToString();
      if (!string.IsNullOrEmpty(body))
        body = body.Remove(body.Length - 1);
      this.body = WebAPI.GetRequestString(body);
      this.callback = response;
    }

    [Serializable]
    public class GenesisStar
    {
      public string area_id;
      public ReqBtlCom.GenesisStar.Mode[] mode;

      [Serializable]
      public class Mode
      {
        public int[] is_reward;
      }
    }
  }
}
