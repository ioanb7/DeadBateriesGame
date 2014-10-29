using System;

namespace MyFirstXNAGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            bool isHavingAUserDefinedMap = false;
            if (args.Length == 0)
            {
                //default start
                isHavingAUserDefinedMap = false;
            }
            else
            {
                isHavingAUserDefinedMap = true;
            }

#if DEBUG
            isHavingAUserDefinedMap = true;
#else
            isHavingAUserDefinedMap = true;
#endif

            isHavingAUserDefinedMap = false;
            using (Game1 game = new Game1(isHavingAUserDefinedMap))
            {
                game.Run();
            }
        }
    }
#endif
}

