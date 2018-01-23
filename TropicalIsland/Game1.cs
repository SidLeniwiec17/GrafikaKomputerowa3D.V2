using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TropicalIsland.Objects;

namespace TropicalIsland
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        bool useDefaultBasicEffect = false;
        bool IsFullScreen = false;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Model palmModel;
        Texture2D palmTexture;
        Model rockModel;
        Texture2D rockTexture;
        Vector4[] fale;

        Model robotModel;

        int skyboxFirstVertIndex;

        Texture2D ocean1Texture;
        Texture2D ocean2Texture;
        Texture2D ocean3Texture;
        Texture2D dnoTexture;
        Texture2D EnvironmentMap;

        SkyBox skyboxObject;

        Random random = new Random();

        //Camera
        Camera camera;
        public List<Object3D> palms;
        public List<Object3D> rocks;
        Object3D glassPalm;

        //BasicEffect for rendering
        BasicEffect basicEffect;

        Effect custom_effect;
        Effect alfa_effect;
        Effect glass_effect;
        Effect ocean_effect;

        Effect skyBox_effect;
        Texture2D skyUp;
        Texture2D skyDown;
        Texture2D skyLeft;
        Texture2D skyRight;
        Texture2D skyFront;
        Texture2D skyBack;

        Texture2D texToChange;

        //Geometric info
        Vertexes vertexes;
        Vertexes vertexesSkyBox;
        Vertexes vertexesAlfa;
        Vertexes vertexesTest;
        int dnoVerticeCount = 0;

        RenderTarget2D renderTarget;
        RenderTargetCube RefCubeMap;

        public bool change;
        public bool first;
        float time;

        public Game1()
        {
            skyboxFirstVertIndex = 0;
            change = false;
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.IsFullScreen = IsFullScreen;
            if (graphics.IsFullScreen)
            {
                graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            }
            else
            {
                graphics.PreferredBackBufferWidth = (1920 / 3) * 2;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = (1080 / 3) * 2;   // set this value to the desired height of your window
            }
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            first = true;
            camera = new Camera();
            camera.Init(GraphicsDevice);
            skyboxObject = new SkyBox();
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1.0f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            basicEffect.PreferPerPixelLighting = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            vertexes = new Vertexes(GraphicsDevice);
            vertexesAlfa = new Vertexes(GraphicsDevice);
            vertexesTest = new Vertexes(GraphicsDevice);
            vertexesSkyBox = new Vertexes(GraphicsDevice);
            //Sphere
            Sphere sphere = new Sphere(100.0f, new Vector3(0.0f, 120.0f, 0.0f), 16, 0.0f, 0.0f, (float)Math.PI, 1.5f);
            VertexPositionNormalTexture[] testSphere = sphere.Init(true);

            //Ocean
            OceanSurface ocean = new OceanSurface(new Vector3(0, -4.8f, 0), 0, 0, 0, 10f);
            VertexPositionNormalTexture[] testOcean = ocean.Init();

            //Ocean
            OceanSurface dno = new OceanSurface(new Vector3(0, -5.2f, 0), 0, 0, 0, 10f);
            VertexPositionNormalTexture[] testdno = dno.Init(true);
            dnoVerticeCount = testdno.Length;
            VertexPositionNormalTexture[] skyCube = skyboxObject.initTestCubeSkybox(new Vector3(0, 0, 0));
            VertexPositionNormalTexture[] testCube = HelperClass.initTestCube();

            //Palms
            palms = new List<Object3D>();
            InitPalms();

            //Rocks
            rocks = new List<Object3D>();
            InitRocks();

            vertexes.addObject(testSphere, true);
            vertexesAlfa.addObject(testOcean, true);
            vertexes.addObject(testdno, false);
            skyboxFirstVertIndex = vertexes.triangleVertices.Length;
            vertexesSkyBox.addObject(skyCube, true);
            vertexesTest.addObject(testCube, true);

            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            RefCubeMap = new RenderTargetCube(this.GraphicsDevice, 256, true, SurfaceFormat.Color, DepthFormat.Depth16, 1, RenderTargetUsage.PreserveContents);

            fale = new Vector4[] { new Vector4(1.0f, 0.0f, 0.1f, 0.4f), new Vector4(1.0f, 1.0f, 0.2f, 0.3f), new Vector4(1.0f, 1.0f, 0.4f, 0.16f),
            new Vector4(0.0f, 0.0f, 0.6f, 0.08f), new Vector4(-1.0f, 0.0f, 1.0f, 0.1f)};
            time = 0.0f;
            base.Initialize();
        }

        private void InitPalms()
        {
            for (int i = 0; i < 4; i++)
            {
                float x = (i * 15.0f) - 30.0f + GetRandomNumber(-5.0f, 5.0f);
                float y = -40.0f + GetRandomNumber(-5.0f, 5.0f);
                float z = 0.0f + GetRandomNumber(-10.0f, 10.0f);
                palms.Add(new Object3D(new Vector3(x, y, z), 0.0f, i * 1.0f, 0.0f, 0.04f));
            }

            for (int i = 0; i < 3; i++)
            {
                float x = (i * 15.0f) - 20.0f + GetRandomNumber(-5.0f, 5.0f);
                float y = -50.0f + GetRandomNumber(-5.0f, 5.0f);
                float z = -35.0f + GetRandomNumber(-5.0f, 5.0f);
                palms.Add(new Object3D(new Vector3(x, y, z), 0.0f, i * 1.2f, 0.0f, 0.04f));
            }

            for (int i = 0; i < 4; i++)
            {
                float x = (i * 15.0f) - 30.0f + GetRandomNumber(-5.0f, 5.0f);
                float y = -50.0f + GetRandomNumber(-5.0f, 5.0f);
                float z = 35.0f + GetRandomNumber(-5.0f, 5.0f);
                palms.Add(new Object3D(new Vector3(x, y, z), 0.0f, i * 1.0f, 0.0f, 0.04f));
            }
            //glassPalm = new Object3D(new Vector3(85.0f, -15.0f, 0.0f), 0.0f, 0.0f, 0.0f, 0.65f);
            glassPalm = new Object3D(new Vector3(60.0f, -10.0f, 0.0f), 0.0f, 0.0f, 0.0f, 10.0f);
        }

        private void InitRocks()
        {
            rocks.Add(new Object3D(new Vector3(0.0f, -4.8f, 0.0f), 0.0f, 0.0f, 0.0f, 5.0f));
            rocks.Add(new Object3D(new Vector3(-1.0f, -4.4f, 2.0f), 0.0f, 0.0f, 0.0f, 5.0f));
            rocks.Add(new Object3D(new Vector3(2.0f, -8.0f, 15.0f), 0.0f, 0.0f, 0.0f, 5.0f));
            rocks.Add(new Object3D(new Vector3(0.0f, -7.0f, 14.0f), 0.0f, 0.0f, 0.0f, 5.0f));
            rocks.Add(new Object3D(new Vector3(-2.0f, -7.5f, 14.5f), 0.0f, 0.0f, 0.0f, 5.0f));
        }

        public float GetRandomNumber(float minimum, float maximum)
        {
            double sample = random.NextDouble();
            float result = ((float)(sample) * (maximum - minimum)) + minimum;
            return result;
        }

        public void MoveSkyBox(Vector3 camPos)
        {
            VertexPositionNormalTexture[] skypos = skyboxObject.initTestCubeSkybox(camPos);
            // int counter = 0;
            // for (int i = indexStart; i < indexStart + (6 * 6); i++)
            // {
            vertexesSkyBox.triangleVertices = skypos;
            //      counter++;
            //}
            VertexBuffer newvertexBuffer = new VertexBuffer(graphics.GraphicsDevice, typeof(VertexPositionNormalTexture), vertexesSkyBox.triangleVertices.Length, BufferUsage.
                              WriteOnly);
            newvertexBuffer.SetData<VertexPositionNormalTexture>(vertexesSkyBox.triangleVertices);
            vertexesSkyBox.vertexBuffer = newvertexBuffer;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            palmModel = this.Content.Load<Model>("Models/MY_PALM");
            palmTexture = this.Content.Load<Texture2D>("Models/palm1_uv_m2");
            rockModel = this.Content.Load<Model>("Models/Rock");
            rockTexture = this.Content.Load<Texture2D>("Models/RockTexture");
            //custom_effect = this.Content.Load<Effect>("Shaders/texturing");
            custom_effect = this.Content.Load<Effect>("Shaders/phong");
            alfa_effect = this.Content.Load<Effect>("Shaders/phongAlpha");
            glass_effect = this.Content.Load<Effect>("Shaders/glass");
            ocean_effect = this.Content.Load<Effect>("Shaders/ocean");

            ocean1Texture = this.Content.Load<Texture2D>("Models/ocean1");
            ocean2Texture = this.Content.Load<Texture2D>("Models/ocean2");
            ocean3Texture = this.Content.Load<Texture2D>("Models/ocean3");
            dnoTexture = this.Content.Load<Texture2D>("Models/dno");

            skyBox_effect = this.Content.Load<Effect>("SkyBox/skyShader");
            skyFront = this.Content.Load<Texture2D>("SkyBox/front");
            skyRight = this.Content.Load<Texture2D>("SkyBox/right");
            skyLeft = this.Content.Load<Texture2D>("SkyBox/left");
            skyUp = this.Content.Load<Texture2D>("SkyBox/up");
            skyDown = this.Content.Load<Texture2D>("SkyBox/down");
            skyBack = this.Content.Load<Texture2D>("SkyBox/back");
            texToChange = ocean3Texture;

            robotModel = Content.Load<Model>("Models/free_car_1");
            //robotModel = Content.Load<Model>("Models/rifle");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MoveSkyBox(camera.CamPosition);

            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {
                change = true;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                if (change)
                {
                    if (texToChange == ocean3Texture)
                    {
                        texToChange = ocean1Texture;
                        change = false;
                    }
                    else
                    {
                        texToChange = ocean3Texture;
                        change = false;
                    }
                }
            }
            // TODO: Add your update logic here
            camera.Update(gameTime);

            base.Update(gameTime);
        }

        private void DoTheCubeMap()
        {
            Matrix viewMatrix;
            var pos = new Vector3(-450.0f, -20.0f, 0.0f);
            Matrix world = Matrix.CreateTranslation(new Vector3(0.0f, 0.0f, 0.0f));
            CubeMapFace cubeMapFace = CubeMapFace.NegativeX;
            glass_effect.Parameters["World"].SetValue(camera.WorldMatrix);
            glass_effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);

            for (int i = 0; i < 6; i++)
            {
                // render the scene to all cubemap faces
                cubeMapFace = (CubeMapFace)i;
                switch (cubeMapFace)
                {
                    case CubeMapFace.NegativeX:
                        {
                            viewMatrix = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Right, Vector3.Up);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.NegativeY:
                        {
                            viewMatrix = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Up, Vector3.Forward);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.NegativeZ:
                        {
                            viewMatrix = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Forward, Vector3.Up);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.PositiveX:
                        {
                            viewMatrix = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Left, Vector3.Up);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.PositiveY:
                        {
                            viewMatrix = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Down, Vector3.Backward);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.PositiveZ:
                        {
                            viewMatrix = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 0.0f), Vector3.Backward, Vector3.Up);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetVertexBuffer(null);
            if (first)
            {
                first = false;
                GraphicsDevice.Clear(Color.Black);
                GraphicsDevice.SetVertexBuffer(vertexes.vertexBuffer);
                //Turn off culling so we see both sides of our rendered triangle
                RasterizerState rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                GraphicsDevice.RasterizerState = rasterizerState;

                GraphicsDevice.SetRenderTarget(renderTarget);
                DrawScene(gameTime);
                DrawSkyBox();
                DoTheCubeMap();
                this.EnvironmentMap = renderTarget;
                glass_effect.Parameters["ReflectionCubeMap"].SetValue(EnvironmentMap);
            }
            GraphicsDevice.SetVertexBuffer(null);
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Reset();
            GraphicsDevice.SetVertexBuffer(vertexes.vertexBuffer);
            DrawScene(gameTime);
            DrawSkyBox();
            DrawTestCube(camera);
            DrawGlassPalm(camera);
            base.Draw(gameTime);
        }

        public void DrawGlassPalm(Camera camera)
        {
            glassPalm.DrawModelWithGlassEffect(robotModel, camera, glass_effect);
        }

        public void DrawTestCube(Camera camera)
        {
            custom_effect.Parameters["World"].SetValue(camera.WorldMatrix);
            custom_effect.Parameters["View"].SetValue(camera.ViewMatrix);
            custom_effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
            custom_effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
            custom_effect.Parameters["AmbientIntensity"].SetValue(0.2f);
            custom_effect.Parameters["LightDirection"].SetValue(new Vector3(0.0f, 0.5f, 1.0f));
            custom_effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector4());
            custom_effect.Parameters["DiffuseIntensity"].SetValue(0.7f);
            custom_effect.Parameters["SpecularColor"].SetValue(Color.White.ToVector4());
            custom_effect.Parameters["ModelTexture"].SetValue(EnvironmentMap);
            custom_effect.Parameters["CameraPosition"].SetValue(camera.CamPosition);
            custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);

            GraphicsDevice.SetVertexBuffer(vertexesTest.vertexBuffer);
            foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexesTest.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexesTest.vertexBuffer.VertexCount);
                }

            }
        }

        public void DrawRocks()
        {
            basicEffect.Projection = camera.ProjectionMatrix;
            basicEffect.View = camera.ViewMatrix;
            basicEffect.World = camera.WorldMatrix;
            basicEffect.AmbientLightColor = Color.White.ToVector3();
            basicEffect.DiffuseColor = Color.White.ToVector3();
            basicEffect.SpecularColor = Color.White.ToVector3();
            basicEffect.SpecularPower = 0.2f;

            foreach (var r in rocks)
            {
                r.Draw(rockModel, basicEffect, rockTexture);
            }
        }

        public void DrawOcean(Effect alfa_effect, Camera camera, Texture2D ocean2texture, Texture2D textochange, GameTime gametime)
        {
            time = time + 1.5f * (float)gametime.ElapsedGameTime.TotalSeconds;

            alfa_effect.Parameters["World"].SetValue(camera.WorldMatrix);
            alfa_effect.Parameters["View"].SetValue(camera.ViewMatrix);
            alfa_effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
            alfa_effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
            alfa_effect.Parameters["AmbientIntensity"].SetValue(0.9f);
            alfa_effect.Parameters["LightDirection"].SetValue(new Vector3(0.0f, 0.5f, 1.0f));
            alfa_effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector4());
            alfa_effect.Parameters["DiffuseIntensity"].SetValue(0.1f);
            alfa_effect.Parameters["SpecularColor"].SetValue(Color.White.ToVector4());
            alfa_effect.Parameters["CameraPosition"].SetValue(camera.CamPosition);
            alfa_effect.Parameters["ModelTexture"].SetValue(ocean2texture);
            alfa_effect.Parameters["TextureAlpha"].SetValue(0.65f);
            alfa_effect.Parameters["ModelTexture2"].SetValue(textochange);
            alfa_effect.Parameters["TextureAlpha2"].SetValue(0.45f);
            alfa_effect.Parameters["Wave"].SetValue(fale);
            alfa_effect.Parameters["time"].SetValue(time);
            GraphicsDevice.SetVertexBuffer(vertexesAlfa.vertexBuffer);
            foreach (EffectPass pass in alfa_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexesAlfa.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexesAlfa.vertexBuffer.VertexCount);
                }
            }
        }

        public void DrawSkyBox()
        {
            custom_effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
            custom_effect.Parameters["AmbientIntensity"].SetValue(1.0f);
            custom_effect.Parameters["LightDirection"].SetValue(new Vector3(0.0f, 1.0f, 0.0f));
            custom_effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector4());
            custom_effect.Parameters["DiffuseIntensity"].SetValue(0.0f);
            custom_effect.Parameters["SpecularColor"].SetValue(Color.White.ToVector4());
            custom_effect.Parameters["ModelTexture"].SetValue(skyFront);
            custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
            GraphicsDevice.SetVertexBuffer(vertexesSkyBox.vertexBuffer);
            foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexesSkyBox.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexesSkyBox.vertexBuffer.VertexCount - 6);
                }
            }

            custom_effect.Parameters["ModelTexture"].SetValue(skyLeft);
            custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
            foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexes.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 6, 12);
                }
            }

            custom_effect.Parameters["ModelTexture"].SetValue(skyRight);
            custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
            foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexes.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 12, 18);
                }
            }

            custom_effect.Parameters["ModelTexture"].SetValue(skyUp);
            custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
            foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexes.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 18, 24);
                }
            }

            custom_effect.Parameters["ModelTexture"].SetValue(skyDown);
            custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
            foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexes.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 24, 30);
                }
            }

            custom_effect.Parameters["ModelTexture"].SetValue(skyBack);
            custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
            foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexes.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 30, vertexesSkyBox.vertexBuffer.VertexCount);
                }
            }
        }

        public void DrawScene(GameTime gametime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Texture2D mySolidColorTexture = new SolidColorTexture(GraphicsDevice, Color.Yellow);

            if (useDefaultBasicEffect)
            {
            }
            else
            {
                custom_effect.Parameters["World"].SetValue(camera.WorldMatrix);
                custom_effect.Parameters["View"].SetValue(camera.ViewMatrix);
                custom_effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
                custom_effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                custom_effect.Parameters["AmbientIntensity"].SetValue(0.2f);
                custom_effect.Parameters["LightDirection"].SetValue(new Vector3(0.0f, 0.5f, 1.0f));
                custom_effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector4());
                custom_effect.Parameters["DiffuseIntensity"].SetValue(0.7f);
                custom_effect.Parameters["SpecularColor"].SetValue(Color.White.ToVector4());
                custom_effect.Parameters["ModelTexture"].SetValue(mySolidColorTexture);
                custom_effect.Parameters["CameraPosition"].SetValue(camera.CamPosition);
                custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);


                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexes.vertexBuffer.VertexCount - dnoVerticeCount);
                    }

                }

                custom_effect.Parameters["ModelTexture"].SetValue(dnoTexture);
                custom_effect.Parameters["TextureAlpha"].SetValue(0.6f);
                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, vertexes.vertexBuffer.VertexCount - dnoVerticeCount, vertexes.vertexBuffer.VertexCount);
                    }
                }



                foreach (var p in palms)
                {
                    p.DrawModelWithEffect(palmModel, camera, custom_effect, palmTexture);
                }

                DrawRocks();
                DrawOcean(ocean_effect, camera, ocean2Texture, texToChange, gametime);
            }
        }
    }
}
