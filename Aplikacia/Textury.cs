using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Aplikacia
{
    public class Textury
    {
        public readonly Texture2D TexturaHrac;
        public readonly Texture2D TexturaProtihrac;
        public readonly Texture2D TexturaPozadie;
        public readonly SpriteFont Font;
        public readonly Texture2D TexturaSipka;
        public readonly Texture2D TexturaZivot;
        public readonly int PrvyZivotX = (int)(0.5 * Hra.RozlisenieSirka);
        public readonly int PrvyZivotY = (int)(0.055 * Hra.RozlisenieVyska);
        public readonly int VyskaZivota = (Hra.RozlisenieVyska / 8);
        public readonly int SirkaZivota = (Hra.RozlisenieSirka / 14);

        public Textury(ContentManager manazerObsahu)
        {
            TexturaHrac = manazerObsahu.Load<Texture2D>("hrac");
            TexturaProtihrac = manazerObsahu.Load<Texture2D>("protihrac");
            TexturaPozadie = manazerObsahu.Load<Texture2D>("pozadie");
            Font = manazerObsahu.Load<SpriteFont>("font");
            Font.Spacing = 5;
            TexturaSipka = manazerObsahu.Load<Texture2D>("sipka");
            TexturaZivot = manazerObsahu.Load<Texture2D>("zivot");
        }
    }
}
