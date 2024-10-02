using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Aplikacia
{
    public class Hra : Game, INotifyPropertyChanged
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static readonly int RozlisenieSirka = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static readonly int RozlisenieVyska = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public static readonly double VitaznaCiaraX = RozlisenieSirka * 0.9;
        public Level Level_ { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _KoniecHry = false;
        public bool KoniecHry
        {
            get => _KoniecHry;
            set
            {
                _KoniecHry = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Hra()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Initialize();
        }

        public static float VypocitajRychlostHraca()
        {
            double rychlost = 0.005 * RozlisenieSirka;
            return (float)rychlost;
        }
        public void NacitajLevel(Level level)
        {
            Level_ = level;
        }

        protected override void Initialize()
        {
            Window.Position = new Point(0, 0);
            _graphics.PreferredBackBufferWidth = RozlisenieSirka;
            _graphics.PreferredBackBufferHeight = RozlisenieVyska;
            _graphics.ApplyChanges();
            Exiting += OnExiting;
            base.Initialize();

        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            KoniecHry = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (!Level_.HraBezi && Level_.PresloOdpocitavanie)
            {
                Exit();
                KoniecHry = true;
            }
            Level_.Aktualizuj(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            Level_.Vykresli(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
