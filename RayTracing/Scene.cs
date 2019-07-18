using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class Scene
    {
        public List<HitableObject> hitableObjects = new List<HitableObject>();
        public bool Hit(Ray ray, float tMin, float tMax, out HitRecord rec)
        {
            rec = null;
            bool hitAnything = false;
            float closestSoFar = tMax;

            foreach (var ob in hitableObjects)
            {
                if (ob.Hit(ray, tMin, tMax, out HitRecord recTemp))
                {
                    hitAnything = true;
                    if(closestSoFar>recTemp.t)
                    {
                        closestSoFar = recTemp.t;
                        rec = recTemp;
                    }
                }
            }

            return hitAnything;
        }


        static Random random = new Random();
        static float randF { get { return (float)random.NextDouble(); } }


        public static Scene CreateRandomScene(int objectNumber = 500)
        {
            Scene scene = new Scene();
            for (int a = -objectNumber; a < objectNumber; a++)
            {
                for (int b = -objectNumber; b < objectNumber; b++)
                {
                    float choose_mat = randF;
                    Vector3 center = new Vector3(a + 0.9f * randF, 0.2f, b + 0.9f * randF);
                    if ((center - new Vector3(4f, 0.2f, 0)).Length() > 0.9f)
                    {
                        if (choose_mat < 0.8f) // Diffuse
                        {
                            scene.hitableObjects.Add(new Sphere(center, 0.2f, new Lambertian(new Vector3(randF * randF, randF * randF, randF * randF))));
                        }
                        else if (choose_mat < 0.95f) // Metal
                        {
                            scene.hitableObjects.Add(new Sphere(center, 0.2f, new Metal(new Vector3(0.5f * (1f + randF), 0.5f * (1f + randF), 0.5f * (1 + randF)), 0.5f * randF)));
                        }
                        else // glass
                        {
                            scene.hitableObjects.Add(new Sphere(center, 0.2f, new Dielectric(1.5f)));
                        }
                    }
                }
            }

            scene.hitableObjects.Add(new Sphere(new Vector3(0, -1000, 0), 1000, new Lambertian(new Vector3(0.5f, 0.5f, 0.5f))));

            scene.hitableObjects.Add(new Sphere(new Vector3(0, 1, 0), 1f, new Dielectric(1.5f)));
            scene.hitableObjects.Add(new Sphere(new Vector3(-4, 1, 0), 1f, new Lambertian(new Vector3(0.4f, 0.2f, 0.1f))));
            scene.hitableObjects.Add(new Sphere(new Vector3(4f, 1f, 0f), 1f, new Metal(new Vector3(0.7f, 0.6f, 0.5f), 0f)));


            return scene;

        }


    }
}
