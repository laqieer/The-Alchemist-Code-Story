﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerFloorReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;

namespace SRPG
{
  public class ReqTowerFloorReset : WebAPI
  {
    public ReqTowerFloorReset(string tower_iname, string floor_iname, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "tower/floor/reset";
      stringBuilder.Append(WebAPI.KeyValueToString(nameof (tower_iname), tower_iname));
      stringBuilder.Append(",");
      stringBuilder.Append(WebAPI.KeyValueToString(nameof (floor_iname), floor_iname));
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public class JSON_CoinParam
    {
      public int free;
      public int paid;
      public int com;
    }

    [Serializable]
    public class Json_Response
    {
      public ReqTowerFloorReset.JSON_CoinParam coin;
    }
  }
}
