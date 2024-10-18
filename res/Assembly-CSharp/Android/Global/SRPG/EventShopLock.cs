// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
