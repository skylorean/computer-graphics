#include <GLFW/glfw3.h>
#include <iostream>

void drawCoordinateAxes()
{
	glColor3f(1.0, 1.0, 1.0); // установить цвет линий

	glBegin(GL_LINES);
	// Ось X
	glVertex2f(-10.0, 0.0);
	glVertex2f(10.0, 0.0);
	// Ось Y
	glVertex2f(0.0, -10.0);
	glVertex2f(0.0, 10.0);
	glEnd();
}

void drawParabola(float a, float b, float c)
{
	glColor3f(1.0, 0.0, 0.0); // красный цвет для графика параболы

	glBegin(GL_POINTS);
	for (float x = -10.0; x <= 10.0; x += 0.01)
	{
		float y = a * x * x + b * x + c; // вычислить значение y для заданного x
		glVertex2f(x, y); // нарисовать точку с координатами (x, y)
	}
	glEnd();
}

void display()
{
	glClear(GL_COLOR_BUFFER_BIT);

	drawCoordinateAxes(); // нарисовать оси координат
	drawParabola(10, 0, 0); // рисовать параболу с уравнением y = 10x^2 - 0x + 0

	glFlush();
}

int main()
{
	glfwInit();
	GLFWwindow* window = glfwCreateWindow(800, 600, "OpenGL Parabola", NULL, NULL);
	glfwMakeContextCurrent(window);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(-10.0, 10.0, -10.0, 10.0, -1.0, 1.0);

	while (!glfwWindowShouldClose(window))
	{
		glClearColor(0.0, 0.0, 0.0, 1.0);
		glClear(GL_COLOR_BUFFER_BIT);

		display();

		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	glfwDestroyWindow(window);
	glfwTerminate();

	return 0;
}
