// Decompiled with JetBrains decompiler
// Type: SRPG.FlexibleLayoutGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  [RequireComponent(typeof (RectTransform))]
  public class FlexibleLayoutGroup : UIBehaviour, ILayoutElement, ILayoutGroup, ILayoutController
  {
    [SerializeField]
    private RectOffset m_Padding;
    [SerializeField]
    private Vector2 m_Spacing;
    [SerializeField]
    private FlexibleLayoutGroup.Axis m_StartAxis;
    [SerializeField]
    [HideInInspector]
    private float m_LineSpace;
    [SerializeField]
    [HideInInspector]
    private float m_ColumnSpace;
    private List<RectTransform> m_RectChildren = new List<RectTransform>();
    private Vector2 m_TotalMinSize = Vector2.zero;
    private Vector2 m_TotalPreferredSize = Vector2.zero;
    private Vector2 m_TotalFlexibleSize = Vector2.zero;
    private RectTransform m_RectTransform;

    protected FlexibleLayoutGroup()
    {
      if (this.m_Padding != null)
        return;
      this.m_Padding = new RectOffset();
    }

    private List<RectTransform> rectChildren
    {
      get
      {
        if (this.m_RectChildren == null)
          this.m_RectChildren = new List<RectTransform>();
        return this.m_RectChildren;
      }
    }

    private RectTransform rectTransform
    {
      get
      {
        if (Object.op_Equality((Object) this.m_RectTransform, (Object) null))
          this.m_RectTransform = ((Component) this).GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    private bool isRootLayoutGroup
    {
      get
      {
        return Object.op_Equality((Object) ((Component) this).transform.parent, (Object) null) || Object.op_Equality((Object) ((Component) ((Component) this).transform.parent).GetComponent(typeof (ILayoutGroup)), (Object) null);
      }
    }

    public virtual float minWidth => this.GetTotalMinSize(0);

    public virtual float preferredWidth => this.GetTotalPreferredSize(0);

    public virtual float flexibleWidth => this.GetTotalFlexibleSize(0);

    public virtual float minHeight => this.GetTotalMinSize(1);

    public virtual float preferredHeight => this.GetTotalPreferredSize(1);

    public virtual float flexibleHeight => this.GetTotalFlexibleSize(1);

    public virtual int layoutPriority => 0;

    protected float GetTotalMinSize(int axis) => ((Vector2) ref this.m_TotalMinSize)[axis];

    protected float GetTotalPreferredSize(int axis)
    {
      return ((Vector2) ref this.m_TotalPreferredSize)[axis];
    }

    protected float GetTotalFlexibleSize(int axis)
    {
      return ((Vector2) ref this.m_TotalFlexibleSize)[axis];
    }

    public virtual void CalculateLayoutInputHorizontal()
    {
      this.rectChildren.Clear();
      for (int index = 0; index < ((Transform) this.rectTransform).childCount; ++index)
      {
        RectTransform child = ((Transform) this.rectTransform).GetChild(index) as RectTransform;
        if (((Component) child).gameObject.activeInHierarchy)
        {
          ILayoutIgnorer component = ((Component) child).gameObject.GetComponent<ILayoutIgnorer>();
          if (component == null || !component.ignoreLayout)
            this.rectChildren.Add(child);
        }
      }
    }

    public virtual void CalculateLayoutInputVertical()
    {
    }

    public virtual void SetLayoutHorizontal() => this.UpdateLayout();

    public virtual void SetLayoutVertical() => this.UpdateLayout();

    private void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
    {
      if (Object.op_Equality((Object) rect, (Object) null))
        return;
      rect.SetInsetAndSizeFromParentEdge(axis != 0 ? (RectTransform.Edge) 2 : (RectTransform.Edge) 0, pos, size);
    }

    private void UpdateLayout()
    {
      Bounds rectTransformBounds1 = RectTransformUtility.CalculateRelativeRectTransformBounds((Transform) this.rectTransform);
      ref Bounds local1 = ref rectTransformBounds1;
      ((Bounds) ref local1).min = Vector3.op_Addition(((Bounds) ref local1).min, new Vector3((float) this.m_Padding.left, (float) this.m_Padding.bottom));
      ref Bounds local2 = ref rectTransformBounds1;
      ((Bounds) ref local2).max = Vector3.op_Subtraction(((Bounds) ref local2).max, new Vector3((float) this.m_Padding.right, (float) this.m_Padding.top));
      Vector2 sizeDelta1 = this.rectTransform.sizeDelta;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector((float) this.m_Padding.left, (float) this.m_Padding.top);
      Vector2 vector2_2 = vector2_1;
      for (int index = 0; index < this.rectChildren.Count; ++index)
      {
        Vector2 sizeDelta2 = this.rectChildren[index].sizeDelta;
        this.SetChildAlongAxis(this.rectChildren[index], 0, ((Vector2) ref vector2_2)[0], ((Vector2) ref sizeDelta2)[0]);
        this.SetChildAlongAxis(this.rectChildren[index], 1, ((Vector2) ref vector2_2)[1], ((Vector2) ref sizeDelta2)[1]);
        Bounds rectTransformBounds2 = RectTransformUtility.CalculateRelativeRectTransformBounds((Transform) this.rectTransform, (Transform) this.rectChildren[index]);
        if (this.m_StartAxis == FlexibleLayoutGroup.Axis.Horizontal)
        {
          if ((double) ((Bounds) ref rectTransformBounds1).max.x <= (double) ((Bounds) ref rectTransformBounds2).max.x)
          {
            ((Vector2) ref vector2_2)[0] = ((Vector2) ref vector2_1)[0];
            // ISSUE: variable of a reference type
            Vector2& local3;
            ((Vector2) (local3 = ref vector2_2))[1] = ((Vector2) ref local3)[1] + (this.m_LineSpace + ((Vector2) ref this.m_Spacing)[1]);
            this.SetChildAlongAxis(this.rectChildren[index], 0, ((Vector2) ref vector2_2)[0], ((Vector2) ref sizeDelta2)[0]);
            this.SetChildAlongAxis(this.rectChildren[index], 1, ((Vector2) ref vector2_2)[1], ((Vector2) ref sizeDelta2)[1]);
          }
          // ISSUE: variable of a reference type
          Vector2& local4;
          ((Vector2) (local4 = ref vector2_2))[0] = ((Vector2) ref local4)[0] + (((Vector2) ref sizeDelta2)[0] + ((Vector2) ref this.m_Spacing)[0]);
        }
        else
        {
          if ((double) ((Bounds) ref rectTransformBounds1).min.y >= (double) ((Bounds) ref rectTransformBounds2).min.y)
          {
            // ISSUE: variable of a reference type
            Vector2& local5;
            ((Vector2) (local5 = ref vector2_2))[0] = ((Vector2) ref local5)[0] + (this.m_ColumnSpace + ((Vector2) ref this.m_Spacing)[0]);
            ((Vector2) ref vector2_2)[1] = ((Vector2) ref vector2_1)[1];
            this.SetChildAlongAxis(this.rectChildren[index], 0, ((Vector2) ref vector2_2)[0], ((Vector2) ref sizeDelta2)[0]);
            this.SetChildAlongAxis(this.rectChildren[index], 1, ((Vector2) ref vector2_2)[1], ((Vector2) ref sizeDelta2)[1]);
          }
          // ISSUE: variable of a reference type
          Vector2& local6;
          ((Vector2) (local6 = ref vector2_2))[1] = ((Vector2) ref local6)[1] + (((Vector2) ref sizeDelta2)[1] + ((Vector2) ref this.m_Spacing)[1]);
        }
      }
      if (this.m_StartAxis == FlexibleLayoutGroup.Axis.Horizontal)
      {
        ((Vector2) ref this.m_TotalMinSize)[0] = ((Vector2) ref sizeDelta1)[0];
        ((Vector2) ref this.m_TotalMinSize)[1] = ((Vector2) ref vector2_2)[1] + (float) this.m_Padding.bottom + this.m_LineSpace;
        ((Vector2) ref this.m_TotalPreferredSize)[0] = ((Vector2) ref sizeDelta1)[0];
        ((Vector2) ref this.m_TotalPreferredSize)[1] = ((Vector2) ref vector2_2)[1] + (float) this.m_Padding.bottom + this.m_LineSpace;
        ((Vector2) ref this.m_TotalFlexibleSize)[0] = 0.0f;
        ((Vector2) ref this.m_TotalFlexibleSize)[1] = 0.0f;
      }
      else
      {
        ((Vector2) ref this.m_TotalMinSize)[0] = ((Vector2) ref vector2_2)[0] + (float) this.m_Padding.right + this.m_ColumnSpace;
        ((Vector2) ref this.m_TotalMinSize)[1] = ((Vector2) ref sizeDelta1)[1];
        ((Vector2) ref this.m_TotalPreferredSize)[0] = ((Vector2) ref vector2_2)[0] + (float) this.m_Padding.right + this.m_ColumnSpace;
        ((Vector2) ref this.m_TotalPreferredSize)[1] = ((Vector2) ref sizeDelta1)[1];
        ((Vector2) ref this.m_TotalFlexibleSize)[0] = 0.0f;
        ((Vector2) ref this.m_TotalFlexibleSize)[1] = 0.0f;
      }
    }

    protected virtual void OnEnable()
    {
      base.OnEnable();
      this.SetDirty();
    }

    protected virtual void OnDisable()
    {
      LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
      base.OnDisable();
    }

    protected virtual void OnDidApplyAnimationProperties() => this.SetDirty();

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      if (!this.isRootLayoutGroup)
        return;
      this.SetDirty();
    }

    protected virtual void OnTransformChildrenChanged() => this.SetDirty();

    protected void SetDirty()
    {
      if (!this.IsActive())
        return;
      LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
    }

    public enum Constraint
    {
      Flexible,
      FixedColumnCount,
      FixedRowCount,
    }

    public enum Corner
    {
      UpperLeft,
      UpperRight,
      LowerLeft,
      LowerRight,
    }

    public enum Axis
    {
      Horizontal,
      Vertical,
    }
  }
}
