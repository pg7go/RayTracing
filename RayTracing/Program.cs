using System;
using System.Diagnostics;
using System.Numerics;

namespace RayTracing
{
    class Program
    {
        

        static void Main(string[] args)
        {




            //Scene scene = new Scene();
            //Scene scene7 = new Scene();
            //scene7.hitableObjects.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, new Lambertian(new Vector3(0.8f, 0.3f, 0.3f))));
            //scene7.hitableObjects.Add(new Sphere(new Vector3(0, -100.5f, -1), 100, new Lambertian(new Vector3(0.8f, 0.8f, 0f))));
            //scene7.hitableObjects.Add(new Sphere(new Vector3(1, 0, -1), 0.5f, new Metal(new Vector3(0.8f, 0.6f, 0.2f), 1f)));
            //scene7.hitableObjects.Add(new Sphere(new Vector3(-1, 0, -1), 0.5f, new Metal(new Vector3(0.8f, 0.8f, 0.8f), 0.3f)));

            //scene.hitableObjects.Add(new Sphere(new Vector3(-2, 0, -1), 0.5f, new Lambertian(new Vector3(0.3f, 0.8f, 0.3f))));
            //scene.hitableObjects.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, new Lambertian(new Vector3(0.8f, 0.3f, 0.3f))));
            //scene.hitableObjects.Add(new Sphere(new Vector3(0, -100.5f, -1), 100, new Lambertian(new Vector3(0.8f, 0.8f, 0f))));
            //scene.hitableObjects.Add(new Sphere(new Vector3(1, 0, -1), 0.5f, new Metal(new Vector3(0.2f, 0.6f, 0.6f), 0.2f)));
            //scene.hitableObjects.Add(new Sphere(new Vector3(-1, 0, -1), 0.5f, new Dielectric(1.5f)));
            //scene.hitableObjects.Add(new Sphere(new Vector3(-1, 0, -1), -0.45f, new Dielectric(1.5f)));




            int width = 800;
            int height = 400;
            int samples = 200;

            Vector3 lookFrom = new Vector3(13, 2, 3);
            Vector3 lookat = new Vector3(0, 0, 0f);
            Camera camera = new Camera(lookFrom, lookat, Vector3.UnitY, 20,width/height,0,(lookFrom- lookat).Length());

            Render render = new Render(width, height, samples);
            render.RenderScene(Scene.CreateRandomScene(11), camera);


            ShowPic(render.CreateBmp());
        }





        public static void ShowPic(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = path;
            startInfo.UseShellExecute = true;
            startInfo.CreateNoWindow = true;
            startInfo.Verb = string.Empty;
            Process.Start(startInfo);
        }





    }
}
