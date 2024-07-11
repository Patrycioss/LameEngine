using OpenTK.Graphics.OpenGL.Compatibility;

namespace LameEngine;

public class Frame
{
    private int fbo;

    public Frame(int pWidth, int pHeight)
    {
        GL.GenFramebuffer(out fbo);

        // Make a texture to render to
        GL.GenTexture(out int textureColorBuffer);
        GL.BindTexture(TextureTarget.Texture2d, textureColorBuffer);
        GL.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgb, 800, 600, 0, PixelFormat.Rgb,
            PixelType.UnsignedByte, 0);

        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
        GL.BindTexture(TextureTarget.Texture2d, 0);

        GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
            TextureTarget.Texture2d, textureColorBuffer, 0);

        // Render buffer object
        GL.GenRenderbuffer(out int rbo);
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rbo);
        GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, InternalFormat.Depth24Stencil8, pWidth, pHeight);
        GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);

        GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment,
            RenderbufferTarget.Renderbuffer, rbo);

        if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferStatus.FramebufferComplete)
        {
            throw new Exception("Framebuffer is not complete!");
        }
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        
        
        
    }
}