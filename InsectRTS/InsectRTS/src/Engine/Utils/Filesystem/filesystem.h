#pragma once

#include "../../../global.h"

#if defined(_WIN32) || defined(__CYGWIN__) 
#define PATH_SEPARATOR '\\' 
#else 
#define PATH_SEPARATOR '/'
#endif 

std::string GetApplicationPath();
bool CreateFolder(const std::string path);
