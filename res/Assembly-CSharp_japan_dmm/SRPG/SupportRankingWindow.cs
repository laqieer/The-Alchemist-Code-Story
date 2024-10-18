// Decompiled with JetBrains decompiler
// Type: SRPG.SupportRankingWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "Select User", FlowNode.PinTypes.Output, 100)]
  public class SupportRankingWindow : MonoBehaviour, IFlowInterface
  {
    private const int OUTPUT_SELECT_USER = 100;
    private static SupportRankingWindow mInstance;
    public const int MAX_SCORE = 999999999;
    [SerializeField]
    private GameObject mUserParent;
    [SerializeField]
    private GameObject mUnitParent;
    [SerializeField]
    private GameObject mUserTemplate;
    [SerializeField]
    private GameObject mPlayerInfo;
    [SerializeField]
    private GameObject mUnitTemplete;
    [SerializeField]
    private GameObject mTabUser;
    [SerializeField]
    private GameObject mTabUnit;
    [SerializeField]
    private Text mTitleText;
    private GameObject mCurrentTab;
    private SupportRankingWindow.SupportRankingType mSelectType = SupportRankingWindow.SupportRankingType.Unit;

    public static SupportRankingWindow Instance => SupportRankingWindow.mInstance;

    public SupportUserRanking mSupportMyInfo { get; private set; }

    public List<SupportUserRanking> mSupportUser { get; private set; }

    public List<SupportUnitRanking> mSupportUnit { get; private set; }

    private void Awake()
    {
      SupportRankingWindow.mInstance = this;
      GameUtility.SetGameObjectActive(this.mUserParent, false);
      GameUtility.SetGameObjectActive(this.mUnitParent, false);
      GameUtility.SetGameObjectActive(this.mUserTemplate, false);
      GameUtility.SetGameObjectActive(this.mUnitTemplete, false);
      GameUtility.SetGameObjectActive(this.mPlayerInfo, true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTitleText, (UnityEngine.Object) null))
      {
        DateTime dateTime = TimeManager.ServerTime.AddDays(-1.0);
        this.mTitleText.text = string.Format(LocalizedText.Get("sys.SUPPORT_RANKING_DAYS_TITLE"), (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day);
      }
      this.mSelectType = SupportRankingWindow.SupportRankingType.User;
    }

    private void OnDestroy() => SupportRankingWindow.mInstance = (SupportRankingWindow) null;

    public void Init()
    {
      if (this.mSupportUser != null && this.mSupportMyInfo != null && this.mSupportUnit != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayerInfo, (UnityEngine.Object) null))
      {
        for (int index = 0; index < this.mSupportUser.Count; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mUserTemplate, this.mUserTemplate.transform.parent);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            DataSource.Bind<SupportUserRanking>(gameObject, this.mSupportUser[index]);
            gameObject.SetActive(true);
          }
        }
        DataSource.Bind<SupportUserRanking>(this.mPlayerInfo, this.mSupportMyInfo);
        for (int index = 0; index < this.mSupportUnit.Count; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mUnitTemplete, this.mUnitTemplete.transform.parent);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            DataSource.Bind<SupportUnitRanking>(gameObject, this.mSupportUnit[index]);
            gameObject.SetActive(true);
          }
        }
      }
      SupportRankingUserList componentInChildren = this.mPlayerInfo.GetComponentInChildren<SupportRankingUserList>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        componentInChildren.Refresh();
      this.Refresh();
    }

    public void Activated(int pinID)
    {
    }

    private void Refresh()
    {
      GameUtility.SetGameObjectActive(this.mUserParent, this.mSelectType == SupportRankingWindow.SupportRankingType.User);
      GameUtility.SetGameObjectActive(this.mUnitParent, this.mSelectType != SupportRankingWindow.SupportRankingType.User);
    }

    public void OnClickTab(GameObject go)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentTab, (UnityEngine.Object) go))
        return;
      this.mCurrentTab = go;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) this.mTabUser))
        this.mSelectType = SupportRankingWindow.SupportRankingType.User;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) this.mTabUnit))
        this.mSelectType = SupportRankingWindow.SupportRankingType.Unit;
      this.Refresh();
    }

    public void OnUserClick(GameObject go)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return;
      SupportUserRanking dataOfClass = DataSource.FindDataOfClass<SupportUserRanking>(go, (SupportUserRanking) null);
      if (dataOfClass == null)
        return;
      FlowNode_Variable.Set("SelectUserID", dataOfClass.uid);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void OnGuildClick(GameObject go)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return;
      SerializeValueBehaviour component = go.GetComponent<SerializeValueBehaviour>();
      SupportUserRanking dataOfClass = DataSource.FindDataOfClass<SupportUserRanking>(go, (SupportUserRanking) null);
      if (dataOfClass == null || UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.list.SetField(GuildSVB_Key.GUILD_ID, dataOfClass.guildId);
    }

    public void SetupSupportUserRanking(JSON_SupportRankingUser json)
    {
      if (json == null || json.my_info == null || json.ranking == null)
        return;
      if (this.mSupportMyInfo == null)
        this.mSupportMyInfo = new SupportUserRanking();
      this.mSupportMyInfo.score = json.my_info.score <= 999999999 ? json.my_info.score : 999999999;
      this.mSupportMyInfo.rank = json.my_info.rank;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null)
      {
        this.mSupportMyInfo.guildId = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid;
        this.mSupportMyInfo.guildName = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Name;
      }
      this.mSupportMyInfo.name = MonoSingleton<GameManager>.Instance.Player.Name;
      this.mSupportMyInfo.lv = MonoSingleton<GameManager>.Instance.Player.Lv;
      this.mSupportMyInfo.award = MonoSingleton<GameManager>.Instance.Player.SelectedAward;
      if (json.my_info.unit != null)
      {
        UnitData unitData = new UnitData();
        unitData.Deserialize(json.my_info.unit);
        this.mSupportMyInfo.unit = unitData;
      }
      if (this.mSupportUser == null)
        this.mSupportUser = new List<SupportUserRanking>();
      this.mSupportUser.Clear();
      for (int index = 0; index < json.ranking.Length; ++index)
      {
        SupportUserRanking supportUserRanking = new SupportUserRanking();
        supportUserRanking.rank = json.ranking[index].rank;
        supportUserRanking.score = json.ranking[index].score <= 999999999 ? json.ranking[index].score : 999999999;
        if (json.ranking[index].unit != null)
        {
          UnitData unitData = new UnitData();
          unitData.Deserialize(json.ranking[index].unit);
          supportUserRanking.unit = unitData;
        }
        supportUserRanking.uid = json.ranking[index].uid;
        supportUserRanking.lv = json.ranking[index].lv;
        supportUserRanking.name = json.ranking[index].name;
        supportUserRanking.guildName = json.ranking[index].guild.name;
        supportUserRanking.guildId = json.ranking[index].guild.id;
        supportUserRanking.award = json.ranking[index].selected_award;
        this.mSupportUser.Add(supportUserRanking);
      }
    }

    public void SetupSupportUnitRanking(JSON_SupportRankingUnit json)
    {
      if (json == null || json.ranking == null)
        return;
      if (this.mSupportUnit == null)
        this.mSupportUnit = new List<SupportUnitRanking>();
      this.mSupportUnit.Clear();
      for (int index = 0; index < json.ranking.Length; ++index)
      {
        SupportUnitRanking supportUnitRanking = new SupportUnitRanking();
        supportUnitRanking.rank = json.ranking[index].rank;
        supportUnitRanking.score = json.ranking[index].score;
        JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(json.ranking[index].job_iname);
        if (jobParam != null)
          supportUnitRanking.jobName = jobParam.name;
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(json.ranking[index].unit_iname);
        supportUnitRanking.unit = new UnitData();
        supportUnitRanking.unit.Setup(unitParam.iname, 0, 1, 1, jobParam.iname, elem: unitParam.element);
        supportUnitRanking.jobName = jobParam.iname;
        this.mSupportUnit.Add(supportUnitRanking);
      }
    }

    private enum SupportRankingType
    {
      User,
      Unit,
    }
  }
}
