// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidLogItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "上アニメーション開始", FlowNode.PinTypes.Output, 1)]
  public class WorldRaidLogItem : MonoBehaviour, IFlowInterface
  {
    public const int PIN_OUT_MOVE_ITEM = 1;
    [SerializeField]
    private Text UserNameText;
    [SerializeField]
    private BitmapText DamageText;
    [SerializeField]
    private PolyImage BossIcon;
    [SerializeField]
    private CanvasGroup CanvasGroup;
    [SerializeField]
    private float[] AlphaList;
    private WorldRaidLogData mData;
    private int mIndex;
    private Transform mSecondParent;

    public void Activated(int pinID)
    {
    }

    public void SetUp(WorldRaidLogData _data, Transform _second_parent)
    {
      this.mData = _data;
      this.mSecondParent = _second_parent;
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return;
      WorldRaidParam.BossInfo bossInfo = currentWorldRaidParam.BossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == this.mData.BossIname));
      if (bossInfo != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BossIcon, (UnityEngine.Object) null))
      {
        Sprite worldRaidBossIcon = WorldRaidBossManager.GetWorldRaidBossIcon(bossInfo.BossId);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) worldRaidBossIcon, (UnityEngine.Object) null))
          this.BossIcon.sprite = worldRaidBossIcon;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UserNameText, (UnityEngine.Object) null))
        this.UserNameText.text = this.mData.PlayerName;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DamageText, (UnityEngine.Object) null))
        return;
      ((Text) this.DamageText).text = this.mData.Damage.ToString();
    }

    public void ItemMove()
    {
      ++this.mIndex;
      if (this.mIndex == 1 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSecondParent, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) this).gameObject, (UnityEngine.Object) null))
        ((Component) this).gameObject.transform.SetParent(this.mSecondParent);
      if (this.AlphaList.Length > this.mIndex)
        this.CanvasGroup.alpha = this.AlphaList[this.mIndex];
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public void RemoveItem() => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this).gameObject);
  }
}
