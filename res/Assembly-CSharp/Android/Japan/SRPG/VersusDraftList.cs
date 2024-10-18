// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Initialize Complete", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(102, "Unit Selecting", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "Start Drafting", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(2, "Decide Unit", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(3, "Random Decide Unit", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(110, "Turn Player", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(111, "Turn Enemy", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(120, "Finish Draft", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(130, "Unit Select SE", FlowNode.PinTypes.Output, 30)]
  public class VersusDraftList : MonoBehaviour, IFlowInterface
  {
    public static List<VersusDraftUnitParam> VersusDraftUnitList = new List<VersusDraftUnitParam>();
    public static List<UnitData> VersusDraftPartyUnits = new List<UnitData>();
    public static List<int> VersusDraftPartyPlaces = new List<int>();
    private readonly int[] DRAFT_UNIT_LIST_COLS = new int[4]{ 6, 12, 14, 16 };
    private readonly int[] SECRET_INDEX_LIST = new int[6]{ 14, 13, 15, 12, 11, 6 };
    private readonly int[] SELECTABLE_UNIT_COUNT_OF_TURN = new int[7]{ 1, 2, 2, 2, 2, 2, 1 };
    private int mTurn = -1;
    private List<JSON_MyPhotonPlayerParam> mAudiencePlayers = new List<JSON_MyPhotonPlayerParam>();
    public static bool VersusDraftTurnOwn;
    public static List<UnitData> VersusDraftUnitDataListPlayer;
    public static List<UnitData> VersusDraftUnitDataListEnemy;
    public static int DraftID;
    private const int DRAFT_UNIT_LIST_COL_MAX = 6;
    private const int SELECTING_UNIT_COUNT = 6;
    private const float SINGLE_ENEMY_TIME = 5f;
    private const int INPUT_PIN_INITIALIZE = 1;
    private const int INPUT_PIN_DECIDE_UNIT = 2;
    private const int INPUT_PIN_DECIDE_UNIT_RANDOM = 3;
    private const int INPUT_PIN_START_DRAFTING = 4;
    private const int OUTPUT_PIN_INITIALIZE = 101;
    private const int OUTPUT_PIN_UNIT_SELECTING = 102;
    private const int OUTPUT_PIN_TURN_PLAYER = 110;
    private const int OUTPUT_PIN_TURN_ENEMY = 111;
    private const int OUTPUT_PIN_FINISH_DRAFT = 120;
    private const int OUTPUT_PIN_UNIT_SELECT_SE = 130;
    [SerializeField]
    private Transform[] mDraftUnitTransforms;
    [SerializeField]
    private VersusDraftUnit mDraftUnitItem;
    [SerializeField]
    private Transform mSelectedUnitTransform;
    [SerializeField]
    private VersusDraftSelectedUnit mSelectedUnitItem;
    [SerializeField]
    private GameObject mPlayerName;
    [SerializeField]
    private GameObject mEnemyName;
    [SerializeField]
    private Text mPlayerText;
    [SerializeField]
    private Text mEnemyText;
    [SerializeField]
    private GameObject mTimerGO;
    [SerializeField]
    private Text mTimerText;
    [SerializeField]
    private GameObject mTurnChangePlayer;
    [SerializeField]
    private GameObject mTurnChangeEnemy;
    [SerializeField]
    private Text mTurnChangeMessage;
    private bool mSingleMode;
    private bool mRandomSelecting;
    private float mDraftSec;
    private MyPhoton.MyPlayer mEnemyPlayer;
    private float mEnemyTimer;
    private float mPlayerTimer;
    private List<VersusDraftUnit> mVersusDraftUnitList;
    private List<VersusDraftSelectedUnit> mVersusDraftSelectedUnit;
    private int mSelectingUnitIndex;
    private int mEnemyUnitIndex;

    public int SelectableUnitCount
    {
      get
      {
        if (this.mTurn < 0 || this.SELECTABLE_UNIT_COUNT_OF_TURN.Length <= this.mTurn)
          return 0;
        return this.SELECTABLE_UNIT_COUNT_OF_TURN[this.mTurn];
      }
    }

    private void Start()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        VersusDraftList.VersusDraftTurnOwn = !VersusDraftList.VersusDraftTurnOwn;
      if ((UnityEngine.Object) this.mDraftUnitItem != (UnityEngine.Object) null)
        this.mDraftUnitItem.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.mSelectedUnitItem != (UnityEngine.Object) null)
        this.mSelectedUnitItem.gameObject.SetActive(false);
      this.mDraftSec = (float) (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DraftSelectSeconds;
      this.mTurn = -1;
      VersusDraftUnit.CurrentSelectCursors = new List<VersusDraftUnit>();
      for (int index = 0; index < 3; ++index)
      {
        VersusDraftUnit.CurrentSelectCursors.Add((VersusDraftUnit) null);
        VersusDraftUnit.CurrentSelectCursors.Add((VersusDraftUnit) null);
        VersusDraftUnit.CurrentSelectCursors.Add((VersusDraftUnit) null);
      }
      VersusDraftUnit.VersusDraftList = this;
      this.mEnemyPlayer = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList().Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID != PunMonoSingleton<MyPhoton>.Instance.GetMyPlayer().playerID));
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Initialize();
          break;
        case 2:
          this.DecideUnit();
          break;
        case 3:
          this.StartCoroutine(this.RandomSelecting());
          break;
        case 4:
          this.StartDrafting();
          break;
      }
    }

    [DebuggerHidden]
    private IEnumerator RandomSelecting()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusDraftList.\u003CRandomSelecting\u003Ec__Iterator0() { \u0024this = this };
    }

    private void Update()
    {
      if (this.mTurn < 0 || this.SELECTABLE_UNIT_COUNT_OF_TURN.Length <= this.mTurn)
        return;
      if (!MonoSingleton<GameManager>.Instance.AudienceMode)
      {
        this.UpdateTimer();
        this.UpdatePhotonMessage();
        this.UpdateSingleMode();
      }
      else if (this.mAudiencePlayers.Count == 0)
      {
        if (!MonoSingleton<GameManager>.Instance.AudienceMode)
          return;
        AudienceStartParam startedParam = MonoSingleton<GameManager>.Instance.AudienceManager.GetStartedParam();
        if (startedParam == null)
          return;
        for (int index = 0; index < startedParam.players.Length; ++index)
          this.mAudiencePlayers.Add(startedParam.players[index]);
      }
      else
      {
        this.AudienceUpdate();
        this.UpdateTimer();
      }
    }

    private void UpdateTimer()
    {
      if (!VersusDraftList.VersusDraftTurnOwn || int.Parse(FlowNode_Variable.Get("START_PLAYER_TURN")) < 1)
        return;
      this.mPlayerTimer += Time.unscaledDeltaTime;
      int num = (int) ((double) this.mDraftSec - (double) this.mPlayerTimer);
      if (num < 0)
        num = 0;
      this.mTimerText.text = num.ToString();
      if (MonoSingleton<GameManager>.Instance.AudienceMode || (double) this.mPlayerTimer < (double) this.mDraftSec)
        return;
      if (this.mRandomSelecting)
        this.StopCoroutine(this.RandomSelecting());
      this.DecideUnitRandom(true, true);
      this.DecideUnit();
    }

    private void UpdatePhotonMessage()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      List<MyPhoton.MyEvent> events = instance.GetEvents();
      if (events == null)
        return;
      while (events.Count > 0)
      {
        MyPhoton.MyEvent myEvent = events[0];
        events.RemoveAt(0);
        if (myEvent.code == MyPhoton.SEND_TYPE.Normal && myEvent.binary != null)
        {
          VersusDraftList.VersusDraftMessageData buffer = (VersusDraftList.VersusDraftMessageData) null;
          if (GameUtility.Binary2Object<VersusDraftList.VersusDraftMessageData>(out buffer, myEvent.binary) && buffer != null)
          {
            switch ((VersusDraftList.VersusDraftMessageDataHeader) buffer.h)
            {
              case VersusDraftList.VersusDraftMessageDataHeader.CURSOR:
                VersusDraftUnit.ResetSelectUnit();
                if (buffer.uidx0 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx0)
                  this.mVersusDraftUnitList[buffer.uidx0].SelectUnit(false);
                if (buffer.uidx1 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx1)
                  this.mVersusDraftUnitList[buffer.uidx1].SelectUnit(false);
                if (buffer.uidx2 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx2)
                {
                  this.mVersusDraftUnitList[buffer.uidx2].SelectUnit(false);
                  continue;
                }
                continue;
              case VersusDraftList.VersusDraftMessageDataHeader.DECIDE:
                if (buffer.uidx0 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx0)
                {
                  this.mVersusDraftUnitList[buffer.uidx0].DecideUnit(false);
                  ++this.mEnemyUnitIndex;
                }
                if (buffer.uidx1 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx1)
                {
                  this.mVersusDraftUnitList[buffer.uidx1].DecideUnit(false);
                  ++this.mEnemyUnitIndex;
                }
                if (buffer.uidx2 >= 0 && this.mVersusDraftUnitList.Count > buffer.uidx2)
                {
                  this.mVersusDraftUnitList[buffer.uidx2].DecideUnit(false);
                  ++this.mEnemyUnitIndex;
                  continue;
                }
                continue;
              case VersusDraftList.VersusDraftMessageDataHeader.FINISH_TURN:
                if (!this.ChangeTurn(true))
                {
                  for (int index = 0; index < this.SELECTABLE_UNIT_COUNT_OF_TURN[this.mTurn]; ++index)
                    this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex + index].Selecting();
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
    }

    private void UpdateSingleMode()
    {
      if (!this.mSingleMode)
      {
        List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
        if (roomPlayerList != null && roomPlayerList.Count >= 2)
          return;
        this.mSingleMode = true;
      }
      else
      {
        if (VersusDraftList.VersusDraftTurnOwn)
          return;
        this.mEnemyTimer += Time.unscaledDeltaTime;
        if ((double) this.mEnemyTimer < 5.0)
          return;
        this.mEnemyTimer = 0.0f;
        VersusDraftList.VersusDraftMessageData mess1 = new VersusDraftList.VersusDraftMessageData();
        mess1.h = 2;
        mess1.b = this.mTurn;
        for (int index = 0; index < this.SelectableUnitCount; ++index)
        {
          VersusDraftUnit randomUnit = this.GetRandomUnit();
          if ((UnityEngine.Object) randomUnit == (UnityEngine.Object) null)
            return;
          randomUnit.SelectUnit(false);
          randomUnit.DecideUnit(false);
          ++this.mEnemyUnitIndex;
          int num = this.mVersusDraftUnitList.IndexOf(randomUnit);
          switch (index)
          {
            case 0:
              mess1.uidx0 = num;
              break;
            case 1:
              mess1.uidx1 = num;
              break;
            case 2:
              mess1.uidx2 = num;
              break;
          }
        }
        this.SendRoomMessage(mess1, false, true);
        VersusDraftList.VersusDraftMessageData mess2 = new VersusDraftList.VersusDraftMessageData();
        mess2.h = 3;
        mess2.b = this.mTurn;
        this.SendRoomMessage(mess2, true, true);
        this.ChangeTurn(true);
        if (this.SELECTABLE_UNIT_COUNT_OF_TURN.Length <= this.mTurn)
          return;
        for (int index = 0; index < this.SELECTABLE_UNIT_COUNT_OF_TURN[this.mTurn]; ++index)
          this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex + index].Selecting();
      }
    }

    private void AudienceUpdate()
    {
      VersusDraftList.VersusDraftMessageData draftData = MonoSingleton<GameManager>.Instance.AudienceManager.GetDraftData();
      if (draftData == null)
        return;
      int playerID = draftData.pid;
      JSON_MyPhotonPlayerParam photonPlayerParam = this.mAudiencePlayers.Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerID == playerID));
      if (photonPlayerParam == null)
      {
        DebugUtility.LogError("AudiencePlayers not ready : playerId" + (object) playerID + " is not exist");
      }
      else
      {
        bool isPlayer = false;
        if (photonPlayerParam.playerIndex == 1)
          isPlayer = true;
        switch (draftData.h)
        {
          case 1:
            VersusDraftUnit.ResetSelectUnit();
            if (draftData.uidx0 >= 0 && this.mVersusDraftUnitList.Count > draftData.uidx0)
              this.mVersusDraftUnitList[draftData.uidx0].SelectUnit(isPlayer);
            if (draftData.uidx1 >= 0 && this.mVersusDraftUnitList.Count > draftData.uidx1)
              this.mVersusDraftUnitList[draftData.uidx1].SelectUnit(isPlayer);
            if (draftData.uidx2 < 0 || this.mVersusDraftUnitList.Count <= draftData.uidx2)
              break;
            this.mVersusDraftUnitList[draftData.uidx2].SelectUnit(isPlayer);
            break;
          case 2:
            if (draftData.uidx0 >= 0 && this.mVersusDraftUnitList.Count > draftData.uidx0)
            {
              this.mVersusDraftUnitList[draftData.uidx0].DecideUnit(isPlayer);
              if (isPlayer)
              {
                if (this.mVersusDraftSelectedUnit.Count > this.mSelectingUnitIndex)
                {
                  this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(!this.mVersusDraftUnitList[draftData.uidx0].IsHidden ? this.mVersusDraftUnitList[draftData.uidx0].UnitData : (UnitData) null);
                  ++this.mSelectingUnitIndex;
                }
              }
              else
                ++this.mEnemyUnitIndex;
            }
            if (draftData.uidx1 >= 0 && this.mVersusDraftUnitList.Count > draftData.uidx1)
            {
              this.mVersusDraftUnitList[draftData.uidx1].DecideUnit(isPlayer);
              if (isPlayer)
              {
                if (this.mVersusDraftSelectedUnit.Count > this.mSelectingUnitIndex)
                {
                  this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(!this.mVersusDraftUnitList[draftData.uidx1].IsHidden ? this.mVersusDraftUnitList[draftData.uidx1].UnitData : (UnitData) null);
                  ++this.mSelectingUnitIndex;
                }
              }
              else
                ++this.mEnemyUnitIndex;
            }
            if (draftData.uidx2 < 0 || this.mVersusDraftUnitList.Count <= draftData.uidx2)
              break;
            this.mVersusDraftUnitList[draftData.uidx2].DecideUnit(isPlayer);
            if (isPlayer)
            {
              if (this.mVersusDraftSelectedUnit.Count <= this.mSelectingUnitIndex)
                break;
              this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(!this.mVersusDraftUnitList[draftData.uidx2].IsHidden ? this.mVersusDraftUnitList[draftData.uidx2].UnitData : (UnitData) null);
              ++this.mSelectingUnitIndex;
              break;
            }
            ++this.mEnemyUnitIndex;
            break;
          case 3:
            if (this.ChangeTurn(!isPlayer) || isPlayer)
              break;
            for (int index = 0; index < this.SELECTABLE_UNIT_COUNT_OF_TURN[this.mTurn]; ++index)
            {
              if (this.mSelectingUnitIndex + index < this.mVersusDraftSelectedUnit.Count)
                this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex + index].Selecting();
            }
            break;
        }
      }
    }

    private void Initialize()
    {
      this.mSingleMode = false;
      Dictionary<int, VersusDraftUnitParam> dictionary = new Dictionary<int, VersusDraftUnitParam>();
      for (int key = 0; key < VersusDraftList.VersusDraftUnitList.Count; ++key)
        dictionary.Add(key, (VersusDraftUnitParam) null);
      int index1 = 0;
      int index2 = 0;
      for (; index1 < VersusDraftList.VersusDraftUnitList.Count; ++index1)
      {
        if (VersusDraftList.VersusDraftUnitList[index1].IsHidden)
        {
          int secretIndex = this.SECRET_INDEX_LIST[index2];
          ++index2;
          dictionary[secretIndex] = VersusDraftList.VersusDraftUnitList[index1];
        }
      }
      int index3 = 0;
      int index4 = 0;
      for (; index3 < VersusDraftList.VersusDraftUnitList.Count; ++index3)
      {
        if (!VersusDraftList.VersusDraftUnitList[index3].IsHidden)
        {
          while (dictionary[index4] != null)
            ++index4;
          dictionary[index4] = VersusDraftList.VersusDraftUnitList[index3];
        }
      }
      this.mVersusDraftUnitList = new List<VersusDraftUnit>();
      for (int index5 = 0; index5 < dictionary.Count && (this.mDraftUnitTransforms != null && this.mDraftUnitTransforms.Length > 0); ++index5)
      {
        Json_Unit jsonUnit = dictionary[index5].GetJson_Unit();
        if (jsonUnit != null)
        {
          UnitData unit = new UnitData();
          unit.Deserialize(jsonUnit);
          if (unit != null)
          {
            VersusDraftUnit versusDraftUnit = UnityEngine.Object.Instantiate<VersusDraftUnit>(this.mDraftUnitItem);
            this.mVersusDraftUnitList.Add(versusDraftUnit);
            Transform draftUnitTransform = this.mDraftUnitTransforms[0];
            int index6 = 0;
            for (int index7 = 0; index7 < this.DRAFT_UNIT_LIST_COLS.Length; ++index7)
            {
              if (index5 < this.DRAFT_UNIT_LIST_COLS[index7])
              {
                index6 = index7;
                break;
              }
            }
            if (this.mDraftUnitTransforms.Length > index6)
              draftUnitTransform = this.mDraftUnitTransforms[index6];
            versusDraftUnit.SetUp(unit, draftUnitTransform, dictionary[index5].IsHidden);
          }
        }
      }
      this.mVersusDraftSelectedUnit = new List<VersusDraftSelectedUnit>();
      for (int index5 = 0; index5 < 6 && !((UnityEngine.Object) this.mSelectedUnitTransform == (UnityEngine.Object) null); ++index5)
      {
        VersusDraftSelectedUnit draftSelectedUnit = UnityEngine.Object.Instantiate<VersusDraftSelectedUnit>(this.mSelectedUnitItem);
        this.mVersusDraftSelectedUnit.Add(draftSelectedUnit);
        draftSelectedUnit.transform.SetParent(this.mSelectedUnitTransform, false);
        draftSelectedUnit.gameObject.SetActive(true);
        draftSelectedUnit.Initialize();
      }
      this.mRandomSelecting = false;
      VersusDraftList.VersusDraftUnitDataListPlayer = new List<UnitData>();
      VersusDraftList.VersusDraftUnitDataListEnemy = new List<UnitData>();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void StartDrafting()
    {
      this.mSelectingUnitIndex = 0;
      this.mEnemyUnitIndex = 0;
      this.mTurn = -1;
      if (VersusDraftList.VersusDraftTurnOwn)
      {
        this.ChangeTurn(true);
        this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Selecting();
      }
      else
        this.ChangeTurn(false);
    }

    private void DecideUnitRandom(bool notice = false, bool spaceOnly = false)
    {
      if (!VersusDraftList.VersusDraftTurnOwn)
        return;
      int num = this.SelectableUnitCount;
      if (spaceOnly)
        num = this.SelectableUnitCount - VersusDraftUnit.CurrentSelectCursors.FindAll((Predicate<VersusDraftUnit>) (u => (UnityEngine.Object) u != (UnityEngine.Object) null)).Count;
      for (int index = 0; index < num; ++index)
      {
        VersusDraftUnit randomUnit = this.GetRandomUnit();
        if ((UnityEngine.Object) randomUnit == (UnityEngine.Object) null)
          return;
        randomUnit.SelectUnit(true);
      }
      if (!notice)
        return;
      this.SelectUnit();
    }

    private VersusDraftUnit GetRandomUnit()
    {
      List<VersusDraftUnit> all = this.mVersusDraftUnitList.FindAll((Predicate<VersusDraftUnit>) (unit =>
      {
        if (!VersusDraftUnit.CurrentSelectCursors.Contains(unit))
          return !unit.IsSelected;
        return false;
      }));
      if (all == null || all.Count <= 0)
        return (VersusDraftUnit) null;
      int index = Random.Range(0, all.Count);
      if (index < 0)
        index = 0;
      else if (index >= all.Count)
        index = all.Count - 1;
      return all[index];
    }

    public void SetUnit(UnitData unit, int offset)
    {
      if (this.mSelectingUnitIndex + offset >= this.mVersusDraftSelectedUnit.Count)
        return;
      this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex + offset].SetUnit(unit);
    }

    private void DecideUnit()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || this.mRandomSelecting || (!VersusDraftList.VersusDraftTurnOwn || this.mSelectingUnitIndex >= 6))
        return;
      VersusDraftList.VersusDraftMessageData mess = new VersusDraftList.VersusDraftMessageData();
      mess.h = 2;
      mess.b = this.mTurn;
      if ((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[0] != (UnityEngine.Object) null)
      {
        VersusDraftUnit currentSelectCursor = VersusDraftUnit.CurrentSelectCursors[0];
        int num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[0]);
        currentSelectCursor.DecideUnit(true);
        mess.uidx0 = num;
        this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(currentSelectCursor.UnitData);
        ++this.mSelectingUnitIndex;
      }
      if ((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[1] != (UnityEngine.Object) null)
      {
        VersusDraftUnit currentSelectCursor = VersusDraftUnit.CurrentSelectCursors[1];
        int num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[1]);
        currentSelectCursor.DecideUnit(true);
        mess.uidx1 = num;
        this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(currentSelectCursor.UnitData);
        ++this.mSelectingUnitIndex;
      }
      if ((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[2] != (UnityEngine.Object) null)
      {
        VersusDraftUnit currentSelectCursor = VersusDraftUnit.CurrentSelectCursors[2];
        int num = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[2]);
        currentSelectCursor.DecideUnit(true);
        mess.uidx2 = num;
        this.mVersusDraftSelectedUnit[this.mSelectingUnitIndex].Select(currentSelectCursor.UnitData);
        ++this.mSelectingUnitIndex;
      }
      this.SendRoomMessage(mess, false, false);
      this.FinishTurn();
    }

    private void FinishTurn()
    {
      if (!VersusDraftList.VersusDraftTurnOwn)
        return;
      this.ChangeTurn(false);
      VersusDraftList.VersusDraftMessageData mess = new VersusDraftList.VersusDraftMessageData();
      mess.h = 3;
      mess.b = this.mTurn;
      this.SendRoomMessage(mess, true, false);
    }

    public void SelectUnit()
    {
      VersusDraftList.VersusDraftMessageData mess = new VersusDraftList.VersusDraftMessageData();
      mess.h = 1;
      mess.b = this.mTurn;
      int num = 0;
      if ((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[0] != (UnityEngine.Object) null)
      {
        mess.uidx0 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[0]);
        ++num;
      }
      if ((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[1] != (UnityEngine.Object) null)
      {
        mess.uidx1 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[1]);
        ++num;
      }
      if ((UnityEngine.Object) VersusDraftUnit.CurrentSelectCursors[2] != (UnityEngine.Object) null)
      {
        mess.uidx2 = this.mVersusDraftUnitList.IndexOf(VersusDraftUnit.CurrentSelectCursors[2]);
        ++num;
      }
      this.SendRoomMessage(mess, false, false);
      if (num < this.SelectableUnitCount)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void SendRoomMessage(VersusDraftList.VersusDraftMessageData mess, bool immediate = false, bool is_enemy = false)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || mess == null)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
      int myPlayerIndex = instance.MyPlayerIndex;
      if (!is_enemy)
      {
        mess.pidx = myPlayerIndex;
        mess.pid = myPlayer != null ? myPlayer.playerID : 0;
      }
      else
      {
        mess.pidx = myPlayerIndex != 1 ? 1 : 2;
        mess.pid = this.mEnemyPlayer != null ? this.mEnemyPlayer.playerID : 0;
      }
      byte[] msg = GameUtility.Object2Binary<VersusDraftList.VersusDraftMessageData>(mess);
      instance.SendRoomMessageBinary(true, msg, MyPhoton.SEND_TYPE.Normal, false);
      if (!immediate)
        return;
      instance.SendFlush();
    }

    private bool ChangeTurn(bool isPlayer = true)
    {
      VersusDraftUnit.ResetSelectUnit();
      ++this.mTurn;
      if (this.SELECTABLE_UNIT_COUNT_OF_TURN.Length <= this.mTurn)
      {
        this.StartCoroutine(this.DownloadUnitImage());
        return true;
      }
      VersusDraftList.VersusDraftTurnOwn = isPlayer;
      this.mTimerGO.SetActive(isPlayer);
      this.mPlayerName.SetActive(isPlayer);
      this.mPlayerText.gameObject.SetActive(isPlayer);
      this.mEnemyName.SetActive(!isPlayer);
      this.mEnemyText.gameObject.SetActive(!isPlayer);
      string str;
      if (isPlayer)
      {
        this.mPlayerTimer = 0.0f;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
        this.mTurnChangePlayer.SetActive(true);
        this.mTurnChangeEnemy.SetActive(false);
        str = (this.mSelectingUnitIndex + 1).ToString();
        for (int index = 1; index < this.SelectableUnitCount; ++index)
          str = str + "," + (this.mSelectingUnitIndex + 1 + index).ToString();
        this.mTimerText.text = ((int) this.mDraftSec).ToString();
        this.mPlayerText.text = string.Format(LocalizedText.Get("sys.DRAFT_UNIT_SELECT_MESSAGE"), (object) str);
      }
      else
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
        this.mTurnChangePlayer.SetActive(false);
        this.mTurnChangeEnemy.SetActive(true);
        str = (this.mEnemyUnitIndex + 1).ToString();
        for (int index = 1; index < this.SelectableUnitCount; ++index)
          str = str + "," + (this.mEnemyUnitIndex + 1 + index).ToString();
        this.mEnemyText.text = string.Format(LocalizedText.Get("sys.DRAFT_UNIT_SELECT_MESSAGE"), (object) str);
      }
      this.mTurnChangeMessage.text = string.Format(LocalizedText.Get("sys.DRAFT_CHANGE_TURN_MESSAGE"), (object) str);
      return false;
    }

    [DebuggerHidden]
    private IEnumerator DownloadUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new VersusDraftList.\u003CDownloadUnitImage\u003Ec__Iterator1() { \u0024this = this };
    }

    public class VersusDraftMessageData : SceneBattle.MultiPlayRecvData
    {
      public int uidx0 = -1;
      public int uidx1 = -1;
      public int uidx2 = -1;
    }

    public enum VersusDraftMessageDataHeader
    {
      NOP,
      CURSOR,
      DECIDE,
      FINISH_TURN,
      COMPLETE,
      NUM,
    }
  }
}
