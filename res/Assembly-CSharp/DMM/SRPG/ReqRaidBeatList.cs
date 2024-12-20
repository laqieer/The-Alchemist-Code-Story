﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidBeatList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidBeatList : WebAPI
  {
    public ReqRaidBeatList(Network.ResponseCallback response)
    {
      this.name = "raidboss/rescue/beatlist";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public int round;
      public int[] beat_stamp_index;
      public int is_get_complete;
    }
  }
}
