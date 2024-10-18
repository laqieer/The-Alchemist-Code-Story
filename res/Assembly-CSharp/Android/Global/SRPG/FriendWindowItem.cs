// Decompiled with JetBrains decompiler
// Type: SRPG.FriendWindowItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class FriendWindowItem : MonoBehaviour
  {
    [SerializeField]
    private Toggle toggle;
    public FlowNode_MultiPlayFriendRequest FriendRequest;
    public JSON_MyPhotonPlayerParam PlayerParam;

    public bool IsOn
    {
      get
      {
        return this.toggle.isOn;
      }
    }

    public bool Interactable
    {
      set
      {
        this.toggle.interactable = value;
      }
    }

    private void Start()
    {
      this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.Set));
    }

    public void Set(bool on)
    {
      this.FriendRequest.SetInteractable();
    }
  }
}
