%module(directors="1") ros_csharp_interop

%pragma(csharp) moduleclassmodifiers="public static partial class"

%include <stdint.i>
%include <std_shared_ptr.i>
%include <std_string.i>
%include <std_vector.i>
%include <exception.i>

%shared_ptr(ros_csharp_interop::interop_node)
%shared_ptr(ros_csharp_interop::subscriber_callback)
%shared_ptr(ros_csharp_interop::subscriber)
%shared_ptr(ros_csharp_interop::publisher)

%feature("director") ros_csharp_interop::subscriber_callback;

%typemap(csclassmodifiers) std::vector<std::string> "class";
%template(vectorstring) std::vector<std::string>;

%exception {
  try {
    $action
  } 
  SWIG_CATCH_STDEXCEPT  
}

%{
#include <ros_csharp_interop_native.h>
%}

namespace ros_csharp_interop
{
    %rename(_init_ros) init_ros;
    %csmethodmodifiers init_ros "internal";
    void init_ros(std::vector<std::string> args, std::string name, bool anonymous_name);

    %rename(ROSNode) interop_node;
    %typemap(csclassmodifiers) interop_node "public partial class";
    class interop_node
    {
    public:

                
        void start_spinner();
        
        void stop_spinner();

        %csmethodmodifiers subscribe "internal";
        std::shared_ptr<ros_csharp_interop::subscriber> subscribe(const std::string& topic, uint32_t queue_size, std::shared_ptr<ros_csharp_interop::subscriber_callback> callback);     

        %csmethodmodifiers advertise "internal";
        std::shared_ptr<ros_csharp_interop::publisher> advertise(const std::string& topic, uint32_t queue_size, bool latch,
            const std::string& md5sum, const std::string& data_type, const std::string& msg_def);

    };

    %apply void *VOID_INT_PTR { void * }

    %typemap(csclassmodifiers) subscriber_callback "class";
    class subscriber_callback
    {
    public:

        %csmethodmodifiers callback "public virtual unsafe";
        virtual void callback(const std::string md5, void* msg_buf, size_t msg_len) = 0;
        virtual ~subscriber_callback() = default;
    };

    %typemap(csclassmodifiers) subscriber "class";
    %nodefaultctor subscriber;
    class subscriber
    {
    public:

        void shutdown();
    };

    %typemap(csclassmodifiers) publisher "class";
    %nodefaultctor publisher;
    class publisher
    {
    public:        

        void publish(void* msg_buf, size_t msg_len);

        void shutdown();
    };
}