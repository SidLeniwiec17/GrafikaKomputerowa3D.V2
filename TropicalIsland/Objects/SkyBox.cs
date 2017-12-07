using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalIsland.Objects
{
    public class SkyBox
    {
        public VertexPositionNormalTexture[] vertexes;
        public Vector3 center;

        public SkyBox()
        {
            center = new Vector3(0, 0, 0);
        }
        public VertexPositionNormalTexture[] initTestCubeSkybox(Vector3 _center)
        {
            center = _center;
            List<VertexPositionNormalTexture> temptriangleVertices = new List<VertexPositionNormalTexture>();
            //front
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, -1000), new Vector3(0, 0, 1), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, -1000), new Vector3(0, 0, 1), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, -1000), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, -1000), new Vector3(0, 0, 1), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, -1000), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, -1000), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));

            //left
            //TODO Why is right ??
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, 1000), new Vector3(1, 0, 0), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, -1000), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, 1000), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, -1000), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, -1000), new Vector3(1, 0, 0), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, 1000), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));

            //right
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, -1000), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, 1000), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, -1000), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, 1000), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, 1000), new Vector3(-1, 0, 0), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, -1000), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));

            //top
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, 1000), new Vector3(0, -1, 0), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, 1000), new Vector3(0, -1, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, -1000), new Vector3(0, -1, 0), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, 1000), new Vector3(0, -1, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, -1000), new Vector3(0, -1, 0), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, -1000), new Vector3(0, -1, 0), new Vector2(0.0f, 1.0f)));

            //bottom
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, -1000), new Vector3(0, 1, 0), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, -1000), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, 1000), new Vector3(0, 1, 0), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, -1000), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, 1000), new Vector3(0, 1, 0), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, 1000), new Vector3(0, 1, 0), new Vector2(0.0f, 1.0f)));

            //back
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, 900, 1000), new Vector3(0, 0, -1), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, 1000), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, 1000), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, 900, 1000), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(-1000, -1100, 1000), new Vector3(0, 0, -1), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(center + new Vector3(1000, -1100, 1000), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f)));

            return temptriangleVertices.ToArray();
        }
    }
}
