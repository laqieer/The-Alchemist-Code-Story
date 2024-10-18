// Decompiled with JetBrains decompiler
// Type: SRPG.CoinBuyUseBonusIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class CoinBuyUseBonusIcon : MonoBehaviour
  {
    [SerializeField]
    private Button mIcon;
    [SerializeField]
    private GameObject mBadge;
    private CoinBuyUseBonusParam[] mBonusParams;
    private bool mNeedWaitHomeApi;
    private static CoinBuyUseBonusIcon mInstance;

    public static CoinBuyUseBonusIcon Instance => CoinBuyUseBonusIcon.mInstance;

    private void Awake()
    {
      CoinBuyUseBonusIcon.mInstance = this;
      this.mNeedWaitHomeApi = CoinBuyUseBonusWindow.IsDirtyBonusProgress();
      this.mBonusParams = MonoSingleton<GameManager>.Instance.MasterParam.GetEnableCoinBuyUseBonusParams();
      ((Component) this.mIcon).gameObject.SetActive(this.mBonusParams != null);
      this.RefreshBadge();
    }

    public void RefreshBadge()
    {
      bool flag = false;
      if (this.mBonusParams != null)
      {
        for (int index = 0; index < this.mBonusParams.Length; ++index)
        {
          if (MonoSingleton<GameManager>.Instance.Player.IsExistReceivableCoinBuyUseBonus(this.mBonusParams[index].Trigger, this.mBonusParams[index].Type))
          {
            flag = true;
            break;
          }
        }
      }
      this.mBadge.SetActive(flag);
    }

    private void Update()
    {
      ((Selectable) this.mIcon).interactable = !this.mNeedWaitHomeApi;
      if (!this.mNeedWaitHomeApi || CoinBuyUseBonusWindow.IsDirtyBonusProgress())
        return;
      this.RefreshBadge();
      this.mNeedWaitHomeApi = false;
    }
  }
}
