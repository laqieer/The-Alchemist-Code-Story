// Decompiled with JetBrains decompiler
// Type: SRPG.DelayStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DelayStart : MonoBehaviour
  {
    public float ActivateInterval = 0.5f;
    private List<GameObject> mChildren = new List<GameObject>();
    private int mActivateIndex;
    private float mInterval;

    private void Start()
    {
      Transform transform = ((Component) this).transform;
      int childCount = transform.childCount;
      for (int index = 0; index < childCount; ++index)
      {
        this.mChildren.Add(((Component) transform.GetChild(index)).gameObject);
        this.mChildren[index].SetActive(false);
      }
      this.mInterval = 0.0f;
    }

    private void Update()
    {
      if (this.mActivateIndex < this.mChildren.Count)
      {
        this.mInterval -= Time.deltaTime;
        if ((double) this.mInterval > 0.0)
          return;
        this.mChildren[this.mActivateIndex++].SetActive(true);
        this.mInterval = this.ActivateInterval;
      }
      else
      {
        for (int index = 0; index < this.mChildren.Count; ++index)
        {
          if (Object.op_Inequality((Object) this.mChildren[index], (Object) null))
            return;
        }
        Object.Destroy((Object) ((Component) this).gameObject);
      }
    }
  }
}
