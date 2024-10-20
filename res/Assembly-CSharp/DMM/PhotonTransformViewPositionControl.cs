﻿// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewPositionControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PhotonTransformViewPositionControl
{
  private PhotonTransformViewPositionModel m_Model;
  private float m_CurrentSpeed;
  private double m_LastSerializeTime;
  private Vector3 m_SynchronizedSpeed = Vector3.zero;
  private float m_SynchronizedTurnSpeed;
  private Vector3 m_NetworkPosition;
  private Queue<Vector3> m_OldNetworkPositions = new Queue<Vector3>();
  private bool m_UpdatedPositionAfterOnSerialize = true;

  public PhotonTransformViewPositionControl(PhotonTransformViewPositionModel model)
  {
    this.m_Model = model;
  }

  private Vector3 GetOldestStoredNetworkPosition()
  {
    Vector3 storedNetworkPosition = this.m_NetworkPosition;
    if (this.m_OldNetworkPositions.Count > 0)
      storedNetworkPosition = this.m_OldNetworkPositions.Peek();
    return storedNetworkPosition;
  }

  public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
  {
    this.m_SynchronizedSpeed = speed;
    this.m_SynchronizedTurnSpeed = turnSpeed;
  }

  public Vector3 UpdatePosition(Vector3 currentPosition)
  {
    Vector3 vector3 = Vector3.op_Addition(this.GetNetworkPosition(), this.GetExtrapolatedPositionOffset());
    switch (this.m_Model.InterpolateOption)
    {
      case PhotonTransformViewPositionModel.InterpolateOptions.Disabled:
        if (!this.m_UpdatedPositionAfterOnSerialize)
        {
          currentPosition = vector3;
          this.m_UpdatedPositionAfterOnSerialize = true;
          break;
        }
        break;
      case PhotonTransformViewPositionModel.InterpolateOptions.FixedSpeed:
        currentPosition = Vector3.MoveTowards(currentPosition, vector3, Time.deltaTime * this.m_Model.InterpolateMoveTowardsSpeed);
        break;
      case PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed:
        if (this.m_OldNetworkPositions.Count != 0)
        {
          float num = Vector3.Distance(this.m_NetworkPosition, this.GetOldestStoredNetworkPosition()) / (float) this.m_OldNetworkPositions.Count * (float) PhotonNetwork.sendRateOnSerialize;
          currentPosition = Vector3.MoveTowards(currentPosition, vector3, Time.deltaTime * num);
          break;
        }
        break;
      case PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues:
        currentPosition = (double) ((Vector3) ref this.m_SynchronizedSpeed).magnitude != 0.0 ? Vector3.MoveTowards(currentPosition, vector3, Time.deltaTime * ((Vector3) ref this.m_SynchronizedSpeed).magnitude) : vector3;
        break;
      case PhotonTransformViewPositionModel.InterpolateOptions.Lerp:
        currentPosition = Vector3.Lerp(currentPosition, vector3, Time.deltaTime * this.m_Model.InterpolateLerpSpeed);
        break;
    }
    if (this.m_Model.TeleportEnabled && (double) Vector3.Distance(currentPosition, this.GetNetworkPosition()) > (double) this.m_Model.TeleportIfDistanceGreaterThan)
      currentPosition = this.GetNetworkPosition();
    return currentPosition;
  }

  public Vector3 GetNetworkPosition() => this.m_NetworkPosition;

  public Vector3 GetExtrapolatedPositionOffset()
  {
    float num = (float) (PhotonNetwork.time - this.m_LastSerializeTime);
    if (this.m_Model.ExtrapolateIncludingRoundTripTime)
      num += (float) PhotonNetwork.GetPing() / 1000f;
    Vector3 extrapolatedPositionOffset = Vector3.zero;
    switch (this.m_Model.ExtrapolateOption)
    {
      case PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues:
        extrapolatedPositionOffset = Quaternion.op_Multiply(Quaternion.Euler(0.0f, this.m_SynchronizedTurnSpeed * num, 0.0f), Vector3.op_Multiply(this.m_SynchronizedSpeed, num));
        break;
      case PhotonTransformViewPositionModel.ExtrapolateOptions.EstimateSpeedAndTurn:
        extrapolatedPositionOffset = Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_Subtraction(this.m_NetworkPosition, this.GetOldestStoredNetworkPosition()), (float) PhotonNetwork.sendRateOnSerialize), num);
        break;
      case PhotonTransformViewPositionModel.ExtrapolateOptions.FixedSpeed:
        Vector3 vector3 = Vector3.op_Subtraction(this.m_NetworkPosition, this.GetOldestStoredNetworkPosition());
        extrapolatedPositionOffset = Vector3.op_Multiply(Vector3.op_Multiply(((Vector3) ref vector3).normalized, this.m_Model.ExtrapolateSpeed), num);
        break;
    }
    return extrapolatedPositionOffset;
  }

  public void OnPhotonSerializeView(
    Vector3 currentPosition,
    PhotonStream stream,
    PhotonMessageInfo info)
  {
    if (!this.m_Model.SynchronizeEnabled)
      return;
    if (stream.isWriting)
      this.SerializeData(currentPosition, stream, info);
    else
      this.DeserializeData(stream, info);
    this.m_LastSerializeTime = PhotonNetwork.time;
    this.m_UpdatedPositionAfterOnSerialize = false;
  }

  private void SerializeData(Vector3 currentPosition, PhotonStream stream, PhotonMessageInfo info)
  {
    stream.SendNext((object) currentPosition);
    this.m_NetworkPosition = currentPosition;
    if (this.m_Model.ExtrapolateOption != PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues && this.m_Model.InterpolateOption != PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
      return;
    stream.SendNext((object) this.m_SynchronizedSpeed);
    stream.SendNext((object) this.m_SynchronizedTurnSpeed);
  }

  private void DeserializeData(PhotonStream stream, PhotonMessageInfo info)
  {
    Vector3 next = (Vector3) stream.ReceiveNext();
    if (this.m_Model.ExtrapolateOption == PhotonTransformViewPositionModel.ExtrapolateOptions.SynchronizeValues || this.m_Model.InterpolateOption == PhotonTransformViewPositionModel.InterpolateOptions.SynchronizeValues)
    {
      this.m_SynchronizedSpeed = (Vector3) stream.ReceiveNext();
      this.m_SynchronizedTurnSpeed = (float) stream.ReceiveNext();
    }
    if (this.m_OldNetworkPositions.Count == 0)
      this.m_NetworkPosition = next;
    this.m_OldNetworkPositions.Enqueue(this.m_NetworkPosition);
    this.m_NetworkPosition = next;
    while (this.m_OldNetworkPositions.Count > this.m_Model.ExtrapolateNumberOfStoredPositions)
      this.m_OldNetworkPositions.Dequeue();
  }
}