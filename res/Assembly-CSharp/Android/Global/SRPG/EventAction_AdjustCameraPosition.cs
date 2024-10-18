// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_AdjustCameraPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("カメラ/調整", "指定したアクターが画面内に収まるようにカメラ位置を調整します。", 5592405, 4473992)]
  public class EventAction_AdjustCameraPosition : EventAction
  {
    [SerializeField]
    private string[] ActorIDs = new string[1];
    public CameraInterpSpeed InterpSpeed;

    public override void OnActivate()
    {
      Vector3 zero = Vector3.zero;
      List<GameObject> gameObjectList = new List<GameObject>();
      for (int index = 0; index < this.ActorIDs.Length; ++index)
      {
        GameObject actor = EventAction.FindActor(this.ActorIDs[index]);
        if (!((UnityEngine.Object) actor == (UnityEngine.Object) null))
        {
          zero += actor.transform.position;
          gameObjectList.Add(actor);
        }
      }
      if (gameObjectList.Count <= 0)
      {
        this.ActivateNext();
      }
      else
      {
        Vector3 vector3 = zero * (1f / (float) gameObjectList.Count);
        UnityEngine.Camera main = UnityEngine.Camera.main;
        Transform transform = main.transform;
        vector3.y += GameSettings.Instance.GameCamera_UnitHeightOffset;
        Vector3 position = vector3 - main.transform.forward * GameSettings.Instance.GameCamera_EventCameraDistance;
        ObjectAnimator.Get((Component) main).AnimateTo(position, transform.rotation, this.InterpSpeed.ToSpan(), ObjectAnimator.CurveType.EaseInOut);
      }
    }

    public override void Update()
    {
      if (ObjectAnimator.Get((Component) UnityEngine.Camera.main).isMoving)
        return;
      this.ActivateNext();
    }
  }
}
