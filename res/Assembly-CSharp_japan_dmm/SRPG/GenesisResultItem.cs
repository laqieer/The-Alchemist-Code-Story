// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisResultItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GenesisResultItem : MonoBehaviour
  {
    [SerializeField]
    private Text TextRewardName;
    [SerializeField]
    private Text TextRewardNum;
    [SerializeField]
    private GameObject TextRewardConn;
    [Space(5f)]
    [SerializeField]
    private GenesisRewardIcon RewardIconTemplate;
    [SerializeField]
    private Transform TrRewardIconParent;
    private int mIndex;
    private GiftData mGiftData;

    public int Index => this.mIndex;

    public GiftData GiftData => this.mGiftData;

    public void SetItem(int index, GiftData gift)
    {
      if (index < 0 || gift == null)
        return;
      this.mIndex = index;
      this.mGiftData = gift;
      string name;
      int amount;
      gift.GetRewardNameAndAmount(out name, out amount);
      if (Object.op_Implicit((Object) this.TextRewardName))
        this.TextRewardName.text = name;
      if (Object.op_Implicit((Object) this.TextRewardNum))
        this.TextRewardNum.text = !gift.CheckGiftTypeIncluded(GiftTypes.Gold) ? amount.ToString() : string.Format("{0:#,0}", (object) amount);
      if (Object.op_Implicit((Object) this.TextRewardConn))
        this.TextRewardConn.SetActive(true);
      if (!Object.op_Implicit((Object) this.RewardIconTemplate))
        return;
      Object.Instantiate<GenesisRewardIcon>(this.RewardIconTemplate, this.TrRewardIconParent).Initialize(gift);
    }
  }
}
