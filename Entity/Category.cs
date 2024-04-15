using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public enum CategoryType
    {
        Food,
        Drinks,
        Snacks,
        Course
    }


    public class Category
    {
        public int Id { get; set; }
        public CategoryType Type { get; set; }
    }
}
