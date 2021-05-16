using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace ElementDemos.Entities
{
    [Table("t_menu")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public int ParentId { get; set; }

        public string Path { get; set; }

        [Computed]
        [Write(false)]
        public IEnumerable<Menu> Children { get; set; }
    }
}
