// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_ActivateObjectInScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
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
      if (Object.op_Equality((Object) lastScene, (Object) null))
        return;
      List<Transform> transformList = new List<Transform>();
      float num = float.PositiveInfinity;
      Transform transform = (Transform) null;
      Transform[] componentsInChildren = ((Component) lastScene).gameObject.GetComponentsInChildren<Transform>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (((Object) componentsInChildren[index]).name == this.objectName)
        {
          Debug.Log((object) "find");
          transformList.Add(componentsInChildren[index]);
          Vector3 vector3 = Vector3.op_Subtraction(componentsInChildren[index].position, this.objectPosition);
          float sqrMagnitude = ((Vector3) ref vector3).sqrMagnitude;
          if ((double) sqrMagnitude < (double) num)
          {
            transform = componentsInChildren[index];
            num = sqrMagnitude;
          }
        }
      }
      if (Object.op_Inequality((Object) transform, (Object) null))
        ((Component) transform).gameObject.SetActive(this.visibleType == EventAction_ActivateObjectInScene.VisibleType.Visible);
      this.ActivateNext();
    }

    public enum VisibleType
    {
      Visible,
      Invisible,
    }
  }
}
