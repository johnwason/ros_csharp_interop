using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ros_csharp_interop;
using ros_csharp_interop.rosmsg.gen;
using ros_csharp_interop.rosmsg.gen.std_msgs;
using ros_csharp_interop.rosmsg.gen.sensor_msgs;

namespace JointStateSanityTest
{
    class Program
    {
        static Publisher<JointState> pub;

        static public void Main(String[] args)
        {
            ros_csharp_interop.ros_csharp_interop.init_ros(args, "JointStateSanityTest", true);

            using (var node = new ROSNode())
            {
                using (var sub = node.subscribe<JointState>("joint_states", 1, joint_states_cb))
                using (pub = node.advertise<JointState>("joint_states2", 1, false))
                {
                    node.start_spinner();
                    Console.WriteLine("Press enter to quit");
                    Console.ReadKey();
                }                
            }
        }

        static void joint_states_cb(JointState joint_states)
        {
            Console.WriteLine("Got joint_states message: {0}", String.Join(", ", joint_states.position.Select(x => x.ToString())));

            var js2 = new JointState();
            js2.header = new Header();
            js2.name = new string[] { "joint_1", "joint_2" };
            js2.position = new double[] { joint_states.position[0]*2.0, joint_states.position[1]*4.0 };

            pub.publish(js2);
        }
    }
}
