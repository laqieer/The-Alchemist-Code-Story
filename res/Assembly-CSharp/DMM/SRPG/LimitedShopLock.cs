// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      Button component = ((Component) this).GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.mButton = component;
    }

    private void Start() => this.UpdateLockState();

    private void UpdateLockState()
    {
      if (Object.op_Equality((Object) this.mButton, (Object) null))
        return;
      this.LockObject.SetActive(!((Selectable) this.mButton).interactable);
    }
  }
}
