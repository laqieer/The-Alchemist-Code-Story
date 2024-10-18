// Decompiled with JetBrains decompiler
// Type: SRPG.DelayStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

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
      Transform transform = this.transform;
      int childCount = transform.childCount;
      for (int index = 0; index < childCount; ++index)
      {
        this.mChildren.Add(transform.GetChild(index).gameObject);
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
          if ((UnityEngine.Object) this.mChildren[index] != (UnityEngine.Object) null)
            return;
        }
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
    }
  }
}
