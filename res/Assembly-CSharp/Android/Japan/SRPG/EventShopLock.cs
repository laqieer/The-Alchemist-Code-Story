// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopLock : MonoBehaviour
  {
    [SerializeField]
    private GameObject LockObject;

    private void Start()
    {
      if ((UnityEngine.Object) this.LockObject != (UnityEngine.Object) null)
        this.LockObject.SetActive(!(bool) GlobalVars.IsEventShopOpen);
      Button component = this.gameObject.GetComponent<Button>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.interactable = (bool) GlobalVars.IsEventShopOpen;
    }
  }
}
