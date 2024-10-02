using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aplikacia.PozadieHry
{
    public class Pozadie(Texture2D textura)
    {
        private readonly Texture2D Textura = textura;

        public void Vykresli(SpriteBatch nastrojVykreslovanie)
        {
            nastrojVykreslovanie.Draw(Textura, new Rectangle(0, 0, Hra.RozlisenieSirka, Hra.RozlisenieVyska), Color.White);
        }
    }
}
