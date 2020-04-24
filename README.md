# 前言  
学校的学生团队只有一位搞图形学的大佬，他每天会在屏幕前呆呆地望着一个红色小球。  
他每次都能说出一些深奥的词汇，但是每每与其交流都在劝我不要搞图形学，太头秃了。  
在我大二时他就已经大四并差不多离校了，之后，学校基本上就没有搞图形学的了。  
想了想毕竟我是想走游戏方向的，学一学这方面知识总是有好处的。  
  
在本项目基础上我继续做了个CPU离线图形渲染器，支持本项目所有功能，以及OBJ模型导入，开源地址：[CPU-Render](https://github.com/pg7go/CPU-Render)  
  
# 简介  
本项目是用的最常见的一种光线追踪结构，是书
[Ray Tracing in One Weekend by Peter Shirley](http://in1weekend.blogspot.com/2016/01/ray-tracing-in-one-weekend.html)
的C#实现版本，可能与书上有一些小地方的出入，不过达到的结果几乎是一样的  

![img](https://raw.githubusercontent.com/pg7go/RayTracing/master/Screenshots/Chapter_12.png)  
![img](https://raw.githubusercontent.com/pg7go/RayTracing/master/Screenshots/Chapter_8.png)  
![img](https://raw.githubusercontent.com/pg7go/RayTracing/master/Screenshots/Chapter_9.png)  
![img](https://raw.githubusercontent.com/pg7go/RayTracing/master/Screenshots/Chapter_10.png)  
![img](https://raw.githubusercontent.com/pg7go/RayTracing/master/Screenshots/Chapter_11.png)  
![img](https://raw.githubusercontent.com/pg7go/RayTracing/master/Screenshots/progress.png)  

# 环境
这里我是用的.Net Core 3.0   
  
# 使用方法
1.渲染一个场景，首先需要一个渲染器（宽度，高度，取样次数（抗锯齿））  
`Render render = new Render(400, 200, 100);`  
  
2.建立一个场景  
`Scene scene = new Scene();`  
  
3.添加一个可光线碰撞的物体  
`scene.hitableObjects.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, new Lambertian(new Vector3(0.8f, 0.3f, 0.3f))));`  
  
4.建立一个摄像机并设置位置和朝向  
`Camera camera = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, -1), Vector3.UnitY, 90,2,0,1);`  
  
5.开始渲染  
`render.RenderScene(scene, camera)`  
  
6.保存文件并打开  
`ShowPic(render.CreateBmp());`  
   
# 踩过的一些坑  
踩过的最大的一个坑是球体碰撞时法线设置那里  
书上是用：`(rec.position - center)/radius; `  
我是用：`rec.normal = Vector3.Normalize(rec.position - center); `   
作用都一样，但是到了后面的第九章那里  
遇到了球的半径居然是负数！！！  
然后渲染不出来对应的图像！  
卡了几个小时，代码反复比对很多遍，也在网上找对应的资料看了很多遍…  
居然是在这里中的坑……  
  
# 参考
[Ray Tracing in One Weekend by Peter Shirley](http://in1weekend.blogspot.com/2016/01/ray-tracing-in-one-weekend.html)  
[林兮大大的光线追踪blog（超详细）](https://www.cnblogs.com/lv-anchoret/category/1368696.html)  
[jcant0n/RayTracingInAWeekend](https://github.com/jcant0n/RayTracingInAWeekend)  
  


