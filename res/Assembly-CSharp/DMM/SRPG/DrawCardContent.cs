// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Shuffle", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(21, "Ready", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(31, "Decide", FlowNode.PinTypes.Input, 31)]
  [FlowNode.Pin(41, "Draw", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(51, "Continue", FlowNode.PinTypes.Input, 51)]
  [FlowNode.Pin(101, "Initialized", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "Shuffled", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(121, "Choice", FlowNode.PinTypes.Output, 121)]
  [FlowNode.Pin(131, "Decided", FlowNode.PinTypes.Output, 131)]
  [FlowNode.Pin(141, "Win", FlowNode.PinTypes.Output, 141)]
  [FlowNode.Pin(151, "Next", FlowNode.PinTypes.Output, 151)]
  [FlowNode.Pin(161, "End", FlowNode.PinTypes.Output, 161)]
  [FlowNode.Pin(171, "Continued", FlowNode.PinTypes.Output, 171)]
  public class DrawCardContent : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_PIN_INITIALIZE = 1;
    private const int INPUT_PIN_SHUFFLE = 11;
    private const int INPUT_PIN_READY = 21;
    private const int INPUT_PIN_DECIDE = 31;
    private const int INPUT_PIN_DRAW = 41;
    private const int INPUT_PIN_CONTINUE = 51;
    private const int OUTPUT_PIN_INITIALIZED = 101;
    private const int OUTPUT_PIN_SHUFFLED = 111;
    private const int OUTPUT_PIN_CHOICE = 121;
    private const int OUTPUT_PIN_DECIDED = 131;
    private const int OUTPUT_PIN_WIN = 141;
    private const int OUTPUT_PIN_NEXT = 151;
    private const int OUTPUT_PIN_END = 161;
    private const int OUTPUT_PIN_CONTINUED = 171;
    [SerializeField]
    private DrawCardObject CardPrefabTemplate;
    [SerializeField]
    private Text GetItemText;
    [SerializeField]
    private Transform VerticalParent;
    [SerializeField]
    private Transform HorizontalParentTemplate;
    [SerializeField]
    private int HorizontalItemMax = 3;
    [SerializeField]
    private Transform MagnusPosition;
    [SerializeField]
    private Transform ShufflePosition;
    [SerializeField]
    private GameObject DrawMaskObject;
    [SerializeField]
    private DrawCardRewardTable DrawCardRewardTable;
    [SerializeField]
    private DrawCardObject ResultCardObject;
    [SerializeField]
    private bool TapWaitEnable;
    [SerializeField]
    private CustomSound SE_CardDealMove;
    [SerializeField]
    private CustomSound SE_CardShuffleMove;
    [SerializeField]
    private CustomSound SE_CardLineUpMove;
    [SerializeField]
    private CustomSound SE_CardGetMove;
    [SerializeField]
    private CustomSound SE_CardGetMoveEnd;
    [SerializeField]
    private float CardDealMoveSec = 0.14f;
    [SerializeField]
    private float CardDealDelaySec = 0.04f;
    [SerializeField]
    private float CardDealEndWaitSec = 0.3f;
    [SerializeField]
    private float CardOpenDelaySec = 0.1f;
    [SerializeField]
    private float CardOpenEndWaitSec = 0.5f;
    [SerializeField]
    private float CardFaceDownDelaySec = 0.04f;
    [SerializeField]
    private float CardFaceDownEndWaitSec = 0.15f;
    [SerializeField]
    private float CardShuffleMoveDelaySec = 0.04f;
    [SerializeField]
    private float CardShuffleMoveSec = 0.14f;
    [SerializeField]
    private float CardShuffleMoveEndWaitSec = 0.15f;
    [SerializeField]
    private float CardLineUpMoveSec = 0.14f;
    [SerializeField]
    private float CardLineUpDelaySec = 0.04f;
    [SerializeField]
    private float CardDrawEndWaitSec = 0.8f;
    [SerializeField]
    private float GetItemMoveSec = 0.5f;
    private List<Transform> mHolizontalParents = new List<Transform>();
    private List<DrawCardObject> mDrawCardObjects = new List<DrawCardObject>();
    private DrawCardObject mSelectCardObject;
    private List<DrawCardRewardParam.Data> mRewardList;

    private void Awake()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Initialize();
          break;
        case 11:
          this.Shuffle();
          break;
        case 21:
          this.Ready();
          break;
        case 31:
          this.Decide();
          break;
        case 41:
          this.Draw();
          break;
        case 51:
          this.Continue();
          break;
      }
    }

    public void Initialize()
    {
      if (Object.op_Inequality((Object) this.GetItemText, (Object) null))
        ((Component) this.GetItemText).gameObject.SetActive(false);
      if (this.mDrawCardObjects != null)
      {
        foreach (Component mDrawCardObject in this.mDrawCardObjects)
          Object.Destroy((Object) mDrawCardObject.gameObject);
      }
      if (this.mHolizontalParents != null && this.mHolizontalParents.Count > 0)
      {
        foreach (Component holizontalParent in this.mHolizontalParents)
          Object.Destroy((Object) holizontalParent.gameObject);
      }
      if (Object.op_Inequality((Object) this.DrawMaskObject, (Object) null))
        this.DrawMaskObject.SetActive(false);
      this.mDrawCardObjects = new List<DrawCardObject>();
      this.mHolizontalParents = new List<Transform>();
      ((Component) this.CardPrefabTemplate).gameObject.SetActive(false);
      ((Component) this.HorizontalParentTemplate).gameObject.SetActive(false);
      List<DrawCardParam.CardData> selectDrawCardList = DrawCardParam.SelectDrawCardList;
      if (selectDrawCardList != null && selectDrawCardList.Count > 0)
      {
        int num = this.HorizontalItemMax;
        if (selectDrawCardList.Count > this.HorizontalItemMax)
        {
          num = selectDrawCardList.Count / 2;
          if (selectDrawCardList.Count % 2 != 0)
            num = this.HorizontalItemMax;
        }
        Transform transform = (Transform) null;
        for (int index = 0; index < selectDrawCardList.Count; ++index)
        {
          if (index % num == 0)
          {
            transform = Object.Instantiate<Transform>(this.HorizontalParentTemplate, this.VerticalParent);
            this.mHolizontalParents.Add(transform);
            ((Component) transform).gameObject.SetActive(true);
          }
          DrawCardObject drawCardObject = Object.Instantiate<DrawCardObject>(this.CardPrefabTemplate, transform);
          this.mDrawCardObjects.Add(drawCardObject);
          drawCardObject.SetCardData(selectDrawCardList[index]);
          drawCardObject.Initialize(this);
          drawCardObject.CardItemActive(false);
          ((Component) drawCardObject).gameObject.SetActive(true);
          drawCardObject.SetStartPosition(this.MagnusPosition.position);
        }
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Continue() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 171);

    public void Ready()
    {
    }

    private void Deal()
    {
    }

    private void PlaySE(CustomSound se_object)
    {
      if (!Object.op_Inequality((Object) se_object, (Object) null))
        return;
      ((Component) se_object).gameObject.SetActive(true);
      se_object.Play();
    }

    [DebuggerHidden]
    private IEnumerator ShuffleCroutine()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DrawCardContent.\u003CShuffleCroutine\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void Shuffle() => this.StartCoroutine(this.ShuffleCroutine());

    public void SelectCard(DrawCardObject card)
    {
      if (Object.op_Equality((Object) card, (Object) this.mSelectCardObject))
        return;
      if (Object.op_Inequality((Object) this.mSelectCardObject, (Object) null))
        this.mSelectCardObject.Select(false);
      this.mSelectCardObject = card;
      this.mSelectCardObject.Select(true);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 121);
    }

    private void Decide()
    {
      if (!Object.op_Inequality((Object) this.mSelectCardObject, (Object) null))
        return;
      foreach (DrawCardObject mDrawCardObject in this.mDrawCardObjects)
        mDrawCardObject.ButtonActive(false);
      DrawCardParam.SelectDrawCardIndex = this.mDrawCardObjects.IndexOf(this.mSelectCardObject);
      this.mSelectCardObject.Select(false);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 131);
    }

    [DebuggerHidden]
    private IEnumerator DrawCroutine()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DrawCardContent.\u003CDrawCroutine\u003Ec__Iterator1()
      {
        \u0024this = this
      };
    }

    private void SetDrawItemText(DrawCardParam.CardData card)
    {
      if (Object.op_Equality((Object) this.GetItemText, (Object) null) || card == null)
        return;
      string str = string.Empty;
      string formatedText = card.ItemNum.ToString();
      switch (card.ItemType)
      {
        case 0:
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(card.ItemIname);
          if (itemParam != null)
          {
            str = itemParam.name;
            break;
          }
          break;
        case 1:
          str = LocalizedText.Get("sys.GOLD");
          formatedText = CurrencyBitmapText.CreateFormatedText(formatedText);
          break;
        case 2:
          str = LocalizedText.Get("sys.COIN");
          break;
        case 4:
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(card.ItemIname);
          if (unitParam != null)
          {
            str = unitParam.name;
            break;
          }
          break;
        case 5:
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(card.ItemIname);
          if (conceptCardParam != null)
          {
            str = conceptCardParam.name;
            break;
          }
          break;
      }
      if (string.IsNullOrEmpty(str))
        return;
      this.GetItemText.text = LocalizedText.Get("sys.CHALLENGE_CARD_TEXT_2", (object) str, (object) formatedText);
    }

    private void Draw() => this.StartCoroutine(this.DrawCroutine());
  }
}
