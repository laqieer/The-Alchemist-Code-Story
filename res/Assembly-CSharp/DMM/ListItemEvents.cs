// Decompiled with JetBrains decompiler
// Type: ListItemEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("Event/List Item Events")]
public class ListItemEvents : MonoBehaviour
{
  public ListItemEvents.ListItemEvent OnSelect;
  public ListItemEvents.ListItemEvent OnOpenDetail;
  public ListItemEvents.ListItemEvent OnCloseDetail;
  public Transform Body;
  public Vector2 DisplayRectMergin = Vector2.zero;
  public Vector2 ParentScale = Vector2.one;
  private RectTransform mTransform;
  public bool IsEnableSkillChange;
  private ChapterParam mChapterCache;

  public ChapterParam Chapter
  {
    get
    {
      if (this.mChapterCache == null)
      {
        DataSource component = ((Component) this).GetComponent<DataSource>();
        if (Object.op_Inequality((Object) component, (Object) null))
          this.mChapterCache = component.FindDataOfClass<ChapterParam>((ChapterParam) null);
      }
      return this.mChapterCache;
    }
  }

  protected virtual void Awake() => this.mTransform = ((Component) this).transform as RectTransform;

  public void Select()
  {
    if (this.OnSelect == null)
      return;
    this.OnSelect(((Component) this).gameObject);
  }

  public void OpenDetail()
  {
    if (this.OnOpenDetail == null)
      return;
    this.OnOpenDetail(((Component) this).gameObject);
  }

  public void CloseDetail()
  {
    if (this.OnCloseDetail == null)
      return;
    this.OnCloseDetail(((Component) this).gameObject);
  }

  public void AttachBody()
  {
    if (!Object.op_Inequality((Object) this.Body, (Object) null) || !Object.op_Inequality((Object) this.Body.parent, (Object) this.mTransform))
      return;
    this.Body.SetParent((Transform) this.mTransform, false);
    Animator component1 = ((Component) this).GetComponent<Animator>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      component1.Rebind();
    ((Component) this.Body).gameObject.SetActive(true);
    Selectable component2 = ((Component) this).GetComponent<Selectable>();
    if (!Object.op_Inequality((Object) component2, (Object) null) || !((Behaviour) component2).enabled)
      return;
    ((Behaviour) component2).enabled = !((Behaviour) component2).enabled;
    ((Behaviour) component2).enabled = !((Behaviour) component2).enabled;
  }

  public void DetachBody(Transform pool)
  {
    if (!Object.op_Inequality((Object) this.Body, (Object) null) || !Object.op_Inequality((Object) this.Body.parent, (Object) pool))
      return;
    this.Body.SetParent(pool, false);
    ((Component) this.Body).gameObject.SetActive(false);
  }

  public RectTransform GetRectTransform() => this.mTransform;

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.Body, (Object) null))
      return;
    Object.Destroy((Object) this.Body);
    this.Body = (Transform) null;
  }

  public delegate void ListItemEvent(GameObject go);
}
