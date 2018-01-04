using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalIsland.Objects
{
    public class OceanSurface
    {
        public Matrix RotationMatrix;
        public Matrix TranslationMatrix;
        public Matrix ScaleMatrix;
        public Vector3 Center;
        public VertexPositionNormalTexture[] Vertexes;

        public OceanSurface(Vector3 move, float rX = 0.0f, float rY = 0.0f, float rZ = 0.0f, float scale = 1.0f)
        {
            Center = new Vector3(0.0f, 0.0f, 0.0f);
            RotationMatrix = Matrix.CreateRotationX(rX) * Matrix.CreateRotationY(rY) * Matrix.CreateRotationZ(rZ);
            TranslationMatrix = Matrix.CreateTranslation(move);
            ScaleMatrix = Matrix.CreateScale(scale);
        }
        public VertexPositionNormalTexture[] Init(bool alfa = false)
        {
            List<VertexPositionNormalTexture> triangleVertices = new List<VertexPositionNormalTexture>();
           
            if (alfa)
            {
                int size = 2800;

                int split = 70;
                int xpos = size / 2;

                for (int iX = 0; iX < split; iX++)
                {
                    int zpos = size / 2;
                    for (int iZ = 0; iZ < split; iZ++)
                    {
                        triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-xpos + ((size / split) * (iX + 1)) - (size / split), 0, -zpos + ((size / split) * (iZ + 1)) - (size / split)), Vector3.Up, new Vector2(0.0f, 0.0f)));
                        triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-xpos + ((size / split) * (iX + 1)) - (size / split), 0, -zpos + ((size / split) * (iZ + 1))), Vector3.Up, new Vector2(1.0f, 0.0f)));
                        triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-xpos + ((size / split) * (iX + 1)), 0, -zpos + ((size / split) * (iZ + 1)) - (size / split)), Vector3.Up, new Vector2(0.0f, 1.0f)));
                        triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-xpos + ((size / split) * (iX + 1)) - (size / split), 0, -zpos + ((size / split) * (iZ + 1))), Vector3.Up, new Vector2(1.0f, 0.0f)));
                        triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-xpos + ((size / split) * (iX + 1)), 0, -zpos + ((size / split) * (iZ + 1))), Vector3.Up, new Vector2(1.0f, 1.0f)));
                        triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-xpos + ((size / split) * (iX + 1)), 0, -zpos + ((size / split) * (iZ + 1)) - (size / split)), Vector3.Up, new Vector2(0.0f, 1.0f)));
                    }
                }
            }
            else
            {
                triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-1000, 0, -1000), Vector3.Up, new Vector2(0.0f, 0.0f)));
                triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-1000, 0, 1000), Vector3.Up, new Vector2(1.0f, 0.0f)));
                triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(1000, 0, -1000), Vector3.Up, new Vector2(0.0f, 1.0f)));
                triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-1000, 0, 1000), Vector3.Up, new Vector2(1.0f, 0.0f)));
                triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(1000, 0, 1000), Vector3.Up, new Vector2(1.0f, 1.0f)));
                triangleVertices.Add(new VertexPositionNormalTexture(new Vector3(1000, 0, -1000), Vector3.Up, new Vector2(0.0f, 1.0f)));
            }

            Matrix finalMatrix = TranslationMatrix * RotationMatrix * ScaleMatrix;
            VertexPositionNormalTexture[] movedVertices = triangleVertices.ToArray();
            for (int i = 0; i < movedVertices.Length; i++)
            {
                movedVertices[i].Position = Vector3.Transform(movedVertices[i].Position, finalMatrix);
            }

            Vertexes = movedVertices;
            return Vertexes;
        }
    }
}
