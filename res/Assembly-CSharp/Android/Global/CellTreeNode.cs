// Decompiled with JetBrains decompiler
// Type: CellTreeNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CellTreeNode
{
  public int Id;
  public Vector3 Center;
  public Vector3 Size;
  public Vector3 TopLeft;
  public Vector3 BottomRight;
  public CellTreeNode.ENodeType NodeType;
  public CellTreeNode Parent;
  public List<CellTreeNode> Childs;
  private float maxDistance;

  public CellTreeNode()
  {
  }

  public CellTreeNode(int id, CellTreeNode.ENodeType nodeType, CellTreeNode parent)
  {
    this.Id = id;
    this.NodeType = nodeType;
    this.Parent = parent;
  }

  public void AddChild(CellTreeNode child)
  {
    if (this.Childs == null)
      this.Childs = new List<CellTreeNode>(1);
    this.Childs.Add(child);
  }

  public void Draw()
  {
  }

  public void GetActiveCells(List<int> activeCells, bool yIsUpAxis, Vector3 position)
  {
    if (this.NodeType != CellTreeNode.ENodeType.Leaf)
    {
      using (List<CellTreeNode>.Enumerator enumerator = this.Childs.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.GetActiveCells(activeCells, yIsUpAxis, position);
      }
    }
    else
    {
      if (!this.IsPointNearCell(yIsUpAxis, position))
        return;
      if (this.IsPointInsideCell(yIsUpAxis, position))
      {
        activeCells.Insert(0, this.Id);
        for (CellTreeNode parent = this.Parent; parent != null; parent = parent.Parent)
          activeCells.Insert(0, parent.Id);
      }
      else
        activeCells.Add(this.Id);
    }
  }

  public bool IsPointInsideCell(bool yIsUpAxis, Vector3 point)
  {
    if ((double) point.x < (double) this.TopLeft.x || (double) point.x > (double) this.BottomRight.x)
      return false;
    if (yIsUpAxis)
    {
      if ((double) point.y >= (double) this.TopLeft.y && (double) point.y <= (double) this.BottomRight.y)
        return true;
    }
    else if ((double) point.z >= (double) this.TopLeft.z && (double) point.z <= (double) this.BottomRight.z)
      return true;
    return false;
  }

  public bool IsPointNearCell(bool yIsUpAxis, Vector3 point)
  {
    if ((double) this.maxDistance == 0.0)
      this.maxDistance = (float) (((double) this.Size.x + (double) this.Size.y + (double) this.Size.z) / 2.0);
    return (double) (point - this.Center).sqrMagnitude <= (double) this.maxDistance * (double) this.maxDistance;
  }

  public enum ENodeType
  {
    Root,
    Node,
    Leaf,
  }
}
