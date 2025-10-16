cmake_minimum_required(VERSION 3.20)
project(webp.net)

set(CMAKE_OPTIONS
        -DBUILD_SHARED_LIBS=ON
        -DCMAKE_BUILD_TYPE=Release
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
)

set(BUILDS)

function(add_build os arch)
    if (arch STREQUAL "x64")
        set(BITS 64)
    elseif (arch STREQUAL "x86")
        set(BITS 32)
    else ()
        message(SEND_ERROR "Unrecognized architecture type")
    endif ()

    if (os STREQUAL "win")
        set(SYSTEM Windows)
        set(NAME libwebp.dll)
        if (WIN32)
            set(C_COMPILER "${CMAKE_C_COMPILER}")
            set(CXX_COMPILER "${CMAKE_CXX_COMPILER}")
        else ()
            if (BITS EQUAL 64)
                set(C_COMPILER "x86_64-w64-mingw32-gcc")
                set(CXX_COMPILER "x86_64-w64-mingw32-g++")
            elseif (BITS EQUAL 32)
                set(C_COMPILER "i686-w64-mingw32-gcc")
                set(CXX_COMPILER "i686-w64-mingw32-g++")
            endif ()
        endif ()
    elseif (os STREQUAL "linux")
        set(SYSTEM Linux)
        set(NAME libwebp.so)
        if (WIN32)
            set(C_COMPILER "gcc")
            set(CXX_COMPILER "g++")
            set(LINKER "GOLD")
        else ()
            set(C_COMPILER "${CMAKE_C_COMPILER}")
            set(CXX_COMPILER "${CMAKE_CXX_COMPILER}")
            set(LINKER "LLD")
        endif ()

        if (BITS EQUAL 64)
            set(FLAGS -m64)
        elseif (BITS EQUAL 32)
            set(FLAGS -m32 -march=i686)
        endif ()
    else ()
        message(SEND_ERROR "Unrecognized OS type")
    endif ()

    set(RID "${os}-${arch}")

    list(APPEND BUILDS "webp.${RID}")
    add_custom_target(
            webp.${RID} ALL

            # Configure CMake
            COMMAND "${CMAKE_COMMAND}"
            -S "${CMAKE_CURRENT_SOURCE_DIR}/sub/libwebp"
            -B "${CMAKE_CURRENT_BINARY_DIR}/webp.${RID}"
            ${CMAKE_OPTIONS}
            -DCMAKE_SYSTEM_NAME="${SYSTEM}"
            -DCMAKE_LINKER_TYPE="${LINKER}"
            -DCMAKE_C_COMPILER="${C_COMPILER}"
            -DCMAKE_CXX_COMPILER="${CXX_COMPILER}"
            -DCMAKE_C_FLAGS="${FLAGS}"
            -DCMAKE_CXX_FLAGS="${FLAGS}"

            # Build libwebp
            COMMAND "${CMAKE_COMMAND}"
            --build "${CMAKE_CURRENT_BINARY_DIR}/webp.${RID}"

            # Prepare runtimes folder
            COMMAND "${CMAKE_COMMAND}"
            -E make_directory "${CMAKE_CURRENT_SOURCE_DIR}/WebP.Net/runtimes/${RID}/native"

            # Copy libwebp to runtimes folder
            COMMAND "${CMAKE_COMMAND}"
            -E copy_if_different
            "${CMAKE_CURRENT_BINARY_DIR}/webp.${RID}/${NAME}"
            "${CMAKE_CURRENT_SOURCE_DIR}/WebP.Net/runtimes/${RID}/native/${NAME}"
    )
endfunction()

add_build(win x86)
add_build(win x64)
add_build(linux x86)
add_build(linux x64)

add_custom_target(
        webp.net ALL
        COMMAND dotnet pack -o "${CMAKE_CURRENT_BINARY_DIR}"
        WORKING_DIRECTORY "${CMAKE_CURRENT_SOURCE_DIR}/WebP.Net"
)
add_dependencies(webp.net ${BUILDS})
