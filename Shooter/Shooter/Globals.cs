//Auteur : All
//Date : 15.02.2023
//Description : Stock all the global variables.
//Version : 1.0
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml.Serialization;
using Shooter;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

public static class Globals
{
    public static float TotalSeconds { get; set; }
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }

    public static void Update(GameTime gt)
    {
        TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
    }
    public static void Restart(GameTime gt)
    {

            isPause = false;
            score = 0;
            playerLife = 1000;

    }
    public static StreamReader reader;
    public static StreamWriter writer;
    public static XmlSerializer serializer;
    public static string fileHighScore = "fileHighScore.txt";

    public static GraphicsDeviceManager graphics;   
    public static bool isPause;
    public static bool isResume;
    public static int resumeState;
    public static int bullet;
    public static int score;
    public static int playerLife;
    public static int highScore;
    public static int timeScore;
    public static float timerTest;
    public static  bool ModeStatic;  

    public static Texture2D teddyBearStatic2D;
    public static Texture2D teddyBearSpriteShit2D;
    public static Texture2D pandaBear2D;
    public static Texture2D poulpy2D;
    public static Texture2D heart2D;
    public static Texture2D background2D;
    public static Texture2D pvItem2D;
    public static Texture2D bulletItem2D;

    public static Song gameSong;
    public static SoundEffect plushSound01;
    public static SoundEffect pack01;
    public static SoundEffect pill01;



}
