// Decompiled with JetBrains decompiler
// Type: SRPG.GachaCoinChangeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaCoinChangeWindow : MonoBehaviour
  {
    [SerializeField]
    private Text ChangeText;
    [SerializeField]
    private Text CoinNum;
    [SerializeField]
    private Text StoneNum;
    [SerializeField]
    private GameObject OldIcon;
    [SerializeField]
    private GameObject NewIcon;

    public void Refresh(GachaCoinChangeWindow.CoinType coinType)
    {
      if (coinType == GachaCoinChangeWindow.CoinType.New)
      {
        this.RefreshNewCoin();
      }
      else
      {
        if (coinType != GachaCoinChangeWindow.CoinType.Old)
          return;
        this.RefreshOldCoin();
      }
    }

    private void RefreshNewCoin()
    {
      if (GlobalVars.NewSummonCoinInfo == null)
        return;
      this.ChangeText.text = LocalizedText.Get("sys.GACHA_SUMMON_NEW_COIN_CHANGED_TEXT", (object) this.ToDateString(GlobalVars.NewSummonCoinInfo.ConvertedDate));
      this.CoinNum.text = GlobalVars.NewSummonCoinInfo.ConvertedSummonCoin.ToString();
      this.StoneNum.text = GlobalVars.NewSummonCoinInfo.ReceivedStone.ToString();
      this.OldIcon.SetActive(false);
      this.NewIcon.SetActive(true);
    }

    private void RefreshOldCoin()
    {
      if (GlobalVars.OldSummonCoinInfo == null)
        return;
      this.ChangeText.text = LocalizedText.Get("sys.GACHA_SUMMON_OLD_COIN_CHANGED_TEXT", (object) this.ToDateString(GlobalVars.OldSummonCoinInfo.ConvertedDate));
      this.CoinNum.text = GlobalVars.OldSummonCoinInfo.ConvertedSummonCoin.ToString();
      this.StoneNum.text = GlobalVars.OldSummonCoinInfo.ReceivedStone.ToString();
      this.OldIcon.SetActive(true);
      this.NewIcon.SetActive(false);
    }

    private string ToDateString(long unixTime)
    {
      return GameUtility.UnixtimeToLocalTime(unixTime).ToString("yyyy/M/dd");
    }

    public enum CoinType
    {
      New,
      Old,
    }
  }
}
