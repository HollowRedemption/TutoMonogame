using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Shooter
{
    internal class Item
    {
        float _positionX;
        float _positionY;
        float _speed;
        Texture2D _texture;
        int _sizeX;
        int _sizeY;
        Rectangle _collider;
        float _damage;
        int _bulletAdd;
        int _pvAdd;
        bool _dead;
        int _itemType;


        public static List<Item> allItem = new List<Item>();
        public float Speed { get => _speed; set => _speed = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float PositionX { get => _positionX; set => _positionX = value; }
        public float PositionY { get => _positionY; set => _positionY = value; }
        public Rectangle Collider { get => _collider; set => _collider = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public int SizeX { get => _sizeX; set => _sizeX = value; }
        public int SizeY { get => _sizeY; set => _sizeY = value; }
        public bool Dead { get => _dead; set => _dead = value; }
        public int BulletAdd { get => _bulletAdd; set => _bulletAdd = value; }
        public int PvAdd { get => _pvAdd; set => _pvAdd = value; }
        public int ItemType { get => _itemType; set => _itemType = value; }

        public Item(float positionY, float positionX, float speed,int itemType)
        {
            _positionY = positionY;
            _positionX = positionX;
       
            _speed = speed;
            _itemType = itemType;
            _dead = false;
            _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);
            SetItem(_itemType);
            allItem.Add(this);

        }

        public void ItemUpdate(GameTime gameTime)
        {
            if (_positionY < Globals.graphics.PreferredBackBufferHeight - Texture.Height/4)
            {
                if (_itemType == 0) BasicMovement();
                if (_itemType == 1) BasicMovement();

                _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);
            }
            else
            {
                Dead = true;
            }

        }

        public void BasicMovement()
        {

            _positionY += _speed;

        }
        public void WaveMovement(GameTime gameTime)
        {
            float amplitude = 20f; // Amplitude de la vague
            float frequency = 5f; // Fréquence de la vague

            float time = (float)gameTime.TotalGameTime.TotalSeconds;
            float newPositionX = _positionX - _speed / 2; // Modifier '+' en '-' pour inverser la direction
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

        public void SetItem(int itemType)
        {
            if (0 == itemType)//pv
            {
                _texture = Globals.pvItem2D;
                //_life = 1;
                _speed /= 2;
                _sizeX = 50;
                _sizeY = 75;
                _pvAdd = 420;
                //_scoreDeath = 200;
            }
            if (1 == itemType)//bullet
            {
                //_life = 2;
                _speed /= 10;
                _texture = Globals.bulletItem2D;
                _sizeX = 125;
                _sizeY = 125;
                _bulletAdd = 5;
         
                //_scoreDeath = 150;
            }
        }


    

        public void Draw(SpriteBatch _spriteBatch, Texture2D sprite)
        {
            _spriteBatch.Draw(sprite, new Vector2(_positionX, _positionY), Color.White);
        }

        public bool CollisionEnemyWithPlayer(Player player)
        {
            if (_collider.Intersects(player.Collider))
            {

               
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool CollisionEnemyWithMissile(Missile missile)
        {
            if (_collider.Intersects(missile.Collider))
            {
                if (!missile.Dead) { 
                Globals.playerLife += _pvAdd;
                Globals.bullet += _bulletAdd;
                    if (ItemType == 0) Globals.pill01.Play(volume: 1f, pitch: 0.0f, pan: 0.0f);

                    if (ItemType == 1) Globals.pack01.Play(volume: 1f, pitch: 0.0f, pan: 0.0f);

                }
                return true;

            }
            else
            {
                return false;
            }
        }

    }
}
