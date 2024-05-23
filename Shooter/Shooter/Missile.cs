using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
namespace Shooter
{
    internal class Missile
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

      

        public static List<Missile> allMissile = new List<Missile>();
        public float Speed { get => _speed; set => _speed = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float PositionX { get => _positionX; set => _positionX = value; }
        public float PositionY { get => _positionY; set => _positionY = value; }
        public Rectangle Collider { get => _collider; set => _collider = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public int SizeX { get => _sizeX; set => _sizeX = value; }
        public int SizeY { get => _sizeY; set => _sizeY = value; }
        public bool Dead { get => _dead; set => _dead = value; }

        public Missile(float positionY, float positionX, int sizeX, int sizeY, float speed, Texture2D texture, float damage)
        {
            _positionY = positionY;
            _positionX = positionX;
            _speed = speed;
            _texture = texture;
            _sizeX = sizeX;
            _sizeY = sizeY;
            _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);
            _damage = damage;
            _dead = false;
            allMissile.Add(this);
        }

        public void MissileUpdate()
        {
            _positionX -= Speed;
            _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);
        }


        public void Draw(SpriteBatch _spriteBatch, Texture2D sprite)
        {
            _spriteBatch.Draw(sprite, new Vector2(_positionX, _positionY), Color.White);
        }
    }
}
