// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersus_ItemMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayVersus_ItemMap : MonoBehaviour
  {
    public Text Name;
    public Text Desc;
    public Image Thumnail;

    public void UpdateParam(QuestParam param)
    {
      if ((UnityEngine.Object) this.Name != (UnityEngine.Object) null)
        this.Name.text = param.name;
      if ((UnityEngine.Object) this.Desc != (UnityEngine.Object) null)
        this.Desc.text = param.expr;
      if ((bool) ((UnityEngine.Object) this.Thumnail))
        ;
    }
  }
}
