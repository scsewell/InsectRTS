#include "filesystem.h"

std::string GetApplicationPath()
{
    // get the path to the executable
    TCHAR buf[MAX_PATH];
    DWORD length = GetModuleFileName(nullptr, buf, MAX_PATH);
    std::string filename = std::string(buf, length);

    // get the directory from the path
    const size_t index = filename.rfind(PATH_SEPARATOR);
    if (std::string::npos != index)
    {
        return filename.substr(0, index + 1);
    }
    else
    {
        return filename;
    }
}

bool CreateFolder(const std::string path)
{
    return CreateDirectory(path.c_str(), nullptr) || GetLastError() == ERROR_ALREADY_EXISTS;
}
