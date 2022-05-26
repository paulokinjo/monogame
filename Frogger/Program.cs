using System;

namespace Frogger
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var frogger = new FroggerGame())
                frogger.Run();
        }
    }
}
