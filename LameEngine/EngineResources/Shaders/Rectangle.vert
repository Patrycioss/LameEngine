#version 330 core
layout (location = 0) in vec2 vertex;
layout (location = 1) in vec2 uv;
out vec2 TexCoords;

uniform mat4 modelMatrix;
uniform mat4 projectionMatrix;

void main()
{
    TexCoords = uv;
//    gl_Position = projection * model * vec4(vertex, 0.0, 1.0);
    gl_Position = projectionMatrix * modelMatrix * vec4(vertex, 0.0, 1.0);
//    gl_Position =  vec4(vertex, 0.0, 1.0);
}