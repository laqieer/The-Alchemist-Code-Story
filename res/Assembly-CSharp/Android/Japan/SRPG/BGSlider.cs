// Decompiled with JetBrains decompiler
// Type: SRPG.BGSlider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class BGSlider : MonoBehaviour, IDragHandler, IEventSystemHandler
  {
    public float ScrollSpeed = 10f;
    public float ReviseWidthValue = 100f;
    public List<GiziScroll> SyncScrollWith = new List<GiziScroll>();
    private float WHEEL_SCROLL_COEF = 300f;
    private List<RaycastResult> raycast_result_list = new List<RaycastResult>();
    private List<GameObject> child_objects = new List<GameObject>();
    public string SyncScrollWithID;
    private float mScrollPos;
    private float mDesiredScrollPos;
    private Vector2 mDefaultPosition;
    private bool mResetScrollPos;
    public float DefaultScrollRatio;
    private float axis_val;
    private PointerEventData pointer_event;

    private void Start()
    {
      this.mResetScrollPos = true;
      if (this.SyncScrollWith.Count != 0 || string.IsNullOrEmpty(this.SyncScrollWithID))
        return;
      GameObject[] gameObjects = GameObjectID.FindGameObjects(this.SyncScrollWithID);
      if (gameObjects == null)
        return;
      foreach (GameObject gameObject in gameObjects)
        this.SyncScrollWith.Add(gameObject.GetComponent<GiziScroll>());
    }

    private void ClampScrollPos(float min, float max)
    {
      this.mScrollPos = Mathf.Clamp(this.mScrollPos, min, max);
      this.mDesiredScrollPos = Mathf.Clamp(this.mDesiredScrollPos, min, max);
    }

    private void Update()
    {
      this.UpdateWheelScroll();
      this.mScrollPos = Mathf.Lerp(this.mScrollPos, this.mDesiredScrollPos, Time.deltaTime * this.ScrollSpeed);
      float width = (this.transform as RectTransform).rect.width;
      float num = (float) Screen.width + this.ReviseWidthValue;
      if ((double) Screen.width < (double) width)
        num = width + this.ReviseWidthValue;
      float max = Mathf.Max(num - width, 0.0f);
      if (this.mResetScrollPos)
      {
        this.mScrollPos = this.mDesiredScrollPos = max * this.DefaultScrollRatio;
        this.mResetScrollPos = false;
      }
      this.ClampScrollPos(0.0f, max);
      foreach (GiziScroll giziScroll in this.SyncScrollWith)
      {
        if ((UnityEngine.Object) giziScroll != (UnityEngine.Object) null && (double) max > 0.0)
          giziScroll.ScrollPos = this.mScrollPos / max;
      }
    }

    public void OnDrag(PointerEventData eventData)
    {
      if ((UnityEngine.Object) eventData.pointerDrag != (UnityEngine.Object) this.gameObject)
        return;
      this.mDesiredScrollPos -= eventData.delta.x;
      eventData.Use();
    }

    private void UpdateWheelScroll()
    {
      this.axis_val = Input.GetAxis("Mouse ScrollWheel");
      if ((double) this.axis_val == 0.0 || !this.IsHitRayCast())
        return;
      this.mDesiredScrollPos -= this.axis_val * this.WHEEL_SCROLL_COEF;
    }

    private bool IsHitRayCast()
    {
      this.raycast_result_list.Clear();
      this.pointer_event = new PointerEventData(EventSystem.current);
      this.pointer_event.position = (Vector2) Input.mousePosition;
      EventSystem.current.RaycastAll(this.pointer_event, this.raycast_result_list);
      if (this.raycast_result_list.Count <= 0)
        return false;
      this.child_objects.Clear();
      foreach (Component componentsInChild in this.GetComponentsInChildren<Transform>())
        this.child_objects.Add(componentsInChild.gameObject);
      return this.child_objects.Contains(this.raycast_result_list[0].gameObject);
    }
  }
}
