#pragma once

// standard libraries
#include <memory>
#include <string>
#include <vector>

// GL extentions library
#define GLEW_STATIC
#include <GL/glew.h>

// GL Math
#define GLM_FORCE_MESSAGES  // Print the compilation options on build
#define GLM_FORCE_INLINE    // higher performance, gerally speaking
#define GLM_FORCE_SSE2      // make sure fp operations are deterministic
#define GLM_FORCE_SWIZZLE   // allow swizzling vector components

#include <glm/glm.hpp>

#include <glm/ext/vector_float2.hpp>
#include <glm/ext/vector_float3.hpp>
#include <glm/ext/vector_float4.hpp>

#include <glm/ext/matrix_float3x3.hpp>
#include <glm/ext/matrix_float4x4.hpp>

#include <glm/ext/quaternion_float.hpp>
#include <glm/ext/quaternion_common.hpp>
#include <glm/ext/quaternion_geometric.hpp>
#include <glm/ext/quaternion_trigonometric.hpp>
#include <glm/ext/quaternion_relational.hpp>
#include <glm/ext/quaternion_transform.hpp>

#define M_DEG_TO_RAD (M_PI / 180)
#define M_RAD_TO_DEG (180 / M_PI)

// other includes
#include "Engine/Utils/Logger/logger.h"

// type definitions
typedef unsigned int uint;
typedef unsigned long long ulong;
typedef std::string String;

// Contants
#define GAME_TITLE "Game"
