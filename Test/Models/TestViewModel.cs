using System.Collections.Generic;

namespace Test.Models
{
    public class TestViewModel
    {
        public enum Types
        {
            Type1,
            Type2
        }

        public Types SelectType { get; set; }

        public List<SubTestViewModel> Sub { get; set; }
    }

    public class SubTestViewModel
    {
        public TestViewModel.Types SelectType { get; set; }

        public List<SubTestViewModel> Sub { get; set; }
    }
}