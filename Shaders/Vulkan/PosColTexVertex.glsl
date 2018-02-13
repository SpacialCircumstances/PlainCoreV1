#version 450
#extension GL_ARB_separate_shader_objects : enable
#extension GL_ARB_shading_language_420pack : enable

layout(location = 0) in vec2 Position;
layout(location = 1) in vec4 Color;
layout(location = 2) in vec2 TextureCoords;

uniform World
{
    mat4 field_World;
};

layout(location = 0) out vec4 frColor;
layout(location = 1) out vec2 UV;

void main()
{
    gl_Position = field_World * vec4(Position, 0, 1);
    gl_Position.y = -gl_Position.y;
    UV = TextureCoords;
    frColor = Color;
}