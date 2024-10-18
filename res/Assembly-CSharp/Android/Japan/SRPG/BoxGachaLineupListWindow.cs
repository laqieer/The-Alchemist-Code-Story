// Decompiled with JetBrains decompiler
// Type: SRPG.BoxGachaLineupListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BoxGachaLineupListWindow : FlowWindowBase
  {
    private BoxGachaLineupListWindow.SerializeParam m_Param;
    private bool m_Destroy;
    private BoxGachaLineupListWindow.LineupContent.ItemSource m_LineupSource;
    private ContentController m_LineupController;
    private BoxGachaLineupListWindow.LineupData m_LineupData;
    private static BoxGachaLineupListWindow m_Instance;

    public int step
    {
      get
      {
        if (this.m_LineupData != null)
          return this.m_LineupData.step;
        return 1;
      }
    }

    public static BoxGachaLineupListWindow Instance
    {
      get
      {
        return BoxGachaLineupListWindow.m_Instance;
      }
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      BoxGachaLineupListWindow.m_Instance = this;
      base.Initialize(param);
      this.m_Param = param as BoxGachaLineupListWindow.SerializeParam;
      if (this.m_Param == null)
        return;
      if ((UnityEngine.Object) this.m_Param.lineupList != (UnityEngine.Object) null)
      {
        this.m_LineupController = this.m_Param.lineupList.GetComponentInChildren<ContentController>();
        if ((UnityEngine.Object) this.m_LineupController != (UnityEngine.Object) null)
          this.m_LineupController.SetWork((object) this);
      }
      this.Close(true);
    }

    public override void Release()
    {
      base.Release();
      BoxGachaLineupListWindow.m_Instance = (BoxGachaLineupListWindow) null;
    }

    public override int Update()
    {
      base.Update();
      if (!this.m_Destroy || !this.isClosed)
        return -1;
      this.SetActiveChild(false);
      return -1;
    }

    public void InitalizeLineupList()
    {
      this.ReleaseLineupList();
      if (!((UnityEngine.Object) this.m_LineupController != (UnityEngine.Object) null))
        return;
      this.m_LineupSource = new BoxGachaLineupListWindow.LineupContent.ItemSource();
      if (this.m_LineupData != null && this.m_LineupData.box_items != null)
      {
        for (int index = 0; index < this.m_LineupData.box_items.Length; ++index)
        {
          BoxGachaLineupListWindow.LineupData.BoxItemData boxItem = this.m_LineupData.box_items[index];
          if (boxItem != null)
          {
            BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam itemParam = new BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam(boxItem);
            if (itemParam != null)
              this.m_LineupSource.Add(itemParam);
          }
        }
      }
      this.m_LineupController.Initialize((ContentSource) this.m_LineupSource, Vector2.zero);
    }

    public void ReleaseLineupList()
    {
      if ((UnityEngine.Object) this.m_LineupController != (UnityEngine.Object) null)
        this.m_LineupController.Release();
      this.m_LineupSource = (BoxGachaLineupListWindow.LineupContent.ItemSource) null;
    }

    public bool DeserializeLineupList(JSON_BoxGachaSteps json, bool is_lineup = false)
    {
      this.m_LineupData = new BoxGachaLineupListWindow.LineupData();
      this.m_LineupData.Deserialize(json, is_lineup);
      return true;
    }

    public override int OnActivate(int pinId)
    {
      if (pinId != 100)
        return -1;
      this.InitalizeLineupList();
      this.Open();
      return 101;
    }

    public static class LineupContent
    {
      public static BoxGachaLineupListWindow.LineupContent.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private BoxGachaLineupListWindow.LineupData.BoxItemData m_Param;
        private DataSource m_DataSource;
        private GameObject m_Pickup;
        private GameObject m_NormalTop;
        private GameObject m_NormalDefault;
        private GameObject m_FeatureBadge;
        private Text m_Name;
        private Text m_Remain;
        private Text m_Num;
        private GameObject m_Icon;
        private Text m_RemainAllFeture;
        private Text m_RemainAllNormal;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public BoxGachaLineupListWindow.LineupData.BoxItemData param
        {
          get
          {
            return this.m_Param;
          }
        }

        public void Setup(BoxGachaLineupListWindow.LineupData.BoxItemData param)
        {
          this.m_Param = param;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_DataSource = DataSource.Create(node.gameObject);
          this.m_DataSource.Add(typeof (BoxGachaLineupListWindow.LineupData.BoxItemData), (object) this.m_Param);
          SerializeValueBehaviour component1 = this.m_Node.GetComponent<SerializeValueBehaviour>();
          if (!((UnityEngine.Object) component1 != (UnityEngine.Object) null))
            return;
          this.m_FeatureBadge = component1.list.GetGameObject("feature_badge");
          if ((UnityEngine.Object) this.m_FeatureBadge != (UnityEngine.Object) null)
            this.m_FeatureBadge.SetActive(this.param.isFeatureItem);
          this.m_Name = component1.list.GetUILabel("name");
          if ((UnityEngine.Object) this.m_Name != (UnityEngine.Object) null)
            this.m_Name.text = this.param.name;
          this.m_Remain = component1.list.GetUILabel("remain");
          if ((UnityEngine.Object) this.m_Remain != (UnityEngine.Object) null)
          {
            string empty = string.Empty;
            string str;
            if (this.param.isLineup)
              str = this.param.total_num.ToString();
            else
              str = LocalizedText.Get("sys.TEXT_DENOMINATOR", (object) this.param.remain_num, (object) this.param.total_num);
            this.m_Remain.text = str;
          }
          this.m_Num = component1.list.GetUILabel("num");
          if ((UnityEngine.Object) this.m_Num != (UnityEngine.Object) null)
            this.m_Num.text = this.param.num.ToString();
          this.m_Pickup = component1.list.GetGameObject("pickup");
          this.m_NormalTop = component1.list.GetGameObject("item_top");
          this.m_NormalDefault = component1.list.GetGameObject("item_normal");
          if ((UnityEngine.Object) this.m_Pickup != (UnityEngine.Object) null)
            this.m_Pickup.SetActive(this.param.isFeatureItem && this.param.isFirst);
          if ((UnityEngine.Object) this.m_NormalTop != (UnityEngine.Object) null)
            this.m_NormalTop.SetActive(!this.param.isFeatureItem && this.param.isFirst);
          if ((UnityEngine.Object) this.m_NormalDefault != (UnityEngine.Object) null)
            this.m_NormalDefault.SetActive(!this.param.isFeatureItem && !this.param.isFirst || this.param.isFeatureItem && !this.param.isFirst);
          this.m_Icon = component1.list.GetGameObject("icon");
          if ((UnityEngine.Object) this.m_Icon != (UnityEngine.Object) null)
          {
            GenesisRewardIcon component2 = this.m_Icon.GetComponent<GenesisRewardIcon>();
            if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
            {
              GenesisRewardDataParam reward = this.param.reward;
              component2.Initialize(reward);
            }
          }
          this.m_RemainAllFeture = component1.list.GetUILabel("remain_all_feature");
          if ((UnityEngine.Object) this.m_RemainAllFeture != (UnityEngine.Object) null)
          {
            string empty = string.Empty;
            if (!this.param.isLineup)
              empty = LocalizedText.Get("sys.TEXT_DENOMINATOR", (object) this.param.remain_num_feature, (object) this.param.total_num_feature);
            this.m_RemainAllFeture.text = empty;
          }
          this.m_RemainAllNormal = component1.list.GetUILabel("remain_all_normal");
          if (!((UnityEngine.Object) this.m_RemainAllNormal != (UnityEngine.Object) null))
            return;
          string empty1 = string.Empty;
          if (!this.param.isLineup)
            empty1 = LocalizedText.Get("sys.TEXT_DENOMINATOR", (object) this.param.remain_num_normal, (object) this.param.total_num_normal);
          this.m_RemainAllNormal.text = empty1;
        }

        public void Clear()
        {
          if (!((UnityEngine.Object) this.m_DataSource != (UnityEngine.Object) null))
            return;
          this.m_DataSource.Clear();
          this.m_DataSource = (DataSource) null;
        }

        public void ForceUpdate()
        {
        }
      }

      public class ItemSource : ContentSource
      {
        private List<BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam> m_Params = new List<BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam param)
        {
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam, bool> predicate = (Func<BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam>(predicate).ToArray<BoxGachaLineupListWindow.LineupContent.ItemSource.ItemParam>());
          else
            this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
          this.contentController.Resize(0);
          if (!((UnityEngine.Object) this.contentController.scroller != (UnityEngine.Object) null))
            return;
          this.contentController.scroller.StopMovement();
        }

        public class ItemParam : ContentSource.Param
        {
          private BoxGachaLineupListWindow.LineupContent.ItemAccessor m_Accessor = new BoxGachaLineupListWindow.LineupContent.ItemAccessor();

          public ItemParam(BoxGachaLineupListWindow.LineupData.BoxItemData param)
          {
            this.m_Accessor.Setup(param);
          }

          public BoxGachaLineupListWindow.LineupContent.ItemAccessor accessor
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public BoxGachaLineupListWindow.LineupData.BoxItemData param
          {
            get
            {
              return this.m_Accessor.param;
            }
          }

          public override void OnEnable(ContentNode node)
          {
            this.m_Accessor.Bind(node);
            this.m_Accessor.ForceUpdate();
          }

          public override void OnDisable(ContentNode node)
          {
            this.m_Accessor.Clear();
          }

          public override void OnClick(ContentNode node)
          {
          }
        }
      }
    }

    public class LineupData
    {
      private int m_Step;
      private int m_TotalNum;
      private BoxGachaLineupListWindow.LineupData.BoxItemData[] m_BoxItems;

      public int step
      {
        get
        {
          return this.m_Step;
        }
      }

      public int total
      {
        get
        {
          return this.m_TotalNum;
        }
      }

      public BoxGachaLineupListWindow.LineupData.BoxItemData[] box_items
      {
        get
        {
          return this.m_BoxItems;
        }
      }

      public bool Deserialize(JSON_BoxGachaSteps json, bool is_lineup = false)
      {
        if (json == null)
          return false;
        this.m_Step = json.step;
        this.m_TotalNum = json.total_num;
        this.m_BoxItems = new BoxGachaLineupListWindow.LineupData.BoxItemData[json.box_items.Length];
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < json.box_items.Length; ++index)
        {
          JSON_BoxGachaItems boxItem = json.box_items[index];
          bool is_first;
          if (boxItem.is_feature_item == 1)
          {
            is_first = num1 <= 0;
            ++num1;
          }
          else
          {
            is_first = num2 <= 0;
            ++num2;
          }
          BoxGachaLineupListWindow.LineupData.BoxItemData boxItemData = new BoxGachaLineupListWindow.LineupData.BoxItemData(boxItem, is_first, is_lineup);
          if (boxItemData != null)
          {
            if (is_first)
            {
              int total = boxItem.is_feature_item != 1 ? json.total_num_normal : json.total_num_feature;
              int remain = boxItem.is_feature_item != 1 ? json.remain_num_normal : json.remain_num_feature;
              boxItemData.SetTotalNum(total, remain, boxItem.is_feature_item == 1);
            }
            this.m_BoxItems[index] = boxItemData;
          }
        }
        return true;
      }

      public class BoxItemData
      {
        private string m_Itype = string.Empty;
        private string m_Iname = string.Empty;
        private string m_BoxSetIname = string.Empty;
        private string m_BoxSetName = string.Empty;
        private string m_Name = string.Empty;
        private bool m_IsFeatureItem;
        private int m_Num;
        private int m_TotalNum;
        private int m_RemainNum;
        private int m_Coin;
        private int m_Gold;
        private bool m_IsFirst;
        private bool m_IsLineup;
        private int m_TotalNumFeature;
        private int m_TotalNumNormal;
        private int m_RemainNumFeature;
        private int m_RemainNumNormal;
        private GenesisRewardDataParam m_Reward;

        public BoxItemData(JSON_BoxGachaItems json, bool is_first = false, bool is_lineup = false)
        {
          this.Init(json, is_first, is_lineup);
        }

        public bool isFeatureItem
        {
          get
          {
            return this.m_IsFeatureItem;
          }
        }

        public string itype
        {
          get
          {
            return this.m_Itype;
          }
        }

        public string iname
        {
          get
          {
            return this.m_Iname;
          }
        }

        public int num
        {
          get
          {
            return this.m_Num;
          }
        }

        public int total_num
        {
          get
          {
            return this.m_TotalNum;
          }
        }

        public int remain_num
        {
          get
          {
            return this.m_RemainNum;
          }
        }

        public int coin
        {
          get
          {
            return this.m_Coin;
          }
        }

        public int gold
        {
          get
          {
            return this.m_Gold;
          }
        }

        public string box_set_iname
        {
          get
          {
            return this.m_BoxSetIname;
          }
        }

        public string box_set_name
        {
          get
          {
            return this.m_BoxSetName;
          }
        }

        public bool isFirst
        {
          get
          {
            return this.m_IsFirst;
          }
        }

        public GenesisRewardDataParam reward
        {
          get
          {
            return this.m_Reward;
          }
        }

        public string name
        {
          get
          {
            return this.m_Name;
          }
        }

        public bool isLineup
        {
          get
          {
            return this.m_IsLineup;
          }
        }

        public int total_num_feature
        {
          get
          {
            return this.m_TotalNumFeature;
          }
        }

        public int total_num_normal
        {
          get
          {
            return this.m_TotalNumNormal;
          }
        }

        public int remain_num_feature
        {
          get
          {
            return this.m_RemainNumFeature;
          }
        }

        public int remain_num_normal
        {
          get
          {
            return this.m_RemainNumNormal;
          }
        }

        private void Init(JSON_BoxGachaItems json, bool is_first = false, bool is_lineup = false)
        {
          if (json == null)
            return;
          this.m_IsFeatureItem = json.is_feature_item == 1;
          this.m_Itype = json.itype;
          this.m_Iname = json.iname;
          this.m_Num = json.num;
          this.m_TotalNum = json.total_num;
          this.m_RemainNum = json.remain_num;
          this.m_Coin = json.coin;
          this.m_Gold = json.gold;
          this.m_BoxSetIname = json.box_set_iname;
          this.m_BoxSetName = json.box_set_name;
          this.m_IsFirst = is_first;
          this.m_IsLineup = is_lineup;
          this.m_Reward = new GenesisRewardDataParam();
          this.m_Reward.ItemIname = this.m_Iname;
          this.m_Reward.ItemNum = this.m_Num;
          if (json.itype == "item" || json.itype == "lineup_set")
          {
            this.m_Reward.ItemType = 0;
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(json.iname);
            if (itemParam == null)
              DebugUtility.LogError("iname:" + json.iname + "はItemParamには存在しません");
            else
              this.m_Name = itemParam.name;
          }
          else if (json.itype == "unit")
          {
            this.m_Reward.ItemType = 4;
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(json.iname);
            if (unitParam == null)
              DebugUtility.LogError("iname:" + json.iname + "はUnitParamには存在しません");
            else
              this.m_Name = unitParam.name;
          }
          else if (json.itype == "artifact")
          {
            this.m_Reward.ItemType = 6;
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(json.iname);
            if (artifactParam == null)
              DebugUtility.LogError("iname:" + json.iname + "はArtifactParamには存在しません");
            else
              this.m_Name = artifactParam.name;
          }
          else if (json.itype == "concept_card")
          {
            this.m_Reward.ItemType = 5;
            ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
            if (conceptCardParam == null)
              DebugUtility.LogError("iname:" + json.iname + "はConceptCardParamには存在しません");
            else
              this.m_Name = conceptCardParam.name;
          }
          else if (json.coin > 0)
          {
            this.m_Reward.ItemType = 2;
            this.m_Name = LocalizedText.Get("sys.COIN");
            this.m_Num = this.m_Coin;
          }
          else
          {
            if (json.gold <= 0)
              return;
            this.m_Reward.ItemType = 1;
            this.m_Name = LocalizedText.Get("sys.GOLD");
            this.m_Num = this.m_Gold;
          }
        }

        public void SetTotalNum(int total, int remain, bool is_feature = false)
        {
          if (is_feature)
          {
            this.m_TotalNumFeature = total;
            this.m_RemainNumFeature = remain;
          }
          else
          {
            this.m_TotalNumNormal = total;
            this.m_RemainNumNormal = remain;
          }
        }
      }
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public GameObject lineupList;

      public override System.Type type
      {
        get
        {
          return typeof (BoxGachaLineupListWindow);
        }
      }
    }
  }
}
