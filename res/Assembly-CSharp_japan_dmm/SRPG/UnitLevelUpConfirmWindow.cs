// Decompiled with JetBrains decompiler
// Type: SRPG.UnitLevelUpConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Close", FlowNode.PinTypes.Output, 0)]
  public class UnitLevelUpConfirmWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private RectTransform ItemLayoutParent;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private Button DecideButton;
    private List<GameObject> mExpItems = new List<GameObject>();
    public UnitLevelUpConfirmWindow.ConfirmDecideEvent OnDecideEvent;
    private Dictionary<string, int> mSelectItems;

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.DecideButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.DecideButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnDecide)));
    }

    private void Start()
    {
    }

    public void Refresh(Dictionary<string, int> dict)
    {
      if (dict == null || dict.Count < 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      foreach (string key in dict.Keys)
      {
        ItemParam itemParam = instance.MasterParam.GetItemParam(key);
        if (itemParam != null && dict[key] > 0)
        {
          ItemData data = new ItemData();
          data.Setup(0L, itemParam, dict[key]);
          GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
          DataSource.Bind<ItemData>(gameObject, data);
          this.mExpItems.Add(gameObject);
          gameObject.SetActive(true);
        }
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
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
