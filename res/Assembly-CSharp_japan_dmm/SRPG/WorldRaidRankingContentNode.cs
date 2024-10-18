// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidRankingContentNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class WorldRaidRankingContentNode : ContentNode
  {
    [SerializeField]
    private ImageArray mRankingImages;
    [SerializeField]
    private GameObject mRankingTextObj;
    [SerializeField]
    private Text mRankingText;
    [SerializeField]
    private Text mUserNameTxt;
    [SerializeField]
    private Text mUserLevelTxt;
    [SerializeField]
    private Text mScoreTxt;
    [SerializeField]
    private Text mGuildNameTxt;
    [SerializeField]
    private GameObject mGuildNoneObj;
    [SerializeField]
    private AwardItem mAwardItem;

    public void Setup(WorldRaidRankingData ranking_data)
    {
      if (Object.op_Inequality((Object) this.mRankingImages, (Object) null))
      {
        int num = ranking_data.Rank <= 0 ? this.mRankingImages.Images.Length : ranking_data.Rank;
        this.mRankingImages.ImageIndex = Mathf.Clamp(num - 1, 0, this.mRankingImages.Images.Length - 1);
        if (Object.op_Inequality((Object) this.mRankingTextObj, (Object) null))
          this.mRankingTextObj.SetActive(num >= this.mRankingImages.Images.Length);
        if (Object.op_Inequality((Object) this.mRankingText, (Object) null))
          this.mRankingText.text = ranking_data.Rank <= 0 ? LocalizedText.Get("sys.WORLDRAID_BOSS_RANKING_NULL") : ranking_data.Rank.ToString();
      }
      if (Object.op_Inequality((Object) this.mUserNameTxt, (Object) null))
        this.mUserNameTxt.text = ranking_data.Name;
      if (Object.op_Inequality((Object) this.mUserLevelTxt, (Object) null))
        this.mUserLevelTxt.text = ranking_data.Lv.ToString();
      if (Object.op_Inequality((Object) this.mScoreTxt, (Object) null))
        this.mScoreTxt.text = ranking_data.Score < 0L ? LocalizedText.Get("sys.WORLDRAID_BOSS_RANKING_NULL") : ranking_data.Score.ToString();
      if (ranking_data.Unit != null)
        DataSource.Bind<UnitData>(((Component) this).gameObject, ranking_data.Unit);
      DataSource.Bind<AwardParam>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.MasterParam.GetAwardParam(ranking_data.AwardId, false));
      if (Object.op_Inequality((Object) this.mAwardItem, (Object) null))
        this.mAwardItem.Refresh();
      if (ranking_data.GuildId > 0 && !string.IsNullOrEmpty(ranking_data.GuildName))
      {
        if (Object.op_Inequality((Object) this.mGuildNameTxt, (Object) null))
          this.mGuildNameTxt.text = ranking_data.GuildName;
        SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.list.SetField(GuildSVB_Key.GUILD_ID, ranking_data.GuildId <= 0 ? 0 : ranking_data.GuildId);
      }
      if (Object.op_Inequality((Object) this.mGuildNoneObj, (Object) null))
        this.mGuildNoneObj.SetActive(ranking_data.GuildId <= 0 || string.IsNullOrEmpty(ranking_data.GuildName));
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
