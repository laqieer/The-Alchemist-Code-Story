// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerSkipFloorItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
      if ((bool) ((UnityEngine.Object) this.TextValue))
        this.TextValue.text = string.Format(LocalizedText.Get("sys.MULTI_TOWER_HIERARCHY"), (object) floor);
      if (action == null || !(bool) ((UnityEngine.Object) this.SelectBtn))
        return;
      this.SelectBtn.onClick.RemoveListener(action);
      this.SelectBtn.onClick.AddListener(action);
    }
  }
}
