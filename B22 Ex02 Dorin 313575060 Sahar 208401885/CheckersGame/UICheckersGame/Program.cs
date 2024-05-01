// $G$ SFN-012 (+10) Bonus: Events in the Logic layer are handled by the UI.
// $G$ SFN-012 (+10) Bonus: Implemented user interface with richer graphics / motion.
// $G$ DSN-999 (-5) Delegate can define in the class that use them
// $G$ CSS-999 (-6) Event handler parameters should be (object sender, EventArgs e)
// $G$ SFN-999 (-3) You sholud alert when the user didn't fill name for the player/s
using System;
using System.Windows.Forms;

namespace UICheckersGame
{
    public class Program
    {
        [STAThread]

        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WindowsFormUI checkersGame = new WindowsFormUI();
            checkersGame.LaunchGame();
        }
    }
}
