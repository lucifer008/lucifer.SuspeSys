using SuspeSys.Domain;
using System.Collections.Generic;

namespace SuspeSys.Service
{
    public interface ICatService
    {
        // IList<Cat> GetCatList();
        // void AddCat();
        string TestRemoting();
        void TestTransaction();
        void AddEmployee(Employee employee);
    }
}
