using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalIsland.Objects
{
    public class Camera
    {
        public Vector3 CamTarget;
        public Vector3 CamPosition;
        public Matrix ProjectionMatrix;
        public Matrix ViewMatrix;
        public Matrix WorldMatrix;

        public float leftRightRotation = 0.0f;
        public float upDownRotation = 0.0f;
        public const float rotationSpeed = 0.5f;
        public const float moveSpeed = 70.0f;

        public void Init(GraphicsDevice graphicsDevice)
        {
            CamTarget = new Vector3(0.0f, 0.0f, -1.0f);
            CamPosition = new Vector3(0.0f, -25.0f, 100.0f);
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f),
                               graphicsDevice.Viewport.AspectRatio, 1f, 1000f);
            ViewMatrix = Matrix.CreateLookAt(CamPosition, CamTarget,
                         Vector3.Up);
            WorldMatrix = Matrix.CreateWorld(CamTarget, Vector3.Forward, Vector3.Up);
        }

        public void Update(GameTime gameTime)
        {
            Vector3 moveVector = new Vector3(0, 0, 0);

            ////---------------------LEWO PRAWO GORA DOL
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                moveVector += new Vector3(-1, 0, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                moveVector += new Vector3(1, 0, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                moveVector += new Vector3(0, 0, -1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                moveVector += new Vector3(0, 0, 1);
            }
            ////----------------------PRZOD TYL
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
            {
                moveVector += new Vector3(0, 1, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
            {
                moveVector += new Vector3(0, -1, 0);
            }

            ////--------------------OBROTY LEWO PRAWO GORA DOL
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                upDownRotation += 0.02f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                upDownRotation -= 0.02f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                leftRightRotation += 0.02f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                leftRightRotation -= 0.02f;
            }

            UpdatePosition(moveVector, gameTime);
        }

        private void UpdatePosition(Vector3 vectorToAdd, GameTime gameTime)
        {
            Matrix cameraRotation = Matrix.CreateRotationX(upDownRotation) * Matrix.CreateRotationY(leftRightRotation);
            Vector3 cameraRotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            CamPosition += (moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) * cameraRotatedVector;
            UpdateViewMatrix();
        }

        private void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(upDownRotation) * Matrix.CreateRotationY(leftRightRotation);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);

            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = CamPosition + cameraRotatedTarget;

            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);

            ViewMatrix = Matrix.CreateLookAt(CamPosition, cameraFinalTarget, cameraRotatedUpVector);
        }       
    }
}
