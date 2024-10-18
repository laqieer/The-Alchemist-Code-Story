// Decompiled with JetBrains decompiler
// Type: SRPG.ContentController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [ExecuteInEditMode]
  public class ContentController : MonoBehaviour
  {
    public bool m_WidthLoop;
    public bool m_HeightLoop;
    public ContentScroller m_Scroller;
    public ContentNode m_Node;
    public float m_PaddingLeft;
    public float m_PaddingRight;
    public float m_PaddingTop;
    public float m_PaddingBottom;
    public Vector2 m_CellSize = Vector2.zero;
    public Vector2 m_Spacing = Vector2.zero;
    public ContentController.Constraint m_Constraint;
    public int m_ConstraintCount;
    private RectTransform m_RectTransform;
    private ContentSource m_Source;
    private bool m_NodeStatic;
    private Dictionary<string, ContentNode> m_NodeUsed = new Dictionary<string, ContentNode>();
    private List<ContentNode> m_NodeEmpty = new List<ContentNode>();
    private Vector2 m_PageSize = Vector2.zero;
    private int m_NodeWidthNum = 1;
    private int m_NodeHeightNum = 1;
    private int m_ViewWidthNum = 1;
    private int m_ViewHeightNum = 1;
    private int m_SelectNode = -1;
    private float m_MoveRefreshTime;
    private bool m_MoveRefresh;
    private object m_Work;
    public Vector2 _test = Vector2.zero;

    public ContentScroller scroller => this.m_Scroller;

    public bool isScrollHorizontal => this.m_Scroller.horizontal;

    public bool isScrollVertical => this.m_Scroller.vertical;

    public Vector2 anchoredPosition
    {
      set => this.m_RectTransform.anchoredPosition = value;
      get
      {
        return Object.op_Inequality((Object) this.m_RectTransform, (Object) null) ? this.m_RectTransform.anchoredPosition : Vector2.zero;
      }
    }

    protected virtual void Awake()
    {
      this.m_RectTransform = ((Component) this).gameObject.GetComponent<RectTransform>();
      if (Object.op_Equality((Object) this.m_Scroller, (Object) null))
        this.m_Scroller = ((Component) this).gameObject.GetComponentInParent<ContentScroller>();
      if (Object.op_Inequality((Object) this.m_Node, (Object) null))
      {
        this.m_NodeStatic = false;
        ((Component) this.m_Node).gameObject.SetActive(false);
      }
      else
      {
        this.m_NodeStatic = true;
        this.Initialize((ContentSource) null, Vector2.zero);
      }
    }

    public virtual void Initialize(ContentSource source, Vector2 pos)
    {
      this.InitializeParam();
      if (!this.m_NodeStatic)
      {
        this.anchoredPosition = pos;
        if (source != null)
          this.SetCurrentSource(source);
        this.Resize();
        this.CreateNode();
      }
      else
      {
        List<ContentNode> nodeChilds = this.GetNodeChilds();
        this.anchoredPosition = pos;
        this.Resize(nodeChilds.Count);
        this.CreateStaticNode(nodeChilds);
      }
    }

    private void InitializeParam()
    {
      if (Object.op_Equality((Object) this.m_RectTransform, (Object) null))
        this.m_RectTransform = ((Component) this).gameObject.GetComponent<RectTransform>();
      if (Object.op_Equality((Object) this.m_Scroller, (Object) null))
        this.m_Scroller = ((Component) this).gameObject.GetComponentInParent<ContentScroller>();
      if (!Object.op_Inequality((Object) this.m_RectTransform, (Object) null))
        return;
      this.m_RectTransform.anchorMin = new Vector2(0.0f, 1f);
      this.m_RectTransform.anchorMax = new Vector2(0.0f, 1f);
      this.m_RectTransform.pivot = new Vector2(0.0f, 1f);
      if (Object.op_Inequality((Object) this.m_Scroller, (Object) null))
      {
        ref Vector2 local1 = ref this.m_PageSize;
        Rect rect1 = this.m_Scroller.viewport.rect;
        double width = (double) ((Rect) ref rect1).width;
        local1.x = (float) width;
        ref Vector2 local2 = ref this.m_PageSize;
        Rect rect2 = this.m_Scroller.viewport.rect;
        double height = (double) ((Rect) ref rect2).height;
        local2.y = (float) height;
      }
      else
      {
        ref Vector2 local3 = ref this.m_PageSize;
        Rect rect3 = this.m_RectTransform.rect;
        double width = (double) ((Rect) ref rect3).width;
        local3.x = (float) width;
        ref Vector2 local4 = ref this.m_PageSize;
        Rect rect4 = this.m_RectTransform.rect;
        double height = (double) ((Rect) ref rect4).height;
        local4.y = (float) height;
      }
      Vector2 pageSize = this.m_PageSize;
      if (this.m_Constraint == ContentController.Constraint.Flexible)
      {
        pageSize.x -= this.m_PaddingLeft + this.m_PaddingRight;
        pageSize.y -= this.m_PaddingTop + this.m_PaddingBottom;
        this.m_NodeWidthNum = Mathf.CeilToInt((float) (((double) pageSize.x + (double) this.m_Spacing.x) / ((double) this.m_CellSize.x + (double) this.m_Spacing.x)));
        this.m_NodeHeightNum = Mathf.CeilToInt((float) (((double) pageSize.y + (double) this.m_Spacing.y) / ((double) this.m_CellSize.y + (double) this.m_Spacing.y)));
      }
      else if (this.m_Constraint == ContentController.Constraint.FixedColumnCount)
      {
        pageSize.y -= this.m_PaddingTop + this.m_PaddingBottom;
        this.m_NodeWidthNum = this.m_ConstraintCount;
        this.m_NodeHeightNum = Mathf.CeilToInt((float) (((double) pageSize.y + (double) this.m_Spacing.y) / ((double) this.m_CellSize.y + (double) this.m_Spacing.y)));
      }
      else
      {
        if (this.m_Constraint != ContentController.Constraint.FixedRowCount)
          return;
        pageSize.x -= this.m_PaddingLeft + this.m_PaddingRight;
        this.m_NodeWidthNum = Mathf.CeilToInt((float) (((double) pageSize.x + (double) this.m_Spacing.x) / ((double) this.m_CellSize.x + (double) this.m_Spacing.x)));
        this.m_NodeHeightNum = this.m_ConstraintCount;
      }
    }

    public void ForceUpdate()
    {
      this.UpdateNode();
      if (!Object.op_Inequality((Object) this.scroller, (Object) null))
        return;
      Vector2 normalizedPosition = this.scroller.normalizedPosition;
      normalizedPosition.x = Mathf.Clamp01(normalizedPosition.x);
      normalizedPosition.y = Mathf.Clamp01(normalizedPosition.y);
      this.scroller.normalizedPosition = normalizedPosition;
    }

    public virtual void Release()
    {
      if (this.m_Source != null)
      {
        this.m_Source.Release();
        this.m_Source = (ContentSource) null;
      }
      this.DestroyNode();
    }

    protected virtual void Update()
    {
      this.UpdateNode();
      if (this.m_Source == null)
        return;
      this.m_Source.Update();
    }

    private void LateUpdate()
    {
      if (Input.touchCount <= 0)
      {
        if (this.m_MoveRefresh)
          return;
        Vector2 velocity = this.m_Scroller.velocity;
        if ((double) ((Vector2) ref velocity).magnitude >= 0.0099999997764825821)
          return;
        this.m_MoveRefreshTime += Time.deltaTime;
        if ((double) this.m_MoveRefreshTime <= 0.10000000149011612)
          return;
        this.m_MoveRefresh = true;
        this.MoveRefresh();
        this.m_Scroller.StopMovement();
      }
      else
      {
        this.m_MoveRefresh = false;
        this.m_MoveRefreshTime = 0.0f;
      }
    }

    private void UpdateNode()
    {
      if (!this.m_NodeStatic)
      {
        this.CheckActiveNode();
      }
      else
      {
        List<ContentNode> nodeChilds = this.GetNodeChilds();
        if (nodeChilds.Count != this.m_NodeUsed.Count)
        {
          this.Resize(nodeChilds.Count);
          this.CreateStaticNode(nodeChilds);
        }
      }
      RectTransform viewport = this.m_Scroller.viewport;
      if (!Object.op_Inequality((Object) viewport, (Object) null))
        return;
      Rect rect = viewport.rect;
      IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.m_NodeUsed.GetEnumerator();
      while (enumerator.MoveNext())
      {
        ContentNode contentNode = (ContentNode) enumerator.Value;
        if (Object.op_Inequality((Object) contentNode, (Object) null))
        {
          Vector2 vector2 = Vector2.op_Implicit(((Transform) viewport).InverseTransformPoint(((Transform) contentNode.rectTransform).position));
          vector2 = contentNode.GetPivotAnchoredPosition(vector2);
          float num1 = (float) ((double) vector2.x - (double) contentNode.sizeX * 0.5 + 2.5);
          float num2 = (float) ((double) vector2.x + (double) contentNode.sizeX * 0.5 - 2.5);
          float num3 = (float) ((double) vector2.y + (double) contentNode.sizeY * 0.5 - 2.5);
          float num4 = (float) ((double) vector2.y - (double) contentNode.sizeY * 0.5 + 2.5);
          if ((double) num2 > (double) ((Rect) ref rect).x && (double) num1 < (double) ((Rect) ref rect).x + (double) ((Rect) ref rect).width && (double) num3 > (double) ((Rect) ref rect).y && (double) num4 < (double) ((Rect) ref rect).y + (double) ((Rect) ref rect).height)
            contentNode.OnViewIn(vector2);
          else
            contentNode.OnViewOut(vector2);
          if (contentNode.index == this.m_SelectNode)
            contentNode.OnSelectOn();
          else
            contentNode.OnSelectOff();
        }
      }
    }

    public bool MoveRefresh()
    {
      Vector2 anchoredPosition = this.anchoredPosition;
      bool flag = false;
      if (this.m_WidthLoop)
      {
        float num = this.m_RectTransform.sizeDelta.x - (this.m_PaddingLeft + this.m_PaddingRight);
        if ((double) anchoredPosition.x > (double) this.m_PageSize.x)
        {
          while ((double) anchoredPosition.x > 0.0)
            anchoredPosition.x -= num;
          flag = true;
        }
        else if ((double) anchoredPosition.x < -(double) this.m_RectTransform.sizeDelta.x)
        {
          while ((double) anchoredPosition.x < -(double) num)
            anchoredPosition.x += num;
          flag = true;
        }
      }
      if (this.m_HeightLoop)
      {
        float num = this.m_RectTransform.sizeDelta.y - (this.m_PaddingTop + this.m_PaddingBottom);
        if ((double) anchoredPosition.y > (double) this.m_PageSize.y)
        {
          while ((double) anchoredPosition.y > 0.0)
            anchoredPosition.y -= num;
          flag = true;
        }
        else if ((double) anchoredPosition.y < -(double) this.m_RectTransform.sizeDelta.y)
        {
          while ((double) anchoredPosition.y < -(double) num)
            anchoredPosition.y += num;
          flag = true;
        }
      }
      if (flag)
      {
        this.anchoredPosition = anchoredPosition;
        this.UpdateNode();
      }
      return flag;
    }

    public void CreateStaticNode(List<ContentNode> list)
    {
      this.DestroyNode();
      for (int index = 0; index < list.Count; ++index)
      {
        ContentNode contentNode = list[index];
        int x = index % this.m_ViewWidthNum;
        int y = index / this.m_ViewWidthNum;
        int paramIndex = this.GetParamIndex(x, y);
        contentNode.Initialize(this);
        contentNode.Setup(paramIndex, x, y, this.GetParam(paramIndex));
        this.m_NodeUsed.Add(this.GetNodeKey(x, y), contentNode);
      }
      this.UpdateNode();
      this.m_SelectNode = -1;
    }

    public void CreateNode()
    {
      if (Object.op_Equality((Object) this.m_Node, (Object) null))
      {
        Debug.LogError((object) ("ベースノードが設定されていません > " + ((Object) ((Component) this).gameObject).name));
      }
      else
      {
        this.DestroyNode();
        int nodeWidthNum = this.m_NodeWidthNum;
        int nodeHeightNum = this.m_NodeHeightNum;
        if (this.isScrollHorizontal)
          nodeWidthNum += 2;
        if (this.isScrollVertical)
          nodeHeightNum += 2;
        for (int index1 = 0; index1 < nodeHeightNum; ++index1)
        {
          for (int index2 = 0; index2 < nodeWidthNum; ++index2)
          {
            ContentNode contentNode = (ContentNode) null;
            if (this.m_Source != null)
            {
              contentNode = this.m_Source.Instantiate(this.m_Node);
            }
            else
            {
              GameObject gameObject = Object.Instantiate<GameObject>(((Component) this.m_Node).gameObject);
              if (Object.op_Inequality((Object) gameObject, (Object) null))
                contentNode = gameObject.GetComponent<ContentNode>();
            }
            if (Object.op_Inequality((Object) contentNode, (Object) null))
            {
              contentNode.Initialize(this);
              ((Component) contentNode).gameObject.transform.SetParent((Transform) this.m_RectTransform, false);
              ((Component) contentNode).gameObject.SetActive(false);
              this.m_NodeEmpty.Add(contentNode);
            }
          }
        }
        this.UpdateNode();
        this.m_SelectNode = -1;
      }
    }

    public void DestroyNode()
    {
      for (int index = 0; index < this.m_NodeEmpty.Count; ++index)
      {
        ContentNode contentNode = this.m_NodeEmpty[index];
        if (Object.op_Inequality((Object) contentNode, (Object) null))
        {
          contentNode.Release();
          Object.Destroy((Object) ((Component) contentNode).gameObject);
        }
      }
      this.m_NodeEmpty.Clear();
      IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.m_NodeUsed.GetEnumerator();
      while (enumerator.MoveNext())
      {
        ContentNode contentNode = (ContentNode) enumerator.Value;
        if (Object.op_Inequality((Object) contentNode, (Object) null))
        {
          contentNode.Release();
          if (!this.m_NodeStatic)
            Object.Destroy((Object) ((Component) contentNode).gameObject);
        }
      }
      this.m_NodeUsed.Clear();
    }

    private void CheckActiveNode()
    {
      ContentGrid grid = this.GetGrid();
      List<ContentNode> contentNodeList = new List<ContentNode>();
      IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.m_NodeUsed.GetEnumerator();
      while (enumerator.MoveNext())
      {
        ContentNode contentNode = (ContentNode) enumerator.Value;
        if (Object.op_Inequality((Object) contentNode, (Object) null) && (contentNode.IsReMake() || !contentNode.IsValid() || contentNode.gridX < grid.x - 1 || contentNode.gridX > grid.x + this.m_NodeWidthNum || contentNode.gridY < grid.y - 1 || contentNode.gridY > grid.y + this.m_NodeHeightNum))
          contentNodeList.Add(contentNode);
      }
      for (int index = 0; index < contentNodeList.Count; ++index)
      {
        ContentNode contentNode = contentNodeList[index];
        if (Object.op_Inequality((Object) contentNode, (Object) null))
        {
          contentNode.OnSelectOff();
          contentNode.SetActive(false);
          this.m_NodeUsed.Remove(this.GetNodeKey(contentNode.gridX, contentNode.gridY));
          this.m_NodeEmpty.Add(contentNode);
        }
      }
      int x1 = grid.x;
      int y1 = grid.y;
      int num1 = grid.x + this.m_NodeWidthNum;
      int num2 = grid.y + this.m_NodeHeightNum;
      if (this.isScrollHorizontal)
      {
        --x1;
        ++num1;
      }
      if (this.isScrollVertical)
      {
        --y1;
        ++num2;
      }
      for (int y2 = y1; y2 < num2; ++y2)
      {
        for (int x2 = x1; x2 < num1; ++x2)
        {
          if ((this.m_WidthLoop || x2 >= 0 && x2 < this.m_ViewWidthNum) && (this.m_HeightLoop || y2 >= 0 && y2 < this.m_ViewHeightNum) && Object.op_Equality((Object) this.GetNodeUsed(x2, y2), (Object) null))
          {
            int paramIndex = this.GetParamIndex(x2, y2);
            ContentSource.Param obj = this.GetParam(paramIndex);
            if (obj != null)
            {
              ContentNode nodeEmpty = this.GetNodeEmpty();
              if (Object.op_Inequality((Object) nodeEmpty, (Object) null))
              {
                nodeEmpty.Setup(paramIndex, x2, y2, obj);
                nodeEmpty.SetActive(true);
                this.m_NodeUsed.Add(this.GetNodeKey(x2, y2), nodeEmpty);
              }
              else
                Debug.LogError((object) "ノードが不足しています");
            }
          }
        }
      }
    }

    private string GetNodeKey(int x, int y) => x.ToString() + ":" + (object) y;

    private ContentNode GetNodeUsed(int x, int y)
    {
      ContentNode nodeUsed = (ContentNode) null;
      this.m_NodeUsed.TryGetValue(this.GetNodeKey(x, y), out nodeUsed);
      return nodeUsed;
    }

    private ContentNode GetNodeEmpty()
    {
      if (this.m_NodeEmpty.Count <= 0)
        return (ContentNode) null;
      ContentNode nodeEmpty = this.m_NodeEmpty[0];
      this.m_NodeEmpty.RemoveAt(0);
      return nodeEmpty;
    }

    public int GetNodeCount() => this.m_NodeEmpty.Count + this.m_NodeUsed.Count;

    public List<ContentNode> GetNodeAll()
    {
      List<ContentNode> nodeAll = new List<ContentNode>();
      nodeAll.AddRange((IEnumerable<ContentNode>) this.m_NodeEmpty);
      nodeAll.AddRange((IEnumerable<ContentNode>) this.m_NodeUsed.Values.ToArray<ContentNode>());
      return nodeAll;
    }

    public List<ContentNode> GetNodeChilds()
    {
      List<ContentNode> nodeChilds = new List<ContentNode>();
      for (int index = 0; index < ((Component) this).transform.childCount; ++index)
      {
        Transform child = ((Component) this).transform.GetChild(index);
        if (Object.op_Inequality((Object) child, (Object) null) && ((Component) child).gameObject.activeSelf)
        {
          ContentNode component = ((Component) child).GetComponent<ContentNode>();
          if (Object.op_Inequality((Object) component, (Object) null))
            nodeChilds.Add(component);
        }
      }
      return nodeChilds;
    }

    public int GetParamIndex(int x, int y)
    {
      ContentGrid normalizeGrid = this.GetNormalizeGrid(x, y);
      return this.m_Constraint == ContentController.Constraint.FixedRowCount ? normalizeGrid.x * this.m_ViewHeightNum + normalizeGrid.y : normalizeGrid.y * this.m_ViewWidthNum + normalizeGrid.x;
    }

    public ContentSource.Param GetParam(int x, int y)
    {
      return this.m_Source != null ? this.m_Source.GetParam(this.GetParamIndex(x, y)) : (ContentSource.Param) null;
    }

    public ContentSource.Param GetParam(int index)
    {
      return this.m_Source != null ? this.m_Source.GetParam(index) : (ContentSource.Param) null;
    }

    public ContentGrid GetLastPageGrid()
    {
      Vector2 lastPageAnchorePos = this.GetLastPageAnchorePos();
      if ((double) lastPageAnchorePos.x < 0.0)
        lastPageAnchorePos.x -= this.m_CellSize.x * 0.5f;
      else
        lastPageAnchorePos.x += this.m_CellSize.x * 0.5f;
      if ((double) lastPageAnchorePos.y < 0.0)
        lastPageAnchorePos.y -= this.m_CellSize.y * 0.5f;
      else
        lastPageAnchorePos.y += this.m_CellSize.y * 0.5f;
      return new ContentGrid((float) (-(double) lastPageAnchorePos.x / ((double) this.m_CellSize.x + (double) this.m_Spacing.x)), lastPageAnchorePos.y / (this.m_CellSize.y + this.m_Spacing.y));
    }

    public ContentGrid GetGrid()
    {
      return Object.op_Inequality((Object) this.m_RectTransform, (Object) null) ? this.GetGrid(this.anchoredPosition) : ContentGrid.zero;
    }

    public ContentGrid GetGrid(Vector2 pos)
    {
      ContentGrid zero = ContentGrid.zero;
      if (Object.op_Inequality((Object) this.m_RectTransform, (Object) null))
      {
        pos.x += this.m_PaddingLeft - this.m_Spacing.x;
        zero.fx = (float) (-(double) pos.x / ((double) this.m_CellSize.x + (double) this.m_Spacing.x));
        pos.y -= this.m_PaddingTop - this.m_Spacing.y;
        zero.fy = pos.y / (this.m_CellSize.y + this.m_Spacing.y);
        if (!this.m_WidthLoop && zero.x < 0)
          zero.x = 0;
        if (!this.m_HeightLoop && zero.y < 0)
          zero.y = 0;
      }
      return zero;
    }

    public ContentGrid GetGrid(int index)
    {
      ContentGrid zero = ContentGrid.zero;
      if (this.m_Constraint == ContentController.Constraint.Flexible || this.m_Constraint == ContentController.Constraint.FixedRowCount)
      {
        zero.x = index / this.m_ViewHeightNum;
        zero.y = index % this.m_ViewHeightNum;
      }
      else if (this.m_Constraint == ContentController.Constraint.FixedColumnCount)
      {
        zero.x = index % this.m_ViewWidthNum;
        zero.y = index / this.m_ViewWidthNum;
      }
      return zero;
    }

    public ContentGrid GetNormalizeGrid(int x, int y)
    {
      if (x < 0)
        x = (this.m_ViewWidthNum - x % this.m_ViewWidthNum) % this.m_ViewWidthNum;
      else
        x %= this.m_ViewWidthNum;
      if (y < 0)
        y = (this.m_ViewHeightNum - y % this.m_ViewHeightNum) % this.m_ViewHeightNum;
      else
        y %= this.m_ViewHeightNum;
      return new ContentGrid(x, y);
    }

    public Vector2 GetNodePos(int x, int y)
    {
      return new Vector2(this.m_PaddingLeft + (float) x * (this.m_CellSize.x + this.m_Spacing.x), (float) -((double) this.m_PaddingTop + (double) y * ((double) this.m_CellSize.y + (double) this.m_Spacing.y)));
    }

    public Vector2 GetLastPageAnchorePos()
    {
      Vector2 sizeDelta = this.m_RectTransform.sizeDelta;
      sizeDelta.x -= this.m_PageSize.x;
      if ((double) sizeDelta.x < 0.0)
        sizeDelta.x = 0.0f;
      sizeDelta.y -= this.m_PageSize.y;
      if ((double) sizeDelta.y < 0.0)
        sizeDelta.y = 0.0f;
      return new Vector2(-sizeDelta.x, sizeDelta.y);
    }

    public Vector2 GetAnchorePosFromGrid(int x, int y)
    {
      Vector2 zero = Vector2.zero;
      if (this.isScrollHorizontal && x >= 0)
        zero.x = (float) -((double) x * ((double) this.m_CellSize.x + (double) this.m_Spacing.x)) - this.m_PaddingLeft;
      if (this.isScrollVertical && y >= 0)
        zero.y = (float) y * (this.m_CellSize.y + this.m_Spacing.y) + this.m_PaddingTop;
      return zero;
    }

    public ContentNode GetNode(Vector2 screenPos) => (ContentNode) null;

    public void Resize(int count = 0)
    {
      if (count == 0)
      {
        if (this.m_Source == null || Object.op_Equality((Object) this.m_RectTransform, (Object) null))
          return;
        count = this.m_Source.GetCount();
      }
      Vector2 pageSize = this.m_PageSize;
      if (this.m_Constraint == ContentController.Constraint.Flexible)
      {
        this.m_ViewWidthNum = Mathf.CeilToInt((float) count / (float) this.m_NodeHeightNum);
        this.m_ViewHeightNum = this.m_NodeHeightNum;
      }
      else if (this.m_Constraint == ContentController.Constraint.FixedColumnCount)
      {
        this.m_ViewWidthNum = this.m_ConstraintCount;
        this.m_ViewHeightNum = Mathf.CeilToInt((float) count / (float) this.m_ConstraintCount);
      }
      else if (this.m_Constraint == ContentController.Constraint.FixedRowCount)
      {
        this.m_ViewWidthNum = Mathf.CeilToInt((float) count / (float) this.m_ConstraintCount);
        this.m_ViewHeightNum = this.m_ConstraintCount;
      }
      pageSize.x = (float) this.m_ViewWidthNum * (this.m_CellSize.x + this.m_Spacing.x) + this.m_PaddingLeft + this.m_PaddingRight;
      pageSize.y = (float) this.m_ViewHeightNum * (this.m_CellSize.y + this.m_Spacing.y) + this.m_PaddingTop + this.m_PaddingBottom;
      this.m_RectTransform.sizeDelta = pageSize;
    }

    public void SetCurrentSource(ContentSource source)
    {
      if (this.m_Source != null)
        this.m_Source.Release();
      this.m_Source = source;
      if (this.m_Source == null)
        return;
      this.m_Source.Initialize(this);
    }

    public ContentSource GetCurrentSource() => this.m_Source;

    public void SetWork(object value) => this.m_Work = value;

    public object GetWork() => this.m_Work;

    public void SetSelect(int index) => this.m_SelectNode = index;

    public int GetSelect() => this.m_SelectNode;

    public void SetSpacing(Vector2 value) => this.m_Spacing = value;

    public Vector2 GetSpacing() => this.m_Spacing;

    public Vector2 GetAnchorePos()
    {
      return Object.op_Inequality((Object) this.m_RectTransform, (Object) null) ? this.anchoredPosition : Vector2.zero;
    }

    public enum Constraint
    {
      Flexible,
      FixedColumnCount,
      FixedRowCount,
    }
  }
}
