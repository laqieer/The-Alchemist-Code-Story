// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetUnlockWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Unlock", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Selected Quest", FlowNode.PinTypes.Output, 101)]
  public class UnitGetUnlockWindow : MonoBehaviour, IFlowInterface
  {
    private UnitParam UnlockUnit;
    public Text UnitName;

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      this.UnlockUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(GlobalVars.UnlockUnitID);
      DataSource.Bind<UnitParam>(this.gameObject, this.UnlockUnit, false);
      this.UnitName.text = LocalizedText.Get("sys.GET_UNIT_WINDOW_UNIT_NAME", new object[1]
      {
        (object) this.UnlockUnit.name
      });
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
