// Decompiled with JetBrains decompiler
// Type: LayoutElementOverwrite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class LayoutElementOverwrite : MonoBehaviour
{
  [SerializeField]
  private LayoutElement TargetLayoutElement;
  [SerializeField]
  private GameObject BaseGameObject;
  [SerializeField]
  private bool minWidth;
  [SerializeField]
  private bool minHeight;
  [SerializeField]
  private bool preferredWidth;
  [SerializeField]
  private bool preferredHeight;
  [SerializeField]
  private bool flexibleWidth;
  [SerializeField]
  private bool flexibleHeight;
  private Vector3 prevPos = Vector3.zero;

  public void Refresh()
  {
    if (Object.op_Equality((Object) this.BaseGameObject, (Object) null))
    {
      DebugUtility.LogWarning("LayoutElementOverwrite.cs => Apply():Base Game Object is Nothing.");
    }
    else
    {
      if (Object.op_Equality((Object) this.TargetLayoutElement, (Object) null))
      {
        this.TargetLayoutElement = ((Component) this).GetComponent<LayoutElement>();
        if (Object.op_Equality((Object) this.TargetLayoutElement, (Object) null))
        {
          DebugUtility.LogWarning("LayoutElementOverwrite.cs => Apply():Target Layout Element is Nothing.");
          return;
        }
      }
      Vector3 vector3 = ((Component) this.TargetLayoutElement).transform.InverseTransformPoint(this.BaseGameObject.transform.position);
      RectTransform component = this.BaseGameObject.GetComponent<RectTransform>();
      vector3.y -= component.sizeDelta.y;
      vector3.x += component.sizeDelta.x;
      if (this.minHeight)
        this.TargetLayoutElement.minHeight = -vector3.y;
      if (this.preferredHeight)
        this.TargetLayoutElement.preferredHeight = -vector3.y;
      if (this.flexibleHeight)
        this.TargetLayoutElement.flexibleHeight = -vector3.y;
      if (this.minWidth)
        this.TargetLayoutElement.minWidth = vector3.x;
      if (this.preferredWidth)
        this.TargetLayoutElement.preferredWidth = vector3.x;
      if (this.flexibleWidth)
        this.TargetLayoutElement.flexibleWidth = vector3.x;
      ((Behaviour) this.TargetLayoutElement).enabled = false;
      ((Behaviour) this.TargetLayoutElement).enabled = true;
    }
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.BaseGameObject, (Object) null))
      return;
    this.BaseGameObject.transform.SetAsLastSibling();
    Vector3 vector3 = ((Component) this.TargetLayoutElement).transform.InverseTransformPoint(this.BaseGameObject.transform.position);
    if ((double) this.prevPos.x == (double) vector3.x && (double) this.prevPos.y == (double) vector3.y)
      return;
    this.prevPos = vector3;
    this.Refresh();
  }
}
