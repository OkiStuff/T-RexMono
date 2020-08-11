namespace TRex.Models
{
    public enum SoundTypes
    {
        Jump,
        Death,
        Restart,
        ScoreBonus,
        ButtonHover,
        BGMusic,
    }

    public class Sounds
    {
        
        public static void PlaySound(SoundTypes sound)
        {
            switch (sound)
            {
                case SoundTypes.Jump:
                    Game1.JumpSound.Pitch = (float)Game1.Random.NextDouble() - .5f;
                    Game1.JumpSound.Play();
                    break;
                case SoundTypes.Death:
                    Game1.DeathSound.Play();
                    break;
                case SoundTypes.Restart:
                    Game1.RestartSound.Play();
                    break;
                case SoundTypes.ScoreBonus:
                    Game1.ScoreBonusSound.Play();
                    break;
                case SoundTypes.ButtonHover:
                    Game1.ButtonHover.Play();
                    break;
                case SoundTypes.BGMusic:
                    Game1.BGMusic.Play();
                    break;
                    
            }
        }
    }
}