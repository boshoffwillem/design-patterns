using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using System;
using Autofac;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            IRenderer renderer1 = new RasterRenderder();
            Circle circle1 = new(renderer1, 5);
            circle1.Draw();
            circle1.Resize(2);
            circle1.Draw();

            // The above code demonstrates the bridge between the Circle and the IRenderer.
            // The Circle is completely oblivious to what is rendering it.

            // The best practice way you would use the bridge pattern
            // is with a Dependency Injection container.
            ContainerBuilder servicesContainer = new();
            servicesContainer.RegisterType<VectorRenderer>()
                .As<IRenderer>();
            servicesContainer.Register((c, p) =>
                new Circle(c.Resolve<IRenderer>(),
                    p.Positional<float>(0)));

            using Autofac.IContainer services = servicesContainer.Build();
            Circle circle = services.Resolve<Circle>(
                new PositionalParameter(0, 5.0f)
            );
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
        }
    }

    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing circle with radius {radius}");
        }
    }

    public class RasterRenderder : IRenderer
    {
        public RasterRenderder()
        {
        }

        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle with radius {radius}");
        }
    }

    /// <summary>
    /// Let's suppose we some abstract hierarchy.
    /// <br />This is where the Bridge comes in.
    /// <br />Instead of specifying whether the Shape is
    /// <br />able to draw itself as a Raster form, or some other form,
    /// <br />you don't put this limitation in place.
    /// <br />You don't let Shape decide how it can be drawn.
    /// <br />Instead what you have to do is build a bridge between the Shape
    /// <br />and whoever is drawing it, which in this case is an IRenderer.
    /// </summary>
    public abstract class Shape
    {
        protected IRenderer Renderer;

        protected Shape(IRenderer renderer)
        {
            Renderer = renderer;
        }

        public abstract void Draw();

        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private float _radius;
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;
        }

        public override void Draw()
        {
            Renderer.RenderCircle(_radius);
        }

        public override void Resize(float factor)
        {
            _radius *= factor;
        }
    }
}
