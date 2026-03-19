#if GL
namespace Silk.NET.OpenGL.Extensions.Hexa.ImGui
#elif GLES
namespace Silk.NET.OpenGLES.Extensions.Hexa.ImGui
#elif LEGACY
namespace Silk.NET.OpenGL.Legacy.Extensions.Hexa.ImGui
#endif
{
    class TextureInfo {
        public Texture Texture;
        public bool IsManaged;

        public TextureInfo(Texture texture, bool isManaged)
        {
            Texture = texture;
            IsManaged = isManaged;
        }
    }
}