cmake_minimum_required(VERSION 3.20)
project(webp.net)

if (NOT WIN32)
    set(C_FLAG_X86 "-DCMAKE_C_COMPILER=\"i686-w64-mingw32-gcc\"")
    set(C_FLAG_X64 "-DCMAKE_C_COMPILER=\"x86_64-w64-mingw32-gcc\"")
endif ()

add_custom_target(
        webp.x86 ALL
        COMMAND "${CMAKE_COMMAND}"
        -B "${CMAKE_CURRENT_BINARY_DIR}/webp.x86"
        -DBUILD_SHARED_LIBS=ON
        -DCMAKE_BUILD_TYPE=Release
        -DCMAKE_SYSTEM_NAME=Windows
        -DWEBP_BUILD_ANIM_UTILS=OFF
        -DWEBP_BUILD_CWEBP=OFF
        -DWEBP_BUILD_DWEBP=OFF
        -DWEBP_BUILD_GIF2WEBP=OFF
        -DWEBP_BUILD_IMG2WEBP=OFF
        -DWEBP_BUILD_VWEBP=OFF
        -DWEBP_BUILD_WEBPINFO=OFF
        -DWEBP_BUILD_LIBWEBPMUX=OFF
        -DWEBP_BUILD_WEBPMUX=OFF
        -DWEBP_BUILD_EXTRAS=OFF
        -DWEBP_BUILD_WEBP_JS=OFF
        -DWEBP_BUILD_FUZZTEST=OFF
        -DWEBP_USE_THREAD=OFF
        -DWEBP_NEAR_LOSSLESS=OFF
        -DWEBP_ENABLE_SWAP_16BIT_CSP=OFF
        ${C_FLAG} ${C_FLAG_X86}
        COMMAND "${CMAKE_COMMAND}" --build "${CMAKE_CURRENT_BINARY_DIR}/webp.x86"
        COMMAND_EXPAND_LISTS
        WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/sub/libwebp"
)

add_custom_target(
        webp.x64 ALL
        COMMAND "${CMAKE_COMMAND}"
        -B "${CMAKE_CURRENT_BINARY_DIR}/webp.x64"
        -DBUILD_SHARED_LIBS=ON
        -DCMAKE_BUILD_TYPE=Release
        -DCMAKE_SYSTEM_NAME=Windows
        -DWEBP_BUILD_ANIM_UTILS=OFF
        -DWEBP_BUILD_CWEBP=OFF
        -DWEBP_BUILD_DWEBP=OFF
        -DWEBP_BUILD_GIF2WEBP=OFF
        -DWEBP_BUILD_IMG2WEBP=OFF
        -DWEBP_BUILD_VWEBP=OFF
        -DWEBP_BUILD_WEBPINFO=OFF
        -DWEBP_BUILD_LIBWEBPMUX=OFF
        -DWEBP_BUILD_WEBPMUX=OFF
        -DWEBP_BUILD_EXTRAS=OFF
        -DWEBP_BUILD_WEBP_JS=OFF
        -DWEBP_BUILD_FUZZTEST=OFF
        -DWEBP_USE_THREAD=OFF
        -DWEBP_NEAR_LOSSLESS=OFF
        -DWEBP_ENABLE_SWAP_16BIT_CSP=OFF
        ${C_FLAG} ${C_FLAG_X64}
        COMMAND "${CMAKE_COMMAND}" --build "${CMAKE_CURRENT_BINARY_DIR}/webp.x64"
        COMMAND_EXPAND_LISTS
        WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/sub/libwebp"
)

add_custom_target(
        webp.net ALL
        COMMAND "${CMAKE_COMMAND}" -E make_directory "runtimes/win-x86/native/"
        COMMAND "${CMAKE_COMMAND}" -E copy_if_different "${CMAKE_CURRENT_BINARY_DIR}/webp.x86/libwebp.dll" "runtimes/win-x86/native/libwebp.dll"
        COMMAND "${CMAKE_COMMAND}" -E make_directory "runtimes/win-x64/native/"
        COMMAND "${CMAKE_COMMAND}" -E copy_if_different "${CMAKE_CURRENT_BINARY_DIR}/webp.x64/libwebp.dll" "runtimes/win-x64/native/libwebp.dll"
        COMMAND dotnet pack -o "${CMAKE_CURRENT_BINARY_DIR}"
        WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/WebP.Net"
)
add_dependencies(webp.net webp.x86 webp.x64)
