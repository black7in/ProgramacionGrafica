#version 330 core

layout(location = 0) in vec3 aPosition;  


uniform mat4 projection;
uniform mat4 origenObjeto;
uniform mat4 origenParte;

uniform mat4 scale;
uniform mat4 rotar;
uniform mat4 trans;

void main(void) {
    gl_Position = vec4(aPosition, 1.0) * origenParte  * origenObjeto * rotar* scale * trans * projection;
}