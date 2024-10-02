using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aplikacia.HraTexty
{
    public class Pismeno(Textury textury, string pismeno, Vector2 pozicia)
    {
        public string Pismeno_ { get; } = pismeno;

        public bool JeHladane { get; set; } = false;
        public bool Najdene { get; set; } = false;
        public Vector2 Pozicia { get; set; } = pozicia;
        private readonly Texture2D TexturaSipka = textury.TexturaSipka;

        public void Vykresli(SpriteBatch nastrojVykreslovanie, SpriteFont font, Vector2 skalovanie)
        {
            if (!Najdene)
                nastrojVykreslovanie.DrawString(font, Pismeno_, Pozicia, Color.White, 0, new Vector2(), skalovanie, new SpriteEffects(), 0);
            else
                nastrojVykreslovanie.DrawString(font, Pismeno_, Pozicia, Color.LimeGreen, 0, new Vector2(), skalovanie, new SpriteEffects(), 0);

            if (JeHladane)
                nastrojVykreslovanie.Draw(TexturaSipka, new Rectangle((int)Pozicia.X - 30, 50 + (int)font.MeasureString(Pismeno_).Y / 2,
                    (int)font.MeasureString("X").X * 2, (int)font.MeasureString("X").Y / 2), Color.White);
        }
    }
}
