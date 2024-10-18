// Decompiled with JetBrains decompiler
// Type: SRPG.BGSlider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class BGSlider : MonoBehaviour, IDragHandler, IEventSystemHandler
  {
    public float ScrollSpeed = 10f;
    public float BGWidth = 1500f;
    public List<GiziScroll> SyncScrollWith = new List<GiziScroll>();
    public string SyncScrollWithID;
    private float mScrollPos;
    private float mDesiredScrollPos;
    private Vector2 mDefaultPosition;
    private bool mResetScrollPos;
    public float DefaultScrollRatio;

    private void Start()
    {
      this.mResetScrollPos = true;
      if (this.SyncScrollWith.Count == 0 && !string.IsNullOrEmpty(this.SyncScrollWithID))
      {
        GameObject[] gameObjects = GameObjectID.FindGameObjects(this.SyncScrollWithID);
        if (gameObjects != null)
        {
          foreach (GameObject gameObject in gameObjects)
            this.SyncScrollWith.Add(gameObject.GetComponent<GiziScroll>());
        }
      }
      Canvas component = this.gameObject.GetComponent<Canvas>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      this.BGWidth = (float) ((double) component.pixelRect.width / (double) component.scaleFactor + 100.0);
    }

    private void ClampScrollPos(float min, float max)
    {
      this.mScrollPos = Mathf.Clamp(this.mScrollPos, min, max);
      this.mDesiredScrollPos = Mathf.Clamp(this.mDesiredScrollPos, min, max);
    }

    private void Update()
    {
      this.mScrollPos = Mathf.Lerp(this.mScrollPos, this.mDesiredScrollPos, Time.deltaTime * this.ScrollSpeed);
      float max = Mathf.Max(this.BGWidth - (this.transform as RectTransform).rect.width, 0.0f);
      if (this.mResetScrollPos)
      {
        this.mScrollPos = this.mDesiredScrollPos = max * this.DefaultScrollRatio;
        this.mResetScrollPos = false;
      }
      this.ClampScrollPos(0.0f, max);
      using (List<GiziScroll>.Enumerator enumerator = this.SyncScrollWith.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GiziScroll current = enumerator.Current;
          if ((UnityEngine.Object) current != (UnityEngine.Object) null && (double) max > 0.0)
            current.ScrollPos = this.mScrollPos / max;
        }
      }
    }

    public void OnDrag(PointerEventData eventData)
    {
      if ((UnityEngine.Object) eventData.pointerDrag != (UnityEngine.Object) this.gameObject)
        return;
      this.mDesiredScrollPos -= eventData.delta.x;
      eventData.Use();
    }
  }
}
