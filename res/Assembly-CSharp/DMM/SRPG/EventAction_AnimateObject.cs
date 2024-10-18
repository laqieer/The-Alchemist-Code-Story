// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_AnimateObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      if (Object.op_Inequality((Object) clip, (Object) null))
        animation.clip = clip;
      animation.Play(animationID);
    }

    public override void OnActivate()
    {
      GameObject[] gameObjects = GameObjectID.FindGameObjects(this.ObjectID);
      for (int index = 0; index < gameObjects.Length; ++index)
      {
        if (Object.op_Inequality((Object) gameObjects[index], (Object) null))
        {
          Animation component = gameObjects[index].GetComponent<Animation>();
          if (Object.op_Inequality((Object) component, (Object) null))
            EventAction_AnimateObject.PlayAnimation(component, this.AnimationID);
        }
      }
      for (int index = 0; index < this.Sequence.SpawnedObjects.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.Sequence.SpawnedObjects[index], (Object) null) && ((Object) this.Sequence.SpawnedObjects[index]).name == this.ObjectID)
        {
          Animation component = this.Sequence.SpawnedObjects[index].GetComponent<Animation>();
          if (Object.op_Inequality((Object) component, (Object) null))
            EventAction_AnimateObject.PlayAnimation(component, this.AnimationID);
        }
      }
      this.ActivateNext();
    }
  }
}
