#version 450
#extension GL_ARB_separate_shader_objects : enable
#extension GL_ARB_shading_language_420pack : enable

layout(location = 0) in vec4 frColor;
layout(location = 1) in vec2 UV;

uniform sampler2D SpriteTexture;

layout(location = 0) out vec4 fsout_Color;

void main()
{
    fsout_Color = texture(SpriteTexture, UV) * frColor;
}