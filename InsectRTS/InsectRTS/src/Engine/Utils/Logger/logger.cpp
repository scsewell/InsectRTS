#include "logger.h"

#include <spdlog/sinks/basic_file_sink.h>
#include <spdlog/sinks/msvc_sink.h>

#include "../Filesystem/filesystem.h"

#define LOG_DIRECTORY "logs"
#define LOG_FILE_NAME "Session"
#define LOG_FILE_EXTENTION "log"

std::shared_ptr<spdlog::logger> logger = nullptr;

void StartLogger()
{
    if (logger == nullptr)
    {
        std::vector<spdlog::sink_ptr> sinks;

        // outputs to the console in visual studio
#ifdef _DEBUG
        sinks.push_back(std::make_shared<spdlog::sinks::msvc_sink_mt>());
#endif

        // output to a log file
        std::string directory = GetApplicationPath() + LOG_DIRECTORY;
        std::string filePath = directory + PATH_SEPARATOR + LOG_FILE_NAME + '.' + LOG_FILE_EXTENTION;

        CreateFolder(directory);
        sinks.push_back(std::make_shared<spdlog::sinks::basic_file_sink_mt>(filePath, true));

        // create the logger
        logger = std::make_shared<spdlog::logger>("Root", begin(sinks), end(sinks));

        // make sure the logger frequently is writing out
        spdlog::flush_every(std::chrono::milliseconds(50));
    }
}
