// Decompiled with JetBrains decompiler
// Type: SRPG.GachaCoinChangeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      this.ChangeText.text = LocalizedText.Get("sys.GACHA_SUMMON_NEW_COIN_CHANGED_TEXT", new object[1]
      {
        (object) this.ToDateString(GlobalVars.NewSummonCoinInfo.ConvertedDate)
      });
      this.CoinNum.text = GlobalVars.NewSummonCoinInfo.ConvertedSummonCoin.ToString();
      this.StoneNum.text = GlobalVars.NewSummonCoinInfo.ReceivedStone.ToString();
      this.OldIcon.SetActive(false);
      this.NewIcon.SetActive(true);
    }

    private void RefreshOldCoin()
    {
      if (GlobalVars.OldSummonCoinInfo == null)
        return;
      this.ChangeText.text = LocalizedText.Get("sys.GACHA_SUMMON_OLD_COIN_CHANGED_TEXT", new object[1]
      {
        (object) this.ToDateString(GlobalVars.OldSummonCoinInfo.ConvertedDate)
      });
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
