// Decompiled with JetBrains decompiler
// Type: SRPG.ResultInspirationSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "終了", FlowNode.PinTypes.Output, 101)]
  public class ResultInspirationSkill : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [Space(5f)]
    [SerializeField]
    private GameObject[] GoInsps;
    [SerializeField]
    private GameObject[] GoLvUps;
    [Space(5f)]
    [SerializeField]
    private GameObject GoBindUnit;
    [SerializeField]
    private GameObject GoBindArtifact;
    [SerializeField]
    private GameObject GoBindAbility;
    [Space(5f)]
    [SerializeField]
    private SRPG_Button NextBtn;
    private const int PIN_IN_START = 1;
    private const int PIN_OUT_EXIT = 101;
    private static BattleCore.Record mRecord;
    private static int mInspCount;
    private static int mLvUpCount;

    private void Awake()
    {
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void EnableGos(GameObject[] gos, bool is_enable)
    {
      if (gos == null)
        return;
      for (int index = 0; index < gos.Length; ++index)
      {
        if (Object.op_Implicit((Object) gos[index]))
          gos[index].SetActive(is_enable);
      }
    }

    private void EffectStart()
    {
      ((Component) this).gameObject.SetActive(true);
      if (!ResultInspirationSkill.IsRemainEffect())
        return;
      this.EnableGos(this.GoInsps, false);
      this.EnableGos(this.GoLvUps, false);
      if (ResultInspirationSkill.mLvUpCount > 0)
      {
        if (ResultInspirationSkill.mLvUpCount > ResultInspirationSkill.mRecord.mInspResultLvUpList.Count)
          ResultInspirationSkill.mLvUpCount = ResultInspirationSkill.mRecord.mInspResultLvUpList.Count;
        int index = ResultInspirationSkill.mRecord.mInspResultLvUpList.Count - ResultInspirationSkill.mLvUpCount;
        BattleCore.Record.InspResult mInspResultLvUp = ResultInspirationSkill.mRecord.mInspResultLvUpList[index];
        if (Object.op_Implicit((Object) this.GoBindUnit))
          DataSource.Bind<UnitData>(this.GoBindUnit, mInspResultLvUp.mUnitData, true);
        if (Object.op_Implicit((Object) this.GoBindArtifact))
          DataSource.Bind<ArtifactData>(this.GoBindArtifact, mInspResultLvUp.mArtifactData, true);
        if (Object.op_Implicit((Object) this.GoBindAbility))
          DataSource.Bind<AbilityData>(this.GoBindAbility, mInspResultLvUp.mAbilityData, true);
        this.EnableGos(this.GoLvUps, true);
        --ResultInspirationSkill.mLvUpCount;
      }
      else if (ResultInspirationSkill.mInspCount > 0)
      {
        if (ResultInspirationSkill.mInspCount > ResultInspirationSkill.mRecord.mInspResultInspList.Count)
          ResultInspirationSkill.mInspCount = ResultInspirationSkill.mRecord.mInspResultInspList.Count;
        int index = ResultInspirationSkill.mRecord.mInspResultInspList.Count - ResultInspirationSkill.mInspCount;
        BattleCore.Record.InspResult mInspResultInsp = ResultInspirationSkill.mRecord.mInspResultInspList[index];
        if (Object.op_Implicit((Object) this.GoBindUnit))
          DataSource.Bind<UnitData>(this.GoBindUnit, mInspResultInsp.mUnitData, true);
        if (Object.op_Implicit((Object) this.GoBindArtifact))
          DataSource.Bind<ArtifactData>(this.GoBindArtifact, mInspResultInsp.mArtifactData, true);
        if (Object.op_Implicit((Object) this.GoBindAbility))
          DataSource.Bind<AbilityData>(this.GoBindAbility, mInspResultInsp.mAbilityData, true);
        this.EnableGos(this.GoInsps, true);
        --ResultInspirationSkill.mInspCount;
      }
      if (Object.op_Implicit((Object) this.NextBtn))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.NextBtn.onClick).RemoveListener(new UnityAction((object) this, __methodptr(OnNext)));
        // ISSUE: method pointer
        ((UnityEvent) this.NextBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnNext)));
      }
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(true);
      GameParameter.UpdateAll(this.Window);
    }

    private void OnNext() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.EffectStart();
    }

    private static void Init()
    {
      ResultInspirationSkill.mRecord = (BattleCore.Record) null;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      ResultInspirationSkill.mRecord = instance.ResultData == null ? (BattleCore.Record) null : instance.ResultData.Record;
      if (ResultInspirationSkill.mRecord == null)
        return;
      ResultInspirationSkill.mInspCount = ResultInspirationSkill.mRecord.mInspResultInspList.Count;
      ResultInspirationSkill.mLvUpCount = ResultInspirationSkill.mRecord.mInspResultLvUpList.Count;
    }

    private static void Finish()
    {
      ResultInspirationSkill.mRecord = (BattleCore.Record) null;
      ResultInspirationSkill.mInspCount = 0;
      ResultInspirationSkill.mLvUpCount = 0;
    }

    public static void InitEffect() => ResultInspirationSkill.Init();

    public static void DestroyEffect() => ResultInspirationSkill.Finish();

    public static bool IsRemainEffect()
    {
      return ResultInspirationSkill.mRecord != null && (ResultInspirationSkill.mInspCount > 0 || ResultInspirationSkill.mLvUpCount > 0);
    }
  }
}
