// Decompiled with JetBrains decompiler
// Type: SRPG.BattleStamp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleStamp : MonoBehaviour
  {
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public BattleStamp.SelectEvent OnSelectItem;
    public RectTransform ListParent;
    public ListItemEvents ItemTemplate;
    public Sprite[] Sprites;
    public GameObject[] Prefabs;
    public string SpriteGameObjectID;
    public string SelectCursorGameObjectID;
    private int mSelectID;

    public int SelectStampID
    {
      get
      {
        return this.mSelectID;
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
      {
        this.ItemTemplate.gameObject.SetActive(false);
        if ((UnityEngine.Object) this.ListParent == (UnityEngine.Object) null)
          this.ListParent = this.ItemTemplate.transform.parent as RectTransform;
      }
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ListParent == (UnityEngine.Object) null || this.Sprites == null)
        return;
      for (int index1 = 0; index1 < this.Sprites.Length; ++index1)
      {
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ItemTemplate);
        listItemEvents.gameObject.SetActive(true);
        listItemEvents.gameObject.name = listItemEvents.gameObject.name + ":" + index1.ToString();
        listItemEvents.transform.SetParent((Transform) this.ListParent, false);
        GameObjectID[] componentsInChildren1 = listItemEvents.GetComponentsInChildren<GameObjectID>();
        if (componentsInChildren1 != null)
        {
          for (int index2 = 0; index2 < componentsInChildren1.Length; ++index2)
          {
            if (componentsInChildren1[index2].ID.Equals(this.SpriteGameObjectID))
            {
              Image image = !((UnityEngine.Object) componentsInChildren1[index2] == (UnityEngine.Object) null) ? componentsInChildren1[index2].gameObject.GetComponent<Image>() : (Image) null;
              if ((UnityEngine.Object) image != (UnityEngine.Object) null)
                image.sprite = this.Sprites[index1];
            }
            else if (componentsInChildren1[index2].ID.Equals(this.SelectCursorGameObjectID))
              componentsInChildren1[index2].gameObject.SetActive(index1 == this.mSelectID);
          }
        }
        this.mItems.Add(listItemEvents);
        listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go =>
        {
          this.mSelectID = this.mItems.FindIndex((Predicate<ListItemEvents>) (it => (UnityEngine.Object) it.gameObject == (UnityEngine.Object) go.gameObject));
          for (int index1 = 0; index1 < this.mItems.Count; ++index1)
          {
            GameObjectID[] componentsInChildren = this.mItems[index1].GetComponentsInChildren<GameObjectID>(true);
            if (componentsInChildren != null)
            {
              for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
              {
                if (componentsInChildren[index2].ID.Equals(this.SelectCursorGameObjectID))
                  componentsInChildren[index2].gameObject.SetActive(index1 == this.mSelectID);
              }
            }
          }
          if (this.mSelectID < 0)
            return;
          Sprite sprite = this.Sprites[this.mSelectID];
          if (!(bool) ((UnityEngine.Object) sprite) || this.OnSelectItem == null)
            return;
          this.OnSelectItem(sprite);
        });
      }
    }

    public delegate void SelectEvent(Sprite sprite);
  }
}
