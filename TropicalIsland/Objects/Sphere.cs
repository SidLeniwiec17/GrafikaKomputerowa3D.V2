using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalIsland.Objects
{
    public class Sphere
    {
        public Matrix RotationMatrix;
        public Matrix TranslationMatrix;
        public Matrix ScaleMatrix;
        public float Radius;
        public Vector3 Center;
        public int Tessellation;
        public Color[] Colors;
        public VertexPositionNormalTexture[] Vertexes;

        public Sphere(float radius, Vector3 move, int tessellation, float rX = 0.0f, float rY = 0.0f, float rZ = 0.0f, float scale = 1.0f)
        {
            Radius = radius;
            Center = new Vector3(0.0f, 0.0f, 0.0f);
            Tessellation = tessellation;
            Colors = new Color[] { Color.Yellow, Color.LightYellow, Color.LightGoldenrodYellow };
            RotationMatrix = Matrix.CreateRotationX(rX) * Matrix.CreateRotationY(rY) * Matrix.CreateRotationZ(rZ);
            TranslationMatrix = Matrix.CreateTranslation(move);
            ScaleMatrix = Matrix.CreateScale(scale);
        }

        public VertexPositionNormalTexture[] Init(bool isCutted = false)
        {
            int colorCounter = 0;
            List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
            List<ushort> indices = new List<ushort>();

            if (Tessellation < 3)
                return vertices.ToArray();

            int verticalSegments = Tessellation;
            int horizontalSegments = Tessellation * 2;


            // Start with a single vertex at the bottom of the sphere.
            vertices.Add(new VertexPositionNormalTexture(Vector3.Down * Radius, Vector3.Down * Radius, new Vector2(0.0f, 0.0f)));
            colorCounter++;

            int tempCounter = 0;

            // Create rings of vertices at progressively higher latitudes.
            for (int i = 0; i < verticalSegments - 1; i++)
            {
                float latitude = ((i + 1) * MathHelper.Pi / verticalSegments) - MathHelper.PiOver2;

                float dy = (float)Math.Sin(latitude);
                float dxz = (float)Math.Cos(latitude);

                // Create a single ring of vertices at this latitude.                

                for (int j = 0; j < horizontalSegments; j++)
                {
                    float longitude = j * MathHelper.TwoPi / horizontalSegments;

                    float dx = (float)Math.Cos(longitude) * dxz;
                    float dz = (float)Math.Sin(longitude) * dxz;

                    Vector3 normal = new Vector3(dx, dy, dz);

                    vertices.Add(new VertexPositionNormalTexture(normal * Radius, normal * Radius, new Vector2(0.0f, 0.0f)));
                    colorCounter++;
                }
            }

            // Finish with a single vertex at the top of the sphere.

            vertices.Add(new VertexPositionNormalTexture(Vector3.Up * Radius, Vector3.Up * Radius, new Vector2(0.0f, 0.0f)));


            // Create a fan connecting the bottom vertex to the bottom latitude ring.

            for (int i = 0; i < horizontalSegments; i++)
            {
                indices.Add((ushort)0);
                indices.Add((ushort)(1 + (i + 1) % horizontalSegments));
                indices.Add((ushort)(1 + i));
            }

            tempCounter = indices.Count;

            // Fill the sphere body with triangles joining each pair of latitude rings.
            for (int i = 0; i < verticalSegments - 2; i++)
            {
                for (int j = 0; j < horizontalSegments; j++)
                {
                    int nextI = i + 1;
                    int nextJ = (j + 1) % horizontalSegments;

                    indices.Add((ushort)(1 + i * horizontalSegments + j));
                    indices.Add((ushort)(1 + i * horizontalSegments + nextJ));
                    indices.Add((ushort)(1 + nextI * horizontalSegments + j));

                    indices.Add((ushort)(1 + i * horizontalSegments + nextJ));
                    indices.Add((ushort)(1 + nextI * horizontalSegments + nextJ));
                    indices.Add((ushort)(1 + nextI * horizontalSegments + j));
                }
                if (i == 1)
                {
                    tempCounter = indices.Count;
                }
            }

            // Create a fan connecting the top vertex to the top latitude ring.
            for (int i = 0; i < horizontalSegments; i++)
            {
                indices.Add((ushort)(vertices.Count - 1));
                indices.Add((ushort)(vertices.Count - 2 - (i + 1) % horizontalSegments));
                indices.Add((ushort)(vertices.Count - 2 - i));
            }

            List<VertexPositionNormalTexture> triangleVertices = new List<VertexPositionNormalTexture>();

            int edge = indices.Count;
            if (isCutted)
            {
                edge = tempCounter;
            }
            for (int i = 0; i < edge; i++)
            {
                triangleVertices.Add(vertices[indices[i]]);
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
