using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

//include GLM library
using GlmNet;

using System.IO;
using System.Diagnostics;

namespace Graphics
{
    class Renderer
    {
        Shader sh;

        uint roofBufferID;
        uint bodyBufferID;
        uint doorBufferID;
        uint windowBufferID;
        uint windowlineBufferID;
        uint okraBufferID;
        uint tringle2BufferID;
        uint veticesBufferID_XYZ;

        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;
        mat4 mvp;
        vec3 Triangle_center;

        Stopwatch timer = Stopwatch.StartNew();

        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;
        public float transX = 0;
        public float transY = 0;
        public float transZ = 0;
        int MVP_ID;

        const float rotationSpeed = 0.1f;
        float rotAngel = 0;
        vec3 scale = new vec3(1, 1, 1);
        Texture tex1;
        Texture tex2;
        public void Initialize()
        {
            timer.Start();
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
            Gl.glClearColor(1, 1, 1.0f, 1);
            tex1 = new Texture(projectPath + "\\Textures\\home.jpg", 1);

            float[] vertices_Triangle = { 
           
                // 1- house roof
		        //-----
               
 		    //triangl1
                4.0f, 6.0f, -1.5f, //0
                0,0,0,
                0,0,

                0.0f,0.0f,0.0f,//1
                0,0,0,
                0,1,
                8.0f, 0.0f,0.0f,//2
                0,0,0,
                1,1,

                //triangle2
                4.0f, 6.0f, -1.5f, //3
                0,0,0,
                0,0,
               8.0f, 0.0f,0.0f,//4
                0,0,0,
                0,1,
               8.0f, 0.0f,-7.0f,//5
                0,0,0,
                1,1,

                //trinagle3
                4.0f, 6.0f, -1.5f, //6
                0,0,0,
                0,0,
               8.0f, 0.0f,-7.0f,//7
                0,0,0,
                0,1,
              0.0f, 0.0f,-7.0f,//8
                0,0,0,
                1,1,

                //triangle4
                 4.0f, 6.0f, -1.5f, //9
                0,0,0,
                0,0,
                0.0f, 0.0f,-7.0f,//10
                0,0,0,
                0,1,
                0.0f, 0.0f,0.0f,//11
                0,0,0,
                1,1,
            };


            // 2 - body top1
            //----------
            float[] vertices_Body = {
            0.0f,0.0f,0.0f,//12
            204,102,0,
            //0,1,

            0.0f,-9.0f,0.0f,//13
             204,102,0,
             //0,0,

            8.0f,-9.0f,0.0f,//15
             204,102,0,

            8.0f, 0.0f,0.0f,//17
            204,102,0,
        

            //top2

            0.0f,0.0f,-7.0f,//18
            204,102,0,
            0.0f,-9.0f,-7.0f,//19
             204,102,0,

            8.0f,-9.0f,-7.0f,//21
             204,102,0,

            8.0f, 0.0f,-7.0f,//23
            204,102,0,


            //combine1
            0.0f,0.0f,0.0f,//24
             0.9f,0.9f,0.3f,
            0.0f,0.0f,-7f,//25
              0.9f,0.9f,0.3f,

            0.0f,-9.0f,-7.0f,//26
             0.9f,0.9f,0.3f,
            0.0f,-9.0f,0.0f,//27
              0.9f,0.9f,0.3f,

            //combine2

            8.0f, 0.0f,0.0f,//28
             0.9f,0.9f,0.3f,
            8.0f, 0.0f,-7.0f,//29
            0.9f,0.9f,0.3f,

             8.0f,-9.0f,-7.0f,//30
              0.9f,0.9f,0.3f,
             8.0f,-9.0f,0.0f,//31
              0.9f,0.9f,0.3f,

            };
            float[] vertices_Window = {
           
             // 3 - window1
            //----------
            5.0f,-1.0f,0.0f,//32
            	0.0f, 1.0f, 1.0f,
            5.0f,-3.0f,0.0f,//33
            0.0f, 1.0f, 1.0f,

            5.0f,-3.0f,0.0f,//34
           0.0f, 1.0f, 1.0f,
            7.0f,-3.0f,0.0f,//35
            	0.0f, 1.0f, 1.0f,
            7.0f,-3.0f,0.0f,//36
            	0.0f, 1.0f, 1.0f,
            7.0f,-1.0f,0.0f,//37
            	0.0f, 1.0f, 1.0f,

            //window2

            1.0f,-1.0f,0.0f,//38
            	0.0f, 1.0f, 1.0f,
            1.0f,-3.0f,0.0f,//39
            		0.0f, 1.0f, 1.0f,

            1.0f,-3.0f,0.0f,//40
            		0.0f, 1.0f, 1.0f,
            3.0f,-3.0f,0.0f,//41
            		0.0f, 1.0f, 1.0f,

            3.0f,-3.0f,0.0f,//42
            		0.0f, 1.0f, 1.0f,
            3.0f,-1.0f,0.0f,//43
            		0.0f, 1.0f, 1.0f,

            };
            float[] vertices_door = {
            //4-door
            //-------

            3.0f,-4.0f,0.0f,//44
            0.153f, 0.76f, 0.0f,

            3.0f,-9.0f,0.0f,//45
            0.153f, 0.76f, 0.0f,

            3.0f,-9.0f,0.0f,//46
            	0.153f, 0.76f, 0.0f,

            5.0f,-9.0f,0.0f,//47
            	0.153f, 0.76f, 0.0f,

            5.0f,-9.0f,0.0f,//48
             0.153f, 0.76f, 0.0f,

            5.0f,-4.0f,0.0f,//49
            0.153f, 0.76f, 0.0f,


            };
            float[] vertices_WindowLine = {
            //window lines
            ////window line 1


            5.0f,-1.0f,0.0f,//50
             0,0,0,
            5.0f,-3.0f,0.0f,//51
            0,0,0,

            7.0f,-3.0f,0.0f,//52
            	 0,0,0,

            7.0f,-1.0f,0.0f,//53
            	 0,0,0,

            //     ///
                 
            6.0f,-1.0f,0.0f,//54
            0,0,0,
            6.0f,-3.0f,0.0f,//55
            0,0,0,


            5.0f,-2.0f,0.0f,//56
            0,0,0,
            7.0f,-2.0f,0.0f,//56
            0,0,0,

            //window line2

            1.0f,-1.0f,0.0f,//58
            	 0,0,0,
            1.0f,-3.0f,0.0f,//59
            		 0,0,0,

            3.0f,-3.0f,0.0f,//60
            		 0,0,0,

            3.0f,-1.0f,0.0f,//61
            		 0,0,0,

                     ////
            2.0f,-1.0f,0.0f,//62
            0,0,0,
            2.0f,-3.0f,0.0f,//63
            0,0,0,

            1.0f,-2.0f,0.0f,//64
            0,0,0,
            3.0f,-2.0f,0.0f,//65
            0,0,0,
            };

            float[] vertices_okra = {
                //okra

            3.5f,-6.5f,0.0f,//78
            0,0,0,

            };

            Triangle_center = new vec3(4, -2, -7);
            float[] vertices_XYZ = { 
                
            
                //x
		        0.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, //R
		        100.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f, //R
		        //y
	            0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, //G
		        0.0f, 100.0f, 0.0f,
                0.0f, 1.0f, 0.0f, //G
		        //z
	            0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f,  //B
		        0.0f, 0.0f, -100.0f,
                0.0f, 0.0f, 1.0f,  //B
                
            };



            roofBufferID = GPU.GenerateBuffer(vertices_Triangle);
            bodyBufferID = GPU.GenerateBuffer(vertices_Body);
            doorBufferID = GPU.GenerateBuffer(vertices_door);
            windowBufferID = GPU.GenerateBuffer(vertices_Window);
            windowlineBufferID = GPU.GenerateBuffer(vertices_WindowLine);
            okraBufferID = GPU.GenerateBuffer(vertices_okra);
            // tringle2BufferID = GPU.GenerateBuffer(vertices_Triangle2);
            veticesBufferID_XYZ = GPU.GenerateBuffer(vertices_XYZ);

            // Model Matrix Initialization

            // View matrix 
            ViewMatrix = glm.lookAt(
                        new vec3(15, 0, 20),
                        //new vec3(50, 50, 100),
                        new vec3(0, 0, 0),
                        new vec3(0, 1, 0)
                );

            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);
            List<mat4> l = new List<mat4>();
            List<mat4> l2 = new List<mat4>();

            ModelMatrix = new mat4(1);



            sh.UseShader();





            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());

        }

        public void Draw()
        {

            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);


            #region XYZ axis
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, veticesBufferID_XYZ);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_LINES, 0, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion


            #region roof
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, roofBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glEnableVertexAttribArray(2);
            Gl.glVertexAttribPointer(2, 2, Gl.GL_FLOAT, Gl.GL_FALSE, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));

            tex1.Bind();
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 0, 12);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            Gl.glDisableVertexAttribArray(2);
            #endregion





            #region body
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, bodyBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_QUADS, 0, 4);

            Gl.glDrawArrays(Gl.GL_QUADS, 4, 4);
            //c1
            Gl.glDrawArrays(Gl.GL_QUADS, 8, 4);
            //c2
            Gl.glDrawArrays(Gl.GL_QUADS, 12, 4);


            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region window 
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, windowBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 6);
            Gl.glDrawArrays(Gl.GL_POLYGON, 6, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region window lines
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, windowlineBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            //1
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 0, 4);
            Gl.glDrawArrays(Gl.GL_LINES, 4, 2);
            Gl.glDrawArrays(Gl.GL_LINES, 6, 2);
            //2
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 8, 4);
            Gl.glDrawArrays(Gl.GL_LINES, 12, 2);
            Gl.glDrawArrays(Gl.GL_LINES, 14, 2);
            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion
            #region door
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, doorBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region okra

            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, okraBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glPointSize(5);
            Gl.glDrawArrays(Gl.GL_POINTS, 0, 1);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

        }



        public void Update()
        {


            timer.Stop();
            float deltaT = timer.ElapsedMilliseconds / 1000.0f;
            rotAngel += rotationSpeed * deltaT;
            List<mat4> trans = new List<mat4>();
            trans.Add(glm.translate(new mat4(1), -1 * Triangle_center));
            trans.Add(glm.rotate(rotAngel, new vec3(0, 1, 0)));
            trans.Add(glm.translate(new mat4(1), Triangle_center));
            trans.Add(glm.translate(new mat4(1), new vec3(transX, 0, 0)));
            trans.Add(glm.translate(new mat4(1), new vec3(0, transY, 0)));
            trans.Add(glm.translate(new mat4(1), new vec3(0, 0, transZ)));
            trans.Add(glm.scale(new mat4(1), scale));
            ModelMatrix = MathHelper.MultiplyMatrices(trans);
            timer.Reset();
            timer.Restart();


        }


        public void keypress(char KeyChar)
        {
            float step = 0.5f;
            if (KeyChar == 'd')
                transX += step;
            if (KeyChar == 'a')
                transX -= step;
            if (KeyChar == 'w')
            {
                transY += step;
                scale.y *= 2;
            }
            if (KeyChar == 's')
            {
                transY -= step;
                scale.y /= 2;

            }
            if (KeyChar == 'z')
                transZ += step;
            if (KeyChar == 'c')
                transZ -= step;



        }



        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}