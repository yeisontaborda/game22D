using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace juego2D
{
    class pelotascaidas
    {
        private Texture2D texture;
        private Rectangle rectangle;

        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }

        public void Initilize(Texture2D texture,Rectangle rectangle)
        {
            this.texture = texture;
            this.rectangle = rectangle;
        }
        public void Update(GameTime gameTime)
        {
            rectangle.Y++;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
