// Decompiled with JetBrains decompiler
// Type: CustomTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
    System.Type customType1 = typeof (Vector2);
    int num1 = 87;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomTypes.\u003C\u003Ef__mg\u0024cache0 = new SerializeStreamMethod(CustomTypes.SerializeVector2);
    }
    // ISSUE: reference to a compiler-generated field
    SerializeStreamMethod fMgCache0 = CustomTypes.\u003C\u003Ef__mg\u0024cache0;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomTypes.\u003C\u003Ef__mg\u0024cache1 = new DeserializeStreamMethod(CustomTypes.DeserializeVector2);
    }
    // ISSUE: reference to a compiler-generated field
    DeserializeStreamMethod fMgCache1 = CustomTypes.\u003C\u003Ef__mg\u0024cache1;
    PhotonPeer.RegisterType(customType1, (byte) num1, fMgCache0, fMgCache1);
    System.Type customType2 = typeof (Vector3);
    int num2 = 86;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomTypes.\u003C\u003Ef__mg\u0024cache2 = new SerializeStreamMethod(CustomTypes.SerializeVector3);
    }
    // ISSUE: reference to a compiler-generated field
    SerializeStreamMethod fMgCache2 = CustomTypes.\u003C\u003Ef__mg\u0024cache2;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomTypes.\u003C\u003Ef__mg\u0024cache3 = new DeserializeStreamMethod(CustomTypes.DeserializeVector3);
    }
    // ISSUE: reference to a compiler-generated field
    DeserializeStreamMethod fMgCache3 = CustomTypes.\u003C\u003Ef__mg\u0024cache3;
    PhotonPeer.RegisterType(customType2, (byte) num2, fMgCache2, fMgCache3);
    System.Type customType3 = typeof (Quaternion);
    int num3 = 81;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomTypes.\u003C\u003Ef__mg\u0024cache4 = new SerializeStreamMethod(CustomTypes.SerializeQuaternion);
    }
    // ISSUE: reference to a compiler-generated field
    SerializeStreamMethod fMgCache4 = CustomTypes.\u003C\u003Ef__mg\u0024cache4;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomTypes.\u003C\u003Ef__mg\u0024cache5 = new DeserializeStreamMethod(CustomTypes.DeserializeQuaternion);
    }
    // ISSUE: reference to a compiler-generated field
    DeserializeStreamMethod fMgCache5 = CustomTypes.\u003C\u003Ef__mg\u0024cache5;
    PhotonPeer.RegisterType(customType3, (byte) num3, fMgCache4, fMgCache5);
    System.Type customType4 = typeof (PhotonPlayer);
    int num4 = 80;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomTypes.\u003C\u003Ef__mg\u0024cache6 = new SerializeStreamMethod(CustomTypes.SerializePhotonPlayer);
    }
    // ISSUE: reference to a compiler-generated field
    SerializeStreamMethod fMgCache6 = CustomTypes.\u003C\u003Ef__mg\u0024cache6;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomTypes.\u003C\u003Ef__mg\u0024cache7 = new DeserializeStreamMethod(CustomTypes.DeserializePhotonPlayer);
    }
    // ISSUE: reference to a compiler-generated field
    DeserializeStreamMethod fMgCache7 = CustomTypes.\u003C\u003Ef__mg\u0024cache7;
    PhotonPeer.RegisterType(customType4, (byte) num4, fMgCache6, fMgCache7);
  }

  private static short SerializeVector3(StreamBuffer outStream, object customobject)
  {
    Vector3 vector3 = (Vector3) customobject;
    int targetOffset = 0;
    lock ((object) CustomTypes.memVector3)
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
    lock ((object) CustomTypes.memVector3)
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
    lock ((object) CustomTypes.memVector2)
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
    lock ((object) CustomTypes.memVector2)
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
    lock ((object) CustomTypes.memQuarternion)
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
    lock ((object) CustomTypes.memQuarternion)
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
    lock ((object) CustomTypes.memPlayer)
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
    lock ((object) CustomTypes.memPlayer)
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
