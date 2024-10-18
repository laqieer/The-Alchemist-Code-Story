// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraParamLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitTobiraParamLevel : MonoBehaviour
  {
    [SerializeField]
    private GameObject OnGO;
    [SerializeField]
    private GameObject OffGO;
    [SerializeField]
    private int OwnLevel;

    public void Refresh(int targetLevel)
    {
      if ((UnityEngine.Object) this.OnGO == (UnityEngine.Object) null || (UnityEngine.Object) this.OffGO == (UnityEngine.Object) null)
        return;
      bool flag = targetLevel >= this.OwnLevel;
      this.OnGO.SetActive(flag);
      this.OffGO.SetActive(!flag);
    }
  }
}
