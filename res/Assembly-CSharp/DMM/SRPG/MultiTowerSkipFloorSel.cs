// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerSkipFloorSel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "有効化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "セレクトした", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "キャンセル", FlowNode.PinTypes.Output, 102)]
  public class MultiTowerSkipFloorSel : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [Space(5f)]
    [SerializeField]
    private SRPG_ScrollRect ScrollRectController;
    [SerializeField]
    private GameObject GoSelParent;
    [SerializeField]
    private MultiTowerSkipFloorItem SelBaseItem;
    [Space(5f)]
    [SerializeField]
    private SRPG_Button CancelBtn;
    private const int PIN_IN_INIT = 1;
    private const int PIN_OUT_SELECTED = 101;
    private const int PIN_OUT_CANCEL = 102;
    private static MultiTowerSkipFloorSel mInstance;
    [HideInInspector]
    public int SelectFloor;

    public static MultiTowerSkipFloorSel Instance => MultiTowerSkipFloorSel.mInstance;

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) MultiTowerSkipFloorSel.mInstance, (Object) null))
        return;
      MultiTowerSkipFloorSel.mInstance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) MultiTowerSkipFloorSel.mInstance, (Object) this))
        return;
      MultiTowerSkipFloorSel.mInstance = (MultiTowerSkipFloorSel) null;
    }

    private void Awake()
    {
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void Init()
    {
      ((Component) this).gameObject.SetActive(true);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Implicit((Object) instanceDirect))
        return;
      if (Object.op_Implicit((Object) this.GoSelParent) && Object.op_Implicit((Object) this.SelBaseItem))
      {
        ((Component) this.SelBaseItem).gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects<MultiTowerSkipFloorItem>(this.GoSelParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          ((Component) this.SelBaseItem).gameObject
        }));
      }
      this.SelectFloor = -1;
      if (Object.op_Implicit((Object) this.ScrollRectController))
        this.ScrollRectController.ResetVerticalPosition();
      List<int> mtSkipFloorList = instanceDirect.GetMTSkipFloorList();
      mtSkipFloorList.Sort();
      mtSkipFloorList.Reverse();
      for (int index = 0; index < mtSkipFloorList.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        MultiTowerSkipFloorSel.\u003CInit\u003Ec__AnonStorey0 initCAnonStorey0 = new MultiTowerSkipFloorSel.\u003CInit\u003Ec__AnonStorey0();
        // ISSUE: reference to a compiler-generated field
        initCAnonStorey0.\u0024this = this;
        // ISSUE: reference to a compiler-generated field
        initCAnonStorey0.sfi = Object.Instantiate<MultiTowerSkipFloorItem>(this.SelBaseItem);
        // ISSUE: reference to a compiler-generated field
        if (Object.op_Implicit((Object) initCAnonStorey0.sfi))
        {
          // ISSUE: reference to a compiler-generated field
          ((Component) initCAnonStorey0.sfi).transform.SetParent(this.GoSelParent.transform);
          // ISSUE: reference to a compiler-generated field
          ((Component) initCAnonStorey0.sfi).transform.localScale = Vector3.one;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          initCAnonStorey0.sfi.SetItem(mtSkipFloorList[index], new UnityAction((object) initCAnonStorey0, __methodptr(\u003C\u003Em__0)));
          // ISSUE: reference to a compiler-generated field
          ((Component) initCAnonStorey0.sfi).gameObject.SetActive(true);
        }
      }
      if (Object.op_Implicit((Object) this.CancelBtn))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CancelBtn.onClick).RemoveListener(new UnityAction((object) this, __methodptr(OnCancel)));
        // ISSUE: method pointer
        ((UnityEvent) this.CancelBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(true);
    }

    private void OnSelectFloor(MultiTowerSkipFloorItem sfi)
    {
      if (!Object.op_Implicit((Object) sfi))
        return;
      this.SelectFloor = sfi.Floor;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnCancel() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }
  }
}
