
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shooter
{
    internal class Option
    {
        bool isOpen = false;
        float resumeStateTimer;
        bool activeChrono = false;
        int compteur = 0;
       
        public void saveCollectInFile()
        {
            List<int> infosHighscore = new List<int>();
            infosHighscore.Add(Globals.highScore);
            infosHighscore.Add(Globals.timeScore);
            Globals.serializer = new XmlSerializer(typeof(List<int>));
            Globals.writer = new StreamWriter(Globals.fileHighScore);
            Globals.serializer.Serialize(Globals.writer, infosHighscore);
            Globals.writer.Close();
            Globals.writer = null;
        }

        public void OptionUpdate(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !isOpen )
            
            {
                if (compteur==0)
                {
                    compteur++;
                    isOpen = true;
                    
                }
                else if (compteur == 1)
                {
                    compteur++;
                }
       

            }
            if (Keyboard.GetState().IsKeyUp(Keys.Escape) && isOpen)
            {
                isOpen = false;
            }

            if (compteur == 1)
            {
                Globals.resumeState = 4;
                Globals.isResume = true;
            }
            if (compteur == 2)
            {
                activeChrono = true;
                compteur = 3;
                Globals.resumeState = 4;
               
            }

            if (activeChrono)
            {
                resumeStateTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                Globals.timerTest = resumeStateTimer;
                if (resumeStateTimer > 1)
                {
                    resumeStateTimer = 0f;
                    if (Globals.resumeState <= 0)
                    {
                        Globals.isResume = false;
                        activeChrono = false;
               
                        resumeStateTimer = 0;
                        compteur = 0;
                    }
                    Globals.resumeState--;

                }


            }



        }
    }
}
