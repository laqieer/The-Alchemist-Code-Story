// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakeraWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "クエスト選択", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "クエスト書庫選択", FlowNode.PinTypes.Output, 110)]
  public class UnitKakeraWindow : MonoBehaviour, IFlowInterface
  {
    public UnitKakeraWindow.AwakeEvent OnAwakeAccept;
    [SerializeField]
    private GameObject QuestList;
    [SerializeField]
    private RectTransform QuestListParent;
    [SerializeField]
    private GameObject QuestListItemTemplate;
    [SerializeField]
    private List<GameObject> PieceUnits = new List<GameObject>(3);
    [SerializeField]
    private List<GameObject> PieceMasks = new List<GameObject>(3);
    [SerializeField]
    private List<Text> PieceConsumeTexts = new List<Text>(3);
    [SerializeField]
    private List<Text> PieceAmountTexts = new List<Text>(3);
    [SerializeField]
    private List<Slider> PieceSliders = new List<Slider>(3);
    [SerializeField]
    private List<Button> PiecePlusButtons = new List<Button>(3);
    [SerializeField]
    private List<Button> PieceMinusButtons = new List<Button>(3);
    [SerializeField]
    private List<Button> PiecePlus5Buttons = new List<Button>(3);
    [SerializeField]
    private List<Toggle> PieceMaxToggles = new List<Toggle>(3);
    [SerializeField]
    private Text Kakera_Consume_Message;
    [SerializeField]
    private Text Kakera_Caution_Message;
    [SerializeField]
    private GameObject NotFoundGainQuestObject;
    [SerializeField]
    private GameObject CautionObject;
    [SerializeField]
    private Button DecideButton;
    [SerializeField]
    private Button MaxButton;
    [SerializeField]
    private Button CancelButton;
    [SerializeField]
    private GameObject JobUnlock;
    [SerializeField]
    private GameObject UnlockArtifactSlot;
    [SerializeField]
    private Text AwakeResultLv;
    [SerializeField]
    private Text AwakeResultComb;
    [SerializeField]
    private Text AwakeResultArtifactSlots;
    [SerializeField]
    private RectTransform JobUnlockParent;
    [SerializeField]
    private GameObject NotPieceDataMask;
    [SerializeField]
    private Text NextNeedPieceCount;
    [SerializeField]
    private UnitKakeraConfirm ConfirmWindowPrefab;
    private UnitData mCurrentUnit;
    private JobParam mUnlockJobParam;
    private List<GameObject> mGainedQuests = new List<GameObject>();
    private ItemParam LastUpadatedItemParam;
    private UnitData mTempUnit;
    private List<GameObject> mUnlockJobList = new List<GameObject>();
    private List<JobSetParam> mCacheCCJobs = new List<JobSetParam>();
    private List<int> mBeforeUseCounts = new List<int>(3);

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null))
        DebugUtility.LogError("Need Attatch to QuestListItemTemplate");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.DecideButton, (UnityEngine.Object) null))
        DebugUtility.LogError("Need Attatch to DecideButton");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MaxButton, (UnityEngine.Object) null))
        DebugUtility.LogError("Need Attatch to MaxButton");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CancelButton, (UnityEngine.Object) null))
        DebugUtility.LogError("Need Attatch to CancelButton");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.JobUnlock, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("Need Attatch to JobUnlock");
      }
      else
      {
        List<string> stringList = new List<string>((IEnumerable<string>) PlayerPrefsUtility.GetString(PlayerPrefsUtility.UNIT_AWAKE_PIECE_CHECKS, string.Empty).Split('|'));
        this.QuestListItemTemplate.SetActive(false);
        // ISSUE: method pointer
        ((UnityEvent) this.DecideButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnDecideClick)));
        // ISSUE: method pointer
        ((UnityEvent) this.MaxButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnMaxClick)));
        // ISSUE: method pointer
        ((UnityEvent) this.CancelButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCancelClick)));
        this.JobUnlock.SetActive(false);
        for (int index = 0; index < 3; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          UnitKakeraWindow.\u003CStart\u003Ec__AnonStorey0 startCAnonStorey0 = new UnitKakeraWindow.\u003CStart\u003Ec__AnonStorey0();
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey0.\u0024this = this;
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey0.kind = (UnitKakeraWindow.PieceKind) index;
          // ISSUE: method pointer
          ((UnityEvent) this.PiecePlus5Buttons[index].onClick).AddListener(new UnityAction((object) startCAnonStorey0, __methodptr(\u003C\u003Em__0)));
          // ISSUE: method pointer
          ((UnityEvent) this.PiecePlusButtons[index].onClick).AddListener(new UnityAction((object) startCAnonStorey0, __methodptr(\u003C\u003Em__1)));
          // ISSUE: method pointer
          ((UnityEvent) this.PieceMinusButtons[index].onClick).AddListener(new UnityAction((object) startCAnonStorey0, __methodptr(\u003C\u003Em__2)));
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.PieceMaxToggles[index].onValueChanged).AddListener(new UnityAction<bool>((object) startCAnonStorey0, __methodptr(\u003C\u003Em__3)));
          if (stringList != null && stringList.Count > 0)
            this.PieceMaxToggles[index].isOn = stringList.IndexOf(index.ToString()) != -1;
        }
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh(this.mCurrentUnit, this.mUnlockJobParam);
    }

    public void Refresh(UnitData unit, JobParam jobUnlock)
    {
      if (unit == null)
        return;
      this.mCurrentUnit = unit;
      this.mUnlockJobParam = jobUnlock;
      this.mCacheCCJobs.Clear();
      this.mTempUnit = new UnitData();
      this.mTempUnit.Setup(this.mCurrentUnit);
      for (int index = 0; index < this.mUnlockJobList.Count; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnlockJobList[index], (UnityEngine.Object) null))
        {
          DataSource.Bind<JobParam>(this.mUnlockJobList[index], (JobParam) null);
          this.mUnlockJobList[index].SetActive(false);
        }
      }
      int length = this.mCurrentUnit.Jobs.Length;
      if (this.mUnlockJobList.Count < length)
      {
        int num = length - this.mUnlockJobList.Count;
        for (int index = 0; index < num; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.JobUnlock);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            gameObject.transform.SetParent((Transform) this.JobUnlockParent, false);
            this.mUnlockJobList.Add(gameObject);
          }
        }
      }
      JobSetParam[] changeJobSetParam = MonoSingleton<GameManager>.Instance.MasterParam.GetClassChangeJobSetParam(this.mCurrentUnit.UnitParam.iname);
      if (changeJobSetParam != null)
        this.mCacheCCJobs.AddRange((IEnumerable<JobSetParam>) changeJobSetParam);
      for (int index = 0; index < length; ++index)
      {
        if (!this.mCurrentUnit.CheckJobUnlockable(index) && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnlockJobList[index], (UnityEngine.Object) null))
          DataSource.Bind<JobParam>(this.mUnlockJobList[index], this.mCurrentUnit.Jobs[index].Param);
      }
      DataSource.Bind<UnitData>(((Component) this).gameObject, (UnitData) null);
      GameParameter.UpdateAll(((Component) this).gameObject);
      DataSource.Bind<UnitData>(((Component) this).gameObject, this.mTempUnit);
      int awakeLv = this.mCurrentUnit.AwakeLv;
      int awakeLevelCap = this.mCurrentUnit.GetAwakeLevelCap();
      bool flag1 = awakeLevelCap > awakeLv;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CautionObject, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("Need Attatch to CautionObject");
      }
      else
      {
        this.CautionObject.SetActive(!flag1);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.DecideButton, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("Need Attatch to DecideButton");
        }
        else
        {
          ((Selectable) this.DecideButton).interactable = false;
          ((Selectable) this.MaxButton).interactable = flag1;
          if (flag1)
          {
            bool flag2 = false;
            for (int index = 0; index < 3; ++index)
            {
              if ((!this.mCurrentUnit.IsRental || index <= 0) && this.PieceMaxToggles[index].isOn)
              {
                flag2 = true;
                break;
              }
            }
            ((Selectable) this.MaxButton).interactable = flag2;
          }
          ((Selectable) this.CancelButton).interactable = flag1;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.NextNeedPieceCount, (UnityEngine.Object) null))
          {
            DebugUtility.LogError("Need Attatch to NextNeedPieceCount");
          }
          else
          {
            for (int index = 0; index < 3 && index <= this.PieceSliders.Count - 1; ++index)
              this.PieceSliders[index].value = 0.0f;
            if (flag1)
            {
              PlayerData player = MonoSingleton<GameManager>.Instance.Player;
              int awakeNeedPieces = this.mCurrentUnit.GetAwakeNeedPieces();
              bool flag3 = false;
              ItemData itemDataByItemId = player.FindItemDataByItemID(this.mCurrentUnit.UnitParam.piece);
              ItemData elementPieceData = this.mCurrentUnit.GetElementPieceData();
              ItemData commonPieceData = this.mCurrentUnit.GetCommonPieceData();
              int[] numArray = new int[3]
              {
                itemDataByItemId == null ? 0 : itemDataByItemId.Num,
                elementPieceData == null ? 0 : elementPieceData.Num,
                commonPieceData == null ? 0 : commonPieceData.Num
              };
              int pieceCountForAwake = this.GetPieceCountForAwake(awakeLv, awakeLevelCap, numArray[0], numArray[1], numArray[2]);
              for (int index = 0; index < 3 && index <= this.PieceUnits.Count - 1 && index <= this.PieceMasks.Count - 1 && index <= this.PieceAmountTexts.Count - 1 && index <= this.PieceConsumeTexts.Count - 1 && index <= this.PiecePlus5Buttons.Count - 1 && index <= this.PiecePlusButtons.Count - 1 && index <= this.PieceMinusButtons.Count - 1; ++index)
              {
                ItemData itemDataByPieceKind = this.GetItemDataByPieceKind(this.mCurrentUnit, (UnitKakeraWindow.PieceKind) index);
                if (itemDataByPieceKind == null)
                  return;
                DataSource.Bind<ItemData>(this.PieceUnits[index], itemDataByPieceKind);
                this.PieceAmountTexts[index].text = itemDataByPieceKind.Num.ToString();
                int num = Math.Min(awakeNeedPieces, itemDataByPieceKind.Num);
                if (awakeNeedPieces > 0)
                {
                  flag3 = true;
                  awakeNeedPieces -= num;
                }
                this.PieceConsumeTexts[index].text = "0";
                if (unit.IsRental && index != 0)
                  this.PieceUnits[index].SetActive(false);
                else
                  this.PieceUnits[index].SetActive(true);
                ((UnityEventBase) this.PieceSliders[index].onValueChanged).RemoveAllListeners();
                this.PieceSliders[index].minValue = 0.0f;
                this.PieceSliders[index].maxValue = (float) Math.Min(numArray[index], pieceCountForAwake);
                this.PieceSliders[index].value = 0.0f;
                // ISSUE: method pointer
                ((UnityEvent<float>) this.PieceSliders[index].onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnAwakeLvSelect)));
                ((Selectable) this.PiecePlus5Buttons[index]).interactable = (double) this.PieceSliders[index].value <= (double) this.PieceSliders[index].maxValue - 5.0;
                ((Selectable) this.PiecePlusButtons[index]).interactable = (double) this.PieceSliders[index].value <= (double) this.PieceSliders[index].maxValue - 1.0;
                ((Selectable) this.PieceMinusButtons[index]).interactable = (double) this.PieceSliders[index].value >= (double) this.PieceSliders[index].minValue + 1.0;
                this.PieceMasks[index].SetActive((double) this.PieceSliders[index].maxValue <= 0.0);
              }
              for (int index = 0; index < this.mUnlockJobList.Count && index <= length; ++index)
              {
                if (this.mCacheCCJobs != null && this.mCacheCCJobs.Count > 0)
                {
                  JobSetParam js = this.mCurrentUnit.GetJobSetParam(index);
                  if (js == null || this.mCacheCCJobs.Find((Predicate<JobSetParam>) (v => v.iname == js.iname)) != null)
                    continue;
                }
                this.mUnlockJobList[index].SetActive(this.CheckUnlockJob(index, awakeLv));
              }
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AwakeResultLv, (UnityEngine.Object) null))
              {
                DebugUtility.LogError("Need Attatch to AwakeResultLv");
                return;
              }
              this.AwakeResultLv.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_LV", (object) 0);
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AwakeResultComb, (UnityEngine.Object) null))
              {
                DebugUtility.LogError("Need Attatch to AwakeResultComb");
                return;
              }
              this.AwakeResultComb.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_COMB", (object) 0);
              int num1 = 0;
              OInt[] artifactSlotUnlock = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.EquipArtifactSlotUnlock;
              for (int index = 0; index < artifactSlotUnlock.Length; ++index)
              {
                if ((int) artifactSlotUnlock[index] != 0 && (int) artifactSlotUnlock[index] > awakeLv && (int) artifactSlotUnlock[index] <= awakeLv)
                  ++num1;
              }
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.UnlockArtifactSlot, (UnityEngine.Object) null))
              {
                DebugUtility.LogError("Need Attatch to UnlockArtifactSlot");
                return;
              }
              bool flag4 = num1 > 0;
              this.UnlockArtifactSlot.SetActive(flag4);
              if (flag4)
                this.AwakeResultArtifactSlots.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_SLOT", (object) num1);
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.NotPieceDataMask, (UnityEngine.Object) null))
              {
                DebugUtility.LogError("Need Attatch to NotPieceDataMask");
                return;
              }
              this.NotPieceDataMask.SetActive(awakeNeedPieces > 0);
              if (flag3)
              {
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Kakera_Consume_Message, (UnityEngine.Object) null))
                {
                  DebugUtility.LogError("Need Attatch to Kakera_Consume_Message");
                  return;
                }
                this.Kakera_Consume_Message.text = LocalizedText.Get(awakeNeedPieces != 0 ? "sys.CONFIRM_KAKUSEI4" : "sys.CONFIRM_KAKUSEI2");
              }
              else
              {
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Kakera_Caution_Message, (UnityEngine.Object) null))
                {
                  DebugUtility.LogError("Need Attatch to Kakera_Caution_Message");
                  return;
                }
                this.Kakera_Caution_Message.text = LocalizedText.Get("sys.CONFIRM_KAKUSEI3");
                if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.CautionObject, (UnityEngine.Object) null))
                {
                  DebugUtility.LogError("Need Attatch to CautionObject");
                  return;
                }
                this.CautionObject.SetActive(true);
              }
            }
            else
            {
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Kakera_Caution_Message, (UnityEngine.Object) null))
              {
                DebugUtility.LogError("Need Attatch to Kakera_Caution_Message");
                return;
              }
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.NotPieceDataMask, (UnityEngine.Object) null))
              {
                DebugUtility.LogError("Need Attatch to NotPieceDataMask");
                return;
              }
              this.NotPieceDataMask.SetActive(false);
              this.Kakera_Caution_Message.text = LocalizedText.Get("sys.KAKUSEI_CAPPED");
            }
            int countForNextAwakeLv = this.GetPieceCountForNextAwakeLv(awakeLv, awakeLevelCap, (int) this.PieceSliders[0].value + (int) this.PieceSliders[1].value + (int) this.PieceSliders[2].value);
            this.NextNeedPieceCount.text = countForNextAwakeLv <= 0 ? LocalizedText.Get("sys.UNITAWAKE_NEXT_MAX") : string.Format(LocalizedText.Get("sys.UNITAWAKE_NEXT_COUNT"), (object) countForNextAwakeLv);
            for (int index = 0; index < 3; ++index)
            {
              ((Selectable) this.PiecePlus5Buttons[index]).interactable = (double) this.PieceSliders[index].value <= (double) this.PieceSliders[index].maxValue - 5.0;
              ((Selectable) this.PiecePlusButtons[index]).interactable = (double) this.PieceSliders[index].value <= (double) this.PieceSliders[index].maxValue - 1.0;
              ((Selectable) this.PieceMinusButtons[index]).interactable = (double) this.PieceSliders[index].value >= (double) this.PieceSliders[index].minValue + 1.0;
            }
            this.RefreshGainedQuests(this.mCurrentUnit);
            GameParameter.UpdateAll(((Component) this).gameObject);
          }
        }
      }
    }

    private bool CheckUnlockJob(int jobno, int awake_lv)
    {
      if (awake_lv == 0 || this.mCurrentUnit.CheckJobUnlockable(jobno))
        return false;
      JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(jobno);
      return jobSetParam != null && jobSetParam.lock_awakelv != 0 && jobSetParam.lock_awakelv <= awake_lv;
    }

    public int CalcCanAwakeMaxLv(
      int awakelv,
      int awakelvcap,
      int piece_amount,
      int element_piece_amount,
      int common_piece_amount)
    {
      int num1 = awakelv;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      if (masterParam == null)
        return num1;
      int val2 = awakelv;
      for (int awakeLv = val2; awakeLv < awakelvcap; ++awakeLv)
      {
        int awakeNeedPieces = masterParam.GetAwakeNeedPieces(awakeLv);
        if (piece_amount > 0 && awakeNeedPieces > 0)
        {
          int num2 = Math.Min(awakeNeedPieces, piece_amount);
          awakeNeedPieces -= num2;
          piece_amount -= num2;
        }
        if (element_piece_amount > 0 && awakeNeedPieces > 0)
        {
          int num3 = Math.Min(awakeNeedPieces, element_piece_amount);
          awakeNeedPieces -= num3;
          element_piece_amount -= num3;
        }
        if (common_piece_amount > 0 && awakeNeedPieces > 0)
        {
          int num4 = Math.Min(awakeNeedPieces, common_piece_amount);
          awakeNeedPieces -= num4;
          common_piece_amount -= num4;
        }
        if (awakeNeedPieces == 0)
          val2 = awakeLv + 1;
        if (piece_amount == 0 && element_piece_amount == 0 && common_piece_amount == 0)
          break;
      }
      return Math.Min(awakelvcap, val2);
    }

    private int GetPieceCountForAwake(
      int awakelv,
      int awakelvcap,
      int piece_amount,
      int element_piece_amount,
      int common_piece_amount)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      if (masterParam == null)
        return 0;
      int num = piece_amount + element_piece_amount + common_piece_amount;
      int pieceCountForAwake = 0;
      for (int awakeLv = awakelv; awakeLv < awakelvcap; ++awakeLv)
      {
        int awakeNeedPieces = masterParam.GetAwakeNeedPieces(awakeLv);
        if (awakeNeedPieces > 0 && num >= awakeNeedPieces)
        {
          pieceCountForAwake += awakeNeedPieces;
          num -= awakeNeedPieces;
        }
        else
          break;
      }
      return pieceCountForAwake;
    }

    private int GetPieceCountForNextAwakeLv(int awakelv, int awakelvcap, int pieceCount)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      if (masterParam == null)
        return 0;
      int num = 0;
      for (int awakeLv = awakelv; awakeLv < awakelvcap; ++awakeLv)
      {
        num = masterParam.GetAwakeNeedPieces(awakeLv);
        if (num > 0 && pieceCount >= num)
        {
          pieceCount -= num;
          num = 0;
        }
        else
          break;
      }
      return num - pieceCount;
    }

    private ItemData GetItemDataByPieceKind(UnitData unit, UnitKakeraWindow.PieceKind kind)
    {
      ItemData itemDataByPieceKind = (ItemData) null;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      switch (kind)
      {
        case UnitKakeraWindow.PieceKind.Unit:
          itemDataByPieceKind = player.FindItemDataByItemID(unit.UnitParam.piece);
          if (itemDataByPieceKind == null)
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(unit.UnitParam.piece);
            if (itemParam == null)
            {
              DebugUtility.LogError("Not Unit Piece Settings => [" + unit.UnitParam.iname + "]");
              return (ItemData) null;
            }
            itemDataByPieceKind = new ItemData();
            itemDataByPieceKind.Setup(0L, itemParam, 0);
            break;
          }
          break;
        case UnitKakeraWindow.PieceKind.Element:
          itemDataByPieceKind = unit.GetElementPieceData();
          if (itemDataByPieceKind == null)
          {
            ItemParam elementPieceParam = unit.GetElementPieceParam();
            if (elementPieceParam == null)
            {
              DebugUtility.LogError("[Unit Setting Error?]Not Element Piece!");
              return (ItemData) null;
            }
            itemDataByPieceKind = new ItemData();
            itemDataByPieceKind.Setup(0L, elementPieceParam, 0);
            break;
          }
          break;
        case UnitKakeraWindow.PieceKind.Common:
          itemDataByPieceKind = unit.GetCommonPieceData();
          if (itemDataByPieceKind == null)
          {
            ItemParam commonPieceParam = unit.GetCommonPieceParam();
            if (commonPieceParam == null)
            {
              DebugUtility.LogError("[FixParam Setting Error?]Not Common Piece Settings!");
              return (ItemData) null;
            }
            itemDataByPieceKind = new ItemData();
            itemDataByPieceKind.Setup(0L, commonPieceParam, 0);
            break;
          }
          break;
      }
      return itemDataByPieceKind;
    }

    private void OnAwakeLvSelect(float value) => this.PointRefresh();

    private int CalcNeedPieceAll(int value)
    {
      int num1 = 0;
      int awakeLv1 = this.mCurrentUnit.AwakeLv;
      int awakeLevelCap = this.mCurrentUnit.GetAwakeLevelCap();
      int num2 = this.mCurrentUnit.AwakeLv + value;
      if (value == 0 || awakeLv1 >= num2 || num2 > awakeLevelCap)
        return 0;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      for (int awakeLv2 = awakeLv1; awakeLv2 < num2; ++awakeLv2)
      {
        int awakeNeedPieces = masterParam.GetAwakeNeedPieces(awakeLv2);
        if (awakeLv2 >= 0)
          num1 += awakeNeedPieces;
      }
      return num1;
    }

    public void PointRefresh()
    {
      PlayerData player = MonoSingleton<GameManager>.GetInstanceDirect().Player;
      UnitData unit = new UnitData();
      unit.Setup(this.mCurrentUnit);
      int awakeLv = this.mCurrentUnit.AwakeLv;
      int awakeLevelCap = this.mCurrentUnit.GetAwakeLevelCap();
      int awake_lv = this.CalcCanAwakeMaxLv(awakeLv, awakeLevelCap, (int) this.PieceSliders[0].value, (int) this.PieceSliders[1].value, (int) this.PieceSliders[2].value);
      this.mTempUnit.SetVirtualAwakeLv(Mathf.Min(awakeLevelCap, awake_lv));
      int num1 = 0;
      OInt[] artifactSlotUnlock = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.EquipArtifactSlotUnlock;
      for (int index = 0; index < artifactSlotUnlock.Length; ++index)
      {
        if ((int) artifactSlotUnlock[index] != 0 && (int) artifactSlotUnlock[index] > unit.AwakeLv && (int) artifactSlotUnlock[index] <= awake_lv)
          ++num1;
      }
      int[] numArray = new int[3];
      ItemData itemDataByItemId = player.FindItemDataByItemID(unit.UnitParam.piece);
      numArray[0] = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
      ItemData elementPieceData = unit.GetElementPieceData();
      numArray[1] = elementPieceData == null ? 0 : elementPieceData.Num;
      ItemData commonPieceData = unit.GetCommonPieceData();
      numArray[2] = commonPieceData == null ? 0 : commonPieceData.Num;
      int num2 = this.GetPieceCountForAwake(unit.AwakeLv, unit.GetAwakeLevelCap(), numArray[0], numArray[1], numArray[2]);
      for (int index = 0; index < 3; ++index)
        num2 -= (int) this.PieceSliders[index].value;
      if (num2 < 0)
      {
        for (int index = 0; index < 3 && index <= this.PieceSliders.Count - 1; ++index)
        {
          if (index > this.mBeforeUseCounts.Count - 1)
            this.mBeforeUseCounts.Add(0);
          if ((double) this.PieceSliders[index].value != (double) this.mBeforeUseCounts[index])
          {
            this.PieceSliders[index].value += (float) num2;
            num2 = 0;
            break;
          }
        }
      }
      for (int index = 0; index < 3 && index <= this.PieceUnits.Count - 1 && index <= this.PieceAmountTexts.Count - 1 && index <= this.PieceConsumeTexts.Count - 1 && index <= this.PiecePlus5Buttons.Count - 1 && index <= this.PiecePlusButtons.Count - 1 && index <= this.PieceMinusButtons.Count - 1; ++index)
      {
        if (index > this.mBeforeUseCounts.Count - 1)
          this.mBeforeUseCounts.Add(0);
        ItemData itemDataByPieceKind = this.GetItemDataByPieceKind(unit, (UnitKakeraWindow.PieceKind) index);
        if (itemDataByPieceKind == null)
          return;
        DataSource.Bind<ItemData>(this.PieceUnits[index], itemDataByPieceKind);
        this.PieceAmountTexts[index].text = itemDataByPieceKind.Num.ToString();
        int num3 = Math.Min((int) this.PieceSliders[index].value, itemDataByPieceKind.Num);
        this.PieceConsumeTexts[index].text = num3.ToString();
        this.mBeforeUseCounts[index] = num3;
      }
      int num4 = awake_lv - unit.AwakeLv;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AwakeResultLv, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("Need Attatch to AwakeResultLv");
      }
      else
      {
        this.AwakeResultLv.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_LV", (object) num4);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AwakeResultComb, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("Need Attatch to AwakeResultComb");
        }
        else
        {
          this.AwakeResultComb.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_COMB", (object) num4);
          for (int index = 0; index < this.mUnlockJobList.Count && index <= unit.Jobs.Length; ++index)
          {
            if (this.mCacheCCJobs != null && this.mCacheCCJobs.Count > 0)
            {
              JobSetParam js = unit.GetJobSetParam(index);
              if (js == null || this.mCacheCCJobs.Find((Predicate<JobSetParam>) (v => v.iname == js.iname)) != null)
                continue;
            }
            this.mUnlockJobList[index].SetActive(this.CheckUnlockJob(index, awake_lv));
          }
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.UnlockArtifactSlot, (UnityEngine.Object) null))
            DebugUtility.LogError("Need Attatch to UnlockArtifactSlot");
          else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AwakeResultArtifactSlots, (UnityEngine.Object) null))
          {
            DebugUtility.LogError("Need Attatch to AwakeResultArtifactSlots");
          }
          else
          {
            bool flag = num1 > 0;
            this.UnlockArtifactSlot.SetActive(flag);
            if (flag)
              this.AwakeResultArtifactSlots.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_SLOT", (object) num1);
            for (int index = 0; index < 3; ++index)
            {
              ((Selectable) this.PiecePlus5Buttons[index]).interactable = (double) this.PieceSliders[index].value <= (double) this.PieceSliders[index].maxValue - 5.0 && num2 > 0;
              ((Selectable) this.PiecePlusButtons[index]).interactable = (double) this.PieceSliders[index].value <= (double) this.PieceSliders[index].maxValue - 1.0 && num2 > 0;
              ((Selectable) this.PieceMinusButtons[index]).interactable = (double) this.PieceSliders[index].value >= (double) this.PieceSliders[index].minValue + 1.0;
            }
            ((Selectable) this.DecideButton).interactable = awakeLevelCap > awakeLv && unit.CheckUnitAwaking() && awake_lv > awakeLv;
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.NextNeedPieceCount, (UnityEngine.Object) null))
            {
              DebugUtility.LogError("Need Attatch to NextNeedPieceCount");
            }
            else
            {
              int countForNextAwakeLv = this.GetPieceCountForNextAwakeLv(awakeLv, awakeLevelCap, (int) this.PieceSliders[0].value + (int) this.PieceSliders[1].value + (int) this.PieceSliders[2].value);
              this.NextNeedPieceCount.text = countForNextAwakeLv <= 0 ? LocalizedText.Get("sys.UNITAWAKE_NEXT_MAX") : string.Format(LocalizedText.Get("sys.UNITAWAKE_NEXT_COUNT"), (object) countForNextAwakeLv);
              GameParameter.UpdateAll(((Component) this).gameObject);
            }
          }
        }
      }
    }

    private void RefreshGainedQuests(UnitData unit)
    {
      this.ClearPanel();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestList, (UnityEngine.Object) null))
        return;
      this.QuestList.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NotFoundGainQuestObject, (UnityEngine.Object) null))
      {
        Text component = this.NotFoundGainQuestObject.GetComponent<Text>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.text = LocalizedText.Get("sys.UNIT_GAINED_COMMENT");
        this.NotFoundGainQuestObject.SetActive(true);
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.QuestListParent, (UnityEngine.Object) null) || unit == null)
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(unit.UnitParam.piece);
      DataSource.Bind<ItemParam>(this.QuestList, itemParam);
      if (this.LastUpadatedItemParam != itemParam)
      {
        this.SetScrollTop();
        this.LastUpadatedItemParam = itemParam;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      foreach (QuestParam itemDropQuest in QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime()))
        this.AddPanel(itemDropQuest, availableQuests);
    }

    private void SetScrollTop()
    {
      RectTransform component = ((Component) this.QuestListParent).GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      Vector2 anchoredPosition = component.anchoredPosition;
      anchoredPosition.y = 0.0f;
      component.anchoredPosition = anchoredPosition;
    }

    public void ClearPanel()
    {
      this.mGainedQuests.Clear();
      for (int index = 0; index < ((Transform) this.QuestListParent).childCount; ++index)
      {
        GameObject gameObject = ((Component) ((Transform) this.QuestListParent).GetChild(index)).gameObject;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestListItemTemplate, (UnityEngine.Object) gameObject))
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
    }

    private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
    {
      this.QuestList.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NotFoundGainQuestObject, (UnityEngine.Object) null))
        this.NotFoundGainQuestObject.SetActive(false);
      if (questparam == null || questparam.IsMulti)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestListItemTemplate);
      SRPG_Button component1 = gameObject.GetComponent<SRPG_Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        component1.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
      this.mGainedQuests.Add(gameObject);
      Button component2 = gameObject.GetComponent<Button>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
      {
        bool flag1 = questparam.IsDateUnlock();
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questparam)) != null;
        ((Selectable) component2).interactable = flag1 && flag2 && LevelLock.IsPlayableQuest(questparam);
        if (!((Selectable) component2).interactable && flag2 && questparam.Chapter != null && MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(questparam.Chapter.iname) && LevelLock.IsPlayableQuest(questparam))
          ((Selectable) component2).interactable = true;
      }
      DataSource.Bind<QuestParam>(gameObject, questparam);
      gameObject.transform.SetParent((Transform) this.QuestListParent, false);
      gameObject.SetActive(true);
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[this.mGainedQuests.IndexOf(((Component) button).gameObject)], (QuestParam) null);
      if (quest == null)
        return;
      ArchiveParam archiveByArea = MonoSingleton<GameManager>.Instance.FindArchiveByArea(quest.ChapterID);
      if (archiveByArea != null && archiveByArea.IsAvailable() && MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Archive))
      {
        if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
        }
        else
        {
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          if (LevelLock.IsNeedCheckUnlockConds(quest))
          {
            UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
              return;
          }
          if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpen(archiveByArea.iname))
          {
            GlobalVars.SelectedQuestID = quest.iname;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          }
          else
          {
            GlobalVars.SelectedArchiveID = archiveByArea.iname;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
          }
        }
      }
      else if (!quest.IsDateUnlock())
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (LevelLock.IsNeedCheckUnlockConds(quest))
        {
          UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
            return;
        }
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnDecideClick()
    {
      int awakeLv = this.mCurrentUnit.AwakeLv;
      if (this.CalcCanAwakeMaxLv(awakeLv, this.mCurrentUnit.GetAwakeLevelCap(), (int) this.PieceSliders[0].value, (int) this.PieceSliders[1].value, (int) this.PieceSliders[2].value) <= awakeLv)
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ConfirmWindowPrefab, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("Need Attatch to ConfirmWindowPrefab");
      }
      else
      {
        int[] clampUseItemCount = this.GetClampUseItemCount();
        List<ItemData> itemDataList = new List<ItemData>();
        if (clampUseItemCount[0] > 0)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.mCurrentUnit.UnitParam.piece);
          ItemData itemData = new ItemData();
          itemData.Setup(0L, itemParam, clampUseItemCount[0]);
          itemDataList.Add(itemData);
        }
        if (clampUseItemCount[1] > 0)
        {
          ItemParam elementPieceParam = this.mCurrentUnit.GetElementPieceParam();
          ItemData itemData = new ItemData();
          itemData.Setup(0L, elementPieceParam, clampUseItemCount[1]);
          itemDataList.Add(itemData);
        }
        if (clampUseItemCount[2] > 0)
        {
          ItemParam commonPieceParam = this.mCurrentUnit.GetCommonPieceParam();
          ItemData itemData = new ItemData();
          itemData.Setup(0L, commonPieceParam, clampUseItemCount[2]);
          itemDataList.Add(itemData);
        }
        UnityEngine.Object.Instantiate<UnitKakeraConfirm>(this.ConfirmWindowPrefab, ((Component) this).transform).Setup(new UnitKakeraConfirm.OnDecide(this.OnKakusei), itemDataList.ToArray());
      }
    }

    private int[] GetClampUseItemCount()
    {
      int[] clampUseItemCount = new int[3];
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      if (masterParam == null)
        return clampUseItemCount;
      int num1 = 0;
      for (int index = 0; index < 3; ++index)
      {
        clampUseItemCount[index] = (int) this.PieceSliders[index].value;
        num1 += clampUseItemCount[index];
      }
      for (int awakeLv = this.mCurrentUnit.AwakeLv; awakeLv < this.mCurrentUnit.GetAwakeLevelCap(); ++awakeLv)
      {
        int awakeNeedPieces = masterParam.GetAwakeNeedPieces(awakeLv);
        if (awakeNeedPieces > 0 && num1 >= awakeNeedPieces)
          num1 -= awakeNeedPieces;
        else
          break;
      }
      for (int index = 2; index >= 0 && num1 > 0; --index)
      {
        int num2 = Mathf.Min(clampUseItemCount[index], num1);
        clampUseItemCount[index] -= num2;
        num1 -= num2;
      }
      return clampUseItemCount;
    }

    private void OnKakusei()
    {
      if (this.OnAwakeAccept != null)
      {
        new UnitData().Setup(this.mCurrentUnit);
        int[] clampUseItemCount = this.GetClampUseItemCount();
        this.OnAwakeAccept(clampUseItemCount[0], clampUseItemCount[1], clampUseItemCount[2]);
      }
      else
        MonoSingleton<GameManager>.Instance.Player.AwakingUnit(this.mCurrentUnit);
    }

    private void OnMaxClick()
    {
      for (int index = 0; index < 3; ++index)
        this.PieceSliders[index].value = 0.0f;
      this.PointRefresh();
      for (int index = 0; index < 3; ++index)
      {
        if (this.PieceMaxToggles[index].isOn && (!this.mCurrentUnit.IsRental || index <= 0))
        {
          this.PieceSliders[index].value = this.PieceSliders[index].maxValue;
          this.PointRefresh();
        }
      }
    }

    private void OnCancelClick()
    {
      for (int index = 0; index < 3 && index <= this.PieceSliders.Count - 1; ++index)
        this.PieceSliders[index].value = 0.0f;
    }

    private void RefreshAwakeLv(UnitKakeraWindow.PieceKind kind, int value = 0)
    {
      if (kind > (UnitKakeraWindow.PieceKind) (this.PieceSliders.Count - 1))
        return;
      Slider pieceSlider = this.PieceSliders[(int) kind];
      pieceSlider.value = Mathf.Clamp(pieceSlider.value + (float) value, pieceSlider.minValue, pieceSlider.maxValue);
    }

    private void SaveSelectUseMax()
    {
      bool flag = false;
      List<string> stringList = new List<string>();
      for (int index = 0; index < 3; ++index)
      {
        if (this.PieceMaxToggles[index].isOn)
        {
          stringList.Add(index.ToString());
          if (!this.mCurrentUnit.IsRental || index <= 0)
            flag = true;
        }
      }
      string str = stringList == null ? string.Empty : string.Join("|", stringList.ToArray());
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.UNIT_AWAKE_PIECE_CHECKS, str, true);
      if (this.mCurrentUnit.AwakeLv >= this.mCurrentUnit.GetAwakeLevelCap())
        return;
      ((Selectable) this.MaxButton).interactable = flag;
    }

    public delegate void AwakeEvent(
      int pieceCountUnit,
      int pieceCountElement,
      int pieceCountCommon);

    private enum PieceKind
    {
      Unit,
      Element,
      Common,
      Max,
    }
  }
}
