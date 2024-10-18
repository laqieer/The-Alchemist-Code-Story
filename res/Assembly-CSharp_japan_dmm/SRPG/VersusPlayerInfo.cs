// Decompiled with JetBrains decompiler
// Type: SRPG.VersusPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class VersusPlayerInfo : MonoBehaviour
  {
    public GameObject template;
    public GameObject parent;

    private void Start()
    {
      if (Object.op_Equality((Object) this.template, (Object) null))
        return;
      this.RefreshData();
    }

    private void RefreshData()
    {
      JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
      if (multiPlayerParam == null)
        return;
      for (int index = 0; index < multiPlayerParam.units.Length; ++index)
      {
        if (multiPlayerParam.units[index] != null)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.template);
          if (Object.op_Inequality((Object) gameObject, (Object) null))
            DataSource.Bind<UnitData>(gameObject, multiPlayerParam.units[index].unit);
          gameObject.SetActive(true);
          gameObject.transform.SetParent(this.parent.transform, false);
        }
      }
    }
  }
}
