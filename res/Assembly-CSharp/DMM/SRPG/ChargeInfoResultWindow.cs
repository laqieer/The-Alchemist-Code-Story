// Decompiled with JetBrains decompiler
// Type: SRPG.ChargeInfoResultWindow
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
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Skip", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "Close", FlowNode.PinTypes.Output, 20)]
  public class ChargeInfoResultWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_REFRESH = 0;
    private const int INPUT_SKIP = 10;
    private const int OUTPUT_CLOSE = 20;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject UnitTemplate;
    [SerializeField]
    private GameObject ArtifactTemplate;
    [SerializeField]
    private GameObject ConceptCardTemplate;
    [SerializeField]
    private GameObject CoinTemplate;
    [SerializeField]
    private GameObject GoldTemplate;
    [SerializeField]
    private string CheckAnimState = "opened";
    [SerializeField]
    private string SkipAnimTrigger = "skip";
    [SerializeField]
    private string SkipToAnimState = "dropitem_End";
    [SerializeField]
    private GameObject BackGround;
    private List<FirstChargeReward> m_Rewards = new List<FirstChargeReward>();
    private List<GameObject> m_IconObj = new List<GameObject>();
    private Animator m_WindowAnim;
    private bool m_IsRefresh;
    private bool m_IsClosed;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.m_IsRefresh = true;
          break;
        case 10:
          if (this.m_IsClosed)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
            break;
          }
          this.SkipIconAnimation();
          break;
      }
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
        this.UnitTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactTemplate, (Object) null))
        this.ArtifactTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
        this.ConceptCardTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.CoinTemplate, (Object) null))
        this.CoinTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.GoldTemplate, (Object) null))
        this.GoldTemplate.SetActive(false);
      Animator component = ((Component) this).gameObject.GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.m_WindowAnim = component;
    }

    private void Update()
    {
      if (this.m_IsRefresh)
        this.Refresh();
      else
        this.CheckIconAnimation();
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.m_WindowAnim, (Object) null))
      {
        DebugUtility.LogError("Animator Not Found");
        this.m_IsRefresh = false;
      }
      else
      {
        if (!GameUtility.CompareAnimatorStateName((Component) this.m_WindowAnim, this.CheckAnimState))
          return;
        if (this.m_Rewards == null || this.m_Rewards.Count < 0)
        {
          DebugUtility.LogError("受け取り報酬が存在しません.");
        }
        else
        {
          for (int index = 0; index < this.m_Rewards.Count; ++index)
          {
            FirstChargeReward reward = this.m_Rewards[index];
            GameObject gameObject = (GameObject) null;
            if (reward.CheckGiftTypes(GiftTypes.Item))
            {
              gameObject = this.SetItem(reward.iname, reward.num);
              if (Object.op_Equality((Object) gameObject, (Object) null))
              {
                DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Item)!");
                continue;
              }
            }
            else if (reward.CheckGiftTypes(GiftTypes.Gold))
            {
              gameObject = this.SetGold(reward.num);
              if (Object.op_Equality((Object) gameObject, (Object) null))
              {
                DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Gold)!");
                continue;
              }
            }
            else if (reward.CheckGiftTypes(GiftTypes.Coin))
            {
              gameObject = this.SetCoin(reward.num);
              if (Object.op_Equality((Object) gameObject, (Object) null))
              {
                DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Coin)!");
                continue;
              }
            }
            else if (reward.CheckGiftTypes(GiftTypes.Unit))
            {
              gameObject = this.SetUnit(reward.iname);
              if (Object.op_Equality((Object) gameObject, (Object) null))
              {
                DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Unit)!");
                continue;
              }
            }
            else if (reward.CheckGiftTypes(GiftTypes.Artifact))
            {
              gameObject = this.SetArtifact(reward.iname);
              if (Object.op_Equality((Object) gameObject, (Object) null))
              {
                DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:Artifact)!");
                continue;
              }
            }
            else if (reward.CheckGiftTypes(GiftTypes.ConceptCard))
            {
              gameObject = this.SetConceptCard(reward.iname);
              if (Object.op_Equality((Object) gameObject, (Object) null))
              {
                DebugUtility.LogError("[ChargeInfoResultWindow.cs]obj is null(Type:ConceptCard)!");
                continue;
              }
            }
            if (Object.op_Inequality((Object) gameObject, (Object) null))
              this.m_IconObj.Add(gameObject);
          }
          this.m_IsRefresh = false;
        }
      }
    }

    private void SkipIconAnimation()
    {
      if (this.m_IsClosed || this.m_IconObj == null)
        return;
      for (int index = 0; index < this.m_IconObj.Count; ++index)
      {
        GameObject gameObject = this.m_IconObj[index];
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          Animator component = gameObject.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.SetTrigger(this.SkipAnimTrigger);
        }
      }
      this.m_IsClosed = true;
      if (!Object.op_Inequality((Object) this.BackGround, (Object) null))
        return;
      Button component1 = this.BackGround.GetComponent<Button>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      ((Selectable) component1).interactable = false;
    }

    private void CheckIconAnimation()
    {
      if (this.m_IsClosed || this.m_IconObj == null || this.m_IconObj.Count <= 0)
        return;
      int num = 0;
      for (int index = 0; index < this.m_IconObj.Count; ++index)
      {
        GameObject go = this.m_IconObj[index];
        if (Object.op_Inequality((Object) go, (Object) null))
          num = !GameUtility.CompareAnimatorStateName(go, this.SkipToAnimState) ? num : num + 1;
      }
      if (num != this.m_IconObj.Count)
        return;
      this.m_IsClosed = true;
    }

    public void SetUp(FirstChargeReward[] _rewards)
    {
      if (_rewards == null || _rewards.Length <= 0)
        return;
      this.m_Rewards.Clear();
      this.m_Rewards.AddRange((IEnumerable<FirstChargeReward>) _rewards);
    }

    private GameObject SetItem(string _iname, int _num)
    {
      GameObject gameObject = (GameObject) null;
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
      {
        gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.transform.SetParent(this.ItemTemplate.transform.parent, false);
          gameObject.SetActive(true);
          ItemData data = new ItemData();
          data.Setup(0L, _iname, _num);
          DataSource.Bind<ItemData>(gameObject, data);
          this.SetAnimatorTrigger(gameObject, "on");
        }
      }
      return gameObject;
    }

    private GameObject SetUnit(string _iname)
    {
      GameObject gameObject = (GameObject) null;
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(_iname);
      if (unitParam == null)
      {
        DebugUtility.LogError("ユニット INAME:" + _iname + "は存在しません.");
        return (GameObject) null;
      }
      if (Object.op_Inequality((Object) this.UnitTemplate, (Object) null))
      {
        gameObject = Object.Instantiate<GameObject>(this.UnitTemplate);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.transform.SetParent(this.UnitTemplate.transform.parent, false);
          UnitData data = new UnitData();
          data.Setup(_iname, 0, 0, 0, string.Empty, elem: unitParam.element);
          DataSource.Bind<UnitData>(gameObject, data);
          gameObject.SetActive(true);
          this.SetAnimatorTrigger(gameObject, "on");
        }
      }
      return gameObject;
    }

    private GameObject SetGold(int _num = 0)
    {
      GameObject gameObject = (GameObject) null;
      if (Object.op_Inequality((Object) this.GoldTemplate, (Object) null))
      {
        gameObject = Object.Instantiate<GameObject>(this.GoldTemplate);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.transform.SetParent(this.GoldTemplate.transform.parent, false);
          gameObject.SetActive(true);
          this.SetBitmapText(gameObject, "num", _num);
          this.SetAnimatorTrigger(gameObject, "on");
        }
      }
      return gameObject;
    }

    private GameObject SetCoin(int _num = 0)
    {
      GameObject gameObject = (GameObject) null;
      if (Object.op_Inequality((Object) this.CoinTemplate, (Object) null))
      {
        gameObject = Object.Instantiate<GameObject>(this.CoinTemplate);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.transform.SetParent(this.CoinTemplate.transform.parent, false);
          gameObject.SetActive(true);
          this.SetBitmapText(gameObject, "num", _num);
          this.SetAnimatorTrigger(gameObject, "on");
        }
      }
      return gameObject;
    }

    private void SetBitmapText(GameObject _obj, string _name, int _num)
    {
      SerializeValueBehaviour component = _obj.GetComponent<SerializeValueBehaviour>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Text uiLabel = component.list.GetUILabel(_name);
      if (!Object.op_Inequality((Object) uiLabel, (Object) null))
        return;
      uiLabel.text = _num.ToString();
    }

    private GameObject SetArtifact(string _iname)
    {
      GameObject gameObject = (GameObject) null;
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(_iname);
      if (artifactParam == null)
      {
        DebugUtility.LogError("武具 INAME:" + _iname + "は存在しません");
        return (GameObject) null;
      }
      if (Object.op_Inequality((Object) this.ArtifactTemplate, (Object) null))
      {
        gameObject = Object.Instantiate<GameObject>(this.ArtifactTemplate);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.transform.SetParent(this.ArtifactTemplate.transform.parent, false);
          gameObject.SetActive(true);
          ArtifactData data = new ArtifactData();
          data.Deserialize(new Json_Artifact()
          {
            iid = 1L,
            exp = 0,
            iname = artifactParam.iname,
            fav = 0,
            rare = artifactParam.rareini
          });
          DataSource.Bind<ArtifactData>(gameObject, data);
          this.SetAnimatorTrigger(gameObject, "on");
        }
      }
      return gameObject;
    }

    private GameObject SetConceptCard(string _iname)
    {
      GameObject gameObject = (GameObject) null;
      if (MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(_iname) == null)
      {
        DebugUtility.LogError("真理念装 INAME:" + _iname + "は存在しません");
        return (GameObject) null;
      }
      if (Object.op_Inequality((Object) this.ConceptCardTemplate, (Object) null))
      {
        gameObject = Object.Instantiate<GameObject>(this.ConceptCardTemplate);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          gameObject.transform.SetParent(this.ConceptCardTemplate.transform.parent, false);
          gameObject.SetActive(true);
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(_iname);
          if (cardDataForDisplay != null)
          {
            ConceptCardIcon component = gameObject.GetComponent<ConceptCardIcon>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Setup(cardDataForDisplay);
          }
          this.SetAnimatorTrigger(gameObject, "on");
        }
      }
      return gameObject;
    }

    private void SetAnimatorTrigger(GameObject _obj, string _name)
    {
      if (!Object.op_Inequality((Object) _obj, (Object) null))
        return;
      Animator component = _obj.GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetTrigger(_name);
    }
  }
}
