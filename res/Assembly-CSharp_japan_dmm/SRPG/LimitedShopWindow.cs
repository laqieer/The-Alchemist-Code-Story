// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "換金", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "退店", FlowNode.PinTypes.Output, 11)]
  public class LimitedShopWindow : MonoBehaviour, IFlowInterface
  {
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    [Space(16f)]
    public ImageArray NamePlateImages;
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_";

    private void Awake()
    {
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ImgNPC, (Object) null))
        this.ImgNPC.texture = (Texture) AssetManager.Load<Texture2D>(LimitedShopWindow.ImgPathPrefix + EShopType.Limited.ToString());
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    public void Activated(int pinID)
    {
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        return;
      MonoSingleton<GameManager>.GetInstanceDirect().OnSceneChange -= new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    private bool OnGoOutShop()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      return true;
    }
  }
}
