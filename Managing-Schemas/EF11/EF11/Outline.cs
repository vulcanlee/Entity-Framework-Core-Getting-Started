using System;
using System.Collections.Generic;

namespace EF11
{
    public partial class Outline
    {
        public int OutlineId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;

        public virtual Course Course { get; set; } = null!;
    }
}
