// Decompiled with JetBrains decompiler
// Type: SRPG.ShopExchangeConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      string key = currentValue.GetString("unit");
      int num = currentValue.GetInt("piecenum");
      string str = (string) null;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(key);
      if (itemParam != null)
      {
        DataSource.Bind<ItemParam>(this.ItemIcon, itemParam);
        str = itemParam.name;
      }
      this.ResultText.text = LocalizedText.Get("sys.PIECE_CONVERT_TO_UNIT_RESULT_MSG", (object) str, (object) num);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
