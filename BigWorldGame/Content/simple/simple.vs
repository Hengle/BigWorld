#version 400
uniform mat4 WorldViewProj;

in vec3 position;
in uint tileIndex;

out vec2 psTexcoord;
flat out uint psTileIndex;

void main()
{
	psTileIndex = tileIndex;
	int dx = int(position.x/2.0);
	int dy = int(position.y/2.0);
	psTexcoord = position.xy;//vec2(position.x-dx*2.0,position.y-dy*2.0);//vec2(position.x % 2,position.y % 2);
	gl_Position = WorldViewProj*vec4(position.xy,0.0,1.0);


}

