using Microsoft.VisualStudio.TestTools.UnitTesting;
using mjlib;
using mjlib.HandCalculating;
using mjlib.HandCalculating.YakuList;
using mjlib.Tiles;
using System.Collections.Generic;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static mjlib.Constants;
using static mjlib.MeldType;
using static mjlib.HandCalculating.HandCalculator;
using static mjlibTest.TestsMixin;

namespace mjlibTest.CalculatingTest
{
    [TestClass]
    public class YakuCalculationTest
    {
        [TestMethod]
        public void HandCalculationTest()
        {
            var playerWind = EAST;

            var tiles = TileList.Parse(pin: "112233999", honor: "11177");
            var winTile = Tile.Parse(pin: "9");
            var melds = new List<Meld>
            {
                MakeMeld(Pon, honors:"111"),
                MakeMeld(Chi, pin:"123"),
                MakeMeld(Chi, pin:"123")
            };
            var result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);

            tiles = TileList.Parse(pin: "22244456799", honor: "444");
            winTile = Tile.Parse(pin: "2");
            var doraIndicators = TileList.Parse(sou: "3", honor: "3");
            melds = new List<Meld>
            {
                MakeMeld(Kan, honors:"4444")
            };
            result = EstimateHandValue(tiles, winTile, melds, doraIndicators);
            AreEqual(null, result.Error);
            AreEqual(6, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(2, result.Yakus.Count);

            tiles = TileList.Parse(man: "11", pin: "123345", sou: "678", honor: "666");
            winTile = Tile.Parse(pin: "3");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(40, result.Fu);

            tiles = TileList.Parse(man: "234789", pin: "12345666");
            winTile = Tile.Parse(pin: "6");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(30, result.Fu);

            tiles = TileList.Parse(pin: "34555789", sou: "678", honor: "555");
            winTile = Tile.Parse(pin: "5");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(40, result.Fu);

            tiles = TileList.Parse(man: "678", pin: "88", sou: "123345678");
            winTile = Tile.Parse(sou: "3");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);

            tiles = TileList.Parse(man: "123456", pin: "456", sou: "12399");
            winTile = Tile.Parse(sou: "1");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);

            tiles = TileList.Parse(sou: "111123666789", honor: "11");
            winTile = Tile.Parse(sou: "1");
            melds = new List<Meld> { MakeMeld(Pon, sou: "666") };
            doraIndicators = TileList.Parse(honor: "4");
            result = EstimateHandValue(tiles, winTile, melds, doraIndicators, new HandConfig(playerWind: playerWind));
            AreEqual(40, result.Fu);
            AreEqual(4, result.Han);

            tiles = TileList.Parse(pin: "12333", sou: "567", honor: "666777");
            winTile = Tile.Parse(pin: "3");
            melds = new List<Meld>
            {
                MakeMeld(Pon, honors:"666"),
                MakeMeld(Pon, honors:"777")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileList.Parse(man: "456", pin: "12367778", sou: "678");
            winTile = Tile.Parse(pin: "7");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRiichi: true));
            AreEqual(40, result.Fu);
            AreEqual(1, result.Han);

            tiles = TileList.Parse(man: "11156677899", honor: "777");
            winTile = Tile.Parse(man: "7");
            melds = new List<Meld>
            {
                MakeMeld(Kan, honors:"7777"),
                MakeMeld(Pon, man:"111"),
                MakeMeld(Chi, man:"678")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(40, result.Fu);
            AreEqual(3, result.Han);

            tiles = TileList.Parse(man: "122223777888", honor: "66");
            winTile = Tile.Parse(man: "2");
            melds = new List<Meld>
            {
                MakeMeld(Chi, man:"123"),
                MakeMeld(Pon, man:"777")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileList.Parse(pin: "11144678888", honor: "444");
            winTile = Tile.Parse(pin: "8");
            melds = new List<Meld>
            {
                MakeMeld(Pon, honors:"444"),
                MakeMeld(Pon, pin:"111"),
                MakeMeld(Pon, pin:"888")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileList.Parse(man: "345", pin: "999", sou: "67778", honor: "222");
            winTile = Tile.Parse(sou: "7");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(40, result.Fu);
            AreEqual(1, result.Han);

            tiles = TileList.Parse(man: "345", sou: "33445577789");
            winTile = Tile.Parse(sou: "7");
            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileList.Parse(pin: "112233667788", honor: "22");
            winTile = Tile.Parse(pin: "3");
            melds = new List<Meld> { MakeMeld(Chi, pin: "123") };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileList.Parse(man: "12333456789", sou: "345");
            winTile = Tile.Parse(man: "3");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileList.Parse(sou: "11123456777888");
            winTile = Tile.Parse(sou: "4");
            melds = new List<Meld>
            {
                MakeMeld(Chi, sou:"123"),
                MakeMeld(Pon, sou:"777"),
                MakeMeld(Pon, sou:"888")
            };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isTsumo: true));
            AreEqual(30, result.Fu);
            AreEqual(5, result.Han);

            tiles = TileList.Parse(sou: "112233789", honor: "55777");
            winTile = Tile.Parse(sou: "2");
            melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(40, result.Fu);
            AreEqual(4, result.Han);

            tiles = TileList.Parse(pin: "234777888999", honor: "22");
            winTile = Tile.Parse(pin: "9");
            melds = new List<Meld>
            {
                MakeMeld(Chi, pin:"234"),
                MakeMeld(Chi, pin:"789")
            };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Han);

            tiles = TileList.Parse(man: "444", pin: "77888899", honor: "777");
            winTile = Tile.Parse(pin: "8");
            melds = new List<Meld>
            {
                MakeMeld(Pon, honors:"777"),
                MakeMeld(Pon, man:"444")
            };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isTsumo: true));
            AreEqual(30, result.Fu);
            AreEqual(1, result.Han);

            tiles = TileList.Parse(man: "567", pin: "12333345", honor: "555");
            winTile = Tile.Parse(pin: "3");
            result = EstimateHandValue(tiles, winTile);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Han);

            tiles = TileList.Parse(pin: "34567777889", honor: "555");
            winTile = Tile.Parse(pin: "7");
            melds = new List<Meld> { MakeMeld(Chi, pin: "345") };
            result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(30, result.Fu);
            AreEqual(3, result.Han);

            tiles = TileList.Parse(pin: "567", sou: "333444555", honor: "77");
            winTile = Tile.Parse(sou: "3");
            melds = new List<Meld> { MakeMeld(Kan, isOpen: false, sou: "4444") };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isRiichi: true));
            AreEqual(60, result.Fu);
            AreEqual(1, result.Han);
        }

        [TestMethod]
        public void IsRiichiTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRiichi: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isRiichi: true));
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsTsumoTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            result = EstimateHandValue(tiles, winTile, melds, config: new HandConfig(isTsumo: true));
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsIppatsuTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRiichi: true, isIppatsu: true));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yakus.Count);

            result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRiichi: false, isIppatsu: true));
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsRinshanTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRinshan: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsChankanTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isChankan: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsHaiteiTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isHaitei: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsHouteiTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isHoutei: true));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsRenhouTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isRenhou: true));
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsDaburuRiichiTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "123444");
            var winTile = Tile.Parse(sou: "4");
            var result = EstimateHandValue(tiles, winTile, config: new HandConfig(isDaburuRiichi: true));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsNagashiManganTest()
        {
            var tiles = TileList.Parse(man: "234456", pin: "66", sou: "13579");
            var result = EstimateHandValue(tiles, null, config: new HandConfig(isNagashiMangan: true));
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsChitoitsuHandTest()
        {
            var tiles = Tiles34.Parse(man: "113355", pin: "11", sou: "113355");
            IsTrue(new Chiitoitsu().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "2299", pin: "1199", sou: "2299", honor: "44");
            IsTrue(new Chiitoitsu().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "113355", pin: "11", sou: "113355");
            var winTile = Tile.Parse(pin: "1");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(25, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsTanyaoTest()
        {
            var tiles = Tiles34.Parse(man: "234567", pin: "22", sou: "234567");
            IsTrue(new Tanyao().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "234567", pin: "22", sou: "123456");
            IsFalse(new Tanyao().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "234567", sou: "234567", honor: "22");
            IsFalse(new Tanyao().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "234567", pin: "22", sou: "234567");
            var winTile = Tile.Parse(man: "7");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false, isRiichi: true));
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(3, result.Yakus.Count);

            tiles_ = TileList.Parse(man: "234567", pin: "22", sou: "234567");
            winTile = Tile.Parse(man: "7");
            var melds = new List<Meld> { MakeMeld(Chi, sou: "234") };
            result = EstimateHandValue(
                tiles_, winTile, melds, config: new HandConfig(options: new OptionalRules(hasOpenTanyao: true)));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);

            tiles_ = TileList.Parse(man: "234567", pin: "22", sou: "234567");
            winTile = Tile.Parse(man: "7");
            melds = new List<Meld> { MakeMeld(Chi, sou: "234") };
            result = EstimateHandValue(
                tiles_, winTile, melds, config: new HandConfig(options: new OptionalRules(hasOpenTanyao: false)));
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsPinfuHandTest()
        {
            var playerWind = EAST;
            var roundWind = WEST;

            var tiles = TileList.Parse(man: "123456", pin: "55", sou: "123456");
            var winTile = Tile.Parse(man: "6");
            var result = EstimateHandValue(tiles, winTile);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);

            tiles = TileList.Parse(man: "123456", pin: "55", sou: "123555");
            winTile = Tile.Parse(man: "5");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileList.Parse(man: "123456", pin: "55", sou: "111456");
            winTile = Tile.Parse(man: "6");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileList.Parse(man: "123456", pin: "55", sou: "123456");
            winTile = Tile.Parse(sou: "3");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileList.Parse(man: "123456", pin: "55", sou: "123567");
            winTile = Tile.Parse(sou: "6");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileList.Parse(man: "22456678", pin: "123678");
            winTile = Tile.Parse(man: "2");
            result = EstimateHandValue(tiles, winTile);
            AreNotEqual(null, result.Error);

            tiles = TileList.Parse(man: "123456", sou: "123678", honor: "11");
            winTile = Tile.Parse(sou: "6");
            result = EstimateHandValue(
                tiles, winTile, config: new HandConfig(playerWind: playerWind, roundWind: roundWind));
            AreNotEqual(null, result.Error);

            tiles = TileList.Parse(man: "123456", sou: "123678", honor: "22");
            winTile = Tile.Parse(sou: "6");
            result = EstimateHandValue(
                tiles, winTile, config: new HandConfig(playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);

            tiles = TileList.Parse(man: "123456", pin: "456", sou: "12399");
            winTile = Tile.Parse(man: "1");
            var melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            result = EstimateHandValue(tiles, winTile, melds);
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsIipeikoTest()
        {
            var tiles = Tiles34.Parse(man: "123", pin: "23444", sou: "112233");
            IsTrue(new Iipeiko().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "333", pin: "12344", sou: "112233");
            var winTile = Tile.Parse(man: "3");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsRyanpeikouTest()
        {
            var tiles = Tiles34.Parse(man: "22", pin: "223344", sou: "112233");
            IsTrue(new Ryanpeikou().Valid(Hand(tiles, 1)));

            tiles = Tiles34.Parse(man: "22", sou: "111122223333");
            IsTrue(new Ryanpeikou().Valid(Hand(tiles, 1)));

            tiles = Tiles34.Parse(man: "123", pin: "23444", sou: "112233");
            IsFalse(new Ryanpeikou().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "33", pin: "223344", sou: "112233");
            var winTile = Tile.Parse(pin: "3");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void IsSanshokuTest()
        {
            var tiles = Tiles34.Parse(man: "123", pin: "12345677", sou: "123");
            IsTrue(new Sanshoku().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "23455", pin: "123", sou: "123456");
            IsFalse(new Sanshoku().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "12399", pin: "123", sou: "123456");
            var winTile = Tile.Parse(man: "2");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsSanshokuDoukouTest()
        {
            var tiles = Tiles34.Parse(man: "111", pin: "11145677", sou: "111");
            IsTrue(new SanshokuDoukou().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "222", pin: "33344455", sou: "111");
            IsFalse(new SanshokuDoukou().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "222", pin: "22245699", sou: "222");
            var winTile = Tile.Parse(pin: "9");
            var melds = new List<Meld> { MakeMeld(Pon, sou: "222") };
            var result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsToitoiTest()
        {
            var tiles = Tiles34.Parse(man: "333", pin: "44555", sou: "111333");
            IsTrue(new Toitoi().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(pin: "777888999", sou: "777", honor: "44");
            IsTrue(new Toitoi().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "333", pin: "44555", sou: "111333");
            var winTile = Tile.Parse(pin: "5");
            var melds = new List<Meld>
            {
                MakeMeld(Pon, sou: "111"),
                MakeMeld(Pon, sou: "333")
            };
            var result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            tiles_ = TileList.Parse(pin: "777888999", sou: "777", honor: "44");
            winTile = Tile.Parse(pin: "9");
            melds = new List<Meld> { MakeMeld(Pon, sou: "777") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsSankantsuTest()
        {
            var melds = new List<Meld>
            {
                MakeMeld(Kan, sou: "1111"),
                MakeMeld(Kan, sou: "3333"),
                MakeMeld(Kan, pin: "6666")
            };
            IsTrue(new SanKantsu().Valid(null, new object[] { melds }));

            var tiles = TileList.Parse(man: "123", pin: "44666", sou: "111333");
            var winTile = Tile.Parse(man: "3");
            melds = new List<Meld>
            {
                MakeMeld(MeldType.Chankan, sou: "1111"),
                MakeMeld(Kan, sou: "3333"),
                MakeMeld(Kan, pin: "6666")
            };
            var result = EstimateHandValue(tiles, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(60, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsHonrotoTest()
        {
            var tiles = Tiles34.Parse(man: "111", sou: "111999", honor: "11222");
            IsTrue(new Honroto().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "1199", pin: "11", honor: "22334466");
            IsTrue(new Honroto().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "111", sou: "111999", honor: "11222");
            var winTile = Tile.Parse(honors: "2");
            var melds = new List<Meld> { MakeMeld(Pon, sou: "111") };
            var result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(4, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(2, result.Yakus.Count);

            tiles_ = TileList.Parse(man: "1199", pin: "11", honor: "22334466");
            winTile = Tile.Parse(man: "1");
            result = EstimateHandValue(tiles_, winTile);
            AreEqual(4, result.Han);
            AreEqual(25, result.Fu);
        }

        [TestMethod]
        public void IsSanankouTest()
        {
            var tiles = Tiles34.Parse(man: "333", pin: "44555", sou: "111444");
            var winTile = Tile.Parse(sou: "4");
            var melds = new List<Meld>
            {
                MakeMeld(Pon, sou: "111"),
                MakeMeld(Pon, sou: "444")
            };
            IsFalse(new Sanankou().Valid(Hand(tiles), new object[] { winTile, melds, false }));

            melds = new List<Meld> { MakeMeld(Pon, sou: "111") };
            IsFalse(new Sanankou().Valid(Hand(tiles), new object[] { winTile, melds, false }));
            IsTrue(new Sanankou().Valid(Hand(tiles), new object[] { winTile, melds, true }));

            tiles = Tiles34.Parse(pin: "444789999", honor: "22333");
            winTile = Tile.Parse(pin: "9");
            IsTrue(new Sanankou().Valid(Hand(tiles), new object[] { winTile, new List<Meld>(), false }));

            melds = new List<Meld> { MakeMeld(Chi, pin: "456") };
            tiles = Tiles34.Parse(pin: "222456666777", honor: "77");
            winTile = Tile.Parse(pin: "6");
            IsFalse(new Sanankou().Valid(Hand(tiles), new object[] { winTile, melds, false }));

            var tiles_ = TileList.Parse(man: "333", pin: "44555", sou: "123444");
            melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            winTile = Tile.Parse(pin: "5");
            var result = EstimateHandValue(tiles_, winTile, melds, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsShosangenTest()
        {
            var tiles = Tiles34.Parse(man: "345", sou: "123", honor: "55666777");
            IsTrue(new Shosangen().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "345", sou: "123", honor: "55666777");
            var winTile = Tile.Parse(honors: "7");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(4, result.Han);
            AreEqual(50, result.Fu);
            AreEqual(3, result.Yakus.Count);
        }

        [TestMethod]
        public void IsChantaTest()
        {
            var tiles = Tiles34.Parse(man: "123789", sou: "123", honor: "22333");
            IsTrue(new Chanta().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "111999", sou: "111", honor: "22333");
            IsFalse(new Chanta().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "111999", sou: "111999", pin: "11999");
            IsFalse(new Chanta().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "123789", sou: "123", honor: "22333");
            var winTile = Tile.Parse(honors: "3");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, sou: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsJunchanTest()
        {
            var tiles = Tiles34.Parse(man: "123789", pin: "12399", sou: "789");
            IsTrue(new Junchan().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "111999", sou: "111", honor: "22333");
            IsFalse(new Junchan().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "111999", pin: "11999", sou: "111999");
            IsFalse(new Junchan().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "123789", pin: "12399", sou: "789");
            var winTile = Tile.Parse(man: "2");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, sou: "789") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsHonitsuTest()
        {
            var tiles = Tiles34.Parse(man: "123456789", honor: "11122");
            IsTrue(new Honitsu().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "123456789", pin: "123", honor: "22");
            IsFalse(new Honitsu().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "12345666778899");
            IsFalse(new Honitsu().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "123455667", honor: "11122");
            var winTile = Tile.Parse(honors: "2");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, man: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsChinitsuTest()
        {
            var tiles = Tiles34.Parse(man: "12345666778899");
            IsTrue(new Chinitsu().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "123456778899", honor: "22");
            IsFalse(new Chinitsu().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "11234567677889");
            var winTile = Tile.Parse(man: "1");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(6, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, man: "678") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsIttsuTest()
        {
            var tiles = Tiles34.Parse(man: "123456789", sou: "123", honor: "22");
            IsTrue(new Ittsu().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "112233456789", honor: "22");
            IsTrue(new Ittsu().Valid(Hand(tiles)));

            tiles = Tiles34.Parse(man: "122334567789", honor: "11");
            IsFalse(new Ittsu().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "123456789", sou: "123", honor: "22");
            var winTile = Tile.Parse(sou: "3");
            var result = EstimateHandValue(tiles_, winTile);
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            var melds = new List<Meld> { MakeMeld(Chi, man: "123") };
            result = EstimateHandValue(tiles_, winTile, melds);
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsHakuTest()
        {
            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honor: "555");
            IsTrue(new Haku().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "23422", sou: "234567", honor: "555");
            var winTile = Tile.Parse(honors: "5");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false, isRiichi: false));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsHatsuTest()
        {
            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honor: "666");
            IsTrue(new Hatsu().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "23422", sou: "234567", honor: "666");
            var winTile = Tile.Parse(honors: "6");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false, isRiichi: false));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsChunTest()
        {
            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honor: "777");
            IsTrue(new Chun().Valid(Hand(tiles)));

            var tiles_ = TileList.Parse(man: "23422", sou: "234567", honor: "777");
            var winTile = Tile.Parse(honors: "7");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(isTsumo: false, isRiichi: false));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);
        }

        [TestMethod]
        public void IsEastTest()
        {
            var (playerWind, roundWind) = (EAST, WEST);

            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honor: "111");
            IsTrue(new YakuhaiEast().Valid(Hand(tiles), new object[] { playerWind, roundWind }));

            var tiles_ = TileList.Parse(man: "23422", sou: "234567", honor: "111");
            var winTile = Tile.Parse(honors: "1");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            roundWind = EAST;
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yakus.Count);
        }

        [TestMethod]
        public void IsSouthTest()
        {
            var (playerWind, roundWind) = (SOUTH, EAST);

            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honor: "222");
            IsTrue(new YakuhaiSouth().Valid(Hand(tiles), new object[] { playerWind, roundWind }));

            var tiles_ = TileList.Parse(man: "23422", sou: "234567", honor: "222");
            var winTile = Tile.Parse(honors: "2");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            roundWind = SOUTH;
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yakus.Count);
        }

        [TestMethod]
        public void IsWestTest()
        {
            var (playerWind, roundWind) = (WEST, EAST);

            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honor: "333");
            IsTrue(new YakuhaiWest().Valid(Hand(tiles), new object[] { playerWind, roundWind }));

            var tiles_ = TileList.Parse(man: "23422", sou: "234567", honor: "333");
            var winTile = Tile.Parse(honors: "3");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            roundWind = WEST;
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yakus.Count);
        }

        [TestMethod]
        public void IsNorthTest()
        {
            var (playerWind, roundWind) = (NORTH, EAST);

            var tiles = Tiles34.Parse(man: "23422", sou: "234567", honor: "444");
            IsTrue(new YakuhaiNorth().Valid(Hand(tiles), new object[] { playerWind, roundWind }));

            var tiles_ = TileList.Parse(man: "23422", sou: "234567", honor: "444");
            var winTile = Tile.Parse(honors: "4");
            var result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(1, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(1, result.Yakus.Count);

            roundWind = NORTH;
            result = EstimateHandValue(tiles_, winTile, config: new HandConfig(
                isTsumo: false, isRiichi: false, playerWind: playerWind, roundWind: roundWind));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yakus.Count);
        }

        [TestMethod]
        public void DoraInHandTest()
        {
            var tiles = TileList.Parse(man: "456789", sou: "345678", honor: "55");
            var winTile = Tile.Parse(sou: "5");
            var doraIndicators = TileList.Parse(sou: "5");
            var melds = new List<Meld> { MakeMeld(Chi, sou: "678") };
            var result = EstimateHandValue(tiles, winTile, melds, doraIndicators);
            AreNotEqual(null, result.Error);

            tiles = TileList.Parse(man: "123456", pin: "33", sou: "123456");
            winTile = Tile.Parse(man: "6");
            doraIndicators = TileList.Parse(pin: "2");
            result = EstimateHandValue(tiles, winTile, doraIndicators: doraIndicators);
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Yakus.Count);

            tiles = TileList.Parse(man: "22456678", pin: "123678");
            winTile = Tile.Parse(man: "2");
            doraIndicators = TileList.Parse(man: "1", pin: "2");
            result = EstimateHandValue(
                tiles, winTile, doraIndicators: doraIndicators, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(4, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Yakus.Count);

            tiles = TileList.Parse(man: "678", pin: "34577", sou: "123345");
            winTile = Tile.Parse(pin: "7");
            doraIndicators = TileList.Parse(sou: "44");
            result = EstimateHandValue(
                tiles, winTile, doraIndicators: doraIndicators, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(3, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Yakus.Count);

            tiles = TileList.Parse(man: "678", pin: "345", sou: "123345", honor: "66");
            winTile = Tile.Parse(pin: "5");
            doraIndicators = TileList.Parse(honor: "55");
            result = EstimateHandValue(
                tiles, winTile, doraIndicators: doraIndicators, config: new HandConfig(isRiichi: true));
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yakus.Count);

            tiles = TileList.Parse(man: "123678", pin: "44", sou: "12346");
            winTile = Tile.Parse(pin: "4");
            tiles.Add(new Tile(FIVE_RED_SOU));
            doraIndicators = TileList.Parse(man: "1", pin: "2");
            result = EstimateHandValue(
                tiles, winTile, doraIndicators: doraIndicators, config: new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(2, result.Han);
            AreEqual(30, result.Fu);
            AreEqual(2, result.Yakus.Count);

            tiles = TileList.Parse(man: "777", pin: "34577", sou: "123345");
            winTile = Tile.Parse(pin: "7");
            melds = new List<Meld> { MakeMeld(Kan, man: "7777", isOpen: false) };
            doraIndicators = TileList.Parse(man: "6");
            result = EstimateHandValue(
                tiles, winTile, melds, doraIndicators, new HandConfig(isTsumo: true));
            AreEqual(null, result.Error);
            AreEqual(5, result.Han);
            AreEqual(40, result.Fu);
            AreEqual(2, result.Yakus.Count);
        }

        [TestMethod]
        public void IsAgariAndClosedKanTest()
        {
            var tiles = TileList.Parse(man: "45666777", pin: "111", honor: "222");
            var winTile = Tile.Parse(man: "4");
            var melds = new List<Meld>
            {
                MakeMeld(Pon, pin: "111"),
                MakeMeld(Kan, man: "6666", isOpen: false),
                MakeMeld(Pon, man: "777")
            };
            var result = EstimateHandValue(tiles, winTile, melds);
            AreNotEqual(null, result.Error);
        }

        [TestMethod]
        public void KazoeSettingsTest()
        {
            var tiles = TileList.Parse(man: "22244466677788");
            var winTile = Tile.Parse(man: "7");
            var melds = new List<Meld> { MakeMeld(Kan, man: "2222", isOpen: false) };
            var doraIndicators = TileList.Parse(man: "1111");
            var config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: Kazoe.Limited));
            var result = EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            AreEqual(28, result.Han);
            AreEqual(32000, result.Score.Main);

            config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: Kazoe.Sanbaiman));
            result = EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            AreEqual(28, result.Han);
            AreEqual(24000, result.Score.Main);

            config = new HandConfig(isRiichi: true, options: new OptionalRules(kazoeLimit: Kazoe.Nolimit));
            result = EstimateHandValue(tiles, winTile, melds, doraIndicators, config);
            AreEqual(28, result.Han);
            AreEqual(64000, result.Score.Main);
        }

        [TestMethod]
        public void OpenHandWithoutAdditionalFuTest()
        {
            var tiles = TileList.Parse(man: "234567", pin: "22", sou: "234678");
            var winTile = Tile.Parse(sou: "6");
            var melds = new List<Meld> { MakeMeld(Chi, sou: "234") };
            var config = new HandConfig(options: new OptionalRules(hasOpenTanyao: true, fuForOpenPinfu: false));
            var result = EstimateHandValue(tiles, winTile, melds, config: config);
            AreEqual(1, result.Han);
            AreEqual(20, result.Fu);
            AreEqual(700, result.Score.Main);
        }

        [TestMethod]
        public void AkaDoraTest()
        {
            var tiles = TileList.Parse(man: "12355599", pin: "456", sou: "345", hasAkaDora: false);
            var winTile = Tile.Parse(man: "9");
            var handConfig = new HandConfig(isTsumo: true, options: new OptionalRules(hasAkaDora: true));
            var handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(4, handCalculation.Han);

            tiles = TileList.Parse(man: "12355599", pin: "456", sou: "345", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(1, handCalculation.Han);

            tiles = TileList.Parse(man: "12355599", pin: "456", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(2, handCalculation.Han);

            tiles = TileList.Parse(man: "12355599", pin: "4r6", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(3, handCalculation.Han);

            tiles = TileList.Parse(man: "123r5599", pin: "4r6", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(4, handCalculation.Han);

            tiles = TileList.Parse(man: "123rr599", pin: "4r6", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(5, handCalculation.Han);

            tiles = TileList.Parse(man: "123rrr99", pin: "4r6", sou: "34r", hasAkaDora: true);
            handCalculation = EstimateHandValue(tiles, winTile, config: handConfig);
            AreEqual(null, handCalculation.Error);
            AreEqual(6, handCalculation.Han);
        }
    }
}