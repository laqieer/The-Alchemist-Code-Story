// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueInfoContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGLeagueInfoContent : MonoBehaviour
  {
    [SerializeField]
    private Image mRankIcon;
    [SerializeField]
    private Image mRankName;
    [SerializeField]
    private Image mRankBG;
    [SerializeField]
    private Text mMinRateText;
    [SerializeField]
    private Text mMaxRateText;
    [SerializeField]
    private GameObject mSelfLeague;
    private GvGLeagueParam mGvGLeagueParam;

    public bool Setup(GvGLeagueParam param)
    {
      if (param == null)
        return false;
      this.mGvGLeagueParam = param;
      GvGLeagueViewGuild myGuildLeagueData = GvGLeagueManager.Instance.MyGuildLeagueData;
      string gvGleagueId1 = GvGLeagueParam.GetGvGLeagueId(param.MinRate);
      if (myGuildLeagueData != null && myGuildLeagueData.league != null)
      {
        string gvGleagueId2 = GvGLeagueParam.GetGvGLeagueId(myGuildLeagueData.league.Rate);
        GameUtility.SetGameObjectActive(this.mSelfLeague, gvGleagueId1 == gvGleagueId2);
      }
      else
        GameUtility.SetGameObjectActive(this.mSelfLeague, false);
      if (Object.op_Inequality((Object) this.mMinRateText, (Object) null))
        this.mMinRateText.text = param.MinRate.ToString();
      if (Object.op_Inequality((Object) this.mMaxRateText, (Object) null))
        this.mMaxRateText.text = param.MaxRate.ToString();
      if (param.MaxRate == 0)
        GameUtility.SetGameObjectActive((Component) this.mMaxRateText, false);
      if (Object.op_Inequality((Object) this.mRankIcon, (Object) null) && Object.op_Inequality((Object) this.mRankName, (Object) null) && Object.op_Inequality((Object) this.mRankBG, (Object) null))
      {
        SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("UI/GvG/gvg_league");
        if (Object.op_Inequality((Object) spriteSheet, (Object) null))
        {
          this.mRankIcon.sprite = spriteSheet.GetSprite(param.LeagueIconSpriteKey);
          ((Behaviour) this.mRankIcon).enabled = true;
          this.mRankName.sprite = spriteSheet.GetSprite(param.LeagueNameSpriteKey);
          ((Behaviour) this.mRankName).enabled = true;
          this.mRankBG.sprite = spriteSheet.GetSprite(param.LeagueBGSpriteKey);
          ((Behaviour) this.mRankBG).enabled = true;
        }
      }
      return true;
    }

    public GvGLeagueParam GetGvGLeagueParam() => this.mGvGLeagueParam;
  }
}
