using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Shooter
{
    internal class Enemy
    {
        float _positionX;
        float _positionY;
        float _speed;
        Texture2D _texture;
        int _sizeX;
        int _sizeY;
        Rectangle _collider;
        float _damage;
        bool _dead;
        private int _nbFrameX;
        private int _nbFrameY;
        private float _row;
        int _enemyType;
        int _life;
        float _scoreDeath;
        SpritesheetAnimation _animation;
        bool startAnimation = false;
        public static List<Enemy> allEnemy = new List<Enemy>();
        Color _color = Color.White;
        public float Speed { get => _speed; set => _speed = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float PositionX { get => _positionX; set => _positionX = value; }
        public float PositionY { get => _positionY; set => _positionY = value; }
        public Rectangle Collider { get => _collider; set => _collider = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public int SizeX { get => _sizeX; set => _sizeX = value; }
        public int SizeY { get => _sizeY; set => _sizeY = value; }
        public bool Dead { get => _dead; set => _dead = value; }
        public int Life { get => _life; set => _life = value; }
        public float ScoreDeath { get => _scoreDeath; set => _scoreDeath = value; }
        public int NbFrameX { get => _nbFrameX; set => _nbFrameX = value; }
        public int NbFrameY { get => _nbFrameY; set => _nbFrameY = value; }
        public float Row { get => _row; set => _row = value; }
        public int EnemyType { get => _enemyType; set => _enemyType = value; }
        public bool StartAnimation { get => startAnimation; set => startAnimation = value; }
        internal SpritesheetAnimation Animation { get => _animation; set => _animation = value; }
        public Color Color { get => _color; set => _color = value; }

        public Enemy(float positionY, float positionX, float speed,int enemyType/*, int nbFrameX, int nbFrameY, float row*/)
        {
            _positionY = positionY;
            _positionX = positionX;
            _speed = speed;
            _dead = false;
            _enemyType = enemyType;
      
            SetEnemy(enemyType);

            _collider = new Rectangle((int)_positionX, (int)_positionY,_sizeX, _sizeY);
            allEnemy.Add(this);
          
        }

        public void EnemyUpdate(GameTime gameTime)
        {
            if (_enemyType == 0) WaveMovement(gameTime);
            if (_enemyType == 1) BasicMovement();
            if (_enemyType == 2)  BasicMovement();
            if(_enemyType == 3) AnimationMovement(gameTime);


            //_anim.Update(); 
        
            _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);
        }

        public void SetEnemy(int enemy)
        {
            if (0 == enemy)//poulpy
            {
                _texture = Globals.poulpy2D;
                _life= 1;
                _speed /= 2;
                _sizeX = 75;
                _sizeY = 150;
                _damage = 150;
                _scoreDeath = 200;
            }
            if (1 == enemy)//panda
            {
                _life = 2;
                _speed /= 10;
                _texture = Globals.pandaBear2D;
                _sizeX = 100;
                _sizeY = 100;
                _damage = 200;
                _scoreDeath = 150;
                _color = Color.White;
            }
            if (2 == enemy) // bear
            {
                _texture = Globals.teddyBearStatic2D;
                _life = 1;
                _sizeX = 75;
                _sizeY = 75;
                _damage = 100;
                _scoreDeath = 300;
                _nbFrameX = 10;
                _nbFrameY = 1;
                _row = 1;
                _speed /= 2;
                _color = Color.White;

            }
            if (3 == enemy) // bear
            {
                _texture = Globals.teddyBearSpriteShit2D;
                _life = 1;
                _sizeX = 1500;
                _sizeY = 150;
                _damage = 100;
                _scoreDeath = 0;
                _nbFrameX = 10;
                _nbFrameY = 1;
                _row = 1;
                _color = Color.White;
            }

        }
        public void AnimationMovement(GameTime gameTime)
        {
            _animation.Update(gameTime);
        }

        public void BasicMovement()
        {
            if (Life > 0)
            {
                _positionX += _speed;

            }   
        }
        public void WaveMovement(GameTime gameTime)
        {
            float amplitude = 20f; // Amplitude de la vague
            float frequency = 5f; // Fréquence de la vague

            float time = (float)gameTime.TotalGameTime.TotalSeconds;
            float newPositionX = _positionX + _speed / 2;
            float newPositionY = _positionY + amplitude * (float)Math.Sin(frequency * time);
            float screenHeight = Globals.graphics.PreferredBackBufferHeight; 

            if (newPositionY > screenHeight - _sizeY)
            {
                newPositionY = screenHeight - _sizeY;
            }

            if (newPositionY < 0)
            {
                newPositionY = 0;
            }

            _positionX = newPositionX;
            _positionY = newPositionY;
        }


        public void Draw()
        {
        }

        public bool CollisionEnemyWithMissile(Missile missile)
        {

                if (_collider.Intersects(missile.Collider))
                {
              
       
                missile.Dead = true;
                //if(EnemyType == 2) _animation = new SpritesheetAnimation(Globals.teddyBearSpriteShit2D, 10, 1, 0.4f, (int)PositionX, (int)PositionY, 150, 150, false);
                //if (EnemyType == 2) _animation.RestartAnimation();
      
                Globals.plushSound01.Play(volume: 1f, pitch: 0.0f, pan: 0.0f);
             

                return true;
                   
                }
            else
            {
                return false;
            }
        }







    }
}
