// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerSkipFloorConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiTowerSkipFloorConfirm", 32741)]
  [FlowNode.Pin(1, "ポップアップオープン", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "スキップ実行", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "幻晶石不足", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(110, "キャンセル", FlowNode.PinTypes.Output, 110)]
  public class FlowNode_MultiTowerSkipFloorConfirm : FlowNode
  {
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string ResourcePath;
    [SerializeField]
    private bool systemModal;
    [SerializeField]
    private int systemModalPriority;
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private string parentName;
    private const int PIN_IN_OPEN = 1;
    private const int PIN_OUT_OK = 101;
    private const int PIN_OUT_LACK_COINS = 102;
    private const int PIN_OUT_CANCEL = 110;
    private GameObject mGoPopup;
    private bool mIsLackCoins;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || Object.op_Implicit((Object) this.mGoPopup))
        return;
      if (!string.IsNullOrEmpty(this.parentName))
      {
        this.parent = GameObject.Find(this.parentName);
        if (!Object.op_Implicit((Object) this.parent))
          DebugUtility.LogWarning("FlowNode_MultiTowerSkipFloorConfirm/can not found gameObject:" + this.parentName);
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int selectFloor = !Object.op_Implicit((Object) MultiTowerSkipFloorSel.Instance) ? 0 : MultiTowerSkipFloorSel.Instance.SelectFloor;
      int num1 = selectFloor + 1 - instance.GetMTChallengeFloor();
      int num2 = (int) instance.MasterParam.FixParam.MTSkipCost * num1;
      string text = string.Format(LocalizedText.Get("sys.MULTI_TOWER_SKIP_CONFIRM"), (object) num2, (object) selectFloor);
      this.mIsLackCoins = instance.Player.Coin < num2;
      this.mGoPopup = this.CreatePrefab(this.ResourcePath, text, instance.Player.Coin, new UIUtility.DialogResultEvent(this.OnConfirmOK), new UIUtility.DialogResultEvent(this.OnConfirmCancel), this.parent, this.systemModal, this.systemModalPriority);
    }

    private void OnConfirmOK(GameObject go)
    {
      if (this.mIsLackCoins)
        this.ActivateOutputLinks(102);
      else
        this.ActivateOutputLinks(101);
      this.mGoPopup = (GameObject) null;
    }

    private void OnConfirmCancel(GameObject go)
    {
      this.ActivateOutputLinks(110);
      this.mGoPopup = (GameObject) null;
    }

    private GameObject CreatePrefab(
      string resource_path,
      string text,
      int have_coin,
      UIUtility.DialogResultEvent ok_event_listener,
      UIUtility.DialogResultEvent cancel_event_listener,
      GameObject go_parent,
      bool system_modal,
      int system_modal_priority)
    {
      Canvas canvas = UIUtility.PushCanvas(system_modal, system_modal_priority);
      if (Object.op_Inequality((Object) go_parent, (Object) null))
        ((Component) canvas).transform.SetParent(go_parent.transform);
      GameObject gameObject1 = AssetManager.Load<GameObject>(resource_path);
      if (!Object.op_Implicit((Object) gameObject1))
      {
        Debug.LogError((object) ("FlowNode_MultiTowerSkipFloorConfirm/Load failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject1);
      if (!Object.op_Implicit((Object) gameObject2))
      {
        Debug.LogError((object) ("FlowNode_MultiTowerSkipFloorConfirm/Instantiate failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      MultiTowerSkipFloor_YN_Flx component = gameObject2.GetComponent<MultiTowerSkipFloor_YN_Flx>();
      if (!Object.op_Implicit((Object) component))
      {
        Debug.LogError((object) "FlowNode_MultiTowerSkipFloorConfirm/Component not attached. 'MultiTowerSkipFloor_YN_Flx'");
        return (GameObject) null;
      }
      ((Component) component).transform.SetParent(((Component) canvas).transform, false);
      if (Object.op_Implicit((Object) component.Text_Message))
        component.Text_Message.text = text;
      if (Object.op_Implicit((Object) component.HaveCoin))
        component.HaveCoin.text = have_coin.ToString();
      component.OnClickYes = ok_event_listener;
      component.OnClickNo = cancel_event_listener;
      return ((Component) component).gameObject;
    }
  }
}
