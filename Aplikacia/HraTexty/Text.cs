using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Aplikacia.Interface;
using System;
using System.Collections.Generic;

namespace Aplikacia.HraTexty
{
    public class Text : IVykreslovane
    {
        private Vector2 Skalovanie;
        public string TextSlova { get; set; }
        private readonly SpriteFont Font;
        private readonly List<Pismeno> Pismena;
        private readonly string[] HerneSlova;
        private readonly Random Randomizator;
        private readonly Textury Textury_;
        private int AktualnePismeno = 0;
        private int KodHladanehoPismena = 0;
        public int PocetPosunovHraca { get; private set; } = 0;
        public int Zivoty { get; private set; }
        private readonly List<Keys> StlaceneKlavesy = [];
        private readonly List<Keys> PusteneKlavesy = [];
        private readonly List<Keys> KlavesyNaVyhodenie = [];



        public Text(Textury textury, SpriteFont font, string[] herneSlova, int zivoty)
        {
            Textury_ = textury;
            Font = font;
            Zivoty = zivoty;
            Pismena = [];
            HerneSlova = herneSlova;
            Randomizator = new Random();
            TextSlova = NacitajDalsieNahodneSlovo();
        }

        public string NacitajDalsieNahodneSlovo()
        {
            var nahodne = Randomizator.Next(HerneSlova.Length - 1);
            var dalsieSlovo = HerneSlova[nahodne];
            TextSlova = dalsieSlovo;
            KodHladanehoPismena = TextSlova[0];
            AktualnePismeno = 0;

            Pismena.Clear();
            float dalsieX = 10;
            int i = 0;
            foreach (var znak in TextSlova)
            {
                string z = znak.ToString().ToUpper();
                Pismena.Add(new Pismeno(Textury_, z, new Vector2(dalsieX, 0)));
                dalsieX += Font.MeasureString(Pismena[i].Pismeno_).X + Font.Spacing;
                i++;
            }
            if (Pismena.Count > 0)
            {
                Pismena[0].JeHladane = true;
            }

            var (x, y) = GetSkalovanie();
            Skalovanie = new Vector2(x, y);
            return dalsieSlovo;
        }

        public void Aktualizuj(GameTime hernyCas)
        {
            foreach (var klavesa in Keyboard.GetState().GetPressedKeys())
            {
                if (!StlaceneKlavesy.Contains(klavesa))
                    StlaceneKlavesy.Add(klavesa);
            }
            KlavesyNaVyhodenie.Clear();
            foreach (var klavesa in StlaceneKlavesy)
            {
                if (Keyboard.GetState().IsKeyUp(klavesa))
                {
                    PusteneKlavesy.Add(klavesa);
                    KlavesyNaVyhodenie.Add(klavesa);
                }
            }
            foreach (var klavesa in KlavesyNaVyhodenie)
            {
                StlaceneKlavesy.Remove(klavesa);
            }

            KlavesyNaVyhodenie.Clear();
            if (PusteneKlavesy.Count > 0)
            {
                foreach (var klavesa in PusteneKlavesy)
                {
                    if ((int)klavesa == KodHladanehoPismena)
                    {
                        Pismena[AktualnePismeno].JeHladane = false;
                        Pismena[AktualnePismeno].Najdene = true;
                        PocetPosunovHraca++;
                        AktualnePismeno++;
                        if (AktualnePismeno == TextSlova.Length)
                        {
                            TextSlova = NacitajDalsieNahodneSlovo();
                        }
                        else
                        {
                            Pismena[AktualnePismeno].JeHladane = true;
                        }
                        KodHladanehoPismena = Pismena[AktualnePismeno].Pismeno_[0];
                        KlavesyNaVyhodenie.Add(klavesa);
                    }
                    else
                    {
                        Zivoty--;
                        if (Zivoty == 0)
                        {
                            return;
                        }
                        KlavesyNaVyhodenie.Add(klavesa);
                    }
                }
            }

            foreach (var klavesa in KlavesyNaVyhodenie)
            {
                PusteneKlavesy.Remove(klavesa);
            }
        }

        public void Vykresli(GameTime hernyCas, SpriteBatch nastrojVykreslovanie)
        {
            for (int i = 0; i < Pismena.Count; i++)
            {
                Pismena[i].Vykresli(nastrojVykreslovanie, Font, Skalovanie);
            }

            for (int i = 0; i < Zivoty; i++)
            {
                nastrojVykreslovanie.Draw(Textury_.TexturaZivot, new Rectangle(Textury_.PrvyZivotX + (Textury_.SirkaZivota * i), Textury_.PrvyZivotY,
                    Textury_.SirkaZivota, Textury_.VyskaZivota), Color.White);
            }
        }

        private static (float x, float y) GetSkalovanie()
        {
            double y = (0.11 * Hra.RozlisenieVyska) / 120;
            return (1, (float)y);
        }
    }
}
