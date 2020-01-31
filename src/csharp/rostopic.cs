using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

using ros_csharp_interop;
using ros_csharp_interop.rosmsg;

namespace ros_csharp_interop
{
    class subscriber_callback1<T> : subscriber_callback
    {
        string expected_md5sum;
        Func<BinaryReader, T> read_func;
        Action<T> callback_action;
        internal subscriber_callback1(Action<T> callback)
        {
            var msg_info = (ROSMsgInfo)Attribute.GetCustomAttribute(typeof(T), typeof(ROSMsgInfo));

            expected_md5sum = msg_info.md5sum;

            var rosread_method = typeof(T).GetMethod("ROSRead");
            read_func = (Func<BinaryReader, T>)Delegate.CreateDelegate(typeof(Func<BinaryReader, T>), rosread_method);
            Debug.Assert(read_func != null);
            this.callback_action = callback;
        }

        public override unsafe void callback(string md5, IntPtr msg_buf, uint msg_len)
        {
            if (md5 != expected_md5sum)
            {
                Debug.WriteLine("Got unexpected md5sum message expected {0} got {1} dropping message", expected_md5sum, md5);
                return;
            }

            T o;

            using (var unsafe_reader = new System.IO.UnmanagedMemoryStream((byte*)msg_buf, msg_len, msg_len, FileAccess.Read))
            {
                var binary_reader = new BinaryReader(unsafe_reader);

                o = read_func(binary_reader);
            }

            try
            {
                callback_action(o);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error occured in callback method {0}", e.ToString());
                return;
            }
        }



    }

    public class Subscriber<T> : IDisposable
    {
        subscriber_callback1<T> callback;
        subscriber ros_subscriber;

        internal Subscriber(subscriber ros_subscriber, subscriber_callback1<T> callback)
        {
            this.callback = callback;
            this.ros_subscriber = ros_subscriber;
        }

        public void Dispose()
        {
            ros_subscriber?.Dispose();
            callback?.Dispose();
        }

        ~Subscriber()
        {
            Dispose();
        }
    }

    public class Publisher<T> : IDisposable
    {
        publisher ros_publisher;
        Action<BinaryWriter, T> write_func;

        internal Publisher(publisher ros_publisher)
        {
            this.ros_publisher = ros_publisher;
            var roswrite_method = typeof(T).GetMethod("ROSWrite");
            write_func = (Action<BinaryWriter, T>)Delegate.CreateDelegate(typeof(Action<BinaryWriter, T>), roswrite_method);
            Debug.Assert(write_func != null);
        }

        public unsafe void publish(T msg)
        {
            var mem_writer = new MemoryStream();
            var binary_writer = new BinaryWriter(mem_writer);
            write_func(binary_writer, msg);

            byte[] writer_dat = mem_writer.ToArray();
            fixed (byte* writer_ptr = writer_dat)
            {
                ros_publisher.publish((IntPtr)writer_ptr, (uint)writer_dat.Length);
            }
        }

        public void Dispose()
        {
            ros_publisher?.Dispose();
        }

        ~Publisher()
        {
            Dispose();
        }
    }

    public partial class ROSNode
    {
        public Subscriber<T> subscribe<T>(string topic, uint queue_size, Action<T> callback)
        {
            var cb1 = new subscriber_callback1<T>(callback);
            var ros_sub = subscribe(topic, queue_size, cb1);
            return new Subscriber<T>(ros_sub, cb1);
        }

        public Publisher<T> advertise<T>(string topic, uint queue_size, bool latch)
        {
            var msg_info = (ROSMsgInfo)Attribute.GetCustomAttribute(typeof(T), typeof(ROSMsgInfo));

            var ros_pub = advertise(topic, queue_size, latch, msg_info.md5sum, msg_info.type, msg_info.full_text);

            return new Publisher<T>(ros_pub);
        }
    }
}
