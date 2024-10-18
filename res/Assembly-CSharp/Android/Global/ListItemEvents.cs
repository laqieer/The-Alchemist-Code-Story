// Decompiled with JetBrains decompiler
// Type: ListItemEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Event/List Item Events")]
public class ListItemEvents : MonoBehaviour
{
  public Vector2 DisplayRectMergin = Vector2.zero;
  public Vector2 ParentScale = Vector2.one;
  public ListItemEvents.ListItemEvent OnSelect;
  public ListItemEvents.ListItemEvent OnOpenDetail;
  public ListItemEvents.ListItemEvent OnCloseDetail;
  public Transform Body;
  private RectTransform mTransform;
  public bool IsEnableSkillChange;
  private ChapterParam mChapterCache;

  public ChapterParam Chapter
  {
    get
    {
      if (this.mChapterCache == null)
      {
        DataSource component = this.GetComponent<DataSource>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          this.mChapterCache = component.FindDataOfClass<ChapterParam>((ChapterParam) null);
      }
      return this.mChapterCache;
    }
  }

  protected virtual void Awake()
  {
    this.mTransform = this.transform as RectTransform;
  }

  public void Select()
  {
    if (this.OnSelect == null)
      return;
    this.OnSelect(this.gameObject);
  }

  public void OpenDetail()
  {
    if (this.OnOpenDetail == null)
      return;
    this.OnOpenDetail(this.gameObject);
  }

  public void CloseDetail()
  {
    if (this.OnCloseDetail == null)
      return;
    this.OnCloseDetail(this.gameObject);
  }

  public void AttachBody()
  {
    if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null) || !((UnityEngine.Object) this.Body.parent != (UnityEngine.Object) this.mTransform))
      return;
    this.Body.SetParent((Transform) this.mTransform, false);
    Animator component1 = this.GetComponent<Animator>();
    if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
      component1.Rebind();
    this.Body.gameObject.SetActive(true);
    Selectable component2 = this.GetComponent<Selectable>();
    if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null) || !component2.enabled)
      return;
    component2.enabled = !component2.enabled;
    component2.enabled = !component2.enabled;
  }

  public void DetachBody(Transform pool)
  {
    if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null) || !((UnityEngine.Object) this.Body.parent != (UnityEngine.Object) pool))
      return;
    this.Body.SetParent(pool, false);
    this.Body.gameObject.SetActive(false);
  }

  public RectTransform GetRectTransform()
  {
    return this.mTransform;
  }

  private void OnDestroy()
  {
    if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null))
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) this.Body);
    this.Body = (Transform) null;
  }

  public delegate void ListItemEvent(GameObject go);
}
