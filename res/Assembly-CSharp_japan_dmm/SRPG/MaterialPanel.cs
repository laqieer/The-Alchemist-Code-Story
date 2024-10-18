// Decompiled with JetBrains decompiler
// Type: SRPG.MaterialPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class MaterialPanel : MonoBehaviour, IGameParameter
  {
    public Text Num;
    public Text Req;
    public Text Left;
    public Slider Slider;
    public string State;
    private ItemParam mItemParam;
    private int mReqNum;
    private int mHasNum;

    public void SetMaterial(int reqNum, ItemParam material)
    {
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(material);
      DataSource.Bind<ItemParam>(((Component) this).gameObject, material);
      this.mReqNum = reqNum;
      this.mHasNum = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
      this.mItemParam = material;
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public void UpdateValue()
    {
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.text = this.mHasNum.ToString();
      if (Object.op_Inequality((Object) this.Req, (Object) null))
        this.Req.text = this.mReqNum.ToString();
      if (Object.op_Inequality((Object) this.Left, (Object) null))
        this.Left.text = Mathf.Max(this.mReqNum - this.mHasNum, 0).ToString();
      if (Object.op_Inequality((Object) this.Slider, (Object) null))
      {
        this.Slider.maxValue = (float) this.mReqNum;
        this.Slider.minValue = 0.0f;
        this.Slider.value = (float) this.mHasNum;
      }
      if (this.mItemParam == null)
        return;
      Animator component = ((Component) this).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetInteger(this.State, this.mReqNum > this.mHasNum ? 0 : 1);
    }
  }
}
