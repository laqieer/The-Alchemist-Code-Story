// Decompiled with JetBrains decompiler
// Type: SRPG.FriendWindowItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class FriendWindowItem : MonoBehaviour
  {
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Toggle block;
    [SerializeField]
    private GameObject FriendMark;
    [SerializeField]
    private GameObject BlockMark;
    [NonSerialized]
    public FlowNode_MultiPlayFriendRequest FriendRequest;
    [NonSerialized]
    public JSON_MyPhotonPlayerParam PlayerParam;
    [NonSerialized]
    public SupportData Support;
    [NonSerialized]
    public bool CanBlock = true;
    private MultiFuid m_Friend;

    public bool IsOn => this.toggle.isOn;

    public bool Interactable
    {
      set => ((Selectable) this.toggle).interactable = value;
    }

    public bool IsBlockOn => this.block.isOn;

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.toggle, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(Refresh)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.block, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.block.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(Refresh)));
    }

    public void Refresh(bool on = false)
    {
      bool flag;
      if (this.Support != null)
      {
        flag = this.Support.mIsFriend == 1;
      }
      else
      {
        if (this.m_Friend == null)
          this.m_Friend = MonoSingleton<GameManager>.Instance.Player.MultiFuids?.Find((Predicate<MultiFuid>) (f => f.fuid != null && f.fuid.Equals(this.PlayerParam.FUID)));
        flag = this.m_Friend != null && this.m_Friend.status.Equals("friend");
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendMark, (UnityEngine.Object) null))
        this.FriendMark.SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.toggle, (UnityEngine.Object) null))
        ((Selectable) this.toggle).interactable = !flag && !this.IsBlockOn;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.block, (UnityEngine.Object) null))
      {
        ((Selectable) this.block).interactable = !this.toggle.isOn;
        ((Component) this.block).gameObject.SetActive(this.CanBlock);
      }
      this.RefreshBlockMark(!flag && this.IsBlockOn);
    }

    private void RefreshBlockMark(bool _active)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.block, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BlockMark, (UnityEngine.Object) null))
        return;
      this.BlockMark.SetActive(_active);
    }
  }
}
