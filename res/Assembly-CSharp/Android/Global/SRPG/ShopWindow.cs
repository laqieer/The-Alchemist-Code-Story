// Decompiled with JetBrains decompiler
// Type: SRPG.ShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "退店", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "換金", FlowNode.PinTypes.Output, 10)]
  public class ShopWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_";
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    public EShopType[] NpcRandArray;
    public ShopWindow.ChangeButton[] ChangeButtons;
    [Space(16f)]
    public ImageArray NamePlateImages;
    public LevelLock[] ShopLevelLock;

    private void Awake()
    {
      List<EShopType> eshopTypeList = new List<EShopType>();
      for (int index = 0; index < this.ShopLevelLock.Length; ++index)
      {
        if (MonoSingleton<GameManager>.Instance.Player.CheckUnlock(this.ShopLevelLock[index].Condition))
        {
          switch (this.ShopLevelLock[index].Condition)
          {
            case UnlockTargets.Shop:
              eshopTypeList.Add(EShopType.Normal);
              continue;
            case UnlockTargets.ShopTabi:
              eshopTypeList.Add(EShopType.Tabi);
              continue;
            case UnlockTargets.ShopKimagure:
              eshopTypeList.Add(EShopType.Kimagure);
              continue;
            default:
              continue;
          }
        }
      }
      this.NpcRandArray = eshopTypeList.ToArray();
    }

    private void Start()
    {
      if (MonoSingleton<GameManager>.Instance.Player.CheckEnableConvertGold())
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      if ((UnityEngine.Object) this.ImgNPC != (UnityEngine.Object) null)
      {
        EShopType npcRand = this.NpcRandArray[UnityEngine.Random.Range(0, this.NpcRandArray.Length)];
        this.ImgNPC.texture = (Texture) AssetManager.Load<Texture2D>(ShopWindow.ImgPathPrefix + (object) npcRand);
      }
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

    [Serializable]
    public class ChangeButton
    {
      public EShopType shopType;
      public Button button;
    }
  }
}
