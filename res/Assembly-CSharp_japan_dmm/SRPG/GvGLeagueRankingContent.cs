// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueRankingContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGLeagueRankingContent : ContentNode
  {
    [SerializeField]
    private int DRAW_STRING_UNDER_RANK = 4;
    [SerializeField]
    private GameObject mOwnGuildBG;
    [SerializeField]
    private GameObject mOtherGuildBG;
    [SerializeField]
    private ImageArray mRankImages;
    [SerializeField]
    private Text mRankText;
    [SerializeField]
    private Text mNameText;
    [SerializeField]
    private Text mLevelText;
    [SerializeField]
    private Text mMasterText;
    [SerializeField]
    private Text mLeagueRate;
    private GvGLeagueViewGuild m_GvGLeagueViewGuild;

    public bool Setup(GvGLeagueViewGuild guild)
    {
      if (guild == null)
        return false;
      this.m_GvGLeagueViewGuild = guild;
      if (Object.op_Inequality((Object) this.mRankImages, (Object) null))
        this.mRankImages.ImageIndex = Mathf.Clamp(guild.league.Rank - 1, 0, this.mRankImages.Images.Length - 1);
      if (Object.op_Inequality((Object) this.mRankText, (Object) null) && guild.league.Rank >= this.DRAW_STRING_UNDER_RANK)
        this.mRankText.text = string.Format(LocalizedText.Get("sys.GVG_RANKING_RANK"), (object) guild.league.Rank);
      GameUtility.SetGameObjectActive((Component) this.mRankText, guild.league.Rank >= this.DRAW_STRING_UNDER_RANK);
      GuildData guild1 = MonoSingleton<GameManager>.Instance.Player.Guild;
      if (guild1 != null)
      {
        GameUtility.SetGameObjectActive(this.mOwnGuildBG, guild1.UniqueID == (long) guild.id);
        GameUtility.SetGameObjectActive(this.mOtherGuildBG, guild1.UniqueID != (long) guild.id);
      }
      else
      {
        GameUtility.SetGameObjectActive(this.mOwnGuildBG, false);
        GameUtility.SetGameObjectActive(this.mOtherGuildBG, true);
      }
      if (Object.op_Inequality((Object) this.mNameText, (Object) null))
        this.mNameText.text = guild.name;
      if (Object.op_Inequality((Object) this.mLevelText, (Object) null))
        this.mLevelText.text = guild.level.ToString();
      if (Object.op_Inequality((Object) this.mMasterText, (Object) null))
        this.mMasterText.text = guild.guild_master;
      if (Object.op_Inequality((Object) this.mLeagueRate, (Object) null))
        this.mLeagueRate.text = guild.league.Rate.ToString();
      DataSource.Bind<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) guild);
      return true;
    }

    public GvGLeagueParam GetGvGLeagueParam()
    {
      if (this.m_GvGLeagueViewGuild != null && this.m_GvGLeagueViewGuild.league != null)
        return GvGLeagueParam.GetGvGLeagueParam(this.m_GvGLeagueViewGuild.league.Id);
      DebugUtility.LogError("リーグ情報が null");
      return (GvGLeagueParam) null;
    }

    public delegate void OnListItemClick(GvGLeagueViewGuild leagueData);

    public class ContentParm : ContentSource.Param
    {
      public GvGLeagueViewGuild mGvGLeagueData;
      public bool IsEmpty;
      public GvGLeagueRankingContent.OnListItemClick onListItemClick;
      private GvGLeagueRankingContent mNode;

      public override void Initialize(ContentSource source)
      {
        base.Initialize(source);
        if (!Object.op_Inequality((Object) this.mNode, (Object) null) || this.mGvGLeagueData == null)
          return;
        this.mNode.Setup(this.mGvGLeagueData);
      }

      public override void OnEnable(ContentNode node)
      {
        base.OnEnable(node);
        this.mNode = node as GvGLeagueRankingContent;
        this.Refresh();
      }

      public override void OnDisable(ContentNode node)
      {
        this.mNode = (GvGLeagueRankingContent) null;
        base.OnDisable(node);
      }

      public override void OnClick(ContentNode node)
      {
        base.OnClick(node);
        if (this.onListItemClick == null)
          return;
        this.onListItemClick(this.mGvGLeagueData);
      }

      public void Refresh()
      {
        if (Object.op_Equality((Object) this.mNode, (Object) null))
          return;
        this.mNode.Setup(this.mGvGLeagueData);
      }
    }
  }
}
