// Decompiled with JetBrains decompiler
// Type: CustomTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using UnityEngine;

#nullable disable
internal static class CustomTypes
{
  public static readonly byte[] memVector3 = new byte[12];
  public static readonly byte[] memVector2 = new byte[8];
  public static readonly byte[] memQuarternion = new byte[16];
  public static readonly byte[] memPlayer = new byte[4];

  internal static void Register()
  {
    System.Type type1 = typeof (Vector2);
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      CustomTypes.\u003C\u003Ef__mg\u0024cache0 = new SerializeStreamMethod((object) null, __methodptr(SerializeVector2));
    }
    // ISSUE: reference to a compiler-generated field
    SerializeStreamMethod fMgCache0 = CustomTypes.\u003C\u003Ef__mg\u0024cache0;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      CustomTypes.\u003C\u003Ef__mg\u0024cache1 = new DeserializeStreamMethod((object) null, __methodptr(DeserializeVector2));
    }
    // ISSUE: reference to a compiler-generated field
    DeserializeStreamMethod fMgCache1 = CustomTypes.\u003C\u003Ef__mg\u0024cache1;
    PhotonPeer.RegisterType(type1, (byte) 87, fMgCache0, fMgCache1);
    System.Type type2 = typeof (Vector3);
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      CustomTypes.\u003C\u003Ef__mg\u0024cache2 = new SerializeStreamMethod((object) null, __methodptr(SerializeVector3));
    }
    // ISSUE: reference to a compiler-generated field
    SerializeStreamMethod fMgCache2 = CustomTypes.\u003C\u003Ef__mg\u0024cache2;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      CustomTypes.\u003C\u003Ef__mg\u0024cache3 = new DeserializeStreamMethod((object) null, __methodptr(DeserializeVector3));
    }
    // ISSUE: reference to a compiler-generated field
    DeserializeStreamMethod fMgCache3 = CustomTypes.\u003C\u003Ef__mg\u0024cache3;
    PhotonPeer.RegisterType(type2, (byte) 86, fMgCache2, fMgCache3);
    System.Type type3 = typeof (Quaternion);
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      CustomTypes.\u003C\u003Ef__mg\u0024cache4 = new SerializeStreamMethod((object) null, __methodptr(SerializeQuaternion));
    }
    // ISSUE: reference to a compiler-generated field
    SerializeStreamMethod fMgCache4 = CustomTypes.\u003C\u003Ef__mg\u0024cache4;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      CustomTypes.\u003C\u003Ef__mg\u0024cache5 = new DeserializeStreamMethod((object) null, __methodptr(DeserializeQuaternion));
    }
    // ISSUE: reference to a compiler-generated field
    DeserializeStreamMethod fMgCache5 = CustomTypes.\u003C\u003Ef__mg\u0024cache5;
    PhotonPeer.RegisterType(type3, (byte) 81, fMgCache4, fMgCache5);
    System.Type type4 = typeof (PhotonPlayer);
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      CustomTypes.\u003C\u003Ef__mg\u0024cache6 = new SerializeStreamMethod((object) null, __methodptr(SerializePhotonPlayer));
    }
    // ISSUE: reference to a compiler-generated field
    SerializeStreamMethod fMgCache6 = CustomTypes.\u003C\u003Ef__mg\u0024cache6;
    // ISSUE: reference to a compiler-generated field
    if (CustomTypes.\u003C\u003Ef__mg\u0024cache7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      CustomTypes.\u003C\u003Ef__mg\u0024cache7 = new DeserializeStreamMethod((object) null, __methodptr(DeserializePhotonPlayer));
    }
    // ISSUE: reference to a compiler-generated field
    DeserializeStreamMethod fMgCache7 = CustomTypes.\u003C\u003Ef__mg\u0024cache7;
    PhotonPeer.RegisterType(type4, (byte) 80, fMgCache6, fMgCache7);
  }

  private static short SerializeVector3(StreamBuffer outStream, object customobject)
  {
    Vector3 vector3 = (Vector3) customobject;
    int num = 0;
    lock ((object) CustomTypes.memVector3)
    {
      byte[] memVector3 = CustomTypes.memVector3;
      Protocol.Serialize(vector3.x, memVector3, ref num);
      Protocol.Serialize(vector3.y, memVector3, ref num);
      Protocol.Serialize(vector3.z, memVector3, ref num);
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
      int num = 0;
      Protocol.Deserialize(ref vector3.x, CustomTypes.memVector3, ref num);
      Protocol.Deserialize(ref vector3.y, CustomTypes.memVector3, ref num);
      Protocol.Deserialize(ref vector3.z, CustomTypes.memVector3, ref num);
    }
    return (object) vector3;
  }

  private static short SerializeVector2(StreamBuffer outStream, object customobject)
  {
    Vector2 vector2 = (Vector2) customobject;
    lock ((object) CustomTypes.memVector2)
    {
      byte[] memVector2 = CustomTypes.memVector2;
      int num = 0;
      Protocol.Serialize(vector2.x, memVector2, ref num);
      Protocol.Serialize(vector2.y, memVector2, ref num);
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
      int num = 0;
      Protocol.Deserialize(ref vector2.x, CustomTypes.memVector2, ref num);
      Protocol.Deserialize(ref vector2.y, CustomTypes.memVector2, ref num);
    }
    return (object) vector2;
  }

  private static short SerializeQuaternion(StreamBuffer outStream, object customobject)
  {
    Quaternion quaternion = (Quaternion) customobject;
    lock ((object) CustomTypes.memQuarternion)
    {
      byte[] memQuarternion = CustomTypes.memQuarternion;
      int num = 0;
      Protocol.Serialize(quaternion.w, memQuarternion, ref num);
      Protocol.Serialize(quaternion.x, memQuarternion, ref num);
      Protocol.Serialize(quaternion.y, memQuarternion, ref num);
      Protocol.Serialize(quaternion.z, memQuarternion, ref num);
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
      int num = 0;
      Protocol.Deserialize(ref quaternion.w, CustomTypes.memQuarternion, ref num);
      Protocol.Deserialize(ref quaternion.x, CustomTypes.memQuarternion, ref num);
      Protocol.Deserialize(ref quaternion.y, CustomTypes.memQuarternion, ref num);
      Protocol.Deserialize(ref quaternion.z, CustomTypes.memQuarternion, ref num);
    }
    return (object) quaternion;
  }

  private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
  {
    int id = ((PhotonPlayer) customobject).ID;
    lock ((object) CustomTypes.memPlayer)
    {
      byte[] memPlayer = CustomTypes.memPlayer;
      int num = 0;
      Protocol.Serialize(id, memPlayer, ref num);
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
      int num = 0;
      Protocol.Deserialize(ref key, CustomTypes.memPlayer, ref num);
    }
    return PhotonNetwork.networkingPeer.mActors.ContainsKey(key) ? (object) PhotonNetwork.networkingPeer.mActors[key] : (object) null;
  }
}
