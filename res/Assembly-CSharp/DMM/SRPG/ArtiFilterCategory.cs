// Decompiled with JetBrains decompiler
// Type: SRPG.ArtiFilterCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArtiFilterCategory : MonoBehaviour
  {
    [SerializeField]
    private Text TextCategory;
    [SerializeField]
    private GameObject GoTabParent;
    [SerializeField]
    private ArtiFilterItemFilter TemplateFilter;
    [SerializeField]
    private GameObject GoHeader;
    private ArtiFilterWindow mParent;
    private int mIndex;
    private FilterArtifactParam mFilterParam;
    private List<ArtiFilterItemFilter> mFilterList = new List<ArtiFilterItemFilter>();

    public int Index => this.mIndex;

    public FilterArtifactParam FilterParam => this.mFilterParam;

    public List<ArtiFilterItemFilter> FilterList => this.mFilterList;

    private void Start()
    {
      GameUtility.SetGameObjectActive(((Component) this.TemplateFilter).gameObject, false);
    }

    public void Init(ArtiFilterWindow parent, int index, FilterArtifactParam filter_param)
    {
      if (filter_param == null)
        return;
      this.mParent = parent;
      this.mIndex = index;
      this.mFilterParam = filter_param;
      if (!Object.op_Implicit((Object) this.TemplateFilter))
        return;
      this.mFilterList.Clear();
      GameUtility.DestroyChildGameObjects(this.GoTabParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[2]
      {
        ((Component) this.TemplateFilter).gameObject,
        this.GoHeader
      }));
      if (Object.op_Implicit((Object) this.TextCategory))
        this.TextCategory.text = filter_param.Name;
      for (int index1 = 0; index1 < filter_param.CondList.Count; ++index1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ArtiFilterCategory.\u003CInit\u003Ec__AnonStorey0 initCAnonStorey0 = new ArtiFilterCategory.\u003CInit\u003Ec__AnonStorey0();
        // ISSUE: reference to a compiler-generated field
        initCAnonStorey0.\u0024this = this;
        FilterArtifactParam.Condition cond = filter_param.CondList[index1];
        // ISSUE: reference to a compiler-generated field
        initCAnonStorey0.item_filter = Object.Instantiate<ArtiFilterItemFilter>(this.TemplateFilter, this.GoTabParent.transform, false);
        // ISSUE: reference to a compiler-generated field
        if (Object.op_Implicit((Object) initCAnonStorey0.item_filter))
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          initCAnonStorey0.item_filter.SetItem(index1, cond, new UnityAction<bool>((object) initCAnonStorey0, __methodptr(\u003C\u003Em__0)));
          // ISSUE: reference to a compiler-generated field
          ((Component) initCAnonStorey0.item_filter).gameObject.SetActive(true);
          // ISSUE: reference to a compiler-generated field
          this.mFilterList.Add(initCAnonStorey0.item_filter);
        }
      }
    }

    private void OnTapFilterItem(bool val, ArtiFilterItemFilter item)
    {
      if (!Object.op_Implicit((Object) item) || !Object.op_Implicit((Object) this.mParent))
        return;
      this.mParent.UpdateTabState();
    }
  }
}
