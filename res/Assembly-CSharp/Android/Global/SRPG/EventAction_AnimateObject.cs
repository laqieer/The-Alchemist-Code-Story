// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_AnimateObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("オブジェクト/アニメーション (レガシー)", "オブジェクトにアタッチされたアニメーションを再生します。", 5592405, 4473992)]
  public class EventAction_AnimateObject : EventAction
  {
    public string ObjectID;
    [HideInInspector]
    public string AnimationID;

    private static void PlayAnimation(Animation animation, string animationID)
    {
      AnimationClip clip = animation.GetClip(animationID);
      if ((UnityEngine.Object) clip != (UnityEngine.Object) null)
        animation.clip = clip;
      animation.Play(animationID);
    }

    public override void OnActivate()
    {
      GameObject[] gameObjects = GameObjectID.FindGameObjects(this.ObjectID);
      for (int index = 0; index < gameObjects.Length; ++index)
      {
        if ((UnityEngine.Object) gameObjects[index] != (UnityEngine.Object) null)
        {
          Animation component = gameObjects[index].GetComponent<Animation>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            EventAction_AnimateObject.PlayAnimation(component, this.AnimationID);
        }
      }
      for (int index = 0; index < this.Sequence.SpawnedObjects.Count; ++index)
      {
        if ((UnityEngine.Object) this.Sequence.SpawnedObjects[index] != (UnityEngine.Object) null && this.Sequence.SpawnedObjects[index].name == this.ObjectID)
        {
          Animation component = this.Sequence.SpawnedObjects[index].GetComponent<Animation>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            EventAction_AnimateObject.PlayAnimation(component, this.AnimationID);
        }
      }
      this.ActivateNext();
    }
  }
}
