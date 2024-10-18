// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ArenaPlayerInfo : MonoBehaviour
  {
    [Space(10f)]
    public GameObject unit1;
    public GameObject unit2;
    public GameObject unit3;

    private void OnEnable() => this.UpdateValue();

    public void UpdateValue()
    {
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(((Component) this).gameObject, (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      DataSource.Bind<ArenaPlayer>(this.unit1, dataOfClass);
      DataSource.Bind<ArenaPlayer>(this.unit2, dataOfClass);
      DataSource.Bind<ArenaPlayer>(this.unit3, dataOfClass);
      this.unit1.GetComponent<UnitIcon>().UpdateValue();
      this.unit2.GetComponent<UnitIcon>().UpdateValue();
      this.unit3.GetComponent<UnitIcon>().UpdateValue();
    }
  }
}
