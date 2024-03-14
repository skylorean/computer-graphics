#pragma once

#include "common_inc.h"

class GLFWInitializer
{
public:
	GLFWInitializer();

	GLFWInitializer(GLFWInitializer const&) = delete;
	GLFWInitializer& operator=(GLFWInitializer const&) = delete;

	~GLFWInitializer() noexcept;
};