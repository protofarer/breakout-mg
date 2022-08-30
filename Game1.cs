using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Constants;

namespace breakout_mg;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;
    private KeyboardState _oldKBState;
    private bool _isDebugOverlay;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

        // _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        // _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        // _graphics.IsFullScreen = true;

        _graphics.ApplyChanges();

        Window.Title = "Breakout!";
        Content.RootDirectory = "Content";

        IsMouseVisible = true;
        IsFixedTimeStep = false;
    }

    protected override void Initialize()
    {
        _renderTarget = new RenderTarget2D(GraphicsDevice, VIRTUAL_WIDTH, VIRTUAL_HEIGHT);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        double dt = gameTime.ElapsedGameTime.TotalSeconds;

        KeyboardState newKBState = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_oldKBState.IsKeyDown(Keys.R) && newKBState.IsKeyUp(Keys.R))
            Program.NewGame();

        // if (_oldKBState.IsKeyDown(Keys.B) && newKBState.IsKeyUp(Keys.B))
        //     phase = phase == Phase.Play ? Phase.Pause : Phase.Play;

        if (_oldKBState.IsKeyDown(Keys.Q) && newKBState.IsKeyUp(Keys.Q))
            _isDebugOverlay = !_isDebugOverlay;

        // if (_oldKBState.IsKeyDown(Keys.T) && newKBState.IsKeyUp(Keys.T))
        // {
        //     if (paddle.IsAI)
        //         paddleOne.TurnAIOff();
        //     else
        //         paddleOne.TurnAIOn();
        // }

        _oldKBState = newKBState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);

        GraphicsDevice.Clear(Color.CornflowerBlue);

        int frameRate = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);

        _spriteBatch.Begin();

        if (_isDebugOverlay)
        {
        //    _spriteBatch.DrawString(
        //         sfontDebug, 
        //         $"fps: {frameRate}", 
        //         new Vector2(VIRTUAL_WIDTH - 75, 3), 
        //         Color.Green
        //     );

            // _spriteBatch.DrawString(
            //     sfontDebug, 
            //     $"sp_b: {ball._speed}", 
            //     new Vector2(VIRTUAL_WIDTH - 75, 10), 
            //     Color.Green
            // );
        }
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            BlendState.AlphaBlend,
            SamplerState.PointClamp
        );

        _spriteBatch.Draw(
            _renderTarget,
            new Rectangle(
                0, 0,
                WINDOW_WIDTH, WINDOW_HEIGHT
            ),
            Color.White
        );

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
