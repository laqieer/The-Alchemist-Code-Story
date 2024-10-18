// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSortWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(101, "決定", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(102, "キャンセル", FlowNode.PinTypes.Output, 2)]
  public class UnitSortWindow : MonoBehaviour, IFlowInterface
  {
    public Toggle[] ToggleElements;
    public Button BtnReset;
    public Button BtnOk;
    public Button BtnClose;
    private bool[] mToggleBkupValues;
    private bool mPlaySE;

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if (this.ToggleElements != null)
      {
        this.mToggleBkupValues = new bool[this.ToggleElements.Length];
        for (int index = 0; index < this.ToggleElements.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.ToggleElements[index].onValueChanged).AddListener(new UnityAction<bool>((object) new UnitSortWindow.\u003CStart\u003Ec__AnonStorey0()
          {
            \u0024this = this,
            toggle_index = index
          }, __methodptr(\u003C\u003Em__0)));
          this.ToggleElements[index].isOn = this.mToggleBkupValues[index] = GameUtility.GetUnitShowSetting(index);
        }
      }
      if (Object.op_Inequality((Object) this.BtnReset, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnReset.onClick).AddListener(new UnityAction((object) this, __methodptr(OnReset)));
      }
      if (Object.op_Inequality((Object) this.BtnOk, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnOk.onClick).AddListener(new UnityAction((object) this, __methodptr(OnDecide)));
      }
      if (Object.op_Inequality((Object) this.BtnClose, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnClose.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
      this.mPlaySE = true;
    }

    private void SelectToggleElement(int index)
    {
      GameUtility.SetUnitShowSetting(index, this.ToggleElements[index].isOn);
      if (!this.mPlaySE)
        return;
      Toggle toggleElement = this.ToggleElements == null || index < 0 || index >= this.ToggleElements.Length ? (Toggle) null : this.ToggleElements[index];
      GameObject gameObject = !Object.op_Equality((Object) toggleElement, (Object) null) ? ((Component) toggleElement).gameObject : (GameObject) null;
      SystemSound componentInChildren = !Object.op_Equality((Object) gameObject, (Object) null) ? gameObject.GetComponentInChildren<SystemSound>() : (SystemSound) null;
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.Play();
    }

    private void OnReset()
    {
      GameUtility.ResetUnitShowSetting();
      bool mPlaySe = this.mPlaySE;
      this.mPlaySE = false;
      for (int index = 0; index < this.ToggleElements.Length; ++index)
        this.ToggleElements[index].isOn = GameUtility.GetUnitShowSetting(index);
      this.mPlaySE = mPlaySe;
    }

    private void OnDecide() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);

    private void OnCancel()
    {
      for (int index = 0; index < this.mToggleBkupValues.Length; ++index)
        GameUtility.SetUnitShowSetting(index, this.mToggleBkupValues[index]);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }
  }
}
