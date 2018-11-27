#include "global.h"
#include <SDL.h>

SDL_Window *mainWindow;
SDL_GLContext mainContext;

bool SetOpenGLAttributes();
void CheckSDLError();
void RunGame();
void Cleanup();

bool Init()
{
    // Initialize SDL's Video subsystem
    if (SDL_Init(SDL_INIT_VIDEO) < 0)
    {
        logger->error("Failed to init SDL");
        return false;
    }

    // Create our window centered at 512x512 resolution
    mainWindow = SDL_CreateWindow(GAME_TITLE, SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 512, 512, SDL_WINDOW_OPENGL);
    
    // Check that everything worked out okay
    if (!mainWindow)
    {
        logger->error("Unable to create window");
        CheckSDLError();
        return false;
    }

    // Create our opengl context and attach it to our window
    mainContext = SDL_GL_CreateContext(mainWindow);

    SetOpenGLAttributes();

    // This makes our buffer swap syncronized with the monitor's vertical refresh
    SDL_GL_SetSwapInterval(1);

    // Init GLEW
    glewInit();

    return true;
}

bool SetOpenGLAttributes()
{
    // SDL_GL_CONTEXT_CORE gives us only the newer version, deprecated functions are disabled
    SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);

    SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 4);
    SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 6);

    SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);

    // log the version used by the context
    int majorValue, minorValue;
    SDL_GL_GetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, &majorValue);
    SDL_GL_GetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, &minorValue);

    logger->info("Using OpenGL version {}.{}", majorValue, minorValue);

    return true;
}

int main(int argc, char *argv[])
{
    StartLogger();

    if (!Init())
    {
        return -1;
    }

    glClearColor(0.0, 0.0, 0.0, 1.0);
    glClear(GL_COLOR_BUFFER_BIT);
    SDL_GL_SwapWindow(mainWindow);

    RunGame();

    Cleanup();

    return 0;
}

void RunGame()
{
    bool loop = true;

    while (loop)
    {
        SDL_Event event;
        while (SDL_PollEvent(&event))
        {
            if (event.type == SDL_QUIT)
                loop = false;

            if (event.type == SDL_KEYDOWN)
            {
                switch (event.key.keysym.sym)
                {
                    case SDLK_ESCAPE:
                        loop = false;
                        break;
                    case SDLK_r:
                        // Cover with red and update
                        glClearColor(1.0, 0.0, 0.0, 1.0);
                        glClear(GL_COLOR_BUFFER_BIT);
                        SDL_GL_SwapWindow(mainWindow);
                        break;
                    case SDLK_g:
                        // Cover with green and update
                        glClearColor(0.0, 1.0, 0.0, 1.0);
                        glClear(GL_COLOR_BUFFER_BIT);
                        SDL_GL_SwapWindow(mainWindow);
                        break;
                    case SDLK_b:
                        // Cover with blue and update
                        glClearColor(0.0, 0.0, 1.0, 1.0);
                        glClear(GL_COLOR_BUFFER_BIT);
                        SDL_GL_SwapWindow(mainWindow);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

void Cleanup()
{
    SDL_GL_DeleteContext(mainContext);
    SDL_DestroyWindow(mainWindow);
    SDL_Quit();
}

void CheckSDLError()
{
    std::string error = SDL_GetError();

    if (error != "")
    {
        SDL_ClearError();
        logger->error("SLD Error : " + error);
    }
}
