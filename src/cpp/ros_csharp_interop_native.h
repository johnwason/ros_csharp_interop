#include <ros/ros.h>
#include <topic_tools/shape_shifter.h>

#include <memory>

#pragma once

namespace ros_csharp_interop
{

    void init_ros(std::vector<std::string> args, std::string name, bool anonymous_name);

    class subscriber_callback;
    class subscriber;

    class publisher;
    
    class interop_node : std::enable_shared_from_this<interop_node>
    {
    public:

        ros::NodeHandle nh;
        ros::AsyncSpinner spinner;

        interop_node();
                
        void start_spinner();
        
        void stop_spinner();

        std::shared_ptr<subscriber> subscribe(const std::string& topic, uint32_t queue_size, std::shared_ptr<subscriber_callback> callback);

        std::shared_ptr<publisher> advertise(const std::string& topic, uint32_t queue_size, bool latch,
            const std::string& md5sum, const std::string& data_type, const std::string& msg_def);

    };

    class subscriber_callback
    {
    public:

        virtual void callback(const std::string md5, void* msg_buf, size_t msg_len) = 0;
        virtual ~subscriber_callback() = default;
    };

    class subscriber
    {
    public:

        subscriber(ros::Subscriber subscriber_) : ros_subscriber(subscriber_) {}

        ros::Subscriber ros_subscriber;

        void shutdown();
    };

    class publisher
    {
    public:

        publisher(ros::Publisher publisher_, const std::string& md5sum, const std::string& data_type, const std::string& msg_def) 
            : ros_publisher(publisher_), data_type_(data_type), md5sum_(md5sum), msg_def_(msg_def) {}

        ros::Publisher ros_publisher;
        std::string data_type_;
        std::string md5sum_;
        std::string msg_def_;

        void publish(void* msg_buf, size_t msg_len);

        void shutdown();
    };
}
