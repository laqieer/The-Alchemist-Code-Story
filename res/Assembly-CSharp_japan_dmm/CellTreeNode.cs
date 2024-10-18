// Decompiled with JetBrains decompiler
// Type: CellTreeNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CellTreeNode
{
  public byte Id;
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

  public CellTreeNode(byte id, CellTreeNode.ENodeType nodeType, CellTreeNode parent)
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

  public void GetActiveCells(List<byte> activeCells, bool yIsUpAxis, Vector3 position)
  {
    if (this.NodeType != CellTreeNode.ENodeType.Leaf)
    {
      foreach (CellTreeNode child in this.Childs)
        child.GetActiveCells(activeCells, yIsUpAxis, position);
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
    Vector3 vector3 = Vector3.op_Subtraction(point, this.Center);
    return (double) ((Vector3) ref vector3).sqrMagnitude <= (double) this.maxDistance * (double) this.maxDistance;
  }

  public enum ENodeType
  {
    Root,
    Node,
    Leaf,
  }
}
