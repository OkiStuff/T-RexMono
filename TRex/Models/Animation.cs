using System.Net.Mime;

namespace TRex.Models
{
    public enum Animations
    {
        Walk
    }
    
    public class Animation
    {

        public static void PlayAnimation(Animations animations)
        {
            //if (animations == Animations.Walk)
            //{
            //    switch (Game1.AnimationTimer)
            //    {
            //        case 0:
            //            Game1.playerTexture = Game1.LoadTexture("Player");
            //            break;
            //        case 3:
            //            Game1.playerTexture = Game1.LoadTexture("Dino2");
            //            break;
            //        case 6:
            //            Game1.playerTexture = Game1.LoadTexture("Dino3");
            //            break;
            //    }
            //    Game1.AnimationTimer = 0f;
            //}
        }
    }
}