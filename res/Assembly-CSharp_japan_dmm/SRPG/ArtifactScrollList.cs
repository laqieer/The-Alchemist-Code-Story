// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactScrollList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(20, "Clear Selection", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(100, "Selected", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Detail Selected", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "Refresh", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "Update Selection", FlowNode.PinTypes.Input, 201)]
  public class ArtifactScrollList : MonoBehaviour, IFlowInterface, ISortableList
  {
    private const int PIN_ID_CLEAR_SELECTION = 20;
    private const int PIN_ID_SELECTED = 100;
    private const int PIN_ID_DETAIL_SELECTED = 101;
    private const int PIN_ID_REFRESH = 200;
    private const int PIN_ID_UPDATE_SELECTION = 201;
    [SerializeField]
    private GameObject EmptyMessage;
    [SerializeField]
    private ContentController mContentController;
    [SerializeField]
    public GameObject ArtifactDetailRef;
    [SerializeField]
    public GameObject ArtifactDetail;
    [SerializeField]
    public Text NumSelection;
    [SerializeField]
    public int MaxSelection;
    [SerializeField]
    public Button ApplyButton;
    [SerializeField]
    private ArtifactScrollList.ListSource Source;
    [SerializeField]
    private bool AddEmptyItem = true;
    [SerializeField]
    private bool SelectableEquipedItem = true;
    [SerializeField]
    private bool SelectableFavoriteItem = true;
    [SerializeField]
    private Text TotalDecomposeCost;
    [SerializeField]
    private Text TotalSellCost;
    [Space(5f)]
    [SerializeField]
    private ImageArray FilterBgImages;
    [BitMask]
    private ArtifactScrollList.KakeraHideFlags KakeraFlags;
    private bool mAutoSelected;
    private List<ArtifactIconParam> mIconParams = new List<ArtifactIconParam>();
    private ArtifactData mSelectedArtifactData;
    private BaseStatus mTmpVal0;
    private BaseStatus mTmpVal1;
    private int[] mTmpSortVal;
    private string mSortMethod;
    private bool mDescending;
    private bool mShouldRefresh = true;
    private string mFilterPrefsStringCache;
    private string mSortMethodCache;
    private bool mDescendingCache;
    [NonSerialized]
    public ArtifactList.SlotExcludeEquipType ExcludeEquipType;
    [NonSerialized]
    public List<string> ExcludeEquipTypeIname = new List<string>();
    [NonSerialized]
    public bool ExcludeEquiped;
    [NonSerialized]
    public bool KakeraCreateOnly;
    private List<ArtifactData> mSelection = new List<ArtifactData>(4);
    private List<ArtifactParam> mSelectionParams = new List<ArtifactParam>();
    private string[] mFiltersPriority;
    private UnitData mCurrentArtifactOwner;
    private bool mFocusSelection;
    private List<ArtifactParam> mRecommendedArtifacts;
    private Vector2 mAnchorPosition = Vector2.zero;
    public ArtifactScrollList.SelectionChangeEvent OnSelectionChange = (ArtifactScrollList.SelectionChangeEvent) (list => { });

    public object[] Selection
    {
      get
      {
        return this.Source == ArtifactScrollList.ListSource.Kakera ? (object[]) this.mSelectionParams.ToArray() : (object[]) this.mSelection.ToArray();
      }
    }

    public string[] FiltersPriority
    {
      get => this.mFiltersPriority;
      set => this.mFiltersPriority = value;
    }

    public UnitData CurrentOwner
    {
      get => this.mCurrentArtifactOwner;
      set => this.mCurrentArtifactOwner = value;
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EmptyMessage, (UnityEngine.Object) null))
        return;
      this.EmptyMessage.SetActive(false);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 20:
          this.ClearSelection();
          break;
        case 200:
          this.Refresh();
          break;
        case 201:
          this.UpdateSelection();
          break;
      }
    }

    private void UpdateSelection()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NumSelection, (UnityEngine.Object) null))
        this.NumSelection.text = this.mSelection.Count.ToString();
      bool flag = this.MaxSelection != 0 && this.mSelection.Count >= this.MaxSelection;
      if (this.mIconParams == null)
        return;
      foreach (ArtifactIconParam mIconParam in this.mIconParams)
      {
        mIconParam.Enable = this.IsEnableArtifact(mIconParam.Data);
        if (this.mSelection.Contains(mIconParam.Data))
        {
          mIconParam.Select = true;
        }
        else
        {
          mIconParam.Select = false;
          mIconParam.Enable = mIconParam.Enable && !flag;
        }
        mIconParam.Refresh();
      }
      if (this.Source == ArtifactScrollList.ListSource.Normal)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalDecomposeCost, (UnityEngine.Object) null))
        {
          long num = 0;
          for (int index = 0; index < this.mSelection.Count; ++index)
          {
            ArtifactData artifactData = this.mSelection[index];
            if (artifactData != null)
              num += (long) (int) artifactData.RarityParam.ArtifactChangeCost;
          }
          this.TotalDecomposeCost.text = num.ToString();
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalSellCost, (UnityEngine.Object) null))
        {
          long num = 0;
          for (int index = 0; index < this.mSelection.Count; ++index)
          {
            if (this.mSelection[index] != null)
              num += (long) this.mSelection[index].GetSellPrice();
          }
          this.TotalSellCost.text = num.ToString();
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ApplyButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.ApplyButton).interactable = this.mSelection.Count > 0;
    }

    public void ClearSelection()
    {
      this.mSelection.Clear();
      this.mSelectionParams.Clear();
      this.mSelectedArtifactData = (ArtifactData) null;
      this.TriggerSelectionChange();
    }

    private void OnEnable() => this._Refresh();

    public void Refresh() => this.mShouldRefresh = true;

    private void _Refresh()
    {
      string str1 = PlayerPrefsUtility.GetString("FilterArtifact", string.Empty);
      if (str1 == this.mFilterPrefsStringCache && this.mSortMethod == this.mSortMethodCache && this.mDescendingCache == this.mDescending && !this.mShouldRefresh)
        return;
      this.mShouldRefresh = false;
      this.mDescendingCache = this.mDescending;
      this.mFilterPrefsStringCache = str1;
      this.mSortMethodCache = this.mSortMethod;
      FilterUtility.FilterPrefs filter = ArtiFilterWindow.ArtiFilterLoadPrefs();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.FilterBgImages) && this.FilterBgImages.Images != null && this.FilterBgImages.Images.Length > 1)
        this.FilterBgImages.ImageIndex = !filter.IsDisableFilterAll() ? 1 : 0;
      GameManager gm = MonoSingleton<GameManager>.Instance;
      if (this.Source == ArtifactScrollList.ListSource.Kakera)
      {
        List<ArtifactParam> arti_params = gm.MasterParam.Artifacts;
        if (arti_params != null)
        {
          arti_params = arti_params.FindAll((Predicate<ArtifactParam>) (arti_param => arti_param.is_create));
          for (int index = 0; index < arti_params.Count; ++index)
          {
            ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(arti_params[index].kakera);
            if (itemDataByItemId == null || itemDataByItemId.Num <= 0)
              arti_params.RemoveAt(index--);
            else if (this.KakeraFlags != (ArtifactScrollList.KakeraHideFlags) 0 && !ArtifactScrollList.ShouldShowKakera(arti_params[index], gm, this.KakeraFlags) || !ArtiFilterWindow.ArtiFilterMatchCondition(filter, arti_params[index]))
              arti_params.RemoveAt(index--);
            else if (this.KakeraCreateOnly)
            {
              if (!MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactMaxNum(arti_params[index]))
                arti_params.RemoveAt(index--);
              else if ((int) MonoSingleton<GameManager>.Instance.GetRarityParam(arti_params[index].rareini).ArtifactCreatePieceNum > itemDataByItemId.Num)
                arti_params.RemoveAt(index--);
            }
          }
          int length;
          if (!string.IsNullOrEmpty(this.mSortMethod) && (length = this.mSortMethod.IndexOf(':')) > 0)
          {
            string name = this.mSortMethod.Substring(0, length);
            string str2 = this.mSortMethod.Substring(length + 1);
            object[] array = (object[]) arti_params.ToArray();
            MethodInfo method = ((object) this).GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if ((object) method != null)
            {
              try
              {
                method.Invoke((object) this, new object[2]
                {
                  (object) array,
                  (object) str2
                });
                arti_params = new List<ArtifactParam>((IEnumerable<ArtifactParam>) array);
              }
              catch (Exception ex)
              {
                DebugUtility.LogException(ex);
              }
            }
            else
              DebugUtility.LogWarning("Kakera: SortMethod Not Found: " + name);
          }
          if (this.mDescending)
            arti_params.Reverse();
        }
        this.Refresh(arti_params);
      }
      else
      {
        List<ArtifactData> artifact_datas;
        if (this.Source == ArtifactScrollList.ListSource.Skill)
        {
          List<ArtifactData> artifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) gm.Player.Artifacts);
          for (int index = 0; index < artifactDataList.Count; ++index)
          {
            if (!artifactDataList[index].IsInspiration())
              artifactDataList.RemoveAt(index--);
          }
          artifact_datas = artifactDataList;
        }
        else
          artifact_datas = new List<ArtifactData>((IEnumerable<ArtifactData>) gm.Player.Artifacts);
        if (artifact_datas == null)
          return;
        if (this.mCurrentArtifactOwner != null)
        {
          artifact_datas.RemoveAll((Predicate<ArtifactData>) (artifact => !artifact.CheckEnableEquip(this.mCurrentArtifactOwner)));
          if (this.ExcludeEquiped)
            artifact_datas.RemoveAll((Predicate<ArtifactData>) (artifact => gm.Player.FindOwner(artifact, out UnitData _, out JobData _)));
        }
        ArtiFilterWindow.ArtiFilterUnitRemove(filter, ref artifact_datas);
        int length;
        if (!string.IsNullOrEmpty(this.mSortMethod) && (length = this.mSortMethod.IndexOf(':')) > 0)
        {
          string name = this.mSortMethod.Substring(0, length);
          string str3 = this.mSortMethod.Substring(length + 1);
          object[] array = (object[]) artifact_datas.ToArray();
          MethodInfo method = ((object) this).GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
          if ((object) method != null)
          {
            try
            {
              method.Invoke((object) this, new object[2]
              {
                (object) array,
                (object) str3
              });
              artifact_datas = new List<ArtifactData>((IEnumerable<ArtifactData>) array);
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
            }
          }
          else
            DebugUtility.LogWarning("SortMethod Not Found: " + name);
        }
        if (this.mDescending)
          artifact_datas.Reverse();
        if (this.mCurrentArtifactOwner != null && !this.mCurrentArtifactOwner.UnitParam.IsNoRecommended())
        {
          if (this.mRecommendedArtifacts == null)
            this.mRecommendedArtifacts = MonoSingleton<GameManager>.Instance.MasterParam.GetRecommendedArtifactParams(this.mCurrentArtifactOwner, false).GetAll();
          List<ArtifactData> artifactDataList = new List<ArtifactData>();
          List<ArtifactData> collection = new List<ArtifactData>();
          for (int index = 0; index < artifact_datas.Count; ++index)
          {
            ArtifactData artifact_data = artifact_datas[index];
            if (artifact_data != null)
            {
              if (this.RecommendedArtifacts != null && this.RecommendedArtifacts.FindIndex((Predicate<ArtifactParam>) (ap => ap.iname == artifact_data.ArtifactParam.iname)) >= 0)
                artifactDataList.Add(artifact_data);
              else
                collection.Add(artifact_data);
            }
          }
          if (artifactDataList.Count > 0)
          {
            artifactDataList.AddRange((IEnumerable<ArtifactData>) collection);
            artifact_datas = artifactDataList;
          }
        }
        List<ArtifactData> arti_datas = new List<ArtifactData>((IEnumerable<ArtifactData>) artifact_datas);
        if (arti_datas == null)
          return;
        if (this.AddEmptyItem && (arti_datas.Count <= 0 || arti_datas[0] != null))
          arti_datas.Insert(0, (ArtifactData) null);
        this.Refresh(arti_datas);
      }
    }

    public void SetSelection(ArtifactData[] sel, bool invoke, bool focus)
    {
      this.mSelection.Clear();
      for (int index = 0; index < sel.Length; ++index)
      {
        if (sel[index] != null && !this.mSelection.Contains(sel[index]))
          this.mSelection.Add(sel[index]);
      }
      if (this.mSelection != null && this.mSelection.Count > 0)
        this.mSelectedArtifactData = this.mSelection[0];
      this.UpdateSelection();
      if (!invoke)
        return;
      this.TriggerSelectionChange();
    }

    private static bool ShouldShowKakera(
      ArtifactParam artifact,
      GameManager gm,
      ArtifactScrollList.KakeraHideFlags flags)
    {
      RarityParam rarityParam = (RarityParam) null;
      if (artifact == null)
        return false;
      if ((flags & (ArtifactScrollList.KakeraHideFlags.EnoughKakera | ArtifactScrollList.KakeraHideFlags.EnoughGold)) != (ArtifactScrollList.KakeraHideFlags) 0)
        rarityParam = gm.MasterParam.GetRarityParam(artifact.rareini);
      if ((flags & (ArtifactScrollList.KakeraHideFlags.LeastKakera | ArtifactScrollList.KakeraHideFlags.EnoughKakera)) != (ArtifactScrollList.KakeraHideFlags) 0)
      {
        int itemAmount = gm.Player.GetItemAmount(artifact.kakera);
        if ((flags & ArtifactScrollList.KakeraHideFlags.LeastKakera) != (ArtifactScrollList.KakeraHideFlags) 0 && itemAmount < 1 || (flags & ArtifactScrollList.KakeraHideFlags.EnoughKakera) != (ArtifactScrollList.KakeraHideFlags) 0 && itemAmount < (int) rarityParam.ArtifactCreatePieceNum)
          return false;
      }
      return (flags & ArtifactScrollList.KakeraHideFlags.EnoughGold) == (ArtifactScrollList.KakeraHideFlags) 0 || gm.Player.Gold >= (int) rarityParam.ArtifactCreateCost;
    }

    public static bool FilterKakeraArtifacts(ArtifactParam artifact, string[] filter)
    {
      if (filter == null)
        return true;
      int num1 = 0;
      int num2 = 0;
      string s = (string) null;
      for (int index = 0; index < filter.Length; ++index)
      {
        if (ArtifactScrollList.GetValue(filter[index], "RARE:", ref s))
        {
          int result;
          if (int.TryParse(s, out result))
            num1 |= 1 << result;
        }
        else if (ArtifactScrollList.GetValue(filter[index], "TYPE:", ref s))
        {
          try
          {
            ArtifactTypes artifactTypes = (ArtifactTypes) Enum.Parse(typeof (ArtifactTypes), s, true);
            num2 |= 1 << (int) (artifactTypes & (ArtifactTypes) 31);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown element type: " + s));
          }
        }
      }
      bool flag1 = true;
      bool flag2 = true;
      if (num1 != 0 && (1 << artifact.rareini & num1) == 0)
        flag1 = false;
      if (num2 != 0 && (1 << (int) (artifact.type & (ArtifactTypes) 31) & num2) == 0)
        flag2 = false;
      return flag1 && flag2 || filter.Length == 0;
    }

    private void TriggerDetailSelectionChange()
    {
      this.OnSelectionChange(this);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Update()
    {
      if (!this.mShouldRefresh)
        return;
      this._Refresh();
    }

    private List<ArtifactParam> RecommendedArtifacts
    {
      get
      {
        if (this.mRecommendedArtifacts == null && this.mCurrentArtifactOwner != null)
          this.mRecommendedArtifacts = MonoSingleton<GameManager>.Instance.MasterParam.GetRecommendedArtifactParams(this.mCurrentArtifactOwner, false).GetAll();
        return this.mRecommendedArtifacts;
      }
    }

    private void Refresh(List<ArtifactParam> arti_params)
    {
      this.mAnchorPosition = this.mContentController.GetAnchorePos();
      this.mContentController.Release();
      ContentSource source = new ContentSource();
      this.mIconParams.Clear();
      bool flag = true;
      if (arti_params != null)
      {
        for (int index = 0; index < arti_params.Count; ++index)
        {
          ArtifactParam artiParam = arti_params[index];
          ArtifactIconParam artifactIconParam = new ArtifactIconParam();
          if (artiParam != null)
            flag = false;
          artifactIconParam.Param = artiParam;
          artifactIconParam.Enable = true;
          artifactIconParam.Select = false;
          artifactIconParam.IsEmpty = artiParam == null;
          artifactIconParam.IsRecommend = false;
          artifactIconParam.Initialize(source);
          this.mIconParams.Add(artifactIconParam);
        }
      }
      source.SetTable((ContentSource.Param[]) this.mIconParams.ToArray());
      this.mContentController.Initialize(source, this.mAnchorPosition);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EmptyMessage, (UnityEngine.Object) null))
        return;
      this.EmptyMessage.SetActive(flag);
    }

    private void Refresh(List<ArtifactData> arti_datas)
    {
      this.mAnchorPosition = this.mContentController.GetAnchorePos();
      this.mContentController.Release();
      ContentSource source = new ContentSource();
      this.mIconParams.Clear();
      bool flag = true;
      if (arti_datas != null)
      {
        for (int index = 0; index < arti_datas.Count; ++index)
        {
          ArtifactData arti_data = arti_datas[index];
          if (arti_data != null)
            flag = false;
          ArtifactIconParam artifactIconParam = new ArtifactIconParam();
          artifactIconParam.Data = arti_data;
          artifactIconParam.Enable = this.IsEnableArtifact(arti_data);
          artifactIconParam.Select = arti_data != null && (this.mSelection.Contains(arti_data) || this.mSelectedArtifactData != null && (long) this.mSelectedArtifactData.UniqueID == (long) arti_data.UniqueID);
          artifactIconParam.IsEmpty = arti_data == null;
          artifactIconParam.IsRecommend = arti_data != null && this.RecommendedArtifacts != null && this.RecommendedArtifacts.FindIndex((Predicate<ArtifactParam>) (ap => ap.iname == arti_data.ArtifactParam.iname)) >= 0;
          artifactIconParam.Initialize(source);
          this.mIconParams.Add(artifactIconParam);
        }
      }
      source.SetTable((ContentSource.Param[]) this.mIconParams.ToArray());
      this.mContentController.Initialize(source, this.mAnchorPosition);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EmptyMessage, (UnityEngine.Object) null))
        return;
      this.EmptyMessage.SetActive(flag);
    }

    private void TriggerSelectionChange()
    {
      this.UpdateSelection();
      this.OnSelectionChange(this);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void OnItemDetail(ArtifactIconNode node)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) node, (UnityEngine.Object) null) || !(node.GetParam() is ArtifactIconParam artifactIconParam))
        return;
      ArtifactData data = artifactIconParam.Data;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ArtifactDetail, (UnityEngine.Object) null))
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactDetailRef, (UnityEngine.Object) null))
          return;
        this.mSelectedArtifactData = data;
        if (data == null)
          return;
        this.mSelection.Clear();
        this.mSelection.Add(data);
        this.UpdateSelection();
        this.TriggerDetailSelectionChange();
      }
      else
      {
        if (data == null)
          return;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactDetail);
        DataSource.Bind<ArtifactData>(gameObject, data);
        UnitData unit;
        JobData job;
        if (MonoSingleton<GameManager>.GetInstanceDirect().Player.FindOwner(data, out unit, out job))
        {
          DataSource.Bind<UnitData>(gameObject, unit);
          DataSource.Bind<JobData>(gameObject, job);
        }
        gameObject.gameObject.SetActive(true);
      }
    }

    public void ButtonEventOnClickNode(ArtifactIconNode node)
    {
      if (this.mShouldRefresh || UnityEngine.Object.op_Equality((UnityEngine.Object) node, (UnityEngine.Object) null) || !(node.GetParam() is ArtifactIconParam artifactIconParam))
        return;
      if (this.Source == ArtifactScrollList.ListSource.Kakera)
      {
        this.SetSelectedArtifactParam(artifactIconParam.Param);
      }
      else
      {
        this.mSelectedArtifactData = artifactIconParam.Data;
        this.SetSelectedArtifactData(artifactIconParam.Data);
      }
    }

    public void ButtonEventOnSelectNode(ArtifactIconNode node)
    {
      if (this.mShouldRefresh || UnityEngine.Object.op_Equality((UnityEngine.Object) node, (UnityEngine.Object) null) || !(node.GetParam() is ArtifactIconParam artifactIconParam) || artifactIconParam.Data == null)
        return;
      ArtifactData data = artifactIconParam.Data;
      if (this.mSelection.Contains(data))
        this.mSelection.Remove(data);
      else if (this.mSelection.Count < this.MaxSelection)
        this.mSelection.Add(data);
      this.TriggerSelectionChange();
    }

    public void ButtonEventArtifactDetail(ArtifactIconNode node)
    {
      if (this.mShouldRefresh || UnityEngine.Object.op_Equality((UnityEngine.Object) node, (UnityEngine.Object) null))
        return;
      this.mSelectedArtifactData = (node.GetParam() as ArtifactIconParam).Data;
    }

    private void SetSelectedArtifactData(ArtifactData arti_data)
    {
      if (this.mIconParams == null)
        return;
      foreach (ArtifactIconParam mIconParam in this.mIconParams)
      {
        mIconParam.Select = mIconParam.Data != null && arti_data != null && (long) mIconParam.Data.UniqueID == (long) arti_data.UniqueID;
        mIconParam.Refresh();
      }
      this.mSelection.Clear();
      this.mSelection.Add(arti_data);
      this.TriggerSelectionChange();
    }

    private void SetSelectedArtifactParam(ArtifactParam arti_param)
    {
      if (this.mIconParams == null)
        return;
      this.mSelectionParams.Clear();
      this.mSelectionParams.Add(arti_param);
      this.TriggerSelectionChange();
    }

    private int GetIndexOf(ArtifactData artifact)
    {
      return artifact == null || this.mIconParams == null ? -1 : this.mIconParams.FindIndex((Predicate<ArtifactIconParam>) (param => param != null && param.Data != null && (long) param.Data.UniqueID == (long) artifact.UniqueID));
    }

    public bool CheckEndOfIndex(ArtifactData artifact)
    {
      return artifact == null || this.GetIndexOf(artifact) == this.mIconParams.Count - 1;
    }

    public bool CheckStartOfIndex(ArtifactData artifact)
    {
      return artifact == null || this.GetIndexOf(artifact) == 0;
    }

    public bool SelectBack(ArtifactData artifactData)
    {
      this.mAutoSelected = true;
      int indexOf = this.GetIndexOf(artifactData);
      int index = indexOf - 1;
      if (indexOf != -1 && index > -1)
      {
        if (this.mIconParams == null || this.mIconParams.Count <= index)
          return false;
        ArtifactIconParam mIconParam = this.mIconParams[index];
        if (mIconParam == null || mIconParam.Data == null)
          return false;
        this.SetSelection(new ArtifactData[1]
        {
          mIconParam.Data
        }, true, true);
      }
      return indexOf > 0;
    }

    public bool SelectFoward(ArtifactData artifactData)
    {
      this.mAutoSelected = true;
      int indexOf = this.GetIndexOf(artifactData);
      int index = indexOf + 1;
      if (indexOf != -1 && index < this.mIconParams.Count)
      {
        if (this.mIconParams == null || this.mIconParams.Count <= index)
          return false;
        ArtifactIconParam mIconParam = this.mIconParams[index];
        if (mIconParam == null || mIconParam.Data == null)
          return false;
        this.SetSelection(new ArtifactData[1]
        {
          mIconParam.Data
        }, true, true);
      }
      return indexOf < this.mIconParams.Count;
    }

    public bool GetAutoSelected(bool autoReset = false)
    {
      if (!this.mAutoSelected)
        return false;
      if (autoReset)
        this.mAutoSelected = false;
      return true;
    }

    private void SortById(object[] artifacts)
    {
      switch (artifacts)
      {
        case ArtifactData[] _:
          Array.Sort<ArtifactData>((ArtifactData[]) artifacts, (Comparison<ArtifactData>) ((x, y) => string.Compare(x.ArtifactParam.iname, y.ArtifactParam.iname)));
          break;
        case ArtifactParam[] _:
          Array.Sort<ArtifactParam>((ArtifactParam[]) artifacts, (Comparison<ArtifactParam>) ((x, y) => string.Compare(x.iname, y.iname)));
          break;
      }
    }

    private void SortByMember(object[] artifacts, string key)
    {
      PropertyInfo propertyInfo = (PropertyInfo) null;
      if (artifacts is ArtifactParam[])
        propertyInfo = typeof (ArtifactParam).GetProperty(key, BindingFlags.Instance | BindingFlags.Public);
      else if (artifacts is ArtifactData[])
        propertyInfo = typeof (ArtifactData).GetProperty(key, BindingFlags.Instance | BindingFlags.Public);
      if ((object) propertyInfo == null)
      {
        DebugUtility.LogWarning("Property Not Found: " + key);
      }
      else
      {
        this.SortById(artifacts);
        if (this.mTmpSortVal == null || this.mTmpSortVal.Length < artifacts.Length)
          this.mTmpSortVal = new int[artifacts.Length];
        for (int index = 0; index < artifacts.Length; ++index)
        {
          try
          {
            string s = propertyInfo.GetValue(artifacts[index], (object[]) null).ToString();
            this.mTmpSortVal[index] = int.Parse(s);
          }
          catch (Exception ex)
          {
            this.mTmpSortVal[index] = 0;
          }
        }
        this.SortItems(artifacts, this.mTmpSortVal);
      }
    }

    private void SortByStatus(object[] artifacts, string key)
    {
      if (string.IsNullOrEmpty(key))
        return;
      this.SortById(artifacts);
      ParamTypes type;
      try
      {
        type = (ParamTypes) Enum.Parse(typeof (ParamTypes), key);
      }
      catch (Exception ex)
      {
        DebugUtility.LogWarning(ex.Message);
        return;
      }
      if (this.mTmpVal0 == null)
        this.mTmpVal0 = new BaseStatus();
      else
        this.mTmpVal0.Clear();
      if (this.mTmpVal1 == null)
        this.mTmpVal1 = new BaseStatus();
      else
        this.mTmpVal1.Clear();
      if (this.mTmpSortVal == null || this.mTmpSortVal.Length < artifacts.Length)
        this.mTmpSortVal = new int[artifacts.Length];
      for (int index = 0; index < artifacts.Length; ++index)
      {
        if (artifacts[index] is ArtifactData)
        {
          ((ArtifactData) artifacts[index]).GetHomePassiveBuffStatus(ref this.mTmpVal0, ref this.mTmpVal1);
          this.mTmpSortVal[index] = this.mTmpVal0[type];
        }
        else if (artifacts[index] is ArtifactParam)
        {
          ((ArtifactParam) artifacts[index]).GetHomePassiveBuffStatus(ref this.mTmpVal0, ref this.mTmpVal1);
          this.mTmpSortVal[index] = this.mTmpVal0[type];
        }
      }
      this.SortItems(artifacts, this.mTmpSortVal);
    }

    private void SortByType(object[] artifacts, string key)
    {
      if (artifacts is ArtifactData[])
        Array.Sort<ArtifactData>((ArtifactData[]) artifacts, (Comparison<ArtifactData>) ((x, y) => x.CompareByTypeAndID(y)));
      else if (artifacts is ArtifactParam[])
        Array.Sort<ArtifactParam>((ArtifactParam[]) artifacts, (Comparison<ArtifactParam>) ((x, y) => x.CompareByTypeAndID(y)));
      if (this.mTmpSortVal == null || this.mTmpSortVal.Length < artifacts.Length)
        this.mTmpSortVal = new int[artifacts.Length];
      for (int index = 0; index < artifacts.Length; ++index)
      {
        if (artifacts[index] is ArtifactData)
          this.mTmpSortVal[index] = (int) ((ArtifactData) artifacts[index]).ArtifactParam.type;
        if (artifacts[index] is ArtifactParam)
          this.mTmpSortVal[index] = (int) ((ArtifactParam) artifacts[index]).type;
      }
      this.SortItems(artifacts, this.mTmpSortVal);
    }

    private void SortItems(object[] items, int[] values)
    {
      ArtifactScrollList.SortData[] array = new ArtifactScrollList.SortData[items.Length];
      for (int index = 0; index < items.Length; ++index)
        array[index] = new ArtifactScrollList.SortData(items[index], values[index]);
      int result = 0;
      Array.Sort<ArtifactScrollList.SortData>(array, (Comparison<ArtifactScrollList.SortData>) ((x, y) =>
      {
        result = x.mStatusValue - y.mStatusValue;
        if (result != 0)
          return result;
        result = x.artifactParam.CompareByID(y.artifactParam);
        if (result != 0 || !x.isArtifactData || !y.isArtifactData)
          return result;
        result = (int) x.artifactData.Lv - (int) y.artifactData.Lv;
        return result != 0 ? result : x.artifactData.Exp - y.artifactData.Exp;
      }));
      for (int index = 0; index < items.Length; ++index)
      {
        items[index] = array[index].mArtifact;
        values[index] = array[index].mStatusValue;
      }
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      this.mSortMethod = method;
      this.mDescending = !ascending;
      this.Refresh();
    }

    public static List<ArtifactData> FilterArtifacts(List<ArtifactData> artifacts, string[] filter)
    {
      if (filter == null)
        return artifacts;
      int num1 = 0;
      int num2 = 0;
      bool flag1 = false;
      List<string> stringList = new List<string>();
      string s = (string) null;
      for (int index = 0; index < filter.Length; ++index)
      {
        if (ArtifactScrollList.GetValue(filter[index], "RARE:", ref s))
        {
          int result;
          if (int.TryParse(s, out result))
            num1 |= 1 << result;
        }
        else if (ArtifactScrollList.GetValue(filter[index], "TYPE:", ref s))
        {
          try
          {
            ArtifactTypes artifactTypes = (ArtifactTypes) Enum.Parse(typeof (ArtifactTypes), s, true);
            num2 |= 1 << (int) (artifactTypes & (ArtifactTypes) 31);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown element type: " + s));
          }
        }
        else if (ArtifactScrollList.GetValue(filter[index], "FAV:", ref s))
          flag1 = true;
        else if (ArtifactScrollList.GetValue(filter[index], "SAME:", ref s))
          stringList.Add(s);
      }
      List<ArtifactData> artifactDataList = new List<ArtifactData>();
      for (int index = 0; index < artifacts.Count; ++index)
        artifactDataList.Add(artifacts[index]);
      for (int index1 = artifactDataList.Count - 1; index1 >= 0; --index1)
      {
        ArtifactData artifactData = artifactDataList[index1];
        if (flag1 && !artifactData.IsFavorite)
        {
          artifactDataList.RemoveAt(index1);
        }
        else
        {
          bool flag2 = false;
          for (int index2 = 0; index2 < stringList.Count; ++index2)
          {
            if (artifactData.ArtifactParam.iname == stringList[index2])
            {
              flag2 = true;
              break;
            }
          }
          bool flag3 = true;
          bool flag4 = true;
          if (num1 != 0 && (1 << (int) artifactData.Rarity & num1) == 0)
            flag3 = false;
          if (num2 != 0 && (1 << (int) (artifactData.ArtifactParam.type & (ArtifactTypes) 31) & num2) == 0)
            flag4 = false;
          if ((flag2 || !flag3 || !flag4) && filter.Length != 0)
            artifactDataList.RemoveAt(index1);
        }
      }
      return artifactDataList;
    }

    private static bool GetValue(string str, string key, ref string value)
    {
      if (!str.StartsWith(key))
        return false;
      value = str.Substring(key.Length);
      return true;
    }

    private bool IsEnableArtifact(ArtifactData artifact_data)
    {
      bool flag1 = true;
      if (artifact_data == null || this.ExcludeEquipTypeIname == null)
        return flag1;
      if (!this.SelectableEquipedItem && artifact_data.IsEquipped(true) || !this.SelectableFavoriteItem && artifact_data.IsFavorite)
        return false;
      bool flag2;
      switch (this.ExcludeEquipType)
      {
        case ArtifactList.SlotExcludeEquipType.Arms:
          flag2 = artifact_data.ArtifactParam.type != ArtifactTypes.Arms;
          break;
        case ArtifactList.SlotExcludeEquipType.Armor:
          flag2 = artifact_data.ArtifactParam.type != ArtifactTypes.Armor;
          break;
        case ArtifactList.SlotExcludeEquipType.Mix:
          flag2 = artifact_data.ArtifactParam.type != ArtifactTypes.Arms && artifact_data.ArtifactParam.type != ArtifactTypes.Armor;
          break;
        default:
          flag2 = true;
          break;
      }
      for (int index = 0; index < this.ExcludeEquipTypeIname.Count; ++index)
      {
        if (this.ExcludeEquipTypeIname[index] == artifact_data.ArtifactParam.iname)
        {
          flag2 = false;
          break;
        }
      }
      return flag2;
    }

    public enum ListSource
    {
      Normal,
      Kakera,
      Skill,
    }

    [Flags]
    public enum KakeraHideFlags
    {
      LeastKakera = 1,
      EnoughKakera = 2,
      EnoughGold = 4,
    }

    public delegate void SelectionChangeEvent(ArtifactScrollList list);

    private class SortData
    {
      public object mArtifact;
      public int mStatusValue;

      public SortData(object artifact, int statusValue)
      {
        this.mArtifact = artifact;
        this.mStatusValue = statusValue;
        this.isArtifactData = this.mArtifact is ArtifactData;
      }

      public ArtifactParam artifactParam
      {
        get
        {
          return this.isArtifactData ? ((ArtifactData) this.mArtifact).ArtifactParam : (ArtifactParam) this.mArtifact;
        }
      }

      public ArtifactData artifactData
      {
        get => this.isArtifactData ? (ArtifactData) this.mArtifact : (ArtifactData) null;
      }

      public bool isArtifactData { get; private set; }
    }
  }
}
