// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardFilterTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardFilterTab : MonoBehaviour
  {
    [SerializeField]
    private GameObject mFilteredObject;
    [SerializeField]
    private GameObject mDefaultObject;
    [SerializeField]
    private Toggle mToggle;
    private bool m_IsFiltered;

    public bool isOn
    {
      get => !Object.op_Equality((Object) this.mToggle, (Object) null) && this.mToggle.isOn;
      set
      {
        if (Object.op_Equality((Object) this.mToggle, (Object) null))
          return;
        this.mToggle.isOn = value;
      }
    }

    public void SetIsFiltered(bool isFiltered)
    {
      this.m_IsFiltered = isFiltered;
      GameUtility.SetGameObjectActive(this.mFilteredObject, this.m_IsFiltered);
      GameUtility.SetGameObjectActive(this.mDefaultObject, !this.m_IsFiltered);
    }
  }
}
