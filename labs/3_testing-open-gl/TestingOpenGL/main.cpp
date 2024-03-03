#include <iostream>
#include <glad/glad.h>
#include <GLFW/glfw3.h>

int main()
{
	glfwInit();

	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	GLFWwindow* window = glfwCreateWindow(800, 600, "Testing Open GL", NULL, NULL);

	if (window == NULL)
	{
		std::cout << "Failed to create GLFW window" << std::endl;
		glfwTerminate();

		return 1;
	}

	glfwMakeContextCurrent(window); // Make the window in current context

	while (!glfwWindowShouldClose(window)) // Hold window opened until its closed by user or other function
	{
		glfwPollEvents(); // Process all the poll events like a dragging resizing and etc
	}

	glfwDestroyWindow(window); // Destroying after close
	glfwTerminate();

	return 0;
}