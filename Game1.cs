using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleAnimationNamespace;

namespace Assignment_01;
// dotnet mgcb-editor Content/Content.mgcb
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _station;
    private Texture2D _beetle;

    private SpriteFont _font;
    private string _output = "Your First Obsatcle";

    private SimpleAnimation _walkingAnimation;
    private Vector2 horseLocation = new Vector2(80, 200);
    private Vector2 beetleLocation = new Vector2(400,140);
    private int beetleModifier = 1;

    Vector2 _PlayerInput;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 320;
        _graphics.ApplyChanges();
        
        base.Initialize();
        _PlayerInput = Vector2.Zero;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _station = Content.Load<Texture2D>("Station");
        _beetle = Content.Load<Texture2D>("Beetle");

        _font = Content.Load<SpriteFont>("SansFont");

        _walkingAnimation = new SimpleAnimation(Content.Load<Texture2D>("Horse"), 100, 80, 8, 8);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _walkingAnimation.Update(gameTime);

        KeyboardState kbCurrentState = Keyboard.GetState();

        _PlayerInput = Vector2.Zero;

        if (kbCurrentState.IsKeyDown(Keys.Down) || kbCurrentState.IsKeyDown(Keys.S))
        {
            _PlayerInput += new Vector2(0,1);
        }
        if (kbCurrentState.IsKeyDown(Keys.Up) || kbCurrentState.IsKeyDown(Keys.W))
        {
            _PlayerInput += new Vector2(0,-1);
        }
        if (kbCurrentState.IsKeyDown(Keys.Right) || kbCurrentState.IsKeyDown(Keys.D))
        {
            _PlayerInput += new Vector2(1,0);
        }
        if (kbCurrentState.IsKeyDown(Keys.Left) || kbCurrentState.IsKeyDown(Keys.A))
        {
            _PlayerInput += new Vector2(-1,0);
        }


        if (beetleLocation.Y >= 275 || beetleLocation.Y <= 0)
        {
            beetleModifier *= -1;
        }
        // TODO: Add your update logic here
        beetleLocation.Y += 4*beetleModifier;
        horseLocation += _PlayerInput*5;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_station, Vector2.Zero, Color.White);

        _spriteBatch.Draw(_beetle, beetleLocation, Color.White);

        _spriteBatch.DrawString(_font, _output, new Vector2(20,20), Color.White);

        _walkingAnimation.Draw(_spriteBatch, horseLocation, SpriteEffects.None);

        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
