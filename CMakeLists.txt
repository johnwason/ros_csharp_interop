cmake_minimum_required(VERSION 3.5)
project(ros_csharp_interop)

find_package(roscpp REQUIRED)
find_package(std_msgs REQUIRED)
find_package(topic_tools REQUIRED)

find_package(SWIG 4.0 REQUIRED)
include(UseSWIG)

find_program(DOTNET dotnet)
message(STATUS "Using DOTNET: ${DOTNET}")

include(GNUInstallDirs)

include_directories(
  ${CMAKE_CURRENT_SOURCE_DIR}/src/cpp
  ${roscpp_INCLUDE_DIRS}
  ${std_msgs_INCLUDE_DIRS}
  ${topic_tools_INCLUDE_DIRS}
)

SET(SWIG_CXX_EXTENSION cxx)
set_property(SOURCE ${CMAKE_CURRENT_SOURCE_DIR}/src/cpp/ros_csharp_interop.i PROPERTY CPLUSPLUS ON)
set_property(SOURCE ${CMAKE_CURRENT_SOURCE_DIR}/src/cpp/ros_csharp_interop.i PROPERTY SWIG_FLAGS -namespace ros_csharp_interop -outfile ros_csharp_interop_SWIG.cs)
#set(CMAKE_SWIG_OUTDIR ${CMAKE_CURRENT_BINARY_DIR})

swig_add_library(ros_csharp_interop_native LANGUAGE CSHARP TYPE SHARED
  SOURCES ${CMAKE_CURRENT_SOURCE_DIR}/src/cpp/ros_csharp_interop.i ${CMAKE_CURRENT_SOURCE_DIR}/src/cpp/ros_csharp_interop_native.cpp
)

set_target_properties(ros_csharp_interop_native PROPERTIES PREFIX "lib")

target_link_libraries(ros_csharp_interop_native
  ${roscpp_LIBRARIES} ${std_msgs_LIBRARIES} ${topic_tools_LIBRARIES}
)

add_custom_target(ros_csharp_interop COMMAND 
    ${DOTNET} build ${CMAKE_CURRENT_SOURCE_DIR}/src/csharp/ros_csharp_interop.csproj 
    -o ${CMAKE_CURRENT_BINARY_DIR} -p:ROS_BUILD_DIR=${CMAKE_CURRENT_BINARY_DIR}
    DEPENDS ros_csharp_interop_native)


install(TARGETS ros_csharp_interop_native
  LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
  ARCHIVE DESTINATION ${CMAKE_INSTALL_LIBDIR}
  RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
 )

 install (FILES ${CMAKE_CURRENT_BINARY_DIR}/ros_csharp_interop.dll
  DESTINATION ${CMAKE_INSTALL_LIBDIR}
)



# add_custom_target(JointStateSanityTest ALL COMMAND 
#     ${DOTNET} build ${CMAKE_CURRENT_SOURCE_DIR}/test/sanity_test/JointStateSanityTest.csproj
#     -o ${CATKIN_DEVEL_PREFIX}/${CATKIN_PACKAGE_LIB_DESTINATION} 
#     -p:ROS_LIB_DIR=${CATKIN_DEVEL_PREFIX}/${CATKIN_PACKAGE_LIB_DESTINATION}
#     DEPENDS ros_csharp_interop)

