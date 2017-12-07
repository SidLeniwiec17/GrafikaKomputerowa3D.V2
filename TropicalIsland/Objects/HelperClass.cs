using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalIsland.Objects
{
    public class HelperClass
    {
        public static VertexPositionColor[] initTestCube()
        {
            List<VertexPositionColor> temptriangleVertices = new List<VertexPositionColor>();
            //front
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 0), Color.Red));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 0), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 0), Color.Blue));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 0), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 0), Color.Yellow));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 0), Color.Blue));

            //left
            //TODO Why is right ??
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 20), Color.Red));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 0), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 20), Color.Blue));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 0), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 0), Color.Yellow));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 20), Color.Blue));

            //right
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 0), Color.Red));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 20), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 0), Color.Blue));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 20), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 20), Color.Yellow));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 0), Color.Blue));

            //top
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 20), Color.Red));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 20), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 0), Color.Blue));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 20), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 0), Color.Yellow));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 0), Color.Blue));

            //bottom
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 0), Color.Red));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 0), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 20), Color.Blue));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 0), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 20), Color.Yellow));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 20), Color.Blue));

            //back
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, 10, 20), Color.Red));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 20), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 20), Color.Blue));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, 10, 20), Color.Green));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(-10, -10, 20), Color.Yellow));
            temptriangleVertices.Add(new VertexPositionColor(new Vector3(10, -10, 20), Color.Blue));

            return temptriangleVertices.ToArray();
        }

        public static VertexPositionNormalTexture[] initTestCubeSkybox()
        {
            List<VertexPositionNormalTexture> temptriangleVertices = new List<VertexPositionNormalTexture>();
            //front
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 0), new Vector3(0, 0, 1), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 0), new Vector3(0, 0, 1), new Vector2(0.0f, 1.0f)));

            //left
            //TODO Why is right ??
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 20), new Vector3(1, 0, 0), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 0), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 20), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 0), new Vector3(1, 0, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 0), new Vector3(1, 0, 0), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 20), new Vector3(1, 0, 0), new Vector2(0.0f, 1.0f)));

            //right
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 0), new Vector3(-1, 0, 0), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 20), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 0), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 20), new Vector3(-1, 0, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 20), new Vector3(-1, 0, 0), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 0), new Vector3(-1, 0, 0), new Vector2(0.0f, 1.0f)));

            //top
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 20), new Vector3(0, -1, 0), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 20), new Vector3(0, -1, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 0), new Vector3(0, -1, 0), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 20), new Vector3(0, -1, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 0), new Vector3(0, -1, 0), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 0), new Vector3(0, -1, 0), new Vector2(0.0f, 1.0f)));

            //bottom
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 0), new Vector3(0, 1, 0), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 0), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 20), new Vector3(0, 1, 0), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 0), new Vector3(0, 1, 0), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 20), new Vector3(0, 1, 0), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 20), new Vector3(0, 1, 0), new Vector2(0.0f, 1.0f)));

            //back
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, 10, 20), new Vector3(0, 0, -1), new Vector2(0.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 20), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 20), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, 10, 20), new Vector3(0, 0, -1), new Vector2(1.0f, 0.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(-10, -10, 20), new Vector3(0, 0, -1), new Vector2(1.0f, 1.0f)));
            temptriangleVertices.Add(new VertexPositionNormalTexture(new Vector3(10, -10, 20), new Vector3(0, 0, -1), new Vector2(0.0f, 1.0f)));

            return temptriangleVertices.ToArray();
        }
    }
}
