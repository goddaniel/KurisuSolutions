﻿using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace Activator.Spells.Health
{
    class yorickreviveally : spell
    {
        internal override string Name
        {
            get { return "yorickreviveally"; }
        }

        internal override string DisplayName
        {
            get { return "Omen of Death | R"; }
        }

        internal override float Range
        {
            get { return 900f; }
        }

        internal override MenuType[] Category
        {
            get { return new[] { MenuType.SelfLowHP }; }
        }

        internal override int DefaultHP
        {
            get { return 20; }
        }

        internal override int DefaultMP
        {
            get { return 0; }
        }

        public override void OnTick(EventArgs args)
        {
            if (!Menu.Item("use" + Name).GetValue<bool>())
                return;

            foreach (var hero in champion.Heroes)
            {
                if (hero.Player.Distance(Player.ServerPosition) > Range)
                    return;

                if (Player.HasBuffOfType(BuffType.Invulnerability))
                    return;

                if (hero.Player.Health / hero.Player.MaxHealth * 100 <=
                    Menu.Item("SelfLowHP" + Name + "Pct").GetValue<Slider>().Value)
                {
                    if (hero.IncomeDamage > 0)
                    {
                        UseSpellOn(hero.Player);
                        RemoveSpell();
                    }
                }
            }
        }
    }
}
