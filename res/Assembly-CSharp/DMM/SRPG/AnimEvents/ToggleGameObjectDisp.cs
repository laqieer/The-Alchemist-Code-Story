// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleGameObjectDisp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ToggleGameObjectDisp : AnimEvent
  {
    public string[] NameGameObjects;
    private List<GameObject> mGoTargetLists;

    private List<GameObject> GetGoTargets(GameObject go)
    {
      if (this.mGoTargetLists == null)
      {
        this.mGoTargetLists = new List<GameObject>();
        if (this.NameGameObjects != null)
        {
          for (int index = 0; index < this.NameGameObjects.Length; ++index)
          {
            string nameGameObject = this.NameGameObjects[index];
            if (!string.IsNullOrEmpty(nameGameObject))
            {
              Transform childRecursively = GameUtility.findChildRecursively(go.transform, nameGameObject);
              if (Object.op_Implicit((Object) childRecursively))
                this.mGoTargetLists.Add(((Component) childRecursively).gameObject);
            }
          }
        }
        if (this.mGoTargetLists.Count == 0 && Object.op_Implicit((Object) go))
          this.mGoTargetLists.Add(go);
      }
      return this.mGoTargetLists;
    }

    public override void OnStart(GameObject go)
    {
      this.mGoTargetLists = (List<GameObject>) null;
      List<GameObject> goTargets = this.GetGoTargets(go);
      if (goTargets == null)
        return;
      for (int index = 0; index < goTargets.Count; ++index)
        goTargets[index].SetActive(false);
    }

    public override void OnEnd(GameObject go)
    {
      List<GameObject> goTargets = this.GetGoTargets(go);
      if (goTargets == null)
        return;
      for (int index = 0; index < goTargets.Count; ++index)
        goTargets[index].SetActive(true);
      this.mGoTargetLists = (List<GameObject>) null;
    }
  }
}
