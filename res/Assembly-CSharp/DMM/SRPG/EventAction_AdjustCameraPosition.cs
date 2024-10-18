// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_AdjustCameraPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("カメラ/調整", "指定したアクターが画面内に収まるようにカメラ位置を調整します。", 5592405, 4473992)]
  public class EventAction_AdjustCameraPosition : EventAction
  {
    public CameraInterpSpeed InterpSpeed;
    [SerializeField]
    private string[] ActorIDs = new string[1];

    public override void OnActivate()
    {
      Vector3 vector3_1 = Vector3.zero;
      List<GameObject> gameObjectList = new List<GameObject>();
      for (int index = 0; index < this.ActorIDs.Length; ++index)
      {
        GameObject actor = EventAction.FindActor(this.ActorIDs[index]);
        if (!Object.op_Equality((Object) actor, (Object) null))
        {
          vector3_1 = Vector3.op_Addition(vector3_1, actor.transform.position);
          gameObjectList.Add(actor);
        }
      }
      if (gameObjectList.Count <= 0)
      {
        this.ActivateNext();
      }
      else
      {
        Vector3 vector3_2 = Vector3.op_Multiply(vector3_1, 1f / (float) gameObjectList.Count);
        Camera main = Camera.main;
        Transform transform = ((Component) main).transform;
        vector3_2.y += GameSettings.Instance.GameCamera_UnitHeightOffset;
        Vector3 position = Vector3.op_Subtraction(vector3_2, Vector3.op_Multiply(((Component) main).transform.forward, GameSettings.Instance.GameCamera_EventCameraDistance));
        ObjectAnimator.Get((Component) main).AnimateTo(position, transform.rotation, this.InterpSpeed.ToSpan(), ObjectAnimator.CurveType.EaseInOut);
      }
    }

    public override void Update()
    {
      if (ObjectAnimator.Get((Component) Camera.main).isMoving)
        return;
      this.ActivateNext();
    }
  }
}
