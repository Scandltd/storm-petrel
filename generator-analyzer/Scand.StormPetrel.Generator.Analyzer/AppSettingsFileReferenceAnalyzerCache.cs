using System;
using System.Collections.Generic;
using System.Threading;

namespace Scand.StormPetrel.Generator.Analyzer
{
    internal class AppSettingsFileReferenceAnalyzerCache
    {
        private static long _cacheClearedOnUtcTicks = long.MinValue;
        private readonly Func<long> _ticksProvider;
        private readonly IDictionary<string, string> _cache;
        /// <summary>
        /// Thread safe as soon as parameters are thread safe.
        /// </summary>
        /// <param name="ticksProvider"></param>
        /// <param name="cache"></param>
        public AppSettingsFileReferenceAnalyzerCache(Func<long> ticksProvider, IDictionary<string, string> cache)
        {
            _ticksProvider = ticksProvider;
            _cache = cache;
        }
        public void AddOrUpdate(string key, string value)
            => _cache[key] = value;
        public void Clear()
        {
            Interlocked.Exchange(ref _cacheClearedOnUtcTicks, _ticksProvider());
            _cache.Clear();
        }
        public bool IsExpired()
        {
            var cacheClearedOnUtcTicks = Interlocked.Read(ref _cacheClearedOnUtcTicks);
            var utcNowTicks = _ticksProvider();
            return TimeSpan.FromTicks(utcNowTicks - cacheClearedOnUtcTicks) > TimeSpan.FromSeconds(3);
        }
        public bool TryGetValue(string key, out string value)
            => _cache.TryGetValue(key, out value);
    }
}
