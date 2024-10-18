// Decompiled with JetBrains decompiler
// Type: SRPG.SupportRankingUserList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SupportRankingUserList : MonoBehaviour
  {
    [SerializeField]
    private ImageArray mRankImage;
    [SerializeField]
    private Text mRankText;
    [SerializeField]
    private Text mName;
    [SerializeField]
    private Text mLevel;
    [SerializeField]
    private Text mGuildName;
    [SerializeField]
    private Text mScore;
    [SerializeField]
    private GameObject mGuildNone;
    [SerializeField]
    private GameObject mPlayerLock;
    [SerializeField]
    private GameObject mOverGold;
    public int PLAYER_LOCK = 60;
    private const int DEFAULT_RANKING_NOTEXT = 3;

    private void Awake()
    {
      GameUtility.SetGameObjectActive(this.mGuildNone, false);
      GameUtility.SetGameObjectActive(this.mPlayerLock, false);
      GameUtility.SetGameObjectActive(this.mOverGold, false);
      this.Refresh();
    }

    public void Refresh()
    {
      SupportUserRanking dataOfClass = DataSource.FindDataOfClass<SupportUserRanking>(((Component) this).gameObject, (SupportUserRanking) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.rank > 0)
      {
        if (Object.op_Inequality((Object) this.mRankImage, (Object) null))
        {
          int num = dataOfClass.rank - 1;
          if (num >= this.mRankImage.Images.Length)
            num = this.mRankImage.Images.Length - 1;
          this.mRankImage.ImageIndex = num;
        }
        if (Object.op_Inequality((Object) this.mRankText, (Object) null))
          this.mRankText.text = dataOfClass.rank <= 3 ? string.Empty : string.Format(LocalizedText.Get("sys.SUPPORT_SET_RANK"), (object) dataOfClass.rank.ToString());
      }
      if (Object.op_Inequality((Object) this.mName, (Object) null))
        this.mName.text = dataOfClass.name;
      if (Object.op_Inequality((Object) this.mLevel, (Object) null))
        this.mLevel.text = dataOfClass.lv.ToString();
      if (Object.op_Inequality((Object) this.mGuildName, (Object) null))
        this.mGuildName.text = dataOfClass.guildName;
      if (Object.op_Inequality((Object) this.mScore, (Object) null) && dataOfClass.score >= 0)
      {
        if (Object.op_Inequality((Object) SupportRankingWindow.Instance, (Object) null) && 999999999 <= dataOfClass.score)
        {
          this.mScore.text = 999999999.ToString();
          GameUtility.SetGameObjectActive(this.mOverGold, true);
        }
        else
          this.mScore.text = dataOfClass.score.ToString();
      }
      if (dataOfClass.unit != null)
        DataSource.Bind<UnitData>(((Component) this).gameObject, dataOfClass.unit);
      if (!string.IsNullOrEmpty(dataOfClass.award))
        DataSource.Bind<AwardParam>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.GetAwardParam(dataOfClass.award));
      if (Object.op_Inequality((Object) this.mGuildNone, (Object) null) && dataOfClass.guildId == 0)
        GameUtility.SetGameObjectActive(this.mGuildNone, true);
      if (!Object.op_Inequality((Object) this.mPlayerLock, (Object) null) || MonoSingleton<GameManager>.Instance.Player.Lv >= this.PLAYER_LOCK)
        return;
      GameUtility.SetGameObjectActive(this.mPlayerLock, true);
    }
  }
}
