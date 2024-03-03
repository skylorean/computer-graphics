#include "GLFWInitializer.h"
#include "GraphWindow.h"
#include <GLFW/glfw3.h>
#include <gl/glew.h>
#include <iostream>

int main(void)
{
	GLFWInitializer initGLFW;

	GraphWindow window(800, 600, "Graph");

	window.Run();
}