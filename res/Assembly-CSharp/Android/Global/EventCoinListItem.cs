// Decompiled with JetBrains decompiler
// Type: EventCoinListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class EventCoinListItem : MonoBehaviour
{
  [SerializeField]
  private GameObject button;

  public ListItemEvents listItemEvents
  {
    get
    {
      return this.GetComponent<ListItemEvents>();
    }
  }

  public GameObject Button
  {
    get
    {
      return this.button;
    }
  }

  public void Set(bool isPeriod, bool isRead, long post_at, long read)
  {
    if (isRead)
      this.button.gameObject.SetActive(false);
    else
      this.button.gameObject.SetActive(true);
  }
}
