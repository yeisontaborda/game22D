using juego2D.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
namespace juego2D
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D cielo;
        Texture2D rectangulo;
        Rectangle rectanguloRectangle;
        int Width;
        int Height;
        List<pelotascaidas> pelotascaidass;
        TimeSpan tiempo;
        float pelotascaidasTime;
        int puntos;
        int vidas;
        SpriteFont font;
        Song song;
        SoundEffect soundEffect;
        TimeSpan timeSpan;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Height = 1920;
            Width = 1080;
            _graphics.PreferredBackBufferHeight = Height;
            _graphics.PreferredBackBufferWidth = Width;
            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            pelotascaidass=new List<pelotascaidas>();
            pelotascaidasTime = 3f;
            tiempo = TimeSpan.Zero;
            puntos = 0;
            vidas = 5;
            timeSpan = TimeSpan.Zero;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            NewMethod();
            cielo = Content.Load<Texture2D>("cielo");
            rectanguloRectangle = new Rectangle((Width - 200) / 2, Height - 80, 200, 100);
            font = Content.Load<SpriteFont>("fonts/font");
            song = Content.Load<Song>("sonidos/sonidoentrada");
            soundEffect = Content.Load<SoundEffect>("sonidos/sonido");
            sonido.sonidojuego(song);

            void NewMethod()
            {
                _spriteBatch = new SpriteBatch(GraphicsDevice);
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            base.Update(gameTime);
            Updatepelotascaidass(gameTime);
           
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left)) rectanguloRectangle.X -= 5;
            if (keyboardState.IsKeyDown(Keys.Left)) rectanguloRectangle.X +=5;
            rectanguloRectangle.X = (int)MathHelper.Clamp(rectanguloRectangle.X, 0, Width - 200);
            TimeSpan time = TimeSpan.FromSeconds(5);
            if (gameTime.TotalGameTime - timeSpan > time)
            {
                timeSpan = gameTime.TotalGameTime;
                SoundEffect.Initialize();
            }
      
            

        }
        private void Updatepelotascaidass(GameTime gameTime)
        {
            TimeSpan Tiempo = TimeSpan.FromSeconds(pelotascaidasTime);
            if(gameTime.TotalGameTime-tiempo>Tiempo)
            {
                tiempo = gameTime.TotalGameTime;
                pelotascaidasTime *= 0.95f;
                if (pelotascaidasTime < 0.5f) pelotascaidasTime = 0.5f;
                Addpelotascaidas(gameTime);
            }
            for(int i=0; i < pelotascaidass.Count;i++)
            {
                if(rectanguloRectangle.Intersects(pelotascaidass[i].Rectangle))
                {
                    puntos++;
                    pelotascaidass.RemoveAt(i);
                }
            }
            for (int i = 0; i < pelotascaidass.Count; i++)
            {
                if (pelotascaidass[i].Rectangle.Y>Height)
                {
                    vidas--;

                    pelotascaidass.RemoveAt(i);
                }
            }

            foreach (pelotascaidas pelotascaidas in pelotascaidass)
            {
                pelotascaidas.Update(gameTime);
            }

        }
        private void Addpelotascaidas(GameTime gameTime)
        { Random rd = new Random();
            Texture2D pelotascaidasTexture = Content.Load<Texture2D>("images/Bola");
            Rectangle pelotascaidasRectangle = new Rectangle(rd.Next(0, Width - 80), 0, 80, 100);
            pelotascaidas pelotascaidas = new pelotascaidas();
            pelotascaidas.Initilize(pelotascaidasTexture, pelotascaidasRectangle);
            pelotascaidass.Add(pelotascaidas);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(cielo, new Vector2(0, 0), Color.White);
            Drawpelotascaidass(_spriteBatch);
           _spriteBatch.Draw(rectangulo, rectanguloRectangle, Color.White);
            _spriteBatch.DrawString(font, "puntos:" + puntos.ToString(), new Vector2(10, 10), Color.Yellow);
            _spriteBatch.DrawString(font, "vidas:"+vidas.ToString(), new Vector2(10,30),Color.Yellow);

            _spriteBatch.End();




            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        private void Drawpelotascaidass(SpriteBatch spriteBatch)
        {
            foreach(pelotascaidas pelotascaidas in pelotascaidass)
            {
                pelotascaidas.Draw(spriteBatch);
            }
        }
    }
}
