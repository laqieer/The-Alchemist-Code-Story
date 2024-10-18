// Decompiled with JetBrains decompiler
// Type: SRPG.TotalLoginBonusWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Get", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Request", FlowNode.PinTypes.Output, 10)]
  public class TotalLoginBonusWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_GET_REWARD = 0;
    private const int PIN_IN_REFRESH = 1;
    private const int PIN_OUT_REQUEST = 10;
    [SerializeField]
    private RectTransform IconParent;
    [SerializeField]
    private ListItemEvents Item_Normal;
    [SerializeField]
    private ListItemEvents Item_Taken;
    [SerializeField]
    private Text TotalLoginCountText;
    [SerializeField]
    private Button RewardButton;
    private TrophyState m_CurrentTrophy;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        this.GetReward();
      }
      else
      {
        if (pinID != 1)
          return;
        this.Refresh(this.RefreshTrophyState());
      }
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Item_Normal, (Object) null))
        ((Component) this.Item_Normal).gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.Item_Taken, (Object) null))
        return;
      ((Component) this.Item_Taken).gameObject.SetActive(false);
    }

    private void Start()
    {
      this.m_CurrentTrophy = (TrophyState) null;
      this.Refresh(this.RefreshTrophyState());
    }

    private void ClearItems()
    {
      if (this.mItems == null)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mItems[index].Body, (Object) null))
        {
          Object.Destroy((Object) ((Component) this.mItems[index].Body).gameObject);
          this.mItems[index].Body = (Transform) null;
        }
      }
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
    }

    private void Refresh(TrophyState[] trophies)
    {
      if (trophies == null || trophies.Length <= 0)
      {
        DebugUtility.LogError("有効な累計ログインレコードミッションが存在しません.");
      }
      else
      {
        int loginCount = MonoSingleton<GameManager>.Instance.Player.LoginCount;
        this.ClearItems();
        TrophyState trophy1 = trophies[0];
        TrophyState trophy2 = trophies.Length <= 1 ? (TrophyState) null : trophies[1];
        this.CreateIcon(trophy1);
        if (this.m_CurrentTrophy != null)
          this.CreateIcon(trophy2);
        if (Object.op_Inequality((Object) this.TotalLoginCountText, (Object) null))
          this.TotalLoginCountText.text = loginCount.ToString();
        this.m_CurrentTrophy = this.m_CurrentTrophy != null ? trophy2 : trophy1;
        if (!Object.op_Inequality((Object) this.RewardButton, (Object) null))
          return;
        ((Selectable) this.RewardButton).interactable = this.m_CurrentTrophy != null && this.m_CurrentTrophy.IsCompleted;
      }
    }

    private void CreateIcon(TrophyState state)
    {
      if (state == null)
      {
        DebugUtility.LogError("表示したいTrophyStateがnullです.");
      }
      else
      {
        string str = string.Empty;
        int num;
        if (state.Param.Items != null && state.Param.Items.Length > 0)
        {
          str = state.Param.Items[0].iname;
          num = state.Param.Items[0].Num;
        }
        else if (state.Param.ConceptCards != null && state.Param.ConceptCards.Length > 0)
        {
          str = state.Param.ConceptCards[0].iname;
          num = state.Param.ConceptCards[0].Num;
        }
        else if (state.Param.Coin > 0)
        {
          str = "$COIN";
          num = state.Param.Coin;
        }
        else if (state.Param.Gold > 0)
        {
          num = state.Param.Gold;
        }
        else
        {
          DebugUtility.LogError("不明な報酬が設定されています.");
          return;
        }
        GiftRecieveItemData data = new GiftRecieveItemData();
        if (!string.IsNullOrEmpty(str))
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(str, false);
          if (itemParam != null)
          {
            data.Set(str, GiftTypes.Item, itemParam.rare, num);
            data.name = itemParam.name;
          }
          ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(str);
          if (conceptCardParam != null)
          {
            data.Set(str, GiftTypes.ConceptCard, conceptCardParam.rare, num);
            data.name = conceptCardParam.name;
          }
        }
        else
        {
          data.Set(string.Empty, GiftTypes.Gold, 0, num);
          data.name = num.ToString() + LocalizedText.Get("sys.GOLD");
        }
        ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(!state.IsEnded ? this.Item_Normal : this.Item_Taken);
        this.mItems.Add(listItemEvents);
        DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, data);
        DataSource.Bind<TrophyParam>(((Component) listItemEvents).gameObject, state.Param);
        ((Component) listItemEvents).transform.SetParent((Transform) this.IconParent, false);
        ((Component) listItemEvents).gameObject.SetActive(true);
        ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
    }

    private void GetReward()
    {
      if (this.m_CurrentTrophy == null)
      {
        DebugUtility.LogError("受け取り対象のTrophyがありません.");
      }
      else
      {
        TrophyParam trophy = this.m_CurrentTrophy.Param;
        if (trophy == null)
          return;
        TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.TrophyData.GetTrophyCounter(trophy, true);
        if (trophyCounter.IsEnded || !trophyCounter.IsCompleted)
          return;
        GlobalVars.SelectedTrophy.Set(trophy.iname);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
    }

    private TrophyState[] RefreshTrophyState()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TrophyParam[] trophies = instance.Trophies;
      List<TrophyState> trophyStateList = new List<TrophyState>();
      for (int index = 0; index < trophies.Length; ++index)
      {
        TrophyState trophyCounter = instance.Player.TrophyData.GetTrophyCounter(trophies[index], true);
        if (trophyCounter.Param.Objectives[0].type == TrophyConditionTypes.logincount)
        {
          if (this.m_CurrentTrophy != null && this.m_CurrentTrophy.iname == trophyCounter.Param.iname)
            trophyStateList.Add(trophyCounter);
          else if (!trophyCounter.IsEnded)
            trophyStateList.Add(trophyCounter);
        }
      }
      return trophyStateList.ToArray();
    }
  }
}
