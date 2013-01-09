using System;
using System.Collections.Generic;
using System.Linq;

namespace StackExchange.Profiling.Storage
{
    /// <summary>
    /// Responsible for storing and retrieving data from associated storage(s)
    /// </summary>
    public class CompositeStorage : IStorage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryStorage"></param>
        /// <param name="shareStorage"></param>
        public CompositeStorage(IStorage primaryStorage, IStorage shareStorage)
        {
            IsInitializaed = false;

            PrimaryStorage = primaryStorage;
            ShareStorage = shareStorage;
        }

        /// <summary>
        /// Storage used to save data to
        /// </summary>
        public IStorage PrimaryStorage { get; set; }

        /// <summary>
        /// Storage used to save data to if "Share" command has arrived
        /// </summary>
        public IStorage ShareStorage { get; set; }

        public void Init()
        {
            if (!IsInitializaed)
            {
                if (PrimaryStorage != null) PrimaryStorage.Init();
                if (ShareStorage != null) ShareStorage.Init();
                IsInitializaed = true;
            }
        }

        public bool IsInitializaed { get; private set; }

        public IEnumerable<Guid> List(int maxResults, DateTime? start = null, DateTime? finish = null, ListResultsOrder orderBy = ListResultsOrder.Decending)
        {
            var primaryList = PrimaryStorage.List(maxResults, start, finish, orderBy);
            var shareList = ShareStorage.List(maxResults, start, finish, orderBy);

            return primaryList.Union(shareList);
        }

        public void Save(MiniProfiler profiler)
        {
            PrimaryStorage.Save(profiler);
        }

        public MiniProfiler Load(Guid id)
        {
            return PrimaryStorage.Load(id) ?? ShareStorage.Load(id);
        }

        public void SetUnviewed(string user, Guid id)
        {
            PrimaryStorage.SetUnviewed(user, id);
            ShareStorage.SetUnviewed(user, id);
        }

        public void SetViewed(string user, Guid id)
        {
            PrimaryStorage.SetViewed(user, id);
            ShareStorage.SetViewed(user, id);
        }

        public List<Guid> GetUnviewedIds(string user)
        {
            var primaryIds = PrimaryStorage.GetUnviewedIds(user);
            var shareIds = ShareStorage.GetUnviewedIds(user);

            return primaryIds.Union(shareIds).ToList();
        }

        /// <summary>
        /// Move profiler from primary storage to shared storage
        /// </summary>
        /// <param name="id"></param>
        public void Share(Guid id)
        {
            var profiler = PrimaryStorage.Load(id);

            if (profiler != null)
                ShareStorage.Save(profiler);
        }
    }
}
