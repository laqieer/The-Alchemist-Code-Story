// Decompiled with JetBrains decompiler
// Type: SRPG.BattleMapRoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class BattleMapRoot
  {
    private int m_Width;
    private int m_Height;
    private BattleMapRoot.Element[] m_Elements;
    private int m_TotalCost;
    private BattleMapRoot.Element m_Start;
    private BattleMapRoot.Element m_End;
    private List<BattleMapRoot.Element> m_CalcStack = new List<BattleMapRoot.Element>();

    public void Initialize(int gridWidth, int gridHeight, Grid[,] gridMap)
    {
      this.m_Width = gridWidth;
      this.m_Height = gridHeight;
      this.m_Elements = new BattleMapRoot.Element[this.m_Height * this.m_Width];
      for (int index1 = 0; index1 < gridHeight; ++index1)
      {
        for (int index2 = 0; index2 < gridWidth; ++index2)
        {
          int index3 = index1 * gridWidth + index2;
          Grid grid = gridMap[index2, index1];
          this.m_Elements[index3] = new BattleMapRoot.Element()
          {
            grid = grid
          };
        }
      }
      BattleMapRoot.Element[] elementArray = new BattleMapRoot.Element[4];
      for (int index4 = 0; index4 < gridHeight; ++index4)
      {
        for (int index5 = 0; index5 < gridWidth; ++index5)
        {
          int index6 = index4 * gridWidth + index5;
          BattleMapRoot.Element element1 = this.m_Elements[index6];
          int length = 0;
          if (index5 - 1 >= 0)
          {
            BattleMapRoot.Element element2 = this.GetElement(index6 - 1);
            if (element2 != null)
              elementArray[length++] = element2;
          }
          if (index5 + 1 < gridWidth)
          {
            BattleMapRoot.Element element3 = this.GetElement(index6 + 1);
            if (element3 != null)
              elementArray[length++] = element3;
          }
          if (index4 - 1 >= 0)
          {
            BattleMapRoot.Element element4 = this.GetElement(index6 - gridWidth);
            if (element4 != null)
              elementArray[length++] = element4;
          }
          if (index4 + 1 < gridHeight)
          {
            BattleMapRoot.Element element5 = this.GetElement(index6 + gridWidth);
            if (element5 != null)
              elementArray[length++] = element5;
          }
          element1.link = new BattleMapRoot.Link[length];
          for (int index7 = 0; index7 < length; ++index7)
          {
            element1.link[index7].element = elementArray[index7];
            element1.link[index7].cost = elementArray[index7].grid.cost;
            element1.link[index7].is_disable_stopped = elementArray[index7].grid.geo != null && (bool) elementArray[index7].grid.geo.DisableStopped;
            element1.link[index7].enter = elementArray[index7].grid.enter;
            element1.link[index7].height = elementArray[index7].grid.height - element1.grid.height;
          }
        }
      }
    }

    public void Release()
    {
      this.m_Elements = (BattleMapRoot.Element[]) null;
      this.m_Start = (BattleMapRoot.Element) null;
      this.m_End = (BattleMapRoot.Element) null;
      this.m_CalcStack.Clear();
    }

    public bool CalcRoot(
      Unit unit,
      int startX,
      int startY,
      int endX,
      int endY,
      int moveHeight,
      GridMap<int> walkableField)
    {
      if (this.m_Elements == null || this.m_Elements.Length == 0 || startX == endX && startY == endY)
        return false;
      this._CalcRootInit(unit, moveHeight, walkableField);
      this.m_Start = this.GetElement(startX, startY);
      if (this.m_Start == null)
        return false;
      this.m_End = this.GetElement(endX, endY);
      if (this.m_End == null)
        return false;
      this.m_Start.cost = 0;
      this.m_CalcStack.Add(this.m_Start);
      while (this.m_CalcStack.Count > 0)
      {
        BattleMapRoot.Element calc = this.m_CalcStack[0];
        this.m_CalcStack.RemoveAt(0);
        if (calc == this.m_End)
          this.m_TotalCost = calc.cost;
        else
          this._CalcRootSubroutine(calc);
      }
      return this.m_TotalCost != int.MaxValue;
    }

    private void _CalcRootInit(Unit unit, int moveHeight, GridMap<int> walkableField)
    {
      this.m_TotalCost = int.MaxValue;
      this.m_Start = (BattleMapRoot.Element) null;
      this.m_End = (BattleMapRoot.Element) null;
      this.m_CalcStack.Clear();
      for (int index1 = 0; index1 < this.m_Elements.Length; ++index1)
      {
        BattleMapRoot.Element element = this.m_Elements[index1];
        element.cost = int.MaxValue;
        element.root = (BattleMapRoot.Element) null;
        for (int index2 = 0; index2 < element.link.Length; ++index2)
        {
          int num = element.link[index2].cost;
          if (element.link[index2].is_disable_stopped && unit != null && unit.MovType != eMovType.Normal && (element.link[index2].enter & 1 << (int) (unit.MovType & (eMovType) 31)) != 0)
            num = 1;
          if (moveHeight < element.link[index2].height)
            num += 10000;
          else if (walkableField.get(element.grid.x, element.grid.y) < 0)
            num += 20000;
          element.link[index2].calc_cost = num;
        }
      }
    }

    private void _CalcRootSubroutine(BattleMapRoot.Element element)
    {
      if (element.cost >= this.m_TotalCost || element.link == null || element.link.Length == 0)
        return;
      for (int index = 0; index < element.link.Length; ++index)
      {
        BattleMapRoot.Link link = element.link[index];
        int num = element.cost + link.calc_cost;
        if (num < link.element.cost)
        {
          link.element.cost = num;
          link.element.root = element;
          this.m_CalcStack.Remove(link.element);
          this.m_CalcStack.Add(link.element);
        }
      }
    }

    public Grid[] GetRoot()
    {
      if (this.m_Start == null || this.m_End == null)
        return (Grid[]) null;
      List<Grid> gridList = new List<Grid>();
      BattleMapRoot.Element element = this.m_End;
      gridList.Add(element.grid);
      for (; element.root != null; element = element.root)
        gridList.Add(element.root.grid);
      if (gridList.Count < 2)
        return (Grid[]) null;
      gridList.Reverse();
      return gridList[0] != this.m_Start.grid ? (Grid[]) null : gridList.ToArray();
    }

    private BattleMapRoot.Element GetElement(int index)
    {
      return index < 0 || index >= this.m_Elements.Length ? (BattleMapRoot.Element) null : this.m_Elements[index];
    }

    private BattleMapRoot.Element GetElement(int x, int y)
    {
      return x < 0 || x >= this.m_Width || y < 0 || y >= this.m_Height ? (BattleMapRoot.Element) null : this.m_Elements[y * this.m_Width + x];
    }

    private struct Link
    {
      public BattleMapRoot.Element element;
      public int cost;
      public bool is_disable_stopped;
      public int enter;
      public int calc_cost;
      public int height;

      public override string ToString()
      {
        return string.Format("pos[{0:D2},{1:D2}] cost[{2}] height[{3}] total[{4}]", (object) this.element.grid.x, (object) this.element.grid.y, (object) this.cost, (object) this.height, (object) this.calc_cost);
      }
    }

    private class Element
    {
      public Grid grid;
      public BattleMapRoot.Link[] link;
      public int cost;
      public BattleMapRoot.Element root;

      public override string ToString()
      {
        return string.Format("pos[{0:D2},{1:D2}] cost[{2}] links[{3}] root[{4}]", (object) this.grid.x, (object) this.grid.y, (object) this.cost, (object) (this.link == null ? 0 : this.link.Length));
      }
    }
  }
}
