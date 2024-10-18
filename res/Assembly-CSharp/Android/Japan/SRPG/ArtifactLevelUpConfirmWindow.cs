// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactLevelUpConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Close", FlowNode.PinTypes.Output, 0)]
  public class ArtifactLevelUpConfirmWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mExpItems = new List<GameObject>();
    [SerializeField]
    private RectTransform ItemLayoutParent;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private Button DecideButton;
    public ArtifactLevelUpConfirmWindow.ConfirmDecideEvent OnDecideEvent;
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
      foreach (string key in dict.Keys)
      {
        ItemParam itemParam = instance.MasterParam.GetItemParam(key);
        if (itemParam != null && dict[key] > 0)
        {
          ItemData data = new ItemData();
          data.Setup(0L, itemParam, dict[key]);
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
          gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
          DataSource.Bind<ItemData>(gameObject, data, false);
          this.mExpItems.Add(gameObject);
          gameObject.SetActive(true);
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
