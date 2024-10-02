using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Aplikacia.Interface;

namespace Aplikacia.Zavodnici
{
    public class Auto : IVykreslovane
    {
        public float Rychlost { get; protected set; }
        public Vector2 Skalovanie { get; protected set; }
        public Vector2 Pozicia { get; protected set; }
        public readonly Texture2D Textura;

        public Auto(Texture2D textura, float rychlost, Vector2 pozicia)
        {
            Rychlost = rychlost;
            Pozicia = pozicia;
            Textura = textura;
            var (x, y) = GetSkalovanie();
            Skalovanie = new Vector2(x, y);
        }

        protected virtual void AktualizujPoziciu()
        {
            var poz = Pozicia;
            poz.X += Rychlost;
            Pozicia = poz;
        }

        public float GetPoziciuDalsiehoAuta()
        {
            var pozicia = Pozicia;
            var noveY = pozicia.Y + (0.18 * Hra.RozlisenieVyska);

            return (float)noveY;
        }

        public void Aktualizuj(GameTime hernyCas)
        {
            AktualizujPoziciu();
        }

        public void Vykresli(GameTime hernyCas, SpriteBatch nastrojVykreslovanie)
        {
            nastrojVykreslovanie.Draw(Textura, Pozicia, null, Color.White, 0, new Vector2(), Skalovanie, new SpriteEffects(), 0);
        }

        private (float x, float y) GetSkalovanie()
        {
            double x = (0.09 * (double)Hra.RozlisenieSirka) / (double)Textura.Width;
            double y = (0.093 * (double)Hra.RozlisenieVyska) / (double)Textura.Height;
            return ((float)x, (float)y);
        }
    }
}
