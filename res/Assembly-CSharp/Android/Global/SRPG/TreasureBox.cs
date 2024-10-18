// Decompiled with JetBrains decompiler
// Type: SRPG.TreasureBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TreasureBox : MonoBehaviour
  {
    public float DropDelay = 1f;
    public float GoldDelay = 1f;
    public Vector3 DropOffset = new Vector3(0.0f, 0.0f, 0.0f);
    public AnimationClip OpenAnimation;
    private DropItemEffect mDropItem;
    private bool mDropSpawned;
    private bool mGoldSpawned;
    private bool mOpened;
    private DropGoldEffect mDropGold;

    public Unit owner { set; get; }

    private void Start()
    {
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject((Component) this.mDropItem);
      GameUtility.DestroyGameObject((Component) this.mDropGold);
    }

    public bool IsPlaying()
    {
      if ((UnityEngine.Object) this.gameObject != (UnityEngine.Object) null)
        return this.GetComponent<Animation>().isPlaying;
      return false;
    }

    private void Update()
    {
      if (!this.mOpened)
        return;
      if (!this.mDropSpawned && (UnityEngine.Object) this.mDropItem != (UnityEngine.Object) null)
      {
        this.DropDelay -= Time.deltaTime;
        if ((double) this.DropDelay <= 0.0)
        {
          this.mDropSpawned = true;
          this.mDropItem.gameObject.SetActive(true);
        }
      }
      if (!this.mGoldSpawned && (UnityEngine.Object) this.mDropGold != (UnityEngine.Object) null)
      {
        this.GoldDelay -= Time.deltaTime;
        if ((double) this.GoldDelay <= 0.0)
        {
          this.mGoldSpawned = true;
          this.mDropGold.gameObject.SetActive(true);
          this.mDropGold = (DropGoldEffect) null;
        }
      }
      if (this.IsPlaying() || (bool) ((UnityEngine.Object) this.mDropGold) || (bool) ((UnityEngine.Object) this.mDropItem))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }

    public void Open(Unit.DropItem DropItem, DropItemEffect dropItemTemplate, int numGolds, DropGoldEffect dropGoldTemplate)
    {
      this.GetComponent<Animation>().AddClip(this.OpenAnimation, this.OpenAnimation.name);
      this.GetComponent<Animation>().Play(this.OpenAnimation.name);
      this.mOpened = true;
      Transform transform = this.transform;
      if (numGolds > 0)
      {
        this.mDropGold = UnityEngine.Object.Instantiate<DropGoldEffect>(dropGoldTemplate);
        this.mDropGold.transform.position = transform.position;
        this.mDropGold.DropOwner = this.owner;
        this.mDropGold.Gold = numGolds;
        this.mDropGold.gameObject.SetActive(false);
      }
      if (!((UnityEngine.Object) dropItemTemplate != (UnityEngine.Object) null) || DropItem == null)
        return;
      this.mDropItem = UnityEngine.Object.Instantiate<DropItemEffect>(dropItemTemplate);
      this.mDropItem.transform.position = transform.position + this.DropOffset;
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
        SceneBattle.Popup2D(this.mDropItem.gameObject, this.mDropItem.transform.position, 0, 0.0f);
      this.mDropItem.DropOwner = this.owner;
      this.mDropItem.DropItem = DropItem;
      this.mDropItem.gameObject.SetActive(false);
    }
  }
}
