#version 330 core

in vec2 TextureCord;

out vec4 FragColor;

uniform sampler2D texture1;
//uniform sampler2D texture2;

void main()
{
    //FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
    FragColor = texture(texture1, TextureCord);
    //FragColor = mix(texture(texture1, TextureCord), texture(texture2, TextureCord), 0.2);
}