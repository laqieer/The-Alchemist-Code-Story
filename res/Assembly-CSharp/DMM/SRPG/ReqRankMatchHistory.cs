﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRankMatchHistory : WebAPI
  {
    public ReqRankMatchHistory(Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/history";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class ResponceHistoryResult
    {
      public long time;
      public string result;
      public int[] beats;
      public string token;
      public int beatcnt;
    }

    [Serializable]
    public class ResponceHistoryList
    {
      public int id;
      public int score;
      public int enemyscore;
      public int value;
      public long time_start;
      public long time_end;
      public ReqRankMatchHistory.ResponceHistoryResult result;
      public Json_Friend enemy;
      public int type;
    }

    [Serializable]
    public class ResponceHistoryOption
    {
      public int totalPage;
    }

    [Serializable]
    public class ResponceHistory
    {
      public ReqRankMatchHistory.ResponceHistoryList[] list;
      public ReqRankMatchHistory.ResponceHistoryOption option;
    }

    [Serializable]
    public class Response
    {
      public ReqRankMatchHistory.ResponceHistory histories;
    }
  }
}
