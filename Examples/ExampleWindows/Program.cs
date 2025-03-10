// This file is part of Silk.NET.
//
// You may modify and distribute Silk.NET under the terms
// of the MIT license. See the LICENSE file for details.

using System.Drawing;
using Silk.NET.Windowing;
using Hexa.NET.ImGui;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.Hexa.ImGui;
using Hexa.NET.ImNodes;

namespace ExampleWindows
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Silk.NET window as usual
            using var window = Window.Create(WindowOptions.Default);

            // Declare some variables
            ImGuiController controller = null;
            GL gl = null;
            IInputContext inputContext = null;

            // Our loading function
            window.Load += () =>
            {
                controller = new ImGuiController(
                    gl = window.CreateOpenGL(), // load OpenGL
                    window, // pass in our window
                    inputContext = window.CreateInput(), // create an input context
                    null,
                    () => ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable
                );

                // Initialize ImNodes
                ImNodes.SetImGuiContext(controller.Context);
                ImNodes.SetCurrentContext(ImNodes.CreateContext());
                ImNodes.StyleColorsDark(ImNodes.GetStyle());
            };

            // Handle resizes
            window.FramebufferResize += s =>
            {
                // Adjust the viewport to the new window size
                gl.Viewport(s);
            };

            // The render function
            window.Render += delta =>
            {
                // Make sure ImGui is up-to-date
                controller.Update((float) delta);

                // This is where you'll do any rendering beneath the ImGui context
                // Here, we just have a blank screen.
                gl.ClearColor(Color.FromArgb(255, (int) (.45f * 255), (int) (.55f * 255), (int) (.60f * 255)));
                gl.Clear((uint) ClearBufferMask.ColorBufferBit);

                // This is where you'll do all of your ImGUi rendering
                // Here, we're just showing the ImGui built-in demo window.
                ImGui.DockSpaceOverViewport();
                ImGui.ShowDemoWindow();

                // Render the node editor
                ImNodes.BeginNodeEditor();
                ImNodes.BeginNode(0);
                ImGui.Text("Hello, world!");
                ImNodes.EndNode();
                ImNodes.EndNodeEditor();

                // Make sure ImGui renders too!
                controller.Render();
            };

            // The closing function
            window.Closing += () =>
            {
                // Dispose our controller first
                controller?.Dispose();

                // Dispose the input context
                inputContext?.Dispose();

                // Unload OpenGL
                gl?.Dispose();
            };

            // Now that everything's defined, let's run this bad boy!
            window.Run();

            window.Dispose();
        }
    }
}
