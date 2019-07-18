using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class Sphere: HitableObject
    {
        public Vector3 center;
        public float radius;
        static Random random=new Random();

        public static Vector3 RandomInUnitSphere()
        {
            Vector3 p;
            do
            {
                p = (new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()))*2 - Vector3.One;
            } while (p.Length() >= 1);

            return p;
        }

        public Sphere(Vector3 center, float radius,Material material)
        {
            this.center = center;
            this.radius = radius;
            base.material = material;
        }

        public override bool Hit(Ray ray,float tMin,float tMax, out HitRecord rec)
        {
            Vector3 oc = ray.from - center;
            float a = Vector3.Dot(ray.direction, ray.direction);
            float b = Vector3.Dot(oc, ray.direction);
            float c = Vector3.Dot(oc, oc) - radius * radius;
            float discriminant = (b * b) - (a * c);
            if( discriminant > 0)
            {
                float temp= (-b - MathF.Sqrt(discriminant)) / a;
                if (temp >= tMax || temp <= tMin)
                {
                    temp = (-b + MathF.Sqrt(discriminant))  / a;
                    if (temp >= tMax || temp <= tMin)
                    {
                        rec = null;
                        return false;
                    }
                }

                rec = new HitRecord();
                rec.t = temp;
                rec.position = ray.GetPointAt(rec.t);
                //rec.normal = Vector3.Normalize((rec.position - center);
                rec.normal = (rec.position - center)/radius;
                rec.material = material;
                return true;
            }


            rec = null;
            return false;
        }
    }


    public abstract class HitableObject
    {
        public Material material;
        public abstract bool Hit(Ray r, float tMin, float tMax, out HitRecord rec);
    }

}
