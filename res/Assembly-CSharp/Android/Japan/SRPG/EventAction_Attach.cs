// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Attach
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("アタッチデタッチ", "指定オブジェクトを別オブジェクトにアタッチ/デタッチします。", 5592405, 4473992)]
  public class EventAction_Attach : EventAction
  {
    public bool Detach;
    public string AttachmentID;
    [HideInInspector]
    public string TargetID;
    [HideInInspector]
    public string BoneName;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.AttachmentID);
      GameObject gameObject = EventAction.FindActor(this.TargetID);
      if ((UnityEngine.Object) actor == (UnityEngine.Object) null)
        Debug.LogError((object) (this.AttachmentID + "は存在しません。"));
      if (!this.Detach)
      {
        if ((UnityEngine.Object) gameObject == (UnityEngine.Object) null)
          Debug.LogError((object) (this.TargetID + "は存在しません。"));
        else if (!string.IsNullOrEmpty(this.BoneName))
        {
          Transform childRecursively = GameUtility.findChildRecursively(gameObject.transform, this.BoneName);
          if ((UnityEngine.Object) childRecursively == (UnityEngine.Object) null)
          {
            gameObject = (GameObject) null;
            Debug.LogError((object) (this.TargetID + "の子供に" + this.BoneName + "は存在しません。"));
          }
          else
            gameObject = childRecursively.gameObject;
        }
      }
      if (this.Detach)
      {
        if ((UnityEngine.Object) actor != (UnityEngine.Object) null)
        {
          DefaultParentReference component = actor.GetComponent<DefaultParentReference>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            actor.transform.SetParent(component.DefaultParent, true);
            UnityEngine.Object.DestroyImmediate((UnityEngine.Object) component);
          }
        }
      }
      else if ((UnityEngine.Object) actor != (UnityEngine.Object) null && (UnityEngine.Object) gameObject != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) actor.GetComponent<DefaultParentReference>() == (UnityEngine.Object) null)
          actor.gameObject.AddComponent<DefaultParentReference>().DefaultParent = actor.transform.parent;
        actor.transform.SetParent(gameObject.transform, false);
      }
      this.ActivateNext();
    }
  }
}
