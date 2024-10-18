// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUpdateTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ReqUpdateTrophy : WebAPI
  {
    private StringBuilder sb;
    private bool is_begin;

    public ReqUpdateTrophy()
    {
    }

    public ReqUpdateTrophy(
      List<TrophyState> trophyprogs,
      SRPG.Network.ResponseCallback response,
      bool finish,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "trophy/exec";
      this.BeginTrophyReqString();
      this.AddTrophyReqString(trophyprogs, finish);
      this.EndTrophyReqString();
      if (Object.op_Implicit((Object) MonoSingleton<GameManager>.Instance))
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (instance.Player.TrophyStarMissionInfo != null && instance.Player.TrophyStarMissionInfo.Daily != null)
          this.sb.Append(",\"trophy_star_ymd\":" + (object) instance.Player.TrophyStarMissionInfo.Daily.YyMmDd);
      }
      this.body = WebAPI.GetRequestString(this.GetTrophyReqString());
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    public string GetTrophyReqString() => this.sb.ToString();

    public void BeginTrophyReqString()
    {
      this.is_begin = true;
      this.sb = WebAPI.GetStringBuilder();
      this.sb.Append("\"trophyprogs\":[");
    }

    public void EndTrophyReqString() => this.sb.Append(']');

    public void AddTrophyReqString(List<TrophyState> trophyprogs, bool finish)
    {
      int num1 = 0;
      int num2 = 0;
      if (finish)
        num2 = TimeManager.ServerTime.ToYMD();
      for (int index1 = 0; index1 < trophyprogs.Count; ++index1)
      {
        if (this.is_begin)
          this.is_begin = false;
        else
          this.sb.Append(',');
        this.sb.Append("{\"iname\":\"");
        this.sb.Append(trophyprogs[index1].iname);
        this.sb.Append("\",");
        this.sb.Append("\"pts\":[");
        for (int index2 = 0; index2 < trophyprogs[index1].Count.Length; ++index2)
        {
          if (index2 > 0)
            this.sb.Append(',');
          this.sb.Append(trophyprogs[index1].Count[index2]);
        }
        this.sb.Append("],");
        this.sb.Append("\"ymd\":");
        this.sb.Append(trophyprogs[index1].StartYMD);
        if (finish)
        {
          this.sb.Append(",\"rewarded_at\":");
          this.sb.Append(num2);
        }
        this.sb.Append("}");
        ++num1;
      }
    }
  }
}
