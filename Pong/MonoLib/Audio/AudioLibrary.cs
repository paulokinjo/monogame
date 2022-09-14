using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace MonoLib.Audio
{
    public class AudioLibrary
  {
    public SoundEffect MenuBack { get; private set; }

    public SoundEffect MenuSelect { get; private set; }

    public SoundEffect MenuScroll { get; private set; }


    public void LoadContent(ContentManager Content)
    {
      MenuBack = Content.Load<SoundEffect>("menu_back");
      MenuSelect = Content.Load<SoundEffect>("menu_select3");
      MenuScroll = Content.Load<SoundEffect>("menu_scroll");
    }
  }
}