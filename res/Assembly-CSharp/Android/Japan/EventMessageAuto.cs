// Decompiled with JetBrains decompiler
// Type: EventMessageAuto
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

public class EventMessageAuto : MonoBehaviour
{
  private GameObject mEnableObject;

  private void Awake()
  {
    RectTransform component = this.GetComponent<RectTransform>();
    float num = (float) Screen.height - SetCanvasBounds.GetSafeArea().height;
    component.anchoredPosition = new Vector2(component.anchoredPosition.x, component.anchoredPosition.y + num);
    EventScript.OnAutoFlagChanged += new EventScript.AutoFlagChange(this.OnFlagChanged);
    this.enableObject.SetActive(EventScript.IsMessageAuto);
  }

  private GameObject enableObject
  {
    get
    {
      if ((UnityEngine.Object) this.mEnableObject == (UnityEngine.Object) null)
      {
        Transform transform = this.transform.Find("Enable");
        if ((UnityEngine.Object) transform == (UnityEngine.Object) null)
          return (GameObject) null;
        this.mEnableObject = transform.gameObject;
      }
      return this.mEnableObject;
    }
  }

  public void Start()
  {
  }

  public void OnClick()
  {
    EventScript.IsMessageAuto = !EventScript.IsMessageAuto;
  }

  private void OnFlagChanged(bool value)
  {
    if (!((UnityEngine.Object) this.enableObject != (UnityEngine.Object) null))
      return;
    this.enableObject.SetActive(value);
  }

  public void OnDestroy()
  {
    EventScript.OnAutoFlagChanged -= new EventScript.AutoFlagChange(this.OnFlagChanged);
    EventScript.IsMessageAuto = false;
  }
}
