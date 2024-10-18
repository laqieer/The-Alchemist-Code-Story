// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "換金", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "退店", FlowNode.PinTypes.Output, 11)]
  public class EventShopWindow : MonoBehaviour, IFlowInterface
  {
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    public Text TxtHaveCoin;
    [Space(16f)]
    public ImageArray NamePlateImages;
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_Monozuki";
    private List<EventShopInfo> mEnableEventShopList = new List<EventShopInfo>();
    private static EventShopWindow mInstance;

    public List<EventShopInfo> EnableEventShopList => this.mEnableEventShopList;

    public static EventShopWindow Instance => EventShopWindow.mInstance;

    private void Awake()
    {
      EventShopWindow.mInstance = this;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtHaveCoin, (UnityEngine.Object) null))
        return;
      this.TxtHaveCoin.text = LocalizedText.Get("sys.CMD_COIN_LIST");
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ImgNPC, (UnityEngine.Object) null))
        this.ImgNPC.texture = (Texture) AssetManager.Load<Texture2D>(EventShopWindow.ImgPathPrefix);
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    public void Activated(int pinID)
    {
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
        MonoSingleton<GameManager>.GetInstanceDirect().OnSceneChange -= new GameManager.SceneChangeEvent(this.OnGoOutShop);
      EventShopWindow.mInstance = (EventShopWindow) null;
    }

    private bool OnGoOutShop()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      return true;
    }

    public void SetEnableEventShopList(JSON_ShopListArray.Shops[] shops_array)
    {
      this.mEnableEventShopList.Clear();
      if (shops_array == null)
        return;
      for (int index = 0; index < shops_array.Length; ++index)
      {
        Json_ShopMsgResponse msg = EventShopList.ParseMsg(shops_array[index]);
        if (msg != null && msg.hide == 0)
        {
          EventShopInfo shop_info = new EventShopInfo();
          shop_info.Setup(shops_array[index], msg);
          if (shop_info.shops != null && this.mEnableEventShopList.FindIndex((Predicate<EventShopInfo>) (s => s.shops.id == shop_info.shops.id)) < 0)
            this.mEnableEventShopList.Add(shop_info);
        }
      }
    }
  }
}
