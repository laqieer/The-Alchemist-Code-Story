// Decompiled with JetBrains decompiler
// Type: SRPG.DeriveListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
