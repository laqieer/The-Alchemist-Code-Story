// Decompiled with JetBrains decompiler
// Type: SRPG.ChangeMaterialList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ChangeMaterialList : MonoBehaviour
  {
    [SerializeField]
    private Material mMat;
    [SerializeField]
    private Image[] ChangeList;

    private void Start()
    {
      if (this.ChangeList == null)
      {
        this.ChangeList = new Image[1];
        Image component = ((Component) this).gameObject.gameObject.GetComponent<Image>();
        if (Object.op_Equality((Object) component, (Object) null))
          return;
        this.ChangeList[0] = component;
      }
      if (!Object.op_Inequality((Object) this.mMat, (Object) null))
        return;
      this.SetMaterial(this.mMat);
    }

    public void SetMaterial(Material mat)
    {
      for (int index = 0; index < this.ChangeList.Length; ++index)
      {
        if (!Object.op_Equality((Object) this.ChangeList[index], (Object) null))
          ((Graphic) this.ChangeList[index]).material = mat;
      }
    }

    public void SetColor(Color color)
    {
      for (int index = 0; index < this.ChangeList.Length; ++index)
      {
        if (!Object.op_Equality((Object) this.ChangeList[index], (Object) null))
        {
          ((Graphic) this.ChangeList[index]).color = color;
          ((Graphic) this.ChangeList[index]).material = (Material) null;
        }
      }
    }
  }
}
