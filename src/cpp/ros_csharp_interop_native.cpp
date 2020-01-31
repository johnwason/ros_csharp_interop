#include "ros_csharp_interop_native.h"

namespace ros_csharp_interop
{
    void init_ros(std::vector<std::string> args, std::string name, bool anonymous_name)
    {
        std::vector<char*> args2;
        for(auto& s : args)
        {
            args2.push_back(&s[0]);
        }
        int argc = (int)args2.size();
        char** argv = &args2[0];

        uint32_t options = ros::init_options::NoRosout | ros::init_options::NoSigintHandler;
        if (anonymous_name)
        {
            options |= ros::init_options::AnonymousName;
        }

        ros::init(argc, argv, name, options);
    }

    void interop_node::start_spinner()
    {
        spinner.start();
    }

    void interop_node::stop_spinner()
    {
        spinner.stop();
    }

    interop_node::interop_node()
        : spinner(4)
    {

    }

    std::shared_ptr<subscriber> interop_node::subscribe(const std::string& topic, uint32_t queue_size, std::shared_ptr<subscriber_callback> callback)
    {
        auto sub = nh.subscribe<topic_tools::ShapeShifter>(topic, queue_size, [callback](const topic_tools::ShapeShifter::ConstPtr& msg)
        {
            size_t len2 = msg->size();
            std::unique_ptr<uint8_t[]> buf2(new uint8_t[len2]);
            ros::serialization::OStream ostream(buf2.get(), len2);
            msg->write(ostream);
            const std::string& md5 = msg->getMD5Sum();

            callback->callback(md5, buf2.get(), len2);
        });

        return std::make_shared<subscriber>(sub);
    }

    std::shared_ptr<publisher> interop_node::advertise(const std::string& topic, uint32_t queue_size, bool latch,
            const std::string& md5sum, const std::string& data_type, const std::string& msg_def)
    {
        topic_tools::ShapeShifter ss;
        ss.morph(md5sum, data_type, msg_def, "");
        auto pub = ss.advertise(nh, topic, queue_size, latch);

        return std::make_shared<publisher>(pub, md5sum, data_type, msg_def);
    }

    void subscriber::shutdown()
    {
        ros_subscriber.shutdown();
    }

    void publisher::publish(void* msg_buf, size_t msg_len)
    {
        topic_tools::ShapeShifter ss;
        ss.morph(md5sum_, data_type_, msg_def_, "");
        ros::serialization::IStream istream((uint8_t*)msg_buf, msg_len);
        ss.read(istream);

        ros_publisher.publish(ss);
    }

    void publisher::shutdown()
    {
        ros_publisher.shutdown();
    }

}

