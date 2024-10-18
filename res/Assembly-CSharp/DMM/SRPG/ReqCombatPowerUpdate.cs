// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCombatPowerUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ReqCombatPowerUpdate : WebAPI
  {
    public ReqCombatPowerUpdate(
      long combatPower,
      string trophyProgs,
      string bingoProgs,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "combat_power/update";
      this.body = this.GetRequestParamString(combatPower, trophyProgs, bingoProgs);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    private string GetRequestParamString(long combatPower, string trophyProgs, string bingoProgs)
    {
      List<string> stringList = new List<string>();
      stringList.Add(WebAPI.KeyValueToString("combat_power", combatPower));
      if (!string.IsNullOrEmpty(trophyProgs))
        stringList.Add(trophyProgs);
      if (!string.IsNullOrEmpty(bingoProgs))
        stringList.Add(bingoProgs);
      return string.Join(",", stringList.ToArray());
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
    }
  }
}
