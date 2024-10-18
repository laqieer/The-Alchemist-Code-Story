// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "演出開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "演出スキップ", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(20, "演出終了", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(30, "終了チェック", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(31, "終了", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(32, "昇格演出", FlowNode.PinTypes.Output, 32)]
  [FlowNode.Pin(33, "降格演出", FlowNode.PinTypes.Output, 33)]
  public class RankMatchResult : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject ResultWin;
    [SerializeField]
    private GameObject ResultLose;
    [SerializeField]
    private GameObject ResultDraw;
    [SerializeField]
    private Text RankLabel;
    [SerializeField]
    private Text UpRankLabel;
    [SerializeField]
    private Text ScoreLabel;
    [SerializeField]
    private Text UpScoreLabel;
    [SerializeField]
    private Text NextLabel;
    [SerializeField]
    private Slider NextSlider;
    [SerializeField]
    private GameObject Result;
    [SerializeField]
    private GameObject UpEffect;
    [SerializeField]
    private GameObject DownEffect;
    [SerializeField]
    private GameObject UpImage;
    [SerializeField]
    private GameObject DownImage;
    private const int MAX_FRAME = 60;
    private int mCounter;

    private void Start()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      float rank = (float) player.RankMatchOldRank;
      if (player.RankMatchOldRank == 0)
        rank = (float) player.RankMatchRank;
      float up_rank = (float) player.RankMatchRank - rank;
      float rankMatchOldScore = (float) player.RankMatchOldScore;
      float up_score = (float) player.RankMatchScore - rankMatchOldScore;
      int rankMatchOldClass = (int) player.RankMatchOldClass;
      this.Refresh((int) rankMatchOldScore, (int) rank, (int) up_rank, (int) up_score, ref rankMatchOldClass);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.mCounter = 0;
          this.StartCoroutine(this.PlayAnimationAsync());
          break;
        case 11:
          this.mCounter = 60;
          break;
        case 30:
          this.CheckFinish();
          break;
      }
    }

    private void CheckFinish()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.RankMatchClass == player.RankMatchOldClass)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 31);
      }
      else
      {
        GameObject gameObject;
        GameObject root;
        string str;
        if (player.RankMatchClass > player.RankMatchOldClass)
        {
          gameObject = this.UpEffect;
          root = this.UpImage;
          str = "rankup";
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 32);
        }
        else
        {
          gameObject = this.DownEffect;
          root = this.DownImage;
          str = "rankdown";
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 33);
        }
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          return;
        gameObject.SetActive(true);
        UnitData unitData = this.GetUnitData();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null) && unitData != null)
        {
          DataSource.Bind<UnitData>(root, unitData);
          GameParameter.UpdateAll(root);
        }
        Animator component = gameObject.GetComponent<Animator>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.SetBool(str, true);
      }
    }

    [DebuggerHidden]
    private IEnumerator PlayAnimationAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new RankMatchResult.\u003CPlayAnimationAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void Refresh(int score, int rank, int up_rank, int up_score, ref int class_num)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num1 = 0;
      int num2 = 1;
      VersusRankClassParam versusRankClass = instance.GetVersusRankClass(instance.RankMatchScheduleId, (RankMatchClass) class_num);
      if (versusRankClass != null)
      {
        num1 = versusRankClass.UpPoint - score;
        if (num1 < 0)
          num1 = 0;
        if (versusRankClass.UpPoint > 0 && versusRankClass.UpPoint <= score)
          ++class_num;
        else if (versusRankClass.DownPoint > 0 && versusRankClass.DownPoint > score)
          --class_num;
        num2 = versusRankClass.UpPoint - versusRankClass.DownPoint;
        if (num2 <= 0)
          num2 = 1;
      }
      this.RankLabel.text = rank.ToString();
      int num3 = up_rank;
      this.UpRankLabel.text = (num3 <= 0 ? string.Empty : "+") + num3.ToString();
      this.ScoreLabel.text = score.ToString();
      int num4 = up_score;
      this.UpScoreLabel.text = (num4 <= 0 ? string.Empty : "+") + num4.ToString();
      this.NextLabel.text = num1.ToString();
      this.NextSlider.value = (float) (1.0 - (double) num1 / (double) num2);
    }

    private UnitData GetUnitData()
    {
      MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) pt, (UnityEngine.Object) null))
      {
        string roomParam = pt.GetRoomParam("started");
        if (roomParam != null)
        {
          JSON_MyPhotonPlayerParam[] players = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(roomParam).players;
          if (players.Length > 0)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam = Array.Find<JSON_MyPhotonPlayerParam>(players, (Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID == pt.GetMyPlayer().playerID));
            if (photonPlayerParam != null)
            {
              UnitData unitData = new UnitData();
              unitData.Deserialize(photonPlayerParam.units[0].unitJson);
              return unitData;
            }
          }
        }
      }
      return (UnitData) null;
    }
  }
}
