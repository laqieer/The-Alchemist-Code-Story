// Decompiled with JetBrains decompiler
// Type: SRPG.ShopExchangeCountWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Tab Element", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Tab Rainbow", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "Plus", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "Minus", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(102, "Do Convert", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(1000, "Do Convert", FlowNode.PinTypes.Output, 1000)]
  public class ShopExchangeCountWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 1;
    private const int PIN_IN_TAB_CHANGE_ELEMENT = 10;
    private const int PIN_IN_TAB_CHANGE_RAINBOW = 11;
    private const int PIN_IN_BTN_PLUS = 100;
    private const int PIN_IN_BTN_MINUS = 101;
    private const int PIN_IN_BTN_DO_CONVERT = 102;
    private const int PIN_OUT_BTN_DO_CONVERT = 1000;
    [SerializeField]
    private GameObject PieceIcon;
    [SerializeField]
    private GameObject UnitIcon;
    [SerializeField]
    private Slider Slider;
    [SerializeField]
    private Button PlusButton;
    [SerializeField]
    private Button MinusButton;
    [SerializeField]
    private Button DecideButton;
    [SerializeField]
    private Text PieceNumText;
    [SerializeField]
    private Text UnitNumText;
    private ShopExchangeCountWindow.PieceType mPieceType;
    private int mConvertRate;
    private int mSliderMaxValue;
    private int mPieceCount;
    private int mMaxSliderValuer;
    private int mElementSliderValue;
    private int mRainbowSliderValue;
    private string mSelectingPiece;
    private string mSelectingUnitPiece;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Refresh();
          break;
        case 10:
          if (this.mPieceType == ShopExchangeCountWindow.PieceType.Element)
            break;
          this.mPieceType = ShopExchangeCountWindow.PieceType.Element;
          this.Refresh();
          break;
        case 11:
          if (this.mPieceType == ShopExchangeCountWindow.PieceType.Rainbow)
            break;
          this.mPieceType = ShopExchangeCountWindow.PieceType.Rainbow;
          this.Refresh();
          break;
        case 100:
          this.AddSliderValue(1);
          break;
        case 101:
          this.AddSliderValue(-1);
          break;
        case 102:
          this.DoConvert();
          break;
      }
    }

    public void SliderOnValueChanged(float value)
    {
      int val = (int) value;
      if (val < 0)
        val = 0;
      if (val > this.mMaxSliderValuer)
        val = this.mMaxSliderValuer;
      this.SetUIInteractable(val);
      this.ChangeNumText(val);
      if (this.mPieceType == ShopExchangeCountWindow.PieceType.Element)
        this.mElementSliderValue = val;
      else
        this.mRainbowSliderValue = val;
    }

    private void SetUIInteractable(int val)
    {
      this.PlusButton.interactable = val < this.mMaxSliderValuer - 1;
      this.MinusButton.interactable = val > 0;
      this.DecideButton.interactable = val > 0;
    }

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      UnitData unitData = instance.Player.GetUnitData((long) GlobalVars.SelectedUnitUniqueID);
      DataSource.Bind<ItemParam>(this.UnitIcon, instance.MasterParam.GetItemParam(unitData.UnitParam.piece), false);
      ItemParam data = this.mPieceType != ShopExchangeCountWindow.PieceType.Element ? instance.GetItemParam("IT_PI_COMMON_ALL") : this.ElementToItemParam(unitData.Element);
      DataSource.Bind<ItemParam>(this.PieceIcon, data, false);
      this.mPieceCount = instance.Player.GetItemAmount(data.iname);
      this.mConvertRate = (int) (this.mPieceType != ShopExchangeCountWindow.PieceType.Element ? instance.MasterParam.FixParam.ConvertRatePieceCommon : instance.MasterParam.FixParam.ConvertRatePieceElement);
      this.mMaxSliderValuer = this.mPieceCount / this.mConvertRate;
      this.mSliderMaxValue = this.mMaxSliderValuer;
      this.Slider.minValue = 0.0f;
      this.Slider.maxValue = (float) this.mSliderMaxValue;
      int val = this.mPieceType != ShopExchangeCountWindow.PieceType.Element ? this.mRainbowSliderValue : this.mElementSliderValue;
      this.Slider.value = (float) val;
      this.ChangeNumText(val);
      this.SetUIInteractable(val);
      this.ChangeNumText(val);
      this.mSelectingUnitPiece = unitData.UnitParam.piece;
      this.mSelectingPiece = data.iname;
      GameParameter.UpdateAll(this.gameObject);
    }

    private void AddSliderValue(int value)
    {
      this.Slider.value += (float) value;
    }

    private void ChangeNumText(int value)
    {
      this.PieceNumText.text = (value * this.mConvertRate).ToString();
      this.UnitNumText.text = value.ToString();
    }

    private void DoConvert()
    {
      SerializeValueList serializeValueList = new SerializeValueList();
      serializeValueList.SetField("consume", this.mSelectingPiece);
      serializeValueList.SetField("unit", this.mSelectingUnitPiece);
      serializeValueList.SetField("piecenum", (int) this.Slider.value);
      FlowNode_ButtonEvent.currentValue = (object) serializeValueList;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
    }

    private ItemParam ElementToItemParam(EElement element)
    {
      string key = (string) null;
      switch (element)
      {
        case EElement.Fire:
          key = "IT_PI_COMMON_FIRE";
          break;
        case EElement.Water:
          key = "IT_PI_COMMON_WATER";
          break;
        case EElement.Wind:
          key = "IT_PI_COMMON_WIND";
          break;
        case EElement.Thunder:
          key = "IT_PI_COMMON_THUNDER";
          break;
        case EElement.Shine:
          key = "IT_PI_COMMON_LIGHT";
          break;
        case EElement.Dark:
          key = "IT_PI_COMMON_DARK";
          break;
      }
      if (string.IsNullOrEmpty(key))
        return (ItemParam) null;
      return MonoSingleton<GameManager>.Instance.GetItemParam(key);
    }

    private enum PieceType
    {
      Element,
      Rainbow,
    }
  }
}
