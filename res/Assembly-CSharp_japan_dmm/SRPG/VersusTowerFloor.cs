// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerFloor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class VersusTowerFloor : MonoBehaviour
  {
    public Text FriendName;
    public Text FloorText;
    public GameObject UnitObj;
    public GameObject FloorInfoObj;
    public GameObject RingObj;
    public Sprite CurrentSprite;
    public Sprite DefaultSprite;
    public Image FloorImage;
    private int mCurrentFloor = -1;

    public int Floor => this.mCurrentFloor;

    public void Refresh(int idx, int max)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int versusTowerFloor = instance.Player.VersusTowerFloor;
      int floor = idx + 1;
      if (idx >= 0 && idx < max)
      {
        this.mCurrentFloor = floor;
        if (Object.op_Inequality((Object) this.FloorInfoObj, (Object) null))
          this.FloorInfoObj.SetActive(true);
        if (Object.op_Inequality((Object) this.FloorText, (Object) null))
        {
          this.FloorText.text = floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
          if (floor == versusTowerFloor)
            ((Graphic) this.FloorText).color = new Color(1f, 1f, 0.0f);
          else
            ((Graphic) this.FloorText).color = new Color(1f, 1f, 1f);
        }
        if (Object.op_Inequality((Object) this.FloorImage, (Object) null))
        {
          if (floor == versusTowerFloor)
          {
            if (Object.op_Inequality((Object) this.CurrentSprite, (Object) null))
              this.FloorImage.sprite = this.CurrentSprite;
          }
          else if (Object.op_Inequality((Object) this.DefaultSprite, (Object) null))
            this.FloorImage.sprite = this.DefaultSprite;
        }
        if (Object.op_Inequality((Object) this.RingObj, (Object) null))
          this.RingObj.SetActive(versusTowerFloor == floor);
        VersusFriendScore[] versusFriendScore = instance.GetVersusFriendScore(floor);
        if (versusFriendScore != null && versusFriendScore.Length > 0 && floor != versusTowerFloor)
        {
          int length = versusFriendScore.Length;
          string empty = string.Empty;
          if (Object.op_Inequality((Object) this.UnitObj, (Object) null))
          {
            this.UnitObj.SetActive(true);
            DataSource.Bind<UnitData>(this.UnitObj, versusFriendScore[0].unit);
            GameParameter.UpdateAll(this.UnitObj);
          }
          int num = length - 1;
          string str = num <= 0 ? versusFriendScore[0].name : string.Format(LocalizedText.Get("sys.MULTI_VERSUS_FRIEND_NAME"), (object) versusFriendScore[0].name, (object) num);
          if (!Object.op_Inequality((Object) this.FriendName, (Object) null))
            return;
          this.FriendName.text = str;
        }
        else
        {
          if (!Object.op_Inequality((Object) this.UnitObj, (Object) null))
            return;
          this.UnitObj.SetActive(false);
        }
      }
      else
      {
        this.mCurrentFloor = -1;
        if (Object.op_Inequality((Object) this.FloorInfoObj, (Object) null))
          this.FloorInfoObj.SetActive(false);
        if (Object.op_Inequality((Object) this.RingObj, (Object) null))
          this.RingObj.SetActive(false);
        if (!Object.op_Inequality((Object) this.FloorImage, (Object) null) || !Object.op_Inequality((Object) this.DefaultSprite, (Object) null))
          return;
        this.FloorImage.sprite = this.DefaultSprite;
      }
    }

    public void SetPlayerObject(GameObject playerObj)
    {
      if (!Object.op_Inequality((Object) playerObj, (Object) null))
        return;
      playerObj.SetActive(true);
      if (!Object.op_Inequality((Object) this.RingObj, (Object) null))
        return;
      playerObj.transform.position = this.RingObj.transform.position;
    }
  }
}
