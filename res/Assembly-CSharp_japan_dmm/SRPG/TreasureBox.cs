// Decompiled with JetBrains decompiler
// Type: SRPG.TreasureBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TreasureBox : MonoBehaviour
  {
    public AnimationClip OpenAnimation;
    public float DropDelay = 1f;
    public float GoldDelay = 1f;
    public Vector3 DropOffset = new Vector3(0.0f, 0.0f, 0.0f);
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
      return Object.op_Inequality((Object) ((Component) this).gameObject, (Object) null) && ((Component) this).GetComponent<Animation>().isPlaying;
    }

    private void Update()
    {
      if (!this.mOpened)
        return;
      if (!this.mDropSpawned && Object.op_Inequality((Object) this.mDropItem, (Object) null))
      {
        this.DropDelay -= Time.deltaTime;
        if ((double) this.DropDelay <= 0.0)
        {
          this.mDropSpawned = true;
          ((Component) this.mDropItem).gameObject.SetActive(true);
        }
      }
      if (!this.mGoldSpawned && Object.op_Inequality((Object) this.mDropGold, (Object) null))
      {
        this.GoldDelay -= Time.deltaTime;
        if ((double) this.GoldDelay <= 0.0)
        {
          this.mGoldSpawned = true;
          ((Component) this.mDropGold).gameObject.SetActive(true);
          this.mDropGold = (DropGoldEffect) null;
        }
      }
      if (this.IsPlaying() || Object.op_Implicit((Object) this.mDropGold) || Object.op_Implicit((Object) this.mDropItem))
        return;
      Object.Destroy((Object) ((Component) this).gameObject);
    }

    public void Open(
      Unit.DropItem DropItem,
      DropItemEffect dropItemTemplate,
      int numGolds,
      DropGoldEffect dropGoldTemplate)
    {
      ((Component) this).GetComponent<Animation>().AddClip(this.OpenAnimation, ((Object) this.OpenAnimation).name);
      ((Component) this).GetComponent<Animation>().Play(((Object) this.OpenAnimation).name);
      this.mOpened = true;
      Transform transform = ((Component) this).transform;
      if (numGolds > 0)
      {
        this.mDropGold = Object.Instantiate<DropGoldEffect>(dropGoldTemplate);
        ((Component) this.mDropGold).transform.position = transform.position;
        this.mDropGold.DropOwner = this.owner;
        this.mDropGold.Gold = numGolds;
        ((Component) this.mDropGold).gameObject.SetActive(false);
      }
      if (!Object.op_Inequality((Object) dropItemTemplate, (Object) null) || DropItem == null)
        return;
      this.mDropItem = Object.Instantiate<DropItemEffect>(dropItemTemplate);
      ((Component) this.mDropItem).transform.position = Vector3.op_Addition(transform.position, this.DropOffset);
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        SceneBattle.Popup2D(((Component) this.mDropItem).gameObject, ((Component) this.mDropItem).transform.position);
      this.mDropItem.DropOwner = this.owner;
      this.mDropItem.DropItem = DropItem;
      ((Component) this.mDropItem).gameObject.SetActive(false);
    }
  }
}
