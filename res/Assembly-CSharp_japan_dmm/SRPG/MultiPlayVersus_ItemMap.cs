// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersus_ItemMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MultiPlayVersus_ItemMap : MonoBehaviour
  {
    public Text Name;
    public Text Desc;
    public Image Thumnail;

    public void UpdateParam(QuestParam param)
    {
      if (Object.op_Inequality((Object) this.Name, (Object) null))
        this.Name.text = param.name;
      if (Object.op_Inequality((Object) this.Desc, (Object) null))
        this.Desc.text = param.expr;
      if (!Object.op_Implicit((Object) this.Thumnail))
        ;
    }
  }
}
