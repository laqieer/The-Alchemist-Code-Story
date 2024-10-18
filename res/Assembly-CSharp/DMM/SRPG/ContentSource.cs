// Decompiled with JetBrains decompiler
// Type: SRPG.ContentSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ContentSource
  {
    private ContentSource.Param[] m_Table;
    private ContentController m_ContentController;

    protected ContentController contentController => this.m_ContentController;

    public virtual void Initialize(ContentController controller)
    {
      this.m_ContentController = controller;
      int index = 0;
      for (int count = this.GetCount(); index < count; ++index)
        this.GetParam(index)?.Initialize(this);
    }

    public virtual void Release() => this.Clear();

    public virtual void Clear()
    {
      int index = 0;
      for (int count = this.GetCount(); index < count; ++index)
        this.GetParam(index)?.Release();
      this.m_Table = (ContentSource.Param[]) null;
    }

    public virtual void Update()
    {
    }

    public virtual ContentNode Instantiate(ContentNode res)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(((Component) res).gameObject);
      return Object.op_Inequality((Object) gameObject, (Object) null) ? gameObject.GetComponent<ContentNode>() : (ContentNode) null;
    }

    public void SetTable(ContentSource.Param[] values)
    {
      if (values != null)
      {
        for (int index = 0; index < values.Length; ++index)
          values[index].id = index;
        this.m_Table = values;
      }
      else
        this.m_Table = (ContentSource.Param[]) null;
    }

    public virtual ContentSource.Param GetParam(int index)
    {
      return this.m_Table != null && index >= 0 && index < this.m_Table.Length ? this.m_Table[index] : (ContentSource.Param) null;
    }

    public virtual ContentSource.Param GetParam(string value)
    {
      if (this.m_Table != null)
      {
        for (int index = 0; index < this.m_Table.Length; ++index)
        {
          ContentSource.Param obj = this.m_Table[index];
          if (obj != null && obj.Equal(value))
            return obj;
        }
      }
      return (ContentSource.Param) null;
    }

    public virtual int GetCount() => this.m_Table != null ? this.m_Table.Length : 0;

    public ContentController GetContentController() => this.m_ContentController;

    public class Param
    {
      private int _id = -1;
      private int _idprev = int.MinValue;

      public int id
      {
        set => this._id = value;
        get => this._id;
      }

      public void Wakeup() => this._idprev = this.id;

      public virtual void Initialize(ContentSource source)
      {
      }

      public virtual void Release() => this._idprev = int.MinValue;

      public virtual bool IsValid() => true;

      public virtual bool IsLock() => false;

      public virtual bool IsReMake() => this.id != this._idprev;

      public virtual void Update()
      {
      }

      public virtual void LateUpdate()
      {
      }

      public virtual void OnSetup(ContentNode node)
      {
      }

      public virtual void OnEnable(ContentNode node)
      {
      }

      public virtual void OnDisable(ContentNode node)
      {
      }

      public virtual void OnViewIn(ContentNode node, Vector2 pivotViewPosition)
      {
      }

      public virtual void OnViewOut(ContentNode node, Vector2 pivotViewPosition)
      {
      }

      public virtual void OnPageFit(ContentNode node)
      {
      }

      public virtual void OnSelectOn(ContentNode node)
      {
      }

      public virtual void OnSelectOff(ContentNode node)
      {
      }

      public virtual void OnClick(ContentNode node)
      {
      }

      public virtual bool Equal(string value) => this.ToString() == value;
    }
  }
}
