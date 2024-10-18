// Decompiled with JetBrains decompiler
// Type: SRPG.BGSlider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  public class BGSlider : MonoBehaviour, IDragHandler, IEventSystemHandler
  {
    public float ScrollSpeed = 10f;
    public float ReviseWidthValue = 100f;
    public List<GiziScroll> SyncScrollWith = new List<GiziScroll>();
    public string SyncScrollWithID;
    private float mScrollPos;
    private float mDesiredScrollPos;
    private Vector2 mDefaultPosition;
    private bool mResetScrollPos;
    public float DefaultScrollRatio;
    [SerializeField]
    private GameObject[] PermissibleObjects;
    private float WHEEL_SCROLL_COEF = 300f;
    private float axis_val;
    private List<RaycastResult> raycast_result_list = new List<RaycastResult>();
    private List<GameObject> child_objects = new List<GameObject>();
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
      Rect rect = (((Component) this).transform as RectTransform).rect;
      float width = ((Rect) ref rect).width;
      float num = (float) Screen.width + this.ReviseWidthValue;
      if ((double) Screen.width < (double) width)
        num = width + this.ReviseWidthValue;
      float max = Mathf.Max(Mathf.Min(num - width, this.ReviseWidthValue), 0.0f);
      if (this.mResetScrollPos)
      {
        this.mScrollPos = this.mDesiredScrollPos = max * this.DefaultScrollRatio;
        this.mResetScrollPos = false;
      }
      this.ClampScrollPos(0.0f, max);
      foreach (GiziScroll giziScroll in this.SyncScrollWith)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) giziScroll, (UnityEngine.Object) null) && (double) max > 0.0)
          giziScroll.ScrollPos = this.mScrollPos / max;
      }
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) eventData.pointerDrag, (UnityEngine.Object) ((Component) this).gameObject) && (this.PermissibleObjects == null || this.PermissibleObjects.Length <= 0 || Array.FindIndex<GameObject>(this.PermissibleObjects, (Predicate<GameObject>) (p => UnityEngine.Object.op_Equality((UnityEngine.Object) p, (UnityEngine.Object) eventData.pointerDrag))) == -1))
        return;
      this.mDesiredScrollPos -= eventData.delta.x;
      ((AbstractEventData) eventData).Use();
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
      this.pointer_event.position = Vector2.op_Implicit(Input.mousePosition);
      EventSystem.current.RaycastAll(this.pointer_event, this.raycast_result_list);
      if (this.raycast_result_list.Count <= 0)
        return false;
      this.child_objects.Clear();
      foreach (Component componentsInChild in ((Component) this).GetComponentsInChildren<Transform>())
        this.child_objects.Add(componentsInChild.gameObject);
      List<GameObject> childObjects = this.child_objects;
      RaycastResult raycastResult1 = this.raycast_result_list[0];
      GameObject gameObject1 = ((RaycastResult) ref raycastResult1).gameObject;
      return childObjects.Contains(gameObject1) || this.PermissibleObjects != null && this.PermissibleObjects.Length > 0 && Array.FindIndex<GameObject>(this.PermissibleObjects, (Predicate<GameObject>) (p =>
      {
        GameObject gameObject2 = p;
        RaycastResult raycastResult2 = this.raycast_result_list[0];
        GameObject gameObject3 = ((RaycastResult) ref raycastResult2).gameObject;
        return UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2, (UnityEngine.Object) gameObject3);
      })) != -1;
    }
  }
}
