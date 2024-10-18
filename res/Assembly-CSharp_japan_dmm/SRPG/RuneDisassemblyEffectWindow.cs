// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDisassemblyEffectWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "自身を閉じてルーン一覧へ", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "分解ウィンドウを開く", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1000, "自身を閉じる処理を呼出後", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(200, "成功時", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "大成功時", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "超成功時", FlowNode.PinTypes.Output, 202)]
  public class RuneDisassemblyEffectWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_CLOSE = 10;
    private const int OUTPUT_OPEN_RUNE_DISASSEMBLY = 100;
    private const int OUTPUT_RESULT_SUCCESS = 200;
    private const int OUTPUT_RESULT_GREAT = 201;
    private const int OUTPUT_RESULT_SUPER = 202;
    private const int OUTPUT_CLOSE_WINDOW = 1000;
    [SerializeField]
    private ItemIcon mItemIcon;
    [SerializeField]
    private RuneDrawDisassemblyEffect mRuneDrawDisassemblyEffect;
    [SerializeField]
    private Image[] mImageReplaceFrom;
    [SerializeField]
    private Image[] mImageReplace;
    private RuneManager mRuneManager;
    private ReqRuneDisassembly.Response.Result mResult;

    public void Awake()
    {
    }

    private void OnDestroy()
    {
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      if (Object.op_Implicit((Object) this.mRuneManager))
        this.mRuneManager.OpenDisassembly();
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
    }

    public void Setup(RuneManager manager, ReqRuneDisassembly.Response res)
    {
      if (res == null)
        return;
      this.mRuneManager = manager;
      this.mResult = res.GetResult();
      if (Object.op_Inequality((Object) this.mItemIcon, (Object) null) && res.rewards != null)
      {
        foreach (ReqRuneDisassembly.Response.Rewards reward in res.rewards)
        {
          ItemIcon itemIcon = Object.Instantiate<ItemIcon>(this.mItemIcon);
          ((Component) itemIcon).gameObject.SetActive(true);
          ((Component) itemIcon).transform.SetParent(((Component) this.mItemIcon).transform.parent, false);
          ConsumeItemData data = new ConsumeItemData();
          ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(reward.iname);
          data.param = itemDataByItemId == null ? (ItemParam) null : itemDataByItemId.Param;
          data.num = reward.num;
          DataSource.Bind<ConsumeItemData>(((Component) itemIcon).gameObject, data);
        }
        ((Component) this.mItemIcon).gameObject.SetActive(false);
      }
      if (Object.op_Inequality((Object) this.mRuneDrawDisassemblyEffect, (Object) null))
        this.mRuneDrawDisassemblyEffect.SetDrawParam(this.mResult);
      switch (this.mResult)
      {
        case ReqRuneDisassembly.Response.Result.success:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
          break;
        case ReqRuneDisassembly.Response.Result.great:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
          break;
        case ReqRuneDisassembly.Response.Result.super:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 202);
          break;
      }
      this.Refresh();
    }

    public void Refresh()
    {
      this.ImageReplace();
      if (!Object.op_Inequality((Object) this.mRuneDrawDisassemblyEffect, (Object) null))
        return;
      this.mRuneDrawDisassemblyEffect.Refresh();
    }

    public void ImageReplace()
    {
      if (this.mImageReplace == null || this.mImageReplaceFrom == null)
        return;
      Sprite sprite = (Sprite) null;
      if (this.mResult < (ReqRuneDisassembly.Response.Result) this.mImageReplaceFrom.Length)
        sprite = this.mImageReplaceFrom[(int) this.mResult].sprite;
      if (Object.op_Equality((Object) sprite, (Object) null))
        return;
      foreach (Image image in this.mImageReplace)
        image.sprite = sprite;
    }
  }
}
