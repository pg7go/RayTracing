using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing
{
    public class Camera
    {
        Vector3 target;
        Vector3 horizontal;
        Vector3 vertical;
        Vector3 origin;
        public float lensRadius;
        Vector3 u, v, w;

        public Camera(Vector3 lookFrom, Vector3 lookat, Vector3 vup, float vfov, float aspect, float aperture, float focus_dist)
        {
            lensRadius = aperture / 2;
            float theta = vfov * MathF.PI / 180f;
            float half_height = MathF.Tan(theta / 2f);
            float half_width = aspect * half_height;

            origin = lookFrom;

            
            w = Vector3.Normalize(lookFrom - lookat);
            u = Vector3.Normalize(Vector3.Cross(vup, w));
            v = Vector3.Cross(w,u);

            horizontal = 2f * half_width * u* focus_dist;
            vertical = 2f * half_height * v* focus_dist;
            target =  -w*focus_dist;
        }



        public Ray GetRay(float u, float v)
        {
            Vector3 rd = lensRadius * RandomInUnitDisk();
            Vector3 offset = this.u * rd.X + this.v * rd.Y;
            return new Ray(origin+ offset, target + (u - 0.5f) * horizontal + (v - 0.5f) * vertical- offset);
        }

        static Random random = new Random();
        public static Vector3 RandomInUnitDisk()
        {
            Vector3 p;
            do
            {
                p = (new Vector3((float)random.NextDouble(), (float)random.NextDouble(), 0)) * 2 - new Vector3(1,1,0);
            } while (p.Length() >= 1);

            return p;
        }
    }
}
