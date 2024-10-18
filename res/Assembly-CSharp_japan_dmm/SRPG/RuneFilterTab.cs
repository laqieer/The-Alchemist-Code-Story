// Decompiled with JetBrains decompiler
// Type: SRPG.RuneFilterTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneFilterTab : MonoBehaviour
  {
    [SerializeField]
    private GameObject m_FilteredObject;
    [SerializeField]
    private GameObject m_DefaultObject;
    [SerializeField]
    private Toggle m_Toggle;
    private bool m_IsFiltered;

    public bool isOn
    {
      get => !Object.op_Equality((Object) this.m_Toggle, (Object) null) && this.m_Toggle.isOn;
      set
      {
        if (Object.op_Equality((Object) this.m_Toggle, (Object) null))
          return;
        this.m_Toggle.isOn = value;
      }
    }

    public void SetIsFiltered(bool isFiltered)
    {
      this.m_IsFiltered = isFiltered;
      GameUtility.SetGameObjectActive(this.m_FilteredObject, this.m_IsFiltered);
      GameUtility.SetGameObjectActive(this.m_DefaultObject, !this.m_IsFiltered);
    }
  }
}
