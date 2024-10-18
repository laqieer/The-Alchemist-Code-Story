// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  public class LimitedShopLock : MonoBehaviour
  {
    [SerializeField]
    private GameObject LockObject;
    private Button mButton;

    private void Awake()
    {
      Button component = this.GetComponent<Button>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      this.mButton = component;
    }

    private void Start()
    {
      this.UpdateLockState();
    }

    private void UpdateLockState()
    {
      if ((UnityEngine.Object) this.mButton == (UnityEngine.Object) null)
        return;
      this.LockObject.SetActive(!this.mButton.interactable);
    }
  }
}
