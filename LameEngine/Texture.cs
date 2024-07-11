using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using StbImageSharp;

namespace LameEngine;

public class Texture
{
    public struct Settings()
    {
        public int LevelOfDetail = 0;
        public TextureWrapMode WrapSMode = TextureWrapMode.Repeat;
        public TextureWrapMode WrapTMode = TextureWrapMode.Repeat;
        public Color4<Rgba> BorderColor = Color4.Black;
        public TextureMinFilter MinFilter = TextureMinFilter.Nearest;
        public TextureMagFilter MagFilter = TextureMagFilter.Linear;
        public TextureMinFilter MipMapMinFilter = TextureMinFilter.LinearMipmapLinear;
        public TextureMagFilter MipMapMagFilter = TextureMagFilter.Linear;
    }

    private int handle;


    static Texture()
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
    }

    public Texture(string pImagePath, Settings pSettings = default)
    {
        handle = GL.GenTexture();

        Use();

        // Texture wrapping
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int)pSettings.WrapSMode);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int)pSettings.WrapTMode);

        // Texture filtering
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)pSettings.MinFilter);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)pSettings.MagFilter);

        ImageResult image = ImageResult.FromStream(File.OpenRead(pImagePath), ColorComponents.RedGreenBlueAlpha);

        GL.TexImage2D(
            TextureTarget.Texture2d,
            pSettings.LevelOfDetail,
            InternalFormat.Rgba,
            image.Width,
            image.Height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            image.Data
        );

        GL.TexParameteri(
            TextureTarget.Texture2d,
            TextureParameterName.TextureMinFilter,
            (int)pSettings.MipMapMinFilter
        );
        GL.TexParameteri(
            TextureTarget.Texture2d,
            TextureParameterName.TextureMagFilter,
            (int)pSettings.MipMapMagFilter
        );

        GL.GenerateMipmap(TextureTarget.Texture2d);
    }

    public void Use()
    {
        GL.BindTexture(TextureTarget.Texture2d, handle);
    }
}