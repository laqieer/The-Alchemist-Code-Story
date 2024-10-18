// Decompiled with JetBrains decompiler
// Type: LayoutElementOverwrite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class LayoutElementOverwrite : MonoBehaviour
{
  private Vector3 prevPos = Vector3.zero;
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

  public void Refresh()
  {
    if ((Object) this.BaseGameObject == (Object) null)
    {
      DebugUtility.LogWarning("LayoutElementOverwrite.cs => Apply():Base Game Object is Nothing.");
    }
    else
    {
      if ((Object) this.TargetLayoutElement == (Object) null)
      {
        this.TargetLayoutElement = this.GetComponent<LayoutElement>();
        if ((Object) this.TargetLayoutElement == (Object) null)
        {
          DebugUtility.LogWarning("LayoutElementOverwrite.cs => Apply():Target Layout Element is Nothing.");
          return;
        }
      }
      Vector3 vector3 = this.TargetLayoutElement.transform.InverseTransformPoint(this.BaseGameObject.transform.position);
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
      this.TargetLayoutElement.enabled = false;
      this.TargetLayoutElement.enabled = true;
    }
  }

  private void Update()
  {
    if ((Object) this.BaseGameObject == (Object) null)
      return;
    this.BaseGameObject.transform.SetAsLastSibling();
    Vector3 vector3 = this.TargetLayoutElement.transform.InverseTransformPoint(this.BaseGameObject.transform.position);
    if ((double) this.prevPos.x == (double) vector3.x && (double) this.prevPos.y == (double) vector3.y)
      return;
    this.prevPos = vector3;
    this.Refresh();
  }
}
