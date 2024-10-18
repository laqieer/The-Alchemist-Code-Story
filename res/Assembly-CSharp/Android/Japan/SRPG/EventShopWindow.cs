// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopWindow
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
  public class EventShopWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_Monozuki";
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    public Text TxtHaveCoin;
    [Space(16f)]
    public ImageArray NamePlateImages;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.TxtHaveCoin != (UnityEngine.Object) null))
        return;
      this.TxtHaveCoin.text = LocalizedText.Get("sys.CMD_COIN_LIST");
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ImgNPC != (UnityEngine.Object) null)
        this.ImgNPC.texture = (Texture) AssetManager.Load<Texture2D>(EventShopWindow.ImgPathPrefix);
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
