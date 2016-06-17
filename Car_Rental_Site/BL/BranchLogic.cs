using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;

namespace BL
{
    public class BranchLogic : BaseLogic
    {
        /// <summary>
        /// Get all branches
        /// </summary>
        /// <returns></returns>
        public List<Branch> GetAllBranches()
        {
            return DB.Branches.ToList();
        }

        /// <summary>
        /// Get the details of one branch
        /// </summary>
        /// <param name="BranchID">the Branch ID</param>
        /// <returns>the details of the Branch</returns>
        public Branch GetOneBranchDetails(int? BranchID)
        {
            return DB.Branches.Where(m => m.BranchID == BranchID).SingleOrDefault();
        }

        /// <summary>
        /// Add new branch
        /// </summary>

        public void AddBranch(Branch Branch)
        {
            DB.Branches.Add(Branch);
            DB.SaveChanges();
        }

        /// <summary>
        /// Edit exist branch
        /// </summary>
        /// <param name="Branch">The branch details with the updates</param>
        public void EditBranch(Branch Branch)
        {
            DB.Branches.Add(Branch);
            DB.Entry(Branch).State = EntityState.Modified;
            DB.SaveChanges();
        }

        /// <summary>
        /// Delete branch
        /// </summary>
        /// <param name="Branch">The details of the branch to delete</param>
        public void DeleteBranch(Branch Branch)
        {
            DB.Entry(Branch).State = EntityState.Deleted;
            DB.SaveChanges();
        }

    }
}
