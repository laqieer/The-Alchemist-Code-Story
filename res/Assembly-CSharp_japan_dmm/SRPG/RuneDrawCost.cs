// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawCost
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawCost : MonoBehaviour
  {
    [SerializeField]
    private GameObject mZenyParent;
    [SerializeField]
    private Text mZeny;
    [Space(5f)]
    [SerializeField]
    private GameObject mMaterialParent;
    [SerializeField]
    private RawImage mMaterialImg;
    [SerializeField]
    private Text mMaterialNum;
    [Space(5f)]
    [SerializeField]
    private GameObject mCoinParent;
    [SerializeField]
    private Text mCoin;
    private RuneCost mRuneCost;
    private int mMaxViewNum;

    private void Awake()
    {
    }

    public void SetDrawParam(RuneCost rune_cost, int max_view_num = 0)
    {
      this.mRuneCost = rune_cost;
      this.mMaxViewNum = max_view_num;
      if (this.mRuneCost.use_item != null)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.mRuneCost.use_item);
        if (Object.op_Implicit((Object) this.mMaterialImg) && itemParam != null)
          DataSource.Bind<ItemParam>(((Component) this.mMaterialImg).gameObject, itemParam);
      }
      this.Refresh();
    }

    public void Refresh()
    {
      if (this.mRuneCost == null)
        return;
      int num = 0;
      if (Object.op_Implicit((Object) this.mZeny))
      {
        if (0 < this.mRuneCost.use_zeny)
        {
          this.mZeny.text = this.mRuneCost.use_zeny.ToString();
          if (Object.op_Implicit((Object) this.mZenyParent))
            this.mZenyParent.SetActive(true);
          ++num;
        }
        else if (Object.op_Implicit((Object) this.mZenyParent))
          this.mZenyParent.SetActive(false);
      }
      if (Object.op_Implicit((Object) this.mMaterialNum) && Object.op_Implicit((Object) this.mMaterialImg) && this.mRuneCost.use_item != null)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.mRuneCost.use_item);
        if (0 < this.mRuneCost.use_num && itemParam != null)
        {
          int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(itemParam.iname);
          if (itemAmount < this.mRuneCost.use_num)
            this.mMaterialNum.text = LocalizedText.Get("sys.RUNE_COST_MATERIAL_NEEDNUM_LESS", (object) itemAmount, (object) this.mRuneCost.use_num);
          else
            this.mMaterialNum.text = LocalizedText.Get("sys.RUNE_COST_MATERIAL_NEEDNUM", (object) itemAmount, (object) this.mRuneCost.use_num);
          if (Object.op_Implicit((Object) this.mMaterialParent))
            this.mMaterialParent.SetActive(true);
          ++num;
        }
        else if (Object.op_Implicit((Object) this.mMaterialParent))
          this.mMaterialParent.SetActive(false);
      }
      if (Object.op_Implicit((Object) this.mCoin))
      {
        if (0 < this.mRuneCost.use_coin)
        {
          this.mCoin.text = this.mRuneCost.use_coin.ToString();
          if (Object.op_Implicit((Object) this.mCoinParent))
            this.mCoinParent.SetActive(true);
          ++num;
        }
        else if (Object.op_Implicit((Object) this.mCoinParent))
          this.mCoinParent.SetActive(false);
      }
      if (this.mMaxViewNum != 0 && this.mMaxViewNum < num)
        DebugUtility.LogError("ステータス変更のコストで、表示しきれていないデータがあります");
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
