// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "換金", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "退店", FlowNode.PinTypes.Output, 11)]
  public class LimitedShopWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_";
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    [Space(16f)]
    public ImageArray NamePlateImages;

    private void Awake()
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ImgNPC != (UnityEngine.Object) null)
        this.ImgNPC.texture = (Texture) AssetManager.Load<Texture2D>(LimitedShopWindow.ImgPathPrefix + EShopType.Limited.ToString());
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    public void Activated(int pinID)
    {
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() != (UnityEngine.Object) null))
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
