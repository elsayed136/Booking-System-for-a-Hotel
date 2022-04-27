using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        private readonly ApplicationDbContext _db;
        public BranchRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Branch branch)
        {
            _db.Branches.Update(branch);
        }
    }
}
