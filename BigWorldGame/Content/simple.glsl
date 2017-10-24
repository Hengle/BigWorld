<Effect>
    <Technique name="Build">
        <Pass name="Pass1">
            <Shader type="PixelShader" filename="simple/build.ps">

            </Shader>
            <Shader type="VertexShader" filename="simple/build.vs">

            </Shader>
            <Attributes>
                <attribute name="position">Position</attribute>
                <attribute name="tileIndex">Normal</attribute>
            </Attributes>
        </Pass>
    </Technique>
     <Technique name="Run">
            <Pass name="Pass1">
                <Shader type="PixelShader" filename="simple/run.ps">
    
                </Shader>
                <Shader type="VertexShader" filename="simple/run.vs">
    
                </Shader>
                <Attributes>
                    <attribute name="position">Position</attribute>
                    <attribute name="tileIndex">Normal</attribute>
                </Attributes>
            </Pass>
     </Technique>
     <Technique name="RunCharacter">
            <Pass name="Pass1">
                <Shader type="PixelShader" filename="simple/run.ps">
    
                </Shader>
                <Shader type="VertexShader" filename="simple/runCharacter.vs">
    
                </Shader>
                <Attributes>
                    <attribute name="position">Position</attribute>
                    <attribute name="tilePosition">TextureCoordinate</attribute>
                    <attribute name="tileIndex">Normal</attribute>
                </Attributes>
            </Pass>
        </Technique>
</Effect>
