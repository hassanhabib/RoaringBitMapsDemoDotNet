using System.Diagnostics;
using Roaring.Net.CRoaring;

namespace RoarDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListExample();
            HashsetExample();
            RoaringExample();
        }

        private static void ListExample()
        {
            List<int> usersWhoWatchedVideo = Enumerable.Range(0, 1_000_000).ToList();
            List<int> usersWhoLikedAI = Enumerable.Range(500_000, 1_000_000).ToList();

            Stopwatch stopwatch = Stopwatch.StartNew();

            List<int> result = usersWhoWatchedVideo
                .Where(userId => usersWhoLikedAI.Contains(userId))
                .ToList();

            stopwatch.Stop();

            Console.WriteLine($"List<int> Result Count: {result.Count}");
            Console.WriteLine($"List<int> Time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void HashsetExample()
        {
            HashSet<int> usersWhoWatchedVideo = Enumerable.Range(0, 1_000_000).ToHashSet();
            HashSet<int> usersWhoLikedAI = Enumerable.Range(500_000, 1_000_000).ToHashSet();

            Stopwatch stopwatch = Stopwatch.StartNew();

            usersWhoWatchedVideo.IntersectWith(usersWhoLikedAI);

            stopwatch.Stop();

            Console.WriteLine($"HashSet<int> Result Count: {usersWhoWatchedVideo.Count}");
            Console.WriteLine($"HashSet<int> Time: {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void RoaringExample()
        {
            using Roaring32Bitmap usersWhoWatchedVideo = new();
            using Roaring32Bitmap usersWhoLikedVideo = new();

            usersWhoWatchedVideo.AddMany(Enumerable.Range(0, 1_000_000)
                .Select(x => (uint)x).ToArray());

            usersWhoLikedVideo.AddMany(Enumerable.Range(500_000, 1_000_000)
                .Select(x => (uint)x).ToArray());

            usersWhoWatchedVideo.Optimize();
            usersWhoLikedVideo.Optimize();

            Stopwatch stopwatch = Stopwatch.StartNew();

            using Roaring32Bitmap result = usersWhoWatchedVideo.And(usersWhoLikedVideo);

            stopwatch.Stop();

            Console.WriteLine($"Roaring Bitmap Result Count: {result.GetStatistics().Count}");
            Console.WriteLine($"RoaringBitmap Time: {stopwatch.Elapsed.TotalMicroseconds} microsecond");
        }
    }
}
