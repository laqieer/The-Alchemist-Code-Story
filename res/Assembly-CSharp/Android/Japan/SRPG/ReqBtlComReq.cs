﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ReqBtlComReq : WebAPI
  {
    public ReqBtlComReq(string iname, string fuid, SupportData support, Network.ResponseCallback response, bool multi, int partyIndex, bool isHost = false, int plid = 0, int seat = 0, Vector2 location = default (Vector2), RankingQuestParam rankingQuestParam = null)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = !multi ? "btl/com/req" : "btl/multi/req";
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\",");
      if (partyIndex >= 0)
      {
        stringBuilder.Append("\"partyid\":");
        stringBuilder.Append(partyIndex);
        stringBuilder.Append(",");
      }
      if (multi)
      {
        stringBuilder.Append("\"token\":\"");
        stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
        stringBuilder.Append("\",");
        stringBuilder.Append("\"host\":\"");
        stringBuilder.Append(!isHost ? "0" : "1");
        stringBuilder.Append("\",");
        stringBuilder.Append("\"plid\":\"");
        stringBuilder.Append(plid);
        stringBuilder.Append("\",");
        stringBuilder.Append("\"seat\":\"");
        stringBuilder.Append(seat);
        stringBuilder.Append("\",");
      }
      else
      {
        stringBuilder.Append("\"req_at\":");
        stringBuilder.Append(Network.GetServerTime());
        stringBuilder.Append(",");
      }
      stringBuilder.Append("\"btlparam\":{\"help\":{\"fuid\":");
      stringBuilder.Append("\"" + fuid + "\"");
      if (support != null && support.Unit != null)
      {
        stringBuilder.Append(",\"elem\":" + (object) support.Unit.SupportElement);
        stringBuilder.Append(",\"iname\":\"" + support.Unit.UnitID + "\"");
      }
      stringBuilder.Append("}");
      if (!multi && rankingQuestParam != null)
      {
        stringBuilder.Append(",\"quest_ranking\":{");
        stringBuilder.Append("\"schedule_id\":");
        stringBuilder.Append(rankingQuestParam.schedule_id);
        stringBuilder.Append(",");
        stringBuilder.Append("\"type\":");
        stringBuilder.Append((int) rankingQuestParam.type);
        stringBuilder.Append("}");
      }
      stringBuilder.Append("},");
      stringBuilder.Append("\"location\":{");
      stringBuilder.Append("\"lat\":" + (object) location.x + ",");
      stringBuilder.Append("\"lng\":" + (object) location.y);
      stringBuilder.Append("}");
      DebugMenu.Log("APIReq", stringBuilder.ToString());
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}