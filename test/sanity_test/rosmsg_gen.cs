// Automatically generated, do not edit!

using System;
using System.IO;

namespace ros_csharp_interop.rosmsg.gen
{
namespace std_msgs
{
[ROSMsgInfo("std_msgs/Header","2176decaecbce78abc3b96ef049fabed","# Standard metadata for higher-level stamped data types.\n# This is generally used to communicate timestamped data \n# in a particular coordinate frame.\n# \n# sequence ID: consecutively increasing ID \nuint32 seq\n#Two-integer timestamp that is expressed as:\n# * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')\n# * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')\n# time-handling sugar is provided by the client library\ntime stamp\n#Frame this data is associated with\nstring frame_id\n")]
public class Header : ROSMsg
{
public string _type => "std_msgs/Header";
public string _md5sum => "2176decaecbce78abc3b96ef049fabed";
public string _full_text => "# Standard metadata for higher-level stamped data types.\n# This is generally used to communicate timestamped data \n# in a particular coordinate frame.\n# \n# sequence ID: consecutively increasing ID \nuint32 seq\n#Two-integer timestamp that is expressed as:\n# * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')\n# * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')\n# time-handling sugar is provided by the client library\ntime stamp\n#Frame this data is associated with\nstring frame_id\n";
public uint seq = default;
public ROSTime stamp = default;
public string frame_id = default;
public static Header ROSRead(BinaryReader reader)
{
var o = new Header();
o.seq = rosmsg_builtin_util.read_uint(reader);
o.stamp = rosmsg_builtin_util.read_ROSTime(reader);
o.frame_id = rosmsg_builtin_util.read_string(reader);
return o;
}
public static Header[] ROSReadArray(BinaryReader reader, int count)
{
if (count < 0) count = (int)reader.ReadUInt32();
var o = new Header[count];
for (int i=0; i<count; i++) o[i] = ROSRead(reader);
return o;
}
public static void ROSWrite(BinaryWriter writer, Header msg)
{
rosmsg_builtin_util.write_uint(writer, msg.seq);
rosmsg_builtin_util.write_ROSTime(writer, msg.stamp);
rosmsg_builtin_util.write_string(writer, msg.frame_id);
}
public static void ROSWriteArray(BinaryWriter writer, Header[] msg, int count)
{
rosmsg_builtin_util.do_write_count(writer,msg,count);
for (int i=0; i<msg.Length; i++) ROSWrite(writer, msg[i]);
}
}
}
namespace sensor_msgs
{
[ROSMsgInfo("sensor_msgs/JointState","3066dcd76a6cfaef579bd0f34173e9fd","# This is a message that holds data to describe the state of a set of torque controlled joints. \n#\n# The state of each joint (revolute or prismatic) is defined by:\n#  * the position of the joint (rad or m),\n#  * the velocity of the joint (rad/s or m/s) and \n#  * the effort that is applied in the joint (Nm or N).\n#\n# Each joint is uniquely identified by its name\n# The header specifies the time at which the joint states were recorded. All the joint states\n# in one message have to be recorded at the same time.\n#\n# This message consists of a multiple arrays, one for each part of the joint state. \n# The goal is to make each of the fields optional. When e.g. your joints have no\n# effort associated with them, you can leave the effort array empty. \n#\n# All arrays in this message should have the same size, or be empty.\n# This is the only way to uniquely associate the joint name with the correct\n# states.\n\n\nHeader header\n\nstring[] name\nfloat64[] position\nfloat64[] velocity\nfloat64[] effort\n\n================================================================================\nMSG: std_msgs/Header\n# Standard metadata for higher-level stamped data types.\n# This is generally used to communicate timestamped data \n# in a particular coordinate frame.\n# \n# sequence ID: consecutively increasing ID \nuint32 seq\n#Two-integer timestamp that is expressed as:\n# * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')\n# * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')\n# time-handling sugar is provided by the client library\ntime stamp\n#Frame this data is associated with\nstring frame_id\n")]
public class JointState : ROSMsg
{
public string _type => "sensor_msgs/JointState";
public string _md5sum => "3066dcd76a6cfaef579bd0f34173e9fd";
public string _full_text => "# This is a message that holds data to describe the state of a set of torque controlled joints. \n#\n# The state of each joint (revolute or prismatic) is defined by:\n#  * the position of the joint (rad or m),\n#  * the velocity of the joint (rad/s or m/s) and \n#  * the effort that is applied in the joint (Nm or N).\n#\n# Each joint is uniquely identified by its name\n# The header specifies the time at which the joint states were recorded. All the joint states\n# in one message have to be recorded at the same time.\n#\n# This message consists of a multiple arrays, one for each part of the joint state. \n# The goal is to make each of the fields optional. When e.g. your joints have no\n# effort associated with them, you can leave the effort array empty. \n#\n# All arrays in this message should have the same size, or be empty.\n# This is the only way to uniquely associate the joint name with the correct\n# states.\n\n\nHeader header\n\nstring[] name\nfloat64[] position\nfloat64[] velocity\nfloat64[] effort\n\n================================================================================\nMSG: std_msgs/Header\n# Standard metadata for higher-level stamped data types.\n# This is generally used to communicate timestamped data \n# in a particular coordinate frame.\n# \n# sequence ID: consecutively increasing ID \nuint32 seq\n#Two-integer timestamp that is expressed as:\n# * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')\n# * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')\n# time-handling sugar is provided by the client library\ntime stamp\n#Frame this data is associated with\nstring frame_id\n";
public std_msgs.Header header = default;
public string[] name = new string[0];
public double[] position = new double[0];
public double[] velocity = new double[0];
public double[] effort = new double[0];
public static JointState ROSRead(BinaryReader reader)
{
var o = new JointState();
o.header = std_msgs.Header.ROSRead(reader);
o.name = rosmsg_builtin_util.read_string_array(reader, -1);
o.position = rosmsg_builtin_util.read_double_array(reader, -1);
o.velocity = rosmsg_builtin_util.read_double_array(reader, -1);
o.effort = rosmsg_builtin_util.read_double_array(reader, -1);
return o;
}
public static JointState[] ROSReadArray(BinaryReader reader, int count)
{
if (count < 0) count = (int)reader.ReadUInt32();
var o = new JointState[count];
for (int i=0; i<count; i++) o[i] = ROSRead(reader);
return o;
}
public static void ROSWrite(BinaryWriter writer, JointState msg)
{
std_msgs.Header.ROSWrite(writer, msg.header);
rosmsg_builtin_util.write_string_array(writer, msg.name, -1);
rosmsg_builtin_util.write_double_array(writer, msg.position, -1);
rosmsg_builtin_util.write_double_array(writer, msg.velocity, -1);
rosmsg_builtin_util.write_double_array(writer, msg.effort, -1);
}
public static void ROSWriteArray(BinaryWriter writer, JointState[] msg, int count)
{
rosmsg_builtin_util.do_write_count(writer,msg,count);
for (int i=0; i<msg.Length; i++) ROSWrite(writer, msg[i]);
}
}
}
}
