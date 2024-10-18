// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventShopLock : MonoBehaviour
  {
    [SerializeField]
    private GameObject LockObject;

    private void Start()
    {
      if (Object.op_Inequality((Object) this.LockObject, (Object) null))
        this.LockObject.SetActive(!(bool) GlobalVars.IsEventShopOpen);
      Button component = ((Component) this).gameObject.GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Selectable) component).interactable = (bool) GlobalVars.IsEventShopOpen;
    }
  }
}
