// Decompiled with JetBrains decompiler
// Type: SRPG.GachaDropAnimationParts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GachaDropAnimationParts : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] DropObjs;

    private void ResetObj()
    {
      if (this.DropObjs == null)
        return;
      for (int index = 0; index < this.DropObjs.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.DropObjs[index], (Object) null))
          this.DropObjs[index].SetActive(false);
      }
    }

    public void Setup(GameObject drop_object)
    {
      this.ResetObj();
      if (Object.op_Equality((Object) drop_object, (Object) null) || this.DropObjs == null || this.DropObjs.Length <= 0 || !Object.op_Inequality((Object) this.DropObjs[0], (Object) null))
        return;
      this.DropObjs[0].SetActive(true);
      drop_object.transform.SetParent(this.DropObjs[0].transform, false);
    }

    public void Setup(GameObject[] drop_objects, GachaDropData[] drops)
    {
      this.ResetObj();
      if (drop_objects == null || drop_objects.Length <= 0 || drops == null || drops.Length <= 0 || this.DropObjs == null || this.DropObjs.Length <= 0)
        return;
      int num = drop_objects.Length <= this.DropObjs.Length ? drop_objects.Length : this.DropObjs.Length;
      for (int index = 0; index < num; ++index)
      {
        GameObject dropObj = this.DropObjs[index];
        if (Object.op_Inequality((Object) dropObj, (Object) null))
        {
          dropObj.SetActive(true);
          drop_objects[index].transform.SetParent(dropObj.transform, false);
          GachaAnimationParts component = dropObj.GetComponent<GachaAnimationParts>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.Setup(drops[index].FirstExcite);
        }
      }
    }

    public void Setup(GachaDropData[] drops)
    {
      this.ResetObj();
      if (drops == null || drops.Length <= 0)
      {
        DebugUtility.LogError("排出データが存在しません.");
      }
      else
      {
        if (this.DropObjs == null || this.DropObjs.Length <= 0)
          return;
        int num = drops.Length <= this.DropObjs.Length ? drops.Length : this.DropObjs.Length;
        for (int index = 0; index < num; ++index)
        {
          GameObject dropObj = this.DropObjs[index];
          if (Object.op_Inequality((Object) dropObj, (Object) null))
          {
            dropObj.SetActive(true);
            GachaAnimationParts componentInChildren = dropObj.GetComponentInChildren<GachaAnimationParts>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null))
              componentInChildren.Setup(drops[index].excites[0]);
          }
        }
      }
    }
  }
}
