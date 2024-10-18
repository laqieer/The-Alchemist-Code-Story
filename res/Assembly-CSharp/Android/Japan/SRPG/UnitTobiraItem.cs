// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitTobiraItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject DisableGO;
    [SerializeField]
    private GameObject EnableGO;
    [SerializeField]
    private GameObject[] LevelIconGOList;
    [SerializeField]
    private GameObject SelectedGO;
    [SerializeField]
    private GameObject LockGO;
    [SerializeField]
    private ImageArray TobiraEnableImages;
    [SerializeField]
    private ImageArray TobiraDisableImages;
    [SerializeField]
    private GameObject HilightGo;
    private UnitData mUnit;
    private TobiraParam.Category mCategory;
    private TobiraParam mParam;

    public TobiraParam.Category Category
    {
      get
      {
        return this.mCategory;
      }
    }

    public TobiraParam Param
    {
      get
      {
        return this.mParam;
      }
    }

    public void Initialize(UnitData unit, TobiraParam.Category category)
    {
      this.mUnit = unit;
      this.mCategory = category;
      this.SelectedGO.SetActive(false);
      this.Refresh();
    }

    public void Refresh()
    {
      Button component = this.GetComponent<Button>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null || this.mCategory <= TobiraParam.Category.START || TobiraParam.Category.MAX <= this.mCategory)
        this.gameObject.SetActive(false);
      this.TobiraEnableImages.ImageIndex = (int) (this.mCategory - 1);
      this.TobiraDisableImages.ImageIndex = (int) (this.mCategory - 1);
      this.mParam = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraParam(this.mUnit.UnitID, this.mCategory);
      if (this.mParam == null || !this.mParam.Enable)
      {
        component.interactable = false;
        this.LockGO.SetActive(true);
      }
      else
      {
        TobiraData tobiraData = this.mUnit.TobiraData.Find((Predicate<TobiraData>) (tobira => tobira.Param.TobiraCategory == this.mCategory));
        bool flag = tobiraData != null && tobiraData.IsUnlocked;
        if (!flag)
        {
          bool isEnable = TobiraUtility.IsClearAllToboraConditions(this.mUnit, this.mCategory);
          this.DisableGO.SetActive(true);
          this.SetHilightAnimationEnable(isEnable);
          this.EnableGO.SetActive(false);
        }
        else
        {
          this.DisableGO.SetActive(false);
          this.SetHilightAnimationEnable(false);
          this.EnableGO.SetActive(true);
        }
        if (this.LevelIconGOList == null || this.LevelIconGOList.Length == 0 || !flag)
          return;
        for (int index = 0; index < this.LevelIconGOList.Length; ++index)
        {
          if (!((UnityEngine.Object) this.LevelIconGOList[index] == (UnityEngine.Object) null))
            this.LevelIconGOList[index].SetActive(index < tobiraData.ViewLv);
        }
      }
    }

    public void Select(bool select)
    {
      this.SelectedGO.SetActive(select);
      Animator component = this.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetBool(nameof (select), select);
    }

    private void SetHilightAnimationEnable(bool isEnable)
    {
      if ((UnityEngine.Object) this.HilightGo == (UnityEngine.Object) null)
        return;
      this.HilightGo.SetActive(isEnable);
      Animator component = this.HilightGo.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.enabled = isEnable;
      component.SetBool("hilit", isEnable);
    }
  }
}
