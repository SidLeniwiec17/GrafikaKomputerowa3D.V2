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
    }
}
