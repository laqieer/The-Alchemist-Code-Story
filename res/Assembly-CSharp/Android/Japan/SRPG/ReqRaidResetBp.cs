﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidResetBp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidResetBp : WebAPI
  {
    public ReqRaidResetBp(Network.ResponseCallback response)
    {
      this.name = "raidboss/reset/bp";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public Json_RaidBP bp;
    }
  }
}
