// Decompiled with JetBrains decompiler
// Type: SRPG.ShopWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "換金(1度だけ表示)", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "ユニットが選択された", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "換金", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "退店", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "ゲリラショップへ遷移", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "欠片変換ウィンドウへ遷移", FlowNode.PinTypes.Output, 13)]
  public class ShopWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly string ImgPathPrefix = "MenuChar/MenuChar_Shop_";
    private const int PIN_IN_START = 1;
    private const int PIN_IN_SELL = 2;
    private const int PIN_IN_UNIT_SELECTED = 3;
    private const int PIN_OUT_SELL = 10;
    private const int PIN_OUT_EXIT = 11;
    private const int PIN_OUT_GOTO_GUERRILLA = 12;
    private const int PIN_OUT_GOTO_PIECE_EXCHANGE = 13;
    public RawImage ImgBackGround;
    public RawImage ImgNPC;
    public EShopType[] NpcRandArray;
    public ShopWindow.ChangeButton[] ChangeButtons;
    [Space(16f)]
    public ImageArray NamePlateImages;
    public GameObject GuerrillaShopBanner;
    private bool alreadyShowExchange;
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
      if ((UnityEngine.Object) this.ImgNPC != (UnityEngine.Object) null)
      {
        EShopType npcRand = this.NpcRandArray[Random.Range(0, this.NpcRandArray.Length)];
        this.ImgNPC.texture = (Texture) AssetManager.Load<Texture2D>(ShopWindow.ImgPathPrefix + (object) npcRand);
      }
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          bool flag = MonoSingleton<GameManager>.Instance.Player.IsGuerrillaShopOpen();
          if ((UnityEngine.Object) this.GuerrillaShopBanner != (UnityEngine.Object) null)
            this.GuerrillaShopBanner.SetActive(flag);
          if (flag)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
            break;
          }
          this.ShowExchangeDialog();
          break;
        case 2:
          this.ShowExchangeDialog();
          break;
        case 3:
          this.ShowExchangePieceDialog();
          break;
      }
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() != (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.GetInstanceDirect().OnSceneChange -= new GameManager.SceneChangeEvent(this.OnGoOutShop);
    }

    private void ShowExchangeDialog()
    {
      if (this.alreadyShowExchange || !MonoSingleton<GameManager>.Instance.Player.CheckEnableConvertGold())
        return;
      this.alreadyShowExchange = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private bool OnGoOutShop()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      return true;
    }

    private void ShowExchangePieceDialog()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      UnitData dataSource = currentValue.GetDataSource<UnitData>("_self");
      GlobalVars.SelectedUnitUniqueID.Set(dataSource.UniqueID);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
    }

    [Serializable]
    public class ChangeButton
    {
      public EShopType shopType;
      public Button button;
    }
  }
}
