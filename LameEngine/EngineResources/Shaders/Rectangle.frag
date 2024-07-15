#version 330 core

in vec2 TexCoords;
out vec4 color;

uniform sampler2D texture0;
uniform vec4 spriteColor;

void main()
{
    color = spriteColor * texture(texture0, TexCoords);
//    color = vec4(TexCoords.x, TexCoords.y, TexCoords.x, 1);
//    color = spriteColor;
}  