using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib;
using mjlib.HandCalculating;
using mjlib.Tiles;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static mjlib.Constants;
using static mjlib.HandCalculating.FuCalculator;
using static mjlibTest.TestsMixin;

namespace mjlibTest.CalculatingTest
{
    [TestClass]
    public class FuCalculationTest
    {
        [TestMethod]
        public void ChitoitsuFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "115599", pin: "6", sou: "112244");
            var winTile = Tile.Parse(pin: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(1, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(25, BASE)));
            AreEqual(fu, 25);
        }

        [TestMethod]
        public void OpenHandBaseTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.Pon,sou:"222")
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, OPEN_PON)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void FuBasedOnWinGroup()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "234789", pin: "1234566");
            var winTile = Tile.Parse(pin: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var winGroups = hand.Where(x => x.Contains(winTile.ToTileKind())).ToList();

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                winGroups[0],
                config);
            AreEqual(30, fu);

            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                winGroups[1],
                config);
            AreEqual(40, fu);
        }

        [TestMethod]
        public void OpenHandWithoutAdditionalFu()
        {
            var config = new HandConfig(options: new OptionalRules(fuForOpenPinfu: false));

            var tiles = TileList.Parse(man: "234567", pin: "22", sou: "23478");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.Chi,sou:"234")
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(1, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            AreEqual(fu, 20);
        }

        [TestMethod]
        public void TsumoHandBaseTest()
        {
            var config = new HandConfig(isTsumo: true);

            var tiles = TileList.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, _) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
        }

        [TestMethod]
        public void TsumoHandAndPinfuTest()
        {
            var config = new HandConfig(isTsumo: true,
                options: new OptionalRules(fuForPinfuTsumo: true));
            var tiles = TileList.Parse(man: "123456", pin: "123", sou: "2278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(1, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            AreEqual(fu, 20);
        }

        [TestMethod]
        public void TsumoAndDisabledPinfuTest()
        {
            var config = new HandConfig(isTsumo: true,
                options: new OptionalRules(fuForPinfuTsumo: false));

            var tiles = TileList.Parse(man: "123456", pin: "123", sou: "2278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, TSUMO)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void TsumoHandAndNotPinfu()
        {
            var config = new HandConfig(isTsumo: true);

            var tiles = TileList.Parse(man: "123456", pin: "111", sou: "2278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, TSUMO)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        // 喰い平和加符あり
        public void PenchanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "55", sou: "12456");
            var winTile = Tile.Parse(sou: "3");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, PENCHAN)));
            AreEqual(fu, 40);

            tiles = TileList.Parse(man: "123456", pin: "55", sou: "34589");
            winTile = Tile.Parse(sou: "7");
            hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, PENCHAN)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void KanChanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "55", sou: "12357");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, KANCHAN)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void ValuedPairFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", sou: "12378", honor: "11");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var valuedTiles = new List<int> { EAST };
            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                valuedTiles: valuedTiles);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, VALUED_PAIR)));
            AreEqual(fu, 40);

            valuedTiles = new List<int> { EAST, EAST };
            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                valuedTiles: valuedTiles);

            AreEqual(3, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, VALUED_PAIR)));
            IsTrue(fuDetails.Contains(new FuDetail(2, VALUED_PAIR)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void PairWaitFu()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "1", sou: "123678");
            var winTile = Tile.Parse(pin: "1");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, PAIR_WAIT)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void ClosedPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(4, CLOSED_PON)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void ClosedTerminalPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "11", sou: "11178");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(8, CLOSED_TERMINAL_PON)));
            AreEqual(fu, 40);

            tiles = TileList.Parse(man: "123456", pin: "11", sou: "11678");
            winTile = Tile.Parse(sou: "1");
            hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(4, OPEN_TERMINAL_PON)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void ClosedHonorPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", sou: "1178", honor: "111");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(8, CLOSED_TERMINAL_PON)));
            AreEqual(fu, 40);

            tiles = TileList.Parse(man: "123456", sou: "11678", honor: "11");
            winTile = Tile.Parse(honors: "1");
            hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());

            (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(4, OPEN_TERMINAL_PON)));
            AreEqual(fu, 40);
        }

        [TestMethod]
        public void OpenPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", sou: "22278", pin: "11");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.Pon,sou:"222")
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(2, OPEN_PON)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void OpenTerminalPonFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", sou: "2278", honor: "111");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.Pon,honors:"111")
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(4, OPEN_TERMINAL_PON)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void ClosedKanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.Kan, sou:"222", isOpen:false)
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(16, CLOSED_KAN)));
            AreEqual(fu, 50);
        }

        [TestMethod]
        public void OpenKanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "11", sou: "22278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.Kan, sou:"222", isOpen:true)
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(8, OPEN_KAN)));
            AreEqual(fu, 30);
        }

        [TestMethod]
        public void ClosedTerminalKanFuTest()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "111", sou: "2278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.Kan, pin:"111", isOpen:false)
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(30, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(32, CLOSED_TERMINAL_KAN)));
            AreEqual(fu, 70);
        }

        [TestMethod]
        public void OpenTerminalKanFu()
        {
            var config = new HandConfig();

            var tiles = TileList.Parse(man: "123456", pin: "111", sou: "2278");
            var winTile = Tile.Parse(sou: "6");
            var hand = Hand(new TileList(tiles.Append(winTile)).ToTiles34());
            var melds = new List<Meld>
            {
                MakeMeld(MeldType.Kan, pin:"111", isOpen:true)
            };

            var (fuDetails, fu) = CalculateFu(hand,
                winTile,
                GetWinGroup(hand, winTile),
                config,
                melds: melds);

            AreEqual(2, fuDetails.Count);
            IsTrue(fuDetails.Contains(new FuDetail(20, BASE)));
            IsTrue(fuDetails.Contains(new FuDetail(16, OPEN_TERMINAL_KAN)));
            AreEqual(fu, 40);
        }

        private static TileKindList GetWinGroup(IList<TileKindList> hand, Tile winTile)
        {
            return hand.Where(x => x.Contains(winTile.ToTileKind())).ToList()[0];
        }
    }
}