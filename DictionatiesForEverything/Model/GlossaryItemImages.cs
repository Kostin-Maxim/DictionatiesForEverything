using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionatiesForEverything.Model
{
    public class GlossaryItemImages
    {
        public int Id { get; set; }
        public int ItmeId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int Position { get; set; }
    }
}
