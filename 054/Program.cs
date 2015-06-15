using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using Tools;


namespace _054
{
    class Program
    {
        const string FILENAME = "../../poker.txt";

        static void Main(string[] args)
        {
            var handStrings = File.ReadAllLines(FILENAME);
            Decorators.Benchmark(Solve, handStrings);
        }

        private static int Solve(string[] handStrings)
        {
            return handStrings
                .AsParallel()
                .Select(CombinationsFromString)
                .Count(combinations => combinations.Item1 > combinations.Item2);
        }

        private static Tuple<Combination, Combination>
            CombinationsFromString(string handString)
        {
            var cards = handString
                .Split()
                .Select(card => new Card(card))
                .ToArray();
            var hands = new[]
            {
                cards.Take(5).ToArray(),
                cards.Skip(5).ToArray(),
            };

            return new Tuple<Combination, Combination>(
                new Combination(hands[0]),
                new Combination(hands[1])
            );
        }

        public enum HandRank
        {
            HighCard,
            OnePair,
            TwoPairs,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush,
        }

        public enum CardValue
        {
            Ten = 10,
            Jack,
            Queen,
            King,
            Ace,
        }

        public enum CardSuit
        {
            Clubs = 'C',
            Diamonds = 'D',
            Hearts = 'H',
            Spades = 'S',
        }

        private static readonly ReadOnlyDictionary<char, CardValue> ValuesDictionary =
            new ReadOnlyDictionary<char, CardValue>(
                new Dictionary<char, CardValue>()
                {
                    { 'T', CardValue.Ten },
                    { 'J', CardValue.Jack },
                    { 'Q', CardValue.Queen },
                    { 'K', CardValue.King },
                    { 'A', CardValue.Ace },
                }
            );


        public struct Card :
            IComparable<Card>,
            IEquatable<Card>
        {
            public CardValue Value { get; private set; }
            public CardSuit Suit { get; private set; }

            public Card(string card)
                : this()
            {
                CardValue value;
                if (card[0] <= 0x39)
                {
                    value = (CardValue) card[0] - '0';
                }
                else
                {
                    value = ValuesDictionary[card[0]];
                }
                Value = value;

                Suit = (CardSuit)card[1];
            }

            public override string ToString()
            {
                return String.Format("{0} of {1}", Value, Suit);
            }

            public int CompareTo(Card other)
            {
                return Value.CompareTo(other.Value);
            }

            public bool Equals(Card other)
            {
                return Value == other.Value && Suit == other.Suit;
            }

            public static bool operator ==(Card a, Card b)
            {
                return a.Equals(b);
            }

            public static bool operator !=(Card a, Card b)
            {
                return !(a == b);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is Card && Equals((Card)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((int)Value * 397) ^ (int)Suit;
                }
            }
        }

        public struct Combination : IComparable<Combination>
        {
            public HandRank Rank { get; private set; }
            public Card[] Hand { get; private set; }
            public Card[] MainCards { get; private set; }
            public Card[] SupplementalCards { get; private set; }
            public Card[] Kicker { get; private set; }

            public Combination(Card[] hand)
                : this()
            {
                if (hand.Length != 5)
                {
                    throw new ArgumentException("There must be exactly 5 cards in hand");
                }

                Array.Sort(hand, (cardA, cardB) => -cardA.CompareTo(cardB));
                Hand = hand;

                bool isStraight = true;

                // стрит -- каждая карта следует по старшинству за предыдущей (младше)
                for (int i = 1; i < hand.Length && isStraight; i++)
                {
                    isStraight &= hand[i - 1].Value - hand[i].Value == 1;
                }

                // группируем карты по масти и достоинству
                var suites = hand.GroupBy(card => card.Suit);
                var values = hand.GroupBy(card => card.Value);

                // одна масть -- флеш, стрит флеш или роял флеш
                if (suites.Count() == 1)
                {
                    // стрит?
                    if (isStraight)
                    {
                        // стрит и старший туз -- роял флеш
                        if (hand.Last().Value == CardValue.Ace)
                        {
                            SetComb(HandRank.RoyalFlush, hand);
                            return;
                        }
                        // иначе -- просто стрит флеш
                        else
                        {
                            SetComb(HandRank.StraightFlush, hand);
                            return;
                        }
                    }
                    // не стрит, но масть одна -- просто флеш
                    SetComb(HandRank.Flush, hand);
                    return;
                }

                // мастей нескольно, но карты идут по порядку -- просто стрит
                if (isStraight)
                {
                    SetComb(HandRank.Straight, hand);
                    return;
                }

                var valuesDict = values.ToDictionary(
                        valuesGroup => valuesGroup.Key,
                        valuesGroup => valuesGroup.ToArray()
                    );

                // смотрим, есть ли каре
                var fours = valuesDict
                    .Where(pair => pair.Value.Length == 4)
                    .Select(pair => pair.Value)
                    .ToArray();
                if (fours.Length == 1)
                {
                    SetComb(HandRank.FourOfAKind, fours[0]);
                    return;
                }

                // смотрим, есть ли тройки...
                var set = valuesDict
                    .Where(pair => pair.Value.Length == 3)
                    .Select(pair => pair.Value)
                    .ToArray();
                // и пары
                var pairs = valuesDict
                    .Where(pair => pair.Value.Length == 2)
                    .Select(pair => pair.Value)
                    .ToArray();

                // если нашли тройку
                if (set.Length == 1)
                {
                    // смотрим, есть ли пара
                    // если есть -- фуллхаус
                    if (pairs.Length == 1)
                    {
                        SetComb(HandRank.FullHouse, set[0], pairs[0]);
                        return;
                    }
                    // если нет -- сет
                    else
                    {
                        SetComb(HandRank.ThreeOfAKind, set[0]);
                        return;
                    }
                }

                // смотрим, сколько пар
                if (pairs.Length == 2)
                {
                    // две пары
                    // из двух пар основная -- старшая
                    Array.Sort(pairs, (cardsA, cardsB) =>
                        -cardsA[0].CompareTo(cardsB[0]));

                    SetComb(HandRank.TwoPairs, pairs[0], pairs[1]);
                    return;
                }
                else if (pairs.Length == 1)
                {
                    // пара
                    SetComb(HandRank.OnePair, pairs[0]);
                    return;
                }
                else
                {
                    // старшая карта
                    SetComb(HandRank.HighCard, hand);
                    return;
                }
            }

            private void SetComb(
                HandRank rank,
                Card[] mainCards,
                Card[] supplementalCards = null)
            {
                if (ReferenceEquals(supplementalCards, null))
                {
                    supplementalCards = new Card[0];
                }

                var kicker = new Card[0];

                // если карт в двух основных наборах не хватает -- значит, есть кикер
                if (mainCards.Length + supplementalCards.Length < 5)
                {
                    var handSet = new HashSet<Card>(Hand);
                    handSet.ExceptWith(mainCards);
                    handSet.ExceptWith(supplementalCards);

                    kicker = handSet.ToArray();
                    Array.Sort(kicker, (cardA, cardB) => -cardA.CompareTo(cardB));
                }

                Array.Sort(mainCards, (cardA, cardB) => -cardA.CompareTo(cardB));
                Array.Sort(supplementalCards, (cardA, cardB) => -cardA.CompareTo(cardB));

                Rank = rank;
                MainCards = mainCards;

                SupplementalCards = supplementalCards;
                Kicker = kicker;
            }

            public int CompareTo(Combination other)
            {
                if (Rank != other.Rank)
                {
                    return Rank.CompareTo(other.Rank);
                }

                for (int i = 0; i < MainCards.Length; i++)
                {
                    if (MainCards[i] != other.MainCards[i])
                    {
                        return MainCards[i].CompareTo(other.MainCards[i]);
                    }
                }

                for (int i = 0; i < SupplementalCards.Length; i++)
                {
                    if (SupplementalCards[i] != other.SupplementalCards[i])
                    {
                        return SupplementalCards[i]
                            .CompareTo(other.SupplementalCards[i]);
                    }
                }

                for (int i = 0; i < Kicker.Length; i++)
                {
                    if (Kicker[i] != other.Kicker[i])
                    {
                        return Kicker[i].CompareTo(other.Kicker[i]);
                    }
                }

                return 0;
            }

            public static bool operator >(Combination a, Combination b)
            {
                return a.CompareTo(b) > 0;
            }

            public static bool operator <(Combination a, Combination b)
            {
                return a.CompareTo(b) < 0;
            }

            public override string ToString()
            {
                return String.Format("{0} on {1}", Rank, MainCards[0]);
            }
        }
    }
}
