// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactLevelUpWindow
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
  [FlowNode.Pin(0, "Close", FlowNode.PinTypes.Output, 0)]
  public class ArtifactLevelUpWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private RectTransform ListParent;
    [SerializeField]
    private GameObject ListItemTemplate;
    [SerializeField]
    private Text CurrentLevel;
    [SerializeField]
    private Text FinishedLevel;
    [SerializeField]
    private Text MaxLevel;
    [SerializeField]
    private Text NextExp;
    [SerializeField]
    private SliderAnimator LevelGauge;
    [SerializeField]
    private Text GetAllExp;
    [SerializeField]
    private Button DecideBtn;
    [SerializeField]
    private Button CancelBtn;
    [SerializeField]
    private Button MaxBtn;
    [SerializeField]
    private SliderAnimator AddLevelGauge;
    private MasterParam master;
    private PlayerData player;
    private ArtifactData mOriginArtifact;
    private List<GameObject> mItems = new List<GameObject>();
    private List<ArtifactLevelUpListItem> mArtifactLevelupLists = new List<ArtifactLevelUpListItem>();
    private float mExpStart;
    private float mExpEnd;
    private float mExpAnimTime;
    private Dictionary<string, int> mSelectExpItems = new Dictionary<string, int>();
    public ArtifactLevelUpWindow.OnArtifactLevelupEvent OnDecideEvent;
    private List<ItemData> mCacheExpItemList = new List<ItemData>();
    public static readonly string CONFIRM_PATH = "UI/ArtifactLevelUpConfirmWindow";

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItemTemplate, (UnityEngine.Object) null))
        this.ListItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecideBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DecideBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnDecideConfirm)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CancelBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CancelBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MaxBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnMax)));
      }
      this.Init();
    }

    private void Init()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItemTemplate, (UnityEngine.Object) null))
        return;
      this.master = MonoSingleton<GameManager>.Instance.MasterParam;
      if (this.master == null)
        return;
      this.player = MonoSingleton<GameManager>.Instance.Player;
      if (this.player == null)
        return;
      ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
      if (dataOfClass1 != null)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentLevel, (UnityEngine.Object) null))
          this.CurrentLevel.text = dataOfClass1.Lv.ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null))
          this.FinishedLevel.text = this.CurrentLevel.text;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxLevel, (UnityEngine.Object) null))
          this.MaxLevel.text = "/" + dataOfClass1.GetLevelCap().ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
          this.NextExp.text = dataOfClass1.GetNextExp().ToString();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LevelGauge, (UnityEngine.Object) null))
        {
          int totalExpFromLevel = dataOfClass1.GetTotalExpFromLevel((int) dataOfClass1.Lv);
          float num1 = (float) (dataOfClass1.GetTotalExpFromLevel((int) dataOfClass1.Lv + 1) - totalExpFromLevel);
          float num2 = (float) (dataOfClass1.Exp - totalExpFromLevel);
          if ((double) num1 <= 0.0)
            num1 = 1f;
          this.LevelGauge.AnimateValue(Mathf.Clamp01(num2 / num1), 0.0f);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
          this.GetAllExp.text = "0";
        this.mOriginArtifact = dataOfClass1;
      }
      List<string> stringList = new List<string>((IEnumerable<string>) PlayerPrefsUtility.GetString(PlayerPrefsUtility.ARTIFACT_BULK_LEVELUP, string.Empty).Split('|'));
      List<ItemData> itemDataList = this.player.Items;
      ArtifactWindow dataOfClass2 = DataSource.FindDataOfClass<ArtifactWindow>(((Component) this).gameObject, (ArtifactWindow) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) dataOfClass2, (UnityEngine.Object) null))
        itemDataList = dataOfClass2.TmpItems;
      for (int index = 0; index < itemDataList.Count; ++index)
      {
        ItemData data = itemDataList[index];
        if (data != null && data.Param != null && data.Param.type == EItemType.ExpUpArtifact && data.Num > 0)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListItemTemplate);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            DataSource.Bind<ItemData>(gameObject, data);
            gameObject.transform.SetParent((Transform) this.ListParent, false);
            ArtifactLevelUpListItem component = gameObject.GetComponent<ArtifactLevelUpListItem>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              component.OnSelect = new ArtifactLevelUpListItem.SelectExpItem(this.RefreshExpSelectItems);
              component.ChangeUseMax = new ArtifactLevelUpListItem.ChangeToggleEvent(this.RefreshUseMaxItems);
              component.OnCheck = new ArtifactLevelUpListItem.CheckSliderValue(this.OnCheck);
              this.mArtifactLevelupLists.Add(component);
              if (stringList != null && stringList.Count > 0)
                component.SetUseMax(stringList.IndexOf(data.Param.iname) != -1);
              if (component.IsUseMax())
                this.mCacheExpItemList.Add(data);
              gameObject.SetActive(true);
            }
            this.mItems.Add(gameObject);
          }
        }
      }
      if (this.mCacheExpItemList != null && this.mCacheExpItemList.Count > 0)
        this.mCacheExpItemList.Sort((Comparison<ItemData>) ((a, b) => b.Param.value - a.Param.value));
      ((Selectable) this.MaxBtn).interactable = this.mCacheExpItemList != null && this.mCacheExpItemList.Count > 0;
      ((Selectable) this.DecideBtn).interactable = this.mSelectExpItems != null && this.mSelectExpItems.Count > 0;
    }

    private void OnDecideConfirm()
    {
      GameObject gameObject1 = AssetManager.Load<GameObject>(ArtifactLevelUpWindow.CONFIRM_PATH);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      ArtifactLevelUpConfirmWindow componentInChildren = gameObject2.GetComponentInChildren<ArtifactLevelUpConfirmWindow>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.Refresh(this.mSelectExpItems);
      componentInChildren.OnDecideEvent = new ArtifactLevelUpConfirmWindow.ConfirmDecideEvent(this.OnDecide);
    }

    private void OnDecide()
    {
      if (this.OnDecideEvent != null)
        this.OnDecideEvent(this.mSelectExpItems);
      this.Close();
    }

    private void OnCancel()
    {
      if (this.mSelectExpItems.Count <= 0)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        ArtifactLevelUpListItem component = this.mItems[index].GetComponent<ArtifactLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      this.mSelectExpItems.Clear();
      this.RefreshFinishedStatus();
    }

    private void OnMax()
    {
      if (this.mCacheExpItemList == null || this.mCacheExpItemList.Count < 0)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        ArtifactLevelUpListItem component = this.mItems[index].GetComponent<ArtifactLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      this.CalcCanArtifactLevelUpMax();
    }

    private int OnCheck(string iname, int num)
    {
      if (string.IsNullOrEmpty(iname) || num == 0 || this.mSelectExpItems.ContainsKey(iname) && this.mSelectExpItems[iname] > num)
        return -1;
      long num1 = (long) (this.mOriginArtifact.GetGainExpCap() - this.mOriginArtifact.Exp);
      long num2 = 0;
      foreach (string key in this.mSelectExpItems.Keys)
      {
        if (!(key == iname))
        {
          ItemParam itemParam = this.master.GetItemParam(key);
          if (itemParam != null)
            num2 += (long) (itemParam.value * this.mSelectExpItems[key]);
        }
      }
      long num3 = num1 - num2;
      ItemParam itemParam1 = this.master.GetItemParam(iname);
      if (itemParam1 == null || this.player.GetItemAmount(iname) == 0)
        return -1;
      long num4 = (long) (itemParam1.value * num);
      return num3 < num4 ? Mathf.CeilToInt((float) num3 / (float) itemParam1.value) : num;
    }

    private void RefreshExpSelectItems(string iname, int value)
    {
      if (string.IsNullOrEmpty(iname) || this.player.GetItemAmount(iname) == 0)
        return;
      if (!this.mSelectExpItems.ContainsKey(iname))
        this.mSelectExpItems.Add(iname, value);
      else
        this.mSelectExpItems[iname] = value;
      this.RefreshFinishedStatus();
    }

    private void RefreshFinishedStatus()
    {
      if (this.mSelectExpItems == null || this.mSelectExpItems.Count <= 0)
        return;
      int exp1 = 0;
      foreach (string key in this.mSelectExpItems.Keys)
      {
        ItemData itemDataByItemId = this.player.FindItemDataByItemID(key);
        if (itemDataByItemId != null)
        {
          int mSelectExpItem = this.mSelectExpItems[key];
          if (mSelectExpItem != 0 && mSelectExpItem <= itemDataByItemId.Num)
          {
            int num = itemDataByItemId.Param.value * mSelectExpItem;
            exp1 += num;
          }
        }
      }
      int gainExpCap = this.mOriginArtifact.GetGainExpCap();
      int num1 = Math.Min(this.mOriginArtifact.Exp + exp1, gainExpCap);
      ArtifactData artifactData = this.mOriginArtifact.Copy();
      artifactData.GainExp(exp1);
      foreach (ArtifactLevelUpListItem artifactLevelupList in this.mArtifactLevelupLists)
        artifactLevelupList.SetInputLock(num1 < gainExpCap);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null))
      {
        this.FinishedLevel.text = artifactData.Lv.ToString();
        if ((int) artifactData.Lv >= this.mOriginArtifact.GetLevelCap())
          ((Graphic) this.FinishedLevel).color = Color.red;
        else if ((int) artifactData.Lv > (int) this.mOriginArtifact.Lv)
          ((Graphic) this.FinishedLevel).color = Color.green;
        else
          ((Graphic) this.FinishedLevel).color = Color.white;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddLevelGauge, (UnityEngine.Object) null))
      {
        if (num1 == this.mOriginArtifact.Exp || exp1 == 0)
        {
          this.AddLevelGauge.AnimateValue(0.0f, 0.0f);
        }
        else
        {
          int totalExpFromLevel = this.mOriginArtifact.GetTotalExpFromLevel((int) this.mOriginArtifact.Lv);
          float num2 = (float) (this.mOriginArtifact.GetTotalExpFromLevel((int) this.mOriginArtifact.Lv + 1) - totalExpFromLevel);
          float num3 = (float) (num1 - totalExpFromLevel);
          if ((double) num2 <= 0.0)
            num2 = 1f;
          this.AddLevelGauge.AnimateValue(Mathf.Clamp01(num3 / num2), 0.0f);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
      {
        int num4 = 0;
        if (num1 < this.mOriginArtifact.GetGainExpCap())
        {
          int exp2 = artifactData.Exp;
          int nextExp = artifactData.GetNextExp();
          int num5 = num1 - exp2;
          num4 = Math.Max(0, nextExp <= num5 ? 0 : nextExp - num5);
        }
        this.NextExp.text = num4.ToString();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
        this.GetAllExp.text = exp1.ToString();
      ((Selectable) this.DecideBtn).interactable = exp1 > 0;
    }

    private void CalcCanArtifactLevelUpMax()
    {
      if (this.mCacheExpItemList == null)
        return;
      long num1 = 0;
      foreach (ItemData mCacheExpItem in this.mCacheExpItemList)
      {
        int num2 = mCacheExpItem.Param.value * mCacheExpItem.Num;
        num1 += (long) num2;
      }
      long num3 = (long) Mathf.Min((float) (this.mOriginArtifact.GetGainExpCap() - this.mOriginArtifact.Exp), (float) num1);
      this.mSelectExpItems.Clear();
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCacheExpItemList.Count; ++index2)
      {
        if (num3 <= 0L)
        {
          num3 = 0L;
          break;
        }
        ItemData mCacheExpItem1 = this.mCacheExpItemList[index2];
        if (mCacheExpItem1 != null || mCacheExpItem1.Num > 0)
        {
          ItemData mCacheExpItem2 = this.mCacheExpItemList[index1];
          if (index2 == index1 || mCacheExpItem2 != null || mCacheExpItem2.Num > 0)
          {
            if ((long) mCacheExpItem1.Param.value > num3)
            {
              index1 = index2;
            }
            else
            {
              int num4 = (int) (num3 / (long) mCacheExpItem1.Param.value);
              int num5 = Mathf.Min(mCacheExpItem1.Num, num4);
              int num6 = mCacheExpItem1.Param.value * num5;
              num3 -= (long) num6;
              this.mSelectExpItems.Add(mCacheExpItem1.Param.iname, num5);
              index1 = index2;
            }
          }
        }
      }
      if (num3 > 0L)
      {
        ItemData mCacheExpItem3 = this.mCacheExpItemList[index1];
        if (mCacheExpItem3 != null && mCacheExpItem3.Num > 0)
        {
          if (this.mSelectExpItems.ContainsKey(mCacheExpItem3.Param.iname))
          {
            if (mCacheExpItem3.Num - this.mSelectExpItems[mCacheExpItem3.Param.iname] > 0)
            {
              Dictionary<string, int> mSelectExpItems;
              string iname;
              (mSelectExpItems = this.mSelectExpItems)[iname = mCacheExpItem3.Param.iname] = mSelectExpItems[iname] + 1;
            }
            else
            {
              for (int index3 = this.mCacheExpItemList.Count - 2; index3 >= 0; --index3)
              {
                ItemData mCacheExpItem4 = this.mCacheExpItemList[index3];
                bool flag = this.mSelectExpItems.ContainsKey(mCacheExpItem4.ItemID);
                int mSelectExpItem = !flag ? 0 : this.mSelectExpItems[mCacheExpItem4.ItemID];
                if (mCacheExpItem4.Num - (mSelectExpItem + 1) > 0)
                {
                  if (flag)
                    this.mSelectExpItems[mCacheExpItem4.ItemID] = mSelectExpItem + 1;
                  else
                    this.mSelectExpItems.Add(mCacheExpItem4.ItemID, 1);
                  this.mSelectExpItems.Remove(this.mCacheExpItemList[this.mCacheExpItemList.Count - 1].ItemID);
                  break;
                }
              }
            }
          }
          else
            this.mSelectExpItems.Add(mCacheExpItem3.Param.iname, 1);
        }
      }
      if (this.mSelectExpItems.Count > 0)
      {
        for (int index4 = 0; index4 < this.mItems.Count; ++index4)
        {
          GameObject mItem = this.mItems[index4];
          ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(mItem, (ItemData) null);
          if (dataOfClass != null && this.mSelectExpItems.ContainsKey(dataOfClass.Param.iname))
          {
            ArtifactLevelUpListItem component = mItem.GetComponent<ArtifactLevelUpListItem>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.SetUseExpItemSliderValue(this.mSelectExpItems[dataOfClass.Param.iname]);
          }
        }
      }
      this.RefreshFinishedStatus();
    }

    private void RefreshUseMaxItems(string iname, bool is_on)
    {
      if (string.IsNullOrEmpty(iname))
        return;
      ItemData item = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
      if (!is_on)
      {
        if (this.mCacheExpItemList.FindIndex((Predicate<ItemData>) (p => p.ItemID == item.ItemID)) != -1)
          this.mCacheExpItemList.RemoveAt(this.mCacheExpItemList.FindIndex((Predicate<ItemData>) (p => p.ItemID == item.ItemID)));
      }
      else if (this.mCacheExpItemList.Find((Predicate<ItemData>) (p => p.ItemID == item.ItemID)) == null)
        this.mCacheExpItemList.Add(item);
      this.mCacheExpItemList.Sort((Comparison<ItemData>) ((a, b) => b.Param.value - a.Param.value));
      this.SaveSelectUseMax();
      ((Selectable) this.MaxBtn).interactable = this.mCacheExpItemList != null && this.mCacheExpItemList.Count > 0;
    }

    private void SaveSelectUseMax()
    {
      string[] strArray = new string[this.mCacheExpItemList.Count];
      for (int index = 0; index < this.mCacheExpItemList.Count; ++index)
        strArray[index] = this.mCacheExpItemList[index].Param.iname;
      string str = strArray == null ? string.Empty : string.Join("|", strArray);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.ARTIFACT_BULK_LEVELUP, str, true);
    }

    private void Close() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);

    public void Activated(int pinID)
    {
    }

    public delegate void OnArtifactLevelupEvent(Dictionary<string, int> dict);
  }
}
