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
        bool IsFullScreen = true;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Model palmModel;
        Texture2D palmTexture;
        Model rockModel;
        Texture2D rockTexture;
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
        Vertexes vertexesAlfa;
        int dnoVerticeCount = 0;

        RenderTarget2D renderTarget;
        RenderTargetCube RefCubeMap;

        public bool multi;
        public bool change;

        public Game1()
        {
            multi = true;
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
                graphics.PreferredBackBufferWidth = 1920 / 2;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 1080 / 2;   // set this value to the desired height of your window
            }
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera();
            camera.Init(GraphicsDevice);
            skyboxObject = new SkyBox();
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1.0f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            basicEffect.PreferPerPixelLighting = true;
            GraphicsDevice.PresentationParameters.MultiSampleCount = 2;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            vertexes = new Vertexes(GraphicsDevice);
            vertexesAlfa = new Vertexes(GraphicsDevice);
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
            vertexes.addObject(skyCube, false);

            renderTarget = new RenderTarget2D(
                GraphicsDevice,
                GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            RefCubeMap = new RenderTargetCube(this.GraphicsDevice, 256, true, SurfaceFormat.Color, DepthFormat.Depth16, 1, RenderTargetUsage.PreserveContents);

            base.Initialize();
        }

        private void InitPalms()
        {
            for (int i = 0; i < 4; i++)
            {
                float x = (i * 400.0f) - 900.0f + GetRandomNumber(-200.0f, 200.0f);
                float y = -1000.0f + GetRandomNumber(-100.0f, 0.0f);
                float z = 0.0f + GetRandomNumber(-200.0f, 200.0f);
                palms.Add(new Object3D(new Vector3(x, y, z), 0.0f, i * 1.0f, 0.0f, 0.045f));
            }

            for (int i = 0; i < 3; i++)
            {
                float x = (i * 400.0f) - 500.0f + GetRandomNumber(-200.0f, 200.0f);
                float y = -1200.0f + GetRandomNumber(-100.0f, 0.0f);
                float z = -600.0f + GetRandomNumber(-200.0f, 200.0f);
                palms.Add(new Object3D(new Vector3(x, y, z), 0.0f, i * 1.2f, 0.0f, 0.045f));
            }

            for (int i = 0; i < 4; i++)
            {
                float x = (i * 400.0f) - 700.0f + GetRandomNumber(-200.0f, 200.0f);
                float y = -1000.0f + GetRandomNumber(-100.0f, 0.0f);
                float z = 600.0f + GetRandomNumber(-200.0f, 200.0f);
                palms.Add(new Object3D(new Vector3(x, y, z), 0.0f, i * 1.0f, 0.0f, 0.045f));
            }
            glassPalm = new Object3D(new Vector3(-1450.0f, -1000.0f, 0.0f), 0.0f, 0.0f, 0.0f, 0.045f);
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

        public void MoveSkyBox(Vertexes vertexes, int indexStart, Vector3 camPos)
        {
            VertexPositionNormalTexture[] skypos = skyboxObject.initTestCubeSkybox(camPos);
            int counter = 0;
            for (int i = indexStart; i < indexStart + (6 * 6); i++)
            {
                vertexes.triangleVertices[i] = skypos[counter];
                counter++;
            }
            VertexBuffer newvertexBuffer = new VertexBuffer(graphics.GraphicsDevice, typeof(VertexPositionNormalTexture), vertexes.triangleVertices.Length, BufferUsage.
                              WriteOnly);
            newvertexBuffer.SetData<VertexPositionNormalTexture>(vertexes.triangleVertices);
            vertexes.vertexBuffer = newvertexBuffer;
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
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MoveSkyBox(vertexes, skyboxFirstVertIndex, camera.CamPosition);

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                if (change)
                {
                    if (multi == false)
                    {
                        graphics.PreferMultiSampling = true;
                        GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                        multi = true;
                        change = false;
                        graphics.ApplyChanges();
                    }
                    else
                    {
                        graphics.PreferMultiSampling = false;
                        multi = false;
                        change = false;
                        graphics.ApplyChanges();
                    }
                }
            }
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
            Matrix world = Matrix.CreateTranslation(new Vector3(-1500.0f, -1000.0f, 0.0f));
            CubeMapFace cubeMapFace = CubeMapFace.NegativeX;           
            glass_effect.Parameters["World"].SetValue(world);
            glass_effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);

            for (int i = 0; i < 6; i++)
            {
                // render the scene to all cubemap faces
                cubeMapFace = (CubeMapFace)i;
                switch (cubeMapFace)
                {
                    case CubeMapFace.NegativeX:
                        {
                            viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Left, Vector3.Up);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.NegativeY:
                        {
                            viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Down, Vector3.Forward);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.NegativeZ:
                        {
                            viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Backward, Vector3.Up);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.PositiveX:
                        {
                            viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Right, Vector3.Up);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.PositiveY:
                        {
                            viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Up, Vector3.Backward);
                            this.GraphicsDevice.SetRenderTarget(RefCubeMap, cubeMapFace);
                            this.GraphicsDevice.Clear(Color.White);
                            glass_effect.Parameters["View"].SetValue(viewMatrix);
                            GraphicsDevice.SetRenderTarget(null);
                            break;
                        }
                    case CubeMapFace.PositiveZ:
                        {
                            viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
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
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetVertexBuffer(vertexes.vertexBuffer);
            //Turn off culling so we see both sides of our rendered triangle
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;


            GraphicsDevice.SetRenderTarget(renderTarget);
            DrawScene();
            GraphicsDevice.SetVertexBuffer(vertexes.vertexBuffer);
            DoTheCubeMap();
            this.EnvironmentMap = renderTarget;
            glass_effect.Parameters["ReflectionCubeMap"].SetValue(EnvironmentMap);
            GraphicsDevice.SetRenderTarget(null);
            DrawScene();
            DrawGlassPalm(camera);

            base.Draw(gameTime);
        }

        public void DrawGlassPalm(Camera camera)
        {
            glassPalm.DrawModelWithGlassEffect(palmModel, camera, glass_effect);
        }

        public void DrawWithDefaultShader(int cubeOffset)
        {
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = true;
            basicEffect.Projection = camera.ProjectionMatrix;
            basicEffect.View = camera.ViewMatrix;
            basicEffect.World = camera.WorldMatrix;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexes.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexes.vertexBuffer.VertexCount - 6 - cubeOffset);
                }
            }

            foreach (var p in palms)
            {
                p.Draw(palmModel, basicEffect, palmTexture);
            }
            foreach (var r in rocks)
            {
                r.Draw(rockModel, basicEffect, rockTexture);
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

        public void DrawOcean(Effect alfa_effect, Camera camera, Texture2D ocean2texture, Texture2D textochange)
        {
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

        public void DrawScene()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Texture2D mySolidColorTexture = new SolidColorTexture(GraphicsDevice, Color.Yellow);
            int cubeOffset = 6 * 6;

            if (useDefaultBasicEffect)
            {
                DrawWithDefaultShader(cubeOffset);
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
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexes.vertexBuffer.VertexCount - dnoVerticeCount - cubeOffset);
                    }

                }

                custom_effect.Parameters["ModelTexture"].SetValue(dnoTexture);
                custom_effect.Parameters["TextureAlpha"].SetValue(0.6f);
                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, vertexes.vertexBuffer.VertexCount - dnoVerticeCount - cubeOffset, vertexes.vertexBuffer.VertexCount - cubeOffset);
                    }
                }

                custom_effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                custom_effect.Parameters["AmbientIntensity"].SetValue(1.0f);
                custom_effect.Parameters["LightDirection"].SetValue(new Vector3(0.0f, 1.0f, 0.0f));
                custom_effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector4());
                custom_effect.Parameters["DiffuseIntensity"].SetValue(0.0f);
                custom_effect.Parameters["SpecularColor"].SetValue(Color.White.ToVector4());
                custom_effect.Parameters["ModelTexture"].SetValue(skyFront);
                custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 0)), vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 1)));
                    }
                }

                custom_effect.Parameters["ModelTexture"].SetValue(skyLeft);
                custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 1)), vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 2)));
                    }
                }

                custom_effect.Parameters["ModelTexture"].SetValue(skyRight);
                custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 2)), vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 3)));
                    }
                }

                custom_effect.Parameters["ModelTexture"].SetValue(skyUp);
                custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 3)), vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 4)));
                    }
                }

                custom_effect.Parameters["ModelTexture"].SetValue(skyDown);
                custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 4)), vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 5)));
                    }
                }

                custom_effect.Parameters["ModelTexture"].SetValue(skyBack);
                custom_effect.Parameters["TextureAlpha"].SetValue(1.0f);
                foreach (EffectPass pass in custom_effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    if (vertexes.vertexBuffer != null)
                    {
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 5)), vertexes.vertexBuffer.VertexCount - ((cubeOffset / 6) * (6 - 6)));
                    }
                }

                foreach (var p in palms)
                {
                    p.DrawModelWithEffect(palmModel, camera, custom_effect, palmTexture);
                }

                DrawRocks();
                DrawOcean(alfa_effect, camera, ocean2Texture, texToChange);
            }
        }
    }
}
