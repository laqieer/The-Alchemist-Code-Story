// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerSkipFloorSel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public static MultiTowerSkipFloorSel Instance
    {
      get
      {
        return MultiTowerSkipFloorSel.mInstance;
      }
    }

    private void OnEnable()
    {
      if (!((UnityEngine.Object) MultiTowerSkipFloorSel.mInstance == (UnityEngine.Object) null))
        return;
      MultiTowerSkipFloorSel.mInstance = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) MultiTowerSkipFloorSel.mInstance == (UnityEngine.Object) this))
        return;
      MultiTowerSkipFloorSel.mInstance = (MultiTowerSkipFloorSel) null;
    }

    private void Awake()
    {
      if (!(bool) ((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void Init()
    {
      this.gameObject.SetActive(true);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!(bool) ((UnityEngine.Object) instanceDirect))
        return;
      if ((bool) ((UnityEngine.Object) this.GoSelParent) && (bool) ((UnityEngine.Object) this.SelBaseItem))
      {
        this.SelBaseItem.gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects<MultiTowerSkipFloorItem>(this.GoSelParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.SelBaseItem.gameObject
        }));
      }
      this.SelectFloor = -1;
      if ((bool) ((UnityEngine.Object) this.ScrollRectController))
        this.ScrollRectController.ResetVerticalPosition();
      List<int> mtSkipFloorList = instanceDirect.GetMTSkipFloorList();
      mtSkipFloorList.Sort();
      mtSkipFloorList.Reverse();
      for (int index = 0; index < mtSkipFloorList.Count; ++index)
      {
        MultiTowerSkipFloorItem sfi = UnityEngine.Object.Instantiate<MultiTowerSkipFloorItem>(this.SelBaseItem);
        if ((bool) ((UnityEngine.Object) sfi))
        {
          sfi.transform.SetParent(this.GoSelParent.transform);
          sfi.transform.localScale = Vector3.one;
          sfi.SetItem(mtSkipFloorList[index], (UnityAction) (() => this.OnSelectFloor(sfi)));
          sfi.gameObject.SetActive(true);
        }
      }
      if ((bool) ((UnityEngine.Object) this.CancelBtn))
      {
        this.CancelBtn.onClick.RemoveListener(new UnityAction(this.OnCancel));
        this.CancelBtn.onClick.AddListener(new UnityAction(this.OnCancel));
      }
      if (!(bool) ((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(true);
    }

    private void OnSelectFloor(MultiTowerSkipFloorItem sfi)
    {
      if (!(bool) ((UnityEngine.Object) sfi))
        return;
      this.SelectFloor = sfi.Floor;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnCancel()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }
  }
}
