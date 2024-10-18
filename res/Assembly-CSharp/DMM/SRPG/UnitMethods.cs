// Decompiled with JetBrains decompiler
// Type: SRPG.UnitMethods
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitMethods : MonoBehaviour
  {
    public void SetUnitToActivePartySlot()
    {
      PartyUnitSlot active = PartyUnitSlot.Active;
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (!Object.op_Inequality((Object) active, (Object) null))
        return;
      PartyData dataOfClass2 = DataSource.FindDataOfClass<PartyData>(((Component) active).gameObject, (PartyData) null);
      if (dataOfClass1 != null)
      {
        for (int index = 0; index < dataOfClass2.MAX_UNIT; ++index)
        {
          if (dataOfClass2.GetUnitUniqueID(index) == dataOfClass1.UniqueID)
            dataOfClass2.SetUnitUniqueID(index, 0L);
        }
        dataOfClass2.SetUnitUniqueID(active.Index, dataOfClass1.UniqueID);
      }
      else
        dataOfClass2.SetUnitUniqueID(active.Index, 0L);
      FlowNodeEvent<FlowNode_OnPartyChange>.Invoke();
    }
  }
}
