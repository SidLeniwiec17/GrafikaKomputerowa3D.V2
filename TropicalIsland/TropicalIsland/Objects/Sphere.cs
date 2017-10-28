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
        public float Radius;
        public Vector3 Center;
        public int Tessellation;
        public Color[] Colors;

        public Sphere(float radius, Vector3 center, int tessellation)
        {
            Radius = radius;
            Center = center;
            Tessellation = tessellation;
            Colors = new Color[] { Color.Red, Color.Blue, Color.Green };
        }

        public VertexPositionColor[] Init(bool isCutted = false)
        {
            int colorCounter = 0;
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            List<ushort> indices = new List<ushort>();

            if (Tessellation < 3)
                return vertices.ToArray();

            int verticalSegments = Tessellation;
            int horizontalSegments = Tessellation * 2;


            // Start with a single vertex at the bottom of the sphere.
            vertices.Add(new VertexPositionColor(Vector3.Down * Radius, Colors[colorCounter % Colors.Length]));
            colorCounter++;

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

                    vertices.Add(new VertexPositionColor(normal * Radius, Colors[colorCounter % Colors.Length]));
                    colorCounter++;
                }
            }

            // Finish with a single vertex at the top of the sphere.
            vertices.Add(new VertexPositionColor(Vector3.Up * Radius, Color.Red));

            // Create a fan connecting the bottom vertex to the bottom latitude ring.
            for (int i = 0; i < horizontalSegments; i++)
            {
                indices.Add((ushort)0);
                indices.Add((ushort)(1 + (i + 1) % horizontalSegments));
                indices.Add((ushort)(1 + i));
            }

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
            }

            // Create a fan connecting the top vertex to the top latitude ring.
            for (int i = 0; i < horizontalSegments; i++)
            {
                indices.Add((ushort)(vertices.Count - 1));
                indices.Add((ushort)(vertices.Count - 2 - (i + 1) % horizontalSegments));
                indices.Add((ushort)(vertices.Count - 2 - i));
            }

            List<VertexPositionColor> triangleVertices = new List<VertexPositionColor>();
            foreach (var i in indices)
            {
                triangleVertices.Add(vertices[i]);
            }

            return triangleVertices.ToArray();
        }
    }
}
