# Generates csharp message serialization

import re
import sys
import importlib
import StringIO

def main():
    argv = sys.argv
    outfile = argv[1]
    msg_names = argv[2:]

    msg_types = []
    for msg_name in msg_names:
        m = re.match(r"^([A-Za-z]\w+)/([A-Za-z]\w+)$", msg_name)
        assert m is not None, "Invalid message name specified"
        p_name = m.group(1)
        m_name = m.group(2)

        msg_module = importlib.import_module(p_name + ".msg")
        msg_type = getattr(msg_module, m_name)
        msg_types.append(msg_type)

    o = StringIO.StringIO()

    o.write("// Automatically generated, do not edit!\n")
    o.write("\n")
    o.write("using System;\n")
    o.write("using System.IO;\n")
    o.write("\n")
    o.write("namespace ros_csharp_interop.rosmsg.gen\n")
    o.write("{\n")
    for m in msg_types:
        m1 = re.match(r"^([A-Za-z]\w+)/([A-Za-z]\w+)$", m._type)
        o.write("namespace %s\n" % m1.group(1))
        o.write("{\n")
        o.write("[ROSMsgInfo(\"%s\",\"%s\",\"%s\")]\n" % (m._type, m._md5sum, m._full_text.replace("\n", "\\n").replace("\"", "\\\"")))
        o.write("public class %s : ROSMsg\n" % m1.group(2))
        o.write("{\n")
        o.write("public string _type => \"%s\";\n" % m._type)
        o.write("public string _md5sum => \"%s\";\n" % m._md5sum)
        o.write("public string _full_text => \"%s\";\n" % m._full_text.replace("\n", "\\n").replace("\"", "\\\""))
        for i in range(len(m.__slots__)):
            slot_name = m.__slots__[i]
            slot_type1 = m._slot_types[i]
            slot_type, slot_is_array, slot_is_builtin = convert_ros_type(slot_type1)
            if slot_is_array is None:
                o.write("public %s %s = default;\n" % (slot_type, slot_name))
            elif slot_is_array == -1:
                o.write("public %s[] %s = new %s[0];\n" % (slot_type, slot_name, slot_type))
            else:
                o.write("public %s[] %s = new %s[%d];\n" % (slot_type, slot_name, slot_type, slot_is_array))

        o.write("public static %s ROSRead(BinaryReader reader)\n" % m1.group(2))
        o.write("{\n")
        o.write("var o = new %s();\n" % m1.group(2))
        for i in range(len(m.__slots__)):
            slot_name = m.__slots__[i]
            slot_type1 = m._slot_types[i]
            slot_type, slot_is_array, slot_is_builtin = convert_ros_type(slot_type1)
            if (slot_is_builtin):
                if (slot_is_array is None):
                    o.write("o.%s = rosmsg_builtin_util.read_%s(reader);\n" % (slot_name,slot_type))
                else:
                    o.write("o.%s = rosmsg_builtin_util.read_%s_array(reader, %d);\n" % (slot_name,slot_type,slot_is_array))
            else:
                if (slot_is_array is None):
                    o.write("o.%s = %s.ROSRead(reader);\n" % (slot_name,slot_type))
                else:
                    o.write("o.%s = %s.ROSReadArray(reader, %d);\n" % (slot_name,slot_type,slot_is_array))            
        o.write("return o;\n")
        o.write("}\n")
        o.write("public static %s[] ROSReadArray(BinaryReader reader, int count)\n" % m1.group(2))
        o.write("{\n")
        o.write("if (count < 0) count = (int)reader.ReadUInt32();\n")
        o.write("var o = new %s[count];\n" % m1.group(2))
        o.write("for (int i=0; i<count; i++) o[i] = ROSRead(reader);\n")
        o.write("return o;\n")
        o.write("}\n")
        o.write("public static void ROSWrite(BinaryWriter writer, %s msg)\n" % m1.group(2))
        o.write("{\n")        
        for i in range(len(m.__slots__)):
            slot_name = m.__slots__[i]
            slot_type1 = m._slot_types[i]
            slot_type, slot_is_array, slot_is_builtin = convert_ros_type(slot_type1)
            if (slot_is_builtin):
                if (slot_is_array is None):
                    o.write("rosmsg_builtin_util.write_%s(writer, msg.%s);\n" % (slot_type,slot_name))
                else:
                    o.write("rosmsg_builtin_util.write_%s_array(writer, msg.%s, %d);\n" % (slot_type,slot_name,slot_is_array))
            else:
                if (slot_is_array is None):
                    o.write("%s.ROSWrite(writer, msg.%s);\n" % (slot_type,slot_name))
                else:
                    o.write("%s.ROSWriteArray(writer, msg.%s, %d);\n" % (slot_type,slot_name,slot_is_array))            
        o.write("}\n")
        o.write("public static void ROSWriteArray(BinaryWriter writer, %s[] msg, int count)\n" % m1.group(2))
        o.write("{\n")
        o.write("rosmsg_builtin_util.do_write_count(writer,msg,count);\n")       
        o.write("for (int i=0; i<msg.Length; i++) ROSWrite(writer, msg[i]);\n")        
        o.write("}\n")

        o.write("}\n")
        o.write("}\n")


    o.write("}\n")
    with open(outfile, "w") as f:
        f.write(o.getvalue())

def convert_ros_type(ros_type):

    m = re.match("^([A-Za-z]\w+)(?:/([A-Za-z]\w+))?(\[(\d+)?\])?$", ros_type)

    assert m is not None, "Invalid ros type %s" % ros_type
    if m.group(3) is None:
        is_array = None
    elif m.group(4) is None:
        is_array = -1
    else:
        is_array = int(m.group(4))
    if m.group(2) is not None:
        return m.group(1) + "." + m.group(2), is_array, False

    t = m.group(1)
    if t == "bool":
        return "bool", is_array, True
    elif t == "int8":
        return "sbyte", is_array, True
    elif t == "uint8":
        return "byte", is_array, True
    elif t == "int16":
        return "short", is_array, True
    elif t == "uint16":
        return "ushort", is_array, True
    elif t == "int32":
        return "int", is_array, True
    elif t == "uint32":
        return "uint", is_array, True
    elif t == "int64":
        return "long", is_array, True
    elif t == "uint64":
        return "ulong", is_array, True
    elif t == "float32":
        return "float", is_array, True
    elif t == "float64":
        return "double", is_array, True
    elif t == "string":
        return "string", is_array, True
    elif t == "time":
        return "ROSTime", is_array, True
    elif t == "duration":
        return "ROSDuration", is_array, True

    return t, is_array, True

if __name__ == "__main__":
    main()