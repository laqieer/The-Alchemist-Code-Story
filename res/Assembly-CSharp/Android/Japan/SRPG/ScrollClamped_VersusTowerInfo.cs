// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusTowerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_VersusTowerInfo : MonoBehaviour, ScrollListSetUp
  {
    private readonly int MARGIN = 5;
    public float Space = 1f;
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
        for (int index = 0; index < versusTowerParam.Length; ++index)
        {
          if (string.Equals((string) versusTowerParam[index].VersusTowerID, instance.VersusTowerMatchName))
            ++this.m_Max;
        }
        this.m_Max += this.MARGIN;
      }
      ScrollListController component1 = this.GetComponent<ScrollListController>();
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this.OnUpdateItems));
      this.GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
      RectTransform component2 = this.GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      sizeDelta.y = component1.ItemScale * this.Space * (float) this.m_Max;
      component2.sizeDelta = sizeDelta;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (idx < 0 || idx >= this.m_Max)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        VersusTowerFloor component = obj.GetComponent<VersusTowerFloor>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.Refresh(idx, this.m_Max - this.MARGIN);
      }
    }
  }
}
