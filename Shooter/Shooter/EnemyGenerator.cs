using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    internal class EnemyGenerator
    {

        int _enemy = 0;

        Texture2D _texture;

        public int enemy { get => _enemy; set => _enemy = value; }

        public Texture2D texture { get => _texture; set => _texture = value; }

        public EnemyGenerator()
        {

         
        }

        public void EnemyBrain()
        {
    
        }


        public void Generate(float speed)
        {
            Random random = new Random();
            List<Texture2D> plushList2D = new List<Texture2D>();
            plushList2D.Add(Globals.poulpy2D);
            plushList2D.Add(Globals.pandaBear2D);
            plushList2D.Add(Globals.teddyBearStatic2D);
            //plushList2D.Add(Globals.teddyBearSpriteShit2D);

            int randomPlush = random.Next(0, plushList2D.Count());
            int minY = 0; // Position Y minimale (haut de la fenêtre)
            int maxY = Globals.graphics.PreferredBackBufferHeight - 150; // Position Y maximale (bas de la fenêtre)
            int randomY = random.Next(minY, maxY + 1);
            int randomSpeed = random.Next(0,(int )speed);




            Enemy enemy = new Enemy(randomY, 0,speed+randomSpeed,randomPlush);
     

        }

        public void GenerateItem(float speed)
        {
            Random random = new Random();
            List<Texture2D> itemsList2D = new List<Texture2D>();
            itemsList2D.Add(Globals.pvItem2D);
            itemsList2D.Add(Globals.bulletItem2D);
            int minY = 0; // Position Y minimale (haut de la fenêtre)
            int maxY = Globals.graphics.PreferredBackBufferHeight - 150; // Position Y maximale (bas de la fenêtre) - ajustée à la hauteur de l'objet
            int randomItem = random.Next(0, itemsList2D.Count());
            int randomX = random.Next(0, Globals.graphics.PreferredBackBufferWidth - 150); // Ajusté à la largeur de l'objet
            int randomY = random.Next(minY, maxY + 1); 
            int randomSpeed = random.Next(0, (int)speed);

            new Item(0, randomY, speed + randomSpeed, randomItem);
        }


    }
}
