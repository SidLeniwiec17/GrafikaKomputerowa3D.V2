using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TropicalIsland.Objects;

namespace TropicalIsland
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Camera
        Camera camera;

        //BasicEffect for rendering
        BasicEffect basicEffect;

        //Geometric info
        Vertexes vertexes;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camera = new Camera();
            camera.Init(GraphicsDevice, new Vector3(0.0f, 0.0f, -100.0f));
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;

            vertexes = new Vertexes();
            vertexes.Init(GraphicsDevice);

            /*
            VertexPositionColor[] testCube = HelperClass.initTestCube();
            vertexes.addObject(testCube, true);*/

            //Sphere
            Sphere sphere = new Sphere(100.0f, new Vector3(0.0f, 0.0f, 0.0f), 16);
            VertexPositionColor[] testSphere = sphere.Init();

            vertexes.addObject(testSphere, true);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            basicEffect.Projection = camera.ProjectionMatrix;
            basicEffect.View = camera.ViewMatrix;
            basicEffect.World = camera.WorldMatrix;

            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetVertexBuffer(vertexes.vertexBuffer);

            //Turn off culling so we see both sides of our rendered triangle
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                if (vertexes.vertexBuffer != null)
                {
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexes.vertexBuffer.VertexCount);
                }
            }

            base.Draw(gameTime);
        }
    }
}
