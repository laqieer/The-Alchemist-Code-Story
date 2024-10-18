// Decompiled with JetBrains decompiler
// Type: SRPG.ShopExchangeConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class ShopExchangeConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 1;
    [SerializeField]
    private Text ResultText;
    [SerializeField]
    private GameObject ItemIcon;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      string key = currentValue.GetString("unit");
      int num = currentValue.GetInt("piecenum");
      string str = (string) null;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(key);
      if (itemParam != null)
      {
        DataSource.Bind<ItemParam>(this.ItemIcon, itemParam, false);
        str = itemParam.name;
      }
      this.ResultText.text = LocalizedText.Get("sys.PIECE_CONVERT_TO_UNIT_RESULT_MSG", (object) str, (object) num);
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
