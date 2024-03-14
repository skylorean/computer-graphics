#include "Window.h"
#include <numbers>

const double FIELD_OF_VIEW = 60 * std::numbers::pi / 180.0;
const double Z_NEAR = 0.1;
const double Z_FAR = 10;
const double VIEWPORT = 20;

void Window::OnResize(int width, int height)
{
	glViewport(0, 0, width, height);

	double aspectRatio = double(width) / double(height);

	glMatrixMode(GL_PROJECTION);
	auto const proj = glm::perspective(FIELD_OF_VIEW, aspectRatio, Z_NEAR, Z_FAR);
	glLoadMatrixd(&proj[0][0]);
	glMatrixMode(GL_MODELVIEW);
}

void Window::OnRunStart()
{
	glClearColor(1, 1, 1, 1);
}

void Window::Draw(int width, int height)
{
	SetupProjectionMatrix(width, height);
	glClear(GL_COLOR_BUFFER_BIT);

	m_pin.Draw();
}

// Настройка ортографической проекционной матрицы
void Window::SetupProjectionMatrix(int width, int height)
{
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	auto const aspectRatio = double(width) / double(height);
	auto viewWidth = VIEWPORT;
	auto viewHeight = viewWidth;

	if (aspectRatio > 1.0)
	{
		viewWidth = viewHeight * aspectRatio;
	}
	else
	{
		viewHeight = viewWidth / aspectRatio;
	}

	glOrtho(-viewWidth * 0.5, viewWidth * 0.5, -viewHeight * 0.5, viewHeight * 0.5, -1.0, 1.0);
}