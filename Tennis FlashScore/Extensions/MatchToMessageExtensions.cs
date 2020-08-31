using FlashScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_FlashScore.Extensions
{
    public static class MatchToMessageExtensions
    {
        public static string ToMessage(this Match m,bool UseLink,int count,double total)
        {
            if(UseLink)
            {
                if (MatchH2H.IsBigger(count, total, m.H2HMatches))
                {
                    return $"Лига {m.League}. Матч {m.FirstPlayer} - {m.SecondPlayer}. Тотал больше {total}. Ссылка - {m.Link}";
                }
                return $"Лига {m.League}. Матч {m.FirstPlayer} - {m.SecondPlayer}. Тотал меньше {total}. Ссылка - {m.Link}";
            }
            else if(MatchH2H.IsBigger(count, total, m.H2HMatches))
            {
                return $"Лига {m.League}. Матч {m.FirstPlayer} - {m.SecondPlayer}. Тотал больше {total}.";
            }
            return $"Лига {m.League}. Матч {m.FirstPlayer} - {m.SecondPlayer}. Тотал меньше {total}.";
        }
    }
}
