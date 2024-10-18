// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_MultiTower
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_MultiTower : MonoBehaviour, ScrollListSetUp
  {
    private readonly float OFFSET = 2f;
    private readonly int MARGIN = 5;
    public float Space = 1f;
    private int mMax;
    public ScrollAutoFit AutoFit;
    public MultiTowerInfo TowerInfo;

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      List<MultiTowerFloorParam> mtAllFloorParam = MonoSingleton<GameManager>.Instance.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
      if (mtAllFloorParam != null)
        this.mMax = mtAllFloorParam.Count;
      this.mMax += this.MARGIN;
      ScrollListController component1 = this.GetComponent<ScrollListController>();
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this.OnUpdateItems));
      this.GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
      RectTransform component2 = this.GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      Vector2 anchoredPosition = component2.anchoredPosition;
      float num1 = component1.ItemScale * this.Space;
      float num2 = num1 - component1.ItemScale;
      anchoredPosition.y = component1.ItemScale * this.OFFSET;
      sizeDelta.y = num1 * (float) (this.mMax - this.MARGIN) - num2;
      component2.sizeDelta = sizeDelta;
      component2.anchoredPosition = anchoredPosition;
      if ((UnityEngine.Object) this.AutoFit != (UnityEngine.Object) null)
        this.AutoFit.ItemScale = component1.ItemScale * this.Space;
      this.TowerInfo.Init();
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (idx < 0 || idx >= this.mMax)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, idx + 1);
        if (mtFloorParam != null)
        {
          DataSource.Bind<MultiTowerFloorParam>(obj, mtFloorParam);
        }
        else
        {
          DataSource component = obj.GetComponent<DataSource>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.Clear();
        }
        MultiTowerFloorInfo component1 = obj.GetComponent<MultiTowerFloorInfo>();
        if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null))
          return;
        component1.Refresh();
      }
    }
  }
}
