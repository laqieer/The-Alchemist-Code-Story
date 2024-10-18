// Decompiled with JetBrains decompiler
// Type: EventBackLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System.Collections.Generic;
using UnityEngine;

public class EventBackLog : MonoBehaviour
{
  private List<EventBackLogContent> mBackLogItems = new List<EventBackLogContent>();
  [SerializeField]
  private Transform mBackLogItemParent;
  [SerializeField]
  private EventBackLogContent mBackLogItemPrefab;
  [SerializeField]
  private SRPG_ScrollRect mScrollRect;
  private bool mIsUpdate;

  public void Add(string name, string text)
  {
    EventBackLogContent eventBackLogContent = UnityEngine.Object.Instantiate<EventBackLogContent>(this.mBackLogItemPrefab, this.mBackLogItemParent);
    eventBackLogContent.SetBackLogText(name, text);
    eventBackLogContent.gameObject.SetActive(true);
    this.mBackLogItems.Add(eventBackLogContent);
    this.mIsUpdate = true;
  }

  public bool CanOpen { get; set; }

  public int Count
  {
    get
    {
      return this.mBackLogItems.Count;
    }
  }

  private void LateUpdate()
  {
    if (!this.mIsUpdate)
      return;
    this.mScrollRect.verticalNormalizedPosition = 0.0f;
    this.mIsUpdate = false;
  }

  public void Open()
  {
    if (!this.CanOpen || this.mBackLogItems == null || this.mBackLogItems.Count <= 0)
      return;
    EventScript.IsMessageAuto = false;
    if ((UnityEngine.Object) EventScript.BackLogCanvas == (UnityEngine.Object) null)
      return;
    EventScript.BackLogCanvas.gameObject.SetActive(true);
    this.gameObject.SetActive(true);
    this.mScrollRect.verticalNormalizedPosition = 0.0f;
  }

  public void Close()
  {
    if ((UnityEngine.Object) EventScript.BackLogCanvas != (UnityEngine.Object) null)
      EventScript.BackLogCanvas.gameObject.SetActive(false);
    this.gameObject.SetActive(false);
  }

  public void Clear()
  {
    for (int index = this.mBackLogItems.Count - 1; index >= 0; --index)
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mBackLogItems[index].gameObject);
      this.mBackLogItems.Remove(this.mBackLogItems[index]);
    }
  }

  private void Start()
  {
  }

  private void Update()
  {
  }

  private void OnDestroy()
  {
  }
}
