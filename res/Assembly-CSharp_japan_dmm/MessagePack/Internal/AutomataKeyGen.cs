// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.AutomataKeyGen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

#nullable disable
namespace MessagePack.Internal
{
  public static class AutomataKeyGen
  {
    private static MethodInfo dynamicGetKeyMethod;
    private static readonly object gate = new object();
    private static DynamicAssembly dynamicAssembly;

    public static MethodInfo GetGetKeyMethod()
    {
      if ((object) AutomataKeyGen.dynamicGetKeyMethod == null)
      {
        lock (AutomataKeyGen.gate)
        {
          if ((object) AutomataKeyGen.dynamicGetKeyMethod == null)
          {
            AutomataKeyGen.dynamicAssembly = new DynamicAssembly("AutomataKeyGenHelper");
            TypeBuilder type = AutomataKeyGen.dynamicAssembly.DefineType(nameof (AutomataKeyGen), TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Sealed, (Type) null);
            ILGenerator ilGenerator = type.DefineMethod("GetKey", MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig, typeof (ulong), new Type[2]
            {
              typeof (byte).MakePointerType().MakeByRefType(),
              typeof (int).MakeByRefType()
            }).GetILGenerator();
            ilGenerator.DeclareLocal(typeof (int));
            ilGenerator.DeclareLocal(typeof (ulong));
            ilGenerator.DeclareLocal(typeof (int));
            Label label1 = ilGenerator.DefineLabel();
            Label label2 = ilGenerator.DefineLabel();
            Label label3 = ilGenerator.DefineLabel();
            Label loc1 = ilGenerator.DefineLabel();
            Label loc2 = ilGenerator.DefineLabel();
            Label loc3 = ilGenerator.DefineLabel();
            Label loc4 = ilGenerator.DefineLabel();
            Label loc5 = ilGenerator.DefineLabel();
            Label loc6 = ilGenerator.DefineLabel();
            Label loc7 = ilGenerator.DefineLabel();
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldind_I4);
            ilGenerator.Emit(OpCodes.Ldc_I4_8);
            ilGenerator.Emit(OpCodes.Blt_S, label1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldind_I8);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldc_I4_8);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Br, label2);
            ilGenerator.MarkLabel(label1);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldind_I4);
            ilGenerator.Emit(OpCodes.Stloc_2);
            ilGenerator.Emit(OpCodes.Ldloc_2);
            ilGenerator.Emit(OpCodes.Switch, new Label[8]
            {
              label3,
              loc1,
              loc2,
              loc3,
              loc4,
              loc5,
              loc6,
              loc7
            });
            ilGenerator.Emit(OpCodes.Br, label3);
            ilGenerator.MarkLabel(loc1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldind_U1);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldc_I4_1);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Br, label2);
            ilGenerator.MarkLabel(loc2);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldind_U2);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldc_I4_2);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Br, label2);
            ilGenerator.MarkLabel(loc3);
            ilGenerator.DeclareLocal(typeof (ushort));
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldind_U1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldc_I4_1);
            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Ldind_U2);
            ilGenerator.Emit(OpCodes.Stloc_3);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Ldloc_3);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Ldc_I4_8);
            ilGenerator.Emit(OpCodes.Shl);
            ilGenerator.Emit(OpCodes.Or);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldc_I4_3);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Br, label2);
            ilGenerator.MarkLabel(loc4);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldind_U4);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldc_I4_4);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Br, label2);
            ilGenerator.MarkLabel(loc5);
            ilGenerator.DeclareLocal(typeof (uint));
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldind_U1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldc_I4_1);
            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Ldind_U4);
            ilGenerator.Emit(OpCodes.Stloc_S, 4);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Ldloc_S, 4);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Ldc_I4_8);
            ilGenerator.Emit(OpCodes.Shl);
            ilGenerator.Emit(OpCodes.Or);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldc_I4_5);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Br, label2);
            ilGenerator.MarkLabel(loc6);
            ilGenerator.DeclareLocal(typeof (ulong));
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldind_U2);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldc_I4_2);
            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Ldind_U4);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Stloc_S, 5);
            ilGenerator.Emit(OpCodes.Ldloc_S, 5);
            ilGenerator.Emit(OpCodes.Ldc_I4_S, 16);
            ilGenerator.Emit(OpCodes.Shl);
            ilGenerator.Emit(OpCodes.Or);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldc_I4_6);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Br, label2);
            ilGenerator.MarkLabel(loc7);
            ilGenerator.DeclareLocal(typeof (ushort));
            ilGenerator.DeclareLocal(typeof (uint));
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldind_U1);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldc_I4_1);
            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Ldind_U2);
            ilGenerator.Emit(OpCodes.Stloc_S, 6);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldc_I4_3);
            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Ldind_U4);
            ilGenerator.Emit(OpCodes.Stloc_S, 7);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Ldloc_S, 6);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Ldc_I4_8);
            ilGenerator.Emit(OpCodes.Shl);
            ilGenerator.Emit(OpCodes.Or);
            ilGenerator.Emit(OpCodes.Ldloc_S, 7);
            ilGenerator.Emit(OpCodes.Conv_U8);
            ilGenerator.Emit(OpCodes.Ldc_I4_S, 24);
            ilGenerator.Emit(OpCodes.Shl);
            ilGenerator.Emit(OpCodes.Or);
            ilGenerator.Emit(OpCodes.Stloc_1);
            ilGenerator.Emit(OpCodes.Ldc_I4_7);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Br, label2);
            ilGenerator.MarkLabel(label3);
            ilGenerator.Emit(OpCodes.Ldstr, "Not Supported Length");
            ilGenerator.Emit(OpCodes.Newobj, typeof (InvalidOperationException).GetConstructor(new Type[1]
            {
              typeof (string)
            }));
            ilGenerator.Emit(OpCodes.Throw);
            ilGenerator.MarkLabel(label2);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldind_I);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Stind_I);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldind_I4);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Sub);
            ilGenerator.Emit(OpCodes.Stind_I4);
            ilGenerator.Emit(OpCodes.Ldloc_1);
            ilGenerator.Emit(OpCodes.Ret);
            AutomataKeyGen.dynamicGetKeyMethod = ((IEnumerable<MethodInfo>) System.Reflection.ReflectionExtensions.CreateTypeInfo(type).AsType().GetMethods()).First<MethodInfo>();
          }
        }
      }
      return AutomataKeyGen.dynamicGetKeyMethod;
    }

    public static ulong GetKeySafe(byte[] bytes, ref int offset, ref int rest)
    {
      if (BitConverter.IsLittleEndian)
      {
        ulong keySafe;
        int num;
        if (rest >= 8)
        {
          keySafe = (ulong) ((long) bytes[offset] << 0 | (long) bytes[offset + 1] << 8 | (long) bytes[offset + 2] << 16 | (long) bytes[offset + 3] << 24 | (long) bytes[offset + 4] << 32 | (long) bytes[offset + 5] << 40 | (long) bytes[offset + 6] << 48 | (long) bytes[offset + 7] << 56);
          num = 8;
        }
        else
        {
          switch (rest)
          {
            case 1:
              keySafe = (ulong) bytes[offset];
              num = 1;
              break;
            case 2:
              keySafe = (ulong) ((long) bytes[offset] << 0 | (long) bytes[offset + 1] << 8);
              num = 2;
              break;
            case 3:
              keySafe = (ulong) ((long) bytes[offset] << 0 | (long) bytes[offset + 1] << 8 | (long) bytes[offset + 2] << 16);
              num = 3;
              break;
            case 4:
              keySafe = (ulong) ((long) bytes[offset] << 0 | (long) bytes[offset + 1] << 8 | (long) bytes[offset + 2] << 16 | (long) bytes[offset + 3] << 24);
              num = 4;
              break;
            case 5:
              keySafe = (ulong) ((long) bytes[offset] << 0 | (long) bytes[offset + 1] << 8 | (long) bytes[offset + 2] << 16 | (long) bytes[offset + 3] << 24 | (long) bytes[offset + 4] << 32);
              num = 5;
              break;
            case 6:
              keySafe = (ulong) ((long) bytes[offset] << 0 | (long) bytes[offset + 1] << 8 | (long) bytes[offset + 2] << 16 | (long) bytes[offset + 3] << 24 | (long) bytes[offset + 4] << 32 | (long) bytes[offset + 5] << 40);
              num = 6;
              break;
            case 7:
              keySafe = (ulong) ((long) bytes[offset] << 0 | (long) bytes[offset + 1] << 8 | (long) bytes[offset + 2] << 16 | (long) bytes[offset + 3] << 24 | (long) bytes[offset + 4] << 32 | (long) bytes[offset + 5] << 40 | (long) bytes[offset + 6] << 48);
              num = 7;
              break;
            default:
              throw new InvalidOperationException("Not Supported Length");
          }
        }
        offset += num;
        rest -= num;
        return keySafe;
      }
      ulong keySafe1;
      int num1;
      if (rest >= 8)
      {
        keySafe1 = (ulong) ((long) bytes[offset] << 56 | (long) bytes[offset + 1] << 48 | (long) bytes[offset + 2] << 40 | (long) bytes[offset + 3] << 32 | (long) bytes[offset + 4] << 24 | (long) bytes[offset + 5] << 16 | (long) bytes[offset + 6] << 8) | (ulong) bytes[offset + 7];
        num1 = 8;
      }
      else
      {
        switch (rest)
        {
          case 1:
            keySafe1 = (ulong) bytes[offset];
            num1 = 1;
            break;
          case 2:
            keySafe1 = (ulong) ((long) bytes[offset] << 8 | (long) bytes[offset + 1] << 0);
            num1 = 2;
            break;
          case 3:
            keySafe1 = (ulong) ((long) bytes[offset] << 16 | (long) bytes[offset + 1] << 8 | (long) bytes[offset + 2] << 0);
            num1 = 3;
            break;
          case 4:
            keySafe1 = (ulong) ((long) bytes[offset] << 24 | (long) bytes[offset + 1] << 16 | (long) bytes[offset + 2] << 8 | (long) bytes[offset + 3] << 0);
            num1 = 4;
            break;
          case 5:
            keySafe1 = (ulong) ((long) bytes[offset] << 32 | (long) bytes[offset + 1] << 24 | (long) bytes[offset + 2] << 16 | (long) bytes[offset + 3] << 8 | (long) bytes[offset + 4] << 0);
            num1 = 5;
            break;
          case 6:
            keySafe1 = (ulong) ((long) bytes[offset] << 40 | (long) bytes[offset + 1] << 32 | (long) bytes[offset + 2] << 24 | (long) bytes[offset + 3] << 16 | (long) bytes[offset + 4] << 8 | (long) bytes[offset + 5] << 0);
            num1 = 6;
            break;
          case 7:
            keySafe1 = (ulong) ((long) bytes[offset] << 48 | (long) bytes[offset + 1] << 40 | (long) bytes[offset + 2] << 32 | (long) bytes[offset + 3] << 24 | (long) bytes[offset + 4] << 16 | (long) bytes[offset + 5] << 8 | (long) bytes[offset + 6] << 0);
            num1 = 7;
            break;
          default:
            throw new InvalidOperationException("Not Supported Length");
        }
      }
      offset += num1;
      rest -= num1;
      return keySafe1;
    }

    public delegate ulong PointerDelegate<T>(ref T p, ref int rest);
  }
}
