// Decompiled with JetBrains decompiler
// Type: SRPG.GvGRankingWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "ランキング初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "撃破ランキング初期化", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "撃破ランキング切り替え", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(12, "防衛ランキング初期化", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(101, "防衛ランキング切り替え", FlowNode.PinTypes.Output, 101)]
  public class GvGRankingWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_BEATINIT = 11;
    private const int PIN_INPUT_DEFENSEINIT = 12;
    private const int PIN_OUTPUT_BEATCHANGE = 100;
    private const int PIN_OUTPUT_DEFENSECHANGE = 101;
    private static GvGRankingWindow mInstance;
    [SerializeField]
    private GameObject mRankingRoot;
    [SerializeField]
    private GameObject mBeatRankingRoot;
    [SerializeField]
    private GameObject mDefenseRankingRoot;
    [SerializeField]
    private GvGRankingContent mRankingTemplate;
    [SerializeField]
    private GvGBeatRankingWindowContent mBeatRankingTemplate;
    [SerializeField]
    private GvGDefenseRankingWindowContent mDefenseRankingTemplate;
    [SerializeField]
    private GameObject mRankingTab;
    [SerializeField]
    private GameObject mBeatRankingTab;
    [SerializeField]
    private GameObject mDefenseRankingTab;
    private GvGRankingData mGvGRanking;
    private GameObject SelectTab;
    private List<GameObject> beatList;
    private List<GameObject> defenseList;
    private const int DEFAULT_PAGEINIT = 1;

    public static GvGRankingWindow Instance => GvGRankingWindow.mInstance;

    public int mBeatRankingPage { get; private set; }

    public int mDefenseRankingPage { get; private set; }

    private void Awake()
    {
      GvGRankingWindow.mInstance = this;
      GameUtility.SetGameObjectActive(this.mRankingRoot, false);
      GameUtility.SetGameObjectActive(this.mBeatRankingRoot, false);
      GameUtility.SetGameObjectActive(this.mDefenseRankingRoot, false);
      GameUtility.SetGameObjectActive((Component) this.mRankingTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.mBeatRankingTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.mDefenseRankingTemplate, false);
      this.mBeatRankingPage = 1;
      this.mDefenseRankingPage = 1;
    }

    private void OnDestroy() => GvGRankingWindow.mInstance = (GvGRankingWindow) null;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Init();
          break;
        case 11:
          this.CreateBeatRanking();
          break;
        case 12:
          this.CreateDefenseRanking();
          break;
      }
    }

    private void Init()
    {
      this.CreateRanking();
      GameUtility.SetGameObjectActive(this.mRankingRoot, true);
    }

    public void OnSelectTab(GameObject go)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) this.SelectTab))
        return;
      this.SelectTab = go;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectTab, (UnityEngine.Object) this.mRankingTab))
        this.SetChangeTab(GvGRankingWindow.GvGRankingTab.Normal);
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectTab, (UnityEngine.Object) this.mBeatRankingTab))
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectTab, (UnityEngine.Object) this.mDefenseRankingTab))
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    private void CreateRanking()
    {
      if (this.mGvGRanking == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRankingTemplate, (UnityEngine.Object) null))
        return;
      this.SetChangeTab(GvGRankingWindow.GvGRankingTab.Normal);
      GameUtility.SetGameObjectActive((Component) this.mRankingTemplate, false);
      for (int index = 0; index < this.mGvGRanking.Guilds.Count; ++index)
      {
        if (this.mGvGRanking.Guilds[index] != null)
        {
          GvGRankingContent gvGrankingContent = UnityEngine.Object.Instantiate<GvGRankingContent>(this.mRankingTemplate, ((Component) this.mRankingTemplate).transform.parent, false);
          gvGrankingContent.Setup(this.mGvGRanking.Guilds[index]);
          ((Component) gvGrankingContent).gameObject.SetActive(true);
        }
      }
      GameParameter.UpdateAll(((Component) ((Component) this.mRankingTemplate).transform.parent).gameObject);
    }

    private void CreateBeatRanking()
    {
      this.SetChangeTab(GvGRankingWindow.GvGRankingTab.Beat);
      if (this.mGvGRanking == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBeatRankingTemplate, (UnityEngine.Object) null))
        return;
      GameUtility.SetGameObjectActive((Component) this.mBeatRankingTemplate, false);
      if (this.beatList == null)
        this.beatList = new List<GameObject>();
      this.beatList.ForEach((Action<GameObject>) (p => UnityEngine.Object.Destroy((UnityEngine.Object) p)));
      this.beatList.Clear();
      for (int index = 0; index < this.mGvGRanking.Beats.Count; ++index)
      {
        if (this.mGvGRanking.Beats[index] != null)
        {
          GvGBeatRankingWindowContent rankingWindowContent = UnityEngine.Object.Instantiate<GvGBeatRankingWindowContent>(this.mBeatRankingTemplate, ((Component) this.mBeatRankingTemplate).transform.parent, false);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) rankingWindowContent, (UnityEngine.Object) null))
          {
            this.beatList.Add(((Component) rankingWindowContent).gameObject);
            DataSource.Bind<GvGScoreRankingData>(((Component) rankingWindowContent).gameObject, this.mGvGRanking.Beats[index]);
            rankingWindowContent.Setup(this.mGvGRanking.Beats[index], this.mBeatRankingPage);
            ((Component) rankingWindowContent).gameObject.SetActive(true);
          }
        }
      }
      GameParameter.UpdateAll(((Component) ((Component) this.mRankingTemplate).transform.parent).gameObject);
    }

    private void CreateDefenseRanking()
    {
      this.SetChangeTab(GvGRankingWindow.GvGRankingTab.Defense);
      if (this.mGvGRanking == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDefenseRankingTemplate, (UnityEngine.Object) null))
        return;
      GameUtility.SetGameObjectActive((Component) this.mDefenseRankingTemplate, false);
      if (this.defenseList == null)
        this.defenseList = new List<GameObject>();
      this.defenseList.ForEach((Action<GameObject>) (p => UnityEngine.Object.Destroy((UnityEngine.Object) p)));
      this.defenseList.Clear();
      for (int index = 0; index < this.mGvGRanking.Defenses.Count; ++index)
      {
        if (this.mGvGRanking.Defenses[index] != null)
        {
          GvGDefenseRankingWindowContent rankingWindowContent = UnityEngine.Object.Instantiate<GvGDefenseRankingWindowContent>(this.mDefenseRankingTemplate, ((Component) this.mDefenseRankingTemplate).transform.parent, false);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) rankingWindowContent, (UnityEngine.Object) null))
          {
            this.defenseList.Add(((Component) rankingWindowContent).gameObject);
            DataSource.Bind<GvGScoreRankingData>(((Component) rankingWindowContent).gameObject, this.mGvGRanking.Defenses[index]);
            rankingWindowContent.Setup(this.mGvGRanking.Defenses[index], this.mDefenseRankingPage);
            ((Component) rankingWindowContent).gameObject.SetActive(true);
          }
        }
      }
      GameParameter.UpdateAll(((Component) ((Component) this.mRankingTemplate).transform.parent).gameObject);
    }

    private void SetChangeTab(GvGRankingWindow.GvGRankingTab tab)
    {
      GameUtility.SetGameObjectActive(this.mRankingRoot, tab == GvGRankingWindow.GvGRankingTab.Normal);
      GameUtility.SetGameObjectActive(this.mBeatRankingRoot, tab == GvGRankingWindow.GvGRankingTab.Beat);
      GameUtility.SetGameObjectActive(this.mDefenseRankingRoot, tab == GvGRankingWindow.GvGRankingTab.Defense);
    }

    public void SetRankingData(JSON_GvGRankingData[] guilds)
    {
      if (this.mGvGRanking == null)
        this.mGvGRanking = new GvGRankingData();
      this.mGvGRanking.Deserialize(guilds);
      GvGManager.Instance.GvGRankingViewList = this.mGvGRanking.Guilds;
    }

    public void SetBeatRankingData(JSON_GvGScoreRanking[] beats, int beatPage)
    {
      if (this.mGvGRanking == null)
        this.mGvGRanking = new GvGRankingData();
      this.mGvGRanking.Deserialize(beats, true);
      if (this.mBeatRankingPage < beatPage)
        return;
      this.mBeatRankingPage = 1;
    }

    public void SetDefenseRankingData(JSON_GvGScoreRanking[] defenses, int defensePage)
    {
      if (this.mGvGRanking == null)
        this.mGvGRanking = new GvGRankingData();
      this.mGvGRanking.Deserialize(defenses, false);
      if (this.mDefenseRankingPage < defensePage)
        return;
      this.mDefenseRankingPage = 1;
    }

    private enum GvGRankingTab
    {
      Normal,
      Beat,
      Defense,
    }
  }
}
