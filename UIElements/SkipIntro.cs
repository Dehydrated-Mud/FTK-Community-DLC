using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityDLC.UIElements;
public static class SkipIntro
{
    public static void Init()
    {
        // Taken from Amadare's QoL mod
        // emulates pressing button to skip intro on every frame
        On.SplashScreen.GetAnyButton += (_, _) => true;

        // prevents "prepare to die" message
        uiStartGame.gIsFirstTime = false;
    }
}
