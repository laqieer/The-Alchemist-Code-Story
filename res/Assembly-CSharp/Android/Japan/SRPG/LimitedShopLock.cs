// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopLock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
