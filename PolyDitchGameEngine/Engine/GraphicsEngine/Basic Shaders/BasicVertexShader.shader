#version 330 core

in vec3 aPosition;
in vec2 textureCoords;
//layout(location = 1) in vec2 aTexCoord;

out vec2 TextureCord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main(void)
{
    gl_Position = vec4(aPosition, 1.0);//((((vec4(aPosition, 1.0))) * model) * view) * projection;
    TextureCord = textureCoords;
}