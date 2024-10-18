// Decompiled with JetBrains decompiler
// Type: UnitTobiraCommonItemConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[FlowNode.Pin(100, "強化確認ボタン押下", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(102, "閉じるボタン押下", FlowNode.PinTypes.Input, 102)]
[FlowNode.Pin(101, "閉じる", FlowNode.PinTypes.Output, 101)]
public class UnitTobiraCommonItemConfirmWindow : MonoBehaviour, IFlowInterface
{
  private const int ON_CLICK_TOBIRA_ENHANCE_BUTTON = 100;
  private const int TOBIRA_ENHANCE_CLOSE = 101;
  private const int ON_CLICK_TOBIRA_CLOSE_BUTTON = 102;
  [SerializeField]
  private GameObject mUnitPieceObj;
  [SerializeField]
  private GameObject mSubPieceObj;
  [SerializeField]
  private Text mUnitPieceShortageText;
  [SerializeField]
  private Text mSubPieceNeedText;
  [SerializeField]
  private LText mSubPieceNameText;
  [SerializeField]
  private LText mSubPieceAmmountText;
  private UnitData mCurrentUnit;
  private TobiraData mCurrectTobira;
  private TobiraRecipeParam mCurrentRecipe;
  public UnitTobiraCommonItemConfirmWindow.CallbackEvent OnEnhanceCallback;
  public UnitTobiraCommonItemConfirmWindow.CallbackEvent OnCloseCallback;

  public void Activated(int pinID)
  {
    if (pinID != 100)
    {
      if (pinID != 102)
        return;
      this.OnCloseCallback();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
    else
    {
      this.OnEnhanceCallback();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }

  private void Start()
  {
    if (this.mCurrentUnit != null && this.mCurrectTobira != null && this.mCurrentRecipe != null)
      return;
    ((Component) this).gameObject.SetActive(false);
  }

  public void Initialize(UnitData unit, TobiraData tobira, TobiraRecipeParam recipe)
  {
    this.mCurrentUnit = unit;
    this.mCurrectTobira = tobira;
    this.mCurrentRecipe = recipe;
    this.Refresh();
  }

  private void Refresh()
  {
    ((Component) this).gameObject.SetActive(false);
    if (this.mCurrentUnit == null || this.mCurrectTobira == null || this.mCurrentRecipe == null)
      return;
    this.SetupUnitPieceData();
    this.SetupSubPieceData();
    ((Component) this).gameObject.SetActive(true);
  }

  private void SetupUnitPieceData()
  {
    ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.UnitParam.piece);
    if (itemParam == null)
    {
      DebugUtility.LogError("UnitTobiraCheckSubPiece.cs => GetNeadPieceNum():unitItemParam is Null Reference!");
    }
    else
    {
      DataSource.Bind<ItemParam>(this.mUnitPieceObj, itemParam);
      if (Object.op_Inequality((Object) this.mUnitPieceShortageText, (Object) null))
        this.mUnitPieceShortageText.text = this.GetNeadPieceNum().ToString();
      else
        DebugUtility.LogError("UnitTobiraCheckSubPiece.cs => SetupUnitPieceData():mUnitPieceShortageText is Null Reference!");
    }
  }

  private void SetupSubPieceData()
  {
    GameManager instance = MonoSingleton<GameManager>.Instance;
    ItemParam itemParam = instance.GetItemParam(this.mCurrentUnit.UnitParam.subPiece);
    if (itemParam == null)
    {
      DebugUtility.LogError("UnitTobiraCheckSubPiece.cs => SetupSubPieceData():subItemParam is Null Reference!");
    }
    else
    {
      DataSource.Bind<ItemParam>(this.mSubPieceObj, itemParam);
      int itemAmount = instance.Player.GetItemAmount(this.mCurrentUnit.UnitParam.subPiece);
      int neadPieceNum = this.GetNeadPieceNum();
      if (Object.op_Inequality((Object) this.mSubPieceNeedText, (Object) null))
        this.mSubPieceNeedText.text = neadPieceNum.ToString();
      else
        DebugUtility.LogError("UnitTobiraCheckSubPiece.cs => SetupSubPieceData():mSubPieceNeedText is Null Reference!");
      if (Object.op_Inequality((Object) this.mSubPieceNameText, (Object) null))
        this.mSubPieceNameText.text = LocalizedText.Get("sys.COMMON_EQUIP_NAME", (object) itemParam.name, (object) neadPieceNum);
      else
        DebugUtility.LogError("UnitTobiraCheckSubPiece.cs => SetupSubPieceData():mSubPieceNameText is Null Reference!");
      if (Object.op_Inequality((Object) this.mSubPieceAmmountText, (Object) null))
        this.mSubPieceAmmountText.text = LocalizedText.Get("sys.COMMON_EQUIP_NUM", (object) itemAmount);
      else
        DebugUtility.LogError("UnitTobiraCheckSubPiece.cs => SetupSubPieceData():mSubPieceAmmountText is Null Reference!");
    }
  }

  private int GetNeadPieceNum()
  {
    if (this.mCurrentRecipe == null)
    {
      DebugUtility.LogError("UnitTobiraCheckSubPiece.cs => GetNeadPieceNum():mCurrentRecipe is Null Reference!");
      return -1;
    }
    if (this.mCurrentUnit != null)
      return this.mCurrentRecipe.UnitPieceNum - MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentUnit.UnitParam.piece);
    DebugUtility.LogError("UnitTobiraCheckSubPiece.cs => GetNeadPieceNum():mCurrentUnit is Null Reference!");
    return -1;
  }

  public delegate void CallbackEvent();
}
