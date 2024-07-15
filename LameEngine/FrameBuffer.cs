using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace LameEngine;

public class FrameBuffer
{
    private readonly GL gl;
    private readonly uint quadVAO;

    private readonly uint fbo;
    private readonly uint textureColorBuffer;

    public FrameBuffer(GL pGL,Vector2D<int> pSize)
    {
        gl = pGL;
        
        float[] quadVertices =
        {
            // positions   // texCoords
            -1.0f, 1.0f, 0.0f, 1.0f,
            -1.0f, -1.0f, 0.0f, 0.0f,
            1.0f, -1.0f, 1.0f, 0.0f,

            -1.0f, 1.0f, 0.0f, 1.0f,
            1.0f, -1.0f, 1.0f, 0.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
        };

        quadVAO = gl.GenVertexArray();
        uint vbo = gl.GenBuffer();

        gl.BindVertexArray(quadVAO);
        gl.BindBuffer(GLEnum.ArrayBuffer, vbo);

        ReadOnlySpan<float> bufferData = new ReadOnlySpan<float>(quadVertices);
        gl.BufferData(GLEnum.ArrayBuffer, bufferData, BufferUsageARB.StaticDraw);

        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(0, 2, GLEnum.Float, false, 4 * sizeof(float), 0);
        gl.EnableVertexAttribArray(1);
        gl.VertexAttribPointer(1, 2, GLEnum.Float, false, 4 * sizeof(float), 2 * sizeof(float));
        
        fbo = gl.GenFramebuffer();
        gl.BindFramebuffer(FramebufferTarget.Framebuffer, fbo);

        textureColorBuffer = gl.GenTexture();
        gl.BindTexture(TextureTarget.Texture2D, textureColorBuffer);

        unsafe
        {
            gl.TexImage2D(GLEnum.Texture2D, 0, (int)GLEnum.Rgb, (uint)pSize.X, (uint)pSize.Y, 0, GLEnum.Rgb,
                GLEnum.UnsignedByte, null);
        }


        gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
        gl.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);

        gl.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
            TextureTarget.Texture2D, textureColorBuffer, 0);


        GLEnum status = gl.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
        if (status != GLEnum.FramebufferComplete)
        {
            throw new Exception($"Failed to create FrameBuffer! Status: {status}");
        }

        gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    public void Resize(Vector2D<int> pNewSize)
    {
        unsafe
        {
            gl.BindTexture(TextureTarget.Texture2D, textureColorBuffer);
            gl.TexImage2D(GLEnum.Texture2D, 0, (int)GLEnum.Rgb, (uint)pNewSize.X, (uint)pNewSize.Y, 0, GLEnum.Rgb,
                GLEnum.UnsignedByte, null);
            gl.BindTexture(TextureTarget.Texture2D, 0);
        }
    }

    public void Bind()
    {
        gl.BindFramebuffer(FramebufferTarget.Framebuffer, fbo);
    }

    public void Draw()
    {
        gl.BindVertexArray(quadVAO);
        gl.BindTexture(TextureTarget.Texture2D, textureColorBuffer);
        gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }

    ~FrameBuffer()
    {
        gl.DeleteFramebuffer(fbo);
    }
}