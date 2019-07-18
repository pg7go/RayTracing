using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Render
    {
        int width;
        int height;
        int samples;
        Color[,] colors;
        public float progress = 0;

        public Render(int width, int height, int samples)
        {
            this.width = width;
            this.height = height;
            this.samples = samples;
            colors = new Color[width, height];
        }

        static Random random = new Random();
        public void RenderScene(Scene scene,Camera camera,bool logToConsole =true)
        {
            //Parallel.For(0, height, y =>
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    Vector3 colorValue = Vector3.Zero;
                    for (int i = 0; i < samples; i++)
                    {
                        float xLerp = (float)(x + random.NextDouble()) / width;
                        float yLerp = (float)(y + random.NextDouble()) / height;
                        Ray ray = camera.GetRay(xLerp, yLerp);
                        colorValue += GetColorValue(ray, scene, 0);
                    }

                    colorValue /= samples;
                    // Gamma 2
                    colorValue.X = MathF.Sqrt(colorValue.X);
                    colorValue.Y = MathF.Sqrt(colorValue.Y);
                    colorValue.Z = MathF.Sqrt(colorValue.Z);

                    colors[x, height - y - 1] = Color.FromArgb((int)(255 * colorValue.X), (int)(255 * colorValue.Y), (int)(255 * colorValue.Z));

                    progress += (float)100 / height / width;
                    if (logToConsole)
                        LogToConsole();
                }

                

            }
        }


        public float progress_last = -100;
        public void LogToConsole()
        {
            if (progress - progress_last < 0.01f)
                return;
            progress_last = progress;

            Console.Clear();
            Console.WriteLine("Progress:");
            float num = progress / 10;
            for (int i = 0; i < 10; i++)
            {
                if (i >= num)
                    Console.Write("□");
                else
                    Console.Write("■");
            }
            Console.WriteLine($"  {progress:n}%");
        }


        public Vector3 GetColorValue(Ray ray, Scene scene, int depth)
        {

            if (scene.Hit(ray, 0.001f, float.MaxValue, out HitRecord rec))
            {
                if (depth < 50 && rec.material.Scatter(ray, rec, out Vector3 attenuation, out Ray scattered))
                {
                    return attenuation * GetColorValue(scattered, scene, depth + 1);
                }
                else
                {
                    return Vector3.Zero;
                }
            }

            Vector3 unitDirection = Vector3.Normalize(ray.direction);
            float t = (0.5f * unitDirection.Y) + 1.0f;
            Vector3 colorValue = ((1.0f - t) * new Vector3(1f, 1f, 1f)) + (t * new Vector3(0.5f, 0.7f, 1f));


            return colorValue;
        }


        public string CreateBmp(string fileName = "Render.png")
        {
            int width = colors.GetLength(0);
            int height = colors.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmp.SetPixel(x, y, colors[x, y]);
                }
            }

            bmp.Save(fileName, ImageFormat.Png);
            return $"{AppDomain.CurrentDomain.BaseDirectory}{fileName}";
        }



    }
}
