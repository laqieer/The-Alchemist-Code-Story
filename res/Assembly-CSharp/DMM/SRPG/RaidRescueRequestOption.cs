// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRescueRequestOption
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
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
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mGuild.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CAwake\u003Em__0)));
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mFriend.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CAwake\u003Em__1)));
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mAll.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CAwake\u003Em__2)));
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
