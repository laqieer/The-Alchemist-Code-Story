// Decompiled with JetBrains decompiler
// Type: SRPG.RoomPlayerViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      DataSource.Bind<JSON_MyPhotonPlayerParam>(((Component) this).gameObject, multiPlayerParam);
      DataSource.Bind<ViewGuildData>(((Component) this).gameObject, new ViewGuildData()
      {
        id = multiPlayerParam.guild_id,
        name = multiPlayerParam.guild_name
      });
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.list.SetField(GuildSVB_Key.GUILD_ID, multiPlayerParam.guild_id);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
