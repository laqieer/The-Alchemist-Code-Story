// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersus_ItemMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
