#version 400
uniform mat4 WorldViewProj;

in vec3 position;
in uint tileIndex;

out vec2 psTexcoord;
flat out uint psTileIndex;

void main()
{
	psTileIndex = tileIndex + 1;
	psTexcoord = position.xy;
	gl_Position = WorldViewProj * vec4(position.xy,0,1.0);
    
}

