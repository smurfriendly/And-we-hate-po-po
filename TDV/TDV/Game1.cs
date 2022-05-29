using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TDV
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Button bPlay, btnResume, btnQuit;
        EnemyGenerator enemyg;
        Enemy enemy;

        enum GameState 
        { 
            MainMenu,
            Playing,
            GameOver,
        }
        GameState CurrentGameState = GameState.MainMenu;

        bool paused = false;
        int finalscore = 0;
        private Texture2D tground;
        private Rectangle pground;

        private Texture2D tgameOver;
        private Rectangle pgameOver;

        List<Song> music = new List<Song>();
        SpriteFont score;

        public static int screenWidth;
        public static int screenHeight;
        float density;
        public Vector2 pos;

        Rectangle playerbounds;
        Rectangle enemybounds;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            screenWidth = _graphics.PreferredBackBufferWidth;
            screenHeight = _graphics.PreferredBackBufferHeight;
        }

        protected override void Initialize()
        {
            pos = new Vector2(_graphics.PreferredBackBufferWidth / 2, (_graphics.PreferredBackBufferHeight / 2) + 160f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            score = Content.Load<SpriteFont>("score");

            music.Add(Content.Load<Song>("1"));
            music.Add(Content.Load<Song>("2"));
            music.Add(Content.Load<Song>("3"));
            music.Add(Content.Load<Song>("4"));
            Random random = new Random();
            random.Next(0, 3);
            if (random.Next(0, 3) == 0)
            {
                MediaPlayer.Play(music[0]);
                density = 0.05f;
            }
            else if (random.Next(0, 3) == 1)
            {
                MediaPlayer.Play(music[1]);
                density = 0.075f;
            }
            else if (random.Next(0, 3) == 2)
            {
                MediaPlayer.Play(music[2]);
                density = 0.1f;
            }
            else
            {
                MediaPlayer.Play(music[3]);
                density = 0.15f;
            }

            enemyg = new EnemyGenerator(Content.Load<Texture2D>("Police"), new Rectangle(screenWidth / 6, (-8) * (screenHeight / 8), (screenWidth - 2 * (screenWidth / 6)), screenHeight / 3), density);

            btnResume = new Button(Content.Load<Texture2D>("resumeButton"), _graphics.GraphicsDevice);
            btnResume.setPosition(new Vector2(320, 240));
            
            btnQuit = new Button(Content.Load<Texture2D>("quitButton"), _graphics.GraphicsDevice);
            btnQuit.setPosition(new Vector2(320, 300));

            bPlay = new Button(Content.Load<Texture2D>("startButton"), _graphics.GraphicsDevice);
            bPlay.setPosition(new Vector2(320, 250));

            tground = Content.Load<Texture2D>("Street");
            pground = new Rectangle(0, 0, screenWidth, screenHeight);

            tgameOver = Content.Load<Texture2D>("GameOver");
            pgameOver = new Rectangle(0, 0, screenWidth, screenHeight);

            player = new Player(this, Content.Load<Texture2D>("Audi"), pos);
            this.Components.Add(player);
         
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            if (!paused)
            {
                switch (CurrentGameState)
                {
                    case GameState.MainMenu:
                        if (bPlay.isClicked == true)
                        {
                            CurrentGameState = GameState.Playing;
                        }
                        if (btnQuit.isClicked == true)
                        {
                            Exit();
                        }
                        bPlay.Update(mouse);
                        btnQuit.Update(mouse);
                        break;
                    case GameState.Playing:
                        enemyg.Update(gameTime, _graphics.GraphicsDevice);

                        playerbounds = new Rectangle((int)(player.posicao.X -40), (int)player.posicao.Y -50, (int)(player.textura.Width * 0.3), (int)(player.textura.Height * 0.3));
                        foreach (var enemy in enemyg.enemies)
                        {
                            enemybounds = new Rectangle((int)enemy.position.X - 80, (int)enemy.position.Y - 80, (int)(enemyg.texture.Width * 0.2), (int)(enemyg.texture.Height * 0.2));

                            if (playerbounds.Intersects(enemybounds) == true)
                            {
                                finalscore = (int)player.score;
                                CurrentGameState = GameState.GameOver;
                            }
                        }
                        
                        if (Keyboard.GetState().IsKeyDown(Keys.P))
                        {
                            paused = true;
                            btnResume.isClicked = false;
                        }
                        break;
                    case GameState.GameOver:
                        if (btnQuit.isClicked == true)
                        {
                            Exit();
                        }
                        btnQuit.Update(mouse);
                        break;
                }
            }
            else if (paused)
            {
                if (btnResume.isClicked)
                {
                    paused = false;
                }
                if (btnQuit.isClicked)
                {
                    Exit();
                }

                btnResume.Update(mouse);
                btnQuit.Update(mouse);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    
                    _spriteBatch.Draw(Content.Load<Texture2D>("menuB"), new Rectangle(0,0, screenWidth,screenHeight), Color.White);
                    bPlay.Draw(_spriteBatch);
                    btnQuit.Draw(_spriteBatch);
                    break;
                
                case GameState.Playing:
                    
                    _spriteBatch.Draw(tground, pground, Color.White);
                    _spriteBatch.Draw(player.textura, player.posicao, null, Color.White, 0, player.Origin, 0.5f, SpriteEffects.None, 0);
                    _spriteBatch.Draw(tground, new Rectangle(0, 0, screenWidth / 8, screenHeight / 8), Color.Black);
                    _spriteBatch.DrawString(score,Convert.ToString(player.rounded),new Vector2(0,0),Color.White,0,new Vector2(0,0),2, SpriteEffects.None, 0);
                    //_spriteBatch.Draw(player.textura, playerbounds, Color.Black);

                    //foreach (var enemy in enemyg.enemies)
                    //{
                    //    _spriteBatch.Draw(enemyg.texture, enemybounds, Color.Black);
                    //}

                    enemyg.Draw(_spriteBatch);
                    if (paused)
                    {
                        _spriteBatch.Draw(Content.Load<Texture2D>("menuB"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                        btnResume.Draw(_spriteBatch);
                        btnQuit.Draw(_spriteBatch);
                    }
                    break;

                case GameState.GameOver:
                    _spriteBatch.Draw(Content.Load<Texture2D>("GameOver"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    _spriteBatch.DrawString(score, Convert.ToString(finalscore), new Vector2(340, 160), Color.White, 0, new Vector2(0, 0), 5, SpriteEffects.None, 0);
                    btnQuit.Draw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
