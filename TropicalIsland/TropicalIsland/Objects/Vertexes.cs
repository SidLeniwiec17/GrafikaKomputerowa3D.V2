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
        public VertexPositionNormalTexture[] triangleVertices;
        public VertexBuffer vertexBuffer;
        public GraphicsDevice graphicsDevice;

        public void Init(GraphicsDevice _graphicsDevice)
        {
            graphicsDevice = _graphicsDevice;            
        }

        public void addObject(VertexPositionNormalTexture[] newObject)
        {
            VertexPositionNormalTexture[] newtriangleVertices = new VertexPositionNormalTexture[triangleVertices.Length + newObject.Length];
            newtriangleVertices = triangleVertices;
            int counter = 0;
            foreach (var v in newObject)
            {
                newtriangleVertices[triangleVertices.Length + counter] = newObject[counter];
                counter++;
            }
            VertexBuffer newvertexBuffer = new VertexBuffer(graphicsDevice, typeof(
                           VertexPositionNormalTexture), newtriangleVertices.Length, BufferUsage.
                           WriteOnly);
            newvertexBuffer.SetData<VertexPositionNormalTexture>(newtriangleVertices);
            triangleVertices = newtriangleVertices;
            vertexBuffer = newvertexBuffer;
        }

        public void addObject(VertexPositionNormalTexture[] newObject, bool isFirst)
        {
            if (isFirst)
            {
                VertexBuffer newvertexBuffer = new VertexBuffer(graphicsDevice, typeof(
                               VertexPositionNormalTexture), newObject.Length, BufferUsage.
                               WriteOnly);
                newvertexBuffer.SetData<VertexPositionNormalTexture>(newObject);
                triangleVertices = new VertexPositionNormalTexture[newObject.Length];
                vertexBuffer = newvertexBuffer;
            }
            else
            {
                this.addObject(newObject);
            }
        }
    }
}
