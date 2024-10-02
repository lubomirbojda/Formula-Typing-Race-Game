using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Aplikacia.Zavodnici
{
    public class Hrac(Texture2D textura, float rychlost, Vector2 pozicia, int pocetZivotov) : Auto(textura, rychlost, pozicia)
    {
        public int PocetPosunov { get; set; } = 0;
        public readonly int PocetZivotov = pocetZivotov;

        protected override void AktualizujPoziciu()
        {
            float pozX = PocetPosunov * Rychlost;
            Pozicia = new Vector2(pozX, Pozicia.Y);
        }

        public static int DajYHraca()
        {
            double y = 0.32 * Hra.RozlisenieVyska;

            return (int)y;
        }
    }
}
