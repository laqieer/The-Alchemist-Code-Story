// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(4, "継続", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "完了", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "ミッション報酬", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(3, "コンプリート報酬", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(5, "全ミッションコンプリート", FlowNode.PinTypes.Output, 5)]
  public class ChallengeMissionReward : MonoBehaviour, IFlowInterface
  {
    public GameObject PanelNormal;
    public GameObject PanelComplete;
    public Transform RewardHolder;
    public GameObject ItemTemplate;
    public GameObject ExpTemplate;
    public GameObject GoldTemplate;
    public GameObject StaminaTemplate;
    public GameObject ConceptCardTemplate;
    public GameObject UnitTemplate;
    public UnityEngine.UI.Text TextMessage;
    private bool isAllMissionCompleteMessageShown;
    [SerializeField]
    private GridLayoutGroup GridLayout;
    [SerializeField]
    private int MaxCellSize = 150;
    [SerializeField]
    private int MinCellSize = 100;
    private TrophyParam mTrophy;

    public void Activated(int pinID)
    {
      if (pinID != 4)
        return;
      if (this.isAllMissionCompleteMessageShown && this.mTrophy.iname == "CHALLENGE_06")
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      else
        this.StartCoroutine(this.showRewardMessage());
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.PanelNormal, (Object) null))
        this.PanelNormal.SetActive(false);
      if (Object.op_Inequality((Object) this.PanelComplete, (Object) null))
        this.PanelComplete.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ExpTemplate, (Object) null))
        this.ExpTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.GoldTemplate, (Object) null))
        this.GoldTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.StaminaTemplate, (Object) null))
        this.StaminaTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
        this.ConceptCardTemplate.gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
        return;
      this.UnitTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.TextMessage, (Object) null))
        ((Behaviour) this).enabled = false;
      else if (string.IsNullOrEmpty(GlobalVars.SelectedChallengeMissionTrophy))
      {
        ((Behaviour) this).enabled = false;
      }
      else
      {
        this.mTrophy = ChallengeMission.GetTrophy(GlobalVars.SelectedChallengeMissionTrophy);
        if (this.mTrophy == null)
        {
          ((Behaviour) this).enabled = false;
        }
        else
        {
          if (this.mTrophy.IsChallengeMissionRoot)
          {
            this.PanelNormal.SetActive(false);
            this.PanelComplete.SetActive(true);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
          }
          else
          {
            this.PanelNormal.SetActive(true);
            this.PanelComplete.SetActive(false);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
          }
          this.UpdateReward(this.mTrophy);
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator showRewardMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ChallengeMissionReward.\u003CshowRewardMessage\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void UpdateRewardSingle(TrophyParam trophy)
    {
      if (trophy == null)
        return;
      bool isLarge = this.SetRewardCellSize(this.GridLayout, 1);
      string format1 = LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_ITEM");
      string format2 = LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_OTHER");
      string str1 = string.Empty;
      if (trophy.Gold != 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.GoldTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        this.UpdateRewardAmount(gameObject, trophy.Gold);
        gameObject.SetActive(true);
        string formatedText = CurrencyBitmapText.CreateFormatedText(trophy.Gold.ToString());
        string str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) formatedText);
        str1 = string.Format(format2, (object) str2);
      }
      else if (trophy.Exp != 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ExpTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        this.UpdateRewardAmount(gameObject, trophy.Exp);
        gameObject.SetActive(true);
        string str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) trophy.Exp);
        str1 = string.Format(format2, (object) str3);
      }
      else if (trophy.Coin != 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        this.UpdateRewardAmount(gameObject, trophy.Coin);
        gameObject.SetActive(true);
        string str4 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) trophy.Coin);
        str1 = string.Format(format2, (object) str4);
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
        DataSource.Bind<ItemParam>(gameObject, itemParam);
      }
      else if (trophy.Stamina != 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.StaminaTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        this.UpdateRewardAmount(gameObject, trophy.Stamina);
        gameObject.SetActive(true);
        string str5 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) trophy.Stamina);
        str1 = string.Format(format2, (object) str5);
      }
      else if (trophy.Items != null && trophy.Items.Length > 0)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(trophy.Items[0].iname);
        if (itemParam != null)
        {
          if (itemParam.type == EItemType.Unit)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.UnitTemplate);
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            gameObject.transform.SetParent(this.RewardHolder, false);
            this.UpdateRewardAmount(gameObject, trophy.Items[0].Num);
            ChallengeMissionRewardIconSelection component = gameObject.GetComponent<ChallengeMissionRewardIconSelection>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.SetLarge(isLarge);
            gameObject.SetActive(true);
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(itemParam.iname);
            if (unitParam != null)
            {
              string str6 = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT_BR", (object) ((int) unitParam.rare + 1), (object) unitParam.name);
              str1 = string.Format(format2, (object) str6);
            }
          }
          else
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            gameObject.transform.SetParent(this.RewardHolder, false);
            this.UpdateRewardAmount(gameObject, trophy.Items[0].Num);
            gameObject.SetActive(true);
            str1 = string.Format(format1, (object) itemParam.name, (object) trophy.Items[0].Num);
          }
        }
      }
      else if (trophy.ConceptCards != null && trophy.ConceptCards.Length > 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ConceptCardTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        this.UpdateRewardAmount(gameObject, trophy.ConceptCards[0].Num);
        gameObject.SetActive(true);
        ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(trophy.ConceptCards[0].iname);
        if (conceptCardParam != null)
        {
          str1 = string.Format(LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_CONCEPT_CARD"), (object) conceptCardParam.name, (object) trophy.ConceptCards[0].Num);
          ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname);
            component.Setup(cardDataForDisplay);
          }
        }
      }
      if (!Object.op_Inequality((Object) this.TextMessage, (Object) null))
        return;
      this.TextMessage.text = str1;
    }

    private void UpdateRewardSome(TrophyParam trophy)
    {
      if (trophy == null)
        return;
      int count = 0;
      List<GameObject> gameObjectList = new List<GameObject>();
      if (trophy.Gold != 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.GoldTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        this.UpdateRewardAmount(gameObject, trophy.Gold);
        gameObject.SetActive(true);
        ++count;
      }
      if (trophy.Exp != 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ExpTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        this.UpdateRewardAmount(gameObject, trophy.Exp);
        gameObject.SetActive(true);
        ++count;
      }
      if (trophy.Coin != 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
        DataSource.Bind<ItemParam>(gameObject, itemParam);
        this.UpdateRewardAmount(gameObject, trophy.Coin);
        gameObject.SetActive(true);
        ++count;
      }
      if (trophy.Stamina != 0)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.StaminaTemplate);
        gameObject.transform.SetParent(this.RewardHolder, false);
        this.UpdateRewardAmount(gameObject, trophy.Stamina);
        gameObject.SetActive(true);
        ++count;
      }
      if (trophy.Items != null && trophy.Items.Length > 0)
      {
        foreach (TrophyParam.RewardItem rewardItem in trophy.Items)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(rewardItem.iname);
          if (itemParam != null)
          {
            GameObject gameObject;
            if (itemParam.type == EItemType.Unit)
            {
              gameObject = Object.Instantiate<GameObject>(this.UnitTemplate);
              gameObjectList.Add(gameObject);
            }
            else
              gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
            gameObject.transform.SetParent(this.RewardHolder, false);
            DataSource.Bind<ItemParam>(gameObject, itemParam);
            this.UpdateRewardAmount(gameObject, rewardItem.Num);
            gameObject.SetActive(true);
            ++count;
          }
        }
      }
      if (trophy.ConceptCards != null && trophy.ConceptCards.Length > 0)
      {
        foreach (TrophyParam.RewardItem conceptCard in trophy.ConceptCards)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.ConceptCardTemplate);
          gameObject.transform.SetParent(this.RewardHolder, false);
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(conceptCard.iname);
          if (conceptCardParam != null)
          {
            ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(conceptCardParam.iname);
              component.Setup(cardDataForDisplay);
            }
          }
          this.UpdateRewardAmount(gameObject, conceptCard.Num);
          gameObject.SetActive(true);
          ++count;
        }
      }
      bool isLarge = this.SetRewardCellSize(this.GridLayout, count);
      foreach (GameObject gameObject in gameObjectList)
      {
        ChallengeMissionRewardIconSelection component = gameObject.GetComponent<ChallengeMissionRewardIconSelection>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.SetLarge(isLarge);
      }
      if (!Object.op_Inequality((Object) this.TextMessage, (Object) null))
        return;
      this.TextMessage.text = LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_SOME");
    }

    private bool SetRewardCellSize(GridLayoutGroup grid, int count)
    {
      if (Object.op_Equality((Object) grid, (Object) null) || count < 0)
        return false;
      bool flag = true;
      Rect rect = ((RectTransform) ((Component) grid).transform).rect;
      float width = ((Rect) ref rect).width;
      int num1 = this.MaxCellSize;
      int num2 = count <= 1 ? 0 : count - 1;
      if ((double) ((float) (((LayoutGroup) grid).padding.left + num1 * count) + grid.spacing.x * (float) num2 + (float) ((LayoutGroup) grid).padding.right) > (double) width)
      {
        num1 = this.MinCellSize;
        flag = false;
      }
      grid.cellSize = new Vector2((float) num1, (float) num1);
      return flag;
    }

    private void UpdateRewardAmount(GameObject obj, int num)
    {
      ItemAmount component = obj.GetComponent<ItemAmount>();
      if (!Object.op_Inequality((Object) component, (Object) null) || !Object.op_Inequality((Object) component.Amount, (Object) null))
        return;
      component.Amount.text = num.ToString();
    }

    private void UpdateReward(TrophyParam trophy)
    {
      int num = 0;
      if (trophy.Gold != 0)
        ++num;
      if (trophy.Exp != 0)
        ++num;
      if (trophy.Coin != 0)
        ++num;
      if (trophy.Stamina != 0)
        ++num;
      if (trophy.Items != null && trophy.Items.Length > 0)
        num += trophy.Items.Length;
      if (trophy.ConceptCards != null && trophy.ConceptCards.Length > 0)
        num += trophy.ConceptCards.Length;
      if (num > 1)
        this.UpdateRewardSome(trophy);
      else
        this.UpdateRewardSingle(trophy);
    }

    private string GetAllRewardText(TrophyParam trophy)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      if (trophy.Items != null && trophy.Items.Length > 0)
      {
        foreach (TrophyParam.RewardItem rewardItem in this.mTrophy.Items)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(rewardItem.iname);
          if (itemParam != null)
          {
            if (itemParam.type == EItemType.UnitPiece)
            {
              stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_PIECE"), (object) itemParam.name, (object) rewardItem.Num));
              flag = true;
            }
            else if (itemParam.type == EItemType.Unit)
            {
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(itemParam.iname);
              if (unitParam != null)
              {
                string str = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT", (object) ((int) unitParam.rare + 1), (object) unitParam.name);
                stringBuilder.AppendLine(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_GET", (object) str));
              }
            }
            else
              stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD"), (object) itemParam.name, (object) rewardItem.Num));
          }
        }
      }
      if (trophy.Gold != 0)
      {
        string formatedText = CurrencyBitmapText.CreateFormatedText(trophy.Gold.ToString());
        stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (object) formatedText));
      }
      if (trophy.Exp != 0)
        stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (object) trophy.Exp));
      if (trophy.Coin != 0)
        stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) trophy.Coin));
      if (trophy.Stamina != 0)
        stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (object) trophy.Stamina));
      if (trophy.ConceptCards != null)
      {
        foreach (TrophyParam.RewardItem conceptCard in trophy.ConceptCards)
        {
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(conceptCard.iname);
          if (conceptCardParam == null)
            Debug.LogError((object) "GetConceptCardParam == null");
          else
            stringBuilder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_CONCEPT_CARD"), (object) conceptCardParam.name, (object) conceptCard.Num));
        }
      }
      if (flag)
      {
        stringBuilder.AppendLine(string.Empty);
        stringBuilder.AppendLine(LocalizedText.Get("sys.CHALLENGE_REWARD_NOTE"));
      }
      return stringBuilder.ToString();
    }
  }
}
