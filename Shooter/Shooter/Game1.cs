using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Emit;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Xml.Serialization;

namespace Shooter
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        Texture2D player;
        Player PlayerShip;
        Texture2D meteore2D;
        Texture2D life2D;
        Background background;
        Background background2;

        EnemyGenerator Generator;
        SpriteFont font;
        float currentTime = 0f;
        float scoreTime = 0f;
        float itemTime = 0f;
        int playerWidth = 75;
        int playerHeight = 100;
        int distanceFromRight = 120;
        int playerX;
        int playerY;
        Texture2D item2D;
        Texture2D teddyBear2D;
        Option _option = new Option();
        List<Enemy> enemiesToRemove; // Liste pour les ennemis à supprimer
        private List<int> HighScores;
        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.graphics.PreferredBackBufferWidth = 1920; // Largeur
            Globals.graphics.PreferredBackBufferHeight = 1080; // Hauteur
            //Globals.graphics.IsFullScreen = true;
            Globals.graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Generator = new EnemyGenerator();
            playerX = Globals.graphics.PreferredBackBufferWidth - distanceFromRight - playerWidth / 2;
            playerY = Globals.graphics.PreferredBackBufferHeight / 2 - playerHeight / 2;
            PlayerShip = new Player(playerX, playerY, playerWidth, playerHeight, 20, player, 3000);
            Globals.score = 0;
            Globals.isPause = false;
       
            background = new Background(0f, Globals.graphics.PreferredBackBufferWidth, Globals.graphics.PreferredBackBufferWidth*2, Globals.graphics.PreferredBackBufferHeight*2, 8f,Color.White, 1);
            background2 = new Background(0f, 0f - Globals.graphics.PreferredBackBufferWidth*2, Globals.graphics.PreferredBackBufferWidth*2, Globals.graphics.PreferredBackBufferHeight*2, 8f, Color.White, 2);

            if (File.Exists(Globals.fileHighScore))
            {
                Globals.reader = new StreamReader(Globals.fileHighScore);
                Globals.serializer = new XmlSerializer(typeof(List<int>));
                HighScores = (List<int>)Globals.serializer.Deserialize(Globals.reader);
                Globals.highScore = HighScores[0];
                Globals.timeScore = HighScores[1];
                Globals.reader.Close();
                Globals.reader = null;
            }
            
            enemiesToRemove = new List<Enemy>(); // Initialisation de la liste
            Globals.isResume = false;
            //Enemy teddy = new Enemy(200, 200, playerWidth, playerHeight, 20, teddyBear2D, 0, 10, 1,1);
        }
  
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = Content.Load<Texture2D>("Player/hand");
            meteore2D = Content.Load<Texture2D>("Ennemie/bucket");
            font = Content.Load<SpriteFont>("Font/dialogue");
            //Globals.egg2D = Content.Load<Texture2D>("Props/heartShoot2");
            Globals.pvItem2D = Content.Load<Texture2D>("Items/Pill42");
            Globals.bulletItem2D = Content.Load<Texture2D>("Items/BulletBag");
            Globals.teddyBearSpriteShit2D = Content.Load<Texture2D>("Ennemie/spriteSheetTeddyBear");
            Globals.teddyBearStatic2D = Content.Load<Texture2D>("Ennemie/teddyBear");
            Globals.pandaBear2D = Content.Load<Texture2D>("Ennemie/pandaBear");
            Globals.poulpy2D = Content.Load<Texture2D>("Ennemie/poulpy");
            Globals.heart2D = Content.Load<Texture2D>("Props/heartShoot2");
            Globals.background2D = Content.Load<Texture2D>("Background/tatami");
            Globals.plushSound01 = Content.Load<SoundEffect>("Sound/Enemy/plushSound01");
            Globals.gameSong = Content.Load<Song>("Song/funkyTown");
            Globals.pack01 = Content.Load<SoundEffect>("Sound/Item/pack01");
            Globals.pill01 = Content.Load<SoundEffect>("Sound/Item/pill01");
            MediaPlayer.Play(Globals.gameSong);
        }

        protected override void Update(GameTime gameTime)
        {

            _option.OptionUpdate(gameTime);
            if (!Globals.isResume) { 

            if (!Globals.isPause)
            {
                Globals.Update(gameTime);
                    background.BackgroundUpdate(gameTime);
                    background2.BackgroundUpdate(gameTime);

                    scoreTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerShip.UpdatePlayer();

                if (Globals.playerLife <= 0) Globals.isPause = !Globals.isPause;
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                itemTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (currentTime >1.1f)
                {
                    Generator.Generate(15);
                    currentTime = 0f;
                }
                if (itemTime > 12f)
                {
                    Generator.GenerateItem(2f);
                    itemTime = 0f;
                }

                foreach (var missile in Missile.allMissile)
                {
                    missile.MissileUpdate();
                    if (missile.PositionX < 0)
                    {
                        missile.Dead = true;
                    }
                    if (missile.Dead)
                    {
                        Missile.allMissile.Remove(missile);
                        break;
                    }
                }

                foreach (var item in Item.allItem)
                {
                    item.ItemUpdate(gameTime);
                    if (item.CollisionEnemyWithPlayer(PlayerShip))
                    {
                        Item.allItem.Remove(item);
                        break;
                    }
                    if (item.Dead)
                    {
                        Item.allItem.Remove(item);
                        break;
                    }
                }

                foreach (var enemy in Enemy.allEnemy)
                {
                    if (enemy.PositionX < Globals.graphics.PreferredBackBufferWidth - enemy.SizeX)
                    {
                        enemy.EnemyUpdate(gameTime);
                        if (PlayerShip.CollisionEnemyWithPlayer(enemy))
                        {

                            enemy.Dead = true;

                            if (Globals.score > 0) Globals.score -= (int)enemy.ScoreDeath;
                        }
                    }
                    else
                    {
                        enemy.Dead = true;
                        if (Globals.score >= 0) Globals.score -= (int)enemy.ScoreDeath;
                        if (Globals.playerLife > 0) Globals.playerLife -= (int)enemy.Damage;
                    }

                    foreach (var missile in Missile.allMissile)
                    {
                        if (enemy.CollisionEnemyWithMissile(missile))
                        {
                            enemy.Life--;
                            if (enemy.Life <= 0)
                            {
                                enemy.Dead= true;   
                            }
                            Globals.score += (int)enemy.ScoreDeath;
                        }


                        foreach (var item in Item.allItem)
                        {
                            if (item.CollisionEnemyWithMissile(missile))
                            {
                                missile.Dead = true;
                                item.Dead = true;
                            }
                        }


                    }
         
                    if (enemy.Dead)
                    {
                        Enemy.allEnemy.Remove(enemy);
                        Globals.plushSound01.Play(volume: 1f, pitch: 0.0f, pan: 0.0f);

                        break;
                    }
                
                }



                foreach (var animationUp in SpritesheetAnimation.animationListe)
                {
                    animationUp.Update(gameTime);
                }
            }
            else
            {
                if (Globals.score > Globals.highScore) Globals.highScore = Globals.score; _option.saveCollectInFile();

                if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    MediaPlayer.Stop();
                    Globals.Restart(gameTime);
                    MediaPlayer.Play(Globals.gameSong);
                    Globals.playerLife = 1000;
                    PlayerShip.PositionX = Globals.graphics.PreferredBackBufferWidth - distanceFromRight - playerWidth / 2;
                    PlayerShip.PositionY = Globals.graphics.PreferredBackBufferHeight / 2 - playerHeight / 2;
                    scoreTime = 0;
                    Enemy.allEnemy.Clear();
                    Missile.allMissile.Clear();
                    Item.allItem.Clear();
                }
            }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(Globals.background2D, new Rectangle(0, 0, Globals.graphics.PreferredBackBufferWidth, Globals.graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.Draw(Globals.background2D, new Rectangle((int)background.PositionX, (int)background.PositionY, background.SizeX, background.SizeY), background.Color);
            _spriteBatch.Draw(Globals.background2D, new Rectangle((int)background2.PositionX, (int)background2.PositionY, background2.SizeX, background2.SizeY), background2.Color);

            if (!Globals.isPause)
            {
                GraphicsDevice.Clear(Color.Black);

                _spriteBatch.Draw(PlayerShip.Texture, new Rectangle((int)PlayerShip.PositionX, (int)PlayerShip.PositionY, PlayerShip.SizeX, PlayerShip.SizeY), Color.White);
                foreach (var enemy in Enemy.allEnemy) { _spriteBatch.Draw(enemy.Texture, new Rectangle((int)enemy.PositionX, (int)enemy.PositionY, enemy.SizeX, enemy.SizeY),enemy.Color); }
                foreach (var missile in Missile.allMissile) { _spriteBatch.Draw(missile.Texture, new Rectangle((int)missile.PositionX, (int)missile.PositionY, missile.SizeX, missile.SizeY), Color.White); }
                foreach (var item in Item.allItem) { _spriteBatch.Draw(item.Texture, new Rectangle((int)item.PositionX, (int)item.PositionY, item.SizeX, item.SizeY), Color.White); }
                foreach (var animation in SpritesheetAnimation.animationListe){ animation.Draw(_spriteBatch, Globals.teddyBearSpriteShit2D); }
            }
            else
            {
                string message = "Relancer la partie press E";
                Vector2 messageSize = font.MeasureString(message);
                Vector2 screenCenter = new Vector2(Globals.graphics.PreferredBackBufferWidth / 2, Globals.graphics.PreferredBackBufferHeight / 2);
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.DrawString(font, message, screenCenter - messageSize / 2, Color.White);
            }
            if (Globals.score <= 0) { Globals.score = 0; }
            if (Globals.playerLife >= 1000) { Globals.playerLife = 1000; }
            if (Globals.playerLife <= 0) { Globals.playerLife = 0; }

            _spriteBatch.DrawString(font, "Survie " + Math.Round(scoreTime).ToString() + " S", new Vector2(Globals.graphics.PreferredBackBufferWidth / 8, Globals.graphics.PreferredBackBufferHeight / 8), Color.White);
            _spriteBatch.DrawString(font, "PV " + Globals.playerLife.ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth / 8, Globals.graphics.PreferredBackBufferHeight / 6), Color.White);
            _spriteBatch.DrawString(font, "Score " + Globals.score.ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth / 8, Globals.graphics.PreferredBackBufferHeight / 12), Color.White);
            _spriteBatch.DrawString(font, "Munition " + Globals.bullet.ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth / 8, Globals.graphics.PreferredBackBufferHeight / 5), Color.White);
            _spriteBatch.DrawString(font, "HighScore " + Globals.highScore.ToString(), new Vector2(Globals.graphics.PreferredBackBufferWidth / 8, Globals.graphics.PreferredBackBufferHeight /20), Color.White);

            if (Globals.isResume)
            {
                string message = "Pause";
                Vector2 messageSize = font.MeasureString(message);
                Vector2 screenCenter = new Vector2(Globals.graphics.PreferredBackBufferWidth / 2, Globals.graphics.PreferredBackBufferHeight / 2);

                GraphicsDevice.Clear(Color.Black);
                //_spriteBatch.DrawString(font, message, screenCenter - messageSize / 2, Color.White);
                if (Globals.resumeState >= 4) _spriteBatch.DrawString(font, message, screenCenter - messageSize / 2, Color.White);
                if (Globals.resumeState != 4) _spriteBatch.DrawString(font," "+Globals.resumeState.ToString(), screenCenter - messageSize /2 , Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
