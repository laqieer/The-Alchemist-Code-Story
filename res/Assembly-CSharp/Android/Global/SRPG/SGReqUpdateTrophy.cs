// Decompiled with JetBrains decompiler
// Type: SRPG.SGReqUpdateTrophy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class SGReqUpdateTrophy : WebAPI
  {
    private StringBuilder sb;

    public SGReqUpdateTrophy()
    {
    }

    public SGReqUpdateTrophy(List<TrophyState> trophyprogs, Network.ResponseCallback response, bool finish)
    {
      this.name = "trophy2/claimrewards";
      this.BeginTrophyReqString();
      this.AddTrophyReqString(trophyprogs, finish);
      this.EndTrophyReqString();
      this.body = WebAPI.GetRequestString(this.GetTrophyReqString());
      this.callback = response;
    }

    public string GetTrophyReqString()
    {
      return this.sb.ToString();
    }

    public void BeginTrophyReqString()
    {
      this.sb = WebAPI.GetStringBuilder();
      this.sb.Append("\"device_id\":\"");
      this.sb.Append(MonoSingleton<GameManager>.Instance.DeviceId);
      this.sb.Append("\",");
      this.sb.Append("\"trophies\":[");
    }

    public void EndTrophyReqString()
    {
      this.sb.Append(']');
    }

    public void AddTrophyReqString(List<TrophyState> trophyprogs, bool finish)
    {
      for (int index = 0; index < trophyprogs.Count; ++index)
      {
        this.sb.Append("{\"iname\":\"");
        this.sb.Append(trophyprogs[index].iname);
        this.sb.Append("\"}");
        if (index < trophyprogs.Count - 1)
          this.sb.Append(",");
      }
    }
  }
}
