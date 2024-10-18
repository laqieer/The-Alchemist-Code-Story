// Decompiled with JetBrains decompiler
// Type: SRPG.GachaInfoRateWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GachaInfoRateWindow : FlowWindowBase
  {
    private GachaInfoRateWindow.SerializeParam m_Param;
    private bool m_Destroy;
    private GachaInfoRateWindow.RateContent.ItemSources m_RateSource;
    private ContentController m_RateController;
    private GachaInfoRateWindow.RateData m_RateData;
    private static GachaInfoRateWindow m_Instance;

    public static GachaInfoRateWindow instance
    {
      get
      {
        return GachaInfoRateWindow.m_Instance;
      }
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      GachaInfoRateWindow.m_Instance = this;
      base.Initialize(param);
      this.m_Param = param as GachaInfoRateWindow.SerializeParam;
      if (this.m_Param == null)
        return;
      if ((UnityEngine.Object) this.m_Param.rateList != (UnityEngine.Object) null)
      {
        this.m_RateController = this.m_Param.rateList.GetComponentInChildren<ContentController>();
        if ((UnityEngine.Object) this.m_RateController != (UnityEngine.Object) null)
          this.m_RateController.SetWork((object) this);
      }
      this.Close(true);
    }

    public override void Release()
    {
      this.ReleaseRateList();
      base.Release();
      GachaInfoRateWindow.m_Instance = (GachaInfoRateWindow) null;
    }

    public override int Update()
    {
      base.Update();
      if (!this.m_Destroy || !this.isClosed)
        return -1;
      this.SetActiveChild(false);
      return -1;
    }

    public void InitializeRateList()
    {
      this.ReleaseRateList();
      if (!((UnityEngine.Object) this.m_RateController != (UnityEngine.Object) null))
        return;
      this.m_RateSource = new GachaInfoRateWindow.RateContent.ItemSources();
      if (this.m_RateData != null && this.m_RateData.rates != null)
      {
        for (int index = 0; index < this.m_RateData.rates.Length; ++index)
        {
          GachaRateParam rate = this.m_RateData.rates[index];
          if (rate != null)
          {
            rate.SetCoef(this.m_RateData.rate_coef);
            GachaInfoRateWindow.RateContent.ItemSources.ItemParam itemParam = new GachaInfoRateWindow.RateContent.ItemSources.ItemParam(rate);
            if (itemParam != null && itemParam.IsValid())
              this.m_RateSource.Add(itemParam);
          }
        }
      }
      this.m_RateController.Initialize((ContentSource) this.m_RateSource, Vector2.zero);
    }

    public void ReleaseRateList()
    {
      if ((UnityEngine.Object) this.m_RateController != (UnityEngine.Object) null)
        this.m_RateController.Release();
      this.m_RateSource = (GachaInfoRateWindow.RateContent.ItemSources) null;
    }

    public override int OnActivate(int pinId)
    {
      if (pinId != 110)
        return -1;
      this.InitializeRateList();
      return 200;
    }

    public bool DeserializeRateList(Json_GachaRateParam json)
    {
      this.m_RateData = new GachaInfoRateWindow.RateData();
      this.m_RateData.Deserialize(json);
      return true;
    }

    public static class RateContent
    {
      public static GachaInfoRateWindow.RateContent.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private GachaRateParam m_Param;
        private DataSource m_DataSource;
        private SerializeValueBehaviour m_Value;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public GachaRateParam param
        {
          get
          {
            return this.m_Param;
          }
        }

        public bool IsValid
        {
          get
          {
            return this.m_Param != null;
          }
        }

        public void Setup(GachaRateParam param)
        {
          this.m_Param = param;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_DataSource = DataSource.Create(node.gameObject);
          this.m_DataSource.Add(typeof (GachaRateParam), (object) this.m_Param);
          this.m_Value = this.m_Node.GetComponent<SerializeValueBehaviour>();
          if (!((UnityEngine.Object) this.m_Value != (UnityEngine.Object) null))
            return;
          string name = this.m_Param.name;
          string empty = string.Empty;
          Sprite sprite = (Sprite) null;
          float calcRate = this.m_Param.CalcRate;
          if (this.m_Param.Type == GachaDropData.Type.Unit)
          {
            empty = this.m_Param.elem.ToString();
            sprite = GameSettings.Instance.UnitIcon_Rarity[this.m_Param.rarity];
          }
          else if (this.m_Param.Type != GachaDropData.Type.Item)
          {
            if (this.m_Param.Type == GachaDropData.Type.Artifact)
              sprite = GameSettings.Instance.ArtifactIcon_Rarity[this.m_Param.rarity];
            else if (this.m_Param.Type == GachaDropData.Type.ConceptCard)
              sprite = GameSettings.Instance.ConceptCardIcon_Rarity[this.m_Param.rarity];
          }
          this.m_Value.list.SetField("name", name);
          this.m_Value.list.SetField("element", empty);
          Image uiImage = this.m_Value.list.GetUIImage("rarity");
          if ((UnityEngine.Object) uiImage != (UnityEngine.Object) null)
            uiImage.sprite = sprite;
          this.m_Value.list.SetField("rate", calcRate.ToString("f4") + "%");
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

      public class ItemSources : ContentSource
      {
        private List<GachaInfoRateWindow.RateContent.ItemSources.ItemParam> m_Params = new List<GachaInfoRateWindow.RateContent.ItemSources.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(GachaInfoRateWindow.RateContent.ItemSources.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<GachaInfoRateWindow.RateContent.ItemSources.ItemParam, bool> predicate = (Func<GachaInfoRateWindow.RateContent.ItemSources.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<GachaInfoRateWindow.RateContent.ItemSources.ItemParam>(predicate).ToArray<GachaInfoRateWindow.RateContent.ItemSources.ItemParam>());
          else
            this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
          this.contentController.Resize(0);
          bool flag = false;
          Vector2 anchoredPosition = this.contentController.anchoredPosition;
          Vector2 lastPageAnchorePos = this.contentController.GetLastPageAnchorePos();
          if ((double) anchoredPosition.x < (double) lastPageAnchorePos.x)
          {
            flag = true;
            anchoredPosition.x = lastPageAnchorePos.x;
          }
          if ((double) anchoredPosition.y < (double) lastPageAnchorePos.y)
          {
            flag = true;
            anchoredPosition.y = lastPageAnchorePos.y;
          }
          if (flag)
            this.contentController.anchoredPosition = anchoredPosition;
          if (!((UnityEngine.Object) this.contentController.scroller != (UnityEngine.Object) null))
            return;
          this.contentController.scroller.StopMovement();
        }

        public class ItemParam : ContentSource.Param
        {
          private GachaInfoRateWindow.RateContent.ItemAccessor m_Accessor = new GachaInfoRateWindow.RateContent.ItemAccessor();

          public ItemParam(GachaRateParam param)
          {
            this.m_Accessor.Setup(param);
          }

          public GachaInfoRateWindow.RateContent.ItemAccessor accessor
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public GachaRateParam param
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

          public override bool IsValid()
          {
            return this.m_Accessor.IsValid;
          }
        }
      }
    }

    public class RateData
    {
      public GachaRateParam[] rates;
      private float m_rate_coef;

      public float rate_coef
      {
        get
        {
          return this.m_rate_coef;
        }
      }

      public bool Deserialize(Json_GachaRateParam json)
      {
        if (json == null | json.drops == null)
          return false;
        List<GachaRateParam> gachaRateParamList1 = new List<GachaRateParam>();
        List<GachaRateParam> gachaRateParamList2 = new List<GachaRateParam>();
        List<GachaRateParam> gachaRateParamList3 = new List<GachaRateParam>();
        List<GachaRateParam> gachaRateParamList4 = new List<GachaRateParam>();
        List<GachaRateParam> gachaRateParamList5 = new List<GachaRateParam>();
        for (int index = 0; index < json.drops.Length; ++index)
        {
          GachaRateParam gachaRateParam = new GachaRateParam();
          if (gachaRateParam.Deserialize(json.drops[index]))
          {
            if (gachaRateParam.Type == GachaDropData.Type.Unit)
              gachaRateParamList1.Add(gachaRateParam);
            else if (gachaRateParam.Type == GachaDropData.Type.Item)
              gachaRateParamList2.Add(gachaRateParam);
            else if (gachaRateParam.Type == GachaDropData.Type.Artifact)
              gachaRateParamList3.Add(gachaRateParam);
            else if (gachaRateParam.Type == GachaDropData.Type.ConceptCard)
              gachaRateParamList4.Add(gachaRateParam);
            this.m_rate_coef += gachaRateParam.Rate;
          }
        }
        for (int index = 0; index < gachaRateParamList1.Count; ++index)
          gachaRateParamList1[index].sortPriority = (long) index;
        gachaRateParamList1.Sort((Comparison<GachaRateParam>) ((a, b) =>
        {
          int num1 = b.rarity - a.rarity;
          if (num1 != 0)
            return num1;
          int num2 = a.elem - b.elem;
          if (num2 != 0)
            return num2;
          return a.sortPriority.CompareTo(b.sortPriority);
        }));
        for (int index = 0; index < gachaRateParamList4.Count; ++index)
          gachaRateParamList4[index].sortPriority = (long) index;
        gachaRateParamList4.Sort((Comparison<GachaRateParam>) ((a, b) =>
        {
          int num = b.rarity - a.rarity;
          if (num != 0)
            return num;
          return a.sortPriority.CompareTo(b.sortPriority);
        }));
        for (int index = 0; index < gachaRateParamList3.Count; ++index)
          gachaRateParamList3[index].sortPriority = (long) index;
        gachaRateParamList3.Sort((Comparison<GachaRateParam>) ((a, b) =>
        {
          int num = b.rarity - a.rarity;
          if (num != 0)
            return num;
          return a.sortPriority.CompareTo(b.sortPriority);
        }));
        for (int index = 0; index < gachaRateParamList2.Count; ++index)
          gachaRateParamList2[index].sortPriority = (long) index;
        gachaRateParamList2.Sort((Comparison<GachaRateParam>) ((a, b) =>
        {
          if (a.hash == b.hash)
          {
            int num = b.num - a.num;
            if (num != 0)
              return num;
          }
          return a.sortPriority.CompareTo(b.sortPriority);
        }));
        gachaRateParamList5.AddRange((IEnumerable<GachaRateParam>) gachaRateParamList1);
        gachaRateParamList5.AddRange((IEnumerable<GachaRateParam>) gachaRateParamList4);
        gachaRateParamList5.AddRange((IEnumerable<GachaRateParam>) gachaRateParamList3);
        gachaRateParamList5.AddRange((IEnumerable<GachaRateParam>) gachaRateParamList2);
        this.rates = gachaRateParamList5.ToArray();
        return true;
      }
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public GameObject rateList;

      public override System.Type type
      {
        get
        {
          return typeof (GachaInfoRateWindow);
        }
      }
    }
  }
}
