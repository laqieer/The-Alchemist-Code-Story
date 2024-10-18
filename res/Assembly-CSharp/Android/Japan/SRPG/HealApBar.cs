// Decompiled with JetBrains decompiler
// Type: SRPG.HealApBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Output, 0)]
  public class HealApBar : MonoBehaviour
  {
    public Slider slider;
    public int UseItemNum;
    public GameObject HealApBase;
    public Text Num;
    public Text HealApNum;
    public Text ItemName;
    public Text ItemCheckUse;
    private ItemData mData;
    public Button up_button;
    public Button down_button;
    public Text CurrentAP;

    public int HealNum
    {
      get
      {
        return this.mData.Param.value * this.UseItemNum;
      }
    }

    public bool IsOverFlow
    {
      get
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        return this.HealNum + instance.Player.Stamina > instance.Player.StaminaStockCap;
      }
    }

    private void OnEnable()
    {
      this.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChanged));
      this.mData = DataSource.FindDataOfClass<ItemData>(this.HealApBase, (ItemData) null);
      this.slider.maxValue = (float) this.GetMaxNum();
      this.slider.minValue = 1f;
      this.UseItemNum = 1;
      this.Refresh();
    }

    public void Refresh()
    {
      this.slider.maxValue = (float) this.GetMaxNum();
      this.slider.value = (float) this.UseItemNum;
      this.Num.text = this.UseItemNum.ToString();
      this.HealApNum.text = (this.UseItemNum * this.mData.Param.value).ToString();
      this.ItemName.text = LocalizedText.Get("sys.TEXT_APHEAL_ITEM_NUM", new object[1]
      {
        (object) this.mData.Param.name
      });
      this.ItemCheckUse.text = LocalizedText.Get("sys.TEXT_APHEAL_CHECK_USE", new object[1]
      {
        (object) this.mData.Param.name
      });
      GameParameter.UpdateAll(this.gameObject);
      this.up_button.interactable = (double) this.slider.maxValue > (double) this.slider.value;
      this.down_button.interactable = (double) this.slider.minValue < (double) this.slider.value;
      if (!((UnityEngine.Object) this.CurrentAP != (UnityEngine.Object) null))
        return;
      this.CurrentAP.text = LocalizedText.Get("sys.TEXT_DENOMINATOR", (object) MonoSingleton<GameManager>.Instance.Player.Stamina, (object) MonoSingleton<GameManager>.Instance.Player.StaminaStockCap);
    }

    public int GetMaxNum()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num = instance.Player.StaminaStockCap - instance.Player.Stamina;
      if (this.mData.Param.value == 0)
        return 1;
      return Mathf.Min((num - 1) / this.mData.Param.value + 1, this.mData.Num);
    }

    public void OnValueChanged(float value)
    {
      this.UseItemNum = Mathf.FloorToInt(value);
      this.UseItemNum = Mathf.Max(this.UseItemNum, 1);
      this.slider.value = (float) this.UseItemNum;
      this.Refresh();
    }

    public void Up()
    {
      ++this.UseItemNum;
      this.UseItemNum = Mathf.Min(this.UseItemNum, this.GetMaxNum());
      this.Refresh();
    }

    public void Down()
    {
      --this.UseItemNum;
      this.UseItemNum = Mathf.Max(this.UseItemNum, 1);
      this.Refresh();
    }
  }
}
