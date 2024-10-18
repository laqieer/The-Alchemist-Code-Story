// Decompiled with JetBrains decompiler
// Type: SRPG.RoomPlayerViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class RoomPlayerViewer : MonoBehaviour
  {
    public GameObject[] PartyUnitSlots = new GameObject[3];

    private void Start()
    {
      JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
      if (multiPlayerParam == null || multiPlayerParam.units == null || this.PartyUnitSlots == null)
        return;
      for (int index1 = 0; index1 < this.PartyUnitSlots.Length; ++index1)
      {
        for (int index2 = 0; index2 < multiPlayerParam.units.Length; ++index2)
        {
          if (multiPlayerParam.units[index2].slotID == index1)
          {
            DataSource.Bind<UnitData>(this.PartyUnitSlots[index1], multiPlayerParam.units[index2].unit);
            break;
          }
        }
      }
      DataSource.Bind<JSON_MyPhotonPlayerParam>(this.gameObject, multiPlayerParam);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
