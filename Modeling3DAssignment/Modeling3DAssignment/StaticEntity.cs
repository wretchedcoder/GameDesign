using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Modeling3DAssignment
{
    public class StaticEntity
    {
        public ModelingGame Game;
        public Model Model;
        public Matrix ModelMatrix;

        public void Draw(GameTime GameTime)
        {
            foreach (ModelMesh Mesh in Model.Meshes)
            {
                foreach (BasicEffect Effect in Mesh.Effects)
                {
                    Effect.World = ModelMatrix;
                    Effect.View = Game.ViewMatrix;
                    Effect.Projection = Game.PerspectiveMatrix;
                    Effect.PreferPerPixelLighting = true;
                }// BasicEffect
                Mesh.Draw();
            }// ModelMesh
        }
    }

    
}
