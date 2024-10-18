// Decompiled with JetBrains decompiler
// Type: EventMessageAuto
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;

#nullable disable
public class EventMessageAuto : MonoBehaviour
{
  private GameObject mEnableObject;

  private void Awake()
  {
    RectTransform component = ((Component) this).GetComponent<RectTransform>();
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    if ((double) (((Rect) ref safeArea).width / (float) Screen.width) < 1.0)
    {
      float num = (float) Screen.height - ((Rect) ref safeArea).height;
      component.anchoredPosition = new Vector2(component.anchoredPosition.x, component.anchoredPosition.y + num);
    }
    EventScript.OnAutoFlagChanged += new EventScript.AutoFlagChange(this.OnFlagChanged);
    this.enableObject.SetActive(EventScript.IsMessageAuto);
  }

  private GameObject enableObject
  {
    get
    {
      if (Object.op_Equality((Object) this.mEnableObject, (Object) null))
      {
        Transform transform = ((Component) this).transform.Find("Enable");
        if (Object.op_Equality((Object) transform, (Object) null))
          return (GameObject) null;
        this.mEnableObject = ((Component) transform).gameObject;
      }
      return this.mEnableObject;
    }
  }

  public void Start()
  {
  }

  public void OnClick() => EventScript.IsMessageAuto = !EventScript.IsMessageAuto;

  private void OnFlagChanged(bool value)
  {
    if (!Object.op_Inequality((Object) this.enableObject, (Object) null))
      return;
    this.enableObject.SetActive(value);
  }

  public void OnDestroy()
  {
    EventScript.OnAutoFlagChanged -= new EventScript.AutoFlagChange(this.OnFlagChanged);
    EventScript.IsMessageAuto = false;
  }
}
