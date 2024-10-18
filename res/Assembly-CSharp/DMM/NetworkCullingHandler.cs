// Decompiled with JetBrains decompiler
// Type: NetworkCullingHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (PhotonView))]
public class NetworkCullingHandler : MonoBehaviour, IPunObservable
{
  private int orderIndex;
  private CullArea cullArea;
  private List<byte> previousActiveCells;
  private List<byte> activeCells;
  private PhotonView pView;
  private Vector3 lastPosition;
  private Vector3 currentPosition;

  private void OnEnable()
  {
    if (Object.op_Equality((Object) this.pView, (Object) null))
    {
      this.pView = ((Component) this).GetComponent<PhotonView>();
      if (!this.pView.isMine)
        return;
    }
    if (Object.op_Equality((Object) this.cullArea, (Object) null))
      this.cullArea = Object.FindObjectOfType<CullArea>();
    this.previousActiveCells = new List<byte>(0);
    this.activeCells = new List<byte>(0);
    this.currentPosition = this.lastPosition = ((Component) this).transform.position;
  }

  private void Start()
  {
    if (!this.pView.isMine || !PhotonNetwork.inRoom)
      return;
    if (this.cullArea.NumberOfSubdivisions == 0)
    {
      this.pView.group = this.cullArea.FIRST_GROUP_ID;
      PhotonNetwork.SetInterestGroups(this.cullArea.FIRST_GROUP_ID, true);
    }
    else
      this.pView.ObservedComponents.Add((Component) this);
  }

  private void Update()
  {
    if (!this.pView.isMine)
      return;
    this.lastPosition = this.currentPosition;
    this.currentPosition = ((Component) this).transform.position;
    if (!Vector3.op_Inequality(this.currentPosition, this.lastPosition) || !this.HaveActiveCellsChanged())
      return;
    this.UpdateInterestGroups();
  }

  private void OnGUI()
  {
    if (!this.pView.isMine)
      return;
    string str1 = "Inside cells:\n";
    string str2 = "Subscribed cells:\n";
    for (int index = 0; index < this.activeCells.Count; ++index)
    {
      if (index <= this.cullArea.NumberOfSubdivisions)
        str1 = str1 + (object) this.activeCells[index] + " | ";
      str2 = str2 + (object) this.activeCells[index] + " | ";
    }
    GUI.Label(new Rect(20f, (float) Screen.height - 120f, 200f, 40f), "<color=white>PhotonView Group: " + (object) this.pView.group + "</color>", new GUIStyle()
    {
      alignment = (TextAnchor) 0,
      fontSize = 16
    });
    GUI.Label(new Rect(20f, (float) Screen.height - 100f, 200f, 40f), "<color=white>" + str1 + "</color>", new GUIStyle()
    {
      alignment = (TextAnchor) 0,
      fontSize = 16
    });
    GUI.Label(new Rect(20f, (float) Screen.height - 60f, 200f, 40f), "<color=white>" + str2 + "</color>", new GUIStyle()
    {
      alignment = (TextAnchor) 0,
      fontSize = 16
    });
  }

  private bool HaveActiveCellsChanged()
  {
    if (this.cullArea.NumberOfSubdivisions == 0)
      return false;
    this.previousActiveCells = new List<byte>((IEnumerable<byte>) this.activeCells);
    this.activeCells = this.cullArea.GetActiveCells(((Component) this).transform.position);
    while (this.activeCells.Count <= this.cullArea.NumberOfSubdivisions)
      this.activeCells.Add(this.cullArea.FIRST_GROUP_ID);
    return this.activeCells.Count != this.previousActiveCells.Count || (int) this.activeCells[this.cullArea.NumberOfSubdivisions] != (int) this.previousActiveCells[this.cullArea.NumberOfSubdivisions];
  }

  private void UpdateInterestGroups()
  {
    List<byte> byteList = new List<byte>(0);
    foreach (byte previousActiveCell in this.previousActiveCells)
    {
      if (!this.activeCells.Contains(previousActiveCell))
        byteList.Add(previousActiveCell);
    }
    PhotonNetwork.SetInterestGroups(byteList.ToArray(), this.activeCells.ToArray());
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    while (this.activeCells.Count <= this.cullArea.NumberOfSubdivisions)
      this.activeCells.Add(this.cullArea.FIRST_GROUP_ID);
    if (this.cullArea.NumberOfSubdivisions == 1)
    {
      this.orderIndex = ++this.orderIndex % this.cullArea.SUBDIVISION_FIRST_LEVEL_ORDER.Length;
      this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_FIRST_LEVEL_ORDER[this.orderIndex]];
    }
    else if (this.cullArea.NumberOfSubdivisions == 2)
    {
      this.orderIndex = ++this.orderIndex % this.cullArea.SUBDIVISION_SECOND_LEVEL_ORDER.Length;
      this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_SECOND_LEVEL_ORDER[this.orderIndex]];
    }
    else
    {
      if (this.cullArea.NumberOfSubdivisions != 3)
        return;
      this.orderIndex = ++this.orderIndex % this.cullArea.SUBDIVISION_THIRD_LEVEL_ORDER.Length;
      this.pView.group = this.activeCells[this.cullArea.SUBDIVISION_THIRD_LEVEL_ORDER[this.orderIndex]];
    }
  }
}
