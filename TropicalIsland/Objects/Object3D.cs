using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalIsland.Objects
{
    public class Object3D
    {
        public Matrix RotationMatrix;
        public Matrix TranslationMatrix;
        public Matrix ScaleMatrix;
        public Vector3 Position;

        public Object3D(Vector3 move, float rX = 0.0f, float rY = 0.0f, float rZ = 0.0f, float scale = 1.0f)
        {
            Position = new Vector3(0.0f, 0.0f, 0.0f);
            RotationMatrix = Matrix.CreateRotationX(rX) * Matrix.CreateRotationY(rY) * Matrix.CreateRotationZ(rZ);
            TranslationMatrix = Matrix.CreateTranslation(move);
            ScaleMatrix = Matrix.CreateScale(scale);
        }

        public void Draw(Model model, BasicEffect basicEffect, Texture2D palmTexture)
        {
            Matrix finalMatrix = TranslationMatrix * RotationMatrix * ScaleMatrix;
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.Multiply(basicEffect.World, finalMatrix);
                    effect.View = basicEffect.View;
                    effect.Projection = basicEffect.Projection;
                    effect.LightingEnabled = false;
                    effect.TextureEnabled = true;
                    effect.Texture = palmTexture;
                }
                mesh.Draw();
            }
        }

        public void DrawModelWithEffect(Model model, Camera camera, Effect effect, Texture2D modelTexture)
        {
            Matrix finalMatrix = TranslationMatrix * RotationMatrix * ScaleMatrix;
            foreach (ModelMesh mesh in model.Meshes)
            {
                Matrix worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * finalMatrix * camera.WorldMatrix));
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                    effect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
                    effect.Parameters["World"].SetValue(finalMatrix * camera.WorldMatrix * mesh.ParentBone.Transform);
                    effect.Parameters["View"].SetValue(camera.ViewMatrix);
                    effect.Parameters["Projection"].SetValue(camera.ProjectionMatrix);
                    effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
                    effect.Parameters["AmbientIntensity"].SetValue(0.25f);
                    effect.Parameters["ModelTexture"].SetValue(modelTexture);
                    effect.Parameters["CameraPosition"].SetValue(camera.CamPosition);

                }
                mesh.Draw();
            }
        }
    }
}
