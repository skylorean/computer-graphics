#include <GLFW/glfw3.h>
#include <iostream>

struct Axis
{
	float min;
	float max;
};

void drawCoordinateAxes(const Axis& xAxis, const Axis& yAxis)
{
	glColor3f(1.0, 1.0, 1.0);

	glBegin(GL_LINES);
	// X
	glVertex2f(xAxis.min, 0.0);
	glVertex2f(xAxis.max, 0.0);

	// Y
	glVertex2f(0.0, yAxis.min);
	glVertex2f(0.0, yAxis.max);
	glEnd();
}

void drawParabola(float a, float b, float c, const Axis& xAxis, float step)
{
	glColor3f(1.0, 0.0, 0.0);

	glBegin(GL_POINTS);
	for (float x = xAxis.min; x <= xAxis.max; x += step)
	{
		float y = a * x * x + b * x + c;
		glVertex2f(x, y);
	}
	glEnd();
}

void display(const Axis& xAxis, const Axis& yAxis)
{
	glClear(GL_COLOR_BUFFER_BIT);

	drawCoordinateAxes(xAxis, yAxis);
	drawParabola(2.0f, -3.f, -8.f, xAxis, 0.1); // y = ax^2 + bx + c

	glFlush();
}

int main()
{
	glfwInit();
	GLFWwindow* window = glfwCreateWindow(800, 600, "OpenGL Parabola", NULL, NULL);
	glfwMakeContextCurrent(window);

	Axis xAxis = { -20.0, 30.0 };
	Axis yAxis = { -100.0, 100.0 };

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(xAxis.min, xAxis.max, yAxis.min, yAxis.max, -1.0, 1.0);

	while (!glfwWindowShouldClose(window))
	{
		glClearColor(0.0, 0.0, 0.0, 1.0);
		glClear(GL_COLOR_BUFFER_BIT);

		display(xAxis, yAxis);

		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	glfwDestroyWindow(window);
	glfwTerminate();

	return 0;
}