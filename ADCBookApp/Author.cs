using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCBookApp
{
    public class Author
    {
        public int authorId { get; set; }
        public string authorName { get; set; }
        public DateTime birstDay { get; set; }
        public string homeTown { get; set; }

        public Author()
        {
        }

        public Author(int authorId, string authorName, DateTime birstDay, string homeTown)
        {
            this.authorId = authorId;
            this.authorName = authorName;
            this.birstDay = birstDay;
            this.homeTown = homeTown;
        }

        public Author(string authorName, DateTime birstDay, string homeTown)
        {
            this.authorName = authorName;
            this.birstDay = birstDay;
            this.homeTown = homeTown;
        }
    }
}
