using System;
using System.Collections.Generic;
using System.Text;

namespace ros_csharp_interop
{
    public static partial class ros_csharp_interop
    {
        public static void init_ros(string[] args, string name, bool anonymous_name = false)
        {
            using (var args1 = new vectorstring())
            {
                foreach (var a in args)
                {
                    args1.Add(a);
                }

                _init_ros(args1, name, anonymous_name);
            }
        }
    }

}