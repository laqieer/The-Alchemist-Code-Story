// Decompiled with JetBrains decompiler
// Type: SRPG.DeriveListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DeriveListItem : MonoBehaviour
  {
    [HeaderBar("▼派生先のスキル/アビリティの罫線")]
    [SerializeField]
    private RectTransform m_DeriveLineV;
    [SerializeField]
    private RectTransform m_DeriveLineH;

    public void SetLineActive(bool lineActive, bool verticalActive)
    {
      GameUtility.SetGameObjectActive((Component) this.m_DeriveLineH, lineActive);
      if (lineActive)
        GameUtility.SetGameObjectActive((Component) this.m_DeriveLineV, verticalActive);
      else
        GameUtility.SetGameObjectActive((Component) this.m_DeriveLineV, false);
    }
  }
}
