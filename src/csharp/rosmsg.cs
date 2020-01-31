using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ros_csharp_interop.rosmsg
{

    public interface ROSMsg
    {
        string _type { get; }
        string _md5sum { get; }
        string _full_text { get; }

    }

    public struct ROSTime
    {
        public uint secs;
        public uint nsecs;
    }

    public struct ROSDuration
    {
        public int secs;
        public int nsecs;
    }

    public static class rosmsg_builtin_util
    {
        public static double read_double(BinaryReader reader)
        {
            return reader.ReadDouble();
        }

        public static double[] read_double_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new double[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadDouble();
            }
            return o;
        }

        public static float read_float(BinaryReader reader)
        {
            return reader.ReadSingle();
        }

        public static float[] read_float_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadSingle();
            }
            var o = new float[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadSingle();
            }
            return o;
        }

        public static byte read_byte(BinaryReader reader)
        {
            return reader.ReadByte();
        }

        public static byte[] read_byte_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new byte[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadByte();
            }
            return o;
        }

        public static sbyte read_sbyte(BinaryReader reader)
        {
            return reader.ReadSByte();
        }

        public static sbyte[] read_sbyte_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new sbyte[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadSByte();
            }
            return o;
        }

        public static short read_short(BinaryReader reader)
        {
            return reader.ReadInt16();
        }

        public static short[] read_short_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new short[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadInt16();
            }
            return o;
        }

        public static ushort read_ushort(BinaryReader reader)
        {
            return reader.ReadUInt16();
        }

        public static ushort[] read_ushort_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new ushort[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadUInt16();
            }
            return o;
        }

        public static int read_int(BinaryReader reader)
        {
            return reader.ReadInt32();
        }

        public static int[] read_int_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new int[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadInt32();
            }
            return o;
        }

        public static uint read_uint(BinaryReader reader)
        {
            return reader.ReadUInt32();
        }

        public static uint[] read_uint_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new uint[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadUInt32();
            }
            return o;
        }

        public static long read_long(BinaryReader reader)
        {
            return reader.ReadInt64();
        }
        public static long[] read_long_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new long[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadInt64();
            }
            return o;
        }

        public static ulong read_ulong(BinaryReader reader)
        {
            return reader.ReadUInt64();
        }

        public static ulong[] read_ulong_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new ulong[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = reader.ReadUInt64();
            }
            return o;
        }

        public static bool read_bool(BinaryReader reader)
        {
            return reader.ReadByte() != 0;
        }

        public static bool[] read_bool_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new bool[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = read_bool(reader);
            }
            return o;
        }

        public static string read_string(BinaryReader reader)
        {
            uint len = reader.ReadUInt32();
            var raw_utf8 = reader.ReadBytes((int)len);
            return System.Text.UTF8Encoding.UTF8.GetString(raw_utf8);
        }

        public static string[] read_string_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new string[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = read_string(reader);
            }
            return o;
        }

        public static ROSTime read_ROSTime(BinaryReader reader)
        {
            var o = new ROSTime();
            o.secs = reader.ReadUInt32();
            o.nsecs = reader.ReadUInt32();
            return o;
        }

        public static ROSTime[] read_ROSTime_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new ROSTime[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = read_ROSTime(reader);
            }
            return o;
        }

        public static ROSDuration read_ROSDuration(BinaryReader reader)
        {
            var o = new ROSDuration();
            o.secs = reader.ReadInt32();
            o.nsecs = reader.ReadInt32();
            return o;
        }

        public static ROSDuration[] read_ROSDuration_array(BinaryReader reader, int count = -1)
        {
            if (count < 0)
            {
                count = (int)reader.ReadUInt32();
            }
            var o = new ROSDuration[count];
            for (uint i = 0; i < count; i++)
            {
                o[i] = read_ROSDuration(reader);
            }
            return o;
        }
        
        public static void do_write_count(BinaryWriter writer, Array val, int count)
        {
            if (count < 0)
            {
                writer.Write((uint)val.Length);
            }
            else
            {
                if (val.Length != count)
                {
                    throw new ArgumentException("Expected fixed length ROS field");
                }
            }
        }

        public static void write_double(BinaryWriter writer, double val)
        {
            writer.Write(val);
        }

        public static void write_double_array(BinaryWriter writer, double[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }           
        }

        public static void write_float(BinaryWriter writer, float val)
        {
            writer.Write(val);
        }

        public static void write_float_array(BinaryWriter writer, float[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_sbyte(BinaryWriter writer, sbyte val)
        {
            writer.Write(val);
        }

        public static void write_sbyte_array(BinaryWriter writer, sbyte[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_byte(BinaryWriter writer, byte val)
        {
            writer.Write(val);
        }

        public static void write_byte_array(BinaryWriter writer, byte[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_short(BinaryWriter writer, short val)
        {
            writer.Write(val);
        }

        public static void write_short_array(BinaryWriter writer, short[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_ushort(BinaryWriter writer, ushort val)
        {
            writer.Write(val);
        }

        public static void write_ushort_array(BinaryWriter writer, ushort[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_int(BinaryWriter writer, int val)
        {
            writer.Write(val);
        }

        public static void write_int_array(BinaryWriter writer, int[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_uint(BinaryWriter writer, uint val)
        {
            writer.Write(val);
        }

        public static void write_uint_array(BinaryWriter writer, uint[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_long(BinaryWriter writer, long val)
        {
            writer.Write(val);
        }

        public static void write_long_array(BinaryWriter writer, long[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_ulong(BinaryWriter writer, ulong val)
        {
            writer.Write(val);
        }

        public static void write_ulong_array(BinaryWriter writer, ulong[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                writer.Write(val[i]);
            }
        }

        public static void write_bool(BinaryWriter writer, bool val)
        {
            writer.Write((byte)(val ? 1 : 0));
        }

        public static void write_bool_array(BinaryWriter writer, bool[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                write_bool(writer, val[i]);
            }
        }

        public static void write_string(BinaryWriter writer, string val)
        {
            byte[] utf8_bytes = System.Text.UTF8Encoding.UTF8.GetBytes(val ?? "");
            writer.Write((uint)utf8_bytes.Length);
            writer.Write(utf8_bytes);
        }

        public static void write_string_array(BinaryWriter writer, string[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                write_string(writer,val[i]);
            }
        }

        public static void write_ROSTime(BinaryWriter writer, ROSTime val)
        {
            writer.Write(val.secs);
            writer.Write(val.nsecs);            
        }

        public static void write_ROSTime_array(BinaryWriter writer, ROSTime[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                write_ROSTime(writer, val[i]);
            }
        }

        public static void write_ROSDuration(BinaryWriter writer, ROSDuration val)
        {
            writer.Write(val.secs);
            writer.Write(val.nsecs);
            
        }

        public static void write_ROSDuration_array(BinaryWriter writer, ROSDuration[] val, int count = -1)
        {
            do_write_count(writer, val, count);
            for (uint i = 0; i < val.Length; i++)
            {
                write_ROSDuration(writer, val[i]);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ROSMsgInfo : Attribute
    {
        public ROSMsgInfo (string type, string md5sum, string full_text)
        {
            this.type = type;
            this.md5sum = md5sum;
            this.full_text = full_text;
        }

        public string type;
        public string md5sum;
        public string full_text;
    }
}
