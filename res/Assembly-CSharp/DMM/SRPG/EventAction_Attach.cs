// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Attach
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      if (Object.op_Equality((Object) actor, (Object) null))
        Debug.LogError((object) (this.AttachmentID + "は存在しません。"));
      if (!this.Detach)
      {
        if (Object.op_Equality((Object) gameObject, (Object) null))
          Debug.LogError((object) (this.TargetID + "は存在しません。"));
        else if (!string.IsNullOrEmpty(this.BoneName))
        {
          Transform childRecursively = GameUtility.findChildRecursively(gameObject.transform, this.BoneName);
          if (Object.op_Equality((Object) childRecursively, (Object) null))
          {
            gameObject = (GameObject) null;
            Debug.LogError((object) (this.TargetID + "の子供に" + this.BoneName + "は存在しません。"));
          }
          else
            gameObject = ((Component) childRecursively).gameObject;
        }
      }
      if (this.Detach)
      {
        if (Object.op_Inequality((Object) actor, (Object) null))
        {
          DefaultParentReference component = actor.GetComponent<DefaultParentReference>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            actor.transform.SetParent(component.DefaultParent, true);
            Object.DestroyImmediate((Object) component);
          }
        }
      }
      else if (Object.op_Inequality((Object) actor, (Object) null) && Object.op_Inequality((Object) gameObject, (Object) null))
      {
        if (Object.op_Equality((Object) actor.GetComponent<DefaultParentReference>(), (Object) null))
          actor.gameObject.AddComponent<DefaultParentReference>().DefaultParent = actor.transform.parent;
        actor.transform.SetParent(gameObject.transform, false);
      }
      this.ActivateNext();
    }
  }
}
