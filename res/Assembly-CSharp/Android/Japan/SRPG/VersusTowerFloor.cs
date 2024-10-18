// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerFloor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusTowerFloor : MonoBehaviour
  {
    private int mCurrentFloor = -1;
    public Text FriendName;
    public Text FloorText;
    public GameObject UnitObj;
    public GameObject FloorInfoObj;
    public GameObject RingObj;
    public Sprite CurrentSprite;
    public Sprite DefaultSprite;
    public Image FloorImage;

    public int Floor
    {
      get
      {
        return this.mCurrentFloor;
      }
    }

    public void Refresh(int idx, int max)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int versusTowerFloor = instance.Player.VersusTowerFloor;
      int floor = idx + 1;
      if (idx >= 0 && idx < max)
      {
        this.mCurrentFloor = floor;
        if ((UnityEngine.Object) this.FloorInfoObj != (UnityEngine.Object) null)
          this.FloorInfoObj.SetActive(true);
        if ((UnityEngine.Object) this.FloorText != (UnityEngine.Object) null)
        {
          this.FloorText.text = floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR");
          if (floor == versusTowerFloor)
            this.FloorText.color = new Color(1f, 1f, 0.0f);
          else
            this.FloorText.color = new Color(1f, 1f, 1f);
        }
        if ((UnityEngine.Object) this.FloorImage != (UnityEngine.Object) null)
        {
          if (floor == versusTowerFloor)
          {
            if ((UnityEngine.Object) this.CurrentSprite != (UnityEngine.Object) null)
              this.FloorImage.sprite = this.CurrentSprite;
          }
          else if ((UnityEngine.Object) this.DefaultSprite != (UnityEngine.Object) null)
            this.FloorImage.sprite = this.DefaultSprite;
        }
        if ((UnityEngine.Object) this.RingObj != (UnityEngine.Object) null)
          this.RingObj.SetActive(versusTowerFloor == floor);
        VersusFriendScore[] versusFriendScore = instance.GetVersusFriendScore(floor);
        if (versusFriendScore != null && versusFriendScore.Length > 0 && floor != versusTowerFloor)
        {
          int length = versusFriendScore.Length;
          string empty = string.Empty;
          if ((UnityEngine.Object) this.UnitObj != (UnityEngine.Object) null)
          {
            this.UnitObj.SetActive(true);
            DataSource.Bind<UnitData>(this.UnitObj, versusFriendScore[0].unit, false);
            GameParameter.UpdateAll(this.UnitObj);
          }
          int num = length - 1;
          string str = num <= 0 ? versusFriendScore[0].name : string.Format(LocalizedText.Get("sys.MULTI_VERSUS_FRIEND_NAME"), (object) versusFriendScore[0].name, (object) num);
          if (!((UnityEngine.Object) this.FriendName != (UnityEngine.Object) null))
            return;
          this.FriendName.text = str;
        }
        else
        {
          if (!((UnityEngine.Object) this.UnitObj != (UnityEngine.Object) null))
            return;
          this.UnitObj.SetActive(false);
        }
      }
      else
      {
        this.mCurrentFloor = -1;
        if ((UnityEngine.Object) this.FloorInfoObj != (UnityEngine.Object) null)
          this.FloorInfoObj.SetActive(false);
        if ((UnityEngine.Object) this.RingObj != (UnityEngine.Object) null)
          this.RingObj.SetActive(false);
        if (!((UnityEngine.Object) this.FloorImage != (UnityEngine.Object) null) || !((UnityEngine.Object) this.DefaultSprite != (UnityEngine.Object) null))
          return;
        this.FloorImage.sprite = this.DefaultSprite;
      }
    }

    public void SetPlayerObject(GameObject playerObj)
    {
      if (!((UnityEngine.Object) playerObj != (UnityEngine.Object) null))
        return;
      playerObj.SetActive(true);
      if (!((UnityEngine.Object) this.RingObj != (UnityEngine.Object) null))
        return;
      playerObj.transform.position = this.RingObj.transform.position;
    }
  }
}
