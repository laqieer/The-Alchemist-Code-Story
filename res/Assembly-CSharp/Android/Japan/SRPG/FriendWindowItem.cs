// Decompiled with JetBrains decompiler
// Type: SRPG.FriendWindowItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class FriendWindowItem : MonoBehaviour
  {
    [NonSerialized]
    public bool CanBlock = true;
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
    private MultiFuid m_Friend;

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

    public bool IsBlockOn
    {
      get
      {
        return this.block.isOn;
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.toggle != (UnityEngine.Object) null)
        this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.Refresh));
      if (!((UnityEngine.Object) this.block != (UnityEngine.Object) null))
        return;
      this.block.onValueChanged.AddListener(new UnityAction<bool>(this.Refresh));
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
          this.m_Friend = MonoSingleton<GameManager>.Instance.Player.MultiFuids?.Find((Predicate<MultiFuid>) (f =>
          {
            if (f.fuid != null)
              return f.fuid.Equals(this.PlayerParam.FUID);
            return false;
          }));
        flag = this.m_Friend != null && this.m_Friend.status.Equals("friend");
      }
      if ((UnityEngine.Object) this.FriendMark != (UnityEngine.Object) null)
        this.FriendMark.SetActive(flag);
      if ((UnityEngine.Object) this.toggle != (UnityEngine.Object) null)
        this.toggle.interactable = !flag && !this.IsBlockOn;
      if ((UnityEngine.Object) this.block != (UnityEngine.Object) null)
      {
        this.block.interactable = !this.toggle.isOn;
        this.block.gameObject.SetActive(this.CanBlock);
      }
      this.RefreshBlockMark(!flag && this.IsBlockOn);
    }

    private void RefreshBlockMark(bool _active)
    {
      if (!((UnityEngine.Object) this.block != (UnityEngine.Object) null) || !((UnityEngine.Object) this.BlockMark != (UnityEngine.Object) null))
        return;
      this.BlockMark.SetActive(_active);
    }
  }
}
