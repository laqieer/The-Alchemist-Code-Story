// Decompiled with JetBrains decompiler
// Type: SRPG.UnitRentalSelectPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitRentalSelectPartyWindow : MonoBehaviour
  {
    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      UnitData rentalUnit = instance.Player.GetRentalUnit();
      if (rentalUnit == null)
        return;
      DataSource.Bind<UnitData>(((Component) this).gameObject, rentalUnit);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
