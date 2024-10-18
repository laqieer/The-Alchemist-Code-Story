// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNPCUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGNPCUnitParam : VersusDraftUnitParam
  {
    public bool Deserialize(JSON_GvGNPCUnitParam json)
    {
      if (json == null)
        return false;
      json.draft_unit_id = json.id;
      return this.Deserialize(json.id, (JSON_VersusDraftUnitParam) json);
    }

    public GvGPartyUnit CreateUnitData()
    {
      Json_Unit jsonUnit = this.GetJson_Unit();
      GvGPartyUnit unitData = new GvGPartyUnit();
      unitData.Deserialize(jsonUnit);
      return unitData;
    }

    public static GvGPartyUnit CreateNPCUnitData(int id)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGPartyUnit) null;
      List<GvGNPCUnitParam> mGvGnpcUnitParam = MonoSingleton<GameManager>.Instance.mGvGNPCUnitParam;
      if (mGvGnpcUnitParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/mGvGNPCUnitParam no data!</color>"));
        return (GvGPartyUnit) null;
      }
      return mGvGnpcUnitParam.Find((Predicate<GvGNPCUnitParam>) (u => u != null && u.Id == (long) id))?.CreateUnitData();
    }
  }
}
