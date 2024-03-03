#include <cmath>
#include <iostream>
#include <vector>

#include <GLFW/glfw3.h>

const int WIDTH = 1000;
const int HEIGHT = 1000;


void drawParabola(float a, float b, float c)
{
	glPointSize(1.0f); // Set point size for better visibility
	glColor3f(1.0f, 1.0f, 1.0f); // Set color to white

	glBegin(GL_POINTS);
	for (float x = -10.0f; x <= 10.0f; x += 0.01f)
	{
		float y = a * x * x + b * x + c;
		glVertex2f(x, y);
	}
	glEnd();
}

void drawX(float cx, float cy)
{
	glColor3f(1.0f, 1.0f, 1.0f);
	glBegin(GL_LINES);
	glVertex2f(cx - 100.0f, cy);
	glVertex2f(cx + 100.0f, cy);
	glEnd();
}

void resize(int width, int height)
{
	glViewport(0, 0, width, height);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();

	if (width <= height)
	{
		glOrtho(-10.0, 10.0, -10.0 * height / width, 10.0 * height / width, -1, 1);
	}
	else
	{
		glOrtho(-10.0 * width / height, 10.0 * width / height, -10.0, 10.0, -1, 1);
	}

	glMatrixMode(GL_MODELVIEW);
}

int main()
{
	if (!glfwInit())
		return -1;

	GLFWwindow* window = glfwCreateWindow(WIDTH, HEIGHT, "Parabola", NULL, NULL);

	if (!window)
	{
		glfwTerminate();
		return -1;
	}

	glfwMakeContextCurrent(window);
	 resize(WIDTH, HEIGHT);

	while (!glfwWindowShouldClose(window))
	{
		glClear(GL_COLOR_BUFFER_BIT);

		drawX(0.0f, 0.0f);
		drawParabola(1.0f, 2.0f, 2.0f);

		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	glfwTerminate();

	return 0;
}