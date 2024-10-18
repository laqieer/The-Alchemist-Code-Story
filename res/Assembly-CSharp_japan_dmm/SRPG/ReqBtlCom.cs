// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlCom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqBtlCom : WebAPI
  {
    public ReqBtlCom(
      Network.ResponseCallback response,
      bool refresh = false,
      bool tower_progress = false,
      bool is_genesis = false,
      bool is_advance = false)
    {
      this.name = "btl/com";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      if (is_genesis)
        stringBuilder.Append("\"is_genesis\":1,");
      else if (is_advance)
      {
        stringBuilder.Append("\"is_advance\":1,");
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

    [Serializable]
    public class AdvanceStar
    {
      public string area_id;
      public ReqBtlCom.AdvanceStar.Mode[] mode;

      [Serializable]
      public class Mode
      {
        public int[] is_reward;
      }
    }
  }
}
