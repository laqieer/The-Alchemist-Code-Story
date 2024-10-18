// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerSkipFloorConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
      if (pinID != 1 || (bool) ((UnityEngine.Object) this.mGoPopup))
        return;
      if (!string.IsNullOrEmpty(this.parentName))
      {
        this.parent = GameObject.Find(this.parentName);
        if (!(bool) ((UnityEngine.Object) this.parent))
          DebugUtility.LogWarning("FlowNode_MultiTowerSkipFloorConfirm/can not found gameObject:" + this.parentName);
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num1 = !(bool) ((UnityEngine.Object) MultiTowerSkipFloorSel.Instance) ? 0 : MultiTowerSkipFloorSel.Instance.SelectFloor;
      int num2 = num1 + 1 - instance.GetMTChallengeFloor();
      int num3 = (int) instance.MasterParam.FixParam.MTSkipCost * num2;
      string text = string.Format(LocalizedText.Get("sys.MULTI_TOWER_SKIP_CONFIRM"), (object) num3, (object) num1);
      this.mIsLackCoins = instance.Player.Coin < num3;
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

    private GameObject CreatePrefab(string resource_path, string text, int have_coin, UIUtility.DialogResultEvent ok_event_listener, UIUtility.DialogResultEvent cancel_event_listener, GameObject go_parent, bool system_modal, int system_modal_priority)
    {
      Canvas canvas = UIUtility.PushCanvas(system_modal, system_modal_priority);
      if ((UnityEngine.Object) go_parent != (UnityEngine.Object) null)
        canvas.transform.SetParent(go_parent.transform);
      GameObject original = AssetManager.Load<GameObject>(resource_path);
      if (!(bool) ((UnityEngine.Object) original))
      {
        Debug.LogError((object) ("FlowNode_MultiTowerSkipFloorConfirm/Load failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
      if (!(bool) ((UnityEngine.Object) gameObject))
      {
        Debug.LogError((object) ("FlowNode_MultiTowerSkipFloorConfirm/Instantiate failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      MultiTowerSkipFloor_YN_Flx component = gameObject.GetComponent<MultiTowerSkipFloor_YN_Flx>();
      if (!(bool) ((UnityEngine.Object) component))
      {
        Debug.LogError((object) "FlowNode_MultiTowerSkipFloorConfirm/Component not attached. 'MultiTowerSkipFloor_YN_Flx'");
        return (GameObject) null;
      }
      component.transform.SetParent(canvas.transform, false);
      if ((bool) ((UnityEngine.Object) component.Text_Message))
        component.Text_Message.text = text;
      if ((bool) ((UnityEngine.Object) component.HaveCoin))
        component.HaveCoin.text = have_coin.ToString();
      component.OnClickYes = ok_event_listener;
      component.OnClickNo = cancel_event_listener;
      return component.gameObject;
    }
  }
}
