using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aplikacia.Interface
{
    public interface IVykreslovane
    {
        void Aktualizuj(GameTime hernyCas);
        public void Vykresli(GameTime hernyCas, SpriteBatch nastrojVykreslovanie);
    }
}
