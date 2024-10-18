// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryFilterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Save Setting", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Enable All Toggle", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Disable All Toggle", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  public class GalleryFilterWindow : MonoBehaviour, IFlowInterface
  {
    private const int SAVE_SETTING = 1;
    private const int ENABLE_ALL_TOGGLE = 2;
    private const int DISABLE_ALL_TOGGLE = 3;
    private const int OUTPUT_CLOSE = 100;
    [SerializeField]
    private Toggle[] mToggles = new Toggle[0];
    private GallerySettings mSettings;
    private List<int> mRareFiltters;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.mSettings.rareFilters = this.mRareFiltters.ToArray();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          if (this.mToggles == null)
            break;
          foreach (Toggle mToggle in this.mToggles)
            mToggle.isOn = true;
          break;
        case 3:
          if (this.mToggles == null)
            break;
          foreach (Toggle mToggle in this.mToggles)
            mToggle.isOn = false;
          break;
      }
    }

    private void Awake()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      FlowNode_ButtonEvent.currentValue = (object) null;
      this.mSettings = currentValue.GetObject("settings") as GallerySettings;
      if (this.mSettings == null)
        return;
      this.mRareFiltters = ((IEnumerable<int>) this.mSettings.rareFilters).OrderBy<int, int>((Func<int, int>) (x => x)).ToList<int>();
      foreach (Toggle mToggle in this.mToggles)
        mToggle.isOn = false;
      if (GallerySettings.IsFilterTotallyOn((IEnumerable<int>) this.mRareFiltters))
        this.mRareFiltters = new List<int>();
      if (this.mToggles != null && this.mToggles.Length >= 0)
      {
        foreach (int mRareFiltter in this.mRareFiltters)
        {
          if (mRareFiltter >= 0 && mRareFiltter < this.mToggles.Length)
            this.mToggles[mRareFiltter].isOn = true;
        }
      }
      if (this.mToggles == null || this.mToggles.Length < 0)
        return;
      for (int index = 0; index < this.mToggles.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.mToggles[index].onValueChanged).AddListener(new UnityAction<bool>((object) new GalleryFilterWindow.\u003CAwake\u003Ec__AnonStorey0()
        {
          \u0024this = this,
          index = index
        }, __methodptr(\u003C\u003Em__0)));
      }
    }
  }
}
