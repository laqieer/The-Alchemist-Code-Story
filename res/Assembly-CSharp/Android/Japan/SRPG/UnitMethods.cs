// Decompiled with JetBrains decompiler
// Type: SRPG.UnitMethods
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitMethods : MonoBehaviour
  {
    public void SetUnitToActivePartySlot()
    {
      PartyUnitSlot active = PartyUnitSlot.Active;
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (!((UnityEngine.Object) active != (UnityEngine.Object) null))
        return;
      PartyData dataOfClass2 = DataSource.FindDataOfClass<PartyData>(active.gameObject, (PartyData) null);
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
