// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusReward
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
  public class ScrollClamped_VersusReward : MonoBehaviour, ScrollListSetUp
  {
    public float Space = 1f;
    public bool Arrival = true;
    private List<VersusTowerParam> m_Param = new List<VersusTowerParam>();
    private int m_Max;

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam[] versusTowerParam = instance.GetVersusTowerParam();
      if (versusTowerParam != null)
      {
        for (int index = versusTowerParam.Length - 1; index >= 0; --index)
        {
          if (string.Equals((string) versusTowerParam[index].VersusTowerID, instance.VersusTowerMatchName))
          {
            if (this.Arrival)
            {
              if (string.IsNullOrEmpty((string) versusTowerParam[index].ArrivalIteminame))
                continue;
            }
            else if (versusTowerParam[index].SeasonIteminame == null || versusTowerParam[index].SeasonIteminame.Length == 0)
              continue;
            this.m_Param.Add(versusTowerParam[index]);
          }
        }
        this.m_Max = this.m_Param.Count;
      }
      ScrollListController component1 = this.GetComponent<ScrollListController>();
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this.OnUpdateItems));
      this.GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
      RectTransform component2 = this.GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      Vector2 anchoredPosition = component2.anchoredPosition;
      anchoredPosition.y = 0.0f;
      sizeDelta.y = component1.ItemScale * this.Space * (float) this.m_Max;
      component2.sizeDelta = sizeDelta;
      component2.anchoredPosition = anchoredPosition;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (idx < 0 || idx >= this.m_Max || this.m_Param == null)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        DataSource.Bind<VersusTowerParam>(obj, this.m_Param[idx]);
        if (this.Arrival)
        {
          VersusTowerRewardItem component = obj.GetComponent<VersusTowerRewardItem>();
          if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
            return;
          component.Refresh(VersusTowerRewardItem.REWARD_TYPE.Arrival, 0);
        }
        else
        {
          VersusSeasonRewardInfo component = obj.GetComponent<VersusSeasonRewardInfo>();
          if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
            return;
          component.Refresh();
        }
      }
    }
  }
}
