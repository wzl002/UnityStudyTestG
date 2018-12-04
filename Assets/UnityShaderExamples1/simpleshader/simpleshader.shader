// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "COMP7051 Shader Demo/Simple shader" {
// Adapted from tutorials on Unity website
    Properties
    {
        // Color property for material inspector, default to white
        _Color ("Main Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            // vertex shader
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }
            
            // color from the material
            fixed4 _Color;
            uniform float myRedOffset;

            // pixel shader, no inputs needed
            fixed4 frag () : SV_Target
            {
                return _Color; // just return it
            }
            ENDCG
        }
    }
}
