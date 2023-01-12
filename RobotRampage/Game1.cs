using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RobotRampage;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    Texture2D spriteSheet;
    Texture2D titleScreen;
    SpriteFont pericles14;

    Sprite tempSprite;
    Sprite tempSprite2;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        graphics.PreferredBackBufferWidth = 800;
        graphics.PreferredBackBufferHeight = 600;
        graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: use this.Content to load your game content here

        spriteSheet = Content.Load<Texture2D>(@"Textures\SpriteSheet");
        titleScreen = Content.Load<Texture2D>(@"Textures\TitleScreen");
        pericles14 = Content.Load<SpriteFont>(@"Fonts\Pericles14");

        Camera.WorldRectangle = new Rectangle(0, 0, 1600, 1600);
        Camera.ViewPortWidth = 800;
        Camera.ViewPortHeight = 600;

        // Temporary Demo Code Begin
        tempSprite = new Sprite(
        new Vector2(100, 100),
        spriteSheet,
        new Rectangle(0, 64, 32, 32),
        Vector2.Zero);
        tempSprite2 = new Sprite(
        new Vector2(200, 200),
        spriteSheet,
        new Rectangle(0, 160, 32, 32),
        Vector2.Zero);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        Vector2 spriteMove = Vector2.Zero;
        Vector2 cameraMove = Vector2.Zero;
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            spriteMove.X = -1;
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            spriteMove.X = 1;
        if (Keyboard.GetState().IsKeyDown(Keys.W))
            spriteMove.Y = -1;
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            spriteMove.Y = 1;
        if (Keyboard.GetState().IsKeyDown(Keys.Left))
            cameraMove.X = -1;
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
            cameraMove.X = 1;
        if (Keyboard.GetState().IsKeyDown(Keys.Up))
            cameraMove.Y = -1;
        if (Keyboard.GetState().IsKeyDown(Keys.Down))
            cameraMove.Y = 1;

        Camera.Move(cameraMove);
        tempSprite.Velocity = spriteMove * 60;
        tempSprite.Update(gameTime);
        tempSprite2.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        spriteBatch.Begin();
        tempSprite.Draw(spriteBatch);
        tempSprite2.Draw(spriteBatch);
        spriteBatch.End();
        base.Draw(gameTime);
    }
}
