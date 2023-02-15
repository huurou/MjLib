using System.Collections.Generic;

namespace mjlib.HandCalculating
{
    public class HandResult
    {
        public Score Score { get; }
        public int Han { get; }
        public int Fu { get; }
        public List<Yaku> Yakus { get; }
        public string? Error { get; }
        public List<FuDetail> FuDetailSet { get; }

        public HandResult(Score? socre = null,
            int han = 0,
            int fu = 0,
            List<Yaku>? yaku = null,
            string? error = null,
            List<FuDetail>? fuDetails = null)
        {
            Score = socre ?? new(0, 0);
            Han = han;
            Fu = fu;
            Yakus = yaku ?? new();
            Error = error;
            FuDetailSet = fuDetails ?? new();
        }
    }
}