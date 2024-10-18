// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardGetItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  public class DrawCardGetItemList : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_PIN_INIT = 1;
    [SerializeField]
    private GameObject mItemIconTemplate;

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Initialize();
    }

    private void Initialize()
    {
      if (Object.op_Equality((Object) this.mItemIconTemplate, (Object) null))
        return;
      this.mItemIconTemplate.SetActive(false);
      List<DrawCardParam.CardData> rewardDrawCardList = DrawCardParam.RewardDrawCardList;
      if (rewardDrawCardList == null || rewardDrawCardList.Count <= 0)
        return;
      Transform parent = this.mItemIconTemplate.transform.parent;
      foreach (DrawCardParam.CardData data in rewardDrawCardList)
      {
        GameObject root = Object.Instantiate<GameObject>(this.mItemIconTemplate, parent);
        root.SetActive(true);
        DataSource.Bind<DrawCardParam.CardData>(root, data);
        GameParameter.UpdateAll(root);
      }
    }
  }
}
