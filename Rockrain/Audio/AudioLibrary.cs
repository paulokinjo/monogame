using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Rockrain.Audio
{
  public class AudioLibrary
  {
    public SoundEffect Explosion { get; private set; }

    public SoundEffect NewMeteor { get; private set; }

    public SoundEffect MenuBack { get; private set; }

    public SoundEffect MenuSelect { get; private set; }

    public SoundEffect MenuScroll { get; private set; }

    public SoundEffect PowerGet { get; private set; }

    public SoundEffect PowerShow { get; private set; }

    public Song BackMusic { get; private set; }

    public Song StartMusic { get; private set; }

    public void LoadContent(ContentManager Content)
    {
      Explosion = Content.Load<SoundEffect>("explosion");
      NewMeteor = Content.Load<SoundEffect>("newmeteor");
      BackMusic = Content.Load<Song>("backMusic");
      StartMusic = Content.Load<Song>("startMusic");
      MenuBack = Content.Load<SoundEffect>("menu_back");
      MenuSelect = Content.Load<SoundEffect>("menu_select3");
      MenuScroll = Content.Load<SoundEffect>("menu_scroll");
      PowerShow = Content.Load<SoundEffect>("powershow");
      PowerGet = Content.Load<SoundEffect>("powerget");
    }
  }
}