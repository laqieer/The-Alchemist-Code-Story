// Decompiled with JetBrains decompiler
// Type: CanvasStack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("Layout/Canvas Stack")]
[RequireComponent(typeof (Canvas))]
[DisallowMultipleComponent]
public class CanvasStack : MonoBehaviour
{
  private static List<CanvasStack> mStack = new List<CanvasStack>(8);
  private Canvas mCanvas;
  private GraphicRaycaster mRaycaster;
  [SerializeField]
  private int mPriority = -1;
  public bool Modal = true;
  public bool SystemModal;
  private static bool mBlockRaycasts = true;

  public int Priority
  {
    get => this.mPriority;
    set
    {
      if (this.mPriority >= 0 && value < 0)
      {
        CanvasStack.mStack.Remove(this);
        CanvasStack.mStack.Add(this);
      }
      this.mPriority = value;
      CanvasStack.SortCanvases();
    }
  }

  public static void HideCanvases(int maxPriority)
  {
    for (int index = 0; index < CanvasStack.mStack.Count; ++index)
    {
      if (0 <= CanvasStack.mStack[index].mPriority && CanvasStack.mStack[index].mPriority <= maxPriority)
        ((Behaviour) ((Component) CanvasStack.mStack[index]).GetComponent<Canvas>()).enabled = false;
    }
  }

  public static void ShowAllCanvases()
  {
    for (int index = 0; index < CanvasStack.mStack.Count; ++index)
      ((Behaviour) ((Component) CanvasStack.mStack[index]).GetComponent<Canvas>()).enabled = true;
  }

  public static void BlockRaycasts(bool block)
  {
    if (CanvasStack.mBlockRaycasts == block)
      return;
    CanvasStack.mBlockRaycasts = block;
    if (GameUtility.IsDebugBuild)
    {
      if (CanvasStack.mBlockRaycasts)
        Debug.Log((object) "All raycasts are now BLOCKED");
      else
        Debug.Log((object) "Raycasts are no longer BLOCKED");
    }
    CanvasStack.UpdateGraphicRaycasters();
  }

  private void Awake()
  {
    this.mCanvas = ((Component) this).GetComponent<Canvas>();
    CanvasStack.mStack.Add(this);
    CanvasStack.SortCanvases();
    ((Object) this).hideFlags = (HideFlags) 8;
  }

  public static void SortCanvases()
  {
    List<CanvasStack> canvasStackList1 = new List<CanvasStack>(CanvasStack.mStack.Count);
    List<CanvasStack> canvasStackList2 = new List<CanvasStack>(CanvasStack.mStack.Count);
    List<CanvasStack> canvasStackList3 = new List<CanvasStack>(CanvasStack.mStack.Count);
    for (int index = 0; index < CanvasStack.mStack.Count; ++index)
    {
      if (CanvasStack.mStack[index].SystemModal)
        canvasStackList3.Add(CanvasStack.mStack[index]);
      else if (CanvasStack.mStack[index].Priority >= 0)
        canvasStackList1.Add(CanvasStack.mStack[index]);
      else
        canvasStackList2.Add(CanvasStack.mStack[index]);
    }
    CanvasStack.mStack.Clear();
    for (int index1 = 0; index1 < canvasStackList1.Count; ++index1)
    {
      for (int index2 = index1 + 1; index2 < canvasStackList1.Count; ++index2)
      {
        if (canvasStackList1[index1].Priority > canvasStackList1[index2].Priority)
        {
          CanvasStack canvasStack = canvasStackList1[index1];
          canvasStackList1[index1] = canvasStackList1[index2];
          canvasStackList1[index2] = canvasStack;
        }
      }
      CanvasStack.mStack.Add(canvasStackList1[index1]);
    }
    for (int index = 0; index < canvasStackList2.Count; ++index)
      CanvasStack.mStack.Add(canvasStackList2[index]);
    for (int index3 = 0; index3 < canvasStackList3.Count; ++index3)
    {
      for (int index4 = index3 + 1; index4 < canvasStackList3.Count; ++index4)
      {
        if (canvasStackList3[index3].Priority > canvasStackList3[index4].Priority)
        {
          CanvasStack canvasStack = canvasStackList3[index3];
          canvasStackList3[index3] = canvasStackList3[index4];
          canvasStackList3[index4] = canvasStack;
        }
      }
      CanvasStack.mStack.Add(canvasStackList3[index3]);
    }
    for (int index = CanvasStack.mStack.Count - 1; index >= 0; --index)
    {
      CanvasStack.mStack[index].mCanvas.sortingOrder = index;
      if (CanvasStack.mStack[index].SystemModal)
      {
        CanvasStack.mStack[index].mCanvas.overrideSorting = true;
        CanvasStack.mStack[index].mCanvas.sortingOrder = 20000 + index;
      }
      if (((Behaviour) CanvasStack.mStack[index].mCanvas).enabled)
      {
        ((Behaviour) CanvasStack.mStack[index].mCanvas).enabled = false;
        ((Behaviour) CanvasStack.mStack[index].mCanvas).enabled = true;
      }
    }
    CanvasStack.UpdateGraphicRaycasters();
  }

  private static void UpdateGraphicRaycasters()
  {
    bool flag = false;
    for (int index1 = CanvasStack.mStack.Count - 1; index1 >= 0; --index1)
    {
      if (((Behaviour) CanvasStack.mStack[index1].mCanvas).enabled)
      {
        GraphicRaycaster[] componentsInChildren = ((Component) CanvasStack.mStack[index1]).GetComponentsInChildren<GraphicRaycaster>(true);
        for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
        {
          if (Object.op_Inequality((Object) componentsInChildren[index2], (Object) null))
            ((Behaviour) componentsInChildren[index2]).enabled = !flag && CanvasStack.mBlockRaycasts;
        }
        if (CanvasStack.mStack[index1].Modal || CanvasStack.mStack[index1].SystemModal)
          flag = true;
      }
    }
  }

  private void OnDestroy()
  {
    CanvasStack.mStack.Remove(this);
    CanvasStack.SortCanvases();
  }

  public static Canvas Base
  {
    get => CanvasStack.mStack.Count > 0 ? CanvasStack.mStack[0].mCanvas : (Canvas) null;
  }

  public static Canvas Top
  {
    get
    {
      return CanvasStack.mStack.Count > 0 ? CanvasStack.mStack[CanvasStack.mStack.Count - 1].mCanvas : (Canvas) null;
    }
  }
}
