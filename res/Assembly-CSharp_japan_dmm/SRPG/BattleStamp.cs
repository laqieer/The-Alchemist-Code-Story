// Decompiled with JetBrains decompiler
// Type: SRPG.BattleStamp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleStamp : MonoBehaviour
  {
    public BattleStamp.SelectEvent OnSelectItem;
    public RectTransform ListParent;
    public ListItemEvents ItemTemplate;
    public Sprite[] Sprites;
    public GameObject[] Prefabs;
    public string SpriteGameObjectID;
    public string SelectCursorGameObjectID;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private int mSelectID;

    public int SelectStampID => this.mSelectID;

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
      {
        ((Component) this.ItemTemplate).gameObject.SetActive(false);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListParent, (UnityEngine.Object) null))
          this.ListParent = ((Component) this.ItemTemplate).transform.parent as RectTransform;
      }
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListParent, (UnityEngine.Object) null) || this.Sprites == null)
        return;
      for (int index1 = 0; index1 < this.Sprites.Length; ++index1)
      {
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ItemTemplate);
        ((Component) listItemEvents).gameObject.SetActive(true);
        ((UnityEngine.Object) ((Component) listItemEvents).gameObject).name = ((UnityEngine.Object) ((Component) listItemEvents).gameObject).name + ":" + index1.ToString();
        ((Component) listItemEvents).transform.SetParent((Transform) this.ListParent, false);
        GameObjectID[] componentsInChildren1 = ((Component) listItemEvents).GetComponentsInChildren<GameObjectID>();
        if (componentsInChildren1 != null)
        {
          for (int index2 = 0; index2 < componentsInChildren1.Length; ++index2)
          {
            if (componentsInChildren1[index2].ID.Equals(this.SpriteGameObjectID))
            {
              Image component = !UnityEngine.Object.op_Equality((UnityEngine.Object) componentsInChildren1[index2], (UnityEngine.Object) null) ? ((Component) componentsInChildren1[index2]).gameObject.GetComponent<Image>() : (Image) null;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                component.sprite = this.Sprites[index1];
            }
            else if (componentsInChildren1[index2].ID.Equals(this.SelectCursorGameObjectID))
              ((Component) componentsInChildren1[index2]).gameObject.SetActive(index1 == this.mSelectID);
          }
        }
        this.mItems.Add(listItemEvents);
        listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go =>
        {
          this.mSelectID = this.mItems.FindIndex((Predicate<ListItemEvents>) (it => UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) it).gameObject, (UnityEngine.Object) go.gameObject)));
          for (int index3 = 0; index3 < this.mItems.Count; ++index3)
          {
            GameObjectID[] componentsInChildren2 = ((Component) this.mItems[index3]).GetComponentsInChildren<GameObjectID>(true);
            if (componentsInChildren2 != null)
            {
              for (int index4 = 0; index4 < componentsInChildren2.Length; ++index4)
              {
                if (componentsInChildren2[index4].ID.Equals(this.SelectCursorGameObjectID))
                  ((Component) componentsInChildren2[index4]).gameObject.SetActive(index3 == this.mSelectID);
              }
            }
          }
          if (this.mSelectID < 0)
            return;
          Sprite sprite = this.Sprites[this.mSelectID];
          if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) sprite) || this.OnSelectItem == null)
            return;
          this.OnSelectItem(sprite);
        });
      }
    }

    public delegate void SelectEvent(Sprite sprite);
  }
}
