// Decompiled with JetBrains decompiler
// Type: CustomTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using UnityEngine;

internal static class CustomTypes
{
  public static readonly byte[] memVector3 = new byte[12];
  public static readonly byte[] memVector2 = new byte[8];
  public static readonly byte[] memQuarternion = new byte[16];
  public static readonly byte[] memPlayer = new byte[4];

  internal static void Register()
  {
    PhotonPeer.RegisterType(typeof (Vector2), (byte) 87, new SerializeStreamMethod(CustomTypes.SerializeVector2), new DeserializeStreamMethod(CustomTypes.DeserializeVector2));
    PhotonPeer.RegisterType(typeof (Vector3), (byte) 86, new SerializeStreamMethod(CustomTypes.SerializeVector3), new DeserializeStreamMethod(CustomTypes.DeserializeVector3));
    PhotonPeer.RegisterType(typeof (Quaternion), (byte) 81, new SerializeStreamMethod(CustomTypes.SerializeQuaternion), new DeserializeStreamMethod(CustomTypes.DeserializeQuaternion));
    PhotonPeer.RegisterType(typeof (PhotonPlayer), (byte) 80, new SerializeStreamMethod(CustomTypes.SerializePhotonPlayer), new DeserializeStreamMethod(CustomTypes.DeserializePhotonPlayer));
  }

  private static short SerializeVector3(StreamBuffer outStream, object customobject)
  {
    Vector3 vector3 = (Vector3) customobject;
    int targetOffset = 0;
    lock (CustomTypes.memVector3)
    {
      byte[] memVector3 = CustomTypes.memVector3;
      Protocol.Serialize(vector3.x, memVector3, ref targetOffset);
      Protocol.Serialize(vector3.y, memVector3, ref targetOffset);
      Protocol.Serialize(vector3.z, memVector3, ref targetOffset);
      outStream.Write(memVector3, 0, 12);
    }
    return 12;
  }

  private static object DeserializeVector3(StreamBuffer inStream, short length)
  {
    Vector3 vector3 = new Vector3();
    lock (CustomTypes.memVector3)
    {
      inStream.Read(CustomTypes.memVector3, 0, 12);
      int offset = 0;
      Protocol.Deserialize(out vector3.x, CustomTypes.memVector3, ref offset);
      Protocol.Deserialize(out vector3.y, CustomTypes.memVector3, ref offset);
      Protocol.Deserialize(out vector3.z, CustomTypes.memVector3, ref offset);
    }
    return (object) vector3;
  }

  private static short SerializeVector2(StreamBuffer outStream, object customobject)
  {
    Vector2 vector2 = (Vector2) customobject;
    lock (CustomTypes.memVector2)
    {
      byte[] memVector2 = CustomTypes.memVector2;
      int targetOffset = 0;
      Protocol.Serialize(vector2.x, memVector2, ref targetOffset);
      Protocol.Serialize(vector2.y, memVector2, ref targetOffset);
      outStream.Write(memVector2, 0, 8);
    }
    return 8;
  }

  private static object DeserializeVector2(StreamBuffer inStream, short length)
  {
    Vector2 vector2 = new Vector2();
    lock (CustomTypes.memVector2)
    {
      inStream.Read(CustomTypes.memVector2, 0, 8);
      int offset = 0;
      Protocol.Deserialize(out vector2.x, CustomTypes.memVector2, ref offset);
      Protocol.Deserialize(out vector2.y, CustomTypes.memVector2, ref offset);
    }
    return (object) vector2;
  }

  private static short SerializeQuaternion(StreamBuffer outStream, object customobject)
  {
    Quaternion quaternion = (Quaternion) customobject;
    lock (CustomTypes.memQuarternion)
    {
      byte[] memQuarternion = CustomTypes.memQuarternion;
      int targetOffset = 0;
      Protocol.Serialize(quaternion.w, memQuarternion, ref targetOffset);
      Protocol.Serialize(quaternion.x, memQuarternion, ref targetOffset);
      Protocol.Serialize(quaternion.y, memQuarternion, ref targetOffset);
      Protocol.Serialize(quaternion.z, memQuarternion, ref targetOffset);
      outStream.Write(memQuarternion, 0, 16);
    }
    return 16;
  }

  private static object DeserializeQuaternion(StreamBuffer inStream, short length)
  {
    Quaternion quaternion = new Quaternion();
    lock (CustomTypes.memQuarternion)
    {
      inStream.Read(CustomTypes.memQuarternion, 0, 16);
      int offset = 0;
      Protocol.Deserialize(out quaternion.w, CustomTypes.memQuarternion, ref offset);
      Protocol.Deserialize(out quaternion.x, CustomTypes.memQuarternion, ref offset);
      Protocol.Deserialize(out quaternion.y, CustomTypes.memQuarternion, ref offset);
      Protocol.Deserialize(out quaternion.z, CustomTypes.memQuarternion, ref offset);
    }
    return (object) quaternion;
  }

  private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
  {
    int id = ((PhotonPlayer) customobject).ID;
    lock (CustomTypes.memPlayer)
    {
      byte[] memPlayer = CustomTypes.memPlayer;
      int targetOffset = 0;
      Protocol.Serialize(id, memPlayer, ref targetOffset);
      outStream.Write(memPlayer, 0, 4);
      return 4;
    }
  }

  private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
  {
    int key;
    lock (CustomTypes.memPlayer)
    {
      inStream.Read(CustomTypes.memPlayer, 0, (int) length);
      int offset = 0;
      Protocol.Deserialize(out key, CustomTypes.memPlayer, ref offset);
    }
    if (PhotonNetwork.networkingPeer.mActors.ContainsKey(key))
      return (object) PhotonNetwork.networkingPeer.mActors[key];
    return (object) null;
  }
}
