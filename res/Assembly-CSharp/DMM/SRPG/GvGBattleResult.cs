// Decompiled with JetBrains decompiler
// Type: SRPG.GvGBattleResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "GvGリプレイ", FlowNode.PinTypes.Output, 100)]
  public class GvGBattleResult : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_INIT = 1;
    public const int PIN_OUTPUT_REPLAY = 100;
    [SerializeField]
    private GameObject mPlayerTemplate;
    [SerializeField]
    private GameObject mEnemyTemplate;
    [SerializeField]
    private GameObject mWin;
    [SerializeField]
    private GameObject mLose;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Initialize();
    }

    private void Initialize()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      BattleCore.QuestResult questResult = instance.Battle.GetQuestResult();
      GameUtility.SetGameObjectActive(this.mWin, questResult == BattleCore.QuestResult.Win);
      GameUtility.SetGameObjectActive(this.mLose, questResult == BattleCore.QuestResult.Lose);
      if (!Object.op_Inequality((Object) this.mPlayerTemplate, (Object) null) || !Object.op_Inequality((Object) this.mEnemyTemplate, (Object) null))
        return;
      GameUtility.SetGameObjectActive(this.mPlayerTemplate, false);
      GameUtility.SetGameObjectActive(this.mEnemyTemplate, false);
      List<int> battleResultFinishHp1 = instance.Battle.GetPreBattleResultFinishHp(EUnitSide.Player);
      List<int> battleResultFinishHp2 = instance.Battle.GetPreBattleResultFinishHp(EUnitSide.Enemy);
      int index1 = 0;
      int index2 = 0;
      for (int index3 = 0; index3 < instance.Battle.Units.Count; ++index3)
      {
        Unit unit = instance.Battle.Units[index3];
        if (!unit.IsUnitFlag(EUnitFlag.CreatedBreakObj) && !unit.IsUnitFlag(EUnitFlag.IsDynamicTransform))
        {
          GameObject gameObject = (GameObject) null;
          int hp = 0;
          if (unit.Side == EUnitSide.Player)
          {
            gameObject = Object.Instantiate<GameObject>(this.mPlayerTemplate, this.mPlayerTemplate.transform.parent);
            hp = battleResultFinishHp1[index1];
            ++index1;
          }
          else if (unit.Side == EUnitSide.Enemy)
          {
            gameObject = Object.Instantiate<GameObject>(this.mEnemyTemplate, this.mEnemyTemplate.transform.parent);
            hp = battleResultFinishHp2[index2];
            ++index2;
          }
          if (!Object.op_Equality((Object) gameObject, (Object) null))
          {
            GvGBattleResultUnitContentData data = new GvGBattleResultUnitContentData();
            data.Deserilize(instance.Battle.Units[index3], hp);
            DataSource.Bind<GvGBattleResultUnitContentData>(gameObject, data);
            GameUtility.SetGameObjectActive(gameObject, true);
          }
        }
      }
    }

    public void OnReplaySet()
    {
      GlobalVars.GvGBattleReplay.Set(true);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
