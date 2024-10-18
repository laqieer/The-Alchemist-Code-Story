// Decompiled with JetBrains decompiler
// Type: SRPG.RuneStateData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneStateData
  {
    public RuneBuffDataBaseState base_state = new RuneBuffDataBaseState();
    public List<RuneBuffDataEvoState> evo_state = new List<RuneBuffDataEvoState>();

    public bool Deserialize(Json_RuneStateData json)
    {
      if (json == null)
        return false;
      this.base_state.Deserialize(json.basic);
      if (json.evo != null)
      {
        this.evo_state.Clear();
        foreach (Json_RuneBuffData json1 in json.evo)
        {
          RuneBuffDataEvoState buffDataEvoState = new RuneBuffDataEvoState();
          buffDataEvoState.Deserialize(json1);
          this.evo_state.Add(buffDataEvoState);
        }
        this.evo_state.Sort(new Comparison<RuneBuffDataEvoState>(RuneStateData.Compare));
      }
      return true;
    }

    private static int Compare(RuneBuffDataEvoState a, RuneBuffDataEvoState b)
    {
      return (int) a.slot - (int) b.slot;
    }

    public Json_RuneStateData Serialize()
    {
      Json_RuneStateData jsonRuneStateData = new Json_RuneStateData();
      jsonRuneStateData.basic = this.base_state.Serialize();
      if (this.evo_state != null && this.evo_state.Count > 0)
      {
        jsonRuneStateData.evo = new Json_RuneBuffData[this.evo_state.Count];
        for (int index = 0; index < this.evo_state.Count; ++index)
          jsonRuneStateData.evo[index] = this.evo_state[index].Serialize();
      }
      return jsonRuneStateData;
    }
  }
}
