// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(20, "初期化", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(10, "自動更新の一時停止ON", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "自動更新の一時停止OFF", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(110, "初期化完了", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "自動更新開始", FlowNode.PinTypes.Output, 120)]
  public class AutoRepeatQuestTop : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_AUTO_REFRESH_PAUSE_ON = 10;
    private const int PIN_INPUT_AUTO_REFRESH_PAUSE_OFF = 11;
    private const int PIN_INPUT_INIT = 20;
    private const int PIN_OUTPUT_INIT = 110;
    private const int PIN_OUTPUT_AUTO_REFRESH = 120;
    [SerializeField]
    private string TREASURE_BOX_INAME = "UN_GM_TREASURE";
    [SerializeField]
    private string BG_MODEL_GAMEOBJECT_ID = "BACKGROUND_MODEL";
    [SerializeField]
    private float BG_ROTATE_SPEED = 0.7f;
    [SerializeField]
    private bool mIsDispShadow = true;
    [SerializeField]
    private bool mIsFlatUnitHeight = true;
    [SerializeField]
    private GameObject mBackgroundPrefab;
    [SerializeField]
    private Transform mRunningCameraPos;
    [SerializeField]
    private Transform mFinishedCameraPos;
    [SerializeField]
    private Transform mRunningUnitPos;
    [SerializeField]
    private Transform mFinishedUnitPos;
    [SerializeField]
    private Camera mCamera3D;
    [SerializeField]
    private Camera mBGCmaera;
    [SerializeField]
    private AutoRepeatQuestUnit mUnitControllerTemplate;
    [SerializeField]
    private Transform mBackGroundPos;
    [SerializeField]
    private GameObject mRotateRoot;
    [SerializeField]
    private AutoRepeatQuestUnit.UnitAnimationParam[] mAnimSettings;
    [SerializeField]
    private bool mIsPutTreasureBox = true;
    [SerializeField]
    private Transform[] mTreasureBoxPos;
    [SerializeField]
    private float AUTO_REFRESH_TIME;
    private List<AutoRepeatQuestUnit> mUnitControllerList = new List<AutoRepeatQuestUnit>();
    private List<UnitController> mTreasureBoxList = new List<UnitController>();
    private bool mIsAutoRepeatQuestFinished;
    private float mRestAutoRefreshTime;
    private bool mIsNeedAutoRefresh;
    private bool mIsPauseAutoRefresh;
    private static AutoRepeatQuestTop mInstance;

    public static AutoRepeatQuestTop Instance => AutoRepeatQuestTop.mInstance;

    private bool IsEnableAutoRefresh => this.mIsNeedAutoRefresh && !this.mIsPauseAutoRefresh;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.SetAutoRefreshPause(true);
          break;
        case 11:
          this.SetAutoRefreshPause(false);
          break;
        case 20:
          this.Init();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
          break;
      }
    }

    private void SetAutoRefreshPause(bool is_pause)
    {
      this.mIsPauseAutoRefresh = is_pause;
      this.SetAutoRefreshTime();
    }

    public void PrepareAssets()
    {
      List<UnitData> units = this.GetUnits();
      for (int index = 0; index < units.Count; ++index)
        DownloadUtility.PrepareAutoRepeatQuestUnitAssets(units[index]);
      if (!this.mIsPutTreasureBox || string.IsNullOrEmpty(this.TREASURE_BOX_INAME))
        return;
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.TREASURE_BOX_INAME);
      if (unitParam == null)
        return;
      CharacterDB.Character character = CharacterDB.FindCharacter(unitParam.model);
      if (character == null)
        return;
      for (int index = 0; index < character.Jobs.Count; ++index)
        DownloadUtility.PrepareUnitModels(character.Jobs[index]);
    }

    private void Awake()
    {
      AutoRepeatQuestTop.mInstance = this;
      GameUtility.SetGameObjectActive((Component) this.mUnitControllerTemplate, false);
    }

    private void Update()
    {
      this.Update_AutoRefresh();
      if (this.mIsAutoRepeatQuestFinished || !Object.op_Inequality((Object) this.mRotateRoot, (Object) null))
        return;
      this.mRotateRoot.transform.Rotate(new Vector3(1f, 0.0f, 0.0f), this.BG_ROTATE_SPEED * Time.deltaTime);
    }

    private void Update_AutoRefresh()
    {
      if (!this.IsEnableAutoRefresh)
        return;
      this.mRestAutoRefreshTime -= Time.deltaTime;
      if ((double) this.mRestAutoRefreshTime > 0.0)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.State == AutoRepeatQuestData.eState.AUTO_REPEAT_END)
        this.mIsNeedAutoRefresh = false;
      this.SetAutoRefreshTime();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 120);
    }

    private void SetAutoRefreshTime()
    {
      float num = this.AUTO_REFRESH_TIME;
      if (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.State == AutoRepeatQuestData.eState.AUTO_REPEAT_NOW)
        num = Mathf.Max(0.0f, (float) (MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.EndTime - TimeManager.ServerTime).TotalSeconds + 1f);
      this.mRestAutoRefreshTime = (double) num >= (double) this.AUTO_REFRESH_TIME ? this.AUTO_REFRESH_TIME : num;
    }

    private void Init()
    {
      this.mIsAutoRepeatQuestFinished = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.State == AutoRepeatQuestData.eState.AUTO_REPEAT_END;
      this.mIsNeedAutoRefresh = false;
      if (!this.mIsAutoRepeatQuestFinished && (double) this.AUTO_REFRESH_TIME > 0.0)
        this.mIsNeedAutoRefresh = true;
      this.SetAutoRefreshTime();
      if (Object.op_Inequality((Object) this.mBackgroundPrefab, (Object) null))
        Object.Instantiate<GameObject>(this.mBackgroundPrefab).transform.SetParent(this.mBackGroundPos, false);
      Transform transform = !this.mIsAutoRepeatQuestFinished ? this.mRunningCameraPos : this.mFinishedCameraPos;
      ((Component) this.mCamera3D).transform.SetParent(transform, false);
      ((Component) this.mBGCmaera).transform.position = ((Component) this.mCamera3D).transform.position;
      ((Component) this.mBGCmaera).transform.rotation = ((Component) this.mCamera3D).transform.rotation;
      this.CreateUnits(this.mIsAutoRepeatQuestFinished);
      if (this.mIsPutTreasureBox && this.mIsAutoRepeatQuestFinished)
        this.CreateTeasureBox();
      GameObject gameObject = GameObjectID.FindGameObject(this.BG_MODEL_GAMEOBJECT_ID);
      if (!Object.op_Inequality((Object) gameObject, (Object) null) || !Object.op_Inequality((Object) this.mRotateRoot, (Object) null))
        return;
      this.mRotateRoot.transform.position = gameObject.transform.position;
      (!this.mIsAutoRepeatQuestFinished ? (Component) this.mRunningUnitPos : (Component) this.mFinishedUnitPos).transform.SetParent(this.mRotateRoot.transform, true);
      ((Component) transform).transform.SetParent(this.mRotateRoot.transform, true);
    }

    private void CreateTeasureBox()
    {
      for (int index = 0; index < this.mTreasureBoxPos.Length; ++index)
      {
        Json_Unit json = new Json_Unit();
        json.iname = this.TREASURE_BOX_INAME;
        UnitData unitData = new UnitData();
        unitData.Deserialize(json);
        UnitController go = (UnitController) Object.Instantiate<AutoRepeatQuestUnit>(this.mUnitControllerTemplate);
        go.SetupUnit(unitData);
        ((Component) go).transform.SetParent(this.mTreasureBoxPos[index], false);
        GameUtility.SetGameObjectActive((Component) go, true);
        this.mTreasureBoxList.Add(go);
      }
    }

    private void CreateUnits(bool is_finished)
    {
      List<UnitData> units = this.GetUnits();
      for (int index = 0; index < units.Count && this.mAnimSettings.Length > index; ++index)
      {
        AutoRepeatQuestUnit go = Object.Instantiate<AutoRepeatQuestUnit>(this.mUnitControllerTemplate);
        go.SetUnitData(this.mAnimSettings[index], is_finished, this.mIsDispShadow);
        go.SetupUnit(units[index]);
        GameUtility.SetGameObjectActive((Component) go, true);
        this.mUnitControllerList.Add(go);
      }
      if (is_finished)
        return;
      this.FlatUnitSize();
    }

    private List<UnitData> GetUnits()
    {
      List<UnitData> units = new List<UnitData>();
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.QuestIname);
      if (quest != null && quest.units != null)
      {
        for (int index = 0; index < quest.units.Length; ++index)
        {
          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(quest.units.Get(index));
          if (unitDataByUnitId != null && !units.Contains(unitDataByUnitId))
            units.Add(unitDataByUnitId);
        }
      }
      foreach (long unit in MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.Units)
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit);
        if (unitDataByUniqueId != null && !units.Contains(unitDataByUniqueId))
          units.Add(unitDataByUniqueId);
      }
      return units;
    }

    private void FlatUnitSize()
    {
      if (!this.mIsFlatUnitHeight)
        return;
      for (int index = 0; index < this.mUnitControllerList.Count; ++index)
      {
        AutoRepeatQuestUnit mUnitController = this.mUnitControllerList[index];
        if (Object.op_Inequality((Object) mUnitController, (Object) null) && mUnitController.UnitData.UnitParam.sd > (byte) 0)
        {
          float num = 1f / (float) mUnitController.UnitData.UnitParam.sd;
          ((Component) mUnitController).transform.localScale = new Vector3(num, num, num);
        }
      }
    }

    private void OnDestroy()
    {
      for (int index = 0; index < this.mUnitControllerList.Count; ++index)
      {
        GameUtility.DestroyGameObject((Component) this.mUnitControllerList[index]);
        this.mUnitControllerList[index] = (AutoRepeatQuestUnit) null;
      }
      this.mUnitControllerList.Clear();
      for (int index = 0; index < this.mTreasureBoxList.Count; ++index)
      {
        GameUtility.DestroyGameObject((Component) this.mTreasureBoxList[index]);
        this.mTreasureBoxList[index] = (UnitController) null;
      }
      this.mTreasureBoxList.Clear();
      AutoRepeatQuestTop.mInstance = (AutoRepeatQuestTop) null;
    }
  }
}
