using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Players;

public static class KartySzans
{
	private static readonly Random rng = new Random();
	private const int Idz_pozycja = 0;
	private const int Pieniadze_Bank = 100;
	private const int Bonus = 50;

	private static List<Func<Player, string>> listOfKartySzans = new List<Func<Player, string>>
	{
		BankDajeCiPieniadze,
		DostajeszBonusOdBanku,
		DajPieniadzeGraczowi
	};
	
	private static string BankDajeCiPieniadze (Player player)
    {
		player.IncrementMoney(Pieniadze_Bank);
		return "Bank daje ci 100 pieniążków.";
    }

	private static string DostajeszBonusOdBanku(Player player)
    {
		player.IncrementMoney(DostajeszBonusOdBanku);
		return "Wygrałeś w loterii. Dostajesz 50 pieniążków.";
	}

	private static string DajPieniadzeGraczowi(Player player)
    {
		Board.players[Board.CurrentPlayerIndex].DecrementMoney(30);
		Board.players[(Board.CurrentPlayerIndex + 1) % 2].IncrementMoney(30);
		return "Inny gracz jest mądrzejszy. \nDaj mu 30 pieniążków.";
    }
	
	public static string GenratorRandomCard(Player player)
    {
		listOfKartySzans = new listOfKartySzans
			.OrderBy(x => rng.Next())
		    .ToList();
		Func<Player, string> randomKartySzans = listOfKartySzans[rng.Next(0, 3)];
		return randomKartySzans.Invoke(player);

	}
}
