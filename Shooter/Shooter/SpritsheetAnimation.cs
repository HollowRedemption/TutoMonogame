using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;

namespace Shooter
{
    internal class SpritesheetAnimation
    {
        int frameWidth;
        int frameHeight;
        int frameX;
        int frameY;
        Texture2D textureInitial;
        List<Texture2D> frames;
        int currentFrameIndex;
        float frameTimer;
        float frameDuration;
        bool isLooping; // Ajout de la propriété pour contrôler la boucle
        int _sizeX;
        int _sizeY;
        float _positionX;
        float _positionY;
        public Texture2D CurrentFrame => frames[currentFrameIndex];

        public int SizeX { get => _sizeX; set => _sizeX = value; }
        public int SizeY { get => _sizeY; set => _sizeY = value; }
        public float PositionX { get => _positionX; set => _positionX = value; }
        public float PositionY { get => _positionY; set => _positionY = value; }

  
        public static List<SpritesheetAnimation> animationListe = new List<SpritesheetAnimation>();
        public SpritesheetAnimation(Texture2D textureInitial, int frameX, int frameY, float frameDuration,int positionX,int positionY,int sizeX,int sizeY ,bool isLooping = true)
        {
            this.textureInitial = textureInitial;
            this.frameX = frameX;
            this.frameY = frameY;
            this.frameDuration = frameDuration;
            this.isLooping = isLooping;
            _positionX = positionX;
            _positionY = positionY;
            _sizeX = sizeX;
            _sizeY = sizeY;

            frameWidth = textureInitial.Width / frameX;
            frameHeight = textureInitial.Height / frameY;

            frames = new List<Texture2D>();

            currentFrameIndex = 0;
            frameTimer = 0f;

            for (int y = 0; y < frameY; y++)
            {
                for (int x = 0; x < frameX; x++)
                {
                    // Découpez la texture en frames individuelles
                    Rectangle sourceRectangle = new Rectangle(x * frameWidth, y * frameHeight, frameWidth, frameHeight);
                    Texture2D frameTexture = new Texture2D(textureInitial.GraphicsDevice, frameWidth, frameHeight);
                    Color[] data = new Color[frameWidth * frameHeight];
                    textureInitial.GetData(0, sourceRectangle, data, 0, data.Length);
                    frameTexture.SetData(data);

                    frames.Add(frameTexture);
                }
            }
            animationListe.Add(this);
        }
        public void RestartAnimation()
        {
            currentFrameIndex = 0; // Réinitialise l'index de la frame à la première frame
            frameTimer = 0f; // Réinitialise le timer de l'animation
        }

        public void Update(GameTime gameTime)
        {
            if (!isLooping && currentFrameIndex == frames.Count - 1)
            {
                // Si l'animation ne doit pas boucler et qu'elle est terminée, ne rien faire
                return;
            }

            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (frameTimer >= frameDuration)
            {
                currentFrameIndex = (currentFrameIndex + 1) % frames.Count;
                frameTimer = 0f;
            }
        }

        public void Draw(SpriteBatch _spriteBatch, Texture2D texture2D)
        {
            _spriteBatch.Draw(texture2D, new Rectangle((int)PositionX, (int)PositionY,SizeX,SizeY), Color.White);
        }
    }
    
}
