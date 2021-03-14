using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICrudDemo.ViewModel
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }
    }
}
