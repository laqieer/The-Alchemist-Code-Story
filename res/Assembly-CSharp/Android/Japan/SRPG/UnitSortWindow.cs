// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSortWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
          int toggle_index = index;
          this.ToggleElements[index].onValueChanged.AddListener((UnityAction<bool>) (value => this.SelectToggleElement(toggle_index)));
          this.ToggleElements[index].isOn = this.mToggleBkupValues[index] = GameUtility.GetUnitShowSetting(index);
        }
      }
      if ((UnityEngine.Object) this.BtnReset != (UnityEngine.Object) null)
        this.BtnReset.onClick.AddListener(new UnityAction(this.OnReset));
      if ((UnityEngine.Object) this.BtnOk != (UnityEngine.Object) null)
        this.BtnOk.onClick.AddListener(new UnityAction(this.OnDecide));
      if ((UnityEngine.Object) this.BtnClose != (UnityEngine.Object) null)
        this.BtnClose.onClick.AddListener(new UnityAction(this.OnCancel));
      this.mPlaySE = true;
    }

    private void SelectToggleElement(int index)
    {
      GameUtility.SetUnitShowSetting(index, this.ToggleElements[index].isOn);
      if (!this.mPlaySE)
        return;
      Toggle toggle = this.ToggleElements == null || index < 0 || index >= this.ToggleElements.Length ? (Toggle) null : this.ToggleElements[index];
      GameObject gameObject = !((UnityEngine.Object) toggle == (UnityEngine.Object) null) ? toggle.gameObject : (GameObject) null;
      SystemSound systemSound = !((UnityEngine.Object) gameObject == (UnityEngine.Object) null) ? gameObject.GetComponentInChildren<SystemSound>() : (SystemSound) null;
      if (!((UnityEngine.Object) systemSound != (UnityEngine.Object) null))
        return;
      systemSound.Play();
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

    private void OnDecide()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnCancel()
    {
      for (int index = 0; index < this.mToggleBkupValues.Length; ++index)
        GameUtility.SetUnitShowSetting(index, this.mToggleBkupValues[index]);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }
  }
}
