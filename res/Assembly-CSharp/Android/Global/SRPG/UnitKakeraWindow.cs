﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitKakeraWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "クエスト選択", FlowNode.PinTypes.Output, 100)]
  public class UnitKakeraWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mGainedQuests = new List<GameObject>();
    private List<ItemData> mUsedElemPieces = new List<ItemData>();
    private List<GameObject> mUnlockJobList = new List<GameObject>();
    private List<JobSetParam> mCacheCCJobs = new List<JobSetParam>();
    public UnitKakeraWindow.KakuseiWindowEvent OnKakuseiAccept;
    public UnitKakeraWindow.AwakeEvent OnAwakeAccept;
    public GameObject QuestList;
    public RectTransform QuestListParent;
    public GameObject QuestListItemTemplate;
    public GameObject Kakera_Unit;
    public GameObject Kakera_Elem;
    public GameObject Kakera_Common;
    public Text Kakera_Consume_Unit;
    public Text Kakera_Consume_Elem;
    public Text Kakera_Consume_Common;
    public Text Kakera_Amount_Unit;
    public Text Kakera_Amount_Elem;
    public Text Kakera_Amount_Common;
    public Text Kakera_Consume_Message;
    public Text Kakera_Caution_Message;
    public GameObject NotFoundGainQuestObject;
    public GameObject CautionObject;
    public Button DecideButton;
    public GameObject JobUnlock;
    public Slider SelectAwakeSlider;
    public GameObject UnlockArtifactSlot;
    public Button PlusBtn;
    public Button MinusBtn;
    public Text AwakeResultLv;
    public Text AwakeResultComb;
    public Text AwakeResultArtifactSlots;
    public RectTransform JobUnlockParent;
    public GameObject NotPieceDataMask;
    private UnitData mCurrentUnit;
    private JobParam mUnlockJobParam;
    private ItemParam LastUpadatedItemParam;
    private UnitData mTempUnit;

    private void Start()
    {
      if ((UnityEngine.Object) this.QuestListItemTemplate != (UnityEngine.Object) null)
        this.QuestListItemTemplate.SetActive(false);
      if ((UnityEngine.Object) this.DecideButton != (UnityEngine.Object) null)
        this.DecideButton.onClick.AddListener(new UnityAction(this.OnDecideClick));
      if ((UnityEngine.Object) this.PlusBtn != (UnityEngine.Object) null)
        this.PlusBtn.onClick.AddListener(new UnityAction(this.OnAdd));
      if ((UnityEngine.Object) this.MinusBtn != (UnityEngine.Object) null)
        this.MinusBtn.onClick.AddListener(new UnityAction(this.OnRemove));
      if (!((UnityEngine.Object) this.JobUnlock != (UnityEngine.Object) null))
        return;
      this.JobUnlock.SetActive(false);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh(this.mCurrentUnit, this.mUnlockJobParam);
    }

    public void Refresh(UnitData unit = null, JobParam jobUnlock = null)
    {
      if (unit == null)
        return;
      this.mCurrentUnit = unit;
      this.mUnlockJobParam = jobUnlock;
      this.mUsedElemPieces.Clear();
      this.mCacheCCJobs.Clear();
      this.mTempUnit = new UnitData();
      this.mTempUnit.Setup(unit);
      for (int index = 0; index < this.mUnlockJobList.Count; ++index)
      {
        if ((UnityEngine.Object) this.mUnlockJobList[index] != (UnityEngine.Object) null)
        {
          DataSource.Bind<JobParam>(this.mUnlockJobList[index], (JobParam) null);
          this.mUnlockJobList[index].SetActive(false);
        }
      }
      int length = unit.Jobs.Length;
      if (this.mUnlockJobList.Count < length)
      {
        int num = length - this.mUnlockJobList.Count;
        for (int index = 0; index < num; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.JobUnlock);
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          {
            gameObject.transform.SetParent((Transform) this.JobUnlockParent, false);
            this.mUnlockJobList.Add(gameObject);
          }
        }
      }
      JobSetParam[] changeJobSetParam = MonoSingleton<GameManager>.Instance.MasterParam.GetClassChangeJobSetParam(unit.UnitParam.iname);
      if (changeJobSetParam != null)
        this.mCacheCCJobs.AddRange((IEnumerable<JobSetParam>) changeJobSetParam);
      for (int jobNo = 0; jobNo < length; ++jobNo)
      {
        if (!unit.CheckJobUnlockable(jobNo) && (UnityEngine.Object) this.mUnlockJobList[jobNo] != (UnityEngine.Object) null)
          DataSource.Bind<JobParam>(this.mUnlockJobList[jobNo], unit.Jobs[jobNo].Param);
      }
      DataSource.Bind<UnitData>(this.gameObject, (UnitData) null);
      GameParameter.UpdateAll(this.gameObject);
      DataSource.Bind<UnitData>(this.gameObject, this.mTempUnit);
      int awakeLv = unit.AwakeLv;
      bool flag1 = unit.GetAwakeLevelCap() > awakeLv;
      if ((UnityEngine.Object) this.CautionObject != (UnityEngine.Object) null)
        this.CautionObject.SetActive(!flag1);
      if ((UnityEngine.Object) this.DecideButton != (UnityEngine.Object) null)
        this.DecideButton.interactable = flag1 && unit.CheckUnitAwaking();
      if ((UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null)
        this.SelectAwakeSlider.interactable = flag1 && unit.CheckUnitAwaking();
      if (flag1)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        int awakeNeedPieces = unit.GetAwakeNeedPieces();
        bool flag2 = false;
        if ((UnityEngine.Object) this.Kakera_Unit != (UnityEngine.Object) null)
        {
          ItemData data = player.FindItemDataByItemID((string) unit.UnitParam.piece);
          if (data == null)
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam((string) unit.UnitParam.piece);
            if (itemParam == null)
            {
              DebugUtility.LogError("Not Unit Piece Settings => [" + unit.UnitParam.iname + "]");
              return;
            }
            data = new ItemData();
            data.Setup(0L, itemParam, 0);
          }
          if (data != null)
            DataSource.Bind<ItemData>(this.Kakera_Unit, data);
          if ((UnityEngine.Object) this.Kakera_Amount_Unit != (UnityEngine.Object) null)
            this.Kakera_Amount_Unit.text = data.Num.ToString();
          int num = Math.Min(awakeNeedPieces, data.Num);
          if (awakeNeedPieces > 0)
          {
            num = Math.Min(awakeNeedPieces, data.Num);
            flag2 = true;
            awakeNeedPieces -= num;
          }
          if ((UnityEngine.Object) this.Kakera_Consume_Unit != (UnityEngine.Object) null)
            this.Kakera_Consume_Unit.text = "@" + num.ToString();
          this.Kakera_Unit.SetActive(true);
        }
        if ((UnityEngine.Object) this.Kakera_Elem != (UnityEngine.Object) null)
        {
          ItemData data = unit.GetElementPieceData();
          if (data == null)
          {
            ItemParam elementPieceParam = unit.GetElementPieceParam();
            if (elementPieceParam == null)
            {
              DebugUtility.LogError("[Unit Setting Error?]Not Element Piece!");
              return;
            }
            data = new ItemData();
            data.Setup(0L, elementPieceParam, 0);
          }
          if (data != null)
            DataSource.Bind<ItemData>(this.Kakera_Elem, data);
          if ((UnityEngine.Object) this.Kakera_Amount_Elem != (UnityEngine.Object) null)
            this.Kakera_Amount_Elem.text = data.Num.ToString();
          int num = Math.Min(awakeNeedPieces, data.Num);
          if (data.Num > 0 && awakeNeedPieces > 0)
          {
            num = Math.Min(awakeNeedPieces, data.Num);
            flag2 = true;
            awakeNeedPieces -= num;
            if (!this.mUsedElemPieces.Contains(data))
              this.mUsedElemPieces.Add(data);
          }
          if ((UnityEngine.Object) this.Kakera_Consume_Elem != (UnityEngine.Object) null)
            this.Kakera_Consume_Elem.text = "@" + num.ToString();
          this.Kakera_Elem.SetActive(true);
        }
        if ((UnityEngine.Object) this.Kakera_Common != (UnityEngine.Object) null)
        {
          ItemData data = unit.GetCommonPieceData();
          if (data == null)
          {
            ItemParam commonPieceParam = unit.GetCommonPieceParam();
            if (commonPieceParam == null)
            {
              DebugUtility.LogError("[FixParam Setting Error?]Not Common Piece Settings!");
              return;
            }
            data = new ItemData();
            data.Setup(0L, commonPieceParam, 0);
          }
          if (data != null)
            DataSource.Bind<ItemData>(this.Kakera_Common, data);
          if ((UnityEngine.Object) this.Kakera_Amount_Common != (UnityEngine.Object) null)
            this.Kakera_Amount_Common.text = data.Num.ToString();
          int num = 0;
          if (data.Num > 0 && awakeNeedPieces > 0)
          {
            num = Math.Min(awakeNeedPieces, data.Num);
            flag2 = true;
            awakeNeedPieces -= num;
            if (!this.mUsedElemPieces.Contains(data))
              this.mUsedElemPieces.Add(data);
          }
          if ((UnityEngine.Object) this.Kakera_Consume_Common != (UnityEngine.Object) null)
            this.Kakera_Consume_Common.text = "@" + num.ToString();
          this.Kakera_Common.SetActive(true);
        }
        if ((UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null)
        {
          ItemData itemDataByItemId = player.FindItemDataByItemID((string) unit.UnitParam.piece);
          int piece_amount = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
          ItemData elementPieceData = unit.GetElementPieceData();
          int element_piece_amount = elementPieceData == null ? 0 : elementPieceData.Num;
          ItemData commonPieceData = unit.GetCommonPieceData();
          int common_piece_amount = commonPieceData == null ? 0 : commonPieceData.Num;
          int num = this.CalcCanAwakeMaxLv(unit.AwakeLv, unit.GetAwakeLevelCap(), piece_amount, element_piece_amount, common_piece_amount);
          this.SelectAwakeSlider.onValueChanged.RemoveAllListeners();
          this.SelectAwakeSlider.minValue = num - unit.AwakeLv <= 0 ? 0.0f : 1f;
          this.SelectAwakeSlider.maxValue = (float) (num - unit.AwakeLv);
          this.SelectAwakeSlider.value = this.SelectAwakeSlider.minValue;
          this.SelectAwakeSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnAwakeLvSelect));
        }
        if (this.mUnlockJobList != null)
        {
          for (int index = 0; index < this.mUnlockJobList.Count && index <= length; ++index)
          {
            if (this.mCacheCCJobs != null && this.mCacheCCJobs.Count > 0)
            {
              JobSetParam js = unit.GetJobSetParam(index);
              if (js == null || this.mCacheCCJobs.Find((Predicate<JobSetParam>) (v => v.iname == js.iname)) != null)
                continue;
            }
            this.mUnlockJobList[index].SetActive(this.CheckUnlockJob(index, awakeLv + (int) this.SelectAwakeSlider.value));
          }
        }
        if ((UnityEngine.Object) this.AwakeResultLv != (UnityEngine.Object) null)
          this.AwakeResultLv.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_LV", new object[1]
          {
            (object) (!((UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null) ? 1 : (int) this.SelectAwakeSlider.value)
          });
        if ((UnityEngine.Object) this.AwakeResultComb != (UnityEngine.Object) null)
          this.AwakeResultComb.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_COMB", new object[1]
          {
            (object) (!((UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null) ? 1 : (int) this.SelectAwakeSlider.value)
          });
        int num1 = 0;
        OInt[] artifactSlotUnlock = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.EquipArtifactSlotUnlock;
        for (int index = 0; index < artifactSlotUnlock.Length; ++index)
        {
          if ((int) artifactSlotUnlock[index] != 0 && (int) artifactSlotUnlock[index] > unit.AwakeLv && (int) artifactSlotUnlock[index] <= unit.AwakeLv + (int) this.SelectAwakeSlider.value)
            ++num1;
        }
        if ((UnityEngine.Object) this.UnlockArtifactSlot != (UnityEngine.Object) null)
        {
          this.UnlockArtifactSlot.SetActive(num1 > 0);
          if (num1 > 0)
            this.AwakeResultArtifactSlots.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_SLOT", new object[1]
            {
              (object) num1
            });
        }
        if ((UnityEngine.Object) this.NotPieceDataMask != (UnityEngine.Object) null)
          this.NotPieceDataMask.SetActive(awakeNeedPieces > 0);
        if (flag2)
        {
          if ((UnityEngine.Object) this.Kakera_Consume_Message != (UnityEngine.Object) null)
            this.Kakera_Consume_Message.text = LocalizedText.Get(awakeNeedPieces != 0 ? "sys.CONFIRM_KAKUSEI4" : "sys.CONFIRM_KAKUSEI2");
        }
        else
        {
          if ((UnityEngine.Object) this.Kakera_Caution_Message != (UnityEngine.Object) null)
            this.Kakera_Caution_Message.text = LocalizedText.Get("sys.CONFIRM_KAKUSEI3");
          if ((UnityEngine.Object) this.CautionObject != (UnityEngine.Object) null)
            this.CautionObject.SetActive(true);
        }
      }
      else if ((UnityEngine.Object) this.Kakera_Caution_Message != (UnityEngine.Object) null)
        this.Kakera_Caution_Message.text = LocalizedText.Get("sys.KAKUSEI_CAPPED");
      if ((UnityEngine.Object) this.PlusBtn != (UnityEngine.Object) null)
        this.PlusBtn.interactable = (UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null && (double) this.SelectAwakeSlider.value < (double) this.SelectAwakeSlider.maxValue;
      if ((UnityEngine.Object) this.MinusBtn != (UnityEngine.Object) null)
        this.MinusBtn.interactable = (UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null && (double) this.SelectAwakeSlider.value > (double) this.SelectAwakeSlider.minValue;
      this.RefreshGainedQuests(unit);
      GameParameter.UpdateAll(this.gameObject);
    }

    private bool CheckUnlockJob(int jobno, int awake_lv)
    {
      if (awake_lv == 0 || this.mCurrentUnit.CheckJobUnlockable(jobno))
        return false;
      JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(jobno);
      return jobSetParam != null && jobSetParam.lock_awakelv != 0 && jobSetParam.lock_awakelv <= awake_lv;
    }

    private int CalcCanAwakeMaxLv(int awakelv, int awakelvcap, int piece_amount, int element_piece_amount, int common_piece_amount)
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
          int num2 = Math.Min(awakeNeedPieces, element_piece_amount);
          awakeNeedPieces -= num2;
          element_piece_amount -= num2;
        }
        if (common_piece_amount > 0 && awakeNeedPieces > 0)
        {
          int num2 = Math.Min(awakeNeedPieces, common_piece_amount);
          awakeNeedPieces -= num2;
          common_piece_amount -= num2;
        }
        if (awakeNeedPieces == 0)
          val2 = awakeLv + 1;
        if (piece_amount == 0 && element_piece_amount == 0 && common_piece_amount == 0)
          break;
      }
      return Math.Min(awakelvcap, val2);
    }

    private void OnAwakeLvSelect(float value)
    {
      this.PointRefresh();
    }

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
      this.mUsedElemPieces.Clear();
      PlayerData player = MonoSingleton<GameManager>.GetInstanceDirect().Player;
      UnitData unitData = new UnitData();
      unitData.Setup(this.mCurrentUnit);
      int awake_lv = unitData.AwakeLv + (int) this.SelectAwakeSlider.value;
      int val1 = this.CalcNeedPieceAll((int) this.SelectAwakeSlider.value);
      this.mTempUnit.SetVirtualAwakeLv(Mathf.Min(unitData.GetAwakeLevelCap(), awake_lv - 1));
      int num1 = 0;
      OInt[] artifactSlotUnlock = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.EquipArtifactSlotUnlock;
      for (int index = 0; index < artifactSlotUnlock.Length; ++index)
      {
        if ((int) artifactSlotUnlock[index] != 0 && (int) artifactSlotUnlock[index] > unitData.AwakeLv && (int) artifactSlotUnlock[index] <= unitData.AwakeLv + (int) this.SelectAwakeSlider.value)
          ++num1;
      }
      if ((UnityEngine.Object) this.Kakera_Unit != (UnityEngine.Object) null)
      {
        ItemData itemData = player.FindItemDataByItemID((string) unitData.UnitParam.piece);
        if (itemData == null)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam((string) unitData.UnitParam.piece);
          if (itemParam == null)
          {
            DebugUtility.LogError("Not Unit Piece Settings => [" + unitData.UnitParam.iname + "]");
            return;
          }
          itemData = new ItemData();
          itemData.Setup(0L, itemParam, 0);
        }
        int num2 = Math.Min(val1, itemData.Num);
        if (val1 > 0)
        {
          num2 = Math.Min(val1, itemData.Num);
          val1 -= num2;
        }
        if ((UnityEngine.Object) this.Kakera_Consume_Unit != (UnityEngine.Object) null)
          this.Kakera_Consume_Unit.text = "@" + num2.ToString();
        this.Kakera_Unit.SetActive(true);
      }
      if ((UnityEngine.Object) this.Kakera_Elem != (UnityEngine.Object) null && (UnityEngine.Object) this.Kakera_Elem != (UnityEngine.Object) null)
      {
        ItemData data = unitData.GetElementPieceData();
        if (data == null)
        {
          ItemParam elementPieceParam = unitData.GetElementPieceParam();
          if (elementPieceParam == null)
          {
            DebugUtility.LogError("[Unit Setting Error?]Not Element Piece!");
            return;
          }
          data = new ItemData();
          data.Setup(0L, elementPieceParam, 0);
        }
        if (data != null)
          DataSource.Bind<ItemData>(this.Kakera_Elem, data);
        if ((UnityEngine.Object) this.Kakera_Amount_Elem != (UnityEngine.Object) null)
          this.Kakera_Amount_Elem.text = data.Num.ToString();
        int num2 = 0;
        if (data.Num > 0 && val1 > 0)
        {
          num2 = Math.Min(val1, data.Num);
          val1 -= num2;
          if (!this.mUsedElemPieces.Contains(data))
            this.mUsedElemPieces.Add(data);
        }
        if ((UnityEngine.Object) this.Kakera_Consume_Elem != (UnityEngine.Object) null)
          this.Kakera_Consume_Elem.text = "@" + num2.ToString();
        this.Kakera_Elem.SetActive(true);
      }
      if ((UnityEngine.Object) this.Kakera_Common != (UnityEngine.Object) null)
      {
        ItemData data = unitData.GetCommonPieceData();
        if (data == null)
        {
          ItemParam commonPieceParam = unitData.GetCommonPieceParam();
          if (commonPieceParam == null)
          {
            DebugUtility.LogError("[FixParam Setting Error?]Not Common Piece Settings!");
            return;
          }
          data = new ItemData();
          data.Setup(0L, commonPieceParam, 0);
        }
        if (data != null)
          DataSource.Bind<ItemData>(this.Kakera_Common, data);
        if ((UnityEngine.Object) this.Kakera_Amount_Common != (UnityEngine.Object) null)
          this.Kakera_Amount_Common.text = data.Num.ToString();
        int num2 = 0;
        if (data.Num > 0 && val1 > 0)
        {
          num2 = Math.Min(val1, data.Num);
          int num3 = val1 - num2;
          if (!this.mUsedElemPieces.Contains(data))
            this.mUsedElemPieces.Add(data);
        }
        if ((UnityEngine.Object) this.Kakera_Consume_Common != (UnityEngine.Object) null)
          this.Kakera_Consume_Common.text = "@" + num2.ToString();
        this.Kakera_Common.SetActive(true);
      }
      if ((UnityEngine.Object) this.AwakeResultLv != (UnityEngine.Object) null)
        this.AwakeResultLv.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_LV", new object[1]
        {
          (object) (!((UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null) ? 1 : (int) this.SelectAwakeSlider.value)
        });
      if ((UnityEngine.Object) this.AwakeResultComb != (UnityEngine.Object) null)
        this.AwakeResultComb.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_COMB", new object[1]
        {
          (object) (!((UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null) ? 1 : (int) this.SelectAwakeSlider.value)
        });
      if (this.mUnlockJobList != null)
      {
        for (int index = 0; index < this.mUnlockJobList.Count && index <= unitData.Jobs.Length; ++index)
        {
          if (this.mCacheCCJobs != null && this.mCacheCCJobs.Count > 0)
          {
            JobSetParam js = unitData.GetJobSetParam(index);
            if (js == null || this.mCacheCCJobs.Find((Predicate<JobSetParam>) (v => v.iname == js.iname)) != null)
              continue;
          }
          this.mUnlockJobList[index].SetActive(this.CheckUnlockJob(index, awake_lv));
        }
      }
      if ((UnityEngine.Object) this.UnlockArtifactSlot != (UnityEngine.Object) null)
      {
        this.UnlockArtifactSlot.SetActive(num1 > 0);
        if (num1 > 0 && (UnityEngine.Object) this.AwakeResultArtifactSlots != (UnityEngine.Object) null)
          this.AwakeResultArtifactSlots.text = LocalizedText.Get("sys.TEXT_UNITAWAKE_RESULT_SLOT", new object[1]
          {
            (object) num1
          });
      }
      if ((UnityEngine.Object) this.PlusBtn != (UnityEngine.Object) null)
        this.PlusBtn.interactable = (UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null && (double) this.SelectAwakeSlider.value < (double) this.SelectAwakeSlider.maxValue;
      if ((UnityEngine.Object) this.MinusBtn != (UnityEngine.Object) null)
        this.MinusBtn.interactable = (UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null && (double) this.SelectAwakeSlider.value > (double) this.SelectAwakeSlider.minValue;
      GameParameter.UpdateAll(this.gameObject);
    }

    private void RefreshGainedQuests(UnitData unit)
    {
      this.ClearPanel();
      if ((UnityEngine.Object) this.QuestList == (UnityEngine.Object) null)
        return;
      this.QuestList.SetActive(false);
      if ((UnityEngine.Object) this.NotFoundGainQuestObject != (UnityEngine.Object) null)
      {
        Text component = this.NotFoundGainQuestObject.GetComponent<Text>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.text = LocalizedText.Get("sys.UNIT_GAINED_COMMENT");
        this.NotFoundGainQuestObject.SetActive(true);
      }
      if ((UnityEngine.Object) this.QuestListItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.QuestListParent == (UnityEngine.Object) null || unit == null)
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam((string) unit.UnitParam.piece);
      DataSource.Bind<ItemParam>(this.QuestList, itemParam);
      if (this.LastUpadatedItemParam != itemParam)
      {
        this.SetScrollTop();
        this.LastUpadatedItemParam = itemParam;
      }
      if (!((UnityEngine.Object) QuestDropParam.Instance != (UnityEngine.Object) null))
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      using (List<QuestParam>.Enumerator enumerator = QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime()).GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.AddPanel(enumerator.Current, availableQuests);
      }
    }

    private void SetScrollTop()
    {
      RectTransform component = this.QuestListParent.GetComponent<RectTransform>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      Vector2 anchoredPosition = component.anchoredPosition;
      anchoredPosition.y = 0.0f;
      component.anchoredPosition = anchoredPosition;
    }

    public void ClearPanel()
    {
      this.mGainedQuests.Clear();
      for (int index = 0; index < this.QuestListParent.childCount; ++index)
      {
        GameObject gameObject = this.QuestListParent.GetChild(index).gameObject;
        if ((UnityEngine.Object) this.QuestListItemTemplate != (UnityEngine.Object) gameObject)
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
    }

    private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
    {
      this.QuestList.SetActive(true);
      if ((UnityEngine.Object) this.NotFoundGainQuestObject != (UnityEngine.Object) null)
        this.NotFoundGainQuestObject.SetActive(false);
      if (questparam == null || questparam.IsMulti)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestListItemTemplate);
      SRPG_Button component1 = gameObject.GetComponent<SRPG_Button>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        component1.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
      this.mGainedQuests.Add(gameObject);
      Button component2 = gameObject.GetComponent<Button>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
      {
        bool flag1 = questparam.IsDateUnlock(-1L);
        bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questparam)) != null;
        component2.interactable = flag1 && flag2;
      }
      DataSource.Bind<QuestParam>(gameObject, questparam);
      gameObject.transform.SetParent((Transform) this.QuestListParent, false);
      gameObject.SetActive(true);
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[this.mGainedQuests.IndexOf(button.gameObject)], (QuestParam) null);
      if (quest == null)
        return;
      if (!quest.IsDateUnlock(-1L))
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnDecideClick()
    {
      if (this.mUsedElemPieces.Count > 0)
      {
        string str = (string) null;
        for (int index = 0; index < this.mUsedElemPieces.Count; ++index)
        {
          if (index > 0)
            str += "、";
          str += this.mUsedElemPieces[index].Param.name;
        }
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_ELEMENT_KAKERA"), (object) str), new UIUtility.DialogResultEvent(this.OnKakusei), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      }
      else
        this.OnKakusei((GameObject) null);
    }

    private void OnKakusei(GameObject go)
    {
      if (this.OnAwakeAccept != null)
        this.OnAwakeAccept((int) this.SelectAwakeSlider.value);
      else
        MonoSingleton<GameManager>.Instance.Player.AwakingUnit(this.mCurrentUnit);
    }

    private void OnAdd()
    {
      this.RefreshAwakeLv(1);
    }

    private void OnRemove()
    {
      this.RefreshAwakeLv(-1);
    }

    private void RefreshAwakeLv(int value = 0)
    {
      if (value > 0)
      {
        if (!((UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null) || (double) this.SelectAwakeSlider.maxValue < (double) this.SelectAwakeSlider.value + (double) value)
          return;
        this.SelectAwakeSlider.value += (float) value;
      }
      else
      {
        if (value >= 0 || !((UnityEngine.Object) this.SelectAwakeSlider != (UnityEngine.Object) null) || (double) this.SelectAwakeSlider.value + (double) value < (double) this.SelectAwakeSlider.minValue)
          return;
        this.SelectAwakeSlider.value += (float) value;
      }
    }

    public delegate void KakuseiWindowEvent();

    public delegate void AwakeEvent(int value);
  }
}
