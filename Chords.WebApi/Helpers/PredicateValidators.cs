using System;
using System.Linq;
using Chords.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Chords.WebApi.Helpers
{
    public class PredicateValidators
    {
        private readonly IDbContextFactory<ChordsDbContext> _pooledFactory;
        
        public PredicateValidators(IDbContextFactory<ChordsDbContext> pooledFactory)
        {
            _pooledFactory = pooledFactory;
        }
        public bool IsExist<T>(string id)
        {
            using var context = _pooledFactory.CreateDbContext();
            return context.Find(typeof(T), id) != null;
        }

        public bool IsValidDateTime(string dateTime)
        {
            return DateTime.TryParse(dateTime, out _);
        }
    }
}