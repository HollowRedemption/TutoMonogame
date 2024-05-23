using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    internal class Background
    {



        float _positionX;
        float _positionY;
        float _positionXInitial;
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
        Color _color;
        int _nbBackground;
        private static List<Background> allBackground = new List<Background>();

        public float Speed { get => _speed; set => _speed = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float PositionX { get => _positionX; set => _positionX = value; }
        public float PositionY { get => _positionY; set => _positionY = value; }
        public Rectangle Collider { get => _collider; set => _collider = value; }
        public int SizeX { get => _sizeX; set => _sizeX = value; }
        public int SizeY { get => _sizeY; set => _sizeY = value; }
        public bool Dead { get => _dead; set => _dead = value; }


        internal static List<Background> AllBackground { get => allBackground; set => allBackground = value; }
        public Color Color { get => _color; set => _color = value; }
        public int NbBackground { get => _nbBackground; set => _nbBackground = value; }

        public Background(float positionY, float positionX,int sizeX,int sizeY, float speed,Color color,int nbBackground)
        {
            _positionXInitial = positionX;
            _positionY = positionY;
            _positionX = positionX;
            _color = color;
            _speed = speed;
            _sizeX = sizeX;
            _sizeY = sizeY;
            _nbBackground = nbBackground;
            _dead = false;
            _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);
            allBackground.Add(this);

        }

        public void BackgroundUpdate(GameTime gameTime)
        {
            BasicMovement();
            _collider = new Rectangle((int)_positionX, (int)_positionY, _sizeX, _sizeY);

            // Check if the background has reached the right edge of the screen
            if (_positionX >= Globals.graphics.PreferredBackBufferWidth)
            {
                // Find the index of the current background in the list
                int currentIndex = AllBackground.IndexOf(this);

                // Find the index of the next background (cycling)
                int nextIndex = (currentIndex + 1) % AllBackground.Count;

                // Move the next background to its initial position on the left
                AllBackground[nextIndex]._positionX = 0-Globals.graphics.PreferredBackBufferWidth;

                // Set the current background's position to the right of the screen
                _positionX = Globals.graphics.PreferredBackBufferWidth - _sizeX*2;
            }
        }


        public void BasicMovement()
        {
            if (Globals.ModeStatic) _speed = 0;
            _positionX += _speed;

        }
    }
}
