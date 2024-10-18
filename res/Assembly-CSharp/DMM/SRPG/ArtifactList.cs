// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(20, "Clear Selection", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(100, "Selected", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Detail Selected", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "Refresh", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "Update Selection", FlowNode.PinTypes.Input, 201)]
  public class ArtifactList : UIBehaviour, IFlowInterface, ISortableList
  {
    private const int PIN_ID_CLEAR_SELECTION = 20;
    private const int PIN_ID_SELECTED = 100;
    private const int PIN_ID_DETAIL_SELECTED = 101;
    private const int PIN_ID_REFRESH = 200;
    private const int PIN_ID_UPDATE_SELECTION = 201;
    public ArtifactList.ListSource Source;
    [BitMask]
    public ArtifactList.KakeraHideFlags KakeraFlags;
    public GameObject ListItem;
    private int mPage;
    private int mMaxPages;
    private int mPageSize;
    private bool mStarted;
    private bool mShouldRefresh;
    private bool mInvokeSelChange;
    public Scrollbar PageScrollBar;
    public Text PageIndex;
    public Text PageIndexMax;
    public Button ForwardButton;
    public Button BackButton;
    public Button ApplyButton;
    public bool UseGridLayout = true;
    public float CellWidth = 100f;
    public float CellHeight = 100f;
    public float PaddingX;
    public float PaddingY;
    public float SpacingX;
    public float SpacingY;
    public bool TruncateX;
    public bool TruncateY = true;
    public string SelectionState;
    public int Item_Normal;
    public int Item_Selected = 1;
    public int Item_Disabled = 2;
    public Text NumSelection;
    public int MaxSelection;
    public bool ShowSelection;
    public bool OnlyEquippable;
    public UnitData TestOwner;
    public GameObject[] ExtraItems = new GameObject[0];
    public Text TotalDecomposeCost;
    public GameObject ArtifactDetail;
    public GameObject ArtifactDetailRef;
    public GameObject EmptyMessage;
    public Text TotalSellCost;
    public bool ExcludeEquiped;
    public bool KakeraCreateOnly;
    public ArtifactList.SlotExcludeEquipType ExcludeEquipType;
    public List<string> ExcludeEquipTypeIname = new List<string>();
    private List<GameObject> mItems = new List<GameObject>(32);
    private System.Type mDataType;
    private bool mFocusSelection;
    private object[] mData;
    private BaseStatus mTmpVal0;
    private BaseStatus mTmpVal1;
    private int[] mTmpSortVal;
    private string mSortMethod;
    private bool mDescending;
    private bool mEmptyMessageChanged;
    private bool mAutoSelected;
    private List<object> mSelection = new List<object>(4);
    public Scrollbar AbilityScrollBar;
    public ArtifactList.SelectionChangeEvent OnSelectionChange = (ArtifactList.SelectionChangeEvent) (list => { });
    public int MaxCellCount = 64;
    private string[] mFiltersPriority;
    private string[] mFilters;

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

    public object[] Selection => this.mSelection.ToArray();

    public bool HasSelection => this.mSelection.Count > 0;

    public int CellCount
    {
      get
      {
        GridLayoutGroup component;
        float num1;
        float num2;
        float num3;
        float num4;
        float num5;
        float num6;
        if (this.UseGridLayout && UnityEngine.Object.op_Inequality((UnityEngine.Object) (component = ((Component) this).GetComponent<GridLayoutGroup>()), (UnityEngine.Object) null))
        {
          num1 = component.cellSize.x;
          num2 = component.cellSize.y;
          num3 = component.spacing.x;
          num4 = component.spacing.y;
          num5 = (float) ((LayoutGroup) component).padding.horizontal;
          num6 = (float) ((LayoutGroup) component).padding.vertical;
        }
        else
        {
          num1 = this.CellWidth;
          num2 = this.CellHeight;
          num3 = this.SpacingX;
          num4 = this.SpacingY;
          num5 = this.PaddingX;
          num6 = this.PaddingY;
        }
        Rect rect = ((RectTransform) ((Component) this).transform).rect;
        float num7 = ((Rect) ref rect).width - num5 + num3;
        if (!this.TruncateX)
          num7 += (float) ((double) num1 + (double) num4 - 1.0);
        float num8 = ((Rect) ref rect).height - num6 + num4;
        if (!this.TruncateY)
          num8 += (float) ((double) num2 + (double) num3 - 1.0);
        return Mathf.Clamp(Mathf.FloorToInt(num7 / (num1 + num3)) * Mathf.FloorToInt(num8 / (num2 + num4)), 0, this.MaxCellCount);
      }
    }

    public void GotoPreviousPage()
    {
      if (this.mPage <= 0)
        return;
      --this.mPage;
      this._Refresh();
    }

    public void GotoNextPage()
    {
      if (this.mPage >= this.mMaxPages - 1)
        return;
      ++this.mPage;
      this._Refresh();
    }

    protected virtual void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) && this.ListItem.activeInHierarchy)
        this.ListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EmptyMessage, (UnityEngine.Object) null) && !this.mEmptyMessageChanged)
      {
        this.mEmptyMessageChanged = true;
        this.EmptyMessage.SetActive(false);
      }
      this.mStarted = true;
    }

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.mShouldRefresh = true;
    }

    private void LateUpdate()
    {
      if (!this.mShouldRefresh)
        return;
      this.mShouldRefresh = false;
      this._Refresh();
    }

    public string[] FiltersPriority
    {
      get => this.mFiltersPriority;
      set => this.mFiltersPriority = value;
    }

    public void Refresh() => this.mShouldRefresh = true;

    private bool IsCreatableArtifact(List<ArtifactData> artifactDataList)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || instanceDirect.Player == null)
        return false;
      List<ItemData> all = instanceDirect.Player.Items.FindAll((Predicate<ItemData>) (i => i.ItemType == EItemType.ArtifactPiece));
      foreach (ArtifactData artifactData in artifactDataList)
      {
        ArtifactParam artifactParam = artifactData.ArtifactParam;
        ItemData itemData = all.Find((Predicate<ItemData>) (piece => piece.Param.iname == artifactParam.iname));
        if (itemData != null)
        {
          RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini);
          if (itemData.Num >= (int) rarityParam.ArtifactCreatePieceNum)
            return true;
        }
      }
      return false;
    }

    private List<GenericBadge<ArtifactData>> AddCreatableInfo(List<ArtifactData> artifactDataList)
    {
      List<GenericBadge<ArtifactData>> genericBadgeList = new List<GenericBadge<ArtifactData>>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || instanceDirect.Player == null)
        return genericBadgeList;
      List<ItemData> all = instanceDirect.Player.Items.FindAll((Predicate<ItemData>) (i => i.ItemType == EItemType.ArtifactPiece));
      foreach (ArtifactData artifactData in artifactDataList)
      {
        ArtifactParam artifactParam = artifactData.ArtifactParam;
        ItemData itemData = all.Find((Predicate<ItemData>) (piece => piece.Param.iname == artifactParam.iname));
        if (itemData == null)
        {
          genericBadgeList.Add(new GenericBadge<ArtifactData>(artifactData));
        }
        else
        {
          RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParam.rareini);
          bool flag = itemData.Num >= (int) rarityParam.ArtifactCreatePieceNum;
          genericBadgeList.Add(new GenericBadge<ArtifactData>(artifactData, flag));
        }
      }
      return genericBadgeList;
    }

    private void _Refresh()
    {
      this.mDataType = this.Source != ArtifactList.ListSource.Kakera ? typeof (ArtifactData) : typeof (ArtifactParam);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
        return;
      this.mPageSize = this.CellCount;
      Transform transform = ((Component) this).transform;
      while (this.mItems.Count < this.mPageSize)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ListItem);
        gameObject.transform.SetParent(transform, false);
        this.mItems.Add(gameObject);
        ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        }
      }
      if (this.mItems.Count == 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      object[] objArray = (object[]) null;
      if (this.Source == ArtifactList.ListSource.Normal)
      {
        List<ArtifactData> artifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) instance.Player.Artifacts);
        if (this.OnlyEquippable && this.TestOwner != null)
        {
          for (int index = 0; index < artifactDataList.Count; ++index)
          {
            if (!artifactDataList[index].CheckEnableEquip(this.TestOwner))
              artifactDataList.RemoveAt(index--);
          }
        }
        if (this.ExcludeEquiped && this.TestOwner != null)
        {
          for (int index = 0; index < artifactDataList.Count; ++index)
          {
            if (instance.Player.FindOwner(artifactDataList[index], out UnitData _, out JobData _))
              artifactDataList.RemoveAt(index--);
          }
        }
        objArray = (object[]) artifactDataList.ToArray();
      }
      else if (this.Source == ArtifactList.ListSource.Kakera)
      {
        List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
        if (instance.MasterParam.Artifacts != null)
        {
          ArtifactParam[] array = instance.MasterParam.Artifacts.ToArray();
          for (int index = 0; index < array.Length; ++index)
          {
            if (array[index].is_create)
              artifactParamList.Add(array[index]);
          }
        }
        string[] filter = this.mFiltersPriority == null || this.mFiltersPriority.Length <= 0 ? this.mFilters : this.mFiltersPriority;
        for (int index = 0; index < artifactParamList.Count; ++index)
        {
          if (this.KakeraFlags != (ArtifactList.KakeraHideFlags) 0 && !ArtifactList.ShouldShowKakera(artifactParamList[index], instance, this.KakeraFlags) || !ArtifactList.FilterKakeraArtifacts(artifactParamList[index], filter))
            artifactParamList.RemoveAt(index--);
          else if (this.KakeraCreateOnly)
          {
            if (!MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactMaxNum(artifactParamList[index]))
              artifactParamList.RemoveAt(index--);
            else if ((int) MonoSingleton<GameManager>.Instance.GetRarityParam(artifactParamList[index].rareini).ArtifactCreatePieceNum > MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(artifactParamList[index].kakera).Num)
              artifactParamList.RemoveAt(index--);
          }
        }
        objArray = (object[]) artifactParamList.ToArray();
      }
      else if (this.Source == ArtifactList.ListSource.Skill)
      {
        List<ArtifactData> artifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) instance.Player.Artifacts);
        for (int index = 0; index < artifactDataList.Count; ++index)
        {
          if (!artifactDataList[index].IsInspiration())
            artifactDataList.RemoveAt(index--);
        }
        objArray = (object[]) artifactDataList.ToArray();
      }
      if (this.Source != ArtifactList.ListSource.Kakera)
        objArray = this.mFiltersPriority == null || this.mFiltersPriority.Length <= 0 ? ArtifactList.FilterArtifacts(objArray, this.mFilters) : ArtifactList.FilterArtifacts(objArray, this.mFiltersPriority);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EmptyMessage, (UnityEngine.Object) null))
      {
        this.mEmptyMessageChanged = true;
        this.EmptyMessage.SetActive(objArray == null || objArray.Length == 0);
      }
      if (objArray == null)
        return;
      int length;
      if (!string.IsNullOrEmpty(this.mSortMethod) && (length = this.mSortMethod.IndexOf(':')) > 0)
      {
        string name = this.mSortMethod.Substring(0, length);
        string str = this.mSortMethod.Substring(length + 1);
        MethodInfo method = ((object) this).GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if ((object) method != null)
        {
          try
          {
            method.Invoke((object) this, new object[2]
            {
              (object) objArray,
              (object) str
            });
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
        Array.Reverse((Array) objArray);
      RecommendedArtifactViewParam[] artifactViewParamArray = new RecommendedArtifactViewParam[objArray.Length];
      if (this.Source == ArtifactList.ListSource.Normal && this.TestOwner != null && !this.TestOwner.UnitParam.IsNoRecommended())
      {
        List<ArtifactParam> all = instance.MasterParam.GetRecommendedArtifactParams(this.TestOwner, false).GetAll();
        List<ArtifactData> source = new List<ArtifactData>();
        List<ArtifactData> collection = new List<ArtifactData>();
        for (int index = 0; index < objArray.Length; ++index)
        {
          ArtifactData artifact_data = objArray[index] as ArtifactData;
          if (artifact_data != null)
          {
            if (all.FindIndex((Predicate<ArtifactParam>) (ap => ap.iname == artifact_data.ArtifactParam.iname)) >= 0)
              source.Add(artifact_data);
            else
              collection.Add(artifact_data);
          }
        }
        if (source.Count > 0)
        {
          for (int index = 0; index < artifactViewParamArray.Length; ++index)
            artifactViewParamArray[index] = index >= source.Count ? new RecommendedArtifactViewParam(false) : new RecommendedArtifactViewParam(true);
          source.AddRange((IEnumerable<ArtifactData>) collection);
          objArray = source.Select<ArtifactData, object>((Func<ArtifactData, object>) (ad => (object) ad)).ToArray<object>();
        }
      }
      this.mData = new object[objArray.Length];
      Array.Copy((Array) objArray, (Array) this.mData, objArray.Length);
      if (this.mPageSize > 0)
      {
        this.mMaxPages = (objArray.Length + this.ExtraItems.Length + this.mPageSize - 1) / this.mPageSize;
        this.mPage = Mathf.Max(0, Mathf.Min(this.mPage, this.mMaxPages - 1));
      }
      if (this.mFocusSelection)
      {
        this.mFocusSelection = false;
        if (this.mSelection != null && this.mSelection.Count > 0)
        {
          int num = Array.IndexOf<object>(this.mData, this.mSelection[0]) + this.ExtraItems.Length;
          if (num >= 0)
            this.mPage = num / this.mPageSize;
        }
      }
      for (int index1 = 0; index1 < this.mItems.Count; ++index1)
      {
        int index2 = this.mPage * this.mPageSize + index1 - this.ExtraItems.Length;
        if (0 <= index2 && index2 < this.mData.Length)
        {
          DataSource.Bind(this.mItems[index1], this.mDataType, this.mData[index2]);
          DataSource.Bind<RecommendedArtifactViewParam>(this.mItems[index1], artifactViewParamArray[index2]);
          this.mItems[index1].SetActive(index1 < this.mPageSize);
          ArtifactData artifactData = this.mData[index2] as ArtifactData;
          ArtifactIcon componentInChildren = this.mItems[index1].GetComponentInChildren<ArtifactIcon>(true);
          if (this.Source == ArtifactList.ListSource.Normal && this.MaxSelection <= 0)
          {
            switch (this.ExcludeEquipType)
            {
              case ArtifactList.SlotExcludeEquipType.Arms:
                componentInChildren.EquipForceMask = artifactData.ArtifactParam.type == ArtifactTypes.Arms;
                break;
              case ArtifactList.SlotExcludeEquipType.Armor:
                componentInChildren.EquipForceMask = artifactData.ArtifactParam.type == ArtifactTypes.Armor;
                break;
              case ArtifactList.SlotExcludeEquipType.Mix:
                componentInChildren.EquipForceMask = artifactData.ArtifactParam.type == ArtifactTypes.Arms || artifactData.ArtifactParam.type == ArtifactTypes.Armor;
                break;
              default:
                componentInChildren.EquipForceMask = false;
                break;
            }
            for (int index3 = 0; index3 < this.ExcludeEquipTypeIname.Count; ++index3)
            {
              if (this.ExcludeEquipTypeIname[index3] == artifactData.ArtifactParam.iname)
              {
                componentInChildren.EquipForceMask = true;
                break;
              }
            }
          }
          GameParameter.UpdateAll(this.mItems[index1]);
        }
        else
          this.mItems[index1].SetActive(false);
      }
      for (int index = 0; index < this.ExtraItems.Length; ++index)
      {
        int num = this.mPage * this.mPageSize + index;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExtraItems[index], (UnityEngine.Object) null))
          this.ExtraItems[index].SetActive(0 <= num && num < this.ExtraItems.Length);
      }
      this.UpdateSelection();
      this.UpdatePage();
      if (!this.mInvokeSelChange)
        return;
      this.mInvokeSelChange = false;
      this.TriggerSelectionChange();
    }

    public static object[] FilterArtifacts(object[] artifacts, string[] filter)
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
        if (ArtifactList.GetValue(filter[index], "RARE:", ref s))
        {
          int result;
          if (int.TryParse(s, out result))
            num1 |= 1 << result;
        }
        else if (ArtifactList.GetValue(filter[index], "TYPE:", ref s))
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
        else if (ArtifactList.GetValue(filter[index], "FAV:", ref s))
          flag1 = true;
        else if (ArtifactList.GetValue(filter[index], "SAME:", ref s))
          stringList.Add(s);
      }
      List<ArtifactData> artifactDataList = new List<ArtifactData>();
      for (int index = 0; index < artifacts.Length; ++index)
        artifactDataList.Add(artifacts[index] as ArtifactData);
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
      return artifactDataList.ConvertAll<object>((Converter<ArtifactData, object>) (o => (object) o)).ToArray();
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
        if (ArtifactList.GetValue(filter[index], "RARE:", ref s))
        {
          int result;
          if (int.TryParse(s, out result))
            num1 |= 1 << result;
        }
        else if (ArtifactList.GetValue(filter[index], "TYPE:", ref s))
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

    private static bool GetValue(string str, string key, ref string value)
    {
      if (!str.StartsWith(key))
        return false;
      value = str.Substring(key.Length);
      return true;
    }

    private static bool ShouldShowKakera(
      ArtifactParam artifact,
      GameManager gm,
      ArtifactList.KakeraHideFlags flags)
    {
      RarityParam rarityParam = (RarityParam) null;
      if (artifact == null)
        return false;
      if ((flags & (ArtifactList.KakeraHideFlags.EnoughKakera | ArtifactList.KakeraHideFlags.EnoughGold)) != (ArtifactList.KakeraHideFlags) 0)
        rarityParam = gm.MasterParam.GetRarityParam(artifact.rareini);
      if ((flags & (ArtifactList.KakeraHideFlags.LeastKakera | ArtifactList.KakeraHideFlags.EnoughKakera)) != (ArtifactList.KakeraHideFlags) 0)
      {
        int itemAmount = gm.Player.GetItemAmount(artifact.kakera);
        if ((flags & ArtifactList.KakeraHideFlags.LeastKakera) != (ArtifactList.KakeraHideFlags) 0 && itemAmount < 1 || (flags & ArtifactList.KakeraHideFlags.EnoughKakera) != (ArtifactList.KakeraHideFlags) 0 && itemAmount < (int) rarityParam.ArtifactCreatePieceNum)
          return false;
      }
      return (flags & ArtifactList.KakeraHideFlags.EnoughGold) == (ArtifactList.KakeraHideFlags) 0 || gm.Player.Gold >= (int) rarityParam.ArtifactCreateCost;
    }

    public void UpdateSelection()
    {
      if (this.mData == null || !this.ShowSelection)
        return;
      for (int index = this.mSelection.Count - 1; index >= 0; --index)
      {
        object obj = this.mSelection[index];
        if (Array.IndexOf<object>(this.mData, obj) < 0)
        {
          this.mSelection.RemoveAt(index);
          this.mInvokeSelChange = true;
        }
        else if (obj is ArtifactData && this.MaxSelection > 0 && (obj as ArtifactData).IsFavorite)
        {
          this.mSelection.RemoveAt(index);
          this.mInvokeSelChange = true;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NumSelection, (UnityEngine.Object) null))
        this.NumSelection.text = this.mSelection.Count.ToString();
      List<CheckBadge> checkBadgeList = new List<CheckBadge>();
      for (int index1 = 0; index1 < this.mItems.Count; ++index1)
      {
        int index2 = this.mPage * this.mPageSize + index1 - this.ExtraItems.Length;
        if (0 <= index2 && index2 < this.mData.Length)
        {
          int num = this.mSelection.IndexOf(this.mData[index2]);
          Animator component = this.mItems[index1].GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !string.IsNullOrEmpty(this.SelectionState))
          {
            if (num >= 0)
              component.SetInteger(this.SelectionState, this.Item_Selected);
            else
              component.SetInteger(this.SelectionState, this.Item_Normal);
          }
          this.mItems[index1].GetComponentInChildren<ArtifactIcon>(true).ForceMask = this.mSelection.Count >= this.MaxSelection && num < 0;
          GameParameter.UpdateAll(this.mItems[index1]);
          this.mItems[index1].GetComponentsInChildren<CheckBadge>(true, checkBadgeList);
          for (int index3 = 0; index3 < checkBadgeList.Count; ++index3)
          {
            if (num >= 0)
              ((Component) checkBadgeList[index3]).gameObject.SetActive(true);
            else
              ((Component) checkBadgeList[index3]).gameObject.SetActive(false);
          }
        }
        else
          this.mItems[index1].SetActive(false);
      }
      if (this.Source == ArtifactList.ListSource.Normal)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalDecomposeCost, (UnityEngine.Object) null))
        {
          long num = 0;
          for (int index = 0; index < this.mSelection.Count; ++index)
          {
            if (this.mSelection[index] is ArtifactData artifactData)
              num += (long) (int) artifactData.RarityParam.ArtifactChangeCost;
          }
          this.TotalDecomposeCost.text = num.ToString();
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalSellCost, (UnityEngine.Object) null))
        {
          long num = 0;
          for (int index = 0; index < this.mSelection.Count; ++index)
          {
            if (this.mSelection[index] is ArtifactData)
              num += (long) ((ArtifactData) this.mSelection[index]).GetSellPrice();
            else if (this.mSelection[index] is ArtifactParam)
              num += (long) ((ArtifactParam) this.mSelection[index]).sell;
          }
          this.TotalSellCost.text = num.ToString();
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ApplyButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.ApplyButton).interactable = this.mSelection.Count > 0;
    }

    public void SetSelection(object[] sel, bool invoke, bool focus)
    {
      this.mFocusSelection = focus;
      this.Refresh();
      this.mSelection.Clear();
      for (int index = 0; index < sel.Length; ++index)
      {
        if (sel[index] != null && !this.mSelection.Contains(sel[index]))
          this.mSelection.Add(sel[index]);
      }
      if (!this.mStarted)
      {
        this.mInvokeSelChange = this.mInvokeSelChange || invoke;
      }
      else
      {
        this.UpdateSelection();
        if (!invoke)
          return;
        this.TriggerSelectionChange();
      }
    }

    public void UpdatePage()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageScrollBar, (UnityEngine.Object) null))
      {
        if (this.mMaxPages >= 2)
        {
          this.PageScrollBar.size = 1f / (float) this.mMaxPages;
          this.PageScrollBar.value = (float) this.mPage / ((float) this.mMaxPages - 1f);
        }
        else
        {
          this.PageScrollBar.size = 1f;
          this.PageScrollBar.value = 0.0f;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageIndex, (UnityEngine.Object) null))
        this.PageIndex.text = Mathf.Min(this.mPage + 1, this.mMaxPages).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PageIndexMax, (UnityEngine.Object) null))
        this.PageIndexMax.text = this.mMaxPages.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ForwardButton, (UnityEngine.Object) null))
        ((Selectable) this.ForwardButton).interactable = this.mPage < this.mMaxPages - 1;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.BackButton).interactable = this.mPage > 0;
    }

    private void OnItemDetail(GameObject go)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ArtifactDetail, (UnityEngine.Object) null))
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactDetailRef, (UnityEngine.Object) null))
          return;
        object dataOfClass = DataSource.FindDataOfClass(go, this.mDataType, (object) null);
        if (dataOfClass == null)
          return;
        this.mSelection.Clear();
        this.mSelection.Add(dataOfClass);
        this.UpdateSelection();
        this.TriggerDetailSelectionChange();
      }
      else
      {
        object dataOfClass = DataSource.FindDataOfClass(go, this.mDataType, (object) null);
        if (dataOfClass == null)
          return;
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactDetail);
        DataSource.Bind(gameObject, this.mDataType, dataOfClass);
        if (dataOfClass is ArtifactData)
        {
          ArtifactData arti = dataOfClass as ArtifactData;
          UnitData unit;
          JobData job;
          if (MonoSingleton<GameManager>.GetInstanceDirect().Player.FindOwner(arti, out unit, out job))
          {
            DataSource.Bind<UnitData>(gameObject, unit);
            DataSource.Bind<JobData>(gameObject, job);
          }
        }
        gameObject.gameObject.SetActive(true);
      }
    }

    private void OnItemSelect(GameObject go)
    {
      if (this.mShouldRefresh)
        return;
      object dataOfClass = DataSource.FindDataOfClass(go, this.mDataType, (object) null);
      if (dataOfClass == null)
        return;
      this.UpdateSelection(dataOfClass);
    }

    public void UpdateSelection(object selection)
    {
      if (this.MaxSelection > 0)
      {
        if (selection is ArtifactData)
        {
          ArtifactData artifactData = selection as ArtifactData;
          if (artifactData.IsFavorite || artifactData.CheckEquiped())
            return;
        }
        if (this.mSelection.Contains(selection))
          this.mSelection.Remove(selection);
        else if (this.mSelection.Count < this.MaxSelection)
          this.mSelection.Add(selection);
        this.UpdateSelection();
        this.TriggerSelectionChange();
      }
      else
      {
        if (selection is ArtifactData)
        {
          ArtifactData artifactData = selection as ArtifactData;
          for (int index = 0; index < this.ExcludeEquipTypeIname.Count; ++index)
          {
            if (this.ExcludeEquipTypeIname[index] == artifactData.ArtifactParam.iname)
              return;
          }
          if (this.ExcludeEquipType == ArtifactList.SlotExcludeEquipType.Mix && (artifactData.ArtifactParam.type == ArtifactTypes.Arms || artifactData.ArtifactParam.type == ArtifactTypes.Armor) || this.ExcludeEquipType == ArtifactList.SlotExcludeEquipType.Arms && artifactData.ArtifactParam.type == ArtifactTypes.Arms || this.ExcludeEquipType == ArtifactList.SlotExcludeEquipType.Armor && artifactData.ArtifactParam.type == ArtifactTypes.Armor)
            return;
        }
        this.mSelection.Clear();
        this.mSelection.Add(selection);
        this.UpdateSelection();
        this.TriggerSelectionChange();
      }
    }

    public void ClearSelection()
    {
      this.mSelection.Clear();
      this.UpdateSelection();
      this.TriggerSelectionChange();
    }

    private void TriggerSelectionChange()
    {
      this.OnSelectionChange(this);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AbilityScrollBar, (UnityEngine.Object) null))
        this.AbilityScrollBar.value = 1f;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void TriggerDetailSelectionChange()
    {
      this.OnSelectionChange(this);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
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
      PropertyInfo propertyInfo = !(artifacts is ArtifactParam[]) ? typeof (ArtifactData).GetProperty(key, BindingFlags.Instance | BindingFlags.Public) : typeof (ArtifactParam).GetProperty(key, BindingFlags.Instance | BindingFlags.Public);
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
      ArtifactList.SortData[] array = new ArtifactList.SortData[items.Length];
      for (int index = 0; index < items.Length; ++index)
        array[index] = new ArtifactList.SortData(items[index], values[index]);
      int result = 0;
      Array.Sort<ArtifactList.SortData>(array, (Comparison<ArtifactList.SortData>) ((x, y) =>
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
      this.mFilters = filters;
      this.Refresh();
    }

    private int GetIndexOf(ArtifactData artifact)
    {
      return artifact == null || (object) this.mDataType != (object) typeof (ArtifactData) ? -1 : Array.FindIndex<object>(this.mData, (Predicate<object>) (a => (long) ((ArtifactData) a).UniqueID == (long) artifact.UniqueID));
    }

    public bool CheckEndOfIndex(ArtifactData artifact)
    {
      return artifact == null || (object) this.mDataType != (object) typeof (ArtifactData) || this.GetIndexOf(artifact) == this.mData.Length - 1;
    }

    public bool CheckStartOfIndex(ArtifactData artifact)
    {
      return artifact == null || (object) this.mDataType != (object) typeof (ArtifactData) || this.GetIndexOf(artifact) == 0;
    }

    public bool SelectBack(ArtifactData artifactData)
    {
      if ((object) this.mDataType != (object) typeof (ArtifactData))
        return false;
      this.mAutoSelected = true;
      int indexOf = this.GetIndexOf(artifactData);
      int index = indexOf - 1;
      if (indexOf != -1 && index > -1)
        this.SetSelection(new object[1]{ this.mData[index] }, true, true);
      return indexOf > 0;
    }

    public bool SelectFoward(ArtifactData artifactData)
    {
      if ((object) this.mDataType != (object) typeof (ArtifactData))
        return false;
      this.mAutoSelected = true;
      int indexOf = this.GetIndexOf(artifactData);
      int index = indexOf + 1;
      if (indexOf != -1 && index < this.mData.Length)
        this.SetSelection(new object[1]{ this.mData[index] }, true, true);
      return indexOf < this.mData.Length;
    }

    public bool GetAutoSelected(bool autoReset = false)
    {
      if (!this.mAutoSelected)
        return false;
      if (autoReset)
        this.mAutoSelected = false;
      return true;
    }

    public enum SlotExcludeEquipType
    {
      Non,
      Arms,
      Armor,
      Mix,
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

    public delegate void SelectionChangeEvent(ArtifactList list);

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
