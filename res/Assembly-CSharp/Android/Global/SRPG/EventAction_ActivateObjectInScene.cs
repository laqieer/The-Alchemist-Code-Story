// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_ActivateObjectInScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/シーン内オブジェクトを表示", "シーン内のオブジェクトを表示・非表示します", 5592405, 4473992)]
  public class EventAction_ActivateObjectInScene : EventAction
  {
    public EventAction_ActivateObjectInScene.VisibleType visibleType;
    public string objectName;
    public Vector3 objectPosition;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.objectName))
        return;
      TacticsSceneSettings lastScene = TacticsSceneSettings.LastScene;
      if ((UnityEngine.Object) lastScene == (UnityEngine.Object) null)
        return;
      List<Transform> transformList = new List<Transform>();
      float num = float.PositiveInfinity;
      Transform transform = (Transform) null;
      Transform[] componentsInChildren = lastScene.gameObject.GetComponentsInChildren<Transform>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (componentsInChildren[index].name == this.objectName)
        {
          Debug.Log((object) "find");
          transformList.Add(componentsInChildren[index]);
          float sqrMagnitude = (componentsInChildren[index].position - this.objectPosition).sqrMagnitude;
          if ((double) sqrMagnitude < (double) num)
          {
            transform = componentsInChildren[index];
            num = sqrMagnitude;
          }
        }
      }
      if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
        transform.gameObject.SetActive(this.visibleType == EventAction_ActivateObjectInScene.VisibleType.Visible);
      this.ActivateNext();
    }

    public enum VisibleType
    {
      Visible,
      Invisible,
    }
  }
}
