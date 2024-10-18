// Decompiled with JetBrains decompiler
// Type: SRPG.ContentNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ContentNode : MonoBehaviour
  {
    private ContentGrid m_Grid = ContentGrid.zero;
    private Vector2 m_Pos = Vector2.zero;
    private RectTransform m_RectTransform;
    private ContentController m_ContentController;
    private int m_Index;
    private ContentSource.Param m_Param;
    private bool m_ViewIn;

    public RectTransform rectTransform
    {
      get
      {
        if ((UnityEngine.Object) this.m_RectTransform == (UnityEngine.Object) null)
          this.m_RectTransform = this.gameObject.GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    public ContentController contentController
    {
      get
      {
        return this.m_ContentController;
      }
    }

    public int index
    {
      get
      {
        return this.m_Index;
      }
    }

    public float sizeX
    {
      get
      {
        return this.rectTransform.sizeDelta.x;
      }
    }

    public float sizeY
    {
      get
      {
        return this.rectTransform.sizeDelta.y;
      }
    }

    public float posX
    {
      get
      {
        return this.m_Pos.x;
      }
    }

    public float posY
    {
      get
      {
        return this.m_Pos.y;
      }
    }

    public int gridX
    {
      get
      {
        return this.m_Grid.x;
      }
    }

    public int gridY
    {
      get
      {
        return this.m_Grid.y;
      }
    }

    private void Awake()
    {
    }

    public virtual void Initialize(ContentController controller)
    {
      this.m_ContentController = controller;
      RectTransform rectTransform = this.rectTransform;
      if ((UnityEngine.Object) rectTransform != (UnityEngine.Object) null)
      {
        rectTransform.anchorMin = new Vector2(0.0f, 1f);
        rectTransform.anchorMax = new Vector2(0.0f, 1f);
      }
      this.m_ViewIn = false;
    }

    public virtual void Release()
    {
      this.m_ContentController = (ContentController) null;
    }

    public virtual void Copy(ContentNode src)
    {
      this.m_ContentController = src.m_ContentController;
      this.m_Index = src.m_Index;
      this.m_Param = src.m_Param;
      this.m_Grid = src.m_Grid;
      this.m_Pos = src.m_Pos;
    }

    public virtual void Update()
    {
      if (this.m_Param == null)
        return;
      this.m_Param.Update();
    }

    public virtual void LateUpdate()
    {
      if (this.m_Param == null)
        return;
      this.m_Param.LateUpdate();
    }

    public virtual void Setup(int index, Vector2 pos, ContentSource.Param param)
    {
      this.m_Index = index;
      this.m_Param = param;
      this.m_Pos = pos;
      this.name = "Node_" + (object) this.m_Index;
      this.rectTransform.anchoredPosition = this.PosToLocalPos(this.m_Pos);
      this.m_ViewIn = false;
      if (this.m_Param == null)
        return;
      this.m_Param.OnSetup(this);
    }

    public virtual void Setup(int index, int x, int y, ContentSource.Param param)
    {
      this.m_Grid = new ContentGrid(x, y);
      this.Setup(index, this.m_ContentController.GetNodePos(x, y), param);
    }

    public void SetActive(bool value)
    {
      this.gameObject.SetActive(value);
    }

    public void SetParam(ContentSource.Param param)
    {
      this.m_Param = param;
    }

    public ContentSource.Param GetParam()
    {
      return this.m_Param;
    }

    public T GetParam<T>() where T : ContentSource.Param
    {
      return this.m_Param as T;
    }

    public void SetGrid(int x, int y)
    {
      this.m_Grid.x = x;
      this.m_Grid.y = y;
    }

    public void SetGrid(ContentGrid grid)
    {
      this.m_Grid = grid;
    }

    public void SetPos(float x, float y)
    {
      this.m_Pos.x = x;
      this.m_Pos.y = y;
    }

    public void SetPos(Vector2 pos)
    {
      this.m_Pos = pos;
    }

    public Vector2 PosToLocalPos(Vector2 pos)
    {
      Vector2 pivot = this.rectTransform.pivot;
      Vector2 sizeDelta = this.rectTransform.sizeDelta;
      pos.x += pivot.x * sizeDelta.x;
      pos.y -= (1f - pivot.y) * sizeDelta.y;
      return pos;
    }

    public Vector2 LocalPosToPos(Vector2 pos)
    {
      Vector2 pivot = this.rectTransform.pivot;
      Vector2 sizeDelta = this.rectTransform.sizeDelta;
      pos.x -= pivot.x * sizeDelta.x;
      pos.y += (1f - pivot.y) * sizeDelta.y;
      return pos;
    }

    public void UpdateLocalPos(Vector2 pos)
    {
      this.m_Pos = pos;
      this.rectTransform.localPosition = (Vector3) pos;
    }

    public void UpdateAnchoredPos(Vector2 pos)
    {
      this.m_Pos = pos;
      this.rectTransform.anchoredPosition = pos;
    }

    public Vector2 GetPivotAnchoredPosition(Vector2 pos)
    {
      Vector2 pivot = this.rectTransform.pivot;
      Vector2 sizeDelta = this.rectTransform.sizeDelta;
      pos.x += (0.5f - pivot.x) * sizeDelta.x;
      pos.y += (0.5f - pivot.y) * sizeDelta.y;
      return pos;
    }

    public Vector2 GetPivotAnchoredPosition()
    {
      Vector2 pivot = this.rectTransform.pivot;
      Vector2 sizeDelta = this.rectTransform.sizeDelta;
      Vector2 anchoredPosition = this.rectTransform.anchoredPosition;
      anchoredPosition.x += (0.5f - pivot.x) * sizeDelta.x;
      anchoredPosition.y += (0.5f - pivot.y) * sizeDelta.y;
      return anchoredPosition;
    }

    public Vector2 GetWorldPos()
    {
      Vector2 anchoredPosition = this.m_ContentController.anchoredPosition;
      anchoredPosition.x += this.m_Pos.x;
      anchoredPosition.y += this.m_Pos.y;
      return anchoredPosition;
    }

    public bool IsValid()
    {
      if (this.m_Param != null)
        return this.m_Param.IsValid();
      return false;
    }

    public bool IsInvalid()
    {
      if (this.m_Param != null)
        return !this.m_Param.IsValid();
      return true;
    }

    public bool IsLock()
    {
      if (this.m_Param != null)
        return this.m_Param.IsLock();
      return true;
    }

    public bool IsReMake()
    {
      if (this.m_Param != null)
        return this.m_Param.IsReMake();
      return true;
    }

    public bool IsViewIn()
    {
      return this.m_ViewIn;
    }

    public bool IsViewOut()
    {
      return !this.m_ViewIn;
    }

    public virtual void OnEnable()
    {
      if (this.m_Param == null)
        return;
      if (!this.m_Param.IsValid())
      {
        this.m_Param = (ContentSource.Param) null;
      }
      else
      {
        this.m_Param.Wakeup();
        this.m_Param.OnEnable(this);
      }
    }

    public virtual void OnDisable()
    {
      if (this.m_Param == null)
        return;
      this.m_Param.OnDisable(this);
      if (this.m_Param.IsValid())
        return;
      this.m_Param = (ContentSource.Param) null;
    }

    public virtual void OnViewIn(Vector2 pivotViewPosition)
    {
      this.m_ViewIn = true;
      if (this.m_Param == null)
        return;
      this.m_Param.OnViewIn(this, pivotViewPosition);
    }

    public virtual void OnViewOut(Vector2 pivotViewPosition)
    {
      this.m_ViewIn = false;
      if (this.m_Param == null)
        return;
      this.m_Param.OnViewOut(this, pivotViewPosition);
    }

    public virtual void OnSelectOn()
    {
      if (this.m_Param == null)
        return;
      this.m_Param.OnSelectOn(this);
    }

    public virtual void OnSelectOff()
    {
      if (this.m_Param == null)
        return;
      this.m_Param.OnSelectOff(this);
    }

    public virtual void OnClick()
    {
      if (this.m_Param == null)
        return;
      this.m_Param.OnClick(this);
    }

    public enum EventType
    {
      SETUP,
      ENABLE,
      DISABLE,
    }
  }
}
