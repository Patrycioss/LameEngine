
namespace LameEngine;

public class Renderer
{
    private Frame frame;
    private Window window;
    
    public Renderer(Window pWindow)
    {
        window = pWindow;
        frame = new Frame(pWindow.Bounds.X, pWindow.Bounds.Y);
    }
    
}