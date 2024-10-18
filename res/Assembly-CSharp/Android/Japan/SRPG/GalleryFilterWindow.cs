// Decompiled with JetBrains decompiler
// Type: SRPG.GalleryFilterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Save Setting", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Enable All Toggle", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "Disable All Toggle", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  public class GalleryFilterWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Toggle[] mToggles = new Toggle[0];
    private const int SAVE_SETTING = 1;
    private const int ENABLE_ALL_TOGGLE = 2;
    private const int DISABLE_ALL_TOGGLE = 3;
    private const int OUTPUT_CLOSE = 100;
    private GalleryItemListWindow.Settings mSettings;
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
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      this.mSettings = currentValue.GetObject("settings") as GalleryItemListWindow.Settings;
      if (this.mSettings == null)
        return;
      this.mRareFiltters = ((IEnumerable<int>) this.mSettings.rareFilters).OrderBy<int, int>((Func<int, int>) (x => x)).ToList<int>();
      foreach (Toggle mToggle in this.mToggles)
        mToggle.isOn = false;
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
      for (int index1 = 0; index1 < this.mToggles.Length; ++index1)
      {
        int index = index1;
        this.mToggles[index1].onValueChanged.AddListener((UnityAction<bool>) (isOn =>
        {
          if (isOn)
          {
            this.mRareFiltters.Add(index);
            this.mRareFiltters = this.mRareFiltters.Distinct<int>().OrderBy<int, int>((Func<int, int>) (x => x)).ToList<int>();
          }
          else
            this.mRareFiltters.Remove(index);
        }));
      }
    }
  }
}
