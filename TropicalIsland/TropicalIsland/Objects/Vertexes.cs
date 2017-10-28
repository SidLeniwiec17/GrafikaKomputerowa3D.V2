using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalIsland.Objects
{
    public class Vertexes
    {
        public VertexPositionColor[] triangleVertices;
        public VertexBuffer vertexBuffer;
        public GraphicsDevice graphicsDevice;

        public void Init(GraphicsDevice _graphicsDevice)
        {
            graphicsDevice = _graphicsDevice;            
        }

        public void addObject(VertexPositionColor[] newObject)
        {            
            VertexPositionColor[] newtriangleVertices = new VertexPositionColor[triangleVertices.Length + newObject.Length];
            newtriangleVertices = triangleVertices;
            int counter = 0;
            foreach (var v in newObject)
            {
                newtriangleVertices[triangleVertices.Length + counter] = newObject[counter];
                counter++;
            }
            VertexBuffer newvertexBuffer = new VertexBuffer(graphicsDevice, typeof(
                           VertexPositionColor), newtriangleVertices.Length, BufferUsage.
                           WriteOnly);
            newvertexBuffer.SetData<VertexPositionColor>(newtriangleVertices);
            triangleVertices = newtriangleVertices;
            vertexBuffer = newvertexBuffer;
        }

        public void addObject(VertexPositionColor[] newObject, bool isFirst)
        {
            if (isFirst)
            {
                VertexBuffer newvertexBuffer = new VertexBuffer(graphicsDevice, typeof(
                               VertexPositionColor), newObject.Length, BufferUsage.
                               WriteOnly);
                newvertexBuffer.SetData<VertexPositionColor>(newObject);
                triangleVertices = new VertexPositionColor[newObject.Length];
                vertexBuffer = newvertexBuffer;
            }
            else
            {
                this.addObject(newObject);
            }
        }
    }
}
