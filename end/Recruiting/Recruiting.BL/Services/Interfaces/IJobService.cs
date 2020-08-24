﻿using Recruiting.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Recruiting.BL.Services.Interfaces
{
    public interface IJobService : IServiceBase<Job>, ISortAndSearchService<Job>
    {
        public bool IsReferenceUnique(int id, string reference);
        public Task<int> GetNumberOfApplicationsByJobReference(string reference);
        Task<(int Id, string Title)?> GetIdAndTitleByReference(string jobReference);
    }
}
