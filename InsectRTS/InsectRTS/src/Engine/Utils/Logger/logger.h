#pragma once

#include <spdlog/spdlog.h>

extern std::shared_ptr<spdlog::logger> logger;

void StartLogger();
