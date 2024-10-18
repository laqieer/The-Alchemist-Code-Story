// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerSkipFloorItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MultiTowerSkipFloorItem : MonoBehaviour
  {
    [SerializeField]
    private Text TextValue;
    [SerializeField]
    private SRPG_Button SelectBtn;
    [HideInInspector]
    public int Floor;

    public void SetItem(int floor, UnityAction action)
    {
      this.Floor = floor;
      if (Object.op_Implicit((Object) this.TextValue))
        this.TextValue.text = string.Format(LocalizedText.Get("sys.MULTI_TOWER_HIERARCHY"), (object) floor);
      if (action == null || !Object.op_Implicit((Object) this.SelectBtn))
        return;
      ((UnityEvent) this.SelectBtn.onClick).RemoveListener(action);
      ((UnityEvent) this.SelectBtn.onClick).AddListener(action);
    }
  }
}
