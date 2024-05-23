using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    internal class Player
    {
        float _positionX;
        float _positionY;
        int _sizeX;
        int _sizeY;
        float _speed;
        Texture2D _texture;

        Rectangle _collider;
        float shootTime = 0f;
        float reloadBulletTime = 0f;


        public float Speed { get => _speed; set => _speed = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float PositionX { get => _positionX; set => _positionX = value; }
        public float PositionY { get => _positionY; set => _positionY = value; }
        public Rectangle Collider { get => _collider; set => _collider = value; }

        public int SizeX { get => _sizeX; set => _sizeX = value; }
        public int SizeY { get => _sizeY; set => _sizeY = value; }
        //Animation animTest;
        public Player(int positionX, int positionY, int sizeX, int sizeY, float speed, Texture2D texture,int life) {
        _positionX = positionX;
        _positionY = positionY;
        _sizeX = sizeX;
        _sizeY = sizeY;
        _speed = speed;
        _texture = texture;
        _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);
        Globals.playerLife = 1000;
        Globals.bullet = 10;
            //animTest = new Animation(Globals.teddyBearSpriteShit2D, 10, 1, 2f);

        }

        public void UpdateTimer() {

            shootTime += (float)Globals.TotalSeconds;
            reloadBulletTime += (float)Globals.TotalSeconds;
            if (reloadBulletTime > 1f)
            {
                Globals.bullet++;
                reloadBulletTime = 0f;
            }

        }

        public void UpdatePlayer() {
            MouseState mouseState = Mouse.GetState();
            UpdateTimer();

    
    
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (shootTime > 0.2f && Globals.bullet>0)
                {
                    Missile missile = new Missile(_positionY, _positionX, 60, 60, 20, Globals.heart2D, 100);
                    Globals.bullet--;
                    shootTime = 0f;
                }

            }
            int i = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                Globals.ModeStatic =! Globals.ModeStatic;
                //SpritesheetAnimation test = new SpritesheetAnimation(Globals.teddyBearSpriteShit2D, 10, 1, 4f);
                //if (i <= 10)
                //{
                //    i++;
                //}
                //else
                //{
                //    i = 0;
                //}

                //_texture = test.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (_positionY + Texture.Height/10 > 0)
                {
                    _positionY-=Speed;
                }
                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (_positionY < Globals.graphics.PreferredBackBufferHeight -Texture.Height/2)
                {
                    _positionY += Speed;
                }

            }
            //if (Keyboard.GetState().IsKeyDown(Keys.A))
            //{
            //    if (_positionX + Texture.Width > 0)
            //    {
            //        _positionX -= Speed;
            //    }
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //{
            //    if (_positionX < Globals.graphics.PreferredBackBufferWidth - Texture.Width - 20)
            //    {
            //        _positionX += Speed;
            //    }
            //}
            _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);

        }

        public bool CollisionEnemyWithPlayer(Enemy enemy)
        {

            if (_collider.Intersects(enemy.Collider))
            {
                LooseLife(enemy.Damage);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void LooseLife(float Damage)
        {
            if (Globals.playerLife >= 0)
            {
                Globals.playerLife -= (int)Damage;
            }
      

        }

        public void Draw()
        {
            //animTest.Draw(new System.Numerics.Vector2(_positionX, _positionY));
        }
    }
}
