// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardRewardTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class DrawCardRewardTable : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_PIN_REFRESH = 1;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    public Button PopupButton;
    [SerializeField]
    public float RewardItemScale;
    [SerializeField]
    public float RewardItemRotate;
    private List<DrawCardParam.CardData> mRewardCardList = new List<DrawCardParam.CardData>();
    private List<GameObject> mRewardItems = new List<GameObject>();

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      this.mRewardCardList = new List<DrawCardParam.CardData>();
    }

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      List<DrawCardParam.CardData> rewardDrawCardList = DrawCardParam.RewardDrawCardList;
      Transform parent = this.ItemTemplate.transform.parent;
      if (rewardDrawCardList != null && rewardDrawCardList.Count != this.mRewardCardList.Count)
      {
        for (int index = 0; index < rewardDrawCardList.Count; ++index)
        {
          if (index >= this.mRewardCardList.Count)
          {
            if (!rewardDrawCardList[index].IsMiss)
            {
              GameObject root = Object.Instantiate<GameObject>(this.ItemTemplate, parent);
              root.SetActive(true);
              DataSource.Bind<DrawCardParam.CardData>(root, rewardDrawCardList[index]);
              GameParameter.UpdateAll(root);
              this.mRewardCardList.Add(rewardDrawCardList[index]);
              this.mRewardItems.Add(root);
            }
            else
              break;
          }
        }
      }
      if (Object.op_Inequality((Object) this.PopupButton, (Object) null) && this.mRewardCardList != null && this.mRewardCardList.Count > 0)
        ((Selectable) this.PopupButton).interactable = true;
      else
        ((Selectable) this.PopupButton).interactable = false;
    }

    public GameObject GetLastItemObject()
    {
      return this.mRewardItems != null && this.mRewardItems.Count > 0 ? this.mRewardItems[this.mRewardItems.Count - 1] : (GameObject) null;
    }
  }
}
