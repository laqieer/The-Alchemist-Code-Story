// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidSelect : WebAPI
  {
    public ReqRaidSelect(Network.ResponseCallback response)
    {
      this.name = "raidboss/select";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    [Serializable]
    public class Response
    {
      public JSON_RaidBossData raidboss_current;
    }
  }
}
