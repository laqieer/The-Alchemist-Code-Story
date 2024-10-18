// Decompiled with JetBrains decompiler
// Type: SRPG.FlexibleLayoutGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  [RequireComponent(typeof (RectTransform))]
  public class FlexibleLayoutGroup : UIBehaviour, ILayoutElement, ILayoutGroup, ILayoutController
  {
    private List<RectTransform> m_RectChildren = new List<RectTransform>();
    private Vector2 m_TotalMinSize = Vector2.zero;
    private Vector2 m_TotalPreferredSize = Vector2.zero;
    private Vector2 m_TotalFlexibleSize = Vector2.zero;
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
        if ((UnityEngine.Object) this.m_RectTransform == (UnityEngine.Object) null)
          this.m_RectTransform = this.GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    private bool isRootLayoutGroup
    {
      get
      {
        if ((UnityEngine.Object) this.transform.parent == (UnityEngine.Object) null)
          return true;
        return (UnityEngine.Object) this.transform.parent.GetComponent(typeof (ILayoutGroup)) == (UnityEngine.Object) null;
      }
    }

    public virtual float minWidth
    {
      get
      {
        return this.GetTotalMinSize(0);
      }
    }

    public virtual float preferredWidth
    {
      get
      {
        return this.GetTotalPreferredSize(0);
      }
    }

    public virtual float flexibleWidth
    {
      get
      {
        return this.GetTotalFlexibleSize(0);
      }
    }

    public virtual float minHeight
    {
      get
      {
        return this.GetTotalMinSize(1);
      }
    }

    public virtual float preferredHeight
    {
      get
      {
        return this.GetTotalPreferredSize(1);
      }
    }

    public virtual float flexibleHeight
    {
      get
      {
        return this.GetTotalFlexibleSize(1);
      }
    }

    public virtual int layoutPriority
    {
      get
      {
        return 0;
      }
    }

    protected float GetTotalMinSize(int axis)
    {
      return this.m_TotalMinSize[axis];
    }

    protected float GetTotalPreferredSize(int axis)
    {
      return this.m_TotalPreferredSize[axis];
    }

    protected float GetTotalFlexibleSize(int axis)
    {
      return this.m_TotalFlexibleSize[axis];
    }

    public virtual void CalculateLayoutInputHorizontal()
    {
      this.rectChildren.Clear();
      for (int index = 0; index < this.rectTransform.childCount; ++index)
      {
        RectTransform child = this.rectTransform.GetChild(index) as RectTransform;
        if (child.gameObject.activeInHierarchy)
        {
          ILayoutIgnorer component = child.gameObject.GetComponent<ILayoutIgnorer>();
          if (component == null || !component.ignoreLayout)
            this.rectChildren.Add(child);
        }
      }
    }

    public virtual void CalculateLayoutInputVertical()
    {
    }

    public virtual void SetLayoutHorizontal()
    {
      this.UpdateLayout();
    }

    public virtual void SetLayoutVertical()
    {
      this.UpdateLayout();
    }

    private void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
    {
      if ((UnityEngine.Object) rect == (UnityEngine.Object) null)
        return;
      rect.SetInsetAndSizeFromParentEdge(axis != 0 ? RectTransform.Edge.Top : RectTransform.Edge.Left, pos, size);
    }

    private void UpdateLayout()
    {
      Bounds rectTransformBounds1 = RectTransformUtility.CalculateRelativeRectTransformBounds((Transform) this.rectTransform);
      rectTransformBounds1.min += new Vector3((float) this.m_Padding.left, (float) this.m_Padding.bottom);
      rectTransformBounds1.max -= new Vector3((float) this.m_Padding.right, (float) this.m_Padding.top);
      Vector2 sizeDelta1 = this.rectTransform.sizeDelta;
      Vector2 vector2_1 = new Vector2((float) this.m_Padding.left, (float) this.m_Padding.top);
      Vector2 vector2_2 = vector2_1;
      for (int index = 0; index < this.rectChildren.Count; ++index)
      {
        Vector2 sizeDelta2 = this.rectChildren[index].sizeDelta;
        this.SetChildAlongAxis(this.rectChildren[index], 0, vector2_2[0], sizeDelta2[0]);
        this.SetChildAlongAxis(this.rectChildren[index], 1, vector2_2[1], sizeDelta2[1]);
        Bounds rectTransformBounds2 = RectTransformUtility.CalculateRelativeRectTransformBounds((Transform) this.rectTransform, (Transform) this.rectChildren[index]);
        if (this.m_StartAxis == FlexibleLayoutGroup.Axis.Horizontal)
        {
          if ((double) rectTransformBounds1.max.x <= (double) rectTransformBounds2.max.x)
          {
            vector2_2[0] = vector2_1[0];
            vector2_2[1] += this.m_LineSpace + this.m_Spacing[1];
            this.SetChildAlongAxis(this.rectChildren[index], 0, vector2_2[0], sizeDelta2[0]);
            this.SetChildAlongAxis(this.rectChildren[index], 1, vector2_2[1], sizeDelta2[1]);
          }
          vector2_2[0] += sizeDelta2[0] + this.m_Spacing[0];
        }
        else
        {
          if ((double) rectTransformBounds1.min.y >= (double) rectTransformBounds2.min.y)
          {
            vector2_2[0] += this.m_ColumnSpace + this.m_Spacing[0];
            vector2_2[1] = vector2_1[1];
            this.SetChildAlongAxis(this.rectChildren[index], 0, vector2_2[0], sizeDelta2[0]);
            this.SetChildAlongAxis(this.rectChildren[index], 1, vector2_2[1], sizeDelta2[1]);
          }
          vector2_2[1] += sizeDelta2[1] + this.m_Spacing[1];
        }
      }
      if (this.m_StartAxis == FlexibleLayoutGroup.Axis.Horizontal)
      {
        this.m_TotalMinSize[0] = sizeDelta1[0];
        this.m_TotalMinSize[1] = vector2_2[1] + (float) this.m_Padding.bottom + this.m_LineSpace;
        this.m_TotalPreferredSize[0] = sizeDelta1[0];
        this.m_TotalPreferredSize[1] = vector2_2[1] + (float) this.m_Padding.bottom + this.m_LineSpace;
        this.m_TotalFlexibleSize[0] = 0.0f;
        this.m_TotalFlexibleSize[1] = 0.0f;
      }
      else
      {
        this.m_TotalMinSize[0] = vector2_2[0] + (float) this.m_Padding.right + this.m_ColumnSpace;
        this.m_TotalMinSize[1] = sizeDelta1[1];
        this.m_TotalPreferredSize[0] = vector2_2[0] + (float) this.m_Padding.right + this.m_ColumnSpace;
        this.m_TotalPreferredSize[1] = sizeDelta1[1];
        this.m_TotalFlexibleSize[0] = 0.0f;
        this.m_TotalFlexibleSize[1] = 0.0f;
      }
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.SetDirty();
    }

    protected override void OnDisable()
    {
      LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
      base.OnDisable();
    }

    protected override void OnDidApplyAnimationProperties()
    {
      this.SetDirty();
    }

    protected override void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      if (!this.isRootLayoutGroup)
        return;
      this.SetDirty();
    }

    protected virtual void OnTransformChildrenChanged()
    {
      this.SetDirty();
    }

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
