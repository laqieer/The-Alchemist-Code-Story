// Decompiled with JetBrains decompiler
// Type: SRPG.BoxGachaManager
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
  [FlowNode.NodeType("UI/BoxGachaManager", 32741)]
  [FlowNode.Pin(0, "IN 表示更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "OT 表示更新完了", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(10, "IN 1回交換を選択", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "IN N回交換を選択", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "OT 1回交換を選択", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "OT N回交換を選択", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "IN リザルトから交換を選択", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(15, "OT 交換しようとしたがBOXが空", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(16, "OT 交換しようとしたがコスト不足", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(20, "IN 現在の排出状況を表示", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(21, "OT 現在の排出状況を表示)", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(30, "IN 全ステップの詳細を表示", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(31, "OT 全ステップの詳細を表示", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(40, "IN BOXをリセットする", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(41, "OT BOXをリセットする（BOX内が空じゃない）", FlowNode.PinTypes.Output, 41)]
  [FlowNode.Pin(42, "OT BOXをリセットする（BOX内が空）", FlowNode.PinTypes.Output, 42)]
  public class BoxGachaManager : FlowNodePersistent
  {
    public static readonly int SELECT_SINGLE = 1;
    public static readonly int SELECT_MULTI_MAX = 10;
    private const int PIN_IN_REFRESH = 0;
    private const int PIN_OT_REFRESH = 1;
    private const int PIN_IN_SELECT_EXEC_SINGLE = 10;
    private const int PIN_IN_SELECT_EXEC_MULTI = 11;
    private const int PIN_OT_SELECT_EXEC_SINGLE = 12;
    private const int PIN_OT_SELECT_EXEC_MULTI = 13;
    private const int PIN_IN_SELECT_EXEC_RESULT = 14;
    private const int PIN_OT_SELECT_EXEC_BOX_EMPTY = 15;
    private const int PIN_OT_SELECT_EXEC_COST_SHORT = 16;
    private const int PIN_IN_SELECT_DRAW_STATUS = 20;
    private const int PIN_OT_SELECT_DRAW_STATUS = 21;
    private const int PIN_IN_SELECT_LINEUP = 30;
    private const int PIN_OT_SELECT_LINEUP = 31;
    private const int PIN_IN_SELECT_RESET = 40;
    private const int PIN_OT_SELECT_RESET_NO_EMPTY = 41;
    private const int PIN_OT_SELECT_RESET_EMPTY = 42;
    [SerializeField]
    private Button m_SingleButton;
    [SerializeField]
    private Button m_MultiButton;
    [SerializeField]
    private Button m_ResultButton;
    [SerializeField]
    private GameObject m_Cost;
    [SerializeField]
    private GameObject m_CostResult;
    [SerializeField]
    private Button m_ResetButton;
    [SerializeField]
    private GameObject m_TopImage;
    private GameObject m_CostItemObj;
    private GameObject m_CostCoinObj;
    private GameObject m_CostGoldObj;
    private GameObject m_CostResultItemObj;
    private GameObject m_CostResultCoinObj;
    private GameObject m_CostResultGoldObj;
    private bool m_LastMultiToucheEnabled;
    private bool m_LastSelectExecSingle;
    private static BoxGachaManager.BoxGachaStatus m_CurrentBoxGachaStatus;

    public static BoxGachaManager.BoxGachaStatus CurrentBoxGachaStatus
    {
      get
      {
        return BoxGachaManager.m_CurrentBoxGachaStatus;
      }
      set
      {
        BoxGachaManager.m_CurrentBoxGachaStatus = value;
      }
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          this.ActivateOutputLinks(1);
          break;
        case 10:
          int pinID1 = 12;
          this.m_LastSelectExecSingle = true;
          if (BoxGachaManager.m_CurrentBoxGachaStatus.IsCostShort(false))
            pinID1 = 16;
          else if (BoxGachaManager.m_CurrentBoxGachaStatus.IsBoxEmpty)
            pinID1 = 15;
          this.ActivateOutputLinks(pinID1);
          break;
        case 11:
          int pinID2 = 13;
          this.m_LastSelectExecSingle = false;
          if (BoxGachaManager.m_CurrentBoxGachaStatus.IsCostShort(false))
            pinID2 = 16;
          else if (BoxGachaManager.m_CurrentBoxGachaStatus.IsBoxEmpty)
            pinID2 = 15;
          this.ActivateOutputLinks(pinID2);
          break;
        case 14:
          int pinID3 = !this.m_LastSelectExecSingle ? 13 : 12;
          if (BoxGachaManager.m_CurrentBoxGachaStatus.IsCostShort(false))
            pinID3 = 16;
          else if (BoxGachaManager.m_CurrentBoxGachaStatus.IsBoxEmpty)
            pinID3 = 15;
          this.ActivateOutputLinks(pinID3);
          break;
        case 20:
          this.ActivateOutputLinks(21);
          break;
        case 30:
          this.ActivateOutputLinks(31);
          break;
        case 40:
          if (BoxGachaManager.m_CurrentBoxGachaStatus == null)
            break;
          pinID = !BoxGachaManager.m_CurrentBoxGachaStatus.IsBoxEmpty ? 41 : 42;
          this.ActivateOutputLinks(pinID);
          break;
      }
    }

    protected override void OnDestroy()
    {
      if (BoxGachaManager.m_CurrentBoxGachaStatus != null)
        BoxGachaManager.m_CurrentBoxGachaStatus.Reset();
      BoxGachaManager.m_CurrentBoxGachaStatus = (BoxGachaManager.BoxGachaStatus) null;
    }

    private void OnEnable()
    {
      this.m_LastMultiToucheEnabled = Input.multiTouchEnabled;
      Input.multiTouchEnabled = false;
    }

    private void OnDisable()
    {
      Input.multiTouchEnabled = this.m_LastMultiToucheEnabled;
    }

    private void Refresh()
    {
      if (BoxGachaManager.m_CurrentBoxGachaStatus == null)
        return;
      if ((UnityEngine.Object) this.m_Cost != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) this.m_CostItemObj == (UnityEngine.Object) null || (UnityEngine.Object) this.m_CostCoinObj == (UnityEngine.Object) null || (UnityEngine.Object) this.m_CostGoldObj == (UnityEngine.Object) null)
        {
          SerializeValueBehaviour component = this.m_Cost.GetComponent<SerializeValueBehaviour>();
          if ((UnityEngine.Object) component == (UnityEngine.Object) null)
          {
            DebugUtility.LogError("m_CostにSerializeValueBehaviourがAttachされていません.");
            return;
          }
          this.m_CostItemObj = component.list.GetGameObject("cost_item_obj");
          if ((UnityEngine.Object) this.m_CostItemObj == (UnityEngine.Object) null)
            DebugUtility.LogError("'cost_item_obj'が登録されていません.");
          this.m_CostCoinObj = component.list.GetGameObject("cost_coin_obj");
          if ((UnityEngine.Object) this.m_CostCoinObj == (UnityEngine.Object) null)
            DebugUtility.LogError("'cost_coin_obj'が登録されていません.");
          this.m_CostGoldObj = component.list.GetGameObject("cost_gold_obj");
          if ((UnityEngine.Object) this.m_CostGoldObj == (UnityEngine.Object) null)
            DebugUtility.LogError("'cost_gold_obj'が登録されていません.");
        }
        if (BoxGachaManager.m_CurrentBoxGachaStatus.CostType == BoxGachaManager.BoxGachaCostType.Item)
        {
          ItemData data = MonoSingleton<GameManager>.Instance.Player.Items.Find((Predicate<ItemData>) (select => select.Param.iname == BoxGachaManager.m_CurrentBoxGachaStatus.CostIname));
          if (data != null)
          {
            DataSource.Bind<ItemData>(this.m_Cost, data, false);
          }
          else
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(BoxGachaManager.m_CurrentBoxGachaStatus.CostIname);
            if (itemParam == null)
            {
              DebugUtility.LogError(BoxGachaManager.m_CurrentBoxGachaStatus.CostIname + "が存在しません.");
              return;
            }
            DataSource.Bind<ItemParam>(this.m_Cost, itemParam, false);
          }
        }
        else
          DataSource.Bind<ItemData>(this.m_Cost, (ItemData) null, false);
        this.m_CostGoldObj.SetActive(BoxGachaManager.m_CurrentBoxGachaStatus.CostType == BoxGachaManager.BoxGachaCostType.Gold);
        this.m_CostCoinObj.SetActive(BoxGachaManager.m_CurrentBoxGachaStatus.CostType == BoxGachaManager.BoxGachaCostType.Coin);
        this.m_CostItemObj.SetActive(BoxGachaManager.m_CurrentBoxGachaStatus.CostType == BoxGachaManager.BoxGachaCostType.Item);
      }
      if ((UnityEngine.Object) this.m_CostResult != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) this.m_CostResultItemObj == (UnityEngine.Object) null || (UnityEngine.Object) this.m_CostResultCoinObj == (UnityEngine.Object) null || (UnityEngine.Object) this.m_CostResultGoldObj == (UnityEngine.Object) null)
        {
          SerializeValueBehaviour component = this.m_CostResult.GetComponent<SerializeValueBehaviour>();
          if ((UnityEngine.Object) component == (UnityEngine.Object) null)
          {
            DebugUtility.LogError("m_CostにSerializeValueBehaviourがAttachされていません.");
            return;
          }
          this.m_CostResultItemObj = component.list.GetGameObject("cost_item_obj");
          if ((UnityEngine.Object) this.m_CostResultItemObj == (UnityEngine.Object) null)
            DebugUtility.LogError("'cost_item_obj'が登録されていません.");
          this.m_CostResultCoinObj = component.list.GetGameObject("cost_coin_obj");
          if ((UnityEngine.Object) this.m_CostResultCoinObj == (UnityEngine.Object) null)
            DebugUtility.LogError("'cost_coin_obj'が登録されていません.");
          this.m_CostResultGoldObj = component.list.GetGameObject("cost_gold_obj");
          if ((UnityEngine.Object) this.m_CostResultGoldObj == (UnityEngine.Object) null)
            DebugUtility.LogError("'cost_gold_obj'が登録されていません.");
        }
        if (BoxGachaManager.m_CurrentBoxGachaStatus.CostType == BoxGachaManager.BoxGachaCostType.Item)
        {
          ItemData data = MonoSingleton<GameManager>.Instance.Player.Items.Find((Predicate<ItemData>) (select => select.Param.iname == BoxGachaManager.m_CurrentBoxGachaStatus.CostIname));
          if (data != null)
          {
            DataSource.Bind<ItemData>(this.m_CostResult, data, false);
          }
          else
          {
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(BoxGachaManager.m_CurrentBoxGachaStatus.CostIname);
            if (itemParam == null)
            {
              DebugUtility.LogError(BoxGachaManager.m_CurrentBoxGachaStatus.CostIname + "が存在しません.");
              return;
            }
            DataSource.Bind<ItemParam>(this.m_CostResult, itemParam, false);
          }
        }
        else
          DataSource.Bind<ItemData>(this.m_CostResult, (ItemData) null, false);
        this.m_CostResultGoldObj.SetActive(BoxGachaManager.m_CurrentBoxGachaStatus.CostType == BoxGachaManager.BoxGachaCostType.Gold);
        this.m_CostResultCoinObj.SetActive(BoxGachaManager.m_CurrentBoxGachaStatus.CostType == BoxGachaManager.BoxGachaCostType.Coin);
        this.m_CostResultItemObj.SetActive(BoxGachaManager.m_CurrentBoxGachaStatus.CostType == BoxGachaManager.BoxGachaCostType.Item);
      }
      if ((UnityEngine.Object) this.m_SingleButton != (UnityEngine.Object) null)
      {
        SerializeValueBehaviour component = this.m_SingleButton.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          Text uiLabel1 = component.list.GetUILabel("count");
          Text uiLabel2 = component.list.GetUILabel("cost");
          if ((UnityEngine.Object) uiLabel1 != (UnityEngine.Object) null)
            uiLabel1.text = BoxGachaManager.SELECT_SINGLE.ToString();
          if ((UnityEngine.Object) uiLabel2 != (UnityEngine.Object) null)
            uiLabel2.text = BoxGachaManager.m_CurrentBoxGachaStatus.CostValue.ToString();
        }
      }
      if ((UnityEngine.Object) this.m_MultiButton != (UnityEngine.Object) null)
      {
        SerializeValueBehaviour component = this.m_MultiButton.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          Text uiLabel1 = component.list.GetUILabel("count");
          Text uiLabel2 = component.list.GetUILabel("cost");
          if ((UnityEngine.Object) uiLabel1 != (UnityEngine.Object) null)
            uiLabel1.text = BoxGachaManager.m_CurrentBoxGachaStatus.MultiCount.ToString();
          if ((UnityEngine.Object) uiLabel2 != (UnityEngine.Object) null)
            uiLabel2.text = BoxGachaManager.m_CurrentBoxGachaStatus.MultiCost.ToString();
        }
      }
      if ((UnityEngine.Object) this.m_ResultButton != (UnityEngine.Object) null)
      {
        SerializeValueBehaviour component = this.m_ResultButton.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        {
          Text uiLabel1 = component.list.GetUILabel("count");
          Text uiLabel2 = component.list.GetUILabel("cost");
          if ((UnityEngine.Object) uiLabel1 != (UnityEngine.Object) null)
            uiLabel1.text = !this.m_LastSelectExecSingle ? BoxGachaManager.m_CurrentBoxGachaStatus.MultiCount.ToString() : BoxGachaManager.SELECT_SINGLE.ToString();
          if ((UnityEngine.Object) uiLabel2 != (UnityEngine.Object) null)
            uiLabel2.text = !this.m_LastSelectExecSingle ? BoxGachaManager.m_CurrentBoxGachaStatus.MultiCost.ToString() : BoxGachaManager.m_CurrentBoxGachaStatus.CostValue.ToString();
        }
      }
      if ((UnityEngine.Object) this.m_ResetButton != (UnityEngine.Object) null)
        this.m_ResetButton.interactable = BoxGachaManager.m_CurrentBoxGachaStatus.IsReset;
      if ((UnityEngine.Object) this.m_TopImage != (UnityEngine.Object) null)
      {
        GenesisChapterManager instance = GenesisChapterManager.Instance;
        GenesisChapterParam genesisChapterParam = MonoSingleton<GameManager>.Instance.GetGenesisChapterParam(instance.CurrentChapterParam.Iname);
        if (genesisChapterParam != null && instance.GenesisAssets.GachaTopImage.Length > genesisChapterParam.ChapterUiIndex)
          instance.LoadAssets<GameObject>(instance.GenesisAssets.GachaTopImage[genesisChapterParam.ChapterUiIndex], new GenesisChapterManager.LoadAssetCallback<GameObject>(this.DownLoadedPreview));
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void DownLoadedPreview(GameObject img_prefab)
    {
      if ((UnityEngine.Object) img_prefab == (UnityEngine.Object) null)
        DebugUtility.LogError("img_prefabがnullです.");
      else
        UnityEngine.Object.Instantiate<GameObject>(img_prefab, this.m_TopImage.transform);
    }

    public enum BoxGachaCostType : byte
    {
      None,
      Gold,
      Coin,
      Item,
    }

    public class BoxGachaStatus
    {
      private string iname = string.Empty;
      private string cost_iname = string.Empty;
      private List<GachaDropData> drops = new List<GachaDropData>();
      private int step;
      private int total;
      private int remain;
      private bool reset;
      private BoxGachaManager.BoxGachaCostType cost_type;
      private int cost_value;
      private int multi_count;
      private int multi_cost;

      public string Iname
      {
        get
        {
          return this.iname;
        }
      }

      public int Step
      {
        get
        {
          return this.step;
        }
      }

      public int Total
      {
        get
        {
          return this.total;
        }
      }

      public int Remain
      {
        get
        {
          return this.remain;
        }
      }

      public bool IsReset
      {
        get
        {
          return this.reset;
        }
      }

      public BoxGachaManager.BoxGachaCostType CostType
      {
        get
        {
          return this.cost_type;
        }
      }

      public string CostIname
      {
        get
        {
          return this.cost_iname;
        }
      }

      public int CostValue
      {
        get
        {
          return this.cost_value;
        }
      }

      public bool IsBoxEmpty
      {
        get
        {
          return this.remain <= 0;
        }
      }

      public int MultiCount
      {
        get
        {
          return this.multi_count;
        }
      }

      public int MultiCost
      {
        get
        {
          return this.multi_cost;
        }
      }

      public bool IsSelectMulti
      {
        get
        {
          return this.multi_count > 1;
        }
      }

      public List<GachaDropData> Drops
      {
        get
        {
          return this.drops;
        }
      }

      public void Deserialize(ReqBoxStatus.Response json)
      {
        this.iname = json.box_iname;
        this.step = json.step;
        this.total = json.total_num;
        this.remain = json.remain_num;
        this.reset = json.is_reset_enable == 1;
        if (json.cost == null)
          return;
        if (json.cost.cost_type == "item")
        {
          this.cost_type = BoxGachaManager.BoxGachaCostType.Item;
          this.cost_iname = json.cost.iname;
          if (MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.cost_iname) == null)
            DebugUtility.LogError("iname:" + this.cost_iname + "で登録されているアイテムはありません.");
        }
        else if (json.cost.cost_type == "gold")
          this.cost_type = BoxGachaManager.BoxGachaCostType.Gold;
        else if (json.cost.cost_type == "coin")
          this.cost_type = BoxGachaManager.BoxGachaCostType.Coin;
        this.cost_iname = json.cost.iname;
        this.cost_value = json.cost.num;
        int count = Mathf.Min(this.remain, BoxGachaManager.SELECT_MULTI_MAX);
        int cost = this.cost_value * count;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (this.remain <= 0)
        {
          count = BoxGachaManager.SELECT_MULTI_MAX;
          cost = this.cost_value * count;
        }
        else
          this.CalcMaxExec(ref count, ref cost);
        this.multi_count = count;
        this.multi_cost = cost;
      }

      public void Deserialize(ReqBoxExec.Response json)
      {
        this.iname = json.box_iname;
        this.step = json.step;
        this.total = json.total_num;
        this.remain = json.remain_num;
        this.reset = json.is_reset_enable == 1;
        int count = Mathf.Min(this.remain, BoxGachaManager.SELECT_MULTI_MAX);
        int cost = this.cost_value * count;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (this.remain <= 0)
        {
          count = BoxGachaManager.SELECT_MULTI_MAX;
          cost = this.cost_value * count;
        }
        else
          this.CalcMaxExec(ref count, ref cost);
        this.multi_count = count;
        this.multi_cost = cost;
        if (json.add == null || json.add.Length <= 0)
          return;
        if (this.drops == null)
          this.drops = new List<GachaDropData>();
        this.drops.Clear();
        for (int index = 0; index < json.add.Length; ++index)
        {
          GachaDropData gachaDropData = new GachaDropData();
          if (gachaDropData.Deserialize(json.add[index]))
            this.drops.Add(gachaDropData);
        }
      }

      public void Deserialize(ReqBoxReset.Response json)
      {
        if (json == null)
          return;
        this.iname = json.box_iname;
        this.step = json.step;
        this.total = json.total_num;
        this.remain = json.remain_num;
        this.reset = false;
        int count = Mathf.Min(this.remain, BoxGachaManager.SELECT_MULTI_MAX);
        int cost = this.cost_value * count;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (this.remain <= 0)
        {
          count = BoxGachaManager.SELECT_MULTI_MAX;
          cost = this.cost_value * count;
        }
        else
          this.CalcMaxExec(ref count, ref cost);
        this.multi_count = count;
        this.multi_cost = cost;
      }

      public void Reset()
      {
        this.iname = string.Empty;
        this.step = 0;
        this.total = 0;
        this.remain = 0;
        this.reset = false;
        this.cost_type = BoxGachaManager.BoxGachaCostType.None;
        this.cost_iname = string.Empty;
        this.cost_value = 0;
        this.multi_count = 0;
        this.multi_cost = 0;
        if (this.drops == null)
          return;
        this.drops.Clear();
      }

      public bool IsCostShort(bool is_multi = false)
      {
        bool flag = false;
        int num1 = !is_multi ? this.cost_value : this.multi_cost;
        int num2 = 0;
        if (this.cost_type == BoxGachaManager.BoxGachaCostType.Item)
          num2 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.cost_iname);
        else if (this.cost_type == BoxGachaManager.BoxGachaCostType.Coin)
          num2 = MonoSingleton<GameManager>.Instance.Player.Coin;
        else if (this.cost_type == BoxGachaManager.BoxGachaCostType.Gold)
          num2 = MonoSingleton<GameManager>.Instance.Player.Gold;
        if (num1 > num2)
          flag = true;
        return flag;
      }

      private void CalcMaxExec(ref int count, ref int cost)
      {
        int num = 0;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (this.cost_type == BoxGachaManager.BoxGachaCostType.Item)
          num = player.GetItemAmount(this.cost_iname);
        else if (this.cost_type == BoxGachaManager.BoxGachaCostType.Coin)
          num = player.Coin;
        else if (this.cost_type == BoxGachaManager.BoxGachaCostType.Gold)
          num = player.Gold;
        if (num > 0)
        {
          count = this.remain <= BoxGachaManager.SELECT_MULTI_MAX ? Mathf.Min(num / this.cost_value, this.remain) : Mathf.Min(num / this.cost_value, BoxGachaManager.SELECT_MULTI_MAX);
          count = count <= 0 ? BoxGachaManager.SELECT_MULTI_MAX : count;
        }
        else
          count = BoxGachaManager.SELECT_MULTI_MAX;
        cost = this.cost_value * count;
      }
    }
  }
}
