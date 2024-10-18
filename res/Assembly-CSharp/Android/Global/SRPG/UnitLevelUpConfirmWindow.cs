// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLevelUpConfirmWindow
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
  [FlowNode.Pin(0, "Close", FlowNode.PinTypes.Output, 0)]
  public class UnitLevelUpConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mExpItems = new List<GameObject>();
    [SerializeField]
    private RectTransform ItemLayoutParent;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private Button DecideButton;
    public UnitLevelUpConfirmWindow.ConfirmDecideEvent OnDecideEvent;
    private Dictionary<string, int> mSelectItems;

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.DecideButton != (UnityEngine.Object) null))
        return;
      this.DecideButton.onClick.AddListener(new UnityAction(this.OnDecide));
    }

    private void Start()
    {
    }

    public void Refresh(Dictionary<string, int> dict)
    {
      if (dict == null || dict.Count < 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = dict.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          ItemParam itemParam = instance.MasterParam.GetItemParam(current);
          if (itemParam != null && dict[current] > 0)
          {
            ItemData data = new ItemData();
            data.Setup(0L, itemParam, dict[current]);
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
            gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
            DataSource.Bind<ItemData>(gameObject, data);
            this.mExpItems.Add(gameObject);
            gameObject.SetActive(true);
          }
        }
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnDecide()
    {
      if (this.OnDecideEvent != null)
        this.OnDecideEvent();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 0);
    }

    public delegate void ConfirmDecideEvent();
  }
}
