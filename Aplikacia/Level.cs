using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Aplikacia.HraTexty;
using Aplikacia.Interface;
using Aplikacia.PozadieHry;
using Aplikacia.Zavodnici;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Aplikacia
{
    public class Level : IVykreslovane
    {
        private readonly Hrac Hrac_;
        private readonly List<Auto> Hraci;
        private readonly Pozadie Pozadie_;
        private readonly Textury Textury_;
        public readonly Obtiaznost Obtiaznost_;
        public Text Slovo;
        public bool HraBezi = false;
        private readonly Stopwatch StopkyOdpocitavanie;
        public Stopwatch StopkyHry { get; private set; } = new Stopwatch();
        public bool PresloOdpocitavanie = false;
        private int Vitaz = -1;
        public bool Vyhra { get; private set; } = false;

        public Level(Hrac hrac, Pozadie pozadie, Textury textury, Obtiaznost obtiaznost)
        {
            Hrac_ = hrac;
            Pozadie_ = pozadie;
            Textury_ = textury;
            Obtiaznost_ = obtiaznost;
            StopkyOdpocitavanie = new Stopwatch();
            Hraci = [];
            Hraci.Add(Hrac_);
            NacitajSlovaAProtihracovPodlaObtiaznosti(Obtiaznost_);
        }


        private void NacitajSlovaAProtihracovPodlaObtiaznosti(Obtiaznost obtiaznost)
        {
            var (min, max) = DajMinAMaxRychlostProtihraca(obtiaznost);
            NacitajProtihracov(min, max);
            switch (obtiaznost)
            {
                case Obtiaznost.Lahka:
                    Slovo = new Text(Textury_, Textury_.Font, NacitajHerneSlovaZoSuboru("Content/slovaLahke.txt"), Hrac_.PocetZivotov);
                    break;
                case Obtiaznost.Stredna:
                    Slovo = new Text(Textury_, Textury_.Font, NacitajHerneSlovaZoSuboru("Content/slovaLahke.txt"), Hrac_.PocetZivotov);
                    break;
                case Obtiaznost.Tazka:
                    Slovo = new Text(Textury_, Textury_.Font, NacitajHerneSlovaZoSuboru("Content/slovaTazke.txt"), Hrac_.PocetZivotov);
                    break;
                default:
                    break;
            }
        }

        private void NacitajProtihracov(int minNahodnehoCisla, int maxNahodnehoCisla)
        {
            Random random = new();
            int protihrac = random.Next(2);
            for (int i = 0; i < 3; i++)
            {
                float rychlostProtihraca = random.Next(minNahodnehoCisla, maxNahodnehoCisla) / (float)100;
                var p = Hraci[^1];
                var poz = p.Pozicia;
                if (i == protihrac)
                    Hraci.Add(new Auto(Textury_.TexturaProtihrac, (float)maxNahodnehoCisla / (float)100, new Vector2(poz.X, p.GetPoziciuDalsiehoAuta())));
                else
                    Hraci.Add(new Auto(Textury_.TexturaProtihrac, rychlostProtihraca, new Vector2(poz.X, p.GetPoziciuDalsiehoAuta())));
            }
        }
        public void Aktualizuj(GameTime hernyCas)
        {
            if (!PresloOdpocitavanie)
            {
                StopkyOdpocitavanie.Start();
                if (StopkyOdpocitavanie.Elapsed.Seconds == 3)
                {
                    StopkyOdpocitavanie.Stop();
                    HraBezi = true;
                    PresloOdpocitavanie = true;
                    StopkyHry.Start();
                }
            }

            if (HraBezi)
            {
                Hrac_.PocetPosunov = Slovo.PocetPosunovHraca;
                Slovo.Aktualizuj(hernyCas);
                foreach (var hrac in Hraci)
                {
                    hrac.Aktualizuj(hernyCas);
                    if (hrac.Pozicia.X > (float)Hra.VitaznaCiaraX)
                    {
                        Vitaz = Hraci.IndexOf(hrac);
                        if (Vitaz == 0)
                        {
                            Vyhra = true;
                        }
                        StopkyHry.Stop();
                        HraBezi = false;
                    }
                    if (Slovo.Zivoty == 0)
                    {
                        StopkyHry.Stop();
                        HraBezi = false;
                    }
                }
            }
        }

        public void Vykresli(GameTime hernyCas, SpriteBatch nastrojVykreslovanie)
        {
            if (!PresloOdpocitavanie)
            {
                var sekunda = (3 - StopkyOdpocitavanie.Elapsed.Seconds).ToString();
                nastrojVykreslovanie.DrawString(Textury_.Font, sekunda, new Vector2((Hra.RozlisenieSirka / 2) - Textury_.Font.MeasureString(sekunda).X,
                    (Hra.RozlisenieVyska / 2) - Textury_.Font.MeasureString(sekunda).Y), Color.White);
            }

            if (HraBezi)
            {
                Pozadie_.Vykresli(nastrojVykreslovanie);
                foreach (var hrac in Hraci)
                {
                    hrac.Vykresli(hernyCas, nastrojVykreslovanie);
                }
                Slovo.Vykresli(hernyCas, nastrojVykreslovanie);
                nastrojVykreslovanie.DrawString(Textury_.Font, StopkyHry.Elapsed.ToString(@"m\:ss"), new Vector2(Textury_.PrvyZivotX,
                Textury_.PrvyZivotY + Textury_.VyskaZivota - 20), Color.White, 0, new Vector2(), new Vector2(1, (float)0.68), new SpriteEffects(), 0);
            }
        }

        private static string[] NacitajHerneSlovaZoSuboru(string cestaSuboru)
        {
            string[] slova = File.ReadAllLines(cestaSuboru);
            return slova;
        }

        private static (int min, int max) DajMinAMaxRychlostProtihraca(Obtiaznost obtiaznost)
        {
            var min = 0;
            var max = 0;
            switch (obtiaznost)
            {
                case Obtiaznost.Lahka:
                    min = (int)(0.005 * Hra.RozlisenieSirka);
                    max = (int)(0.018 * Hra.RozlisenieSirka);
                    return (min, max);
                case Obtiaznost.Stredna:
                    min = (int)(0.01 * Hra.RozlisenieSirka);
                    max = (int)(0.023 * Hra.RozlisenieSirka);
                    return (min, max);
                case Obtiaznost.Tazka:
                    min = (int)(0.01 * Hra.RozlisenieSirka);
                    max = (int)(0.023 * Hra.RozlisenieSirka);
                    return (min, max);
                default:
                    return (min, max);
            }
        }
    }
}
