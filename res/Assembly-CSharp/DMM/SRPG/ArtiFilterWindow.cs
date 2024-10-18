// Decompiled with JetBrains decompiler
// Type: SRPG.ArtiFilterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "フィルター更新する", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "キャンセルする", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "全選択", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "全クリア", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(101, "フィルター更新された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "キャンセルされた", FlowNode.PinTypes.Output, 102)]
  public class ArtiFilterWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [Space(5f)]
    [SerializeField]
    private GameObject GoTabParent;
    [SerializeField]
    private ArtiFilterItemTab TemplateTab;
    [Space(5f)]
    [SerializeField]
    private ArtiFilterCategory TemplateCateoryRarity;
    [SerializeField]
    private ArtiFilterCategory TemplateCateoryEquipType;
    [SerializeField]
    private ArtiFilterCategory TemplateCateoryArmsType;
    [Space(5f)]
    [SerializeField]
    private ScrollRect SrController;
    private const int PIN_IN_INIT = 1;
    private const int PIN_IN_UPDATE = 2;
    private const int PIN_IN_CANCEL = 3;
    private const int PIN_IN_ALL_SET = 4;
    private const int PIN_IN_ALL_CLR = 5;
    private const int PIN_OUT_UPDATED = 101;
    private const int PIN_OUT_CANCELED = 102;
    private Dictionary<string, ArtiFilterItemTab> mTabDict = new Dictionary<string, ArtiFilterItemTab>();
    private string mCurrentTabName;
    private Dictionary<string, ArtiFilterCategory> mCategoryDict = new Dictionary<string, ArtiFilterCategory>();
    private IEnumerator mToggleFadeAnimation;

    private void Awake()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void Init()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoTabParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TemplateTab))
      {
        GameUtility.SetGameObjectActive(((Component) this.TemplateTab).gameObject, false);
        GameUtility.DestroyChildGameObjects(this.GoTabParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          ((Component) this.TemplateTab).gameObject
        }));
        this.mTabDict.Clear();
        FilterArtifactParam[] filterArtifactParams = instance.MasterParam.FilterArtifactParams;
        for (int index = 0; index < filterArtifactParams.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          ArtiFilterWindow.\u003CInit\u003Ec__AnonStorey0 initCAnonStorey0 = new ArtiFilterWindow.\u003CInit\u003Ec__AnonStorey0();
          // ISSUE: reference to a compiler-generated field
          initCAnonStorey0.\u0024this = this;
          FilterArtifactParam filterArtifactParam = filterArtifactParams[index];
          if (!this.mTabDict.ContainsKey(filterArtifactParam.TabName))
          {
            // ISSUE: reference to a compiler-generated field
            initCAnonStorey0.item_tab = UnityEngine.Object.Instantiate<ArtiFilterItemTab>(this.TemplateTab, this.GoTabParent.transform, false);
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) initCAnonStorey0.item_tab))
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method pointer
              initCAnonStorey0.item_tab.SetItem(index, filterArtifactParam, new UnityAction<bool>((object) initCAnonStorey0, __methodptr(\u003C\u003Em__0)));
              // ISSUE: reference to a compiler-generated field
              ((Component) initCAnonStorey0.item_tab).gameObject.SetActive(true);
              // ISSUE: reference to a compiler-generated field
              this.mTabDict.Add(filterArtifactParam.TabName, initCAnonStorey0.item_tab);
              if (string.IsNullOrEmpty(this.mCurrentTabName))
                this.mCurrentTabName = filterArtifactParam.TabName;
              // ISSUE: reference to a compiler-generated field
              ((Component) initCAnonStorey0.item_tab).gameObject.SetActive(true);
            }
          }
        }
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TemplateCateoryRarity) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TemplateCateoryEquipType) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TemplateCateoryArmsType))
      {
        GameUtility.SetGameObjectActive(((Component) this.TemplateCateoryRarity).gameObject, false);
        GameUtility.SetGameObjectActive(((Component) this.TemplateCateoryEquipType).gameObject, false);
        GameUtility.SetGameObjectActive(((Component) this.TemplateCateoryArmsType).gameObject, false);
        this.mCategoryDict.Clear();
        int num = 0;
        FilterArtifactParam[] filterArtifactParams = instance.MasterParam.FilterArtifactParams;
        for (int index = 0; index < filterArtifactParams.Length; ++index)
        {
          FilterArtifactParam filter_param = filterArtifactParams[index];
          ArtiFilterCategory artiFilterCategory = (ArtiFilterCategory) null;
          switch (filter_param.FilterType)
          {
            case FilterArtifactParam.eFilterType.Rarity:
              artiFilterCategory = this.TemplateCateoryRarity;
              break;
            case FilterArtifactParam.eFilterType.EquipType:
              artiFilterCategory = this.TemplateCateoryEquipType;
              break;
            case FilterArtifactParam.eFilterType.ArmsType:
              artiFilterCategory = this.TemplateCateoryArmsType;
              break;
          }
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) artiFilterCategory, (UnityEngine.Object) null))
            DebugUtility.LogError(string.Format("FilterArtifact「{0}」にフィルタの条件が1つも設定されていません。何か1つでも設定してください。", (object) filter_param.Iname));
          else if (!this.mCategoryDict.ContainsKey(filter_param.Iname))
          {
            this.mCategoryDict.Add(filter_param.Iname, artiFilterCategory);
            artiFilterCategory.Init(this, index, filter_param);
            GameUtility.SetGameObjectActive(((Component) artiFilterCategory).gameObject, true);
            ((Transform) (((Component) artiFilterCategory).transform as RectTransform)).SetSiblingIndex(num++);
          }
        }
      }
      this.SelectTab(this.mCurrentTabName, true);
      this.LoadPrefs();
      this.UpdateTabState();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(true);
    }

    private void SelectTab(string tab_name, bool is_init = false)
    {
      this.mCurrentTabName = tab_name;
      if (is_init && this.mTabDict.ContainsKey(this.mCurrentTabName))
        this.mTabDict[this.mCurrentTabName].SetToggleSilent(true);
      this.SwitchToggleActiveWithTabName(this.mCurrentTabName);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.SrController))
        return;
      this.SrController.velocity = Vector2.zero;
      this.SrController.normalizedPosition = Vector2.up;
    }

    private void SwitchToggleActiveWithTabName(string tab_name)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return;
      this.StopSwitchVisibleAnimation();
      List<FilterArtifactParam> list = ((IEnumerable<FilterArtifactParam>) instance.MasterParam.FilterArtifactParams).Where<FilterArtifactParam>((Func<FilterArtifactParam, bool>) (param => param.TabName == tab_name)).ToList<FilterArtifactParam>();
      List<CanvasGroup> list_on = new List<CanvasGroup>();
      List<CanvasGroup> list_off = new List<CanvasGroup>();
      foreach (KeyValuePair<string, ArtiFilterCategory> keyValuePair in this.mCategoryDict)
      {
        KeyValuePair<string, ArtiFilterCategory> category = keyValuePair;
        if (list.Find((Predicate<FilterArtifactParam>) (p => p.Iname == category.Key)) != null)
        {
          ((Component) category.Value).gameObject.SetActive(true);
          CanvasGroup component = ((Component) category.Value).GetComponent<CanvasGroup>();
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
            list_on.Add(component);
        }
        else
        {
          ((Component) category.Value).gameObject.SetActive(false);
          CanvasGroup component = ((Component) category.Value).GetComponent<CanvasGroup>();
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
            list_off.Add(component);
        }
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.SrController))
      {
        Scrollbar verticalScrollbar = this.SrController.verticalScrollbar;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) verticalScrollbar))
        {
          CanvasGroup component = ((Component) verticalScrollbar).GetComponent<CanvasGroup>();
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
            list_on.Add(component);
        }
      }
      this.StartSwitchVisibleAnimation(list_on, list_off);
    }

    private void StartSwitchVisibleAnimation(List<CanvasGroup> list_on, List<CanvasGroup> list_off)
    {
      this.mToggleFadeAnimation = UIUtility.SwitchVisibleAsync(list_on, list_off);
      this.StartCoroutine(this.mToggleFadeAnimation);
    }

    private void StopSwitchVisibleAnimation()
    {
      if (this.mToggleFadeAnimation != null)
        this.StartCoroutine(this.mToggleFadeAnimation);
      this.mToggleFadeAnimation = (IEnumerator) null;
    }

    private bool ContainsActiveFilterInTab(string tab_name)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return false;
      foreach (ArtiFilterCategory artiFilterCategory in this.mCategoryDict.Values)
      {
        if (artiFilterCategory.FilterParam.TabName == tab_name)
        {
          foreach (ArtiFilterItemFilter filter in artiFilterCategory.FilterList)
          {
            if (filter.IsOn)
              return true;
          }
        }
      }
      return false;
    }

    private void OnTapTabItem(bool val, ArtiFilterItemTab item)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) item) || !val || !(this.mCurrentTabName != item.Param.TabName))
        return;
      this.SelectTab(item.Param.TabName);
    }

    private void FilterAllSet()
    {
      foreach (KeyValuePair<string, ArtiFilterCategory> keyValuePair in this.mCategoryDict)
      {
        for (int index = 0; index < keyValuePair.Value.FilterList.Count; ++index)
          keyValuePair.Value.FilterList[index].SetToggleSilent(true);
      }
      this.UpdateTabState();
    }

    private void FilterAllClr()
    {
      foreach (KeyValuePair<string, ArtiFilterCategory> keyValuePair in this.mCategoryDict)
      {
        for (int index = 0; index < keyValuePair.Value.FilterList.Count; ++index)
          keyValuePair.Value.FilterList[index].SetToggleSilent(false);
      }
      this.UpdateTabState();
    }

    private void LoadPrefs()
    {
      FilterUtility.FilterPrefs filterPrefs = ArtiFilterWindow.ArtiFilterLoadPrefs();
      foreach (KeyValuePair<string, ArtiFilterCategory> keyValuePair in this.mCategoryDict)
      {
        for (int index = 0; index < keyValuePair.Value.FilterList.Count; ++index)
        {
          ArtiFilterItemFilter filter = keyValuePair.Value.FilterList[index];
          filter.SetToggleSilent(filterPrefs.GetValue(keyValuePair.Value.FilterParam.Iname, filter.Condition.CndsName));
        }
      }
    }

    private void SavePrefs()
    {
      FilterUtility.FilterPrefs filterPrefs = ArtiFilterWindow.ArtiFilterLoadPrefs();
      foreach (KeyValuePair<string, ArtiFilterCategory> keyValuePair in this.mCategoryDict)
      {
        for (int index = 0; index < keyValuePair.Value.FilterList.Count; ++index)
        {
          ArtiFilterItemFilter filter = keyValuePair.Value.FilterList[index];
          filterPrefs.SetValue(keyValuePair.Value.FilterParam.Iname, filter.Condition.CndsName, filter.IsOn);
        }
      }
      FilterUtility.FilterPrefs.Save(filterPrefs, true);
      foreach (KeyValuePair<string, ArtiFilterCategory> keyValuePair in this.mCategoryDict)
      {
        for (int index = 0; index < keyValuePair.Value.FilterList.Count; ++index)
        {
          ArtiFilterItemFilter filter = keyValuePair.Value.FilterList[index];
          filter.SetToggleSilent(filterPrefs.GetValue(keyValuePair.Value.FilterParam.Iname, filter.Condition.CndsName));
        }
      }
    }

    public void UpdateTabState()
    {
      foreach (KeyValuePair<string, ArtiFilterItemTab> keyValuePair in this.mTabDict)
      {
        bool is_filtered = this.ContainsActiveFilterInTab(keyValuePair.Key);
        keyValuePair.Value.SetIsFiltered(is_filtered);
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          break;
        case 2:
          this.SavePrefs();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case 3:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
          break;
        case 4:
          this.FilterAllSet();
          break;
        case 5:
          this.FilterAllClr();
          break;
      }
    }

    public static FilterUtility.FilterPrefs ArtiFilterLoadPrefs()
    {
      FilterUtility.FilterPrefs filterPrefs = FilterUtility.FilterPrefs.Load("FilterArtifact");
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect()))
      {
        DebugUtility.LogError("GameManager が null です。GameManager が生成されてから使用してください。");
        return filterPrefs;
      }
      List<string> all_keys = new List<string>();
      foreach (FilterArtifactParam filterArtifactParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterArtifactParams)
      {
        foreach (FilterArtifactParam.Condition cond in filterArtifactParam.CondList)
        {
          filterPrefs.GetValue(filterArtifactParam.Iname, cond.CndsName);
          all_keys.Add(cond.PrefsKey);
        }
      }
      filterPrefs.RemoveKeys((Predicate<FilterUtility.FilterPrefsData>) (data =>
      {
        string temp_key = data.Key;
        return all_keys.Find((Predicate<string>) (key => key == temp_key)) == null;
      }));
      return filterPrefs;
    }

    public static bool ArtiFilterMatchCondition(
      FilterUtility.FilterPrefs filter,
      ArtifactParam artifact_param,
      ArtifactData artifact_data = null)
    {
      if (artifact_param == null || filter == null)
        return false;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect()))
      {
        DebugUtility.LogError("GameManager が null です。GameManager が生成されてから使用してください。");
        return false;
      }
      foreach (FilterArtifactParam filterArtifactParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterArtifactParams)
      {
        if (!filter.IsDisableFilterAll(filterArtifactParam.Iname))
        {
          bool flag = false;
          for (int index = 0; index < filterArtifactParam.CondList.Count; ++index)
          {
            FilterArtifactParam.Condition cond = filterArtifactParam.CondList[index];
            if (filter.GetValue(filterArtifactParam.Iname, cond.CndsName))
            {
              switch (filterArtifactParam.FilterType)
              {
                case FilterArtifactParam.eFilterType.Rarity:
                  if (artifact_data != null)
                  {
                    flag |= (int) artifact_data.Rarity == cond.Rarity;
                    break;
                  }
                  flag |= (int) artifact_param.Rarity == cond.Rarity;
                  break;
                case FilterArtifactParam.eFilterType.EquipType:
                  flag |= artifact_param.type == cond.EquipType;
                  break;
                case FilterArtifactParam.eFilterType.ArmsType:
                  flag |= Array.Exists<string>(cond.ArmsType, (Predicate<string>) (at => at == artifact_param.tag));
                  break;
              }
              if (flag)
                break;
            }
          }
          if (!flag)
            return false;
        }
      }
      return true;
    }

    public static void ArtiFilterUnitRemove(
      FilterUtility.FilterPrefs filter,
      ref List<ArtifactData> artifact_datas)
    {
      if (filter == null || artifact_datas == null)
        return;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect()))
      {
        DebugUtility.LogError("GameManager が null です。GameManager が生成されてから使用してください。");
      }
      else
      {
        FilterArtifactParam[] filterArtifactParams = MonoSingleton<GameManager>.Instance.MasterParam.FilterArtifactParams;
        for (int index = 0; index < artifact_datas.Count; ++index)
        {
          if (artifact_datas[index] != null && !ArtiFilterWindow.ArtiFilterMatchCondition(filter, artifact_datas[index].ArtifactParam, artifact_datas[index]))
            artifact_datas.RemoveAt(index--);
        }
      }
    }
  }
}
