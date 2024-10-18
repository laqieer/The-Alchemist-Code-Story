// Decompiled with JetBrains decompiler
// Type: SRPG.MaterialPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      DataSource.Bind<ItemParam>(this.gameObject, material, false);
      this.mReqNum = reqNum;
      this.mHasNum = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
      this.mItemParam = material;
      GameParameter.UpdateAll(this.gameObject);
    }

    public void UpdateValue()
    {
      if ((UnityEngine.Object) this.Num != (UnityEngine.Object) null)
        this.Num.text = this.mHasNum.ToString();
      if ((UnityEngine.Object) this.Req != (UnityEngine.Object) null)
        this.Req.text = this.mReqNum.ToString();
      if ((UnityEngine.Object) this.Left != (UnityEngine.Object) null)
        this.Left.text = Mathf.Max(this.mReqNum - this.mHasNum, 0).ToString();
      if ((UnityEngine.Object) this.Slider != (UnityEngine.Object) null)
      {
        this.Slider.maxValue = (float) this.mReqNum;
        this.Slider.minValue = 0.0f;
        this.Slider.value = (float) this.mHasNum;
      }
      if (this.mItemParam == null)
        return;
      Animator component = this.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetInteger(this.State, this.mReqNum > this.mHasNum ? 0 : 1);
    }
  }
}
