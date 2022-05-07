﻿#version 330 core

layout(location = 0) in vec3 aPosition;  


uniform mat4 model;
uniform mat4 trans;
uniform mat4 projection;
uniform mat4 origenObjeto;
uniform mat4 origenParte;
uniform mat4 scale;
uniform mat4 rotar;

void main(void)
{
	// gl_Position = vec4(aPosition, 1.0) * model * view *  projection;
	//gl_Position = vec4(aPosition, 1.0) * origenParte * model * view * projection * origenObjeto * scale;
	gl_Position = vec4(aPosition, 1.0) * origenParte * model * trans *  projection * origenObjeto * scale;
}