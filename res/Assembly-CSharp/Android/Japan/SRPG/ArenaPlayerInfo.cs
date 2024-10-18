// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ArenaPlayerInfo : MonoBehaviour
  {
    [Space(10f)]
    public GameObject unit1;
    public GameObject unit2;
    public GameObject unit3;

    private void OnEnable()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(this.gameObject, (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      DataSource.Bind<ArenaPlayer>(this.unit1, dataOfClass, false);
      DataSource.Bind<ArenaPlayer>(this.unit2, dataOfClass, false);
      DataSource.Bind<ArenaPlayer>(this.unit3, dataOfClass, false);
      this.unit1.GetComponent<UnitIcon>().UpdateValue();
      this.unit2.GetComponent<UnitIcon>().UpdateValue();
      this.unit3.GetComponent<UnitIcon>().UpdateValue();
    }
  }
}
