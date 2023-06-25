using LibRun8.Utils;
using System.Text;

namespace LibRun8.Formats
{
    public class TKB : FileFormat
    {
        public static TKB Read(string path)
        {
            TKB tkb = new TKB();
            Console.WriteLine("EffectShaderType size: " + sizeof(EffectShaderType));
            Console.WriteLine("EffectCompilerFlags size: " + sizeof(EffectCompilerFlags));
            Console.WriteLine("FeatureLevel size: " + sizeof(FeatureLevel));

            return tkb;
        }

        public override void Write()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Identify a single GPU stage in the pipeline.
    /// </summary>
    // Token: 0x02000052 RID: 82
    public enum EffectShaderType : byte
    {
        /// <summary>
        /// Vertex shader stage.
        /// </summary>
        // Token: 0x0400012C RID: 300
        Vertex,
        /// <summary>
        /// Hull shader stage.
        /// </summary>
        // Token: 0x0400012D RID: 301
        Hull,
        /// <summary>
        /// Domain shader stage.
        /// </summary>
        // Token: 0x0400012E RID: 302
        Domain,
        /// <summary>
        /// Geometry shader stage.
        /// </summary>
        // Token: 0x0400012F RID: 303
        Geometry,
        /// <summary>
        /// Pixel shader stage.
        /// </summary>
        // Token: 0x04000130 RID: 304
        Pixel,
        /// <summary>
        /// Compute shader stage.
        /// </summary>
        // Token: 0x04000131 RID: 305
        Compute
    }

    /// <summary>
    /// Effect Compiler flags.
    /// </summary>
    // Token: 0x0200004B RID: 75
    [Flags]
    public enum EffectCompilerFlags
    {
        /// <summary>	
        /// Directs the compiler to insert debug file/line/type/symbol information into the output code.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_DEBUG</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_DEBUG</unmanaged-short>	
        // Token: 0x0400010B RID: 267
        Debug = 1,
        /// <summary>	
        /// Directs the compiler not to validate the generated code against known capabilities and constraints. 
        /// We recommend that you use this constant only with shaders that have been successfully compiled in the past. DirectX always validates shaders before it sets them to a device.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_SKIP_VALIDATION</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_SKIP_VALIDATION</unmanaged-short>	
        // Token: 0x0400010C RID: 268
        SkipValidation = 2,
        /// <summary>	
        /// Directs the compiler to skip optimization steps during code generation. We recommend that you set this constant for debug purposes only.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_SKIP_OPTIMIZATION</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_SKIP_OPTIMIZATION</unmanaged-short>	
        // Token: 0x0400010D RID: 269
        SkipOptimization = 4,
        /// <summary>	
        /// Directs the compiler to pack matrices in row-major order on input and output from the shader.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_PACK_MATRIX_ROW_MAJOR</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_PACK_MATRIX_ROW_MAJOR</unmanaged-short>	
        // Token: 0x0400010E RID: 270
        PackMatrixRowMajor = 8,
        /// <summary>	
        /// Directs the compiler to pack matrices in column-major order on input and output from the shader. This type of packing is generally more efficient because a series of dot-products can then perform vector-matrix multiplication.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_PACK_MATRIX_COLUMN_MAJOR</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_PACK_MATRIX_COLUMN_MAJOR</unmanaged-short>	
        // Token: 0x0400010F RID: 271
        PackMatrixColumnMajor = 16,
        /// <summary>	
        /// Directs the compiler to perform all computations with partial precision. If you set this constant, the compiled code might run faster on some hardware.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_PARTIAL_PRECISION</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_PARTIAL_PRECISION</unmanaged-short>	
        // Token: 0x04000110 RID: 272
        PartialPrecision = 32,
        /// <summary>	
        /// Directs the compiler to not use flow-control constructs where possible.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_AVOID_FLOW_CONTROL</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_AVOID_FLOW_CONTROL</unmanaged-short>	
        // Token: 0x04000111 RID: 273
        AvoidFlowControl = 512,
        /// <summary>	
        /// Directs the compiler to use flow-control constructs where possible.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_PREFER_FLOW_CONTROL</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_PREFER_FLOW_CONTROL</unmanaged-short>	
        // Token: 0x04000112 RID: 274
        PreferFlowControl = 1024,
        /// <summary>	
        /// Forces strict compile, which might not allow for legacy syntax.
        /// </summary>	
        /// <remarks>
        /// By default, the compiler disables strictness on deprecated syntax.
        /// </remarks>
        /// <unmanaged>D3DCOMPILE_ENABLE_STRICTNESS</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_ENABLE_STRICTNESS</unmanaged-short>	
        // Token: 0x04000113 RID: 275
        EnableStrictness = 2048,
        /// <summary>	
        /// Directs the compiler to enable older shaders to compile to 5_0 targets.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_ENABLE_BACKWARDS_COMPATIBILITY</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_ENABLE_BACKWARDS_COMPATIBILITY</unmanaged-short>	
        // Token: 0x04000114 RID: 276
        EnableBackwardsCompatibility = 4096,
        /// <summary>	
        /// Forces the IEEE strict compile.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_IEEE_STRICTNESS</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_IEEE_STRICTNESS</unmanaged-short>	
        // Token: 0x04000115 RID: 277
        IeeeStrictness = 8192,
        /// <summary>	
        /// Directs the compiler to use the lowest optimization level. If you set this constant, the compiler might produce slower code but produces the code more quickly. Set this constant when you develop the shader iteratively.	
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_OPTIMIZATION_LEVEL0</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_OPTIMIZATION_LEVEL0</unmanaged-short>	
        // Token: 0x04000116 RID: 278
        OptimizationLevel0 = 16384,
        /// <summary>	
        /// Directs the compiler to use the second lowest optimization level.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_OPTIMIZATION_LEVEL1</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_OPTIMIZATION_LEVEL1</unmanaged-short>	
        // Token: 0x04000117 RID: 279
        OptimizationLevel1 = 0,
        /// <summary>	
        /// Directs the compiler to use the second highest optimization level.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_OPTIMIZATION_LEVEL2</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_OPTIMIZATION_LEVEL2</unmanaged-short>	
        // Token: 0x04000118 RID: 280
        OptimizationLevel2 = 49152,
        /// <summary>	
        /// Directs the compiler to use the highest optimization level. If you set this constant, the compiler produces the best possible code but might take significantly longer to do so. Set this constant for final builds of an application when performance is the most important factor.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_OPTIMIZATION_LEVEL3</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_OPTIMIZATION_LEVEL3</unmanaged-short>	
        // Token: 0x04000119 RID: 281
        OptimizationLevel3 = 32768,
        /// <summary>	
        /// Directs the compiler to treat all warnings as errors when it compiles the shader code. We recommend that you use this constant for new shader code, so that you can resolve all warnings and lower the number of hard-to-find code defects.
        /// </summary>	
        /// <unmanaged>D3DCOMPILE_WARNINGS_ARE_ERRORS</unmanaged>	
        /// <unmanaged-short>D3DCOMPILE_WARNINGS_ARE_ERRORS</unmanaged-short>	
        // Token: 0x0400011A RID: 282
        WarningsAreErrors = 262144,
        /// <summary>	
        /// None.	
        /// </summary>	
        /// <unmanaged>None</unmanaged>	
        /// <unmanaged-short>None</unmanaged-short>	
        // Token: 0x0400011B RID: 283
        None = 0
    }

    /// <summary>	
    /// <p>Describes the set of features targeted by a Direct3D device.</p>	
    /// </summary>	
    /// <remarks>	
    /// <p>For an overview of  the capabilities of each feature level, see Overview For Each Feature Level.</p><p>For information about limitations creating nonhardware-type devices on certain feature levels, see Limitations Creating WARP and Reference Devices.</p>	
    /// </remarks>	
    /// <!-- No matching elements were found for the following include tag --><include file="..\..\Documentation\CodeComments.xml" path="/comments/comment[@id='D3D_FEATURE_LEVEL']/*" />	
    /// <msdn-id>ff476329</msdn-id>	
    /// <unmanaged>D3D_FEATURE_LEVEL</unmanaged>	
    /// <unmanaged-short>D3D_FEATURE_LEVEL</unmanaged-short>	
    // Token: 0x02000075 RID: 117
    public enum FeatureLevel
    {
        /// <summary>	
        /// <dd> <p>Targets features supported by feature level 9.1 including shader model 2.</p> </dd>	
        /// </summary>	
        /// <!-- No matching elements were found for the following include tag --><include file="..\..\Documentation\CodeComments.xml" path="/comments/comment[@id='D3D_FEATURE_LEVEL_9_1']/*" />	
        /// <msdn-id>ff476329</msdn-id>	
        /// <unmanaged>D3D_FEATURE_LEVEL_9_1</unmanaged>	
        /// <unmanaged-short>D3D_FEATURE_LEVEL_9_1</unmanaged-short>	
        // Token: 0x040001F9 RID: 505
        Level_9_1 = 37120,
        /// <summary>	
        /// <dd> <p>Targets features supported by feature level 9.2 including shader model 2.</p> </dd>	
        /// </summary>	
        /// <!-- No matching elements were found for the following include tag --><include file="..\..\Documentation\CodeComments.xml" path="/comments/comment[@id='D3D_FEATURE_LEVEL_9_2']/*" />	
        /// <msdn-id>ff476329</msdn-id>	
        /// <unmanaged>D3D_FEATURE_LEVEL_9_2</unmanaged>	
        /// <unmanaged-short>D3D_FEATURE_LEVEL_9_2</unmanaged-short>	
        // Token: 0x040001FA RID: 506
        Level_9_2 = 37376,
        /// <summary>	
        /// <dd> <p>Targets features supported by feature level 9.3 including shader model 2.0b.</p> </dd>	
        /// </summary>	
        /// <!-- No matching elements were found for the following include tag --><include file="..\..\Documentation\CodeComments.xml" path="/comments/comment[@id='D3D_FEATURE_LEVEL_9_3']/*" />	
        /// <msdn-id>ff476329</msdn-id>	
        /// <unmanaged>D3D_FEATURE_LEVEL_9_3</unmanaged>	
        /// <unmanaged-short>D3D_FEATURE_LEVEL_9_3</unmanaged-short>	
        // Token: 0x040001FB RID: 507
        Level_9_3 = 37632,
        /// <summary>	
        /// <dd> <p>Targets features supported by Direct3D 10.0 including shader model 4.</p> </dd>	
        /// </summary>	
        /// <!-- No matching elements were found for the following include tag --><include file="..\..\Documentation\CodeComments.xml" path="/comments/comment[@id='D3D_FEATURE_LEVEL_10_0']/*" />	
        /// <msdn-id>ff476329</msdn-id>	
        /// <unmanaged>D3D_FEATURE_LEVEL_10_0</unmanaged>	
        /// <unmanaged-short>D3D_FEATURE_LEVEL_10_0</unmanaged-short>	
        // Token: 0x040001FC RID: 508
        Level_10_0 = 40960,
        /// <summary>	
        /// <dd> <p>Targets features supported by Direct3D 10.1 including shader model 4.</p> </dd>	
        /// </summary>	
        /// <!-- No matching elements were found for the following include tag --><include file="..\..\Documentation\CodeComments.xml" path="/comments/comment[@id='D3D_FEATURE_LEVEL_10_1']/*" />	
        /// <msdn-id>ff476329</msdn-id>	
        /// <unmanaged>D3D_FEATURE_LEVEL_10_1</unmanaged>	
        /// <unmanaged-short>D3D_FEATURE_LEVEL_10_1</unmanaged-short>	
        // Token: 0x040001FD RID: 509
        Level_10_1 = 41216,
        /// <summary>	
        /// <dd> <p>Targets features supported by Direct3D 11.0 including shader model 5.</p> </dd>	
        /// </summary>	
        /// <!-- No matching elements were found for the following include tag --><include file="..\..\Documentation\CodeComments.xml" path="/comments/comment[@id='D3D_FEATURE_LEVEL_11_0']/*" />	
        /// <msdn-id>ff476329</msdn-id>	
        /// <unmanaged>D3D_FEATURE_LEVEL_11_0</unmanaged>	
        /// <unmanaged-short>D3D_FEATURE_LEVEL_11_0</unmanaged-short>	
        // Token: 0x040001FE RID: 510
        Level_11_0 = 45056
    }

    /// <summary>	
    /// Values that identify the class of a shader variable.
    /// </summary>	
    /// <remarks>	
    /// The class of a shader variable is not a programming class; the class identifies the variable class such as scalar, vector, object, and so on.
    /// </remarks>	
    /// <msdn-id>ff728733</msdn-id>	
    /// <unmanaged>D3D_SHADER_VARIABLE_CLASS</unmanaged>	
    /// <unmanaged-short>D3D_SHADER_VARIABLE_CLASS</unmanaged-short>	
    // Token: 0x02000053 RID: 83
    public enum EffectParameterClass : byte
    {
        /// <summary>	
        /// <dd> <p>The shader variable is a scalar.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728733</msdn-id>	
        /// <unmanaged>D3D_SVC_SCALAR</unmanaged>	
        /// <unmanaged-short>D3D_SVC_SCALAR</unmanaged-short>	
        // Token: 0x04000133 RID: 307
        Scalar,
        /// <summary>	
        /// <dd> <p>The shader variable is a vector.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728733</msdn-id>	
        /// <unmanaged>D3D_SVC_VECTOR</unmanaged>	
        /// <unmanaged-short>D3D_SVC_VECTOR</unmanaged-short>	
        // Token: 0x04000134 RID: 308
        Vector,
        /// <summary>	
        /// <dd> <p>The shader variable is a row-major matrix.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728733</msdn-id>	
        /// <unmanaged>D3D_SVC_MATRIX_ROWS</unmanaged>	
        /// <unmanaged-short>D3D_SVC_MATRIX_ROWS</unmanaged-short>	
        // Token: 0x04000135 RID: 309
        MatrixRows,
        /// <summary>	
        /// <dd> <p>The shader variable is a column-major matrix.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728733</msdn-id>	
        /// <unmanaged>D3D_SVC_MATRIX_COLUMNS</unmanaged>	
        /// <unmanaged-short>D3D_SVC_MATRIX_COLUMNS</unmanaged-short>	
        // Token: 0x04000136 RID: 310
        MatrixColumns,
        /// <summary>	
        /// <dd> <p>The shader variable is an object.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728733</msdn-id>	
        /// <unmanaged>D3D_SVC_OBJECT</unmanaged>	
        /// <unmanaged-short>D3D_SVC_OBJECT</unmanaged-short>	
        // Token: 0x04000137 RID: 311
        Object,
        /// <summary>	
        /// <dd> <p>The shader variable is a structure.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728733</msdn-id>	
        /// <unmanaged>D3D_SVC_STRUCT</unmanaged>	
        /// <unmanaged-short>D3D_SVC_STRUCT</unmanaged-short>	
        // Token: 0x04000138 RID: 312
        Struct,
        /// <summary>	
        /// <dd> <p>The shader variable is a class.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728733</msdn-id>	
        /// <unmanaged>D3D_SVC_INTERFACE_CLASS</unmanaged>	
        /// <unmanaged-short>D3D_SVC_INTERFACE_CLASS</unmanaged-short>	
        // Token: 0x04000139 RID: 313
        InterfaceClass,
        /// <summary>	
        /// <dd> <p>The shader variable is an interface.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728733</msdn-id>	
        /// <unmanaged>D3D_SVC_INTERFACE_POINTER</unmanaged>	
        /// <unmanaged-short>D3D_SVC_INTERFACE_POINTER</unmanaged-short>	
        // Token: 0x0400013A RID: 314
        InterfacePointer
    }

    /// <summary>	
    /// <p>Values that identify various data, texture, and buffer types that can be assigned to a shader variable.</p>	
    /// </summary>	
    /// <remarks>	
    /// <p>A call to the <strong><see cref="!:SharpDX.D3DCompiler.ShaderReflectionType.GetDescription" /></strong> method returns a <strong><see cref="!:SharpDX.D3DCompiler.ShaderVariableType" /></strong> value in the  <strong>Type</strong> member of a  <strong><see cref="!:SharpDX.D3DCompiler.ShaderTypeDescription" /></strong> structure.</p><p>The types in a structured buffer describe the structure of the elements in the buffer. The layout of these types generally match their C++ struct counterparts. The following examples show structured buffers:</p><pre><code>struct mystruct {float4 val; uint ind;}; RWStructuredBuffer&lt;mystruct&gt; rwbuf;	
    /// RWStructuredBuffer&lt;float3&gt; rwbuf2;</code></pre>	
    /// </remarks>	
    /// <msdn-id>ff728735</msdn-id>	
    /// <unmanaged>D3D_SHADER_VARIABLE_TYPE</unmanaged>	
    /// <unmanaged-short>D3D_SHADER_VARIABLE_TYPE</unmanaged-short>	
    // Token: 0x02000054 RID: 84
    public enum EffectParameterType : byte
    {
        /// <summary>	
        /// <dd> <p>The variable is a void reference.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_VOID</unmanaged>	
        /// <unmanaged-short>D3D_SVT_VOID</unmanaged-short>	
        // Token: 0x0400013C RID: 316
        Void,
        /// <summary>	
        /// <dd> <p>The variable is a boolean.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_BOOL</unmanaged>	
        /// <unmanaged-short>D3D_SVT_BOOL</unmanaged-short>	
        // Token: 0x0400013D RID: 317
        Bool,
        /// <summary>	
        /// <dd> <p>The variable is an integer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_INT</unmanaged>	
        /// <unmanaged-short>D3D_SVT_INT</unmanaged-short>	
        // Token: 0x0400013E RID: 318
        Int,
        /// <summary>	
        /// <dd> <p>The variable is a floating-point number.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_FLOAT</unmanaged>	
        /// <unmanaged-short>D3D_SVT_FLOAT</unmanaged-short>	
        // Token: 0x0400013F RID: 319
        Float,
        /// <summary>	
        /// <dd> <p>The variable is a string.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_STRING</unmanaged>	
        /// <unmanaged-short>D3D_SVT_STRING</unmanaged-short>	
        // Token: 0x04000140 RID: 320
        String,
        /// <summary>	
        /// <dd> <p>The variable is a texture.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURE</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURE</unmanaged-short>	
        // Token: 0x04000141 RID: 321
        Texture,
        /// <summary>	
        /// <dd> <p>The variable is a 1D texture.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURE1D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURE1D</unmanaged-short>	
        // Token: 0x04000142 RID: 322
        Texture1D,
        /// <summary>	
        /// <dd> <p>The variable is a 2D texture.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURE2D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURE2D</unmanaged-short>	
        // Token: 0x04000143 RID: 323
        Texture2D,
        /// <summary>	
        /// <dd> <p>The variable is a 3D texture.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURE3D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURE3D</unmanaged-short>	
        // Token: 0x04000144 RID: 324
        Texture3D,
        /// <summary>	
        /// <dd> <p>The variable is a texture cube.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURECUBE</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURECUBE</unmanaged-short>	
        // Token: 0x04000145 RID: 325
        TextureCube,
        /// <summary>	
        /// <dd> <p>The variable is a sampler.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_SAMPLER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_SAMPLER</unmanaged-short>	
        // Token: 0x04000146 RID: 326
        Sampler,
        /// <summary>	
        /// <dd> <p>The variable is a sampler.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_SAMPLER1D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_SAMPLER1D</unmanaged-short>	
        // Token: 0x04000147 RID: 327
        Sampler1D,
        /// <summary>	
        /// <dd> <p>The variable is a sampler.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_SAMPLER2D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_SAMPLER2D</unmanaged-short>	
        // Token: 0x04000148 RID: 328
        Sampler2D,
        /// <summary>	
        /// <dd> <p>The variable is a sampler.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_SAMPLER3D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_SAMPLER3D</unmanaged-short>	
        // Token: 0x04000149 RID: 329
        Sampler3D,
        /// <summary>	
        /// <dd> <p>The variable is a sampler.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_SAMPLERCUBE</unmanaged>	
        /// <unmanaged-short>D3D_SVT_SAMPLERCUBE</unmanaged-short>	
        // Token: 0x0400014A RID: 330
        SamplerCube,
        /// <summary>	
        /// <dd> <p>The variable is a pixel shader.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_PIXELSHADER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_PIXELSHADER</unmanaged-short>	
        // Token: 0x0400014B RID: 331
        Pixelshader,
        /// <summary>	
        /// <dd> <p>The variable is a vertex shader.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_VERTEXSHADER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_VERTEXSHADER</unmanaged-short>	
        // Token: 0x0400014C RID: 332
        Vertexshader,
        /// <summary>	
        /// <dd> <p>The variable is a pixel shader.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_PIXELFRAGMENT</unmanaged>	
        /// <unmanaged-short>D3D_SVT_PIXELFRAGMENT</unmanaged-short>	
        // Token: 0x0400014D RID: 333
        Pixelfragment,
        /// <summary>	
        /// <dd> <p>The variable is a vertex shader.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_VERTEXFRAGMENT</unmanaged>	
        /// <unmanaged-short>D3D_SVT_VERTEXFRAGMENT</unmanaged-short>	
        // Token: 0x0400014E RID: 334
        Vertexfragment,
        /// <summary>	
        /// <dd> <p>The variable is an unsigned integer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_UINT</unmanaged>	
        /// <unmanaged-short>D3D_SVT_UINT</unmanaged-short>	
        // Token: 0x0400014F RID: 335
        UInt,
        /// <summary>	
        /// <dd> <p>The variable is an 8-bit unsigned integer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_UINT8</unmanaged>	
        /// <unmanaged-short>D3D_SVT_UINT8</unmanaged-short>	
        // Token: 0x04000150 RID: 336
        UInt8,
        /// <summary>	
        /// <dd> <p>The variable is a geometry shader.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_GEOMETRYSHADER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_GEOMETRYSHADER</unmanaged-short>	
        // Token: 0x04000151 RID: 337
        Geometryshader,
        /// <summary>	
        /// <dd> <p>The variable is a rasterizer-state object.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RASTERIZER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RASTERIZER</unmanaged-short>	
        // Token: 0x04000152 RID: 338
        Rasterizer,
        /// <summary>	
        /// <dd> <p>The variable is a depth-stencil-state object.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_DEPTHSTENCIL</unmanaged>	
        /// <unmanaged-short>D3D_SVT_DEPTHSTENCIL</unmanaged-short>	
        // Token: 0x04000153 RID: 339
        Depthstencil,
        /// <summary>	
        /// <dd> <p>The variable is a blend-state object.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_BLEND</unmanaged>	
        /// <unmanaged-short>D3D_SVT_BLEND</unmanaged-short>	
        // Token: 0x04000154 RID: 340
        Blend,
        /// <summary>	
        /// <dd> <p>The variable is a buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_BUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_BUFFER</unmanaged-short>	
        // Token: 0x04000155 RID: 341
        Buffer,
        /// <summary>	
        /// <dd> <p>The variable is a constant buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_CBUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_CBUFFER</unmanaged-short>	
        // Token: 0x04000156 RID: 342
        ConstantBuffer,
        /// <summary>	
        /// <dd> <p>The variable is a texture buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TBUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TBUFFER</unmanaged-short>	
        // Token: 0x04000157 RID: 343
        TextureBuffer,
        /// <summary>	
        /// <dd> <p>The variable is a 1D-texture array.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURE1DARRAY</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURE1DARRAY</unmanaged-short>	
        // Token: 0x04000158 RID: 344
        Texture1DArray,
        /// <summary>	
        /// <dd> <p>The variable is a 2D-texture array.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURE2DARRAY</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURE2DARRAY</unmanaged-short>	
        // Token: 0x04000159 RID: 345
        Texture2DArray,
        /// <summary>	
        /// <dd> <p>The variable is a render-target view.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RENDERTARGETVIEW</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RENDERTARGETVIEW</unmanaged-short>	
        // Token: 0x0400015A RID: 346
        Rendertargetview,
        /// <summary>	
        /// <dd> <p>The variable is a depth-stencil view.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_DEPTHSTENCILVIEW</unmanaged>	
        /// <unmanaged-short>D3D_SVT_DEPTHSTENCILVIEW</unmanaged-short>	
        // Token: 0x0400015B RID: 347
        Depthstencilview,
        /// <summary>	
        /// <dd> <p>The variable is a 2D-multisampled texture.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURE2DMS</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURE2DMS</unmanaged-short>	
        // Token: 0x0400015C RID: 348
        Texture2DMultisampled,
        /// <summary>	
        /// <dd> <p>The variable is a 2D-multisampled-texture array.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURE2DMSARRAY</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURE2DMSARRAY</unmanaged-short>	
        // Token: 0x0400015D RID: 349
        Texture2DMultisampledArray,
        /// <summary>	
        /// <dd> <p>The variable is a texture-cube array.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_TEXTURECUBEARRAY</unmanaged>	
        /// <unmanaged-short>D3D_SVT_TEXTURECUBEARRAY</unmanaged-short>	
        // Token: 0x0400015E RID: 350
        TextureCubeArray,
        /// <summary>	
        /// <dd> <p>The variable holds a compiled hull-shader binary.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_HULLSHADER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_HULLSHADER</unmanaged-short>	
        // Token: 0x0400015F RID: 351
        Hullshader,
        /// <summary>	
        /// <dd> <p>The variable holds a compiled domain-shader binary.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_DOMAINSHADER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_DOMAINSHADER</unmanaged-short>	
        // Token: 0x04000160 RID: 352
        Domainshader,
        /// <summary>	
        /// <dd> <p>The variable is an interface.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_INTERFACE_POINTER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_INTERFACE_POINTER</unmanaged-short>	
        // Token: 0x04000161 RID: 353
        InterfacePointer,
        /// <summary>	
        /// <dd> <p>The variable holds a compiled compute-shader binary.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_COMPUTESHADER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_COMPUTESHADER</unmanaged-short>	
        // Token: 0x04000162 RID: 354
        Computeshader,
        /// <summary>	
        /// <dd> <p>The variable is a double precision (64-bit) floating-point number.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_DOUBLE</unmanaged>	
        /// <unmanaged-short>D3D_SVT_DOUBLE</unmanaged-short>	
        // Token: 0x04000163 RID: 355
        Double,
        /// <summary>	
        /// <dd> <p>The variable is a 1D read-and-write texture.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RWTEXTURE1D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RWTEXTURE1D</unmanaged-short>	
        // Token: 0x04000164 RID: 356
        RWTexture1D,
        /// <summary>	
        /// <dd> <p>The variable is an array of 1D read-and-write textures.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RWTEXTURE1DARRAY</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RWTEXTURE1DARRAY</unmanaged-short>	
        // Token: 0x04000165 RID: 357
        RWTexture1DArray,
        /// <summary>	
        /// <dd> <p>The variable is a 2D read-and-write texture.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RWTEXTURE2D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RWTEXTURE2D</unmanaged-short>	
        // Token: 0x04000166 RID: 358
        RWTexture2D,
        /// <summary>	
        /// <dd> <p>The variable is an array of 2D read-and-write textures.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RWTEXTURE2DARRAY</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RWTEXTURE2DARRAY</unmanaged-short>	
        // Token: 0x04000167 RID: 359
        RWTexture2DArray,
        /// <summary>	
        /// <dd> <p>The variable is a 3D read-and-write texture.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RWTEXTURE3D</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RWTEXTURE3D</unmanaged-short>	
        // Token: 0x04000168 RID: 360
        RWTexture3D,
        /// <summary>	
        /// <dd> <p>The variable is a read-and-write buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RWBUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RWBUFFER</unmanaged-short>	
        // Token: 0x04000169 RID: 361
        RWBuffer,
        /// <summary>	
        /// <dd> <p>The variable is a byte-address buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_BYTEADDRESS_BUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_BYTEADDRESS_BUFFER</unmanaged-short>	
        // Token: 0x0400016A RID: 362
        ByteAddressBuffer,
        /// <summary>	
        /// <dd> <p>The variable is a read-and-write byte-address buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RWBYTEADDRESS_BUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RWBYTEADDRESS_BUFFER</unmanaged-short>	
        // Token: 0x0400016B RID: 363
        RWByteAddressBuffer,
        /// <summary>	
        /// <dd> <p>The variable is a structured buffer. </p> <p>For more information about structured buffer, see the <strong>Remarks</strong> section.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_STRUCTURED_BUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_STRUCTURED_BUFFER</unmanaged-short>	
        // Token: 0x0400016C RID: 364
        StructuredBuffer,
        /// <summary>	
        /// <dd> <p>The variable is a read-and-write structured buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_RWSTRUCTURED_BUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_RWSTRUCTURED_BUFFER</unmanaged-short>	
        // Token: 0x0400016D RID: 365
        RWStructuredBuffer,
        /// <summary>	
        /// <dd> <p>The variable is an append structured buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_APPEND_STRUCTURED_BUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_APPEND_STRUCTURED_BUFFER</unmanaged-short>	
        // Token: 0x0400016E RID: 366
        AppendStructuredBuffer,
        /// <summary>	
        /// <dd> <p>The variable is a consume structured buffer.</p> </dd>	
        /// </summary>	
        /// <msdn-id>ff728735</msdn-id>	
        /// <unmanaged>D3D_SVT_CONSUME_STRUCTURED_BUFFER</unmanaged>	
        /// <unmanaged-short>D3D_SVT_CONSUME_STRUCTURED_BUFFER</unmanaged-short>	
        // Token: 0x0400016F RID: 367
        ConsumeStructuredBuffer
    }
}
