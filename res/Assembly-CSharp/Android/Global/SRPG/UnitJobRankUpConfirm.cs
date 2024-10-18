// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobRankUpConfirm
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitJobRankUpConfirm : MonoBehaviour
  {
    public Text AllEquipConfirm;
    public GameObject RankMaxEquipAttention;
    public Text CostText;
    public Transform ListTransform;
    public GameObject ListItem;
    public Transform CommonListTransform;
    public GameObject CommonListItem;
    public SRPG_Button YesButton;
    public Text NoGoldWarningText;
    public UnitJobRankUpConfirm.OnAccept OnAllEquipAccept;
    public UnitJobRankUpConfirm.AllInAccept OnAllInAccept;
    private int target_rank;
    private bool can_jobmaster;
    private bool can_jobmax;
    public UnitJobRankUpConfirm.SetFlag SetCommonFlag;
    private UnitData mCurrentUnit;
    private NeedEquipItemList NeedEquipList;
    public Scrollbar Scroll;
    private bool IsSoul;
    public RectTransform ListRectTranceform;
    public ScrollRect ScrollParent;
    private float DecelerationRate;
    public GameObject JobInfo;
    public Text TargetJobLv;
    public Text MaxJobLv;

    public bool IsAllIn { get; set; }

    private void Start()
    {
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null || (UnityEngine.Object) this.ListTransform == (UnityEngine.Object) null)
        return;
      this.ListItem.SetActive(false);
      if ((UnityEngine.Object) this.CostText != (UnityEngine.Object) null)
        this.CostText.text = "0";
      this.mCurrentUnit = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (this.mCurrentUnit == null)
        return;
      bool flag1 = this.mCurrentUnit.CurrentJob.Rank == 0;
      if ((UnityEngine.Object) this.RankMaxEquipAttention != (UnityEngine.Object) null)
        this.RankMaxEquipAttention.SetActive(!flag1);
      if ((UnityEngine.Object) this.JobInfo != (UnityEngine.Object) null)
      {
        this.JobInfo.SetActive(!flag1);
        DataSource.Bind<JobData>(this.JobInfo, this.mCurrentUnit.CurrentJob);
      }
      if ((UnityEngine.Object) this.AllEquipConfirm != (UnityEngine.Object) null)
        this.AllEquipConfirm.text = this.mCurrentUnit.JobIndex < this.mCurrentUnit.NumJobsAvailable ? (!flag1 ? LocalizedText.Get("sys.UNIT_ALLEQUIP_CONFIRM") : LocalizedText.Get("sys.UNIT_ALLEQUIP_UNLOCK_CONFIRM")) : LocalizedText.Get("sys.UNIT_ALLEQUIP_CHANGE_CONFIRM");
      int cost = 0;
      Dictionary<string, int> equips = new Dictionary<string, int>();
      Dictionary<string, int> consumes = new Dictionary<string, int>();
      this.NeedEquipList = new NeedEquipItemList();
      if (!this.mCurrentUnit.CurrentJob.CanAllEquip(ref cost, ref equips, ref consumes, ref this.target_rank, ref this.can_jobmaster, ref this.can_jobmax, this.NeedEquipList, this.IsAllIn))
        return;
      this.target_rank = Mathf.Min(this.mCurrentUnit.GetJobRankCap(), Mathf.Max(this.target_rank, this.mCurrentUnit.CurrentJob.Rank + 1));
      if (!this.IsAllIn)
        this.can_jobmaster = this.mCurrentUnit.GetJobRankCap() == JobParam.MAX_JOB_RANK && this.target_rank == this.mCurrentUnit.GetJobRankCap() && this.mCurrentUnit.CurrentJob.Rank == this.mCurrentUnit.GetJobRankCap();
      this.SetCommonFlag(this.NeedEquipList.IsEnoughCommon());
      List<ItemParam> items = MonoSingleton<GameManager>.Instance.MasterParam.Items;
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = equips.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string key = enumerator.Current;
          ItemParam itemParam = items.Find((Predicate<ItemParam>) (eq => eq.iname == key));
          if (itemParam != null)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListItem);
            gameObject.gameObject.SetActive(true);
            gameObject.transform.SetParent(this.ListTransform, false);
            ItemData itemData = this.CreateItemData(itemParam.iname, equips[key]);
            DataSource.Bind<ItemData>(gameObject, itemData);
          }
        }
      }
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = consumes.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string key = enumerator.Current;
          ItemParam itemParam = items.Find((Predicate<ItemParam>) (eq => eq.iname == key));
          if (itemParam != null)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListItem);
            gameObject.gameObject.SetActive(true);
            gameObject.transform.SetParent(this.ListTransform, false);
            ItemData itemData = this.CreateItemData(itemParam.iname, consumes[key]);
            DataSource.Bind<ItemData>(gameObject, itemData);
          }
        }
      }
      if (this.NeedEquipList.IsEnoughCommon())
      {
        using (Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator enumerator = this.NeedEquipList.CommonNeedNum.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            NeedEquipItemDictionary equipItemDictionary = this.NeedEquipList.CommonNeedNum[enumerator.Current];
            ItemParam commonItemParam = equipItemDictionary.CommonItemParam;
            if (commonItemParam != null)
            {
              bool flag2 = true;
              for (int index = 0; index < equipItemDictionary.list.Count; ++index)
              {
                ItemParam itemParam = equipItemDictionary.list[index].Param;
                if (itemParam != null && (int) itemParam.cmn_type - 1 != 2)
                {
                  flag2 = false;
                  GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CommonListItem);
                  gameObject.gameObject.SetActive(true);
                  gameObject.transform.SetParent(this.CommonListTransform, false);
                  ItemData data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam.iname) ?? this.CreateItemData(itemParam.iname, 0);
                  ItemData cmmon_data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(commonItemParam.iname) ?? this.CreateItemData(commonItemParam.iname, 0);
                  gameObject.GetComponent<CommonConvertItem>().Bind(data, cmmon_data, equipItemDictionary.list[index].NeedPiece);
                }
              }
              if (flag2)
              {
                this.IsSoul = true;
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListItem);
                gameObject.gameObject.SetActive(true);
                gameObject.transform.SetParent(this.ListTransform, false);
                ItemData itemData = this.CreateItemData(commonItemParam.iname, equipItemDictionary.list.Count);
                DataSource.Bind<ItemData>(gameObject, itemData);
              }
            }
          }
        }
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      bool flag3 = cost > instance.Player.Gold;
      if ((UnityEngine.Object) this.YesButton != (UnityEngine.Object) null)
        this.YesButton.interactable = !flag3;
      if ((UnityEngine.Object) this.NoGoldWarningText != (UnityEngine.Object) null)
        this.NoGoldWarningText.gameObject.SetActive(flag3);
      if ((UnityEngine.Object) this.CostText != (UnityEngine.Object) null)
        this.CostText.text = cost.ToString();
      if ((UnityEngine.Object) this.MaxJobLv != (UnityEngine.Object) null)
        this.MaxJobLv.text = "/" + this.mCurrentUnit.GetJobRankCap().ToString();
      if ((UnityEngine.Object) this.TargetJobLv != (UnityEngine.Object) null)
        this.TargetJobLv.text = this.target_rank.ToString();
      if ((UnityEngine.Object) this.ScrollParent != (UnityEngine.Object) null)
      {
        this.DecelerationRate = this.ScrollParent.decelerationRate;
        this.ScrollParent.decelerationRate = 0.0f;
      }
      this.ListRectTranceform.anchoredPosition = new Vector2(this.ListRectTranceform.anchoredPosition.x, 0.0f);
      this.StartCoroutine(this.ScrollInit());
    }

    [DebuggerHidden]
    public IEnumerator ScrollInit()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitJobRankUpConfirm.\u003CScrollInit\u003Ec__Iterator145() { \u003C\u003Ef__this = this };
    }

    public ItemData CreateItemData(string iname, int num)
    {
      Json_Item json = new Json_Item();
      json.iname = iname;
      json.num = num;
      ItemData itemData = new ItemData();
      itemData.Deserialize(json);
      return itemData;
    }

    public void OnAllAccept()
    {
      if (this.OnAllEquipAccept == null)
        return;
      if (this.mCurrentUnit.JobIndex >= this.mCurrentUnit.NumJobsAvailable)
      {
        JobData baseJob = this.mCurrentUnit.GetBaseJob(this.mCurrentUnit.CurrentJob.JobID);
        if (Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, baseJob) < 0)
          return;
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.CONFIRM_CLASSCHANGE"), baseJob == null ? (object) string.Empty : (object) baseJob.Name, (object) this.mCurrentUnit.CurrentJob.Name), (UIUtility.DialogResultEvent) (go => this.OnNeedEquip()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      }
      else
        this.OnNeedEquip();
    }

    public void OnNeedEquip()
    {
      if (this.NeedEquipList.IsEnoughCommon())
        UIUtility.ConfirmBox(LocalizedText.Get(!this.IsSoul ? "sys.COMMON_EQUIP_CHECK" : "sys.COMMON_EQUIP_CHECK_SOUL", new object[1]
        {
          (object) this.NeedEquipList.GetCommonItemListString()
        }), (UIUtility.DialogResultEvent) (go => this.OnAllEquipAccept(this.target_rank, this.can_jobmaster, this.can_jobmax)), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
      else
        this.OnAllEquipAccept(this.target_rank, this.can_jobmaster, this.can_jobmax);
    }

    public delegate void OnAccept(int target_rank, bool can_jobmaster, bool can_jobmax);

    public delegate void AllInAccept(int current_rank, int target_rank);

    public delegate void SetFlag(bool flag);
  }
}
