// Decompiled with JetBrains decompiler
// Type: EventBackLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
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
    EventBackLogContent eventBackLogContent = Object.Instantiate<EventBackLogContent>(this.mBackLogItemPrefab, this.mBackLogItemParent);
    eventBackLogContent.SetBackLogText(name, text);
    ((Component) eventBackLogContent).gameObject.SetActive(true);
    this.mBackLogItems.Add(eventBackLogContent);
    this.mIsUpdate = true;
  }

  public bool CanOpen { get; set; }

  public int Count => this.mBackLogItems.Count;

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
    if (Object.op_Equality((Object) EventScript.BackLogCanvas, (Object) null))
      return;
    ((Component) EventScript.BackLogCanvas).gameObject.SetActive(true);
    ((Component) this).gameObject.SetActive(true);
    this.mScrollRect.verticalNormalizedPosition = 0.0f;
  }

  public void Close()
  {
    if (Object.op_Inequality((Object) EventScript.BackLogCanvas, (Object) null))
      ((Component) EventScript.BackLogCanvas).gameObject.SetActive(false);
    ((Component) this).gameObject.SetActive(false);
  }

  public void Clear()
  {
    for (int index = this.mBackLogItems.Count - 1; index >= 0; --index)
    {
      Object.Destroy((Object) ((Component) this.mBackLogItems[index]).gameObject);
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
