// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRescueRequestOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Finish", FlowNode.PinTypes.Output, 2)]
  public class RaidRescueRequestOption : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Toggle mGuild;
    [SerializeField]
    private Toggle mFriend;
    [SerializeField]
    private Toggle mAll;

    private void Awake()
    {
      GameUtility.SetToggle(this.mGuild, false);
      GameUtility.SetToggle(this.mFriend, false);
      GameUtility.SetToggle(this.mAll, true);
      if (MonoSingleton<GameManager>.Instance.Player.Guild == null)
        this.mGuild.interactable = false;
      if (MonoSingleton<GameManager>.Instance.Player.FriendNum == 0)
        this.mFriend.interactable = false;
      this.mGuild.onValueChanged.AddListener((UnityAction<bool>) (on =>
      {
        if (on)
        {
          GameUtility.SetToggle(this.mAll, false);
        }
        else
        {
          if (this.mFriend.isOn)
            return;
          GameUtility.SetToggle(this.mAll, true);
        }
      }));
      this.mFriend.onValueChanged.AddListener((UnityAction<bool>) (on =>
      {
        if (on)
        {
          GameUtility.SetToggle(this.mAll, false);
        }
        else
        {
          if (this.mGuild.isOn)
            return;
          GameUtility.SetToggle(this.mAll, true);
        }
      }));
      this.mAll.onValueChanged.AddListener((UnityAction<bool>) (on =>
      {
        if (on)
        {
          GameUtility.SetToggle(this.mGuild, false);
          GameUtility.SetToggle(this.mFriend, false);
        }
        else
          GameUtility.SetToggle(this.mAll, true);
      }));
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      RaidManager.Instance.RescueReqOptionGuild = this.mGuild.isOn;
      RaidManager.Instance.RescueReqOptionFriend = this.mFriend.isOn;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
