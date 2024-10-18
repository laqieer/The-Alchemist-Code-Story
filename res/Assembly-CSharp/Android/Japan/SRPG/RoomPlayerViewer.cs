// Decompiled with JetBrains decompiler
// Type: SRPG.RoomPlayerViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
            DataSource.Bind<UnitData>(this.PartyUnitSlots[index1], multiPlayerParam.units[index2].unit, false);
            break;
          }
        }
      }
      DataSource.Bind<JSON_MyPhotonPlayerParam>(this.gameObject, multiPlayerParam, false);
      DataSource.Bind<ViewGuildData>(this.gameObject, new ViewGuildData()
      {
        id = multiPlayerParam.guild_id,
        name = multiPlayerParam.guild_name
      }, false);
      SerializeValueBehaviour component = this.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.list.SetField(GuildSVB_Key.GUILD_ID, multiPlayerParam.guild_id);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
